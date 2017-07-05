// <copyright file="SentryAppender.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Properties;

    using EnsageSharp.Sandbox;

    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Repository.Hierarchy;

    using PlaySharp.Sentry;
    using PlaySharp.Sentry.Data;
    using PlaySharp.Toolkit.Logging;

    public class SentryAppender : AppenderSkeleton
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SentryAppender()
        {
            this.Cache = new MemoryCache("SentryAppender");
            this.Client = new SentryClient(Settings.Default.Logger, "Ensage.SDK");
            this.Client.User.Id = SandboxConfig.Config.Settings["ID"];
            this.Client.Tags["game_version"] = Game.BuildVersion.ToString();

            this.UpdateRepositories();

            this.UpdateUntil = DateTime.Now.AddSeconds(30);
            UpdateManager.SubscribeService(this.UpdateRepositories, 100);
        }

        private MemoryCache Cache { get; }

        private SentryClient Client { get; }

        private string[] Exclusions { get; } = { "EnsageSharp.Sandbox", "PlaySharp.Toolkit", "PlaySharp.Service" };

        private DateTime UpdateUntil { get; }

        public void Capture(Exception e)
        {
            this.Client.Capture(e);
        }

        public void Capture(SentryEvent e)
        {
            this.Client.Capture(e);
        }

        public void CaptureAsync(Exception e)
        {
            this.Client.CaptureAsync(e);
        }

        public void CaptureAsync(SentryEvent e)
        {
            this.Client.CaptureAsync(e);
        }

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

                var assemblyName = loggingEvent.Repository.Name;
                var assembly = AssemblyResolver.AssemblyCache.FirstOrDefault(e => e.AssemblyName?.Name == assemblyName);

                this.Client.Extra["Game"] =
                    new
                    {
                        GameMode = Game.GameMode.ToString(),
                        GameState = Game.GameState.ToString(),
                        Hero = ObjectManager.LocalHero?.HeroId.ToString(),
                        Ping = Game.Ping,
                        GameVersion = Game.BuildVersion,
                        GameTime = TimeSpan.FromSeconds(Game.GameTime),
                        LevelName = Game.ShortLevelName,
                        Assembly = assemblyName
                    };

                if (assembly?.Id > 0)
                {
                    this.Client.Tags["Id"] = assembly.Id.ToString();
                }

                // this.Client.Tags["Build"] = assembly?.Version;
                this.Client.Tags["Plugin"] = assemblyName;
                this.Client.Tags["Map"] = Game.ShortLevelName;
                this.Client.Tags["Hero"] = ObjectManager.LocalHero?.HeroId.ToString();

                this.Cache.Add(exception.Message, loggingEvent, DateTimeOffset.Now.AddMinutes(1));
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

        private void UpdateRepositories()
        {
            if (DateTime.Now > this.UpdateUntil)
            {
                UpdateManager.UnsubscribeService(this.UpdateRepositories);
                Log.Info($"Update completed");
            }

            foreach (var repository in LogManager.GetAllRepositories().Where(e => !this.Exclusions.Contains(e.Name)))
            {
                var hierarchy = repository as Hierarchy;
                if (hierarchy == null)
                {
                    return;
                }

                if (hierarchy.Root.Appenders.Contains(this))
                {
                    continue;
                }

                Log.Info($"LOG {hierarchy.Name}");
                hierarchy.Root.AddAppender(this);
            }
        }
    }
}