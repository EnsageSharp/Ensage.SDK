// <copyright file="SentryAppender.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.ComponentModel.Composition;
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
    using PlaySharp.Toolkit.Logging;

    [Export(typeof(SentryAppender))]
    public class SentryAppender : AppenderSkeleton
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public SentryAppender([Import] IServiceContext context)
        {
            this.Context = context;
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

        private IServiceContext Context { get; }

        private string[] Exclusions { get; } = { "EnsageSharp.Sandbox", "PlaySharp.Toolkit", "PlaySharp.Service" };

        private DateTime UpdateUntil { get; }

        public void Capture(Exception e)
        {
            this.UpdateMetadata(Assembly.GetCallingAssembly().GetName().Name);
            this.Client.Capture(e);
        }

        public void CaptureAsync(Exception e)
        {
            this.UpdateMetadata(Assembly.GetCallingAssembly().GetName().Name);
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
                if (this.IsCached(exception))
                {
                    return;
                }

                this.StoreException(exception);
                this.UpdateMetadata(loggingEvent.Repository.Name);
                this.CaptureAsync(exception);
            }
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            foreach (var loggingEvent in loggingEvents)
            {
                this.Append(loggingEvent);
            }
        }

        private bool IsCached(Exception e)
        {
            return this.Cache.Contains(e.StackTrace);
        }

        private void StoreException(Exception e, int seconds = 60)
        {
            this.Cache.Add(e.StackTrace, e, DateTimeOffset.Now.AddSeconds(seconds));
        }

        private void UpdateMetadata(string origin)
        {
            try
            {
                var assembly = AssemblyResolver.AssemblyCache.FirstOrDefault(e => e.AssemblyName?.Name == origin);

                if (assembly != null)
                {
                    if (assembly.Id > 0 && assembly.Id < 1000)
                    {
                        this.Client.Tags["Id"] = assembly.Id.ToString();
                    }
                    else
                    {
                        this.Client.Tags["Id"] = "local";
                    }

                    // this.Client.Tags["Build"] = assembly.Version;
                }

                this.Client.Tags["Plugin"] = origin;
                this.Client.Tags["Map"] = Game.ShortLevelName;
                this.Client.Tags["Unit"] = this.Context.Owner.ClassId.ToString();

                this.Client.Extra["Game"] =
                    new
                    {
                        GameMode = Game.GameMode.ToString(),
                        GameState = Game.GameState.ToString(),
                        Unit = this.Context.Owner.ClassId.ToString(),
                        Ping = Game.Ping,
                        GameVersion = Game.BuildVersion,
                        GameTime = TimeSpan.FromSeconds(Game.GameTime),
                        LevelName = Game.ShortLevelName,
                        Assembly = origin
                    };
            }
            catch (Exception e)
            {
                Log.Warn(e);
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
                try
                {
                    var hierarchy = repository as Hierarchy;
                    if (hierarchy == null)
                    {
                        continue;
                    }

                    if (hierarchy.Root.Appenders.Contains(this))
                    {
                        continue;
                    }

                    Log.Info($"LOG {hierarchy.Name}");
                    hierarchy.Root.AddAppender(this);
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }
    }
}