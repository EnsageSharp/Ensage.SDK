// <copyright file="item_flask.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_flask : RangedAbility, IHasTargetModifier
    {
        public item_flask(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_flask_healing";
    }
}