// <copyright file="AssemblyInfo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("b2151ad0-6f00-4bc9-adb9-46aa656670eb")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyMetadata("SENTRY_DSN", "https://d8b414df0f964a9c8f803dda0a8b6a98@sentry.ensage.io/2")]