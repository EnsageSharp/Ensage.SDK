// <copyright file="ITargetSelectorManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    public interface ITargetSelectorManager : IServiceManager<ITargetSelector, ITargetSelectorMetadata>, ITargetSelector
    {
        SDKConfig.TargetSelectorConfig Config { get; }
    }
}