using System;
using System.Security;

namespace Ensage.SDK.Service
{
    [SecuritySafeCritical]
    public sealed class EnsageServiceContext : IEnsageServiceContext
    {
        public EnsageServiceContext(Hero unit)
        {
            if (unit == null || !unit.IsValid)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            Owner = unit;
        }

        public Hero Owner { get; }

        public bool Equals(Hero other)
        {
            return Owner.Equals(other);
        }
    }
}