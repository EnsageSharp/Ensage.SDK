// <copyright file="ITextureManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    using Ensage.SDK.Renderer.Texture;

    public interface ITextureManager : IDisposable
    {
        void LoadAbilityFromDota(string abilityName, bool rounded = false);

        void LoadAbilityFromDota(AbilityId abilityId, bool rounded = false);

        void LoadFromDota(string textureKey, string file, TextureProperties properties = default);

        void LoadFromFile(string textureKey, string file, TextureProperties properties = default);

        void LoadFromResource(string textureKey, string file, TextureProperties properties = default);

        void LoadHeroFromDota(HeroId heroId, bool rounded = false, bool icon = false);

        void LoadHeroFromDota(string heroName, bool rounded = false, bool icon = false);

        void LoadUnitFromDota(string unitName, bool rounded = false);
    }
}