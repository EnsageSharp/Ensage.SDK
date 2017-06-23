// <copyright file="enigma_demonic_conversion.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    public class enigma_demonic_conversion : RangedAbility
    {
        public enigma_demonic_conversion(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var target = targets.First();
            if (target is Creep && !target.IsAncient)
            {
                return target.MaximumHealth;
            }

            return 0;
        }
    }
}