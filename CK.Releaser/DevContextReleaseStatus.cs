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
        public enum MainVersionStatus
        {
            /// <summary>
            /// The version is necessarily valid if there is no git repository.
            /// </summary>
            Valid,
            /// <summary>
            /// The <see cref="Workspace.MainVersion"/> does not match the <see cref="GitManager.ReleasedVersion"/>.
            /// </summary>
            FatalMismatchWithReleasedTag,
            /// <summary>
            /// There is no released tag (<see cref="GitManager.ReleasedVersion"/> is not valid) but there is a <see cref="GitManager.GetLastReleased"/> and the 
            /// current <see cref="Workspace.MainVersion"/> is smaller.
            /// </summary>
            TooSmallVersionFromLastReleased,
            /// <summary>
            /// There is a released tag and the repository is dirty: source version sould be increased.
            /// </summary>
            VersionMustBeUpgraded
        }

        public readonly string BranchName;
        public readonly string CommitSha;
        public readonly DateTime CommitUtcTime;
        public readonly VersionOnBranch ReleasedVersion;
        public readonly GitManager.ReleasedCommit LastReleased;
        /// <summary>
        /// The current version. Never null (defaults to 0.1.0).
        /// </summary>
        public readonly Version MainVersion;
        public readonly bool CanSetMainVersion;
        public readonly MainVersionStatus VersionStatus;
        public readonly bool ReleasedTagDifferentBranchError;
        public readonly bool CanCreateReleaseTag;

        public string DisplayMainVersion
        {
            get { return MainVersion.ToString(); }
        }

        public string CommitStandardTimeDisplay
        {
            get { return CommitUtcTime.ToString( Info.InfoReleaseDatabase.TimeFormat ); }
        }

        /// <summary>
        /// Branch name or version difference.
        /// </summary>
        public bool HasVersionError
        {
            get { return ReleasedTagDifferentBranchError || VersionStatus == MainVersionStatus.FatalMismatchWithReleasedTag; }
        }

        /// <summary>
        /// The <see cref="MainVersion"/> is too small.
        /// </summary>
        public bool HasVersionWarning
        {
            get { return VersionStatus == MainVersionStatus.TooSmallVersionFromLastReleased || VersionStatus == MainVersionStatus.VersionMustBeUpgraded; }
        }

        public DevContextReleaseStatus()
        {
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
            CanSetMainVersion = git.IsWorkingFolderWritable() && ctx.Workspace.CanSetMainVersion;
            BranchName = git != null ? git.CurrentBranchName : null;
            if( BranchName == null )
            {
                VersionStatus = MainVersionStatus.Valid;
                monitor.Warn().Send( "Missing or uninitialized Git repository." );
            }
            else
            {
                CommitSha = git.CommitSha;
                CommitUtcTime = git.CommitUtcTime;
                ReleasedVersion = git.ReleasedVersion;
                LastReleased = git.GetLastReleased();
                if( git.ReleasedVersion.IsValid )
                {
                    if( BranchName == "(no branch)" )
                    {
                        BranchName = git.ReleasedVersion.BranchName;
                    }
                    else if( git.ReleasedVersion.BranchName != BranchName )
                    {
                        ReleasedTagDifferentBranchError = true;
                        monitor.Fatal().Send( "Invalid Release Tag: Branch is '{0}' but released tag is '{1}'.", BranchName, git.ReleasedVersion );
                    }
                    // Check the version.
                    if( !git.IsDirty && !git.ReleasedVersion.Equals( MainVersion ) )
                    {
                        VersionStatus = MainVersionStatus.FatalMismatchWithReleasedTag;
                        monitor.Fatal().Send( "Invalid Release Tag: source version is {0} but released tag is '{1}'.", MainVersion, git.ReleasedVersion );
                    }
                    else
                    {
                        // Even if branch differ, in terms of version, it is valid.
                        // But if the repository is dirty, we are not really on the commit point, we must warn if 
                        // the current source version has not been upgraded.
                        if( git.IsDirty && git.ReleasedVersion.CompareTo( MainVersion ) >= 0 )
                        {
                            monitor.Warn().Send( "Current version '{0}' should be increased above '{1}'.", MainVersion, ReleasedVersion.ToStringWithoutBranchName() );
                            VersionStatus = MainVersionStatus.VersionMustBeUpgraded;
                        }
                        else VersionStatus = MainVersionStatus.Valid;
                    }
                }
                else
                {
                    // No current Released tag.
                    if( LastReleased != null && LastReleased.Version.CompareTo( MainVersion ) >= 0 )
                    {
                        monitor.Warn().Send( "Current version '{0}' must be greater than the last one '{1}'.", MainVersion, LastReleased.Version );
                        VersionStatus = MainVersionStatus.TooSmallVersionFromLastReleased;
                    }
                    else
                    {
                        VersionStatus = MainVersionStatus.Valid;
                        CanCreateReleaseTag = !git.IsDirty;
                        Debug.Assert( CanCreateReleaseTag == git.CanSetReleasedVersion ); 
                    }
                }
            }
        }

        public bool Equals( DevContextReleaseStatus x )
        {
            return x != null 
                    && BranchName == x.BranchName
                    && CommitSha == x.CommitSha
                    && CommitUtcTime == x.CommitUtcTime
                    && ReleasedVersion == x.ReleasedVersion
                    && LastReleased == x.LastReleased
                    && MainVersion == x.MainVersion
                    && CanSetMainVersion == x.CanSetMainVersion
                    && VersionStatus == x.VersionStatus
                    && ReleasedTagDifferentBranchError == x.ReleasedTagDifferentBranchError
                    && CanCreateReleaseTag == x.CanCreateReleaseTag;
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as DevContextReleaseStatus );
        }

        public override int GetHashCode()
        {
            return Util.Hash.Combine( Util.Hash.StartValue, BranchName, 
                                                            CommitSha, 
                                                            CommitUtcTime, 
                                                            ReleasedVersion,
                                                            LastReleased,
                                                            MainVersion, 
                                                            CanSetMainVersion, 
                                                            VersionStatus, 
                                                            ReleasedTagDifferentBranchError, 
                                                            CanCreateReleaseTag ).GetHashCode();
        }

        static public bool operator ==( DevContextReleaseStatus s1, DevContextReleaseStatus s2 )
        {
            return ReferenceEquals( s1, null ) ? ReferenceEquals( s2, null ) : s1.Equals( s2 );
        }

        static public bool operator !=( DevContextReleaseStatus s1, DevContextReleaseStatus s2 )
        {
            return !(s1 == s2);
        }


    }

}
