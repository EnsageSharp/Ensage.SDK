// <copyright file="SentryTarget.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using NLog;
    using NLog.Targets;

    using PlaySharp.Sentry;

    [Target("Sentry")]
    public class SentryTarget : TargetWithLayout
    {
        private readonly HashSet<int> cache = new HashSet<int>();

        public SentryTarget(Assembly assembly, AssemblyMetadata metadata)
        {
            this.Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            this.Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));

            if (string.IsNullOrEmpty(metadata.SentryKey))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if (string.IsNullOrEmpty(metadata.SentryProject))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            this.Client = new SentryClient($"https://{metadata.SentryKey}@sentry.ensage.io/{metadata.SentryProject}");
            this.Client.Client.Compression = true;
            this.Client.Client.Release = metadata.Commit;
            this.Client.Client.Environment = metadata.Channel;
            this.Client.Client.Logger = assembly.GetName().Name;

            this.Client.Tags["Id"] = () => metadata.Id;
            this.Client.Tags["Channel"] = () => metadata.Channel;
            this.Client.Tags["Version"] = () => metadata.Version;
            this.Client.Tags["Build"] = () => metadata.Build;
            this.Client.Tags["Commit"] = () => metadata.Commit;
        }

        public Assembly Assembly { get; }

        public SentryClient Client { get; }

        public AssemblyMetadata Metadata { get; }

        protected override void Write(LogEventInfo logEvent)
        {
            if (logEvent.Exception == null || this.Client == null)
            {
                return;
            }

            var id = logEvent.Exception.ToString().GetHashCode();
            if (this.cache.Contains(id))
            {
                return;
            }

            this.cache.Add(id);
            this.Client.CaptureAsync(logEvent.Exception);
        }
    }
}