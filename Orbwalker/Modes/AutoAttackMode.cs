// <copyright file="AutoAttackMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    public abstract class AutoAttackMode : OrbwalkingMode
    {
        protected AutoAttackMode(IOrbwalker orbwalker)
            : base(orbwalker)
        {
        }

        public override void Execute()
        {
            // turning
            if (this.Orbwalker.TurnEndTime > Game.RawGameTime)
            {
                return;
            }

            var target = this.GetTarget();

            // move
            if ((target == null || !this.Orbwalker.CanAttack(target)) && this.Orbwalker.CanMove())
            {
                this.Orbwalker.Move(Game.MousePosition);
                return;
            }

            // attack
            if (target != null && this.Orbwalker.CanAttack(target))
            {
                this.Orbwalker.Attack(target);
            }
        }

        protected abstract Unit GetTarget();
    }
}