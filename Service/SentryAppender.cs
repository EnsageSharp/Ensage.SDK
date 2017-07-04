// <copyright file="SentryAppender.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;

    using Ensage.SDK.Properties;

    using EnsageSharp.Sandbox;

    using log4net.Appender;
    using log4net.Core;

    using SharpRaven;
    using SharpRaven.Data;

    public class SentryAppender : AppenderSkeleton, ISentryUserFactory
    {
        public SentryAppender()
        {
            this.User = new SentryUser(SandboxConfig.Config.Settings["ID"]);
            this.Client = new RavenClient(Settings.Default.Logger, sentryUserFactory: this);
            this.Client.Compression = true;
            this.Client.Logger = "Ensage.SDK";
        }

        private RavenClient Client { get; }

        private SentryUser User { get; }

        public SentryUser Create()
        {
            return this.User;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level != Level.Fatal)
            {
                return;
            }

            var exception = loggingEvent.ExceptionObject ?? loggingEvent.MessageObject as Exception;
            if (exception != null)
            {
                this.Client.CaptureAsync(new SentryEvent(exception));
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