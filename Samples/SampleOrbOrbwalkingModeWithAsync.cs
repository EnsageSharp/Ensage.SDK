// <copyright file="SampleOrbOrbwalkingModeWithAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Abilities.npc_dota_hero_drow_ranger;
    using Ensage.SDK.Input;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Orbwalker.Modes;
    using Ensage.SDK.TargetSelector;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class SampleOrbOrbwalkingModeWithAsync : KeyPressOrbwalkingModeAsync
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ITargetSelectorManager targetSelectorManager;

        private drow_ranger_frost_arrows frostArrows;

        [ImportingConstructor]
        public SampleOrbOrbwalkingModeWithAsync(IOrbwalker orbwalker, IInputManager input, ITargetSelectorManager targetSelectorManager)
            : base(orbwalker, input, Key.Space)
        {
            this.targetSelectorManager = targetSelectorManager;
        }

        public override async Task ExecuteAsync(CancellationToken token)
        {
            Log.Debug($"ExecuteAsynce");
            var target = this.targetSelectorManager.Active.GetTargets().FirstOrDefault(x => x.Distance2D(this.Owner) <= this.frostArrows.Range);
            if (target != null)
            {
                if (!target.HasModifier(this.frostArrows.TargetModifierName))
                {
                    this.frostArrows.UseAbility(target);
                    await Task.Delay((int)(this.frostArrows.CastPoint * 1000.0f), token);
                    return;
                }
            }

            this.Orbwalker.OrbwalkTo(target);
        }

        protected override void OnActivate()
        {
            this.frostArrows = new drow_ranger_frost_arrows(this.Owner.GetAbilityById(AbilityId.drow_ranger_frost_arrows));
            base.OnActivate();
        }
    }
}