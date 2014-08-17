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
        public readonly Version SimpleModeVersion;
        public readonly string PreReleaseVersion;
        public readonly string BuildMetadataVersion;


        public string DisplayVersion
        {
            get
            {
                string text = SimpleModeVersion.ToString();
                if( !String.IsNullOrEmpty( PreReleaseVersion ) ) text += '-' + PreReleaseVersion;
                if( !String.IsNullOrEmpty( BuildMetadataVersion ) ) text += '+' + BuildMetadataVersion;
                return text;
            }
        }

        public string CommitStandardTimeDisplay
        {
            get { return CommitUtcTime.ToString( Info.InfoReleaseDatabase.TimeFormat ); }
        }

        public DevContextReleaseStatus()
        {
            StatusText = "Unitialized status.";
            CommitUtcTime = Util.UtcMinValue;
            SimpleModeVersion = new Version( 0, 1, 0 );
        }

        public DevContextReleaseStatus( IActivityMonitor monitor, IDevContext ctx )
        {
            GitManager git = ctx.GitManager;
            BranchName = git != null ? git.CurrentBranchName : null;
            if( BranchName == null )
            {
                StatusText = "Missing or uninitialized Git repository.";
            }
            else
            {
                CommitSha = git.CommitSha;
                CommitUtcTime = git.CommitUtcTime;
                if( ctx.InfoReleaseDatabase == null )
                {
                    StatusText = "No Info release Database.";
                }
                else
                {
                    if( BranchName == "(no branch)" )
                    {
                        BranchName = null;
                        StatusText = "Not on a valid branch.";
                        if( ctx.InfoReleaseDatabase != null )
                        {
                            ReadyToRelease = ctx.InfoReleaseDatabase.FindByCommit( monitor, CommitSha );
                            if( ReadyToRelease != null )
                            {
                                CurrentBranch = ReadyToRelease.Branch;
                                StatusText += " An info release is available.";
                            }
                            StatusText += " No info release exists for this commit.";
                        }
                        else StatusText += " No Info release Database.";
                    }
                    else
                    {
                        CurrentBranch = ctx.InfoReleaseDatabase.FindBranch( ctx.Workspace.SolutionCKFile.SolutionName, BranchName );
                        if( CurrentBranch == null )
                        {
                            CanCreateCurrentBranch = true;
                            StatusText = String.Format( "Branch '{0}' has no associated information yet.", BranchName );
                        }
                        else
                        {
                            StatusText = String.Format( "Info release for '{0}' is available.", BranchName );
                            if( git.IsDirty )
                            {
                                StatusText += String.Format( " The working folder is dirty. It must be cleaned before releasing '{0}'.", BranchName );
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
            SimpleModeVersion = ctx.Workspace.VersionFileManager.SimpleModeVersion;
            if( SimpleModeVersion == null ) SimpleModeVersion = new Version( 0, 1, 0 );
            CanSetSimpleModeVersion = ctx.IsWorkingFolderWritable() && ctx.Workspace.VersionFileManager.CanSetSimpleModeVersion;
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
                    && SimpleModeVersion == x.SimpleModeVersion
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
            return Util.Hash.Combine( Util.Hash.StartValue, BranchName, SimpleModeVersion, PreReleaseVersion, BuildMetadataVersion, CanSetSimpleModeVersion, CommitSha, CommitUtcTime, CurrentBranch, ReadyToRelease, StatusText, CanReadyToReleaseCurrent, CanCreateCurrentBranch ).GetHashCode();
        }

        public override string ToString()
        {
            return StatusText;
        }
    }

}
