// <copyright file="kunkka_torrent.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_kunkka
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class kunkka_torrent : CircleAbility, IHasTargetModifier
    {
        public kunkka_torrent(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("delay");
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_kunkka_torrent";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "torrent_damage");
            }
        }
    }
}