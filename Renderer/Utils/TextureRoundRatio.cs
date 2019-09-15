// <copyright file="TextureRoundRatio.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Utils
{
    using System.Collections.Generic;

    internal class TextureRoundRatio
    {
        private readonly Dictionary<string, float> ratios = new Dictionary<string, float>
        {
            { "npc_dota_roshan_rounded", 0.65f },
            { "npc_dota_hero_antimage_rounded", 0.8f },
            { "npc_dota_hero_axe_rounded", 0.7f },
            { "npc_dota_hero_bloodseeker_rounded", 0.6f },
            { "npc_dota_hero_crystal_maiden_rounded", 0.7f },
            { "npc_dota_hero_drow_ranger_rounded", 0.3f },
            { "npc_dota_hero_earthshaker_rounded", 0.4f },
            { "npc_dota_hero_juggernaut_rounded", 0.4f },
            { "npc_dota_hero_morphling_rounded", 0.2f },
            { "npc_dota_hero_phantom_lancer_rounded", 0.2f },
            { "npc_dota_hero_puck_rounded", 0.9f },
            { "npc_dota_hero_pudge_rounded", 0.3f },
            { "npc_dota_hero_razor_rounded", 0.9f },
            { "npc_dota_hero_sven_rounded", 0.7f },
            { "npc_dota_hero_zuus_rounded", 0.6f },
            { "npc_dota_hero_kunkka_rounded", 0.3f },
            { "npc_dota_hero_lina_rounded", 0.2f },
            { "npc_dota_hero_lion_rounded", 0.4f },
            { "npc_dota_hero_shadow_shaman_rounded", 0.1f },
            { "npc_dota_hero_tidehunter_rounded", 0.1f },
            { "npc_dota_hero_witch_doctor_rounded", 0.3f },
            { "npc_dota_hero_lich_rounded", 0.3f },
            { "npc_dota_hero_riki_rounded", 0.8f },
            { "npc_dota_hero_enigma_rounded", 0.7f },
            { "npc_dota_hero_tinker_rounded", 0.5f },
            { "npc_dota_hero_sniper_rounded", 0.4f },
            { "npc_dota_hero_necrolyte_rounded", 0.8f },
            { "npc_dota_hero_venomancer_rounded", 0.7f },
            { "npc_dota_hero_dragon_knight_rounded", 0.9f },
            { "npc_dota_hero_skeleton_king_rounded", 0.3f },
            { "npc_dota_hero_death_prophet_rounded", 1f },
            { "npc_dota_hero_pugna_rounded", 0.6f },
            { "npc_dota_hero_templar_assassin_rounded", 0.3f },
            { "npc_dota_hero_rattletrap_rounded", 0.6f },
            { "npc_dota_hero_leshrac_rounded", 0.8f },
            { "npc_dota_hero_life_stealer_rounded", 0.75f },
            { "npc_dota_hero_clinkz_rounded", 0.4f },
            { "npc_dota_hero_omniknight_rounded", 0.3f },
            { "npc_dota_hero_huskar_rounded", 0.3f },
            { "npc_dota_hero_night_stalker_rounded", 0.7f },
            { "npc_dota_hero_broodmother_rounded", 0.4f },
            { "npc_dota_hero_ancient_apparition_rounded", 0.6f },
            { "npc_dota_hero_ursa_rounded", 0.2f },
            { "npc_dota_hero_jakiro_rounded", 0.9f },
            { "npc_dota_hero_weaver_rounded", 0.6f },
            { "npc_dota_hero_batrider_rounded", 0.7f },
            { "npc_dota_hero_spectre_rounded", 0.8f },
            { "npc_dota_hero_spirit_breaker_rounded", 0.6f },
            { "npc_dota_hero_gyrocopter_rounded", 0.3f },
            { "npc_dota_hero_alchemist_rounded", 0f },
            { "npc_dota_hero_invoker_rounded", 0.4f },
            { "npc_dota_hero_lycan_rounded", 0.65f },
            { "npc_dota_hero_brewmaster_rounded", 0.75f },
            { "npc_dota_hero_shadow_demon_rounded", 0.6f },
            { "npc_dota_hero_lone_druid_rounded", 1f },
            { "npc_dota_hero_chaos_knight_rounded", 0.4f },
            { "npc_dota_hero_meepo_rounded", 0.8f },
            { "npc_dota_hero_treant_rounded", 0.4f },
            { "npc_dota_hero_ogre_magi_rounded", 0.2f },
            { "npc_dota_hero_undying_rounded", 0.3f },
            { "npc_dota_hero_disruptor_rounded", 0.3f },
            { "npc_dota_hero_naga_siren_rounded", 0.3f },
            { "npc_dota_hero_keeper_of_the_light_rounded", 0.2f },
            { "npc_dota_hero_wisp_rounded", 0.3f },
            { "npc_dota_hero_slark_rounded", 0.4f },
            { "npc_dota_hero_medusa_rounded", 0.4f },
            { "npc_dota_hero_troll_warlord_rounded", 0.2f },
            { "npc_dota_hero_magnataur_rounded", 0.6f },
            { "npc_dota_hero_shredder_rounded", 0.7f },
            { "npc_dota_hero_bristleback_rounded", 0.3f },
            { "npc_dota_hero_tusk_rounded", 0.3f },
            { "npc_dota_hero_skywrath_mage_rounded", 0.2f },
            { "npc_dota_hero_abaddon_rounded", 0.4f },
            { "npc_dota_hero_legion_commander_rounded", 0.3f },
            { "npc_dota_hero_techies_rounded", 0.1f },
            { "npc_dota_hero_ember_spirit_rounded", 0.2f },
            { "npc_dota_hero_oracle_rounded", 0.7f },
            { "npc_dota_hero_winter_wyvern_rounded", 0.6f },
            { "npc_dota_hero_monkey_king_rounded", 0.2f },
            { "npc_dota_hero_dark_willow_rounded", 0.4f },
            { "npc_dota_hero_pangolier_rounded", 0.8f },
            { "npc_dota_hero_grimstroke_rounded", 0.9f },
            { "npc_dota_hero_mars_rounded", 0.4f },
        };

        public float GetRatio(string textureKey)
        {
            if (!this.ratios.TryGetValue(textureKey, out var ratio))
            {
                ratio = 0.5f;
            }

            return ratio;
        }
    }
}