// <copyright file="Hotkey.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;
    using System.Reflection;
    using System.Windows.Input;

    

    using PlaySharp.Toolkit.Helper.Annotations;
    using NLog;

    public class Hotkey
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly Action<KeyEventArgs> action;

        public Hotkey([NotNull] string name, Key key, [NotNull] Action<KeyEventArgs> action)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.Name = name;
            this.Key = key;
            this.action = action;
        }

        public Key Key { get; set; }

        public string Name { get; }

        public void Execute(KeyEventArgs args)
        {
            // Log.Debug($"Hotkey: {this.Name} {this.Key}");
            this.action(args);
        }
    }
}