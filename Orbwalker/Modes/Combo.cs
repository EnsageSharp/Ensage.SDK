// <copyright file="Combo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;
    using System.Windows.Input;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.TargetSelector;

    [ExportOrbwalkingMode] // export your mode
    public class MyHeroComboMode : AutoAttackMode
    {
        // IOrbwalkingMode
        [ImportingConstructor]
        public MyHeroComboMode([Import] IOrbwalker orbwalker, [Import] ITargetSelectorManager targetSelector)
            : base(orbwalker, targetSelector)
        {
            // IOrbwalker & ITargetSelectorManager are required base services 
            this.Config = new MyHeroConfig(); // create menu
            this.TestSpell = this.Owner.Spellbook.SpellQ; // create spells etc
        }

        public override bool CanExecute => Game.IsKeyDown(Key.Space); // guard to control Execute() execution

        public MyHeroConfig Config { get; }

        private Ability TestSpell { get; }

        public override void Execute()
        {
            // GetTarget() can be overriden - default impl gets first target from ITargetSelectorManager.Active.GetTargets().FirstOrDefault();
            var target = this.GetTarget();

            // casting
            if (target != null && this.TestSpell.CanBeCasted(target))
            {
                this.TestSpell.UseAbility(target);
            }

            // movement / attack
            base.Execute();
        }
    }

    public class MyHeroConfig
    {
        public MyHeroConfig()
        {
            this.Factory = MenuFactory.Create("My Hero"); // create Main Menu
            this.Test = this.Factory.Item("iMeh is Gay", true); // create MenuItem or Menu
        }

        public MenuFactory Factory { get; }

        public MenuItem<bool> Test { get; }
    }
}