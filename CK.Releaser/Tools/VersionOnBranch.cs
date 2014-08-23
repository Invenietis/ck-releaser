using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser
{
    /// <summary>
    /// Captures Major.Minor.Patch-BranchName pattern.
    /// This is NOT a semantic version, this is the version associated to a commit 
    /// in the repository.
    /// When comparing, only Major.Minor.Patch is used: BrancName has no special meaning.
    /// </summary>
    public struct VersionOnBranch : IEquatable<VersionOnBranch>, IEquatable<Version>, IComparable<VersionOnBranch>, IComparable<Version>
    {
        /// <summary>
        /// When <see cref="IsValid"/> is true, necessarily greater or equal to 0.
        /// </summary>
        public readonly int Major;
        /// <summary>
        /// When <see cref="IsValid"/> is true, necessarily greater or equal to 0.
        /// </summary>
        public readonly int Minor;
        /// <summary>
        /// When <see cref="IsValid"/> is true, necessarily greater or equal to 0.
        /// </summary>
        public readonly int Patch;
        /// <summary>
        /// When <see cref="IsValid"/> is true, necessarily not null, nor empty nor be full of white spaces.
        /// </summary>
        public readonly string BranchName;

        /// <summary>
        /// Gets whether this <see cref="VersionOnBranch"/> is valid.
        /// </summary>
        public bool IsValid
        {
            get { return BranchName != null; }
        }

        /// <summary>
        /// Initializes a new <see cref="VersionOnBranch"/>. See <see cref="IsBasicallyValid"/>.
        /// </summary>
        /// <param name="major">Major must be greater or equal to 0.</param>
        /// <param name="minor">Minor must be greater or equal to 0.</param>
        /// <param name="patch">Patch must be greater or equal to 0.</param>
        /// <param name="branchName">Not null, nor empty nor be full of white spaces.</param>
        public VersionOnBranch( int major, int minor, int patch, string branchName )
        {
            if( !IsBasicallyValid( major, minor, patch, branchName ) ) throw new ArgumentException();
            Major = major;
            Minor = minor;
            Patch = patch;
            BranchName = branchName;
        }

        /// <summary>
        /// Comparing: only Major.Minor.Patch is used: BrancName has no special meaning.
        /// </summary>
        /// <param name="other">Other to compare to.</param>
        /// <returns></returns>
        public int CompareTo( VersionOnBranch other )
        {
            int cmp = Major - other.Major;
            if( cmp == 0 ) cmp = Minor - other.Minor;
            if( cmp == 0 )  cmp = Patch - other.Patch;
            return cmp;
        }
        
        /// <summary>
        /// Comparing this a standard <see cref="Version"/> (<see cref="Version.Revision"/> is ignored.
        /// </summary>
        /// <param name="other">Other to compare to.</param>
        /// <returns></returns>
        public int CompareTo( Version other )
        {
            int cmp = Major - other.Major;
            if( cmp == 0 ) cmp = Minor - other.Minor;
            if( cmp == 0 )  cmp = Patch - other.Build;
            return cmp;
        }
        
        public bool Equals( VersionOnBranch other )
        {
            return Major == other.Major && Minor == other.Minor && Patch == other.Patch && BranchName == other.BranchName;
        }

        public bool Equals( Version other )
        {
            return Major == other.Major && Minor == other.Minor && Patch == other.Build;
        }

        public override int GetHashCode()
        {
            return Util.Hash.Combine( Util.Hash.StartValue, Major, Minor, Patch, BranchName ).GetHashCode();
        }

        public override bool Equals( object obj )
        {
            return obj is VersionOnBranch 
                    ? Equals( (VersionOnBranch)obj ) 
                    : (obj is Version ? Equals( (Version)obj ) : false );
        }

        static public bool operator ==( VersionOnBranch v1, VersionOnBranch v2 )
        {
            return v1.Equals( v2 );
        }

        static public bool operator !=( VersionOnBranch v1, VersionOnBranch v2 )
        {
            return !v1.Equals( v2 );
        }

        /// <summary>
        /// Gets the string version (without 'v' prefix) and without <see cref="BranchName"/>.
        /// </summary>
        /// <returns>The Major.Minor.Patch.</returns>
        public string ToStringWithoutBranchName()
        {
            return String.Format( CultureInfo.InvariantCulture, "{0}.{1}.{2}", Major, Minor, Patch );
        }

        /// <summary>
        /// Gets the string version (without 'v' prefix).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format( CultureInfo.InvariantCulture, "{0}.{1}.{2}-{3}", Major, Minor, Patch, BranchName );
        }

        /// <summary>
        /// Light check of validity (exact rules for a branch is reduced to: not null, nor empty nor be full of white spaces).
        /// It is useless to check for: https://www.kernel.org/pub/software/scm/git/docs/git-check-ref-format.html since in our case 
        /// the branch name is already created in the git repository.
        /// </summary>
        /// <param name="major">Major must be greater or equal to 0.</param>
        /// <param name="minor">Minor must be greater or equal to 0.</param>
        /// <param name="patch">Patch must be greater or equal to 0.</param>
        /// <param name="branchName">Not null, nor empty nor be full of white spaces.</param>
        /// <returns>True when valid, false otherwise.</returns>
        public static bool IsBasicallyValid( int major, int minor, int patch, string branchName )
        {
            return major >= 0 && minor >= 0 && patch >= 0 && !String.IsNullOrWhiteSpace( branchName );
        }


        static Regex _regex = new Regex( @"^v?(?<1>0|[1-9][0-9]*)\.(?<2>0|[1-9][0-9]*)\.(?<3>0|[1-9][0-9]*)-(?<4>\w+)$", RegexOptions.Compiled|RegexOptions.CultureInvariant );

        /// <summary>
        /// Attempts to parse a string like "4.0.0-master" or "v1.0-5-develop". The branch must be composed of at least one (non space) char.
        /// Numbers can not start with a 0 (except if it is 0).
        /// </summary>
        /// <param name="s">String to parse.</param>
        /// <param name="v">Resulting version.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool TryParse( string s, out VersionOnBranch v )
        {
            v = TryParse( s );
            return v.IsValid;
        }

        /// <summary>
        /// Parses a string like "4.0.0-master" or "v1.0-5-develop". The branch must be composed of at least one (non space) char.
        /// Numbers can not start with a 0 (except if it is 0).
        /// Returns a VersionOnBranch where <see cref="VersionOnBranch.IsValid"/> is false if the string is not valid.
        /// </summary>
        /// <param name="s">String to parse.</param>
        /// <returns>Resulting version (can be invalid).</returns>
        public static VersionOnBranch TryParse( string s )
        {
            Match m = _regex.Match( s );
            if( m.Success )
            {
                return new VersionOnBranch( Int32.Parse( m.Groups[1].Value ), Int32.Parse( m.Groups[2].Value ), Int32.Parse( m.Groups[3].Value ), m.Groups[4].Value );
            }
            return new VersionOnBranch();
        }

    }
}
