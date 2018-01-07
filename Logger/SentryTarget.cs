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

    using SharpRaven.Data;

    [Target("Sentry")]
    public class SentryTarget : TargetWithLayout
    {
        protected static readonly IDictionary<LogLevel, ErrorLevel> LoggingMap =
            new Dictionary<LogLevel, ErrorLevel>
                {
                    { LogLevel.Trace, ErrorLevel.Debug },
                    { LogLevel.Debug, ErrorLevel.Debug },
                    { LogLevel.Info, ErrorLevel.Info },
                    { LogLevel.Warn, ErrorLevel.Warning },
                    { LogLevel.Error, ErrorLevel.Error },
                    { LogLevel.Fatal, ErrorLevel.Fatal },
                };

        public SentryTarget(Assembly assembly, AssemblyMetadata metadata)
        {
            this.Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            this.Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));

            if (!string.IsNullOrEmpty(metadata.SentryKey) && !string.IsNullOrEmpty(metadata.SentryProject))
            {
                this.Client = new SentryClient($"https://{metadata.SentryKey}@sentry.ensage.io/{metadata.SentryProject}");
                this.Client.Client.Compression = true;
                this.Client.Client.Release = metadata.Commit;
                this.Client.Client.Environment = metadata.Channel;
                this.Client.Client.Logger = assembly.GetName().Name;

                this.Client.Tags["Id"] = () => metadata.Id.ToString();
                this.Client.Tags["Channel"] = () => metadata.Channel;
                this.Client.Tags["Version"] = () => metadata.Version.ToString();
                this.Client.Tags["Build"] = () => metadata.Build.ToString();
                this.Client.Tags["Commit"] = () => metadata.Commit;
            }
        }

        public Assembly Assembly { get; }

        public SentryClient Client { get; }

        public AssemblyMetadata Metadata { get; }

        protected override void Write(LogEventInfo logEvent)
        {
            if (logEvent.Exception == null)
            {
                return;
            }

            this.Client.CaptureAsync(logEvent.Exception);
        }
    }
}