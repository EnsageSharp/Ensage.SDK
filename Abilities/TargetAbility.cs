using System.Linq;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities
{
    public abstract class TargetAbility : BaseAbility
    {
        protected TargetAbility(Ability ability) : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var level = Ability.Level;  
            if (level == 0)
                return 0;
            var damage = Ability.GetDamage(level - 1);

            var target = targets.First();
            if (!Ability.CanAffectTarget(target))
                return 0;

            var amplify = Ability.GetSpellAmp();
            var reduction = Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }

        public virtual float Range 
        {
            get
            {
                var castRange = (float) Ability.CastRange;

                var unit = Ability.Owner as Unit;
                if (unit != null)
                {
                    // items
                    var aetherLense = unit.GetItemById(AbilityId.item_aether_lens);
                    if (aetherLense != null)
                    {
                        castRange += aetherLense.GetAbilitySpecialData("cast_range_bonus");
                    }

                    // talents
                    foreach (var talent in unit.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_") ))
                    {
                        castRange += talent.GetAbilitySpecialData("value");
                    }
                }

                return castRange;
            }
        }
    }
}
