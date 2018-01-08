// <copyright file="AssemblyMetadata.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Logger
{
    using System.Collections.Generic;

    public class AssemblyMetadata
    {
        public string Build { get; internal set; }

        public string Channel { get; internal set; }

        public string Commit { get; internal set; }

        public Dictionary<string, string> Extra { get; } = new Dictionary<string, string>();

        public string Id { get; internal set; }

        public string SentryKey { get; internal set; }

        public string SentryProject { get; internal set; }

        public string Version { get; internal set; }
    }
}