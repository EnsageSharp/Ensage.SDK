namespace Ensage.SDK.Service
{
    public interface IEnsageServiceContext : IServiceContext
    {
        Hero Owner { get; }
    }
}