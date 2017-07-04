// <copyright file="SentryAppender.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Properties;

    using EnsageSharp.Sandbox;

    using log4net.Appender;
    using log4net.Core;

    using PlaySharp.Sentry;

    public class SentryAppender : AppenderSkeleton
    {
        public SentryAppender()
        {
            this.Client = new SentryClient(Settings.Default.Logger, "Ensage.SDK");
            this.Client.User.Id = SandboxConfig.Config.Settings["ID"];
        }

        private HashSet<string> CaptureCache { get; } = new HashSet<string>();

        private SentryClient Client { get; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level < Level.Error)
            {
                return;
            }

            var exception = loggingEvent.ExceptionObject ?? loggingEvent.MessageObject as Exception;
            if (exception != null && !this.CaptureCache.Contains(exception.Message))
            {
                this.CaptureCache.Add(exception.Message);
                this.Client.CaptureAsync(exception);
            }
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            foreach (var loggingEvent in loggingEvents)
            {
                this.Append(loggingEvent);
            }
        }
    }
}