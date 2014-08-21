#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\DevContextReleaseStatus.cs) is part of CiviKey. 
*  
* CiviKey is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* CiviKey is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with CiviKey.  If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2014, 
*     Invenietis <http://www.invenietis.com>,
*     In’Tech INFO <http://www.intechinfo.fr>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser
{
    /// <summary>
    /// Encapsulated, immutable status in terms release capabilities of a <see cref="IDevContext"/>.
    /// </summary>
    public class DevContextReleaseStatus : IEquatable<DevContextReleaseStatus>
    {
        public readonly string BranchName;
        public readonly string CommitSha;
        public readonly DateTime CommitUtcTime;
        public readonly VersionOnBranch ReleasedVersion;
        public readonly Info.BranchRelease CurrentBranch;
        public readonly Info.InfoRelease ReadyToRelease;
        public readonly string StatusText;
        public readonly bool CanReadyToReleaseCurrent;
        public readonly bool CanCreateCurrentBranch;
        public readonly bool CanSetSimpleModeVersion;
        public bool CanSetPreReleaseVersion
        {
            get { return ReadyToRelease != null; }
        }
        public bool CanSetBuildMetadataVersion
        {
            get { return ReadyToRelease != null; }
        }

        /// <summary>
        /// The current version. Never null (defaults to 0.1.0).
        /// </summary>
        public readonly Version MainVersion;
        public readonly string PreReleaseVersion;
        public readonly string BuildMetadataVersion;


        public string DisplayMainVersion
        {
            get { return MainVersion.ToString(); }
        }

        public string CommitStandardTimeDisplay
        {
            get { return CommitUtcTime.ToString( Info.InfoReleaseDatabase.TimeFormat ); }
        }

        public DevContextReleaseStatus()
        {
            StatusText = "Unitialized status.";
            CommitUtcTime = Util.UtcMinValue;
            MainVersion = new Version( 0, 1, 0 );
        }

        public DevContextReleaseStatus( IActivityMonitor monitor, IDevContext ctx )
        {
            MainVersion = ctx.Workspace.MainVersion;
            if( MainVersion == null ) 
            {
                monitor.Warn().Send( "Missing Main Version. Defaults to 1.0.1." );
                MainVersion = new Version( 0, 1, 0 );
            }
            GitManager git = ctx.GitManager;
            BranchName = git != null ? git.CurrentBranchName : null;
            if( BranchName == null )
            {
                monitor.Warn().Send( "Missing or uninitialized Git repository." );
            }
            else
            {
                CommitSha = git.CommitSha;
                CommitUtcTime = git.CommitUtcTime;
                ReleasedVersion = git.ReleasedVersion;
                if( git.ReleasedVersion.IsValid )
                {
                    if( BranchName == "(no branch)" )
                    {
                        BranchName = git.ReleasedVersion.BranchName;
                    }
                    else if( git.ReleasedVersion.BranchName != BranchName )
                    {
                        monitor.Fatal().Send( "Invalid Release Tag: Branch is '{0}' but released tag is '{1}'.", BranchName, git.ReleasedVersion );
                    }
                    if( !git.ReleasedVersion.Equals( MainVersion ) )
                    {
                        monitor.Fatal().Send( "Invalid Release Tag: source version is {0} but released tag is '{1}'.", MainVersion, git.ReleasedVersion );
                    }
                }

                if( ctx.InfoReleaseDatabase == null )
                {
                    monitor.Warn().Send( "No Info release Database." );
                }
                else
                {
                    if( BranchName == "(no branch)" )
                    {
                        BranchName = null;
                        monitor.Warn().Send( "Current head is not Branch nor a Released commit." );
                    }
                    else
                    {
                        CurrentBranch = ctx.InfoReleaseDatabase.FindBranch( ctx.Workspace.SolutionCKFile.SolutionName, BranchName );
                        if( CurrentBranch == null )
                        {
                            if( ReleasedVersion.IsValid )
                            {
                                monitor.Fatal().Send( "Branch '{0}' does not exist in the ReleaseInfo database but a Release tag exits on the commit.", BranchName );
                            }
                            else
                            {
                                CanCreateCurrentBranch = true;
                                monitor.Info().Send( "Branch '{0}' has no release yet.", BranchName );
                            }
                        }
                        else
                        {
                            monitor.Trace().Send( "Info release for '{0}' is available.", BranchName );
                            if( git.IsDirty )
                            {
                                monitor.Info().Send( "The working folder is dirty. Commit your work before creating a 'v{0}-{1}' release.", MainVersion.ToString(), BranchName );
                            }
                            else
                            {
                                ReadyToRelease = ctx.InfoReleaseDatabase.FindByCommit( monitor, CommitSha );
                                if( ReadyToRelease != null )
                                {
                                    StatusText += " Current commit is ready to be released.";
                                }
                                else
                                {
                                    CanReadyToReleaseCurrent = true;
                                    StatusText += " Information release can be created for this commit.";
                                }
                            }
                        }
                    }
                }
            }
            // Version management.
            CanSetSimpleModeVersion = ctx.IsWorkingFolderWritable() && ctx.Workspace.VersionFileManager.CanSetSharedAssemblyInfoVersion;
            if( ReadyToRelease != null )
            {
                var data = ReadyToRelease.GetData( monitor );
                if( data != null )
                {
                    PreReleaseVersion = data.PreReleaseVersion;
                    BuildMetadataVersion = data.BuildMetadataVersion;
                }
                else StatusText += " Error while reading release data!";
            }
        }

        public bool Equals( DevContextReleaseStatus x )
        {
            return BranchName == x.BranchName
                    && CurrentBranch == x.CurrentBranch
                    && ReadyToRelease == x.ReadyToRelease
                    && CommitSha == x.CommitSha
                    && CommitUtcTime == x.CommitUtcTime
                    && StatusText == x.StatusText
                    && CanReadyToReleaseCurrent == x.CanReadyToReleaseCurrent
                    && CanCreateCurrentBranch == x.CanCreateCurrentBranch
                    && CanSetSimpleModeVersion == x.CanSetSimpleModeVersion
                    && MainVersion == x.MainVersion
                    && PreReleaseVersion == x.PreReleaseVersion
                    && BuildMetadataVersion == x.BuildMetadataVersion;
        }

        public override bool Equals( object obj )
        {
            var x = obj as DevContextReleaseStatus;
            return x != null && Equals( x );
        }

        public override int GetHashCode()
        {
            return Util.Hash.Combine( Util.Hash.StartValue, BranchName, MainVersion, PreReleaseVersion, BuildMetadataVersion, CanSetSimpleModeVersion, CommitSha, CommitUtcTime, CurrentBranch, ReadyToRelease, StatusText, CanReadyToReleaseCurrent, CanCreateCurrentBranch ).GetHashCode();
        }

        public override string ToString()
        {
            return StatusText;
        }
    }

}
