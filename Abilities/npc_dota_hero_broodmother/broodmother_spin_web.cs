// <copyright file="broodmother_spin_web.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_broodmother
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class broodmother_spin_web : AreaOfEffectAbility, IHasModifier
    {
        public broodmother_spin_web([NotNull] Ability ability)
            : base(ability)
        {
        }

        public string ChargeModifierName { get; } = "modifier_broodmother_spin_web_charge_counter";

        public int Charges
        {
            get
            {
                var modifier = this.Owner.GetModifierByName(this.ChargeModifierName);
                return modifier?.StackCount ?? 0;
            }
        }

        public int Count
        {
            get
            {
                return EntityManager<Unit>.Entities.Count(x => x.ClassId == ClassId.CDOTA_Unit_Broodmother_Web && x.IsControllable);
            }
        }

        public int MaxCharges
        {
            get
            {
                return (int)this.Ability.GetAbilitySpecialData("max_charges");
            }
        }

        public int MaxCount
        {
            get
            {
                var count = this.Ability.GetAbilitySpecialData("count");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_broodmother_1);
                if (talent != null && talent.Level > 0)
                {
                    count += talent.GetAbilitySpecialData("value");
                }

                return (int)count;
            }
        }

        public string ModifierName { get; } = "modifier_broodmother_spin_web";
    }
}