// <copyright file="AbilityFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Service;

    using PlaySharp.Toolkit.Helper.Annotations;

    [Export(typeof(AbilityFactory))]
    public class AbilityFactory
    {
        [ImportingConstructor]
        public AbilityFactory([Import] IServiceContext context)
        {
            this.Context = context;

            this.Types = Assembly.GetExecutingAssembly()
                                 .GetExportedTypes()
                                 .Where(e => !e.IsAbstract && typeof(BaseAbility).IsAssignableFrom(e))
                                 .ToArray();
        }

        private IServiceContext Context { get; }

        private Type[] Types { get; }

        public BaseAbility GetAbility([NotNull] Ability ability)
        {
            if (ability == null)
            {
                throw new ArgumentNullException(nameof(ability));
            }

            var abilityTypeName = ability.Id.ToString();
            var type = this.Types.FirstOrDefault(e => e.Name == abilityTypeName);

            if (type == null)
            {
                throw new AbilityNotImplementedException($"Could not find {nameof(BaseAbility)} implementation for {abilityTypeName}");
            }

            return (BaseAbility)Activator.CreateInstance(type, ability);
        }

        public BaseAbility GetAbility(AbilityId id)
        {
            var ability = this.Context.Owner.Spellbook.Spells.FirstOrDefault(e => e.Id == id);

            if (ability == null)
            {
                throw new AbilityNotFoundException($"Could not find {id} for {this.Context}");
            }

            return this.GetAbility(ability);
        }
    }
}