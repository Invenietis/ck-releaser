#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\CSProjectObject.cs) is part of CiviKey. 
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CK.Core;
using Mono.Cecil;

namespace CK.Releaser
{
    public class CSProjectObject : VSProjectBase, VersionFileManager.IVersionedByVFile
    {
        public readonly CSProjectObject AboveProject;
        public readonly XDocument XmlDocument;

        IReadOnlyList<CSProjectReference> _references;
        bool _isDirty;
        VersionFileManager.VFile _versionHolderFile;

        internal CSProjectObject( Workspace w, string filePath, CSProjectObject aboveProject = null )
            : base( w, filePath )
        {
            AboveProject = aboveProject;
            XmlDocument = XDocument.Load( filePath );
            XmlDocument.Changed += XmlDocument_Changed;
        }

        void XmlDocument_Changed( object sender, XObjectChangeEventArgs e )
        {
            _isDirty = true;
        }

        public override bool IsDirty
        {
            get { return _isDirty; }
        }

        public override void Save( IActivityMonitor m )
        {
            if( _isDirty && Workspace.DevContext.IsWorkingFolderWritable() )
            {
                try
                {
                    m.Info().Send( "Saving file '{0}'.", WorkspaceBasedPath );
                    XmlDocument.Save( ProjectFilePath );
                    _isDirty = false;
                }
                catch( Exception ex )
                {
                    m.Error().Send( ex );
                }
            }
        }

        public bool SignAssembly
        {
            get { return StringComparer.OrdinalIgnoreCase.Equals( SingleStringValue( VSMP.SignAssembly ), "true" ); }
        }

        public void EnsureSignAssembly( bool signing = true )
        {
            var e = XmlDocument.Root.Descendants( VSMP.SignAssembly ).FirstOrDefault();
            if( e == null )
            {
                if( !signing ) return;
                e = new XElement( VSMP.PropertyGroup, new XElement( VSMP.SignAssembly, "true" ) );
                InsertAfterFirstPropertyGroup( e );
            }
            else
            {
                e.Value = signing ? "true" : "false";
            }
        }


        public string AssemblyOriginatorKeyFile
        {
            get { return SingleStringValue( VSMP.AssemblyOriginatorKeyFile ); }
        }

        public void EnsureAssemblyOriginatorKeyFileUseSharedKey()
        {
            var e = XmlDocument.Root.Descendants( VSMP.AssemblyOriginatorKeyFile ).FirstOrDefault();
            if( e == null )
            {
                e = new XElement( VSMP.PropertyGroup, new XElement( VSMP.AssemblyOriginatorKeyFile, ExpectedRelativePathToSolutionSharedKey ) );
                InsertAfterFirstPropertyGroup( e );
            }
            else
            {
                e.Value = ExpectedRelativePathToSolutionSharedKey;
            }
        }

        /// <summary>
        /// Gets the VFile that holds the version for this project.
        /// In simple versioning mode, it is always Solution.VersionFileManager.SharedAssemblyInfo.
        /// </summary>
        public VersionFileManager.VFile VersionHolderFile
        {
            get { return _versionHolderFile; }
        }

        void VersionFileManager.IVersionedByVFile.OnVersionChanged()
        {
            var e = XmlDocument.Root.Descendants( VSMP.ApplicationVersion ).FirstOrDefault();
            if( e != null )
            {
                var v = _versionHolderFile.Version;
                v = new Version( v.Major, v.Minor, v.Build, 0 );
                e.SetValue( v.ToString() );
            }
        }

        /// <summary>
        /// Gets the name of "Properties" folder if it exists, null otherwise.
        /// </summary>
        public string DesignerFolder
        {
            get { return SingleStringValue( VSMP.AppDesignerFolder ); }
        }

        /// <summary>
        /// If <see cref="DesignerFolder"/> exists, ensures that the path belongs to it.
        /// </summary>
        public string DesignerFolderPrefix( string path )
        {
            string designerFolder = DesignerFolder;
            var link = String.IsNullOrEmpty( designerFolder ) ? String.Empty : FileUtil.NormalizePathSeparator( designerFolder, true );
            return link += path;
        }

        public Guid ProjectGuid
        {
            get 
            {
                var s = SingleStringValue( VSMP.ProjectGuid );
                if( s == null ) return Guid.Empty;
                Guid id;
                Guid.TryParse( s, out id );
                return id;
            }
        }

        public string OutputType
        {
            get { return SingleStringValue( VSMP.OutputType ); }
        }

        public string AssemblyName
        {
            get { return SingleStringValue( VSMP.AssemblyName ); }
        }

        public void SetAssemblyName( string name )
        {
            var e = XmlDocument.Root.Descendants( VSMP.AssemblyName ).First();
            e.Value = name;
        }

        public string TargetFrameworkVersion
        {
            get { return SingleStringValue( VSMP.TargetFrameworkVersion ); }
        }

        public bool HasFodyTargets
        {
            get { return XmlDocument.Root.Elements( VSMP.Import ).Attributes( VSMP.ProjectAttribute ).Any( a => a.Value.EndsWith( "Fody.targets" ) ); }
        }

        public bool FodyWeaverFileContainsCKStamp
        {
            get 
            { 
                string p = Path.Combine( ThisDirectoryPath, "FodyWeavers.xml" );
                if( !File.Exists( p ) ) return false;
                return File.ReadAllText( p ).Contains( "CK.Stamp" );
            }
        }

        /// <summary>
        /// Gets the PublishUrl.
        /// If it exists, it must be the <see cref="ExpectedPublishUrl"/>: Release/ClickOnce/[ProjectFileName].
        /// </summary>
        public string PublishUrl
        {
            get { return SingleStringValue( VSMP.PublishUrl ); }
        }

        /// <summary>
        /// Sets the <see cref="PublishUrl"/> to the <see cref="ExpectedPublishUrl"/>.
        /// </summary>
        public void SetPublishUrl()
        {
            var e = XmlDocument.Root.Descendants( VSMP.PublishUrl ).FirstOrDefault();
            if( e == null ) throw new InvalidOperationException( "PublishUrl must already exists." );
            e.SetValue( ExpectedPublishUrl );
        }

        /// <summary>
        /// Maps "v4.0" to "Net40". It is the identifier that is used to name things.
        /// </summary>
        public string TargetFrameworkVersionIdentifier
        {
            get 
            { 
                switch( SingleStringValue( VSMP.TargetFrameworkVersion ) )
                {
                    case "v4.0": return "Net40";
                    case "v4.5": return "Net45";
                    default: throw new CKException( "Unknown target framework: " + TargetFrameworkVersion );
                }
            }
        }

        public string RelativePathToSolutionFolder
        {
            get 
            {
                string p = String.Empty;
                for( int i = 0; i < SubDirectories.Count; ++i ) p += "..\\";
                return p; 
            }
        }

        public string ExpectedPublishUrl
        {
            get { return RelativePathToSolutionFolder + @"Release\ClickOnce\" + ProjectFileName; }
        }

        public string ExpectedRelativePathToSolutionSharedKey
        {
            get { return RelativePathToSolutionFolder + CKMP.SharedKeyFileName; }
        }

        public string ExpectedRelativePathToSolutionSharedAssemblyInfo
        {
            get { return RelativePathToSolutionFolder + CKMP.SharedAssemblyInfoFileName; }
        }

        public bool IsTestAssemblyOrContainedInATestProject
        {
            get
            {
                return HasNUnitFrameworkReference || IsContainedInATestProject;
            }
        }

        public bool IsContainedInATestProject
        {
            get { return AboveProject != null && AboveProject.IsTestAssemblyOrContainedInATestProject; }
        }

        string SingleStringValue( XName name )
        {
            var k = XmlDocument.Root.Descendants( name ).FirstOrDefault();
            if( k == null ) return null;
            return k.Value;
        }

        public void InsertAfterFirstPropertyGroup( XElement e )
        {
            var first = XmlDocument.Root.Elements( VSMP.PropertyGroup ).FirstOrDefault();
            if( first != null ) first.AddAfterSelf( e );
            else XmlDocument.Root.Add( e );
        }

        public void InsertAfterFirstItemGroup( XElement e )
        {
            var first = XmlDocument.Root.Elements( VSMP.PropertyGroup ).FirstOrDefault();
            if( first != null ) first.AddAfterSelf( e );
            else XmlDocument.Root.Add( e );
        }

        public IReadOnlyList<CSProjectReference> GetProjectReferences( bool refresh = false )
        {
            if( _references == null || refresh )
            {
                var pRef = XmlDocument.Root.Descendants( VSMP.ProjectReference ).Select( e => new CSProjectProjectReference( e ) );
                var aRef = XmlDocument.Root.Descendants( VSMP.Reference ).Select( e => new CSProjectAssemblyReference( this, e ) );
                _references = pRef.Cast<CSProjectReference>().Concat( aRef ).ToArray();
            }
            return _references;
        }

        public IEnumerable<XElement> CompileElements
        {
            get { return XmlDocument.Root.Elements( VSMP.ItemGroup ).Elements( VSMP.Compile ); }
        }

        public IEnumerable<XElement> SharedAssemblyInfoCompileElements
        {
            get { return CompileElements.Where( e => e.GetAttribute( VSMP.IncludeAttribute, String.Empty ).EndsWith( CKMP.SharedAssemblyInfoFileName ) ); }
        }

        public bool HasNUnitFrameworkReference
        {
            get { return GetProjectReferences().OfType<CSProjectAssemblyReference>().Any( r => r.Include.StartsWith( "nunit.framework", StringComparison.OrdinalIgnoreCase ) ); }
        }
        
        public IEnumerable<CSProjectAssemblyReference> GetUnsignedReferencedAssembliesWithHintPathResolved( IActivityMonitor m )
        {
            return GetProjectReferences()
                            .OfType<CSProjectAssemblyReference>()
                            .Where( r => r.HintPath != null 
                                    && (!r.Include.Contains( "PublicKeyToken=" ) || r.Include.Contains( "PublicKeyToken=null" ))
                                    && r.GetHintPathResolved( m ) != null ); 
        }

        internal void Validate( ValidationContext ctx )
        {
            var m = ctx.Monitor;
            using( m.OpenInfo().Send( ValidationContext.ContextProcessingTag, "Validating '{0}.csproj'.", ProjectFileName ) )
            {
                if( OutputType == "Library" || OutputType == "WinExe"  || OutputType == "Exe" )
                {
                    if( !CheckAssemblyName( m ) )
                    {
                        ctx.Add( new Fixes.AssemblyNameMustMatchProjectName( this ) );
                    }

                    // All projects can have a VersionFileManager.VFile.
                    LocateVersionHolderFile( m );
                    if( _versionHolderFile != null ) _versionHolderFile.Register( this );

                    // All projects can be ClickOnce... ClickOnce projects must have a PublishUrl in Release/ folder.
                    var publishUrl = PublishUrl;
                    if( publishUrl != null )
                    {
                        if( publishUrl != ExpectedPublishUrl )
                        {
                            m.Error().Send( "The PublishUrl must be '{0}', not '{1}'.", ExpectedPublishUrl, publishUrl );
                            ctx.Add( new Fixes.EnsureStandardPublishUrl( this ) );
                        }
                    }

                    // Do not require a test assembly to be signed nor to have installed CK.Stamp nor to be bound tp a VersionFileManager.VFile.
                    if( IsTestAssemblyOrContainedInATestProject )
                    {
                        #region IsTestAssemblyOrContainedInATestProject
                        if( IsContainedInATestProject )
                        {
                            // An assembly contained in a Test assembly has no constraint (except the AssemblyName above)
                            // and the reference to the SharedAssemblyInfo below.
                        }
                        else
                        {
                            // A test assembly must have a name that ends with ".Tests" and
                            // its folder must be correcly named also.
                            Debug.Assert( HasNUnitFrameworkReference );
                            if( !ProjectFileName.EndsWith( ".Tests" ) && !ProjectFileName.EndsWith( ".Tests." + TargetFrameworkVersionIdentifier ) )
                            {
                                m.Error().Send( "A test library (that references nunit.framework) name must end with '.Tests'." );
                            }
                            else CheckFolderName( m );
                        }
                        #endregion
                    }
                    else
                    {
                        #region "Normal" project...
                        CheckFolderName( m );
                        if( !SignAssembly )
                        {
                            m.Error().Send( "The assembly should be strong named (<SigningAssembly>true</SigningAssembly>) and uses the {0}.", CKMP.SharedKeyFileName );
                            ctx.Add( new Fixes.AssemblyMustBeSigned( this ) );
                        }
                        else
                        {
                            if( AssemblyOriginatorKeyFile != ExpectedRelativePathToSolutionSharedKey )
                            {
                                m.Error().Send( "The strong naming must use the shared key '{0}'.", ExpectedRelativePathToSolutionSharedKey );
                                ctx.Add( new Fixes.AssemblyMustBeSigned( this ) );
                            }
                        }
                        // Since assembly must be signed, the assemblies that it references must be signed.
                        // This fix automatically this classical problem.
                        var unsignedReferences = GetUnsignedReferencedAssembliesWithHintPathResolved( m );
                        bool fixNeeded = false;
                        foreach( var uref in unsignedReferences )
                        {
                            AssemblyDefinition d = ctx.GetAssembly( m, uref.GetHintPathResolved( m ).FullName );
                            if( d != null && d.Name.PublicKeyToken == null || d.Name.PublicKeyToken.Length == 0 )
                            {
                                fixNeeded = true;
                                m.Error().Send( "Referenced assembly: '{0}' is not signed. It can be signed with the {1}.", d.Name, CKMP.SharedKeyFileName );
                            }
                        }
                        if( fixNeeded ) ctx.Add( new Fixes.AssemblyReferencesMustBeSigned( this ) );

                        if( !HasFodyTargets || !FodyWeaverFileContainsCKStamp )
                        {
                            m.Error().Send( "Please install CK.Stamp.Fody NuGet package in '{0}'.", ProjectFileName );
                        }

                        if( _versionHolderFile == null )
                        {
                            m.Error().Send( "Unable to locate the file that defines the AssemblyVersion attribute. Such a file can be named 'AssemblyInfo.cs' or any '*VersionInfo.cs' file." );
                        }
                        #endregion
                    }
                    // All projects must reference the SharedAssemblyInfo.
                    CheckSharedAssemblyInfo( ctx, m );
                }
                else m.Warn().Send( "OutputType not handled: '{0}'.", OutputType );
            }
        }

        /// <summary>
        /// Tries to locate the VFile and sets it.
        /// It is not an error here if it can not be found (whether this is an error or not depends on the project's type).
        /// </summary>
        void LocateVersionHolderFile( IActivityMonitor m )
        {
            if( !Workspace.SolutionCKFile.VersioningModeSimple ) throw new NotImplementedException( "Multiple Versioning mode is not implemented." );
            _versionHolderFile = Workspace.VersionFileManager.SharedAssemblyInfo;
        }

        /// <summary>
        /// SharedAssemblyInfo.cs: One and only one must exist and it must reference the root one.
        /// </summary>
        void CheckSharedAssemblyInfo( ValidationContext ctx, IActivityMonitor m )
        {
            bool mustFixSharedAssemblyInfo = false;
            var allShared = SharedAssemblyInfoCompileElements.ToList();
            if( allShared.Count > 1 )
            {
                m.Error().Send( "Multiple {0} exits.", CKMP.SharedAssemblyInfoFileName );
                mustFixSharedAssemblyInfo = true;
            }
            else if( allShared.Count == 0 )
            {
                m.Error().Send( "Missing {0}.", CKMP.SharedAssemblyInfoFileName );
                mustFixSharedAssemblyInfo = true;
            }
            else
            {
                var e = allShared[0];
                string fileRef = (string)e.Attribute( VSMP.IncludeAttribute );
                if( !StringComparer.OrdinalIgnoreCase.Equals( fileRef, ExpectedRelativePathToSolutionSharedAssemblyInfo ) )
                {
                    m.Error().Send( "{0} must be an alias to the root file.", CKMP.SharedAssemblyInfoFileName );
                    mustFixSharedAssemblyInfo = true;
                }
            }
            if( mustFixSharedAssemblyInfo )
            {
                ctx.Add( new Fixes.SharedAssemblyInfoMustBeReferenced( this ) );
            }
        }

        bool CheckAssemblyName( IActivityMonitor m )
        {
            if( AssemblyName == ProjectFileName ) return true;
            if( StringComparer.OrdinalIgnoreCase.Equals( AssemblyName + "." + TargetFrameworkVersionIdentifier, ProjectFileName ) ) return true;
            m.Error().Send( "The AssemblyName '{0}' must be the same as the .csproj file name '{1}'.", AssemblyName, ProjectFileName );
            return false;
        }

        bool CheckFolderName( IActivityMonitor m )
        {
            if( SubDirectories.Last() == AssemblyName ) return true;
            m.Error().Send( "The project folder /{0} must be the same as the .csproj file name '{1}'.", SubDirectories.Last(), ProjectFileName );
            return false;
        }

    }
}
