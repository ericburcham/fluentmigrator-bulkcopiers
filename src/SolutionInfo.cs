
using System.Reflection;
using System;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCopyright("Copyright - Eric Burcham 2017")]
[assembly: AssemblyProduct("FluentMigrator Bulk Copiers")]
[assembly: CLSCompliant(true)]


//  We use Semantic Versioning:  http://semver.org/
//  Given a version number MAJOR.MINOR.PATCH.REVISION, increment the:
//
//      MAJOR Version - When you make incompatible API changes,
//      MINOR Version - When you add functionality in a backwards-compatible manner, and
//      PATCH Version - When you make backwards-compatible bug fixes.
//      REVISION Version - Never.
[assembly: AssemblyFileVersion("1.0.1.0")]
[assembly: AssemblyVersion("1.0.1.0")]
