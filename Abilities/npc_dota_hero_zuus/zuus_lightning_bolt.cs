// <copyright file="zuus_lightning_bolt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;

    using log4net;
    using PlaySharp.Toolkit.Logging;

    public class zuus_lightning_bolt : RangedAbility, IAreaOfEffectAbility
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly zuus_static_field staticField;

        public zuus_lightning_bolt(Ability ability)
            : base(ability)
        {
            var passive = this.Owner.GetAbilityById(AbilityId.zuus_static_field);
            if (passive != null)
            {
                this.staticField = new zuus_static_field(passive);
            }
        }

        public bool UseAbility(Unit target, bool onlyTarget = false)
        {
            if (!this.CanBeCasted)
            {
                Log.Debug($"blocked {this}");
                return false;
            }

            bool result;
            if ((this.Ability.AbilityBehavior & AbilityBehavior.NoTarget) == AbilityBehavior.NoTarget)
            {
                result = this.Ability.UseAbility();
            }
            else if ((this.Ability.AbilityBehavior & AbilityBehavior.Point) == AbilityBehavior.Point && !onlyTarget)
            {
                result = this.Ability.UseAbility(target.NetworkPosition);
            }
            else
            {
                result = this.Ability.UseAbility(target);
            }

            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("spread_aoe");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return this.RawDamage;
            }

            return base.GetDamage(targets) + (this.staticField?.GetDamage(targets) ?? 0);
        }
    }
}
