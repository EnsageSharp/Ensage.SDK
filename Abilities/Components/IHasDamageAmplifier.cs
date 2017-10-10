using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.SDK.Abilities.Components
{
    interface IHasDamageAmplifier
    {
        DamageType AmplifierType { get; }

        float Value { get; }

    }
}
