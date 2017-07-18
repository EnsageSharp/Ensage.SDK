// <copyright file="SamplePlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("SamplePlugin", StartupMode.Manual)]
    public class SamplePlugin : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public SamplePlugin([Import] IServiceContext context)
        {
            this.Context = context;
        }

        public IServiceContext Context { get; }

        protected override void OnActivate()
        {
            Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
        }

        private async void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            Console.WriteLine($"pre {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(100);
            Console.WriteLine($"post {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}