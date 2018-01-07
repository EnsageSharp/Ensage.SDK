// <copyright file="Logging.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using NLog.Targets.Wrappers;

    public static class Logging
    {
        public static LoggingConfiguration Config { get; private set; }

        public static LoggingRule ConsoleRule { get; private set; }

        public static ColoredConsoleTarget ConsoleTarget { get; private set; }

        public static Dictionary<Assembly, SentryTarget> Targets { get; } = new Dictionary<Assembly, SentryTarget>();

        public static void Init()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;

            Config = new LoggingConfiguration();

            ConsoleTarget = new ColoredConsoleTarget();
            ConsoleTarget.Layout = "${date:format=HH\\:mm\\:ss} | ${pad:padding=-6:inner=${level:uppercase=true}}| ${message}";

            Config.AddTarget("console", new AsyncTargetWrapper(ConsoleTarget));
            ConsoleRule = new LoggingRule("*", LogLevel.Trace, ConsoleTarget);

            Config.LoggingRules.Add(ConsoleRule);

            LogManager.Configuration = Config;
            LogManager.ReconfigExistingLoggers();
        }

        private static void Add(Assembly assembly, AssemblyMetadata metadata, string @namespace)
        {
            Console.WriteLine($"SentryTarget({@namespace})");

            var target = new SentryTarget(assembly, metadata);
            var rule = new LoggingRule($"{@namespace}*", LogLevel.Trace, target) { Final = true };

            Targets[assembly] = target;

            Config.AddTarget(@namespace, target);
            Config.LoggingRules.Add(rule);

            LogManager.ReconfigExistingLoggers();
        }

        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            var assembly = args.LoadedAssembly;
            var metadata = args.LoadedAssembly.GetMetadata();

            if (metadata == null)
            {
                return;
            }

            Add(assembly, metadata, assembly.GetName().Name);
        }
    }
}