// <copyright file="Logging.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using NLog.Targets.Wrappers;

    public static class Logging
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static LoggingConfiguration Config { get; private set; }

        public static LoggingRule ConsoleRule { get; private set; }

        public static ColoredConsoleTarget ConsoleTarget { get; private set; }

        public static Dictionary<Assembly, SentryTarget> Targets { get; } = new Dictionary<Assembly, SentryTarget>();

        public static void Init()
        {
            Config = new LoggingConfiguration();

            ConsoleTarget = new ColoredConsoleTarget();
            ConsoleTarget.Layout = "${date:format=HH\\:mm\\:ss} | ${pad:padding=-6:inner=${level:uppercase=true}}| ${message}";

            Config.AddTarget("console", new AsyncTargetWrapper(ConsoleTarget));
            ConsoleRule = new LoggingRule("*", LogLevel.Trace, ConsoleTarget);

            Config.LoggingRules.Add(ConsoleRule);

            LogManager.Configuration = Config;
            LogManager.ReconfigExistingLoggers();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.GlobalAssemblyCache))
            {
                Add(assembly);
            }

            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }

        private static void Add(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                var metadata = assembly.GetMetadata();
                var @namespace = assembly.GetName().Name;

                if (string.IsNullOrEmpty(metadata?.SentryKey) || string.IsNullOrEmpty(metadata?.SentryProject))
                {
                    //Log.Info($"Skipped ({@namespace}) missing SentryKey or SentryProject");
                    return;
                }

                var target = new SentryTarget(assembly, metadata);
                var rule = new LoggingRule($"{@namespace}*", LogLevel.Trace, target) { Final = true };

                Targets[assembly] = target;

                Config.AddTarget(@namespace, target);
                Config.LoggingRules.Add(rule);

                Log.Info($"SentryTarget({@namespace})");
                LogManager.ReconfigExistingLoggers();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Add(args.LoadedAssembly);
        }
    }
}