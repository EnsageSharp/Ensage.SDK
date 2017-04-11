// <copyright file="IAssemblyLoader.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;

    public interface IAssemblyLoader
    {
        void Activate();

        void Deactivate();
    }

    [ExportAssembly("Test", HeroId.npc_dota_hero_axe)]
    public class Test : IAssemblyLoader
    {
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }
    }
}