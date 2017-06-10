// <copyright file="SamplePluginWithAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Prediction;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("SamplePluginWithAsync (pudge_meat_hook)", HeroId.npc_dota_hero_pudge)]
    public class SamplePluginWithAsync : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public SamplePluginWithAsync([Import] IServiceContext context, [Import] ITargetSelectorManager selector, [Import] IPrediction prediction)
        {
            this.Selector = selector;
            this.Prediction = prediction;

            // this.Ability = new PredictionAbility(context.Owner as Hero, AbilityId.pudge_meat_hook, prediction);
        }

        // private PredictionAbility Ability { get; }
        private IPrediction Prediction { get; }

        private ITargetSelectorManager Selector { get; }

        protected override void OnActivate()
        {
            UpdateManager.BeginInvoke(this.TestLoop);
        }

        private async void TestLoop()
        {
            while (this.IsActive)
            {
                try
                {
                    var target = this.Selector.Active.GetTargets().FirstOrDefault();

                    if (target != null)
                    {
                        // this.Ability.Use(target);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                await Task.Delay(100);
            }
        }
    }
}