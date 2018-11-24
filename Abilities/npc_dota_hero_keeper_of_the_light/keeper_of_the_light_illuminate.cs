// <copyright file="keeper_of_the_light_illuminate.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Extensions;

    public class keeper_of_the_light_illuminate : LineAbility, IChannable, IHasHealthRestore
    {
        public keeper_of_the_light_illuminate(Ability ability)
            : base(ability)
        {
            var illuminateEnd = this.Owner.GetAbilityById(AbilityId.keeper_of_the_light_illuminate_end);
            this.IlluminateEndAbility = new keeper_of_the_light_illuminate_end(illuminateEnd);
        }

        public float ChannelDuration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                return this.Ability.GetChannelTime(level - 1);
            }
        }

        public keeper_of_the_light_illuminate_end IlluminateEndAbility { get; }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        public float RemainingDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.ChannelDuration - this.Ability.ChannelTime;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "total_damage");
            }
        }

        public override bool UseAbility()
        {
            return this.IlluminateEndAbility.UseAbility();
        }

        public float TotalHealthRestore
        {
            get
            {
                // Heals the same amount as raw damage.
                return RawDamage;
            }
        }
    }
}