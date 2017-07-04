// <copyright file="SentryAppender.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Runtime.Caching;

    using Ensage.SDK.Properties;

    using EnsageSharp.Sandbox;

    using log4net.Appender;
    using log4net.Core;

    using PlaySharp.Sentry;

    public class SentryAppender : AppenderSkeleton
    {
        public SentryAppender()
        {
            this.Cache = new MemoryCache("SentryAppender");
            this.Client = new SentryClient(Settings.Default.Logger, "Ensage.SDK");
            this.Client.User.Id = SandboxConfig.Config.Settings["ID"];
        }

        private MemoryCache Cache { get; }

        private SentryClient Client { get; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level < Level.Error)
            {
                return;
            }

            var exception = loggingEvent.ExceptionObject ?? loggingEvent.MessageObject as Exception;
            if (exception != null)
            {
                if (this.Cache.Contains(exception.Message))
                {
                    return;
                }

                this.Cache.Add(new CacheItem(exception.Message), new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1) });
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