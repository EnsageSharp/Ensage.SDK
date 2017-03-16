using System.ComponentModel;
using System.Security;

namespace Ensage.SDK.Service
{
    [SecuritySafeCritical]
    public interface IServiceMetadata
    {
        [DefaultValue("")]
        string Description { get; }

        [DefaultValue("Default")]
        string Name { get; }

        [DefaultValue("")]
        string Version { get; }
    }
}