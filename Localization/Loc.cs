// <copyright file="Loc.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

// ReSharper disable InconsistentNaming
// ReSharper disable StyleCop.SA1602
namespace Ensage.SDK.Localization
{
    using System.Runtime.Serialization;

    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public enum Loc
    {
        [EnumMember(Value = "#DOTA_AASystem_Green")]
        DOTA_AASystem_Green,

        [EnumMember(Value = "#DOTA_AASystem_Red")]
        DOTA_AASystem_Red,

        [EnumMember(Value = "#DOTA_AASystem_Yellow")]
        DOTA_AASystem_Yellow,

        [EnumMember(Value = "#DOTA_AHC_BestFullChallengeValue")]
        DOTA_AHC_BestFullChallengeValue,

        [EnumMember(Value = "#DOTA_AHC_BestFullChallengeValueNone")]
        DOTA_AHC_BestFullChallengeValueNone,

        [EnumMember(Value = "#DOTA_AbilityBuild_Talent_Title")]
        DOTA_AbilityBuild_Talent_Title,

        [EnumMember(Value = "#DOTA_AbilityDraftTimeLeft")]
        DOTA_AbilityDraftTimeLeft,

        [EnumMember(Value = "#DOTA_AbilityDraftYourTurn")]
        DOTA_AbilityDraftYourTurn,

        [EnumMember(Value = "#DOTA_AbilityDraftYourTurnIn")]
        DOTA_AbilityDraftYourTurnIn,

        [EnumMember(Value = "#DOTA_AbilityDraft_GameStarts")]
        DOTA_AbilityDraft_GameStarts,

        [EnumMember(Value = "#DOTA_AbilityDraft_NextRound")]
        DOTA_AbilityDraft_NextRound,

        [EnumMember(Value = "#DOTA_AbilityDraft_Round")]
        DOTA_AbilityDraft_Round,

        [EnumMember(Value = "#DOTA_AbilityDraft_Start_Timer")]
        DOTA_AbilityDraft_Start_Timer,

        [EnumMember(Value = "#DOTA_AbilityDraft_StrategyTime")]
        DOTA_AbilityDraft_StrategyTime,

        [EnumMember(Value = "#DOTA_Ability_Ping_Cooldown")]
        DOTA_Ability_Ping_Cooldown,

        [EnumMember(Value = "#DOTA_Ability_Ping_Cooldown_No_Time")]
        DOTA_Ability_Ping_Cooldown_No_Time,

        [EnumMember(Value = "#DOTA_Ability_Ping_Mana")]
        DOTA_Ability_Ping_Mana,

        [EnumMember(Value = "#DOTA_Ability_Ping_Ready")]
        DOTA_Ability_Ping_Ready,

        [EnumMember(Value = "#DOTA_Ability_Ping_ReadyPassive")]
        DOTA_Ability_Ping_ReadyPassive,

        [EnumMember(Value = "#DOTA_Ability_Ping_Unlearned")]
        DOTA_Ability_Ping_Unlearned,

        [EnumMember(Value = "#DOTA_ActivateBattlePass")]
        DOTA_ActivateBattlePass,

        [EnumMember(Value = "#DOTA_ActivateBattlePass_Failed")]
        DOTA_ActivateBattlePass_Failed,

        [EnumMember(Value = "#DOTA_ActivateBattlePass_InProgress_Text")]
        DOTA_ActivateBattlePass_InProgress_Text,

        [EnumMember(Value = "#DOTA_ActivateBattlePass_InProgress_Title")]
        DOTA_ActivateBattlePass_InProgress_Title,

        [EnumMember(Value = "#DOTA_ActivateBattlePass_NotActivated")]
        DOTA_ActivateBattlePass_NotActivated,

        [EnumMember(Value = "#DOTA_ActivateBattlePass_NotActivated_Message")]
        DOTA_ActivateBattlePass_NotActivated_Message,

        [EnumMember(Value = "#DOTA_ActivateBattlePass_Question")]
        DOTA_ActivateBattlePass_Question,

        [EnumMember(Value = "#DOTA_AddFriend_ErrorCode_{0}")]
        DOTA_AddFriend_ErrorCode_NUM,

        [EnumMember(Value = "#DOTA_AddFriend_ErrorCode_0")]
        DOTA_AddFriend_ErrorCode_0,

        [EnumMember(Value = "#DOTA_AddGemToSocket_Failed")]
        DOTA_AddGemToSocket_Failed,

        [EnumMember(Value = "#DOTA_AddGemToSocket_Gem_Not_Removable")]
        DOTA_AddGemToSocket_Gem_Not_Removable,

        [EnumMember(Value = "#DOTA_AddGemToSocket_Gem_Not_Removable_Title")]
        DOTA_AddGemToSocket_Gem_Not_Removable_Title,

        [EnumMember(Value = "#DOTA_AddGemToSocket_NeedGems")]
        DOTA_AddGemToSocket_NeedGems,

        [EnumMember(Value = "#DOTA_AddGemToSocket_Succeeded")]
        DOTA_AddGemToSocket_Succeeded,

        [EnumMember(Value = "#DOTA_AddGemToSocket_Text")]
        DOTA_AddGemToSocket_Text,

        [EnumMember(Value = "#DOTA_AddGemToSocket_Title")]
        DOTA_AddGemToSocket_Title,

        [EnumMember(Value = "#DOTA_AddSocket_ConfirmTitle")]
        DOTA_AddSocket_ConfirmTitle,

        [EnumMember(Value = "#DOTA_AddSocket_Failed")]
        DOTA_AddSocket_Failed,

        [EnumMember(Value = "#DOTA_AddSocket_ItemCannotBeSocketed")]
        DOTA_AddSocket_ItemCannotBeSocketed,

        [EnumMember(Value = "#DOTA_AddSocket_NeedChisels")]
        DOTA_AddSocket_NeedChisels,

        [EnumMember(Value = "#DOTA_AddSocket_NeedChisels_Title")]
        DOTA_AddSocket_NeedChisels_Title,

        [EnumMember(Value = "#DOTA_AddSocket_Succeeded")]
        DOTA_AddSocket_Succeeded,

        [EnumMember(Value = "#DOTA_AddSocket_Text")]
        DOTA_AddSocket_Text,

        [EnumMember(Value = "#DOTA_AddSocket_Title")]
        DOTA_AddSocket_Title,

        [EnumMember(Value = "#DOTA_AddSocket_ToolIsInvalid")]
        DOTA_AddSocket_ToolIsInvalid,

        [EnumMember(Value = "#DOTA_AllStars_Locked")]
        DOTA_AllStars_Locked,

        [EnumMember(Value = "#DOTA_AllStars_SubmitSuccess")]
        DOTA_AllStars_SubmitSuccess,

        [EnumMember(Value = "#DOTA_AlreadyTipped")]
        DOTA_AlreadyTipped,

        [EnumMember(Value = "#DOTA_Announcer_Default")]
        DOTA_Announcer_Default,

        [EnumMember(Value = "#DOTA_AntiAddiction_Blocked_Time_Exceeded")]
        DOTA_AntiAddiction_Blocked_Time_Exceeded,

        [EnumMember(Value = "#DOTA_AntiAddiction_CompleteInfoNotNow")]
        DOTA_AntiAddiction_CompleteInfoNotNow,

        [EnumMember(Value = "#DOTA_AntiAddiction_CompleteInfoNow")]
        DOTA_AntiAddiction_CompleteInfoNow,

        [EnumMember(Value = "#DOTA_AntiAddiction_Initial_DateFormat")]
        DOTA_AntiAddiction_Initial_DateFormat,

        [EnumMember(Value = "#DOTA_AntiAddiction_Initial_No_ID")]
        DOTA_AntiAddiction_Initial_No_ID,

        [EnumMember(Value = "#DOTA_AntiAddiction_Initial_No_ID_Popup")]
        DOTA_AntiAddiction_Initial_No_ID_Popup,

        [EnumMember(Value = "#DOTA_AntiAddiction_Initial_With_ID_18Plus")]
        DOTA_AntiAddiction_Initial_With_ID_18Plus,

        [EnumMember(Value = "#DOTA_AntiAddiction_Initial_With_ID_Under_18")]
        DOTA_AntiAddiction_Initial_With_ID_Under_18,

        [EnumMember(Value = "#DOTA_AntiAddiction_Level1_Warning_GREEN_chat")]
        DOTA_AntiAddiction_Level1_Warning_GREEN_chat,

        [EnumMember(Value = "#DOTA_AntiAddiction_Level1_Warning_RED")]
        DOTA_AntiAddiction_Level1_Warning_RED,

        [EnumMember(Value = "#DOTA_AntiAddiction_Level2_Warning_GREEN")]
        DOTA_AntiAddiction_Level2_Warning_GREEN,

        [EnumMember(Value = "#DOTA_AntiAddiction_Level2_Warning_GREEN_chat")]
        DOTA_AntiAddiction_Level2_Warning_GREEN_chat,

        [EnumMember(Value = "#DOTA_AntiAddiction_Level2_Warning_RED")]
        DOTA_AntiAddiction_Level2_Warning_RED,

        [EnumMember(Value = "#DOTA_AntiAddiction_Reminder_Warning1")]
        DOTA_AntiAddiction_Reminder_Warning1,

        [EnumMember(Value = "#DOTA_AntiAddiction_Reminder_Warning2")]
        DOTA_AntiAddiction_Reminder_Warning2,

        [EnumMember(Value = "#DOTA_AnyHero")]
        DOTA_AnyHero,

        [EnumMember(Value = "#DOTA_ApplyingEventLevels_Text")]
        DOTA_ApplyingEventLevels_Text,

        [EnumMember(Value = "#DOTA_ApplyingEventLevels_Title")]
        DOTA_ApplyingEventLevels_Title,

        [EnumMember(Value = "#DOTA_ArcanaVoteFailed_Body")]
        DOTA_ArcanaVoteFailed_Body,

        [EnumMember(Value = "#DOTA_ArcanaVoteFailed_Header")]
        DOTA_ArcanaVoteFailed_Header,

        [EnumMember(Value = "#DOTA_ArcanaVoteFailed_VotingNotEnabled")]
        DOTA_ArcanaVoteFailed_VotingNotEnabled,

        [EnumMember(Value = "#DOTA_Armory_Category_All")]
        DOTA_Armory_Category_All,

        [EnumMember(Value = "#DOTA_Armory_Category_AllNew")]
        DOTA_Armory_Category_AllNew,

        [EnumMember(Value = "#DOTA_Armory_Category_AnnouncerPacks")]
        DOTA_Armory_Category_AnnouncerPacks,

        [EnumMember(Value = "#DOTA_Armory_Category_Audio")]
        DOTA_Armory_Category_Audio,

        [EnumMember(Value = "#DOTA_Armory_Category_Bundles")]
        DOTA_Armory_Category_Bundles,

        [EnumMember(Value = "#DOTA_Armory_Category_CharmFragments")]
        DOTA_Armory_Category_CharmFragments,

        [EnumMember(Value = "#DOTA_Armory_Category_Charms")]
        DOTA_Armory_Category_Charms,

        [EnumMember(Value = "#DOTA_Armory_Category_CompendiumCoins")]
        DOTA_Armory_Category_CompendiumCoins,

        [EnumMember(Value = "#DOTA_Armory_Category_CompendiumLevels")]
        DOTA_Armory_Category_CompendiumLevels,

        [EnumMember(Value = "#DOTA_Armory_Category_Compendiums")]
        DOTA_Armory_Category_Compendiums,

        [EnumMember(Value = "#DOTA_Armory_Category_Couriers")]
        DOTA_Armory_Category_Couriers,

        [EnumMember(Value = "#DOTA_Armory_Category_CouriersAndWards")]
        DOTA_Armory_Category_CouriersAndWards,

        [EnumMember(Value = "#DOTA_Armory_Category_CursorPacks")]
        DOTA_Armory_Category_CursorPacks,

        [EnumMember(Value = "#DOTA_Armory_Category_Effigies")]
        DOTA_Armory_Category_Effigies,

        [EnumMember(Value = "#DOTA_Armory_Category_EffigyBlocks")]
        DOTA_Armory_Category_EffigyBlocks,

        [EnumMember(Value = "#DOTA_Armory_Category_EffigyReforgers")]
        DOTA_Armory_Category_EffigyReforgers,

        [EnumMember(Value = "#DOTA_Armory_Category_Emoticons")]
        DOTA_Armory_Category_Emoticons,

        [EnumMember(Value = "#DOTA_Armory_Category_Event")]
        DOTA_Armory_Category_Event,

        [EnumMember(Value = "#DOTA_Armory_Category_Gifts")]
        DOTA_Armory_Category_Gifts,

        [EnumMember(Value = "#DOTA_Armory_Category_HUDs")]
        DOTA_Armory_Category_HUDs,

        [EnumMember(Value = "#DOTA_Armory_Category_Heroes")]
        DOTA_Armory_Category_Heroes,

        [EnumMember(Value = "#DOTA_Armory_Category_Interface")]
        DOTA_Armory_Category_Interface,

        [EnumMember(Value = "#DOTA_Armory_Category_Leagues")]
        DOTA_Armory_Category_Leagues,

        [EnumMember(Value = "#DOTA_Armory_Category_LoadingScreens")]
        DOTA_Armory_Category_LoadingScreens,

        [EnumMember(Value = "#DOTA_Armory_Category_MegaKills")]
        DOTA_Armory_Category_MegaKills,

        [EnumMember(Value = "#DOTA_Armory_Category_Music")]
        DOTA_Armory_Category_Music,

        [EnumMember(Value = "#DOTA_Armory_Category_Older")]
        DOTA_Armory_Category_Older,

        [EnumMember(Value = "#DOTA_Armory_Category_Other")]
        DOTA_Armory_Category_Other,

        [EnumMember(Value = "#DOTA_Armory_Category_Terrain")]
        DOTA_Armory_Category_Terrain,

        [EnumMember(Value = "#DOTA_Armory_Category_ThisMonth")]
        DOTA_Armory_Category_ThisMonth,

        [EnumMember(Value = "#DOTA_Armory_Category_ThisWeek")]
        DOTA_Armory_Category_ThisWeek,

        [EnumMember(Value = "#DOTA_Armory_Category_Today")]
        DOTA_Armory_Category_Today,

        [EnumMember(Value = "#DOTA_Armory_Category_Tools")]
        DOTA_Armory_Category_Tools,

        [EnumMember(Value = "#DOTA_Armory_Category_Treasures")]
        DOTA_Armory_Category_Treasures,

        [EnumMember(Value = "#DOTA_Armory_Category_TreasuresAndCharms")]
        DOTA_Armory_Category_TreasuresAndCharms,

        [EnumMember(Value = "#DOTA_Armory_Category_VoicePacks")]
        DOTA_Armory_Category_VoicePacks,

        [EnumMember(Value = "#DOTA_Armory_Category_Wards")]
        DOTA_Armory_Category_Wards,

        [EnumMember(Value = "#DOTA_Armory_Category_Weather")]
        DOTA_Armory_Category_Weather,

        [EnumMember(Value = "#DOTA_Armory_Category_World")]
        DOTA_Armory_Category_World,

        [EnumMember(Value = "#DOTA_Armory_Coin_Treasure_Tier1")]
        DOTA_Armory_Coin_Treasure_Tier1,

        [EnumMember(Value = "#DOTA_Armory_Coin_Treasure_Tier2")]
        DOTA_Armory_Coin_Treasure_Tier2,

        [EnumMember(Value = "#DOTA_Armory_Coin_Treasure_Tier3")]
        DOTA_Armory_Coin_Treasure_Tier3,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_AddShuffle")]
        DOTA_Armory_ContextMenu_AddShuffle,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_AddToCollection")]
        DOTA_Armory_ContextMenu_AddToCollection,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_Delete")]
        DOTA_Armory_ContextMenu_Delete,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_EffigyPose")]
        DOTA_Armory_ContextMenu_EffigyPose,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_EffigyReforge")]
        DOTA_Armory_ContextMenu_EffigyReforge,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_Equip")]
        DOTA_Armory_ContextMenu_Equip,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_EquipSet")]
        DOTA_Armory_ContextMenu_EquipSet,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_GiftWrap")]
        DOTA_Armory_ContextMenu_GiftWrap,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_Preview")]
        DOTA_Armory_ContextMenu_Preview,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_PreviewBundle")]
        DOTA_Armory_ContextMenu_PreviewBundle,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_PreviewSet")]
        DOTA_Armory_ContextMenu_PreviewSet,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_RemoveFromCollection")]
        DOTA_Armory_ContextMenu_RemoveFromCollection,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_RemoveShuffle")]
        DOTA_Armory_ContextMenu_RemoveShuffle,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_Tags")]
        DOTA_Armory_ContextMenu_Tags,

        [EnumMember(Value = "#DOTA_Armory_ContextMenu_Unequip")]
        DOTA_Armory_ContextMenu_Unequip,

        [EnumMember(Value = "#DOTA_Armory_DeleteItem_Body")]
        DOTA_Armory_DeleteItem_Body,

        [EnumMember(Value = "#DOTA_Armory_DeleteItem_Header")]
        DOTA_Armory_DeleteItem_Header,

        [EnumMember(Value = "#DOTA_Armory_Filter_CreateNew")]
        DOTA_Armory_Filter_CreateNew,

        [EnumMember(Value = "#DOTA_Armory_Search")]
        DOTA_Armory_Search,

        [EnumMember(Value = "#DOTA_Armory_Tag{0}_Default")]
        DOTA_Armory_Tag_NUM_Default,

        [EnumMember(Value = "#DOTA_AssassinGame_ContractAvailable")]
        DOTA_AssassinGame_ContractAvailable,

        [EnumMember(Value = "#DOTA_AssassinGame_ContractDenied")]
        DOTA_AssassinGame_ContractDenied,

        [EnumMember(Value = "#DOTA_AssassinGame_ContractFilled")]
        DOTA_AssassinGame_ContractFilled,

        [EnumMember(Value = "#DOTA_AssassinGame_TargetAvailable")]
        DOTA_AssassinGame_TargetAvailable,

        [EnumMember(Value = "#DOTA_AssassinGame_TargetDenied")]
        DOTA_AssassinGame_TargetDenied,

        [EnumMember(Value = "#DOTA_AssassinGame_TargetFilled")]
        DOTA_AssassinGame_TargetFilled,

        [EnumMember(Value = "#DOTA_AttackCapability_Melee")]
        DOTA_AttackCapability_Melee,

        [EnumMember(Value = "#DOTA_AttackCapability_Ranged")]
        DOTA_AttackCapability_Ranged,

        [EnumMember(Value = "#DOTA_AutomaticSelectionPriority")]
        DOTA_AutomaticSelectionPriority,

        [EnumMember(Value = "#DOTA_AutomaticSelectionPriority_ChoiceConfirm_{0}")]
        DOTA_AutomaticSelectionPriority_ChoiceConfirm_STRING,

        [EnumMember(Value = "#DOTA_AutomaticSelectionPriority_ChoiceResponse_UnknownError")]
        DOTA_AutomaticSelectionPriority_ChoiceResponse_UnknownError,

        [EnumMember(Value = "#DOTA_BackpackExpander_Confirm")]
        DOTA_BackpackExpander_Confirm,

        [EnumMember(Value = "#DOTA_BadGuys")]
        DOTA_BadGuys,

        [EnumMember(Value = "#DOTA_BadGuysShort")]
        DOTA_BadGuysShort,

        [EnumMember(Value = "#DOTA_BattleCup_DivisionTierDate")]
        DOTA_BattleCup_DivisionTierDate,

        [EnumMember(Value = "#DOTA_BattlePassLevelPurchaseUnavailable_Message")]
        DOTA_BattlePassLevelPurchaseUnavailable_Message,

        [EnumMember(Value = "#DOTA_BattlePassLevelPurchaseUnavailable_Title")]
        DOTA_BattlePassLevelPurchaseUnavailable_Title,

        [EnumMember(Value = "#DOTA_BattlePassLog_AchievementCompleted")]
        DOTA_BattlePassLog_AchievementCompleted,

        [EnumMember(Value = "#DOTA_BattlePassLog_BracketPrediction")]
        DOTA_BattlePassLog_BracketPrediction,

        [EnumMember(Value = "#DOTA_BattlePassLog_CommunityGoalItemReceived")]
        DOTA_BattlePassLog_CommunityGoalItemReceived,

        [EnumMember(Value = "#DOTA_BattlePassLog_CompendiumActivated")]
        DOTA_BattlePassLog_CompendiumActivated,

        [EnumMember(Value = "#DOTA_BattlePassLog_CorrectPrediction")]
        DOTA_BattlePassLog_CorrectPrediction,

        [EnumMember(Value = "#DOTA_BattlePassLog_EventReward")]
        DOTA_BattlePassLog_EventReward,

        [EnumMember(Value = "#DOTA_BattlePassLog_ExploitCorrection")]
        DOTA_BattlePassLog_ExploitCorrection,

        [EnumMember(Value = "#DOTA_BattlePassLog_FantasyChallengeWinnings")]
        DOTA_BattlePassLog_FantasyChallengeWinnings,

        [EnumMember(Value = "#DOTA_BattlePassLog_InGamePredictionCorrect")]
        DOTA_BattlePassLog_InGamePredictionCorrect,

        [EnumMember(Value = "#DOTA_BattlePassLog_MysteryItemReceived")]
        DOTA_BattlePassLog_MysteryItemReceived,

        [EnumMember(Value = "#DOTA_BattlePassLog_PointsItemActivated")]
        DOTA_BattlePassLog_PointsItemActivated,

        [EnumMember(Value = "#DOTA_BattlePassLog_QuestCompletedRanked")]
        DOTA_BattlePassLog_QuestCompletedRanked,

        [EnumMember(Value = "#DOTA_BattlePassLog_QuestCompletedRankedRange")]
        DOTA_BattlePassLog_QuestCompletedRankedRange,

        [EnumMember(Value = "#DOTA_BattlePassLog_QuestCompletedUnranked")]
        DOTA_BattlePassLog_QuestCompletedUnranked,

        [EnumMember(Value = "#DOTA_BattlePassLog_RecycledItem")]
        DOTA_BattlePassLog_RecycledItem,

        [EnumMember(Value = "#DOTA_BattlePassLog_TipGiven")]
        DOTA_BattlePassLog_TipGiven,

        [EnumMember(Value = "#DOTA_BattlePassLog_TipReceived")]
        DOTA_BattlePassLog_TipReceived,

        [EnumMember(Value = "#DOTA_BattlePassLog_WagerWon")]
        DOTA_BattlePassLog_WagerWon,

        [EnumMember(Value = "#DOTA_BattlePassLog_WeeklyGame")]
        DOTA_BattlePassLog_WeeklyGame,

        [EnumMember(Value = "#DOTA_BattlePass_Reward_UnlockLevelTooltip")]
        DOTA_BattlePass_Reward_UnlockLevelTooltip,

        [EnumMember(Value = "#DOTA_BattlePass_WindowTitle")]
        DOTA_BattlePass_WindowTitle,

        [EnumMember(Value = "#DOTA_BillionSuffix")]
        DOTA_BillionSuffix,

        [EnumMember(Value = "#DOTA_Bot_Action_{0}")]
        DOTA_Bot_Action_NUM,

        [EnumMember(Value = "#DOTA_Bot_Mode_{0}")]
        DOTA_Bot_Mode_NUM,

        [EnumMember(Value = "#DOTA_BroadcasterToasts_HeroAbilityMovie_Title")]
        DOTA_BroadcasterToasts_HeroAbilityMovie_Title,

        [EnumMember(Value = "#DOTA_BroadcasterToasts_ItemMovie_Title")]
        DOTA_BroadcasterToasts_ItemMovie_Title,

        [EnumMember(Value = "#DOTA_Broadcaster_Channel")]
        DOTA_Broadcaster_Channel,

        [EnumMember(Value = "#DOTA_BuyBackStateAlert_Cooldown")]
        DOTA_BuyBackStateAlert_Cooldown,

        [EnumMember(Value = "#DOTA_BuyBackStateAlert_Gold")]
        DOTA_BuyBackStateAlert_Gold,

        [EnumMember(Value = "#DOTA_BuyBackStateAlert_Ready")]
        DOTA_BuyBackStateAlert_Ready,

        [EnumMember(Value = "#DOTA_BuyBackStateAlert_ReapersScythe")]
        DOTA_BuyBackStateAlert_ReapersScythe,

        [EnumMember(Value = "#DOTA_Cancel")]
        DOTA_Cancel,

        [EnumMember(Value = "#DOTA_Cannot_Random_PickOrder")]
        DOTA_Cannot_Random_PickOrder,

        [EnumMember(Value = "#DOTA_Cannot_Random_Time")]
        DOTA_Cannot_Random_Time,

        [EnumMember(Value = "#DOTA_ChallengeDesc_Weekly1Preview")]
        DOTA_ChallengeDesc_Weekly1Preview,

        [EnumMember(Value = "#DOTA_ChallengeDesc_Weekly2Preview")]
        DOTA_ChallengeDesc_Weekly2Preview,

        [EnumMember(Value = "#DOTA_ChallengeLore_PlaceHolder")]
        DOTA_ChallengeLore_PlaceHolder,

        [EnumMember(Value = "#DOTA_ChallengeName_PlaceHolder")]
        DOTA_ChallengeName_PlaceHolder,

        [EnumMember(Value = "#DOTA_ChallengeVar_Killstreak_{0}")]
        DOTA_ChallengeVar_Killstreak_NUM,

        [EnumMember(Value = "#DOTA_ChallengeVar_MultiKill_{0}")]
        DOTA_ChallengeVar_MultiKill_NUM,

        [EnumMember(Value = "#DOTA_ChallengeVar_PrimaryAttribute_{0}")]
        DOTA_ChallengeVar_PrimaryAttribute_NUM,

        [EnumMember(Value = "#DOTA_ChallengeVar_Unknown")]
        DOTA_ChallengeVar_Unknown,

        [EnumMember(Value = "#DOTA_Challenge_MultiGame")]
        DOTA_Challenge_MultiGame,

        [EnumMember(Value = "#DOTA_Challenge_New_Not")]
        DOTA_Challenge_New_Not,

        [EnumMember(Value = "#DOTA_Challenge_New_NotAvailable_d")]
        DOTA_Challenge_New_NotAvailable_d,

        [EnumMember(Value = "#DOTA_Challenge_New_NotAvailable_h")]
        DOTA_Challenge_New_NotAvailable_h,

        [EnumMember(Value = "#DOTA_Challenge_New_NotAvailable_m")]
        DOTA_Challenge_New_NotAvailable_m,

        [EnumMember(Value = "#DOTA_Challenge_New_NotAvailable_s")]
        DOTA_Challenge_New_NotAvailable_s,

        [EnumMember(Value = "#DOTA_Challenge_Reroll")]
        DOTA_Challenge_Reroll,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_Available")]
        DOTA_Challenge_Reroll_Available,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_Completed")]
        DOTA_Challenge_Reroll_Completed,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_Completed_Available")]
        DOTA_Challenge_Reroll_Completed_Available,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_Not")]
        DOTA_Challenge_Reroll_Not,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_NotAvailable_d")]
        DOTA_Challenge_Reroll_NotAvailable_d,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_NotAvailable_h")]
        DOTA_Challenge_Reroll_NotAvailable_h,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_NotAvailable_m")]
        DOTA_Challenge_Reroll_NotAvailable_m,

        [EnumMember(Value = "#DOTA_Challenge_Reroll_NotAvailable_s")]
        DOTA_Challenge_Reroll_NotAvailable_s,

        [EnumMember(Value = "#DOTA_Challenge_SingleGame")]
        DOTA_Challenge_SingleGame,

        [EnumMember(Value = "#DOTA_Channel_Bar_Time")]
        DOTA_Channel_Bar_Time,

        [EnumMember(Value = "#DOTA_Charges")]
        DOTA_Charges,

        [EnumMember(Value = "#DOTA_ChatCommand_Channel")]
        DOTA_ChatCommand_Channel,

        [EnumMember(Value = "#DOTA_ChatCommand_Channel_Description")]
        DOTA_ChatCommand_Channel_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Channel_Name")]
        DOTA_ChatCommand_Channel_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_Clear")]
        DOTA_ChatCommand_Clear,

        [EnumMember(Value = "#DOTA_ChatCommand_Clear_Description")]
        DOTA_ChatCommand_Clear_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Clear_Name")]
        DOTA_ChatCommand_Clear_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_FlipCoin")]
        DOTA_ChatCommand_FlipCoin,

        [EnumMember(Value = "#DOTA_ChatCommand_FlipCoin_Description")]
        DOTA_ChatCommand_FlipCoin_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_FlipCoin_Name")]
        DOTA_ChatCommand_FlipCoin_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_GameAll")]
        DOTA_ChatCommand_GameAll,

        [EnumMember(Value = "#DOTA_ChatCommand_GameAll_Description")]
        DOTA_ChatCommand_GameAll_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_GameAll_Name")]
        DOTA_ChatCommand_GameAll_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_GameAllies")]
        DOTA_ChatCommand_GameAllies,

        [EnumMember(Value = "#DOTA_ChatCommand_GameAllies_Description")]
        DOTA_ChatCommand_GameAllies_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_GameAllies_Name")]
        DOTA_ChatCommand_GameAllies_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_GameSpectator")]
        DOTA_ChatCommand_GameSpectator,

        [EnumMember(Value = "#DOTA_ChatCommand_GameSpectator_Description")]
        DOTA_ChatCommand_GameSpectator_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_GameSpectator_Name")]
        DOTA_ChatCommand_GameSpectator_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_Help")]
        DOTA_ChatCommand_Help,

        [EnumMember(Value = "#DOTA_ChatCommand_Help_Description")]
        DOTA_ChatCommand_Help_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Help_Name")]
        DOTA_ChatCommand_Help_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_InviteToParty")]
        DOTA_ChatCommand_InviteToParty,

        [EnumMember(Value = "#DOTA_ChatCommand_InviteToParty_Description")]
        DOTA_ChatCommand_InviteToParty_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_InviteToParty_Name")]
        DOTA_ChatCommand_InviteToParty_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_JoinChannel")]
        DOTA_ChatCommand_JoinChannel,

        [EnumMember(Value = "#DOTA_ChatCommand_JoinChannel_Description")]
        DOTA_ChatCommand_JoinChannel_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_JoinChannel_Name")]
        DOTA_ChatCommand_JoinChannel_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_Laugh")]
        DOTA_ChatCommand_Laugh,

        [EnumMember(Value = "#DOTA_ChatCommand_LeaveChannel")]
        DOTA_ChatCommand_LeaveChannel,

        [EnumMember(Value = "#DOTA_ChatCommand_LeaveChannel_Description")]
        DOTA_ChatCommand_LeaveChannel_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_LeaveChannel_Name")]
        DOTA_ChatCommand_LeaveChannel_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_LeaveParty")]
        DOTA_ChatCommand_LeaveParty,

        [EnumMember(Value = "#DOTA_ChatCommand_LeaveParty_Description")]
        DOTA_ChatCommand_LeaveParty_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_LeaveParty_Name")]
        DOTA_ChatCommand_LeaveParty_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_Lobby")]
        DOTA_ChatCommand_Lobby,

        [EnumMember(Value = "#DOTA_ChatCommand_Lobby_Description")]
        DOTA_ChatCommand_Lobby_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Lobby_Name")]
        DOTA_ChatCommand_Lobby_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_Party")]
        DOTA_ChatCommand_Party,

        [EnumMember(Value = "#DOTA_ChatCommand_Party_Description")]
        DOTA_ChatCommand_Party_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Party_Name")]
        DOTA_ChatCommand_Party_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote")]
        DOTA_ChatCommand_PrivateChatDemote,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote_Description")]
        DOTA_ChatCommand_PrivateChatDemote_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote_Fail_Message")]
        DOTA_ChatCommand_PrivateChatDemote_Fail_Message,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote_Fail_NoPermission")]
        DOTA_ChatCommand_PrivateChatDemote_Fail_NoPermission,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote_Fail_UnknownUser")]
        DOTA_ChatCommand_PrivateChatDemote_Fail_UnknownUser,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote_Name")]
        DOTA_ChatCommand_PrivateChatDemote_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatDemote_Success")]
        DOTA_ChatCommand_PrivateChatDemote_Success,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite")]
        DOTA_ChatCommand_PrivateChatInvite,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Description")]
        DOTA_ChatCommand_PrivateChatInvite_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Fail_AlreadyMember")]
        DOTA_ChatCommand_PrivateChatInvite_Fail_AlreadyMember,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Fail_Message")]
        DOTA_ChatCommand_PrivateChatInvite_Fail_Message,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Fail_NoPermission")]
        DOTA_ChatCommand_PrivateChatInvite_Fail_NoPermission,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Fail_NoRoom")]
        DOTA_ChatCommand_PrivateChatInvite_Fail_NoRoom,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Fail_UnknownUser")]
        DOTA_ChatCommand_PrivateChatInvite_Fail_UnknownUser,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Name")]
        DOTA_ChatCommand_PrivateChatInvite_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatInvite_Success")]
        DOTA_ChatCommand_PrivateChatInvite_Success,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick")]
        DOTA_ChatCommand_PrivateChatKick,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Description")]
        DOTA_ChatCommand_PrivateChatKick_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Fail_CantKickAdmin")]
        DOTA_ChatCommand_PrivateChatKick_Fail_CantKickAdmin,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Fail_Message")]
        DOTA_ChatCommand_PrivateChatKick_Fail_Message,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Fail_NoPermission")]
        DOTA_ChatCommand_PrivateChatKick_Fail_NoPermission,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Fail_NotAMember")]
        DOTA_ChatCommand_PrivateChatKick_Fail_NotAMember,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Fail_UnknownUser")]
        DOTA_ChatCommand_PrivateChatKick_Fail_UnknownUser,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Name")]
        DOTA_ChatCommand_PrivateChatKick_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatKick_Success")]
        DOTA_ChatCommand_PrivateChatKick_Success,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote")]
        DOTA_ChatCommand_PrivateChatPromote,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Description")]
        DOTA_ChatCommand_PrivateChatPromote_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Fail_AlreadyAdmin")]
        DOTA_ChatCommand_PrivateChatPromote_Fail_AlreadyAdmin,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Fail_Message")]
        DOTA_ChatCommand_PrivateChatPromote_Fail_Message,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Fail_NoPermission")]
        DOTA_ChatCommand_PrivateChatPromote_Fail_NoPermission,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Fail_UnknownUser")]
        DOTA_ChatCommand_PrivateChatPromote_Fail_UnknownUser,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Name")]
        DOTA_ChatCommand_PrivateChatPromote_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_PrivateChatPromote_Success")]
        DOTA_ChatCommand_PrivateChatPromote_Success,

        [EnumMember(Value = "#DOTA_ChatCommand_Reply")]
        DOTA_ChatCommand_Reply,

        [EnumMember(Value = "#DOTA_ChatCommand_Reply_Description")]
        DOTA_ChatCommand_Reply_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Reply_Name")]
        DOTA_ChatCommand_Reply_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_RollDice")]
        DOTA_ChatCommand_RollDice,

        [EnumMember(Value = "#DOTA_ChatCommand_RollDice_Description")]
        DOTA_ChatCommand_RollDice_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_RollDice_Name")]
        DOTA_ChatCommand_RollDice_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareLobby")]
        DOTA_ChatCommand_ShareLobby,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareLobby_Description")]
        DOTA_ChatCommand_ShareLobby_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareLobby_Name")]
        DOTA_ChatCommand_ShareLobby_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareParty")]
        DOTA_ChatCommand_ShareParty,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareParty_Description")]
        DOTA_ChatCommand_ShareParty_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareParty_Name")]
        DOTA_ChatCommand_ShareParty_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareProfile")]
        DOTA_ChatCommand_ShareProfile,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareProfile_Description")]
        DOTA_ChatCommand_ShareProfile_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_ShareProfile_Name")]
        DOTA_ChatCommand_ShareProfile_Name,

        [EnumMember(Value = "#DOTA_ChatCommand_Thanks")]
        DOTA_ChatCommand_Thanks,

        [EnumMember(Value = "#DOTA_ChatCommand_Whisper")]
        DOTA_ChatCommand_Whisper,

        [EnumMember(Value = "#DOTA_ChatCommand_Whisper_Description")]
        DOTA_ChatCommand_Whisper_Description,

        [EnumMember(Value = "#DOTA_ChatCommand_Whisper_Name")]
        DOTA_ChatCommand_Whisper_Name,

        [EnumMember(Value = "#DOTA_ChatHelp_CommandFormat")]
        DOTA_ChatHelp_CommandFormat,

        [EnumMember(Value = "#DOTA_ChatHelp_NameFormat")]
        DOTA_ChatHelp_NameFormat,

        [EnumMember(Value = "#DOTA_ChatMacro_MatchID")]
        DOTA_ChatMacro_MatchID,

        [EnumMember(Value = "#DOTA_ChatMacro_MatchIDCommand")]
        DOTA_ChatMacro_MatchIDCommand,

        [EnumMember(Value = "#DOTA_ChatMacro_TournamentID")]
        DOTA_ChatMacro_TournamentID,

        [EnumMember(Value = "#DOTA_ChatMacro_TournamentIDCommand")]
        DOTA_ChatMacro_TournamentIDCommand,

        [EnumMember(Value = "#DOTA_ChatMessage_Default")]
        DOTA_ChatMessage_Default,

        [EnumMember(Value = "#DOTA_ChatMessage_Default_NoTarget")]
        DOTA_ChatMessage_Default_NoTarget,

        [EnumMember(Value = "#DOTA_ChatMessage_Emote")]
        DOTA_ChatMessage_Emote,

        [EnumMember(Value = "#DOTA_ChatMessage_Emote_NoTarget")]
        DOTA_ChatMessage_Emote_NoTarget,

        [EnumMember(Value = "#DOTA_ChatMessage_Hud")]
        DOTA_ChatMessage_Hud,

        [EnumMember(Value = "#DOTA_ChatMessage_HudAll")]
        DOTA_ChatMessage_HudAll,

        [EnumMember(Value = "#DOTA_ChatMessage_HudEmote")]
        DOTA_ChatMessage_HudEmote,

        [EnumMember(Value = "#DOTA_ChatMessage_HudOtherChannel")]
        DOTA_ChatMessage_HudOtherChannel,

        [EnumMember(Value = "#DOTA_ChatMessage_HudTerse")]
        DOTA_ChatMessage_HudTerse,

        [EnumMember(Value = "#DOTA_ChatMessage_System")]
        DOTA_ChatMessage_System,

        [EnumMember(Value = "#DOTA_ChatMessage_SystemTerse")]
        DOTA_ChatMessage_SystemTerse,

        [EnumMember(Value = "#DOTA_ChatMessage_WhisperFrom")]
        DOTA_ChatMessage_WhisperFrom,

        [EnumMember(Value = "#DOTA_ChatMessage_WhisperTo")]
        DOTA_ChatMessage_WhisperTo,

        [EnumMember(Value = "#DOTA_ChatTab_Close")]
        DOTA_ChatTab_Close,

        [EnumMember(Value = "#DOTA_ChatTab_Leave")]
        DOTA_ChatTab_Leave,

        [EnumMember(Value = "#DOTA_ChatTab_NotInChannelMessage")]
        DOTA_ChatTab_NotInChannelMessage,

        [EnumMember(Value = "#DOTA_ChatTarget_Console")]
        DOTA_ChatTarget_Console,

        [EnumMember(Value = "#DOTA_ChatTarget_Fantasy")]
        DOTA_ChatTarget_Fantasy,

        [EnumMember(Value = "#DOTA_ChatTarget_Format_BattleCup")]
        DOTA_ChatTarget_Format_BattleCup,

        [EnumMember(Value = "#DOTA_ChatTarget_Format_CustomGame")]
        DOTA_ChatTarget_Format_CustomGame,

        [EnumMember(Value = "#DOTA_ChatTarget_Format_Default")]
        DOTA_ChatTarget_Format_Default,

        [EnumMember(Value = "#DOTA_ChatTarget_Format_PostGame")]
        DOTA_ChatTarget_Format_PostGame,

        [EnumMember(Value = "#DOTA_ChatTarget_Format_Trivia")]
        DOTA_ChatTarget_Format_Trivia,

        [EnumMember(Value = "#DOTA_ChatTarget_Format_Whisper")]
        DOTA_ChatTarget_Format_Whisper,

        [EnumMember(Value = "#DOTA_ChatTarget_GameAll")]
        DOTA_ChatTarget_GameAll,

        [EnumMember(Value = "#DOTA_ChatTarget_GameAllies")]
        DOTA_ChatTarget_GameAllies,

        [EnumMember(Value = "#DOTA_ChatTarget_GameEvents")]
        DOTA_ChatTarget_GameEvents,

        [EnumMember(Value = "#DOTA_ChatTarget_GameSpectator")]
        DOTA_ChatTarget_GameSpectator,

        [EnumMember(Value = "#DOTA_ChatTarget_Guild")]
        DOTA_ChatTarget_Guild,

        [EnumMember(Value = "#DOTA_ChatTarget_HLTVSpectator")]
        DOTA_ChatTarget_HLTVSpectator,

        [EnumMember(Value = "#DOTA_ChatTarget_Invalid")]
        DOTA_ChatTarget_Invalid,

        [EnumMember(Value = "#DOTA_ChatTarget_Lobby")]
        DOTA_ChatTarget_Lobby,

        [EnumMember(Value = "#DOTA_ChatTarget_Party")]
        DOTA_ChatTarget_Party,

        [EnumMember(Value = "#DOTA_ChatTarget_PostGame")]
        DOTA_ChatTarget_PostGame,

        [EnumMember(Value = "#DOTA_ChatTarget_Prompt_BattleCup")]
        DOTA_ChatTarget_Prompt_BattleCup,

        [EnumMember(Value = "#DOTA_ChatTarget_Prompt_CustomGame")]
        DOTA_ChatTarget_Prompt_CustomGame,

        [EnumMember(Value = "#DOTA_ChatTarget_Prompt_Default")]
        DOTA_ChatTarget_Prompt_Default,

        [EnumMember(Value = "#DOTA_ChatTarget_Prompt_PostGame")]
        DOTA_ChatTarget_Prompt_PostGame,

        [EnumMember(Value = "#DOTA_ChatTarget_Prompt_Trivia")]
        DOTA_ChatTarget_Prompt_Trivia,

        [EnumMember(Value = "#DOTA_ChatTarget_Prompt_Whisper")]
        DOTA_ChatTarget_Prompt_Whisper,

        [EnumMember(Value = "#DOTA_ChatTarget_Team")]
        DOTA_ChatTarget_Team,

        [EnumMember(Value = "#DOTA_ChatTarget_Trivia")]
        DOTA_ChatTarget_Trivia,

        [EnumMember(Value = "#DOTA_ChatWheelPhraseLocked_HeroLevel")]
        DOTA_ChatWheelPhraseLocked_HeroLevel,

        [EnumMember(Value = "#DOTA_Chat_AD_Ban")]
        DOTA_Chat_AD_Ban,

        [EnumMember(Value = "#DOTA_Chat_AD_BanCount")]
        DOTA_Chat_AD_BanCount,

        [EnumMember(Value = "#DOTA_Chat_AD_BanCount1")]
        DOTA_Chat_AD_BanCount1,

        [EnumMember(Value = "#DOTA_Chat_AD_NominatedBan")]
        DOTA_Chat_AD_NominatedBan,

        [EnumMember(Value = "#DOTA_Chat_AssassinDeny02")]
        DOTA_Chat_AssassinDeny02,

        [EnumMember(Value = "#DOTA_Chat_AssassinFulfill02")]
        DOTA_Chat_AssassinFulfill02,

        [EnumMember(Value = "#DOTA_Chat_AssassinOpponents01")]
        DOTA_Chat_AssassinOpponents01,

        [EnumMember(Value = "#DOTA_Chat_AssassinTeammate01")]
        DOTA_Chat_AssassinTeammate01,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_Channels")]
        DOTA_Chat_Autocomplete_Channels,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_CommandDescription")]
        DOTA_Chat_Autocomplete_CommandDescription,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_CommandName")]
        DOTA_Chat_Autocomplete_CommandName,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_Commands")]
        DOTA_Chat_Autocomplete_Commands,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_InviteFriends")]
        DOTA_Chat_Autocomplete_InviteFriends,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_NoMatches")]
        DOTA_Chat_Autocomplete_NoMatches,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_PrivateChatDemote")]
        DOTA_Chat_Autocomplete_PrivateChatDemote,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_PrivateChatInvite")]
        DOTA_Chat_Autocomplete_PrivateChatInvite,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_PrivateChatKick")]
        DOTA_Chat_Autocomplete_PrivateChatKick,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_PrivateChatPromote")]
        DOTA_Chat_Autocomplete_PrivateChatPromote,

        [EnumMember(Value = "#DOTA_Chat_Autocomplete_WhisperFriends")]
        DOTA_Chat_Autocomplete_WhisperFriends,

        [EnumMember(Value = "#DOTA_Chat_Bad")]
        DOTA_Chat_Bad,

        [EnumMember(Value = "#DOTA_Chat_BarracksKilled")]
        DOTA_Chat_BarracksKilled,

        [EnumMember(Value = "#DOTA_Chat_BarracksKilled_By")]
        DOTA_Chat_BarracksKilled_By,

        [EnumMember(Value = "#DOTA_Chat_BeastKilledPlayer")]
        DOTA_Chat_BeastKilledPlayer,

        [EnumMember(Value = "#DOTA_Chat_ChannelDistance")]
        DOTA_Chat_ChannelDistance,

        [EnumMember(Value = "#DOTA_Chat_ChannelMemberCount")]
        DOTA_Chat_ChannelMemberCount,

        [EnumMember(Value = "#DOTA_Chat_ChannelMemberCount_Plural")]
        DOTA_Chat_ChannelMemberCount_Plural,

        [EnumMember(Value = "#DOTA_Chat_ChannelMemberCount_Singular")]
        DOTA_Chat_ChannelMemberCount_Singular,

        [EnumMember(Value = "#DOTA_Chat_ChannelName")]
        DOTA_Chat_ChannelName,

        [EnumMember(Value = "#DOTA_Chat_ChatWheelAllChatAudioCooldown")]
        DOTA_Chat_ChatWheelAllChatAudioCooldown,

        [EnumMember(Value = "#DOTA_Chat_ChatWheelAudioCooldown")]
        DOTA_Chat_ChatWheelAudioCooldown,

        [EnumMember(Value = "#DOTA_Chat_CoopBattlePointsRules")]
        DOTA_Chat_CoopBattlePointsRules,

        [EnumMember(Value = "#DOTA_Chat_CoopLowPriorityNoPassiveReminder")]
        DOTA_Chat_CoopLowPriorityNoPassiveReminder,

        [EnumMember(Value = "#DOTA_Chat_CourierLostBad")]
        DOTA_Chat_CourierLostBad,

        [EnumMember(Value = "#DOTA_Chat_CourierLostGood")]
        DOTA_Chat_CourierLostGood,

        [EnumMember(Value = "#DOTA_Chat_CourierRespawnedBad")]
        DOTA_Chat_CourierRespawnedBad,

        [EnumMember(Value = "#DOTA_Chat_CourierRespawnedGood")]
        DOTA_Chat_CourierRespawnedGood,

        [EnumMember(Value = "#DOTA_Chat_CustomGameChannelName")]
        DOTA_Chat_CustomGameChannelName,

        [EnumMember(Value = "#DOTA_Chat_DoubleKill")]
        DOTA_Chat_DoubleKill,

        [EnumMember(Value = "#DOTA_Chat_DoubleKill_noname")]
        DOTA_Chat_DoubleKill_noname,

        [EnumMember(Value = "#DOTA_Chat_EconItem")]
        DOTA_Chat_EconItem,

        [EnumMember(Value = "#DOTA_Chat_EffigyKilled")]
        DOTA_Chat_EffigyKilled,

        [EnumMember(Value = "#DOTA_Chat_EffigyKilled_By")]
        DOTA_Chat_EffigyKilled_By,

        [EnumMember(Value = "#DOTA_Chat_EndSuggestions")]
        DOTA_Chat_EndSuggestions,

        [EnumMember(Value = "#DOTA_Chat_Frostivus_Abandon_Reminder")]
        DOTA_Chat_Frostivus_Abandon_Reminder,

        [EnumMember(Value = "#DOTA_Chat_GlyphUsedBad")]
        DOTA_Chat_GlyphUsedBad,

        [EnumMember(Value = "#DOTA_Chat_GlyphUsedGood")]
        DOTA_Chat_GlyphUsedGood,

        [EnumMember(Value = "#DOTA_Chat_Good")]
        DOTA_Chat_Good,

        [EnumMember(Value = "#DOTA_Chat_InvitedToJoinParty")]
        DOTA_Chat_InvitedToJoinParty,

        [EnumMember(Value = "#DOTA_Chat_ItemGifted")]
        DOTA_Chat_ItemGifted,

        [EnumMember(Value = "#DOTA_Chat_Item_Alert")]
        DOTA_Chat_Item_Alert,

        [EnumMember(Value = "#DOTA_Chat_Item_Spotted")]
        DOTA_Chat_Item_Spotted,

        [EnumMember(Value = "#DOTA_Chat_JoinedParty")]
        DOTA_Chat_JoinedParty,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_10_Ended")]
        DOTA_Chat_KillStreak_10_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_3_Ended")]
        DOTA_Chat_KillStreak_3_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_4_Ended")]
        DOTA_Chat_KillStreak_4_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_5_Ended")]
        DOTA_Chat_KillStreak_5_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_6_Ended")]
        DOTA_Chat_KillStreak_6_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_7_Ended")]
        DOTA_Chat_KillStreak_7_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_8_Ended")]
        DOTA_Chat_KillStreak_8_Ended,

        [EnumMember(Value = "#DOTA_Chat_KillStreak_9_Ended")]
        DOTA_Chat_KillStreak_9_Ended,

        [EnumMember(Value = "#DOTA_Chat_KilledRoshanBad")]
        DOTA_Chat_KilledRoshanBad,

        [EnumMember(Value = "#DOTA_Chat_KilledRoshanGood")]
        DOTA_Chat_KilledRoshanGood,

        [EnumMember(Value = "#DOTA_Chat_LaneBot")]
        DOTA_Chat_LaneBot,

        [EnumMember(Value = "#DOTA_Chat_LaneMid")]
        DOTA_Chat_LaneMid,

        [EnumMember(Value = "#DOTA_Chat_LaneTop")]
        DOTA_Chat_LaneTop,

        [EnumMember(Value = "#DOTA_Chat_LeftParty")]
        DOTA_Chat_LeftParty,

        [EnumMember(Value = "#DOTA_Chat_Lobby_MVP_Awarded")]
        DOTA_Chat_Lobby_MVP_Awarded,

        [EnumMember(Value = "#DOTA_Chat_Lobby_MVP_Awarded_Local_Player")]
        DOTA_Chat_Lobby_MVP_Awarded_Local_Player,

        [EnumMember(Value = "#DOTA_Chat_Lobby_MVP_Awarded_Toast")]
        DOTA_Chat_Lobby_MVP_Awarded_Toast,

        [EnumMember(Value = "#DOTA_Chat_Lobby_MVP_Vote_Message")]
        DOTA_Chat_Lobby_MVP_Vote_Message,

        [EnumMember(Value = "#DOTA_Chat_Lobby_MVP_Vote_Notify_Generic")]
        DOTA_Chat_Lobby_MVP_Vote_Notify_Generic,

        [EnumMember(Value = "#DOTA_Chat_Lobby_MVP_You_Voted")]
        DOTA_Chat_Lobby_MVP_You_Voted,

        [EnumMember(Value = "#DOTA_Chat_Melee")]
        DOTA_Chat_Melee,

        [EnumMember(Value = "#DOTA_Chat_MemberJoinedParty")]
        DOTA_Chat_MemberJoinedParty,

        [EnumMember(Value = "#DOTA_Chat_MemberLeftParty")]
        DOTA_Chat_MemberLeftParty,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_10")]
        DOTA_Chat_New_KillStreak_10,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_3")]
        DOTA_Chat_New_KillStreak_3,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_4")]
        DOTA_Chat_New_KillStreak_4,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_5")]
        DOTA_Chat_New_KillStreak_5,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_6")]
        DOTA_Chat_New_KillStreak_6,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_7")]
        DOTA_Chat_New_KillStreak_7,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_8")]
        DOTA_Chat_New_KillStreak_8,

        [EnumMember(Value = "#DOTA_Chat_New_KillStreak_9")]
        DOTA_Chat_New_KillStreak_9,

        [EnumMember(Value = "#DOTA_Chat_NoBattlePoints_Bots")]
        DOTA_Chat_NoBattlePoints_Bots,

        [EnumMember(Value = "#DOTA_Chat_NoBattlePoints_CheatsEnabled")]
        DOTA_Chat_NoBattlePoints_CheatsEnabled,

        [EnumMember(Value = "#DOTA_Chat_NoBattlePoints_LowPriority")]
        DOTA_Chat_NoBattlePoints_LowPriority,

        [EnumMember(Value = "#DOTA_Chat_NoBattlePoints_WrongLobby")]
        DOTA_Chat_NoBattlePoints_WrongLobby,

        [EnumMember(Value = "#DOTA_Chat_PartyMemberSeparator")]
        DOTA_Chat_PartyMemberSeparator,

        [EnumMember(Value = "#DOTA_Chat_Ping_Msg_Attack")]
        DOTA_Chat_Ping_Msg_Attack,

        [EnumMember(Value = "#DOTA_Chat_Ping_Msg_Attack_Target")]
        DOTA_Chat_Ping_Msg_Attack_Target,

        [EnumMember(Value = "#DOTA_Chat_Ping_Msg_EnemyWard")]
        DOTA_Chat_Ping_Msg_EnemyWard,

        [EnumMember(Value = "#DOTA_Chat_Ping_Msg_FriendlyWard")]
        DOTA_Chat_Ping_Msg_FriendlyWard,

        [EnumMember(Value = "#DOTA_Chat_Ping_Msg_Retreat")]
        DOTA_Chat_Ping_Msg_Retreat,

        [EnumMember(Value = "#DOTA_Chat_Ping_Msg_Waypoint")]
        DOTA_Chat_Ping_Msg_Waypoint,

        [EnumMember(Value = "#DOTA_Chat_PlayerDenied")]
        DOTA_Chat_PlayerDenied,

        [EnumMember(Value = "#DOTA_Chat_PlayerDeniedSelf")]
        DOTA_Chat_PlayerDeniedSelf,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilled")]
        DOTA_Chat_PlayerKilled,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledAssist")]
        DOTA_Chat_PlayerKilledAssist,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledAssistNearby")]
        DOTA_Chat_PlayerKilledAssistNearby,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledAssistNearbySingle")]
        DOTA_Chat_PlayerKilledAssistNearbySingle,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledAssistXmas")]
        DOTA_Chat_PlayerKilledAssistXmas,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledBad")]
        DOTA_Chat_PlayerKilledBad,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledBadNearby")]
        DOTA_Chat_PlayerKilledBadNearby,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledBadNearbySingle")]
        DOTA_Chat_PlayerKilledBadNearbySingle,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledGeneric")]
        DOTA_Chat_PlayerKilledGeneric,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledGood")]
        DOTA_Chat_PlayerKilledGood,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledGoodNearby")]
        DOTA_Chat_PlayerKilledGoodNearby,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledGoodNearbySingle")]
        DOTA_Chat_PlayerKilledGoodNearbySingle,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledNearby")]
        DOTA_Chat_PlayerKilledNearby,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledNearbySingle")]
        DOTA_Chat_PlayerKilledNearbySingle,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledRoshan")]
        DOTA_Chat_PlayerKilledRoshan,

        [EnumMember(Value = "#DOTA_Chat_PlayerKilledXmas")]
        DOTA_Chat_PlayerKilledXmas,

        [EnumMember(Value = "#DOTA_Chat_PleaseSuggestItems")]
        DOTA_Chat_PleaseSuggestItems,

        [EnumMember(Value = "#DOTA_Chat_QuadKill")]
        DOTA_Chat_QuadKill,

        [EnumMember(Value = "#DOTA_Chat_QuadKill_noname")]
        DOTA_Chat_QuadKill_noname,

        [EnumMember(Value = "#DOTA_Chat_QuintupleKill")]
        DOTA_Chat_QuintupleKill,

        [EnumMember(Value = "#DOTA_Chat_QuintupleKill_10streak")]
        DOTA_Chat_QuintupleKill_10streak,

        [EnumMember(Value = "#DOTA_Chat_QuintupleKill_noname")]
        DOTA_Chat_QuintupleKill_noname,

        [EnumMember(Value = "#DOTA_Chat_RD_YourTurn")]
        DOTA_Chat_RD_YourTurn,

        [EnumMember(Value = "#DOTA_Chat_Random")]
        DOTA_Chat_Random,

        [EnumMember(Value = "#DOTA_Chat_Ranged")]
        DOTA_Chat_Ranged,

        [EnumMember(Value = "#DOTA_Chat_Ranked_Reminder")]
        DOTA_Chat_Ranked_Reminder,

        [EnumMember(Value = "#DOTA_Chat_RequestedToJoinParty")]
        DOTA_Chat_RequestedToJoinParty,

        [EnumMember(Value = "#DOTA_Chat_RiverPainted{0}")]
        DOTA_Chat_RiverPainted_NUM,

        [EnumMember(Value = "#DOTA_Chat_Rune_Arcane")]
        DOTA_Chat_Rune_Arcane,

        [EnumMember(Value = "#DOTA_Chat_Rune_Arcane_Spotted")]
        DOTA_Chat_Rune_Arcane_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_Arcane")]
        DOTA_Chat_Rune_Bottle_Arcane,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_Bounty")]
        DOTA_Chat_Rune_Bottle_Bounty,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_DoubleDamage")]
        DOTA_Chat_Rune_Bottle_DoubleDamage,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_Haste")]
        DOTA_Chat_Rune_Bottle_Haste,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_Illusion")]
        DOTA_Chat_Rune_Bottle_Illusion,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_Invisibility")]
        DOTA_Chat_Rune_Bottle_Invisibility,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bottle_Regeneration")]
        DOTA_Chat_Rune_Bottle_Regeneration,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bounty")]
        DOTA_Chat_Rune_Bounty,

        [EnumMember(Value = "#DOTA_Chat_Rune_Bounty_Spotted")]
        DOTA_Chat_Rune_Bounty_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_DoubleDamage")]
        DOTA_Chat_Rune_DoubleDamage,

        [EnumMember(Value = "#DOTA_Chat_Rune_DoubleDamage_Spotted")]
        DOTA_Chat_Rune_DoubleDamage_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_Haste")]
        DOTA_Chat_Rune_Haste,

        [EnumMember(Value = "#DOTA_Chat_Rune_Haste_Spotted")]
        DOTA_Chat_Rune_Haste_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_Illusion")]
        DOTA_Chat_Rune_Illusion,

        [EnumMember(Value = "#DOTA_Chat_Rune_Illusion_Spotted")]
        DOTA_Chat_Rune_Illusion_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_Invisibility")]
        DOTA_Chat_Rune_Invisibility,

        [EnumMember(Value = "#DOTA_Chat_Rune_Invisibility_Spotted")]
        DOTA_Chat_Rune_Invisibility_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_Pumpkin_Spotted")]
        DOTA_Chat_Rune_Pumpkin_Spotted,

        [EnumMember(Value = "#DOTA_Chat_Rune_Regeneration")]
        DOTA_Chat_Rune_Regeneration,

        [EnumMember(Value = "#DOTA_Chat_Rune_Regeneration_Spotted")]
        DOTA_Chat_Rune_Regeneration_Spotted,

        [EnumMember(Value = "#DOTA_Chat_ScanUsedBad")]
        DOTA_Chat_ScanUsedBad,

        [EnumMember(Value = "#DOTA_Chat_ScanUsedGood")]
        DOTA_Chat_ScanUsedGood,

        [EnumMember(Value = "#DOTA_Chat_ShareLobbyMessage")]
        DOTA_Chat_ShareLobbyMessage,

        [EnumMember(Value = "#DOTA_Chat_SharePartyMessage")]
        DOTA_Chat_SharePartyMessage,

        [EnumMember(Value = "#DOTA_Chat_ShareProfileMessage")]
        DOTA_Chat_ShareProfileMessage,

        [EnumMember(Value = "#DOTA_Chat_ShrineKilledBad")]
        DOTA_Chat_ShrineKilledBad,

        [EnumMember(Value = "#DOTA_Chat_ShrineKilledGood")]
        DOTA_Chat_ShrineKilledGood,

        [EnumMember(Value = "#DOTA_Chat_SuggestHero")]
        DOTA_Chat_SuggestHero,

        [EnumMember(Value = "#DOTA_Chat_SuggestHeroBan")]
        DOTA_Chat_SuggestHeroBan,

        [EnumMember(Value = "#DOTA_Chat_SuggestHeroRole")]
        DOTA_Chat_SuggestHeroRole,

        [EnumMember(Value = "#DOTA_Chat_SuperCreepsBad")]
        DOTA_Chat_SuperCreepsBad,

        [EnumMember(Value = "#DOTA_Chat_SuperCreepsGood")]
        DOTA_Chat_SuperCreepsGood,

        [EnumMember(Value = "#DOTA_Chat_TeamCaptainChanged")]
        DOTA_Chat_TeamCaptainChanged,

        [EnumMember(Value = "#DOTA_Chat_TimestampFormat")]
        DOTA_Chat_TimestampFormat,

        [EnumMember(Value = "#DOTA_Chat_Tip_Toast")]
        DOTA_Chat_Tip_Toast,

        [EnumMember(Value = "#DOTA_Chat_TowerKilledBad")]
        DOTA_Chat_TowerKilledBad,

        [EnumMember(Value = "#DOTA_Chat_TowerKilledGood")]
        DOTA_Chat_TowerKilledGood,

        [EnumMember(Value = "#DOTA_Chat_TripleKill")]
        DOTA_Chat_TripleKill,

        [EnumMember(Value = "#DOTA_Chat_TripleKill_noname")]
        DOTA_Chat_TripleKill_noname,

        [EnumMember(Value = "#DOTA_Chat_UnableToShareLobby")]
        DOTA_Chat_UnableToShareLobby,

        [EnumMember(Value = "#DOTA_Chat_UnableToShareParty")]
        DOTA_Chat_UnableToShareParty,

        [EnumMember(Value = "#DOTA_Chat_UnableToShareProfile")]
        DOTA_Chat_UnableToShareProfile,

        [EnumMember(Value = "#DOTA_Chat_UnknownCommandMessage")]
        DOTA_Chat_UnknownCommandMessage,

        [EnumMember(Value = "#DOTA_Chat_Wheel_All")]
        DOTA_Chat_Wheel_All,

        [EnumMember(Value = "#DOTA_Chat_Wheel_Team")]
        DOTA_Chat_Wheel_Team,

        [EnumMember(Value = "#DOTA_ClaimLevelRewardsFailed")]
        DOTA_ClaimLevelRewardsFailed,

        [EnumMember(Value = "#DOTA_ClaimLevelRewardsFailed_EventExpired")]
        DOTA_ClaimLevelRewardsFailed_EventExpired,

        [EnumMember(Value = "#DOTA_Close")]
        DOTA_Close,

        [EnumMember(Value = "#DOTA_CoachList")]
        DOTA_CoachList,

        [EnumMember(Value = "#DOTA_CoachNone")]
        DOTA_CoachNone,

        [EnumMember(Value = "#DOTA_CodeActivatedSuccessfully")]
        DOTA_CodeActivatedSuccessfully,

        [EnumMember(Value = "#DOTA_CodeAlreadyUsed")]
        DOTA_CodeAlreadyUsed,

        [EnumMember(Value = "#DOTA_CodeNotFound")]
        DOTA_CodeNotFound,

        [EnumMember(Value = "#DOTA_CodeSuccess")]
        DOTA_CodeSuccess,

        [EnumMember(Value = "#DOTA_CoinFlip_Heads")]
        DOTA_CoinFlip_Heads,

        [EnumMember(Value = "#DOTA_CoinFlip_Tails")]
        DOTA_CoinFlip_Tails,

        [EnumMember(Value = "#DOTA_CoinFlip_UnableToFlip")]
        DOTA_CoinFlip_UnableToFlip,

        [EnumMember(Value = "#DOTA_CombatLog_Interval")]
        DOTA_CombatLog_Interval,

        [EnumMember(Value = "#DOTA_CompendiumResults")]
        DOTA_CompendiumResults,

        [EnumMember(Value = "#DOTA_CompendiumTeam_Game_ResultLose")]
        DOTA_CompendiumTeam_Game_ResultLose,

        [EnumMember(Value = "#DOTA_CompendiumTeam_Game_ResultWin")]
        DOTA_CompendiumTeam_Game_ResultWin,

        [EnumMember(Value = "#DOTA_Compendium_ChooseHero")]
        DOTA_Compendium_ChooseHero,

        [EnumMember(Value = "#DOTA_Compendium_ChooseOption")]
        DOTA_Compendium_ChooseOption,

        [EnumMember(Value = "#DOTA_Compendium_ChoosePlayer")]
        DOTA_Compendium_ChoosePlayer,

        [EnumMember(Value = "#DOTA_Compendium_ChooseTeam")]
        DOTA_Compendium_ChooseTeam,

        [EnumMember(Value = "#DOTA_Compendium_Invites_OpenQualifiers")]
        DOTA_Compendium_Invites_OpenQualifiers,

        [EnumMember(Value = "#DOTA_Compendium_Invites_ToBeDetermined")]
        DOTA_Compendium_Invites_ToBeDetermined,

        [EnumMember(Value = "#DOTA_Compendium_Predictions_Final_Prize_PoolLocked")]
        DOTA_Compendium_Predictions_Final_Prize_PoolLocked,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_AllStars")]
        DOTA_Compendium_SecondaryTabHeader_AllStars,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Bracket")]
        DOTA_Compendium_SecondaryTabHeader_Bracket,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Casters")]
        DOTA_Compendium_SecondaryTabHeader_Casters,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_FallComingSoon")]
        DOTA_Compendium_SecondaryTabHeader_FallComingSoon,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Fantasy")]
        DOTA_Compendium_SecondaryTabHeader_Fantasy,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_FavTeam")]
        DOTA_Compendium_SecondaryTabHeader_FavTeam,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Invites")]
        DOTA_Compendium_SecondaryTabHeader_Invites,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Overview")]
        DOTA_Compendium_SecondaryTabHeader_Overview,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_PlayerCards")]
        DOTA_Compendium_SecondaryTabHeader_PlayerCards,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Predictions")]
        DOTA_Compendium_SecondaryTabHeader_Predictions,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Recap")]
        DOTA_Compendium_SecondaryTabHeader_Recap,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_TI6ComingSoon")]
        DOTA_Compendium_SecondaryTabHeader_TI6ComingSoon,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Teams")]
        DOTA_Compendium_SecondaryTabHeader_Teams,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_TrueSight")]
        DOTA_Compendium_SecondaryTabHeader_TrueSight,

        [EnumMember(Value = "#DOTA_Compendium_SecondaryTabHeader_Winter2017ComingSoon")]
        DOTA_Compendium_SecondaryTabHeader_Winter2017ComingSoon,

        [EnumMember(Value = "#DOTA_Compendium_Winter2016Finals")]
        DOTA_Compendium_Winter2016Finals,

        [EnumMember(Value = "#DOTA_CompetitiveMatchmaking_Header")]
        DOTA_CompetitiveMatchmaking_Header,

        [EnumMember(Value = "#DOTA_ConductScorecard_Abandon")]
        DOTA_ConductScorecard_Abandon,

        [EnumMember(Value = "#DOTA_ConductScorecard_Abandons")]
        DOTA_ConductScorecard_Abandons,

        [EnumMember(Value = "#DOTA_ConductScorecard_Abandons_1")]
        DOTA_ConductScorecard_Abandons_1,

        [EnumMember(Value = "#DOTA_ConductScorecard_Abandons_N")]
        DOTA_ConductScorecard_Abandons_N,

        [EnumMember(Value = "#DOTA_ConductScorecard_Abandons_None")]
        DOTA_ConductScorecard_Abandons_None,

        [EnumMember(Value = "#DOTA_ConductScorecard_Abandons_Pct")]
        DOTA_ConductScorecard_Abandons_Pct,

        [EnumMember(Value = "#DOTA_ConductScorecard_Commend")]
        DOTA_ConductScorecard_Commend,

        [EnumMember(Value = "#DOTA_ConductScorecard_Commends")]
        DOTA_ConductScorecard_Commends,

        [EnumMember(Value = "#DOTA_ConductScorecard_Commends_1")]
        DOTA_ConductScorecard_Commends_1,

        [EnumMember(Value = "#DOTA_ConductScorecard_Commends_N")]
        DOTA_ConductScorecard_Commends_N,

        [EnumMember(Value = "#DOTA_ConductScorecard_Commends_None")]
        DOTA_ConductScorecard_Commends_None,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reason_FirstTime")]
        DOTA_ConductScorecard_Reason_FirstTime,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reason_LowPri_Abandons")]
        DOTA_ConductScorecard_Reason_LowPri_Abandons,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reason_LowPri_AbandonsAndReports")]
        DOTA_ConductScorecard_Reason_LowPri_AbandonsAndReports,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reason_LowPri_Reports")]
        DOTA_ConductScorecard_Reason_LowPri_Reports,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reports_Bad")]
        DOTA_ConductScorecard_Reports_Bad,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reports_BadPct")]
        DOTA_ConductScorecard_Reports_BadPct,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reports_Good")]
        DOTA_ConductScorecard_Reports_Good,

        [EnumMember(Value = "#DOTA_ConductScorecard_Reports_GoodPct")]
        DOTA_ConductScorecard_Reports_GoodPct,

        [EnumMember(Value = "#DOTA_ConductScorecard_Title_FirstTime")]
        DOTA_ConductScorecard_Title_FirstTime,

        [EnumMember(Value = "#DOTA_ConductScorecard_Title_LowPri")]
        DOTA_ConductScorecard_Title_LowPri,

        [EnumMember(Value = "#DOTA_ConductScorecard_Title_New")]
        DOTA_ConductScorecard_Title_New,

        [EnumMember(Value = "#DOTA_ConductScorecard_Title_Review")]
        DOTA_ConductScorecard_Title_Review,

        [EnumMember(Value = "#DOTA_ConfirmLANLobbyDisconnect_desc")]
        DOTA_ConfirmLANLobbyDisconnect_desc,

        [EnumMember(Value = "#DOTA_ConfirmLANLobbyDisconnect_ok")]
        DOTA_ConfirmLANLobbyDisconnect_ok,

        [EnumMember(Value = "#DOTA_ConfirmLANLobbyDisconnect_title")]
        DOTA_ConfirmLANLobbyDisconnect_title,

        [EnumMember(Value = "#DOTA_ConfirmLANLobby_desc")]
        DOTA_ConfirmLANLobby_desc,

        [EnumMember(Value = "#DOTA_ConfirmOwnedItemUnbundleText")]
        DOTA_ConfirmOwnedItemUnbundleText,

        [EnumMember(Value = "#DOTA_ConfirmPurchaseText")]
        DOTA_ConfirmPurchaseText,

        [EnumMember(Value = "#DOTA_ConfirmPurchaseTitle")]
        DOTA_ConfirmPurchaseTitle,

        [EnumMember(Value = "#DOTA_ConfirmQuit")]
        DOTA_ConfirmQuit,

        [EnumMember(Value = "#DOTA_ConfirmQuitDesc")]
        DOTA_ConfirmQuitDesc,

        [EnumMember(Value = "#DOTA_ConfirmQuitDescUnsafe")]
        DOTA_ConfirmQuitDescUnsafe,

        [EnumMember(Value = "#DOTA_ConfirmSkip")]
        DOTA_ConfirmSkip,

        [EnumMember(Value = "#DOTA_ConfirmSkipDesc")]
        DOTA_ConfirmSkipDesc,

        [EnumMember(Value = "#DOTA_ConfirmUnbundleText")]
        DOTA_ConfirmUnbundleText,

        [EnumMember(Value = "#DOTA_ConfirmUnbundleTitle")]
        DOTA_ConfirmUnbundleTitle,

        [EnumMember(Value = "#DOTA_Console")]
        DOTA_Console,

        [EnumMember(Value = "#DOTA_Continue")]
        DOTA_Continue,

        [EnumMember(Value = "#DOTA_Crafting_Quality")]
        DOTA_Crafting_Quality,

        [EnumMember(Value = "#DOTA_Crafting_Quality_Output")]
        DOTA_Crafting_Quality_Output,

        [EnumMember(Value = "#DOTA_Crafting_Rarity")]
        DOTA_Crafting_Rarity,

        [EnumMember(Value = "#DOTA_Crafting_Rarity_Output")]
        DOTA_Crafting_Rarity_Output,

        [EnumMember(Value = "#DOTA_Create")]
        DOTA_Create,

        [EnumMember(Value = "#DOTA_Custom_Game")]
        DOTA_Custom_Game,

        [EnumMember(Value = "#DOTA_Custom_Game_Addon_Restricted")]
        DOTA_Custom_Game_Addon_Restricted,

        [EnumMember(Value = "#DOTA_Custom_Game_Connect")]
        DOTA_Custom_Game_Connect,

        [EnumMember(Value = "#DOTA_Custom_Game_Create_Lobby")]
        DOTA_Custom_Game_Create_Lobby,

        [EnumMember(Value = "#DOTA_Custom_Game_Download_Queued")]
        DOTA_Custom_Game_Download_Queued,

        [EnumMember(Value = "#DOTA_Custom_Game_Downloading")]
        DOTA_Custom_Game_Downloading,

        [EnumMember(Value = "#DOTA_Custom_Game_Downloading_Percent")]
        DOTA_Custom_Game_Downloading_Percent,

        [EnumMember(Value = "#DOTA_Custom_Game_FileSize")]
        DOTA_Custom_Game_FileSize,

        [EnumMember(Value = "#DOTA_Custom_Game_Game_Page")]
        DOTA_Custom_Game_Game_Page,

        [EnumMember(Value = "#DOTA_Custom_Game_Info")]
        DOTA_Custom_Game_Info,

        [EnumMember(Value = "#DOTA_Custom_Game_Install")]
        DOTA_Custom_Game_Install,

        [EnumMember(Value = "#DOTA_Custom_Game_Installed")]
        DOTA_Custom_Game_Installed,

        [EnumMember(Value = "#DOTA_Custom_Game_Lobby_Count")]
        DOTA_Custom_Game_Lobby_Count,

        [EnumMember(Value = "#DOTA_Custom_Game_Lobby_Count_Plural")]
        DOTA_Custom_Game_Lobby_Count_Plural,

        [EnumMember(Value = "#DOTA_Custom_Game_Map_AnyNumbered")]
        DOTA_Custom_Game_Map_AnyNumbered,

        [EnumMember(Value = "#DOTA_Custom_Game_Needs_Update")]
        DOTA_Custom_Game_Needs_Update,

        [EnumMember(Value = "#DOTA_Custom_Game_Not_Installed")]
        DOTA_Custom_Game_Not_Installed,

        [EnumMember(Value = "#DOTA_Custom_Game_Quick_Join")]
        DOTA_Custom_Game_Quick_Join,

        [EnumMember(Value = "#DOTA_Custom_Game_Search")]
        DOTA_Custom_Game_Search,

        [EnumMember(Value = "#DOTA_Custom_Game_Subscription")]
        DOTA_Custom_Game_Subscription,

        [EnumMember(Value = "#DOTA_Custom_Game_Subscriptions")]
        DOTA_Custom_Game_Subscriptions,

        [EnumMember(Value = "#DOTA_Custom_Game_Tooltip_Subscriber")]
        DOTA_Custom_Game_Tooltip_Subscriber,

        [EnumMember(Value = "#DOTA_Custom_Game_Tooltip_Subscribers")]
        DOTA_Custom_Game_Tooltip_Subscribers,

        [EnumMember(Value = "#DOTA_Custom_Game_Uninstall")]
        DOTA_Custom_Game_Uninstall,

        [EnumMember(Value = "#DOTA_Custom_Game_Update")]
        DOTA_Custom_Game_Update,

        [EnumMember(Value = "#DOTA_Custom_Game_Update_Note_Title")]
        DOTA_Custom_Game_Update_Note_Title,

        [EnumMember(Value = "#DOTA_Custom_Game_View_Details")]
        DOTA_Custom_Game_View_Details,

        [EnumMember(Value = "#DOTA_Custom_Game_View_Lobbies")]
        DOTA_Custom_Game_View_Lobbies,

        [EnumMember(Value = "#DOTA_Custom_Game_Workshop_Page")]
        DOTA_Custom_Game_Workshop_Page,

        [EnumMember(Value = "#DOTA_DB_SeasonPass_TierTooltipBronze")]
        DOTA_DB_SeasonPass_TierTooltipBronze,

        [EnumMember(Value = "#DOTA_DB_SeasonPass_TierTooltipGold")]
        DOTA_DB_SeasonPass_TierTooltipGold,

        [EnumMember(Value = "#DOTA_DB_SeasonPass_TierTooltipMax")]
        DOTA_DB_SeasonPass_TierTooltipMax,

        [EnumMember(Value = "#DOTA_DB_SeasonPass_TierTooltipSilver")]
        DOTA_DB_SeasonPass_TierTooltipSilver,

        [EnumMember(Value = "#DOTA_Date_Format_DayOfWeek")]
        DOTA_Date_Format_DayOfWeek,

        [EnumMember(Value = "#DOTA_Date_Format_DayOfWeek_Day_Month_Hour_Minute_12")]
        DOTA_Date_Format_DayOfWeek_Day_Month_Hour_Minute_12,

        [EnumMember(Value = "#DOTA_Date_Format_DayOfWeek_Day_Month_Hour_Minute_24")]
        DOTA_Date_Format_DayOfWeek_Day_Month_Hour_Minute_24,

        [EnumMember(Value = "#DOTA_Date_Format_Day_Month")]
        DOTA_Date_Format_Day_Month,

        [EnumMember(Value = "#DOTA_Date_Format_Day_Month_Year")]
        DOTA_Date_Format_Day_Month_Year,

        [EnumMember(Value = "#DOTA_Date_Format_Day_Month_Year_Hour_Minute_12")]
        DOTA_Date_Format_Day_Month_Year_Hour_Minute_12,

        [EnumMember(Value = "#DOTA_Date_Format_Day_Month_Year_Hour_Minute_24")]
        DOTA_Date_Format_Day_Month_Year_Hour_Minute_24,

        [EnumMember(Value = "#DOTA_Date_Format_Day_Month_Year_Hour_Minute_Second_12")]
        DOTA_Date_Format_Day_Month_Year_Hour_Minute_Second_12,

        [EnumMember(Value = "#DOTA_Date_Format_Day_Month_Year_Hour_Minute_Second_24")]
        DOTA_Date_Format_Day_Month_Year_Hour_Minute_Second_24,

        [EnumMember(Value = "#DOTA_Date_Format_Hour_Minute_12")]
        DOTA_Date_Format_Hour_Minute_12,

        [EnumMember(Value = "#DOTA_Date_Format_Hour_Minute_24")]
        DOTA_Date_Format_Hour_Minute_24,

        [EnumMember(Value = "#DOTA_Dismiss")]
        DOTA_Dismiss,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Brand")]
        DOTA_EconItemSearchPrefix_Brand,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Description")]
        DOTA_EconItemSearchPrefix_Description,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Event")]
        DOTA_EconItemSearchPrefix_Event,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Giftable")]
        DOTA_EconItemSearchPrefix_Giftable,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_HeroID")]
        DOTA_EconItemSearchPrefix_HeroID,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_HeroName")]
        DOTA_EconItemSearchPrefix_HeroName,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Hidden")]
        DOTA_EconItemSearchPrefix_Hidden,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Keyword")]
        DOTA_EconItemSearchPrefix_Keyword,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Name")]
        DOTA_EconItemSearchPrefix_Name,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Prefab")]
        DOTA_EconItemSearchPrefix_Prefab,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Public")]
        DOTA_EconItemSearchPrefix_Public,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Rarity")]
        DOTA_EconItemSearchPrefix_Rarity,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Recent")]
        DOTA_EconItemSearchPrefix_Recent,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Sale")]
        DOTA_EconItemSearchPrefix_Sale,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Slot")]
        DOTA_EconItemSearchPrefix_Slot,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_SocketGemItemDef")]
        DOTA_EconItemSearchPrefix_SocketGemItemDef,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_SocketRequiredHero")]
        DOTA_EconItemSearchPrefix_SocketRequiredHero,

        [EnumMember(Value = "#DOTA_EconItemSearchPrefix_Tool")]
        DOTA_EconItemSearchPrefix_Tool,

        [EnumMember(Value = "#DOTA_EconNotification_Header")]
        DOTA_EconNotification_Header,

        [EnumMember(Value = "#DOTA_Edit")]
        DOTA_Edit,

        [EnumMember(Value = "#DOTA_EffigyEditor")]
        DOTA_EffigyEditor,

        [EnumMember(Value = "#DOTA_EffigyEditor_ConfirmCreate")]
        DOTA_EffigyEditor_ConfirmCreate,

        [EnumMember(Value = "#DOTA_EffigyEditor_ConfirmReforge")]
        DOTA_EffigyEditor_ConfirmReforge,

        [EnumMember(Value = "#DOTA_EffigyEditor_Error")]
        DOTA_EffigyEditor_Error,

        [EnumMember(Value = "#DOTA_EffigyEditor_ErrorPose")]
        DOTA_EffigyEditor_ErrorPose,

        [EnumMember(Value = "#DOTA_EffigyEditor_ErrorReforge")]
        DOTA_EffigyEditor_ErrorReforge,

        [EnumMember(Value = "#DOTA_EffigyEditor_SuccessPose")]
        DOTA_EffigyEditor_SuccessPose,

        [EnumMember(Value = "#DOTA_EffigyEditor_SuccessReforge")]
        DOTA_EffigyEditor_SuccessReforge,

        [EnumMember(Value = "#DOTA_Egg_Confirm")]
        DOTA_Egg_Confirm,

        [EnumMember(Value = "#DOTA_EmoticonPanel_Advertisement_NotOpened")]
        DOTA_EmoticonPanel_Advertisement_NotOpened,

        [EnumMember(Value = "#DOTA_EnemyAbility_Ping")]
        DOTA_EnemyAbility_Ping,

        [EnumMember(Value = "#DOTA_EnemyItem_Alert")]
        DOTA_EnemyItem_Alert,

        [EnumMember(Value = "#DOTA_Engine_Mismatch_Header")]
        DOTA_Engine_Mismatch_Header,

        [EnumMember(Value = "#DOTA_Engine_Mismatch_Message")]
        DOTA_Engine_Mismatch_Message,

        [EnumMember(Value = "#DOTA_EquipFullSet")]
        DOTA_EquipFullSet,

        [EnumMember(Value = "#DOTA_EquippedItem_AddToShuffle")]
        DOTA_EquippedItem_AddToShuffle,

        [EnumMember(Value = "#DOTA_EquippedItem_CustomSet")]
        DOTA_EquippedItem_CustomSet,

        [EnumMember(Value = "#DOTA_EquippedItem_Customize")]
        DOTA_EquippedItem_Customize,

        [EnumMember(Value = "#DOTA_EquippedItem_DefaultSet")]
        DOTA_EquippedItem_DefaultSet,

        [EnumMember(Value = "#DOTA_EquippedItem_Details")]
        DOTA_EquippedItem_Details,

        [EnumMember(Value = "#DOTA_EquippedItem_Equip")]
        DOTA_EquippedItem_Equip,

        [EnumMember(Value = "#DOTA_EquippedItem_Item")]
        DOTA_EquippedItem_Item,

        [EnumMember(Value = "#DOTA_EquippedItem_ItemSet")]
        DOTA_EquippedItem_ItemSet,

        [EnumMember(Value = "#DOTA_EquippedItem_Label")]
        DOTA_EquippedItem_Label,

        [EnumMember(Value = "#DOTA_EquippedItem_RemoveFromShuffle")]
        DOTA_EquippedItem_RemoveFromShuffle,

        [EnumMember(Value = "#DOTA_EquippedItem_Unequip")]
        DOTA_EquippedItem_Unequip,

        [EnumMember(Value = "#DOTA_EquippedItem_Unpack_And_Equip")]
        DOTA_EquippedItem_Unpack_And_Equip,

        [EnumMember(Value = "#DOTA_Error")]
        DOTA_Error,

        [EnumMember(Value = "#DOTA_EventGameName_International2017_Act1_Easy")]
        DOTA_EventGameName_International2017_Act1_Easy,

        [EnumMember(Value = "#DOTA_EventGameName_International2017_Act1_Hard")]
        DOTA_EventGameName_International2017_Act1_Hard,

        [EnumMember(Value = "#DOTA_EventGameName_International2017_Act2_Easy")]
        DOTA_EventGameName_International2017_Act2_Easy,

        [EnumMember(Value = "#DOTA_EventGameName_International2017_Act2_Hard")]
        DOTA_EventGameName_International2017_Act2_Hard,

        [EnumMember(Value = "#DOTA_EventPointsPurchaseFailed_GenericError")]
        DOTA_EventPointsPurchaseFailed_GenericError,

        [EnumMember(Value = "#DOTA_EventPointsPurchaseFailed_NotEnoughPoints")]
        DOTA_EventPointsPurchaseFailed_NotEnoughPoints,

        [EnumMember(Value = "#DOTA_EventPointsPurchaseFailed_Title")]
        DOTA_EventPointsPurchaseFailed_Title,

        [EnumMember(Value = "#DOTA_EventRewardAlreadyPassed_Message")]
        DOTA_EventRewardAlreadyPassed_Message,

        [EnumMember(Value = "#DOTA_EventRewardAlreadyPassed_Title")]
        DOTA_EventRewardAlreadyPassed_Title,

        [EnumMember(Value = "#DOTA_EventTicket")]
        DOTA_EventTicket,

        [EnumMember(Value = "#DOTA_Event_Points_Confirm")]
        DOTA_Event_Points_Confirm,

        [EnumMember(Value = "#DOTA_FantasyChangeCard")]
        DOTA_FantasyChangeCard,

        [EnumMember(Value = "#DOTA_FantasyDropdownFilter")]
        DOTA_FantasyDropdownFilter,

        [EnumMember(Value = "#DOTA_FantasyLockUntilTimeD")]
        DOTA_FantasyLockUntilTimeD,

        [EnumMember(Value = "#DOTA_FantasyLockUntilTimeH")]
        DOTA_FantasyLockUntilTimeH,

        [EnumMember(Value = "#DOTA_FantasySelectCard")]
        DOTA_FantasySelectCard,

        [EnumMember(Value = "#DOTA_FeaturedHeroReason_FeaturedItem")]
        DOTA_FeaturedHeroReason_FeaturedItem,

        [EnumMember(Value = "#DOTA_FeaturedHeroReason_FrequentlyPlayedHero")]
        DOTA_FeaturedHeroReason_FrequentlyPlayedHero,

        [EnumMember(Value = "#DOTA_FeaturedHeroReason_NewHero")]
        DOTA_FeaturedHeroReason_NewHero,

        [EnumMember(Value = "#DOTA_FeaturedHeroReason_NewItem")]
        DOTA_FeaturedHeroReason_NewItem,

        [EnumMember(Value = "#DOTA_FeaturedHeroReason_PopularItem")]
        DOTA_FeaturedHeroReason_PopularItem,

        [EnumMember(Value = "#DOTA_FeaturedHeroReason_SaleItem")]
        DOTA_FeaturedHeroReason_SaleItem,

        [EnumMember(Value = "#DOTA_Feed_Timestamp_Date")]
        DOTA_Feed_Timestamp_Date,

        [EnumMember(Value = "#DOTA_Feed_Timestamp_HourAgo")]
        DOTA_Feed_Timestamp_HourAgo,

        [EnumMember(Value = "#DOTA_Feed_Timestamp_HoursAgo")]
        DOTA_Feed_Timestamp_HoursAgo,

        [EnumMember(Value = "#DOTA_Feed_Timestamp_MinutesAgo")]
        DOTA_Feed_Timestamp_MinutesAgo,

        [EnumMember(Value = "#DOTA_Feed_Timestamp_Recently")]
        DOTA_Feed_Timestamp_Recently,

        [EnumMember(Value = "#DOTA_Feed_Timestamp_Yesterday")]
        DOTA_Feed_Timestamp_Yesterday,

        [EnumMember(Value = "#DOTA_FindCustomGameMatch")]
        DOTA_FindCustomGameMatch,

        [EnumMember(Value = "#DOTA_FindGameModesMany")]
        DOTA_FindGameModesMany,

        [EnumMember(Value = "#DOTA_FindMatch")]
        DOTA_FindMatch,

        [EnumMember(Value = "#DOTA_FindRegionsMany")]
        DOTA_FindRegionsMany,

        [EnumMember(Value = "#DOTA_FindSetMarketplace")]
        DOTA_FindSetMarketplace,

        [EnumMember(Value = "#DOTA_FriendCount_Plural")]
        DOTA_FriendCount_Plural,

        [EnumMember(Value = "#DOTA_FriendCount_Singular")]
        DOTA_FriendCount_Singular,

        [EnumMember(Value = "#DOTA_Friend_Finding_Match")]
        DOTA_Friend_Finding_Match,

        [EnumMember(Value = "#DOTA_Friend_Game_Mode_1_Friend")]
        DOTA_Friend_Game_Mode_1_Friend,

        [EnumMember(Value = "#DOTA_Friend_Game_Mode_N_Friends")]
        DOTA_Friend_Game_Mode_N_Friends,

        [EnumMember(Value = "#DOTA_Friend_Game_Mode_Self")]
        DOTA_Friend_Game_Mode_Self,

        [EnumMember(Value = "#DOTA_Friend_In_Match")]
        DOTA_Friend_In_Match,

        [EnumMember(Value = "#DOTA_Friend_In_Open_Party_Self")]
        DOTA_Friend_In_Open_Party_Self,

        [EnumMember(Value = "#DOTA_Friend_In_Party_Open")]
        DOTA_Friend_In_Party_Open,

        [EnumMember(Value = "#DOTA_Friend_Invite_To_Party")]
        DOTA_Friend_Invite_To_Party,

        [EnumMember(Value = "#DOTA_Friend_JoinParty")]
        DOTA_Friend_JoinParty,

        [EnumMember(Value = "#DOTA_Friend_Lobby_Mode_1_Friend")]
        DOTA_Friend_Lobby_Mode_1_Friend,

        [EnumMember(Value = "#DOTA_Friend_Lobby_Mode_N_Friends")]
        DOTA_Friend_Lobby_Mode_N_Friends,

        [EnumMember(Value = "#DOTA_Friend_Lobby_Mode_Self")]
        DOTA_Friend_Lobby_Mode_Self,

        [EnumMember(Value = "#DOTA_Friend_Solo_Open_Party")]
        DOTA_Friend_Solo_Open_Party,

        [EnumMember(Value = "#DOTA_Friend_Solo_Open_Party_Self")]
        DOTA_Friend_Solo_Open_Party_Self,

        [EnumMember(Value = "#DOTA_Friend_SuggestInviteToParty")]
        DOTA_Friend_SuggestInviteToParty,

        [EnumMember(Value = "#DOTA_Friend_Watch")]
        DOTA_Friend_Watch,

        [EnumMember(Value = "#DOTA_Friends_In_Party_Open")]
        DOTA_Friends_In_Party_Open,

        [EnumMember(Value = "#DOTA_Frostivus_Altar")]
        DOTA_Frostivus_Altar,

        [EnumMember(Value = "#DOTA_GCRequest_Timeout")]
        DOTA_GCRequest_Timeout,

        [EnumMember(Value = "#DOTA_GameMode_{0}")]
        DOTA_GameMode_NUM,

        [EnumMember(Value = "#DOTA_GameMode_19_Generic")]
        DOTA_GameMode_19_Generic,

        [EnumMember(Value = "#DOTA_GemCombine_Action")]
        DOTA_GemCombine_Action,

        [EnumMember(Value = "#DOTA_GemCombine_Text")]
        DOTA_GemCombine_Text,

        [EnumMember(Value = "#DOTA_GemCombine_Title")]
        DOTA_GemCombine_Title,

        [EnumMember(Value = "#DOTA_GemCombiner_Confirm")]
        DOTA_GemCombiner_Confirm,

        [EnumMember(Value = "#DOTA_GemCombiner_ConfirmTitle")]
        DOTA_GemCombiner_ConfirmTitle,

        [EnumMember(Value = "#DOTA_GiftRestrictions_FriendshipAge_Default")]
        DOTA_GiftRestrictions_FriendshipAge_Default,

        [EnumMember(Value = "#DOTA_GiftRestrictions_FriendshipAge_TwoFactor")]
        DOTA_GiftRestrictions_FriendshipAge_TwoFactor,

        [EnumMember(Value = "#DOTA_GiftRestrictions_Loading")]
        DOTA_GiftRestrictions_Loading,

        [EnumMember(Value = "#DOTA_GiftWrap_ConfirmDedication_Message")]
        DOTA_GiftWrap_ConfirmDedication_Message,

        [EnumMember(Value = "#DOTA_GiftWrap_ConfirmDedication_Title")]
        DOTA_GiftWrap_ConfirmDedication_Title,

        [EnumMember(Value = "#DOTA_GiftWrap_Error_RateLimitedCharge")]
        DOTA_GiftWrap_Error_RateLimitedCharge,

        [EnumMember(Value = "#DOTA_GiftWrap_Error_RateLimitedChargeNone")]
        DOTA_GiftWrap_Error_RateLimitedChargeNone,

        [EnumMember(Value = "#DOTA_GiftWrap_Error_RateLimitedNormal")]
        DOTA_GiftWrap_Error_RateLimitedNormal,

        [EnumMember(Value = "#DOTA_GiftWrap_NoFriends")]
        DOTA_GiftWrap_NoFriends,

        [EnumMember(Value = "#DOTA_GiftWrap_Success")]
        DOTA_GiftWrap_Success,

        [EnumMember(Value = "#DOTA_GiftWrap_Success_Header")]
        DOTA_GiftWrap_Success_Header,

        [EnumMember(Value = "#DOTA_Gift_Confirm")]
        DOTA_Gift_Confirm,

        [EnumMember(Value = "#DOTA_GlobalItems_Emoticons")]
        DOTA_GlobalItems_Emoticons,

        [EnumMember(Value = "#DOTA_Gold_Graph_Title")]
        DOTA_Gold_Graph_Title,

        [EnumMember(Value = "#DOTA_Gold_Graph_Title_Net_Worth")]
        DOTA_Gold_Graph_Title_Net_Worth,

        [EnumMember(Value = "#DOTA_Gold_Graph_Total")]
        DOTA_Gold_Graph_Total,

        [EnumMember(Value = "#DOTA_Gold_Graph_Total_Net_Worth")]
        DOTA_Gold_Graph_Total_Net_Worth,

        [EnumMember(Value = "#DOTA_GoodGuys")]
        DOTA_GoodGuys,

        [EnumMember(Value = "#DOTA_GoodGuysShort")]
        DOTA_GoodGuysShort,

        [EnumMember(Value = "#DOTA_Guide_Ability_Guide")]
        DOTA_Guide_Ability_Guide,

        [EnumMember(Value = "#DOTA_Guide_AllLanguages")]
        DOTA_Guide_AllLanguages,

        [EnumMember(Value = "#DOTA_Guide_Item_Guide")]
        DOTA_Guide_Item_Guide,

        [EnumMember(Value = "#DOTA_Guide_Suggested_Ability")]
        DOTA_Guide_Suggested_Ability,

        [EnumMember(Value = "#DOTA_Guide_Valve")]
        DOTA_Guide_Valve,

        [EnumMember(Value = "#DOTA_GuildKicked_Header")]
        DOTA_GuildKicked_Header,

        [EnumMember(Value = "#DOTA_GuildKicked_Message")]
        DOTA_GuildKicked_Message,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Enemy")]
        DOTA_HPMana_Alert_Enemy,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Enemy_Dead")]
        DOTA_HPMana_Alert_Enemy_Dead,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Enemy_NPC")]
        DOTA_HPMana_Alert_Enemy_NPC,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Glyph_Cooldown")]
        DOTA_HPMana_Alert_Glyph_Cooldown,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Glyph_Ready")]
        DOTA_HPMana_Alert_Glyph_Ready,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Glyph_Wait")]
        DOTA_HPMana_Alert_Glyph_Wait,

        [EnumMember(Value = "#DOTA_HPMana_Alert_NPC")]
        DOTA_HPMana_Alert_NPC,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Self")]
        DOTA_HPMana_Alert_Self,

        [EnumMember(Value = "#DOTA_HPMana_Alert_Self_Dead")]
        DOTA_HPMana_Alert_Self_Dead,

        [EnumMember(Value = "#DOTA_HP_Alert_Enemy_NPC")]
        DOTA_HP_Alert_Enemy_NPC,

        [EnumMember(Value = "#DOTA_HP_Alert_Healer_Cooldown")]
        DOTA_HP_Alert_Healer_Cooldown,

        [EnumMember(Value = "#DOTA_HP_Alert_Healer_Ready")]
        DOTA_HP_Alert_Healer_Ready,

        [EnumMember(Value = "#DOTA_HP_Alert_NPC")]
        DOTA_HP_Alert_NPC,

        [EnumMember(Value = "#DOTA_HS_CurrentlyEquipped")]
        DOTA_HS_CurrentlyEquipped,

        [EnumMember(Value = "#DOTA_HUD_ActivatedRune")]
        DOTA_HUD_ActivatedRune,

        [EnumMember(Value = "#DOTA_HUD_AegisStolen")]
        DOTA_HUD_AegisStolen,

        [EnumMember(Value = "#DOTA_HUD_BarracksKilled")]
        DOTA_HUD_BarracksKilled,

        [EnumMember(Value = "#DOTA_HUD_BottledRune")]
        DOTA_HUD_BottledRune,

        [EnumMember(Value = "#DOTA_HUD_BoughtBack")]
        DOTA_HUD_BoughtBack,

        [EnumMember(Value = "#DOTA_HUD_Broadcaster_Channel")]
        DOTA_HUD_Broadcaster_Channel,

        [EnumMember(Value = "#DOTA_HUD_Day")]
        DOTA_HUD_Day,

        [EnumMember(Value = "#DOTA_HUD_DeniedAegis")]
        DOTA_HUD_DeniedAegis,

        [EnumMember(Value = "#DOTA_HUD_DireCourierKilled")]
        DOTA_HUD_DireCourierKilled,

        [EnumMember(Value = "#DOTA_HUD_DireCourierRespawned")]
        DOTA_HUD_DireCourierRespawned,

        [EnumMember(Value = "#DOTA_HUD_DireGlyphUsed")]
        DOTA_HUD_DireGlyphUsed,

        [EnumMember(Value = "#DOTA_HUD_Double_Down_Dialog_Cancel")]
        DOTA_HUD_Double_Down_Dialog_Cancel,

        [EnumMember(Value = "#DOTA_HUD_Double_Down_Dialog_Option0")]
        DOTA_HUD_Double_Down_Dialog_Option0,

        [EnumMember(Value = "#DOTA_HUD_Double_Down_Dialog_Option1")]
        DOTA_HUD_Double_Down_Dialog_Option1,

        [EnumMember(Value = "#DOTA_HUD_Double_Down_Dialog_Text")]
        DOTA_HUD_Double_Down_Dialog_Text,

        [EnumMember(Value = "#DOTA_HUD_Double_Down_Dialog_Text1")]
        DOTA_HUD_Double_Down_Dialog_Text1,

        [EnumMember(Value = "#DOTA_HUD_Double_Down_Dialog_Title")]
        DOTA_HUD_Double_Down_Dialog_Title,

        [EnumMember(Value = "#DOTA_HUD_Gold_Each")]
        DOTA_HUD_Gold_Each,

        [EnumMember(Value = "#DOTA_HUD_HeroSelection_AllPick_Draft")]
        DOTA_HUD_HeroSelection_AllPick_Draft,

        [EnumMember(Value = "#DOTA_HUD_Horn")]
        DOTA_HUD_Horn,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_10_Ended")]
        DOTA_HUD_KillStreak_10_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_3_Ended")]
        DOTA_HUD_KillStreak_3_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_4_Ended")]
        DOTA_HUD_KillStreak_4_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_5_Ended")]
        DOTA_HUD_KillStreak_5_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_6_Ended")]
        DOTA_HUD_KillStreak_6_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_7_Ended")]
        DOTA_HUD_KillStreak_7_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_8_Ended")]
        DOTA_HUD_KillStreak_8_Ended,

        [EnumMember(Value = "#DOTA_HUD_KillStreak_9_Ended")]
        DOTA_HUD_KillStreak_9_Ended,

        [EnumMember(Value = "#DOTA_HUD_Killcam_AttackDmg")]
        DOTA_HUD_Killcam_AttackDmg,

        [EnumMember(Value = "#DOTA_HUD_KilledHUDFlippedRoshanBad")]
        DOTA_HUD_KilledHUDFlippedRoshanBad,

        [EnumMember(Value = "#DOTA_HUD_KilledHUDFlippedRoshanGood")]
        DOTA_HUD_KilledHUDFlippedRoshanGood,

        [EnumMember(Value = "#DOTA_HUD_KilledRoshanBad")]
        DOTA_HUD_KilledRoshanBad,

        [EnumMember(Value = "#DOTA_HUD_KilledRoshanGood")]
        DOTA_HUD_KilledRoshanGood,

        [EnumMember(Value = "#DOTA_HUD_Night")]
        DOTA_HUD_Night,

        [EnumMember(Value = "#DOTA_HUD_PickedUpAegis")]
        DOTA_HUD_PickedUpAegis,

        [EnumMember(Value = "#DOTA_HUD_PurchaseCourier")]
        DOTA_HUD_PurchaseCourier,

        [EnumMember(Value = "#DOTA_HUD_QuestStatus_QueryComplete")]
        DOTA_HUD_QuestStatus_QueryComplete,

        [EnumMember(Value = "#DOTA_HUD_RadiantCourierKilled")]
        DOTA_HUD_RadiantCourierKilled,

        [EnumMember(Value = "#DOTA_HUD_RadiantCourierRespawned")]
        DOTA_HUD_RadiantCourierRespawned,

        [EnumMember(Value = "#DOTA_HUD_RadiantGlyphUsed")]
        DOTA_HUD_RadiantGlyphUsed,

        [EnumMember(Value = "#DOTA_HUD_RespawnOutOfLives")]
        DOTA_HUD_RespawnOutOfLives,

        [EnumMember(Value = "#DOTA_HUD_RespawnTime")]
        DOTA_HUD_RespawnTime,

        [EnumMember(Value = "#DOTA_HUD_Rune_Arcane")]
        DOTA_HUD_Rune_Arcane,

        [EnumMember(Value = "#DOTA_HUD_Rune_Bounty")]
        DOTA_HUD_Rune_Bounty,

        [EnumMember(Value = "#DOTA_HUD_Rune_DoubleDamage")]
        DOTA_HUD_Rune_DoubleDamage,

        [EnumMember(Value = "#DOTA_HUD_Rune_Haste")]
        DOTA_HUD_Rune_Haste,

        [EnumMember(Value = "#DOTA_HUD_Rune_Illusion")]
        DOTA_HUD_Rune_Illusion,

        [EnumMember(Value = "#DOTA_HUD_Rune_Invisibility")]
        DOTA_HUD_Rune_Invisibility,

        [EnumMember(Value = "#DOTA_HUD_Rune_Regeneration")]
        DOTA_HUD_Rune_Regeneration,

        [EnumMember(Value = "#DOTA_HUD_ScanUsed")]
        DOTA_HUD_ScanUsed,

        [EnumMember(Value = "#DOTA_HUD_Scoreboard_SwapHero")]
        DOTA_HUD_Scoreboard_SwapHero,

        [EnumMember(Value = "#DOTA_HUD_SelectCourier")]
        DOTA_HUD_SelectCourier,

        [EnumMember(Value = "#DOTA_HUD_Swap_Confirm_Header")]
        DOTA_HUD_Swap_Confirm_Header,

        [EnumMember(Value = "#DOTA_HUD_Swap_Confirm_Message")]
        DOTA_HUD_Swap_Confirm_Message,

        [EnumMember(Value = "#DOTA_HUD_Voice_PartyOverlayMixed")]
        DOTA_HUD_Voice_PartyOverlayMixed,

        [EnumMember(Value = "#DOTA_HUD_Wagering_SpentToken")]
        DOTA_HUD_Wagering_SpentToken,

        [EnumMember(Value = "#DOTA_HUD_Wagering_SpentToken1")]
        DOTA_HUD_Wagering_SpentToken1,

        [EnumMember(Value = "#DOTA_HUD_Wagering_SpentTokenTitle")]
        DOTA_HUD_Wagering_SpentTokenTitle,

        [EnumMember(Value = "#DOTA_HallOfFame_Glow_Description")]
        DOTA_HallOfFame_Glow_Description,

        [EnumMember(Value = "#DOTA_HallOfFame_Glow_Title")]
        DOTA_HallOfFame_Glow_Title,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Banished")]
        DOTA_HealthBar_Status_Banished,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Break")]
        DOTA_HealthBar_Status_Break,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Disarmed")]
        DOTA_HealthBar_Status_Disarmed,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Fear")]
        DOTA_HealthBar_Status_Fear,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Hexed")]
        DOTA_HealthBar_Status_Hexed,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Muted")]
        DOTA_HealthBar_Status_Muted,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Rooted")]
        DOTA_HealthBar_Status_Rooted,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Silenced")]
        DOTA_HealthBar_Status_Silenced,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Sleep")]
        DOTA_HealthBar_Status_Sleep,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Stunned")]
        DOTA_HealthBar_Status_Stunned,

        [EnumMember(Value = "#DOTA_HealthBar_Status_Taunted")]
        DOTA_HealthBar_Status_Taunted,

        [EnumMember(Value = "#DOTA_HeroCustomize_SelectItemsInSet")]
        DOTA_HeroCustomize_SelectItemsInSet,

        [EnumMember(Value = "#DOTA_HeroCustomize_StyleLocked")]
        DOTA_HeroCustomize_StyleLocked,

        [EnumMember(Value = "#DOTA_HeroGuideBuilder_AddNewGroup")]
        DOTA_HeroGuideBuilder_AddNewGroup,

        [EnumMember(Value = "#DOTA_HeroGuide_BadTalentLevels_Body")]
        DOTA_HeroGuide_BadTalentLevels_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_BadTalentLevels_Title")]
        DOTA_HeroGuide_BadTalentLevels_Title,

        [EnumMember(Value = "#DOTA_HeroGuide_ConfirmDelete_Body")]
        DOTA_HeroGuide_ConfirmDelete_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_ConfirmDelete_Title")]
        DOTA_HeroGuide_ConfirmDelete_Title,

        [EnumMember(Value = "#DOTA_HeroGuide_DeleteFailed_Body")]
        DOTA_HeroGuide_DeleteFailed_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_DeleteFailed_DoNotOwn_Body")]
        DOTA_HeroGuide_DeleteFailed_DoNotOwn_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_DeleteFailed_Title")]
        DOTA_HeroGuide_DeleteFailed_Title,

        [EnumMember(Value = "#DOTA_HeroGuide_ErrorCreatingWorkshopItem_Body")]
        DOTA_HeroGuide_ErrorCreatingWorkshopItem_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_ErrorPublishingGuide_Title")]
        DOTA_HeroGuide_ErrorPublishingGuide_Title,

        [EnumMember(Value = "#DOTA_HeroGuide_ErrorSavingGuide_Title")]
        DOTA_HeroGuide_ErrorSavingGuide_Title,

        [EnumMember(Value = "#DOTA_HeroGuide_ErrorUpdatingWorkshopItem_Body")]
        DOTA_HeroGuide_ErrorUpdatingWorkshopItem_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_ErrorWritingFile_Body")]
        DOTA_HeroGuide_ErrorWritingFile_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_PublishingGuide_Body")]
        DOTA_HeroGuide_PublishingGuide_Body,

        [EnumMember(Value = "#DOTA_HeroGuide_PublishingGuide_Title")]
        DOTA_HeroGuide_PublishingGuide_Title,

        [EnumMember(Value = "#DOTA_HeroGuide_Role_None")]
        DOTA_HeroGuide_Role_None,

        [EnumMember(Value = "#DOTA_HeroGuide_Untitled")]
        DOTA_HeroGuide_Untitled,

        [EnumMember(Value = "#DOTA_HeroLoadout_BaseSet")]
        DOTA_HeroLoadout_BaseSet,

        [EnumMember(Value = "#DOTA_HeroLoadout_GlobalFilterName")]
        DOTA_HeroLoadout_GlobalFilterName,

        [EnumMember(Value = "#DOTA_HeroLoadout_HeroFilterName")]
        DOTA_HeroLoadout_HeroFilterName,

        [EnumMember(Value = "#DOTA_HeroLoadout_Search")]
        DOTA_HeroLoadout_Search,

        [EnumMember(Value = "#DOTA_HeroLoadout_UnknownSet")]
        DOTA_HeroLoadout_UnknownSet,

        [EnumMember(Value = "#DOTA_HeroRole_{0}")]
        DOTA_HeroRole_STRING,

        [EnumMember(Value = "#DOTA_HeroRole_Description_{0}")]
        DOTA_HeroRole_Description_STRING,

        [EnumMember(Value = "#DOTA_HeroSelectorCategory_PrimaryAttribute_Agility")]
        DOTA_HeroSelectorCategory_PrimaryAttribute_Agility,

        [EnumMember(Value = "#DOTA_HeroSelectorCategory_PrimaryAttribute_Intelligence")]
        DOTA_HeroSelectorCategory_PrimaryAttribute_Intelligence,

        [EnumMember(Value = "#DOTA_HeroSelectorCategory_PrimaryAttribute_Strength")]
        DOTA_HeroSelectorCategory_PrimaryAttribute_Strength,

        [EnumMember(Value = "#DOTA_HeroStatue_Likes")]
        DOTA_HeroStatue_Likes,

        [EnumMember(Value = "#DOTA_HeroStatue_ReforgeFirstTime")]
        DOTA_HeroStatue_ReforgeFirstTime,

        [EnumMember(Value = "#DOTA_HeroStatue_ReforgeFirstTimeHeader")]
        DOTA_HeroStatue_ReforgeFirstTimeHeader,

        [EnumMember(Value = "#DOTA_HeroTooltip_HeroRecord")]
        DOTA_HeroTooltip_HeroRecord,

        [EnumMember(Value = "#DOTA_Hero_Selection_AGI")]
        DOTA_Hero_Selection_AGI,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_BanSelected_TheyFirst")]
        DOTA_Hero_Selection_AllDraft_BanSelected_TheyFirst,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_BanSelected_YouFirst")]
        DOTA_Hero_Selection_AllDraft_BanSelected_YouFirst,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_Planning_TheyFirst")]
        DOTA_Hero_Selection_AllDraft_Planning_TheyFirst,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_Planning_YouFirst")]
        DOTA_Hero_Selection_AllDraft_Planning_YouFirst,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_TheyPicking")]
        DOTA_Hero_Selection_AllDraft_TheyPicking,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_TheyPicking_LosingGold")]
        DOTA_Hero_Selection_AllDraft_TheyPicking_LosingGold,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_YouPicking")]
        DOTA_Hero_Selection_AllDraft_YouPicking,

        [EnumMember(Value = "#DOTA_Hero_Selection_AllDraft_YouPicking_LosingGold")]
        DOTA_Hero_Selection_AllDraft_YouPicking_LosingGold,

        [EnumMember(Value = "#DOTA_Hero_Selection_AssignedHero_CaptainsMode")]
        DOTA_Hero_Selection_AssignedHero_CaptainsMode,

        [EnumMember(Value = "#DOTA_Hero_Selection_AssignedHero_Picked")]
        DOTA_Hero_Selection_AssignedHero_Picked,

        [EnumMember(Value = "#DOTA_Hero_Selection_AssignedHero_Randomed")]
        DOTA_Hero_Selection_AssignedHero_Randomed,

        [EnumMember(Value = "#DOTA_Hero_Selection_Ban")]
        DOTA_Hero_Selection_Ban,

        [EnumMember(Value = "#DOTA_Hero_Selection_Ban_Confirm")]
        DOTA_Hero_Selection_Ban_Confirm,

        [EnumMember(Value = "#DOTA_Hero_Selection_Ban_InGame")]
        DOTA_Hero_Selection_Ban_InGame,

        [EnumMember(Value = "#DOTA_Hero_Selection_Ban_Other")]
        DOTA_Hero_Selection_Ban_Other,

        [EnumMember(Value = "#DOTA_Hero_Selection_Btn_Suggest_Off")]
        DOTA_Hero_Selection_Btn_Suggest_Off,

        [EnumMember(Value = "#DOTA_Hero_Selection_Btn_Suggest_On")]
        DOTA_Hero_Selection_Btn_Suggest_On,

        [EnumMember(Value = "#DOTA_Hero_Selection_Complete")]
        DOTA_Hero_Selection_Complete,

        [EnumMember(Value = "#DOTA_Hero_Selection_Confirm")]
        DOTA_Hero_Selection_Confirm,

        [EnumMember(Value = "#DOTA_Hero_Selection_DIRE")]
        DOTA_Hero_Selection_DIRE,

        [EnumMember(Value = "#DOTA_Hero_Selection_DIRE_Phase_Ban")]
        DOTA_Hero_Selection_DIRE_Phase_Ban,

        [EnumMember(Value = "#DOTA_Hero_Selection_DIRE_Phase_Pick")]
        DOTA_Hero_Selection_DIRE_Phase_Pick,

        [EnumMember(Value = "#DOTA_Hero_Selection_Export_Succeeded")]
        DOTA_Hero_Selection_Export_Succeeded,

        [EnumMember(Value = "#DOTA_Hero_Selection_Filter_{0}")]
        DOTA_Hero_Selection_Filter_STRING,

        [EnumMember(Value = "#DOTA_Hero_Selection_Filter_PlayerLoadout")]
        DOTA_Hero_Selection_Filter_PlayerLoadout,

        [EnumMember(Value = "#DOTA_Hero_Selection_INT")]
        DOTA_Hero_Selection_INT,

        [EnumMember(Value = "#DOTA_Hero_Selection_Import_Failed")]
        DOTA_Hero_Selection_Import_Failed,

        [EnumMember(Value = "#DOTA_Hero_Selection_Import_Succeeded")]
        DOTA_Hero_Selection_Import_Succeeded,

        [EnumMember(Value = "#DOTA_Hero_Selection_Intro_Body")]
        DOTA_Hero_Selection_Intro_Body,

        [EnumMember(Value = "#DOTA_Hero_Selection_Intro_Header")]
        DOTA_Hero_Selection_Intro_Header,

        [EnumMember(Value = "#DOTA_Hero_Selection_LoadoutTitle")]
        DOTA_Hero_Selection_LoadoutTitle,

        [EnumMember(Value = "#DOTA_Hero_Selection_Msg_Desc_Base")]
        DOTA_Hero_Selection_Msg_Desc_Base,

        [EnumMember(Value = "#DOTA_Hero_Selection_Msg_Desc_Favorites")]
        DOTA_Hero_Selection_Msg_Desc_Favorites,

        [EnumMember(Value = "#DOTA_Hero_Selection_PenaltyTime_AllDraft")]
        DOTA_Hero_Selection_PenaltyTime_AllDraft,

        [EnumMember(Value = "#DOTA_Hero_Selection_PenaltyTime_AllPick")]
        DOTA_Hero_Selection_PenaltyTime_AllPick,

        [EnumMember(Value = "#DOTA_Hero_Selection_Pick")]
        DOTA_Hero_Selection_Pick,

        [EnumMember(Value = "#DOTA_Hero_Selection_Pick_Confirm")]
        DOTA_Hero_Selection_Pick_Confirm,

        [EnumMember(Value = "#DOTA_Hero_Selection_Pick_InGame")]
        DOTA_Hero_Selection_Pick_InGame,

        [EnumMember(Value = "#DOTA_Hero_Selection_Pick_Loadout")]
        DOTA_Hero_Selection_Pick_Loadout,

        [EnumMember(Value = "#DOTA_Hero_Selection_Pick_Other")]
        DOTA_Hero_Selection_Pick_Other,

        [EnumMember(Value = "#DOTA_Hero_Selection_RADIANT")]
        DOTA_Hero_Selection_RADIANT,

        [EnumMember(Value = "#DOTA_Hero_Selection_RADIANT_Phase_Ban")]
        DOTA_Hero_Selection_RADIANT_Phase_Ban,

        [EnumMember(Value = "#DOTA_Hero_Selection_RADIANT_Phase_Pick")]
        DOTA_Hero_Selection_RADIANT_Phase_Pick,

        [EnumMember(Value = "#DOTA_Hero_Selection_RandomDraft_Planning_TheyFirst")]
        DOTA_Hero_Selection_RandomDraft_Planning_TheyFirst,

        [EnumMember(Value = "#DOTA_Hero_Selection_RandomDraft_Planning_YouFirst")]
        DOTA_Hero_Selection_RandomDraft_Planning_YouFirst,

        [EnumMember(Value = "#DOTA_Hero_Selection_STR")]
        DOTA_Hero_Selection_STR,

        [EnumMember(Value = "#DOTA_Hero_Selection_Suggest_Hero_Text{0}")]
        DOTA_Hero_Selection_Suggest_Hero_Text_NUM,

        [EnumMember(Value = "#DOTA_Hero_Selection_Suggest_Hero_Title")]
        DOTA_Hero_Selection_Suggest_Hero_Title,

        [EnumMember(Value = "#DOTA_Hero_Selection_Suggest_Text")]
        DOTA_Hero_Selection_Suggest_Text,

        [EnumMember(Value = "#DOTA_Hero_Selection_Suggest_Title")]
        DOTA_Hero_Selection_Suggest_Title,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_AllHeroQuest_Text")]
        DOTA_Hero_Selection_TT_AllHeroQuest_Text,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_AllHeroQuest_Title")]
        DOTA_Hero_Selection_TT_AllHeroQuest_Title,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_DailyHeroQuest_Text")]
        DOTA_Hero_Selection_TT_DailyHeroQuest_Text,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_DailyHeroQuest_Title")]
        DOTA_Hero_Selection_TT_DailyHeroQuest_Title,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_HeroChallenge_Text")]
        DOTA_Hero_Selection_TT_HeroChallenge_Text,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_HeroChallenge_Title")]
        DOTA_Hero_Selection_TT_HeroChallenge_Title,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_HeroQuest_Text")]
        DOTA_Hero_Selection_TT_HeroQuest_Text,

        [EnumMember(Value = "#DOTA_Hero_Selection_TT_HeroQuest_Title")]
        DOTA_Hero_Selection_TT_HeroQuest_Title,

        [EnumMember(Value = "#DOTA_Hero_Tooltip_Level")]
        DOTA_Hero_Tooltip_Level,

        [EnumMember(Value = "#DOTA_Home_BattlePass_Unclaimed_Plural")]
        DOTA_Home_BattlePass_Unclaimed_Plural,

        [EnumMember(Value = "#DOTA_Home_BattlePass_Unclaimed_Singular")]
        DOTA_Home_BattlePass_Unclaimed_Singular,

        [EnumMember(Value = "#DOTA_Hud_Paused")]
        DOTA_Hud_Paused,

        [EnumMember(Value = "#DOTA_Hud_Skin_Default")]
        DOTA_Hud_Skin_Default,

        [EnumMember(Value = "#DOTA_Hud_Unpausing")]
        DOTA_Hud_Unpausing,

        [EnumMember(Value = "#DOTA_ImmediatePurchase_FailedCantDetermineSuccess")]
        DOTA_ImmediatePurchase_FailedCantDetermineSuccess,

        [EnumMember(Value = "#DOTA_ImmediatePurchase_SendFailed")]
        DOTA_ImmediatePurchase_SendFailed,

        [EnumMember(Value = "#DOTA_ImmediatePurchase_Success_Text")]
        DOTA_ImmediatePurchase_Success_Text,

        [EnumMember(Value = "#DOTA_ImmediatePurchase_Success_Title")]
        DOTA_ImmediatePurchase_Success_Title,

        [EnumMember(Value = "#DOTA_ImmediatePurchase_Timeout")]
        DOTA_ImmediatePurchase_Timeout,

        [EnumMember(Value = "#DOTA_ImmediatePurchase_UnbundleFailure")]
        DOTA_ImmediatePurchase_UnbundleFailure,

        [EnumMember(Value = "#DOTA_InGamePrediction_DropDownSelect")]
        DOTA_InGamePrediction_DropDownSelect,

        [EnumMember(Value = "#DOTA_InGamePrediction_ErrorTitle")]
        DOTA_InGamePrediction_ErrorTitle,

        [EnumMember(Value = "#DOTA_InGamePrediction_SubmittedVoteTooLate")]
        DOTA_InGamePrediction_SubmittedVoteTooLate,

        [EnumMember(Value = "#DOTA_InGamePrediction_SubmittedVoteTooLateTitle")]
        DOTA_InGamePrediction_SubmittedVoteTooLateTitle,

        [EnumMember(Value = "#DOTA_InGamePrediction_UnknownErrorInSubmission")]
        DOTA_InGamePrediction_UnknownErrorInSubmission,

        [EnumMember(Value = "#DOTA_Ingots_Header")]
        DOTA_Ingots_Header,

        [EnumMember(Value = "#DOTA_Ingots_Message")]
        DOTA_Ingots_Message,

        [EnumMember(Value = "#DOTA_InspectHeroInWorld_Error_Dead")]
        DOTA_InspectHeroInWorld_Error_Dead,

        [EnumMember(Value = "#DOTA_InspectHeroInWorld_Error_EnemySelected")]
        DOTA_InspectHeroInWorld_Error_EnemySelected,

        [EnumMember(Value = "#DOTA_InspectHeroInWorld_Error_FreeCamOnly")]
        DOTA_InspectHeroInWorld_Error_FreeCamOnly,

        [EnumMember(Value = "#DOTA_International2017_AchievementCategory_Siltbreaker_Act_1")]
        DOTA_International2017_AchievementCategory_Siltbreaker_Act_1,

        [EnumMember(Value = "#DOTA_International2017_AchievementCategory_Siltbreaker_Act_2")]
        DOTA_International2017_AchievementCategory_Siltbreaker_Act_2,

        [EnumMember(Value = "#DOTA_International2017_WeeklyGame_HowToPlayTextAvail")]
        DOTA_International2017_WeeklyGame_HowToPlayTextAvail,

        [EnumMember(Value = "#DOTA_International2017_WeeklyGame_HowToPlayTextNeeded")]
        DOTA_International2017_WeeklyGame_HowToPlayTextNeeded,

        [EnumMember(Value = "#DOTA_International2017_WeeklyGame_HowToPlayTextPending")]
        DOTA_International2017_WeeklyGame_HowToPlayTextPending,

        [EnumMember(Value = "#DOTA_InvalidPersonaName")]
        DOTA_InvalidPersonaName,

        [EnumMember(Value = "#DOTA_Inventory_BuybackCooldown_NotReady")]
        DOTA_Inventory_BuybackCooldown_NotReady,

        [EnumMember(Value = "#DOTA_Inventory_BuybackCooldown_Ready")]
        DOTA_Inventory_BuybackCooldown_Ready,

        [EnumMember(Value = "#DOTA_Inventory_BuybackCost")]
        DOTA_Inventory_BuybackCost,

        [EnumMember(Value = "#DOTA_Inventory_BuybackCost_Needed")]
        DOTA_Inventory_BuybackCost_Needed,

        [EnumMember(Value = "#DOTA_Inventory_BuybackCost_Surplus")]
        DOTA_Inventory_BuybackCost_Surplus,

        [EnumMember(Value = "#DOTA_Inventory_Courier_Purchaser")]
        DOTA_Inventory_Courier_Purchaser,

        [EnumMember(Value = "#DOTA_Inventory_DeathCost")]
        DOTA_Inventory_DeathCost,

        [EnumMember(Value = "#DOTA_Inventory_Item_Owned_By")]
        DOTA_Inventory_Item_Owned_By,

        [EnumMember(Value = "#DOTA_Inventory_ReliableGold")]
        DOTA_Inventory_ReliableGold,

        [EnumMember(Value = "#DOTA_Inventory_UnreliableGold")]
        DOTA_Inventory_UnreliableGold,

        [EnumMember(Value = "#DOTA_InviteDenied_Header")]
        DOTA_InviteDenied_Header,

        [EnumMember(Value = "#DOTA_InviteDenied_Message")]
        DOTA_InviteDenied_Message,

        [EnumMember(Value = "#DOTA_InvitedToLobbyHeader")]
        DOTA_InvitedToLobbyHeader,

        [EnumMember(Value = "#DOTA_InvitedToLobbyPending")]
        DOTA_InvitedToLobbyPending,

        [EnumMember(Value = "#DOTA_InvitedToPartyHeader")]
        DOTA_InvitedToPartyHeader,

        [EnumMember(Value = "#DOTA_InvitedToPartyPending")]
        DOTA_InvitedToPartyPending,

        [EnumMember(Value = "#DOTA_ItemChange{0}_EnterAttribute_Body")]
        DOTA_ItemChange_STRING_EnterAttribute_Body,

        [EnumMember(Value = "#DOTA_ItemChange{0}_EnterAttribute_Title")]
        DOTA_ItemChange_STRING_EnterAttribute_Title,

        [EnumMember(Value = "#DOTA_ItemChange{0}_Error")]
        DOTA_ItemChange_STRING_Error,

        [EnumMember(Value = "#DOTA_ItemChange{0}_NeedTool")]
        DOTA_ItemChange_STRING_NeedTool,

        [EnumMember(Value = "#DOTA_ItemChange{0}_NeedTool_Title")]
        DOTA_ItemChange_STRING_NeedTool_Title,

        [EnumMember(Value = "#DOTA_ItemChange{0}_Processing_Text")]
        DOTA_ItemChange_STRING_Processing_Text,

        [EnumMember(Value = "#DOTA_ItemChange{0}_Processing_Title")]
        DOTA_ItemChange_STRING_Processing_Title,

        [EnumMember(Value = "#DOTA_ItemChange{0}_Succeeded")]
        DOTA_ItemChange_STRING_Succeeded,

        [EnumMember(Value = "#DOTA_ItemChange{0}_TooLong_Body")]
        DOTA_ItemChange_STRING_TooLong_Body,

        [EnumMember(Value = "#DOTA_ItemChange{0}_TooLong_Title")]
        DOTA_ItemChange_STRING_TooLong_Title,

        [EnumMember(Value = "#DOTA_ItemName_{0}")]
        DOTA_ItemName_STRING,

        [EnumMember(Value = "#DOTA_ItemPack")]
        DOTA_ItemPack,

        [EnumMember(Value = "#DOTA_ItemPicker_Action")]
        DOTA_ItemPicker_Action,

        [EnumMember(Value = "#DOTA_ItemPicker_Title")]
        DOTA_ItemPicker_Title,

        [EnumMember(Value = "#DOTA_ItemRedemption_Action")]
        DOTA_ItemRedemption_Action,

        [EnumMember(Value = "#DOTA_ItemRedemption_ConfirmMessage")]
        DOTA_ItemRedemption_ConfirmMessage,

        [EnumMember(Value = "#DOTA_ItemRedemption_ConfirmTitle")]
        DOTA_ItemRedemption_ConfirmTitle,

        [EnumMember(Value = "#DOTA_ItemRedemption_Error")]
        DOTA_ItemRedemption_Error,

        [EnumMember(Value = "#DOTA_ItemRedemption_Title")]
        DOTA_ItemRedemption_Title,

        [EnumMember(Value = "#DOTA_ItemRemove{0}")]
        DOTA_ItemRemove_STRING,

        [EnumMember(Value = "#DOTA_ItemRemove{0}_CannotBeRemoved")]
        DOTA_ItemRemove_STRING_CannotBeRemoved,

        [EnumMember(Value = "#DOTA_ItemRemove{0}_Failed")]
        DOTA_ItemRemove_STRING_Failed,

        [EnumMember(Value = "#DOTA_ItemRemove{0}_Succeeded")]
        DOTA_ItemRemove_STRING_Succeeded,

        [EnumMember(Value = "#DOTA_ItemRemove{0}_Title")]
        DOTA_ItemRemove_STRING_Title,

        [EnumMember(Value = "#DOTA_ItemRemoveAttribute_Failed_Title")]
        DOTA_ItemRemoveAttribute_Failed_Title,

        [EnumMember(Value = "#DOTA_Item_Build_Auto_Items")]
        DOTA_Item_Build_Auto_Items,

        [EnumMember(Value = "#DOTA_Item_Build_Auto_Items_Copy")]
        DOTA_Item_Build_Auto_Items_Copy,

        [EnumMember(Value = "#DOTA_Item_Build_Other_Items")]
        DOTA_Item_Build_Other_Items,

        [EnumMember(Value = "#DOTA_Item_Build_Starting_Items")]
        DOTA_Item_Build_Starting_Items,

        [EnumMember(Value = "#DOTA_Item_Build_Starting_Items_Secondary")]
        DOTA_Item_Build_Starting_Items_Secondary,

        [EnumMember(Value = "#DOTA_Item_Build_UntitledCategory")]
        DOTA_Item_Build_UntitledCategory,

        [EnumMember(Value = "#DOTA_Item_OnMarket")]
        DOTA_Item_OnMarket,

        [EnumMember(Value = "#DOTA_Item_Ping_Cooldown")]
        DOTA_Item_Ping_Cooldown,

        [EnumMember(Value = "#DOTA_Item_Ping_InBackpack")]
        DOTA_Item_Ping_InBackpack,

        [EnumMember(Value = "#DOTA_Item_Ping_InBackpackCooldown")]
        DOTA_Item_Ping_InBackpackCooldown,

        [EnumMember(Value = "#DOTA_Item_Ping_Mana")]
        DOTA_Item_Ping_Mana,

        [EnumMember(Value = "#DOTA_Item_Ping_NameExtended")]
        DOTA_Item_Ping_NameExtended,

        [EnumMember(Value = "#DOTA_Item_Ping_Ready")]
        DOTA_Item_Ping_Ready,

        [EnumMember(Value = "#DOTA_Item_Ping_ReadyPassive")]
        DOTA_Item_Ping_ReadyPassive,

        [EnumMember(Value = "#DOTA_Item_Tooltip_Disassemble")]
        DOTA_Item_Tooltip_Disassemble,

        [EnumMember(Value = "#DOTA_Item_Tooltip_Disassemble_Time")]
        DOTA_Item_Tooltip_Disassemble_Time,

        [EnumMember(Value = "#DOTA_Item_Tooltip_Sell_Price")]
        DOTA_Item_Tooltip_Sell_Price,

        [EnumMember(Value = "#DOTA_Item_Tooltip_Sell_Price_Time")]
        DOTA_Item_Tooltip_Sell_Price_Time,

        [EnumMember(Value = "#DOTA_Item_WillPurchaseAlert")]
        DOTA_Item_WillPurchaseAlert,

        [EnumMember(Value = "#DOTA_Item_WillPurchaseAlert_GoldRemaining")]
        DOTA_Item_WillPurchaseAlert_GoldRemaining,

        [EnumMember(Value = "#DOTA_Item_WillPurchaseAlert_OutOfStock")]
        DOTA_Item_WillPurchaseAlert_OutOfStock,

        [EnumMember(Value = "#DOTA_Item_WillPurchaseAlert_Suggestion")]
        DOTA_Item_WillPurchaseAlert_Suggestion,

        [EnumMember(Value = "#DOTA_Item_WillPurchaseAlert_Suggestion_Player")]
        DOTA_Item_WillPurchaseAlert_Suggestion_Player,

        [EnumMember(Value = "#DOTA_Items_DefaultSet")]
        DOTA_Items_DefaultSet,

        [EnumMember(Value = "#DOTA_Join")]
        DOTA_Join,

        [EnumMember(Value = "#DOTA_JoinChatChannel_Kicked")]
        DOTA_JoinChatChannel_Kicked,

        [EnumMember(Value = "#DOTA_JoinChatChannel_Locked")]
        DOTA_JoinChatChannel_Locked,

        [EnumMember(Value = "#DOTA_JoinChatChannel_PrivateJoinFailed")]
        DOTA_JoinChatChannel_PrivateJoinFailed,

        [EnumMember(Value = "#DOTA_Join_Lobby")]
        DOTA_Join_Lobby,

        [EnumMember(Value = "#DOTA_Keybind_{0}")]
        DOTA_Keybind_STRING,

        [EnumMember(Value = "#DOTA_KillcamDamage_Alert")]
        DOTA_KillcamDamage_Alert,

        [EnumMember(Value = "#DOTA_KillcamDamage_Alert_Attacks")]
        DOTA_KillcamDamage_Alert_Attacks,

        [EnumMember(Value = "#DOTA_LeagueUnsetHeader")]
        DOTA_LeagueUnsetHeader,

        [EnumMember(Value = "#DOTA_LeagueUnsetMessage")]
        DOTA_LeagueUnsetMessage,

        [EnumMember(Value = "#DOTA_LeagueViewPass")]
        DOTA_LeagueViewPass,

        [EnumMember(Value = "#DOTA_LeagueViewPassNoNeed")]
        DOTA_LeagueViewPassNoNeed,

        [EnumMember(Value = "#DOTA_LearnTabName_HeroGuides")]
        DOTA_LearnTabName_HeroGuides,

        [EnumMember(Value = "#DOTA_LearnTabName_Items")]
        DOTA_LearnTabName_Items,

        [EnumMember(Value = "#DOTA_LearnTabName_Tutorials")]
        DOTA_LearnTabName_Tutorials,

        [EnumMember(Value = "#DOTA_Limited_User_Chat")]
        DOTA_Limited_User_Chat,

        [EnumMember(Value = "#DOTA_LoadingPlayerFailed")]
        DOTA_LoadingPlayerFailed,

        [EnumMember(Value = "#DOTA_LoadingPlayerFullyConnected")]
        DOTA_LoadingPlayerFullyConnected,

        [EnumMember(Value = "#DOTA_LoadingPlayerLoading")]
        DOTA_LoadingPlayerLoading,

        [EnumMember(Value = "#DOTA_LoadingPlayerUnknown")]
        DOTA_LoadingPlayerUnknown,

        [EnumMember(Value = "#DOTA_LobbyAllowSpectatingHeader")]
        DOTA_LobbyAllowSpectatingHeader,

        [EnumMember(Value = "#DOTA_LobbyAllowSpectatingMessage")]
        DOTA_LobbyAllowSpectatingMessage,

        [EnumMember(Value = "#DOTA_LobbyFull_Header")]
        DOTA_LobbyFull_Header,

        [EnumMember(Value = "#DOTA_LobbyFull_Message")]
        DOTA_LobbyFull_Message,

        [EnumMember(Value = "#DOTA_LobbyInvite_Description")]
        DOTA_LobbyInvite_Description,

        [EnumMember(Value = "#DOTA_LobbyInvite_Title")]
        DOTA_LobbyInvite_Title,

        [EnumMember(Value = "#DOTA_LobbyKicked_Header")]
        DOTA_LobbyKicked_Header,

        [EnumMember(Value = "#DOTA_LobbyKicked_Message")]
        DOTA_LobbyKicked_Message,

        [EnumMember(Value = "#DOTA_LobbySeriesTeamMismatchHeader")]
        DOTA_LobbySeriesTeamMismatchHeader,

        [EnumMember(Value = "#DOTA_LobbySeriesTeamMismatchMessage")]
        DOTA_LobbySeriesTeamMismatchMessage,

        [EnumMember(Value = "#DOTA_LobbyStartAutomaticSelectionPriority")]
        DOTA_LobbyStartAutomaticSelectionPriority,

        [EnumMember(Value = "#DOTA_LobbyStartGame")]
        DOTA_LobbyStartGame,

        [EnumMember(Value = "#DOTA_Lobby_BotDifficulty_easy")]
        DOTA_Lobby_BotDifficulty_easy,

        [EnumMember(Value = "#DOTA_Lobby_BotDifficulty_hard")]
        DOTA_Lobby_BotDifficulty_hard,

        [EnumMember(Value = "#DOTA_Lobby_BotDifficulty_medium")]
        DOTA_Lobby_BotDifficulty_medium,

        [EnumMember(Value = "#DOTA_Lobby_BotDifficulty_passive")]
        DOTA_Lobby_BotDifficulty_passive,

        [EnumMember(Value = "#DOTA_Lobby_BotDifficulty_unfair")]
        DOTA_Lobby_BotDifficulty_unfair,

        [EnumMember(Value = "#DOTA_Lobby_Broadcast_Title")]
        DOTA_Lobby_Broadcast_Title,

        [EnumMember(Value = "#DOTA_Lobby_Broadcaster_Channel_Other_Language")]
        DOTA_Lobby_Broadcaster_Channel_Other_Language,

        [EnumMember(Value = "#DOTA_Lobby_Destroy_Header")]
        DOTA_Lobby_Destroy_Header,

        [EnumMember(Value = "#DOTA_Lobby_Destroy_Text")]
        DOTA_Lobby_Destroy_Text,

        [EnumMember(Value = "#DOTA_Lobby_Failed_Pings")]
        DOTA_Lobby_Failed_Pings,

        [EnumMember(Value = "#DOTA_Lobby_MVP_Awarded_Message")]
        DOTA_Lobby_MVP_Awarded_Message,

        [EnumMember(Value = "#DOTA_Lobby_MVP_Awarded_Title")]
        DOTA_Lobby_MVP_Awarded_Title,

        [EnumMember(Value = "#DOTA_Lobby_No_Players_Desc")]
        DOTA_Lobby_No_Players_Desc,

        [EnumMember(Value = "#DOTA_Lobby_No_Players_Title")]
        DOTA_Lobby_No_Players_Title,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bot_Difficulty_Easy")]
        DOTA_Lobby_Settings_Bot_Difficulty_Easy,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bot_Difficulty_Hard")]
        DOTA_Lobby_Settings_Bot_Difficulty_Hard,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bot_Difficulty_Medium")]
        DOTA_Lobby_Settings_Bot_Difficulty_Medium,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bot_Difficulty_Passive")]
        DOTA_Lobby_Settings_Bot_Difficulty_Passive,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bot_Difficulty_Unfair")]
        DOTA_Lobby_Settings_Bot_Difficulty_Unfair,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bots_Browse")]
        DOTA_Lobby_Settings_Bots_Browse,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Bots_Default")]
        DOTA_Lobby_Settings_Bots_Default,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Tournament_None")]
        DOTA_Lobby_Settings_Tournament_None,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Visibility_Friends")]
        DOTA_Lobby_Settings_Visibility_Friends,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Visibility_Public")]
        DOTA_Lobby_Settings_Visibility_Public,

        [EnumMember(Value = "#DOTA_Lobby_Settings_Visibility_Unlisted")]
        DOTA_Lobby_Settings_Visibility_Unlisted,

        [EnumMember(Value = "#DOTA_LocalReconnect_Body")]
        DOTA_LocalReconnect_Body,

        [EnumMember(Value = "#DOTA_LocalReconnect_Title")]
        DOTA_LocalReconnect_Title,

        [EnumMember(Value = "#DOTA_Location_Name_Ancient")]
        DOTA_Location_Name_Ancient,

        [EnumMember(Value = "#DOTA_Location_Name_BadGuys")]
        DOTA_Location_Name_BadGuys,

        [EnumMember(Value = "#DOTA_Location_Name_Base")]
        DOTA_Location_Name_Base,

        [EnumMember(Value = "#DOTA_Location_Name_BotLane")]
        DOTA_Location_Name_BotLane,

        [EnumMember(Value = "#DOTA_Location_Name_Bot_BountyRune")]
        DOTA_Location_Name_Bot_BountyRune,

        [EnumMember(Value = "#DOTA_Location_Name_Bot_Jungle")]
        DOTA_Location_Name_Bot_Jungle,

        [EnumMember(Value = "#DOTA_Location_Name_Bot_Rune")]
        DOTA_Location_Name_Bot_Rune,

        [EnumMember(Value = "#DOTA_Location_Name_GoodGuys")]
        DOTA_Location_Name_GoodGuys,

        [EnumMember(Value = "#DOTA_Location_Name_MidLane")]
        DOTA_Location_Name_MidLane,

        [EnumMember(Value = "#DOTA_Location_Name_NotFound")]
        DOTA_Location_Name_NotFound,

        [EnumMember(Value = "#DOTA_Location_Name_River")]
        DOTA_Location_Name_River,

        [EnumMember(Value = "#DOTA_Location_Name_Roshan")]
        DOTA_Location_Name_Roshan,

        [EnumMember(Value = "#DOTA_Location_Name_SecretShop")]
        DOTA_Location_Name_SecretShop,

        [EnumMember(Value = "#DOTA_Location_Name_SideShop")]
        DOTA_Location_Name_SideShop,

        [EnumMember(Value = "#DOTA_Location_Name_TopLane")]
        DOTA_Location_Name_TopLane,

        [EnumMember(Value = "#DOTA_Location_Name_Top_BountyRune")]
        DOTA_Location_Name_Top_BountyRune,

        [EnumMember(Value = "#DOTA_Location_Name_Top_Jungle")]
        DOTA_Location_Name_Top_Jungle,

        [EnumMember(Value = "#DOTA_Location_Name_Top_Rune")]
        DOTA_Location_Name_Top_Rune,

        [EnumMember(Value = "#DOTA_Location_Name_Tower1")]
        DOTA_Location_Name_Tower1,

        [EnumMember(Value = "#DOTA_Location_Name_Tower2")]
        DOTA_Location_Name_Tower2,

        [EnumMember(Value = "#DOTA_Location_Name_Tower3")]
        DOTA_Location_Name_Tower3,

        [EnumMember(Value = "#DOTA_Location_Name_Tower4")]
        DOTA_Location_Name_Tower4,

        [EnumMember(Value = "#DOTA_Losses_Plural")]
        DOTA_Losses_Plural,

        [EnumMember(Value = "#DOTA_Losses_Singular")]
        DOTA_Losses_Singular,

        [EnumMember(Value = "#DOTA_Low_Badge_Level_Chat")]
        DOTA_Low_Badge_Level_Chat,

        [EnumMember(Value = "#DOTA_Low_Wins_Chat")]
        DOTA_Low_Wins_Chat,

        [EnumMember(Value = "#DOTA_MatchDetailsGame_Leaver")]
        DOTA_MatchDetailsGame_Leaver,

        [EnumMember(Value = "#DOTA_MatchDetailsGame_NeverStarted")]
        DOTA_MatchDetailsGame_NeverStarted,

        [EnumMember(Value = "#DOTA_MatchDetailsGame_PoorNetwork")]
        DOTA_MatchDetailsGame_PoorNetwork,

        [EnumMember(Value = "#DOTA_MatchDetailsGame_ServerCrash")]
        DOTA_MatchDetailsGame_ServerCrash,

        [EnumMember(Value = "#DOTA_MatchNoRating")]
        DOTA_MatchNoRating,

        [EnumMember(Value = "#DOTA_MatchRatingDislike")]
        DOTA_MatchRatingDislike,

        [EnumMember(Value = "#DOTA_MatchRatingDislikes")]
        DOTA_MatchRatingDislikes,

        [EnumMember(Value = "#DOTA_MatchRatingLike")]
        DOTA_MatchRatingLike,

        [EnumMember(Value = "#DOTA_MatchRatingLikes")]
        DOTA_MatchRatingLikes,

        [EnumMember(Value = "#DOTA_MatchRatingText")]
        DOTA_MatchRatingText,

        [EnumMember(Value = "#DOTA_MatchmakingNeedsInitialSkillForParty_Description")]
        DOTA_MatchmakingNeedsInitialSkillForParty_Description,

        [EnumMember(Value = "#DOTA_MatchmakingNeedsInitialSkill_Description")]
        DOTA_MatchmakingNeedsInitialSkill_Description,

        [EnumMember(Value = "#DOTA_MatchmakingNeedsInitialSkill_Header")]
        DOTA_MatchmakingNeedsInitialSkill_Header,

        [EnumMember(Value = "#DOTA_Matchmaking_Header")]
        DOTA_Matchmaking_Header,

        [EnumMember(Value = "#DOTA_Matchmaking_Message")]
        DOTA_Matchmaking_Message,

        [EnumMember(Value = "#DOTA_Matchmaking_NoRegion_Error")]
        DOTA_Matchmaking_NoRegion_Error,

        [EnumMember(Value = "#DOTA_Matchmaking_No_OverrideVPK")]
        DOTA_Matchmaking_No_OverrideVPK,

        [EnumMember(Value = "#DOTA_Matchmaking_Region_Offline")]
        DOTA_Matchmaking_Region_Offline,

        [EnumMember(Value = "#DOTA_Meepo_Error_CanOnlyTargetMeepo")]
        DOTA_Meepo_Error_CanOnlyTargetMeepo,

        [EnumMember(Value = "#DOTA_MegaKill_Default")]
        DOTA_MegaKill_Default,

        [EnumMember(Value = "#DOTA_MillionSuffix")]
        DOTA_MillionSuffix,

        [EnumMember(Value = "#DOTA_MiniKillCam_KillerInfo")]
        DOTA_MiniKillCam_KillerInfo,

        [EnumMember(Value = "#DOTA_MiniKillcam_KilledByBad")]
        DOTA_MiniKillcam_KilledByBad,

        [EnumMember(Value = "#DOTA_MiniKillcam_KilledByCustom")]
        DOTA_MiniKillcam_KilledByCustom,

        [EnumMember(Value = "#DOTA_MiniKillcam_KilledByGood")]
        DOTA_MiniKillcam_KilledByGood,

        [EnumMember(Value = "#DOTA_MiniKillcam_KilledByNeutral")]
        DOTA_MiniKillcam_KilledByNeutral,

        [EnumMember(Value = "#DOTA_MiniKillcam_KilledByRoshan")]
        DOTA_MiniKillcam_KilledByRoshan,

        [EnumMember(Value = "#DOTA_Modifier_Alert")]
        DOTA_Modifier_Alert,

        [EnumMember(Value = "#DOTA_Modifier_Alert_BH_Track")]
        DOTA_Modifier_Alert_BH_Track,

        [EnumMember(Value = "#DOTA_Modifier_Alert_Enemy")]
        DOTA_Modifier_Alert_Enemy,

        [EnumMember(Value = "#DOTA_MultipleRewards")]
        DOTA_MultipleRewards,

        [EnumMember(Value = "#DOTA_New")]
        DOTA_New,

        [EnumMember(Value = "#DOTA_NewPlayerHeroPick2_Body")]
        DOTA_NewPlayerHeroPick2_Body,

        [EnumMember(Value = "#DOTA_NewPlayerHeroPick2_Title")]
        DOTA_NewPlayerHeroPick2_Title,

        [EnumMember(Value = "#DOTA_NewPlayerHeroPick_Body")]
        DOTA_NewPlayerHeroPick_Body,

        [EnumMember(Value = "#DOTA_NewPlayerHeroPick_Title")]
        DOTA_NewPlayerHeroPick_Title,

        [EnumMember(Value = "#DOTA_NoHero")]
        DOTA_NoHero,

        [EnumMember(Value = "#DOTA_NoSlot")]
        DOTA_NoSlot,

        [EnumMember(Value = "#DOTA_None")]
        DOTA_None,

        [EnumMember(Value = "#DOTA_NotEquippedItem_Label")]
        DOTA_NotEquippedItem_Label,

        [EnumMember(Value = "#DOTA_NotShuffledItem_Label")]
        DOTA_NotShuffledItem_Label,

        [EnumMember(Value = "#DOTA_Notification_Paused_Message")]
        DOTA_Notification_Paused_Message,

        [EnumMember(Value = "#DOTA_Notification_Paused_Title")]
        DOTA_Notification_Paused_Title,

        [EnumMember(Value = "#DOTA_Notification_Unpaused_Message")]
        DOTA_Notification_Unpaused_Message,

        [EnumMember(Value = "#DOTA_Notification_Unpaused_Title")]
        DOTA_Notification_Unpaused_Title,

        [EnumMember(Value = "#DOTA_Notifications_ViewAll")]
        DOTA_Notifications_ViewAll,

        [EnumMember(Value = "#DOTA_Notifications_ViewRecent")]
        DOTA_Notifications_ViewRecent,

        [EnumMember(Value = "#DOTA_Ok")]
        DOTA_Ok,

        [EnumMember(Value = "#DOTA_OpenTreasureTooltip_DupeDescription")]
        DOTA_OpenTreasureTooltip_DupeDescription,

        [EnumMember(Value = "#DOTA_OpenTreasureTooltip_ExtraItems")]
        DOTA_OpenTreasureTooltip_ExtraItems,

        [EnumMember(Value = "#DOTA_OpenTreasureTooltip_ItemName")]
        DOTA_OpenTreasureTooltip_ItemName,

        [EnumMember(Value = "#DOTA_OpenTreasureTooltip_NoDupeDescription")]
        DOTA_OpenTreasureTooltip_NoDupeDescription,

        [EnumMember(Value = "#DOTA_OpenTreasureTooltip_UnitName")]
        DOTA_OpenTreasureTooltip_UnitName,

        [EnumMember(Value = "#DOTA_OtherType")]
        DOTA_OtherType,

        [EnumMember(Value = "#DOTA_OverlayLocator_PlayerHero")]
        DOTA_OverlayLocator_PlayerHero,

        [EnumMember(Value = "#DOTA_PartyFull_Header")]
        DOTA_PartyFull_Header,

        [EnumMember(Value = "#DOTA_PartyFull_Message")]
        DOTA_PartyFull_Message,

        [EnumMember(Value = "#DOTA_PartyInvite_Description")]
        DOTA_PartyInvite_Description,

        [EnumMember(Value = "#DOTA_PartyInvite_LowPriority_Description")]
        DOTA_PartyInvite_LowPriority_Description,

        [EnumMember(Value = "#DOTA_PartyInvite_LowPriority_Title")]
        DOTA_PartyInvite_LowPriority_Title,

        [EnumMember(Value = "#DOTA_PartyInvite_Title")]
        DOTA_PartyInvite_Title,

        [EnumMember(Value = "#DOTA_PartyJoinRequest_Description")]
        DOTA_PartyJoinRequest_Description,

        [EnumMember(Value = "#DOTA_PartyJoinRequest_Title")]
        DOTA_PartyJoinRequest_Title,

        [EnumMember(Value = "#DOTA_PartyJoinedLobby_Custom")]
        DOTA_PartyJoinedLobby_Custom,

        [EnumMember(Value = "#DOTA_PartyJoinedLobby_Generic")]
        DOTA_PartyJoinedLobby_Generic,

        [EnumMember(Value = "#DOTA_PartyJoinedLobby_Title")]
        DOTA_PartyJoinedLobby_Title,

        [EnumMember(Value = "#DOTA_PartyKicked_Header")]
        DOTA_PartyKicked_Header,

        [EnumMember(Value = "#DOTA_PartyKicked_Message")]
        DOTA_PartyKicked_Message,

        [EnumMember(Value = "#DOTA_PartyLeader")]
        DOTA_PartyLeader,

        [EnumMember(Value = "#DOTA_PartyMergeRequestLowPriWarning")]
        DOTA_PartyMergeRequestLowPriWarning,

        [EnumMember(Value = "#DOTA_PartyMergeRequest_Description")]
        DOTA_PartyMergeRequest_Description,

        [EnumMember(Value = "#DOTA_PartyMergeRequest_Title")]
        DOTA_PartyMergeRequest_Title,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_ENGINE_MISMATCH")]
        DOTA_PartyMerge_Error_ENGINE_MISMATCH,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_NOT_LEADER")]
        DOTA_PartyMerge_Error_NOT_LEADER,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_NO_SUCH_GROUP")]
        DOTA_PartyMerge_Error_NO_SUCH_GROUP,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_OTHER_GROUP_NOT_OPEN")]
        DOTA_PartyMerge_Error_OTHER_GROUP_NOT_OPEN,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_TOO_MANY_COACHES")]
        DOTA_PartyMerge_Error_TOO_MANY_COACHES,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_TOO_MANY_PLAYERS")]
        DOTA_PartyMerge_Error_TOO_MANY_PLAYERS,

        [EnumMember(Value = "#DOTA_PartyMerge_Error_Title")]
        DOTA_PartyMerge_Error_Title,

        [EnumMember(Value = "#DOTA_PartyStartedFindingEventMatch_Notification_Header")]
        DOTA_PartyStartedFindingEventMatch_Notification_Header,

        [EnumMember(Value = "#DOTA_PartyStartedFindingEventMatch_Notification_Message")]
        DOTA_PartyStartedFindingEventMatch_Notification_Message,

        [EnumMember(Value = "#DOTA_PartyStartedFindingEventMatch_Notification_SkipIntro")]
        DOTA_PartyStartedFindingEventMatch_Notification_SkipIntro,

        [EnumMember(Value = "#DOTA_PartyStartedFindingEventMatch_Notification_ViewIntro")]
        DOTA_PartyStartedFindingEventMatch_Notification_ViewIntro,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_LeaderNotOpen")]
        DOTA_PartyVisTooltip_LeaderNotOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_LeaderOpen")]
        DOTA_PartyVisTooltip_LeaderOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_LeaderOpenManualAccept")]
        DOTA_PartyVisTooltip_LeaderOpenManualAccept,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_NonLeaderNotOpen")]
        DOTA_PartyVisTooltip_NonLeaderNotOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_NonLeaderOpen")]
        DOTA_PartyVisTooltip_NonLeaderOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_NotOpenHidden")]
        DOTA_PartyVisTooltip_NotOpenHidden,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_SoloNotOpen")]
        DOTA_PartyVisTooltip_SoloNotOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_SoloOpen")]
        DOTA_PartyVisTooltip_SoloOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_SoloTitleOpen")]
        DOTA_PartyVisTooltip_SoloTitleOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_TitleNotOpen")]
        DOTA_PartyVisTooltip_TitleNotOpen,

        [EnumMember(Value = "#DOTA_PartyVisTooltip_TitleOpen")]
        DOTA_PartyVisTooltip_TitleOpen,

        [EnumMember(Value = "#DOTA_Party_Add_Friends")]
        DOTA_Party_Add_Friends,

        [EnumMember(Value = "#DOTA_PendingInvitesTooltip_1")]
        DOTA_PendingInvitesTooltip_1,

        [EnumMember(Value = "#DOTA_PendingInvitesTooltip_Leader")]
        DOTA_PendingInvitesTooltip_Leader,

        [EnumMember(Value = "#DOTA_PendingInvitesTooltip_N")]
        DOTA_PendingInvitesTooltip_N,

        [EnumMember(Value = "#DOTA_PendingInvitesTooltip_NonLeader")]
        DOTA_PendingInvitesTooltip_NonLeader,

        [EnumMember(Value = "#DOTA_PendingInvitesTooltip_None")]
        DOTA_PendingInvitesTooltip_None,

        [EnumMember(Value = "#DOTA_PhantomAssassin_Gravestone_Epitaph_%i")]
        DOTA_PhantomAssassin_Gravestone_Epitaph_NUM,

        [EnumMember(Value = "#DOTA_PlayButtonReturnToWeekendTourney")]
        DOTA_PlayButtonReturnToWeekendTourney,

        [EnumMember(Value = "#DOTA_PlayTourneyFindFirstMatch")]
        DOTA_PlayTourneyFindFirstMatch,

        [EnumMember(Value = "#DOTA_PlayTourneyFindNextMatch")]
        DOTA_PlayTourneyFindNextMatch,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_NoTournamentScheduled")]
        DOTA_PlayTourneyStatus_NoTournamentScheduled,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_Offseason")]
        DOTA_PlayTourneyStatus_Offseason,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_Open")]
        DOTA_PlayTourneyStatus_Open,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_OpenRateLimited")]
        DOTA_PlayTourneyStatus_OpenRateLimited,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_StartingLater")]
        DOTA_PlayTourneyStatus_StartingLater,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_StartingSoon")]
        DOTA_PlayTourneyStatus_StartingSoon,

        [EnumMember(Value = "#DOTA_PlayTourneyStatus_TooLate")]
        DOTA_PlayTourneyStatus_TooLate,

        [EnumMember(Value = "#DOTA_PlayerCardBonusStatName{0}")]
        DOTA_PlayerCardBonusStatName_NUM,

        [EnumMember(Value = "#DOTA_PlayerCardGold")]
        DOTA_PlayerCardGold,

        [EnumMember(Value = "#DOTA_PlayerCardSilver")]
        DOTA_PlayerCardSilver,

        [EnumMember(Value = "#DOTA_PlayerCard_RecycleDisabled")]
        DOTA_PlayerCard_RecycleDisabled,

        [EnumMember(Value = "#DOTA_PlayerCardsBuyPackResultErrorInsufficientDust")]
        DOTA_PlayerCardsBuyPackResultErrorInsufficientDust,

        [EnumMember(Value = "#DOTA_PlayerCardsBuyPackResultErrorNotDust")]
        DOTA_PlayerCardsBuyPackResultErrorNotDust,

        [EnumMember(Value = "#DOTA_PlayerCardsBuyPackResultErrorPackCreate")]
        DOTA_PlayerCardsBuyPackResultErrorPackCreate,

        [EnumMember(Value = "#DOTA_PlayerCardsBuyPackResultErrorTitle")]
        DOTA_PlayerCardsBuyPackResultErrorTitle,

        [EnumMember(Value = "#DOTA_PlayerCardsBuyPackResultErrorUnknown")]
        DOTA_PlayerCardsBuyPackResultErrorUnknown,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleConfirmBody")]
        DOTA_PlayerCardsRecycleConfirmBody,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleConfirmBodyInUse")]
        DOTA_PlayerCardsRecycleConfirmBodyInUse,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleConfirmBodyInUseMultiple")]
        DOTA_PlayerCardsRecycleConfirmBodyInUseMultiple,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleConfirmBodyMultiple")]
        DOTA_PlayerCardsRecycleConfirmBodyMultiple,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleConfirmTitle")]
        DOTA_PlayerCardsRecycleConfirmTitle,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleResultErrorDustCreate")]
        DOTA_PlayerCardsRecycleResultErrorDustCreate,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleResultErrorFindCard")]
        DOTA_PlayerCardsRecycleResultErrorFindCard,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleResultErrorLocked")]
        DOTA_PlayerCardsRecycleResultErrorLocked,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleResultErrorNotCard")]
        DOTA_PlayerCardsRecycleResultErrorNotCard,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleResultErrorTitle")]
        DOTA_PlayerCardsRecycleResultErrorTitle,

        [EnumMember(Value = "#DOTA_PlayerCardsRecycleResultErrorUnknown")]
        DOTA_PlayerCardsRecycleResultErrorUnknown,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_AcceptFriendRequest")]
        DOTA_PlayerContextMenu_AcceptFriendRequest,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_AddFriend")]
        DOTA_PlayerContextMenu_AddFriend,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_BattleCup_Bracket")]
        DOTA_PlayerContextMenu_BattleCup_Bracket,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_ChatWithPlayer")]
        DOTA_PlayerContextMenu_ChatWithPlayer,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_CoachParty")]
        DOTA_PlayerContextMenu_CoachParty,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_Ignore")]
        DOTA_PlayerContextMenu_Ignore,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_IgnoreFriendRequest")]
        DOTA_PlayerContextMenu_IgnoreFriendRequest,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_IgnoreSpam")]
        DOTA_PlayerContextMenu_IgnoreSpam,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_InviteToLobby")]
        DOTA_PlayerContextMenu_InviteToLobby,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_InviteToParty")]
        DOTA_PlayerContextMenu_InviteToParty,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_KickFromLobby")]
        DOTA_PlayerContextMenu_KickFromLobby,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_KickFromLobbyTeam")]
        DOTA_PlayerContextMenu_KickFromLobbyTeam,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_KickFromParty")]
        DOTA_PlayerContextMenu_KickFromParty,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_LeaveParty")]
        DOTA_PlayerContextMenu_LeaveParty,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_MakeCaptain")]
        DOTA_PlayerContextMenu_MakeCaptain,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_SetPartyLeader")]
        DOTA_PlayerContextMenu_SetPartyLeader,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_Spectate")]
        DOTA_PlayerContextMenu_Spectate,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_StopCoachingParty")]
        DOTA_PlayerContextMenu_StopCoachingParty,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_SuggestInviteToLobby")]
        DOTA_PlayerContextMenu_SuggestInviteToLobby,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_SuggestInviteToParty")]
        DOTA_PlayerContextMenu_SuggestInviteToParty,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_Trade")]
        DOTA_PlayerContextMenu_Trade,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_Unignore")]
        DOTA_PlayerContextMenu_Unignore,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_ViewProfile")]
        DOTA_PlayerContextMenu_ViewProfile,

        [EnumMember(Value = "#DOTA_PlayerContextMenu_ViewTournament")]
        DOTA_PlayerContextMenu_ViewTournament,

        [EnumMember(Value = "#DOTA_PleaseWait")]
        DOTA_PleaseWait,

        [EnumMember(Value = "#DOTA_PointsGained")]
        DOTA_PointsGained,

        [EnumMember(Value = "#DOTA_PostGame_Draft")]
        DOTA_PostGame_Draft,

        [EnumMember(Value = "#DOTA_PostGame_Graphs")]
        DOTA_PostGame_Graphs,

        [EnumMember(Value = "#DOTA_PostGame_Overview")]
        DOTA_PostGame_Overview,

        [EnumMember(Value = "#DOTA_PostGame_Scoreboard")]
        DOTA_PostGame_Scoreboard,

        [EnumMember(Value = "#DOTA_Post_Match_Survey_Header_{0}")]
        DOTA_Post_Match_Survey_Header_NUM,

        [EnumMember(Value = "#DOTA_Post_Match_Survey_Question_{0}")]
        DOTA_Post_Match_Survey_Question_NUM,

        [EnumMember(Value = "#DOTA_PrivateChatChannel")]
        DOTA_PrivateChatChannel,

        [EnumMember(Value = "#DOTA_ProGear_Econ_Tooltip")]
        DOTA_ProGear_Econ_Tooltip,

        [EnumMember(Value = "#DOTA_ProfileCard_StatTitle_Commends")]
        DOTA_ProfileCard_StatTitle_Commends,

        [EnumMember(Value = "#DOTA_ProfileCard_StatTitle_FirstMatchDate")]
        DOTA_ProfileCard_StatTitle_FirstMatchDate,

        [EnumMember(Value = "#DOTA_ProfileCard_StatTitle_GamesPlayed")]
        DOTA_ProfileCard_StatTitle_GamesPlayed,

        [EnumMember(Value = "#DOTA_ProfileCard_StatTitle_PartyRank")]
        DOTA_ProfileCard_StatTitle_PartyRank,

        [EnumMember(Value = "#DOTA_ProfileCard_StatTitle_SoloRank")]
        DOTA_ProfileCard_StatTitle_SoloRank,

        [EnumMember(Value = "#DOTA_ProfileCard_StatTitle_Wins")]
        DOTA_ProfileCard_StatTitle_Wins,

        [EnumMember(Value = "#DOTA_ProfileCard_StatValue_Date")]
        DOTA_ProfileCard_StatValue_Date,

        [EnumMember(Value = "#DOTA_ProfileCard_StatValue_Int")]
        DOTA_ProfileCard_StatValue_Int,

        [EnumMember(Value = "#DOTA_ProfileCard_StatValue_String")]
        DOTA_ProfileCard_StatValue_String,

        [EnumMember(Value = "#DOTA_Profile_{0}")]
        DOTA_Profile_STRING,

        [EnumMember(Value = "#DOTA_Profile_{0}LocalValue")]
        DOTA_Profile_STRING_LocalValue,

        [EnumMember(Value = "#DOTA_Profile_{0}Value")]
        DOTA_Profile_STRING_Value,

        [EnumMember(Value = "#DOTA_Profile_HeroStatsPage")]
        DOTA_Profile_HeroStatsPage,

        [EnumMember(Value = "#DOTA_Profile_LeaguePassesPage")]
        DOTA_Profile_LeaguePassesPage,

        [EnumMember(Value = "#DOTA_Profile_ProfilePage")]
        DOTA_Profile_ProfilePage,

        [EnumMember(Value = "#DOTA_Profile_TrophyPage")]
        DOTA_Profile_TrophyPage,

        [EnumMember(Value = "#DOTA_PublicTextBanned_Reports")]
        DOTA_PublicTextBanned_Reports,

        [EnumMember(Value = "#DOTA_PublicTextBanned_Reports_Avoid_Overflow")]
        DOTA_PublicTextBanned_Reports_Avoid_Overflow,

        [EnumMember(Value = "#DOTA_PurchaseAndEquip")]
        DOTA_PurchaseAndEquip,

        [EnumMember(Value = "#DOTA_PurchaseBattlePassLevels")]
        DOTA_PurchaseBattlePassLevels,

        [EnumMember(Value = "#DOTA_PurchaseError_Ok")]
        DOTA_PurchaseError_Ok,

        [EnumMember(Value = "#DOTA_PurchaseError_OpenBrowser")]
        DOTA_PurchaseError_OpenBrowser,

        [EnumMember(Value = "#DOTA_PurchaseError_SteamOverlayDisabled")]
        DOTA_PurchaseError_SteamOverlayDisabled,

        [EnumMember(Value = "#DOTA_PurchaseError_Title")]
        DOTA_PurchaseError_Title,

        [EnumMember(Value = "#DOTA_PurchaseEventRewardLevel")]
        DOTA_PurchaseEventRewardLevel,

        [EnumMember(Value = "#DOTA_PurchaseFinalizing_Text")]
        DOTA_PurchaseFinalizing_Text,

        [EnumMember(Value = "#DOTA_PurchaseInProgress_Text")]
        DOTA_PurchaseInProgress_Text,

        [EnumMember(Value = "#DOTA_PurchaseInProgress_Title")]
        DOTA_PurchaseInProgress_Title,

        [EnumMember(Value = "#DOTA_PurchaseRequired_Notification_Header")]
        DOTA_PurchaseRequired_Notification_Header,

        [EnumMember(Value = "#DOTA_PurchaseRequired_Notification_Message")]
        DOTA_PurchaseRequired_Notification_Message,

        [EnumMember(Value = "#DOTA_Purchase_Popup_Dust_Button")]
        DOTA_Purchase_Popup_Dust_Button,

        [EnumMember(Value = "#DOTA_Purchase_Popup_Marketplace_Button")]
        DOTA_Purchase_Popup_Marketplace_Button,

        [EnumMember(Value = "#DOTA_Purchase_Popup_Purchase_Button")]
        DOTA_Purchase_Popup_Purchase_Button,

        [EnumMember(Value = "#DOTA_QuickBuyAlert")]
        DOTA_QuickBuyAlert,

        [EnumMember(Value = "#DOTA_QuickBuyAlert_Enough")]
        DOTA_QuickBuyAlert_Enough,

        [EnumMember(Value = "#DOTA_RP_ACCOUNT_DISABLED")]
        DOTA_RP_ACCOUNT_DISABLED,

        [EnumMember(Value = "#DOTA_RP_AWAY")]
        DOTA_RP_AWAY,

        [EnumMember(Value = "#DOTA_RP_BOTPRACTICE")]
        DOTA_RP_BOTPRACTICE,

        [EnumMember(Value = "#DOTA_RP_BUSY")]
        DOTA_RP_BUSY,

        [EnumMember(Value = "#DOTA_RP_CASTING")]
        DOTA_RP_CASTING,

        [EnumMember(Value = "#DOTA_RP_COOPBOT")]
        DOTA_RP_COOPBOT,

        [EnumMember(Value = "#DOTA_RP_CUSTOM_GAME_SETUP")]
        DOTA_RP_CUSTOM_GAME_SETUP,

        [EnumMember(Value = "#DOTA_RP_DISCONNECT")]
        DOTA_RP_DISCONNECT,

        [EnumMember(Value = "#DOTA_RP_FINDING_EVENT_MATCH")]
        DOTA_RP_FINDING_EVENT_MATCH,

        [EnumMember(Value = "#DOTA_RP_FINDING_MATCH")]
        DOTA_RP_FINDING_MATCH,

        [EnumMember(Value = "#DOTA_RP_GAME_IN_PROGRESS")]
        DOTA_RP_GAME_IN_PROGRESS,

        [EnumMember(Value = "#DOTA_RP_GAME_IN_PROGRESS_CUSTOM")]
        DOTA_RP_GAME_IN_PROGRESS_CUSTOM,

        [EnumMember(Value = "#DOTA_RP_GAME_IN_PROGRESS_CUSTOM_UNNAMED")]
        DOTA_RP_GAME_IN_PROGRESS_CUSTOM_UNNAMED,

        [EnumMember(Value = "#DOTA_RP_HERO_SELECTION")]
        DOTA_RP_HERO_SELECTION,

        [EnumMember(Value = "#DOTA_RP_IDLE")]
        DOTA_RP_IDLE,

        [EnumMember(Value = "#DOTA_RP_INIT")]
        DOTA_RP_INIT,

        [EnumMember(Value = "#DOTA_RP_LEAGUE_MATCH")]
        DOTA_RP_LEAGUE_MATCH,

        [EnumMember(Value = "#DOTA_RP_LEAGUE_MATCH_PLAYING_AS")]
        DOTA_RP_LEAGUE_MATCH_PLAYING_AS,

        [EnumMember(Value = "#DOTA_RP_LOBBY_CUSTOM")]
        DOTA_RP_LOBBY_CUSTOM,

        [EnumMember(Value = "#DOTA_RP_LOBBY_CUSTOM_UNNAMED")]
        DOTA_RP_LOBBY_CUSTOM_UNNAMED,

        [EnumMember(Value = "#DOTA_RP_LOOKING_TO_PLAY")]
        DOTA_RP_LOOKING_TO_PLAY,

        [EnumMember(Value = "#DOTA_RP_LOOKING_TO_TRADE")]
        DOTA_RP_LOOKING_TO_TRADE,

        [EnumMember(Value = "#DOTA_RP_NOT_FRIEND")]
        DOTA_RP_NOT_FRIEND,

        [EnumMember(Value = "#DOTA_RP_ONLINE")]
        DOTA_RP_ONLINE,

        [EnumMember(Value = "#DOTA_RP_OPEN_PARTY")]
        DOTA_RP_OPEN_PARTY,

        [EnumMember(Value = "#DOTA_RP_OPEN_SOLO")]
        DOTA_RP_OPEN_SOLO,

        [EnumMember(Value = "#DOTA_RP_PENDING")]
        DOTA_RP_PENDING,

        [EnumMember(Value = "#DOTA_RP_PLAYING_AS")]
        DOTA_RP_PLAYING_AS,

        [EnumMember(Value = "#DOTA_RP_PLAYING_OTHER")]
        DOTA_RP_PLAYING_OTHER,

        [EnumMember(Value = "#DOTA_RP_POST_GAME")]
        DOTA_RP_POST_GAME,

        [EnumMember(Value = "#DOTA_RP_PRE_GAME")]
        DOTA_RP_PRE_GAME,

        [EnumMember(Value = "#DOTA_RP_PRIVATE_LOBBY")]
        DOTA_RP_PRIVATE_LOBBY,

        [EnumMember(Value = "#DOTA_RP_SNOOZE")]
        DOTA_RP_SNOOZE,

        [EnumMember(Value = "#DOTA_RP_SPECTATING")]
        DOTA_RP_SPECTATING,

        [EnumMember(Value = "#DOTA_RP_STRATEGY_TIME")]
        DOTA_RP_STRATEGY_TIME,

        [EnumMember(Value = "#DOTA_RP_TOURNEY_BETWEEN_GAMES")]
        DOTA_RP_TOURNEY_BETWEEN_GAMES,

        [EnumMember(Value = "#DOTA_RP_TOURNEY_FINDING_MATCH")]
        DOTA_RP_TOURNEY_FINDING_MATCH,

        [EnumMember(Value = "#DOTA_RP_TOURNEY_GAME_IN_PROGRESS")]
        DOTA_RP_TOURNEY_GAME_IN_PROGRESS,

        [EnumMember(Value = "#DOTA_RP_TOURNEY_PLAYING_AS")]
        DOTA_RP_TOURNEY_PLAYING_AS,

        [EnumMember(Value = "#DOTA_RP_TOURNEY_REGISTERING")]
        DOTA_RP_TOURNEY_REGISTERING,

        [EnumMember(Value = "#DOTA_RP_UNKNOWN_EVENT_GAME")]
        DOTA_RP_UNKNOWN_EVENT_GAME,

        [EnumMember(Value = "#DOTA_RP_WAIT_FOR_MAP_TO_LOAD")]
        DOTA_RP_WAIT_FOR_MAP_TO_LOAD,

        [EnumMember(Value = "#DOTA_RP_WAIT_FOR_PLAYERS_TO_LOAD")]
        DOTA_RP_WAIT_FOR_PLAYERS_TO_LOAD,

        [EnumMember(Value = "#DOTA_RP_WATCHING_REPLAY")]
        DOTA_RP_WATCHING_REPLAY,

        [EnumMember(Value = "#DOTA_RP_WATCHING_TOURNAMENT")]
        DOTA_RP_WATCHING_TOURNAMENT,

        [EnumMember(Value = "#DOTA_RP_WATCHING_TOURNAMENT_REPLAY")]
        DOTA_RP_WATCHING_TOURNAMENT_REPLAY,

        [EnumMember(Value = "#DOTA_Rank_Calibrating")]
        DOTA_Rank_Calibrating,

        [EnumMember(Value = "#DOTA_RecentArmoryItems_ItemType")]
        DOTA_RecentArmoryItems_ItemType,

        [EnumMember(Value = "#DOTA_RecentArmoryItems_PartOfSet")]
        DOTA_RecentArmoryItems_PartOfSet,

        [EnumMember(Value = "#DOTA_RecentGame_Watch_Download")]
        DOTA_RecentGame_Watch_Download,

        [EnumMember(Value = "#DOTA_RecentGame_Watch_Error")]
        DOTA_RecentGame_Watch_Error,

        [EnumMember(Value = "#DOTA_RecentGame_Watch_NoFile")]
        DOTA_RecentGame_Watch_NoFile,

        [EnumMember(Value = "#DOTA_RecentGame_Watch_NoMatchInfo")]
        DOTA_RecentGame_Watch_NoMatchInfo,

        [EnumMember(Value = "#DOTA_RecentGame_Watch_Permission")]
        DOTA_RecentGame_Watch_Permission,

        [EnumMember(Value = "#DOTA_Recycling_Action")]
        DOTA_Recycling_Action,

        [EnumMember(Value = "#DOTA_Redeem_Item_Text")]
        DOTA_Redeem_Item_Text,

        [EnumMember(Value = "#DOTA_ReforgeItem_Action")]
        DOTA_ReforgeItem_Action,

        [EnumMember(Value = "#DOTA_ReforgeItem_NoValidItems")]
        DOTA_ReforgeItem_NoValidItems,

        [EnumMember(Value = "#DOTA_ReforgeItem_Text")]
        DOTA_ReforgeItem_Text,

        [EnumMember(Value = "#DOTA_ReforgeItem_Title")]
        DOTA_ReforgeItem_Title,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Confirm")]
        DOTA_RemoveGemFromSocket_Confirm,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Confirm_Arcana")]
        DOTA_RemoveGemFromSocket_Confirm_Arcana,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Confirm_Code")]
        DOTA_RemoveGemFromSocket_Confirm_Code,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Confirm_Immortal")]
        DOTA_RemoveGemFromSocket_Confirm_Immortal,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Confirm_Styles_Affected")]
        DOTA_RemoveGemFromSocket_Confirm_Styles_Affected,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Confirm_Title")]
        DOTA_RemoveGemFromSocket_Confirm_Title,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Fail_Title")]
        DOTA_RemoveGemFromSocket_Fail_Title,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Fail_Unremovable")]
        DOTA_RemoveGemFromSocket_Fail_Unremovable,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Failed")]
        DOTA_RemoveGemFromSocket_Failed,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_HammerCannotRemoveGem")]
        DOTA_RemoveGemFromSocket_HammerCannotRemoveGem,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_HammerIsInvalid")]
        DOTA_RemoveGemFromSocket_HammerIsInvalid,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_ItemIsInvalid")]
        DOTA_RemoveGemFromSocket_ItemIsInvalid,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_NeedHammers")]
        DOTA_RemoveGemFromSocket_NeedHammers,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_NeedHammers_Title")]
        DOTA_RemoveGemFromSocket_NeedHammers_Title,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_NeedMasterHammers")]
        DOTA_RemoveGemFromSocket_NeedMasterHammers,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Rare_Failed")]
        DOTA_RemoveGemFromSocket_Rare_Failed,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_RequiresMasterHammer")]
        DOTA_RemoveGemFromSocket_RequiresMasterHammer,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Safe_Confirm")]
        DOTA_RemoveGemFromSocket_Safe_Confirm,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Text")]
        DOTA_RemoveGemFromSocket_Text,

        [EnumMember(Value = "#DOTA_RemoveGemFromSocket_Title")]
        DOTA_RemoveGemFromSocket_Title,

        [EnumMember(Value = "#DOTA_ReplayDownload")]
        DOTA_ReplayDownload,

        [EnumMember(Value = "#DOTA_ReplayDownload_Error")]
        DOTA_ReplayDownload_Error,

        [EnumMember(Value = "#DOTA_Replays_Search")]
        DOTA_Replays_Search,

        [EnumMember(Value = "#DOTA_Request_Join_Party")]
        DOTA_Request_Join_Party,

        [EnumMember(Value = "#DOTA_Request_Merge_Parties")]
        DOTA_Request_Merge_Parties,

        [EnumMember(Value = "#DOTA_Request_Merge_Parties_NotLeader")]
        DOTA_Request_Merge_Parties_NotLeader,

        [EnumMember(Value = "#DOTA_RequestingBalance_Text")]
        DOTA_RequestingBalance_Text,

        [EnumMember(Value = "#DOTA_RequestingBalance_Title")]
        DOTA_RequestingBalance_Title,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Confirm_Body")]
        DOTA_ResetGemInSocket_Confirm_Body,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Confirm_Title")]
        DOTA_ResetGemInSocket_Confirm_Title,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Failed")]
        DOTA_ResetGemInSocket_Failed,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Succeeded")]
        DOTA_ResetGemInSocket_Succeeded,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Succeeded_Title")]
        DOTA_ResetGemInSocket_Succeeded_Title,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Text")]
        DOTA_ResetGemInSocket_Text,

        [EnumMember(Value = "#DOTA_ResetGemInSocket_Title")]
        DOTA_ResetGemInSocket_Title,

        [EnumMember(Value = "#DOTA_RespawnOutOfLives")]
        DOTA_RespawnOutOfLives,

        [EnumMember(Value = "#DOTA_RespawnTime")]
        DOTA_RespawnTime,

        [EnumMember(Value = "#DOTA_ReturnToLobby")]
        DOTA_ReturnToLobby,

        [EnumMember(Value = "#DOTA_RollDice_HasRolledRange")]
        DOTA_RollDice_HasRolledRange,

        [EnumMember(Value = "#DOTA_RollDice_UnableToRoll")]
        DOTA_RollDice_UnableToRoll,

        [EnumMember(Value = "#DOTA_Sale")]
        DOTA_Sale,

        [EnumMember(Value = "#DOTA_SavedSets_Confirm_Description")]
        DOTA_SavedSets_Confirm_Description,

        [EnumMember(Value = "#DOTA_SavedSets_Confirm_Title")]
        DOTA_SavedSets_Confirm_Title,

        [EnumMember(Value = "#DOTA_Scoreboard_Level_Hero")]
        DOTA_Scoreboard_Level_Hero,

        [EnumMember(Value = "#DOTA_Scoreboard_Picking_Hero")]
        DOTA_Scoreboard_Picking_Hero,

        [EnumMember(Value = "#DOTA_Scoreboard_disconnected")]
        DOTA_Scoreboard_disconnected,

        [EnumMember(Value = "#DOTA_Search_ItemShortDescription")]
        DOTA_Search_ItemShortDescription,

        [EnumMember(Value = "#DOTA_Search_NoResults")]
        DOTA_Search_NoResults,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_Achievements")]
        DOTA_SeasonPass_SecondaryTabHeader_Achievements,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_BattleCup")]
        DOTA_SeasonPass_SecondaryTabHeader_BattleCup,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_Campaign")]
        DOTA_SeasonPass_SecondaryTabHeader_Campaign,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_CampaignDetails")]
        DOTA_SeasonPass_SecondaryTabHeader_CampaignDetails,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_Home")]
        DOTA_SeasonPass_SecondaryTabHeader_Home,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_Rewards")]
        DOTA_SeasonPass_SecondaryTabHeader_Rewards,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_Tournament")]
        DOTA_SeasonPass_SecondaryTabHeader_Tournament,

        [EnumMember(Value = "#DOTA_SeasonPass_SecondaryTabHeader_Wagering")]
        DOTA_SeasonPass_SecondaryTabHeader_Wagering,

        [EnumMember(Value = "#DOTA_SelectGem_Action")]
        DOTA_SelectGem_Action,

        [EnumMember(Value = "#DOTA_SelectGem_Text")]
        DOTA_SelectGem_Text,

        [EnumMember(Value = "#DOTA_SelectGem_Title")]
        DOTA_SelectGem_Title,

        [EnumMember(Value = "#DOTA_SetStyle_ErrorWhileConnected")]
        DOTA_SetStyle_ErrorWhileConnected,

        [EnumMember(Value = "#DOTA_SettingsHeroSelectorDefault")]
        DOTA_SettingsHeroSelectorDefault,

        [EnumMember(Value = "#DOTA_Settings_ChooseUnit")]
        DOTA_Settings_ChooseUnit,

        [EnumMember(Value = "#DOTA_ShareLobby_Available")]
        DOTA_ShareLobby_Available,

        [EnumMember(Value = "#DOTA_ShareLobby_Available_Tooltip")]
        DOTA_ShareLobby_Available_Tooltip,

        [EnumMember(Value = "#DOTA_ShareLobby_Joined")]
        DOTA_ShareLobby_Joined,

        [EnumMember(Value = "#DOTA_ShareLobby_Joined_Tooltip")]
        DOTA_ShareLobby_Joined_Tooltip,

        [EnumMember(Value = "#DOTA_ShareLobby_Sent")]
        DOTA_ShareLobby_Sent,

        [EnumMember(Value = "#DOTA_ShareLobby_Sent_Tooltip")]
        DOTA_ShareLobby_Sent_Tooltip,

        [EnumMember(Value = "#DOTA_ShareParty_Available")]
        DOTA_ShareParty_Available,

        [EnumMember(Value = "#DOTA_ShareParty_Available_Tooltip")]
        DOTA_ShareParty_Available_Tooltip,

        [EnumMember(Value = "#DOTA_ShareParty_Joined")]
        DOTA_ShareParty_Joined,

        [EnumMember(Value = "#DOTA_ShareParty_Requested")]
        DOTA_ShareParty_Requested,

        [EnumMember(Value = "#DOTA_ShareParty_Requested_Tooltip")]
        DOTA_ShareParty_Requested_Tooltip,

        [EnumMember(Value = "#DOTA_ShareParty_Sent")]
        DOTA_ShareParty_Sent,

        [EnumMember(Value = "#DOTA_ShareParty_Sent_Tooltip")]
        DOTA_ShareParty_Sent_Tooltip,

        [EnumMember(Value = "#DOTA_Shop_BackpackStock")]
        DOTA_Shop_BackpackStock,

        [EnumMember(Value = "#DOTA_Shop_Category_{0}")]
        DOTA_Shop_Category_NUM,

        [EnumMember(Value = "#DOTA_Shop_Item_Complete")]
        DOTA_Shop_Item_Complete,

        [EnumMember(Value = "#DOTA_Shop_Item_Error_Cant_Afford")]
        DOTA_Shop_Item_Error_Cant_Afford,

        [EnumMember(Value = "#DOTA_Shop_Item_Error_Disallowed")]
        DOTA_Shop_Item_Error_Disallowed,

        [EnumMember(Value = "#DOTA_Shop_Item_Error_Disallowed_Hero")]
        DOTA_Shop_Item_Error_Disallowed_Hero,

        [EnumMember(Value = "#DOTA_Shop_Item_Error_Need_SecretShop")]
        DOTA_Shop_Item_Error_Need_SecretShop,

        [EnumMember(Value = "#DOTA_Shop_Item_Error_Need_SideShop")]
        DOTA_Shop_Item_Error_Need_SideShop,

        [EnumMember(Value = "#DOTA_Shop_Item_Error_Out_of_Stock")]
        DOTA_Shop_Item_Error_Out_of_Stock,

        [EnumMember(Value = "#DOTA_Shop_Restock")]
        DOTA_Shop_Restock,

        [EnumMember(Value = "#DOTA_Shop_Search_No_Results")]
        DOTA_Shop_Search_No_Results,

        [EnumMember(Value = "#DOTA_Shop_Search_Results")]
        DOTA_Shop_Search_Results,

        [EnumMember(Value = "#DOTA_Shop_Search_Results_Partial")]
        DOTA_Shop_Search_Results_Partial,

        [EnumMember(Value = "#DOTA_Shop_Stock")]
        DOTA_Shop_Stock,

        [EnumMember(Value = "#DOTA_Shop_Tip_Popular")]
        DOTA_Shop_Tip_Popular,

        [EnumMember(Value = "#DOTA_ShuffledItem_Label")]
        DOTA_ShuffledItem_Label,

        [EnumMember(Value = "#DOTA_Shutdown_Header")]
        DOTA_Shutdown_Header,

        [EnumMember(Value = "#DOTA_SignOnMessage_Header")]
        DOTA_SignOnMessage_Header,

        [EnumMember(Value = "#DOTA_Socketing_HasHeroRestriction_Body")]
        DOTA_Socketing_HasHeroRestriction_Body,

        [EnumMember(Value = "#DOTA_Socketing_HasHeroRestriction_Title")]
        DOTA_Socketing_HasHeroRestriction_Title,

        [EnumMember(Value = "#DOTA_Socketing_HasRequiredSlot")]
        DOTA_Socketing_HasRequiredSlot,

        [EnumMember(Value = "#DOTA_Socketing_Legacy_Body")]
        DOTA_Socketing_Legacy_Body,

        [EnumMember(Value = "#DOTA_Socketing_Legacy_Title")]
        DOTA_Socketing_Legacy_Title,

        [EnumMember(Value = "#DOTA_Socketing_Stomp_Body")]
        DOTA_Socketing_Stomp_Body,

        [EnumMember(Value = "#DOTA_Socketing_Stomp_Title")]
        DOTA_Socketing_Stomp_Title,

        [EnumMember(Value = "#DOTA_Socketing_Untradable_Body")]
        DOTA_Socketing_Untradable_Body,

        [EnumMember(Value = "#DOTA_Socketing_Untradable_Title")]
        DOTA_Socketing_Untradable_Title,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_Failed")]
        DOTA_SpectatorLobbyWatchingResult_Failed,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_GCRejected")]
        DOTA_SpectatorLobbyWatchingResult_GCRejected,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_NoGameDetails")]
        DOTA_SpectatorLobbyWatchingResult_NoGameDetails,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_NoLobby")]
        DOTA_SpectatorLobbyWatchingResult_NoLobby,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_NotConnected")]
        DOTA_SpectatorLobbyWatchingResult_NotConnected,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_NotLobbyLeader")]
        DOTA_SpectatorLobbyWatchingResult_NotLobbyLeader,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_Success")]
        DOTA_SpectatorLobbyWatchingResult_Success,

        [EnumMember(Value = "#DOTA_SpectatorLobbyWatchingResult_Timeout")]
        DOTA_SpectatorLobbyWatchingResult_Timeout,

        [EnumMember(Value = "#DOTA_SpectatorLobby_AnyLanguage")]
        DOTA_SpectatorLobby_AnyLanguage,

        [EnumMember(Value = "#DOTA_SpectatorLobby_FailedToLeave")]
        DOTA_SpectatorLobby_FailedToLeave,

        [EnumMember(Value = "#DOTA_SpectatorLobby_UnableToCreate")]
        DOTA_SpectatorLobby_UnableToCreate,

        [EnumMember(Value = "#DOTA_SpectatorLobby_UnableToJoin")]
        DOTA_SpectatorLobby_UnableToJoin,

        [EnumMember(Value = "#DOTA_SpectatorLobby_WatchLiveMatch")]
        DOTA_SpectatorLobby_WatchLiveMatch,

        [EnumMember(Value = "#DOTA_SpectatorLobby_WatchMatchReplay")]
        DOTA_SpectatorLobby_WatchMatchReplay,

        [EnumMember(Value = "#DOTA_SpectatorLobby_WatchNone")]
        DOTA_SpectatorLobby_WatchNone,

        [EnumMember(Value = "#DOTA_SpectatorLobby_WatchStream")]
        DOTA_SpectatorLobby_WatchStream,

        [EnumMember(Value = "#DOTA_SpectatorMode_Directed")]
        DOTA_SpectatorMode_Directed,

        [EnumMember(Value = "#DOTA_SpectatorMode_FreeCam")]
        DOTA_SpectatorMode_FreeCam,

        [EnumMember(Value = "#DOTA_SpectatorMode_HeroChase")]
        DOTA_SpectatorMode_HeroChase,

        [EnumMember(Value = "#DOTA_SpectatorMode_PlayerView")]
        DOTA_SpectatorMode_PlayerView,

        [EnumMember(Value = "#DOTA_StackCount")]
        DOTA_StackCount,

        [EnumMember(Value = "#DOTA_StandardResults")]
        DOTA_StandardResults,

        [EnumMember(Value = "#DOTA_StartSoloBotMatch")]
        DOTA_StartSoloBotMatch,

        [EnumMember(Value = "#DOTA_StatTooltip_Agility")]
        DOTA_StatTooltip_Agility,

        [EnumMember(Value = "#DOTA_StatTooltip_GainPerLevel")]
        DOTA_StatTooltip_GainPerLevel,

        [EnumMember(Value = "#DOTA_StatTooltip_Intelligence")]
        DOTA_StatTooltip_Intelligence,

        [EnumMember(Value = "#DOTA_StatTooltip_Strength")]
        DOTA_StatTooltip_Strength,

        [EnumMember(Value = "#DOTA_StoreBrowse_AnyHero")]
        DOTA_StoreBrowse_AnyHero,

        [EnumMember(Value = "#DOTA_StoreBrowse_HeroName")]
        DOTA_StoreBrowse_HeroName,

        [EnumMember(Value = "#DOTA_StoreItemDetails_Search")]
        DOTA_StoreItemDetails_Search,

        [EnumMember(Value = "#DOTA_StoreItem_InBundle")]
        DOTA_StoreItem_InBundle,

        [EnumMember(Value = "#DOTA_StoreItem_InTreasure")]
        DOTA_StoreItem_InTreasure,

        [EnumMember(Value = "#DOTA_Store_SpotlightSale")]
        DOTA_Store_SpotlightSale,

        [EnumMember(Value = "#DOTA_Store_SpotlightTreasure")]
        DOTA_Store_SpotlightTreasure,

        [EnumMember(Value = "#DOTA_StyleUnlock_Action")]
        DOTA_StyleUnlock_Action,

        [EnumMember(Value = "#DOTA_StyleUnlock_ConfirmMessage")]
        DOTA_StyleUnlock_ConfirmMessage,

        [EnumMember(Value = "#DOTA_StyleUnlock_ConfirmTitle")]
        DOTA_StyleUnlock_ConfirmTitle,

        [EnumMember(Value = "#DOTA_StyleUnlock_EquipInfused")]
        DOTA_StyleUnlock_EquipInfused,

        [EnumMember(Value = "#DOTA_StyleUnlock_Error")]
        DOTA_StyleUnlock_Error,

        [EnumMember(Value = "#DOTA_StyleUnlock_NoValidItems")]
        DOTA_StyleUnlock_NoValidItems,

        [EnumMember(Value = "#DOTA_StyleUnlock_Text")]
        DOTA_StyleUnlock_Text,

        [EnumMember(Value = "#DOTA_StyleUnlock_Title")]
        DOTA_StyleUnlock_Title,

        [EnumMember(Value = "#DOTA_StyleUnlock_UnpackBundle")]
        DOTA_StyleUnlock_UnpackBundle,

        [EnumMember(Value = "#DOTA_StyleUnlock_UnpackBundleError")]
        DOTA_StyleUnlock_UnpackBundleError,

        [EnumMember(Value = "#DOTA_TI7_EventZone_{0}")]
        DOTA_TI7_EventZone_STRING,

        [EnumMember(Value = "#DOTA_TI7_EventZone_completed")]
        DOTA_TI7_EventZone_completed,

        [EnumMember(Value = "#DOTA_TI7_EventZone_completed_act_2")]
        DOTA_TI7_EventZone_completed_act_2,

        [EnumMember(Value = "#DOTA_TIP_PREDICTION_NONEMADE")]
        DOTA_TIP_PREDICTION_NONEMADE,

        [EnumMember(Value = "#DOTA_TIP_UNSET_ELAPSEDTIME")]
        DOTA_TIP_UNSET_ELAPSEDTIME,

        [EnumMember(Value = "#DOTA_TIP_UNSET_TIMESTAMP")]
        DOTA_TIP_UNSET_TIMESTAMP,

        [EnumMember(Value = "#DOTA_TabName_Browse")]
        DOTA_TabName_Browse,

        [EnumMember(Value = "#DOTA_TabName_Featured")]
        DOTA_TabName_Featured,

        [EnumMember(Value = "#DOTA_TabName_GlobalItems")]
        DOTA_TabName_GlobalItems,

        [EnumMember(Value = "#DOTA_TabName_Heroes")]
        DOTA_TabName_Heroes,

        [EnumMember(Value = "#DOTA_TabName_ModsBrowse")]
        DOTA_TabName_ModsBrowse,

        [EnumMember(Value = "#DOTA_TabName_ModsLobbyList")]
        DOTA_TabName_ModsLobbyList,

        [EnumMember(Value = "#DOTA_TabName_ModsOverview")]
        DOTA_TabName_ModsOverview,

        [EnumMember(Value = "#DOTA_TabName_ModsSubscribed")]
        DOTA_TabName_ModsSubscribed,

        [EnumMember(Value = "#DOTA_TabName_Treasury")]
        DOTA_TabName_Treasury,

        [EnumMember(Value = "#DOTA_TeamConfirmKickMember")]
        DOTA_TeamConfirmKickMember,

        [EnumMember(Value = "#DOTA_TeamCreateNewTeam")]
        DOTA_TeamCreateNewTeam,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_CreatorBusy")]
        DOTA_TeamCreate_Error_CreatorBusy,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_Header")]
        DOTA_TeamCreate_Error_Header,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_InsufficientLevel")]
        DOTA_TeamCreate_Error_InsufficientLevel,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_LogoUploadFailed")]
        DOTA_TeamCreate_Error_LogoUploadFailed,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_TeamCreationOnCooldown")]
        DOTA_TeamCreate_Error_TeamCreationOnCooldown,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_TeamLimitReached")]
        DOTA_TeamCreate_Error_TeamLimitReached,

        [EnumMember(Value = "#DOTA_TeamCreate_Error_Unspecified")]
        DOTA_TeamCreate_Error_Unspecified,

        [EnumMember(Value = "#DOTA_TeamCreate_Success")]
        DOTA_TeamCreate_Success,

        [EnumMember(Value = "#DOTA_TeamCreate_Wait")]
        DOTA_TeamCreate_Wait,

        [EnumMember(Value = "#DOTA_TeamCreate_Wait_Header")]
        DOTA_TeamCreate_Wait_Header,

        [EnumMember(Value = "#DOTA_TeamDisbanded_Header")]
        DOTA_TeamDisbanded_Header,

        [EnumMember(Value = "#DOTA_TeamDisbanded_Member_Message")]
        DOTA_TeamDisbanded_Member_Message,

        [EnumMember(Value = "#DOTA_TeamEdit_Error_CannotRename")]
        DOTA_TeamEdit_Error_CannotRename,

        [EnumMember(Value = "#DOTA_TeamEdit_Error_Header")]
        DOTA_TeamEdit_Error_Header,

        [EnumMember(Value = "#DOTA_TeamEdit_Error_LogoUploadFailed")]
        DOTA_TeamEdit_Error_LogoUploadFailed,

        [EnumMember(Value = "#DOTA_TeamEdit_Error_Unspecified")]
        DOTA_TeamEdit_Error_Unspecified,

        [EnumMember(Value = "#DOTA_TeamEdit_Success_Header")]
        DOTA_TeamEdit_Success_Header,

        [EnumMember(Value = "#DOTA_TeamEdit_Success_Message")]
        DOTA_TeamEdit_Success_Message,

        [EnumMember(Value = "#DOTA_TeamEdit_Wait")]
        DOTA_TeamEdit_Wait,

        [EnumMember(Value = "#DOTA_TeamEdit_Wait_Header")]
        DOTA_TeamEdit_Wait_Header,

        [EnumMember(Value = "#DOTA_TeamInviteAccept_Failure_Header")]
        DOTA_TeamInviteAccept_Failure_Header,

        [EnumMember(Value = "#DOTA_TeamInviteAccept_Failure_Message")]
        DOTA_TeamInviteAccept_Failure_Message,

        [EnumMember(Value = "#DOTA_TeamInviteAccept_Failure_Unspecified")]
        DOTA_TeamInviteAccept_Failure_Unspecified,

        [EnumMember(Value = "#DOTA_TeamInviteAccept_Success_Header")]
        DOTA_TeamInviteAccept_Success_Header,

        [EnumMember(Value = "#DOTA_TeamInviteAccept_Success_Message")]
        DOTA_TeamInviteAccept_Success_Message,

        [EnumMember(Value = "#DOTA_TeamInvite_Confirm_Header")]
        DOTA_TeamInvite_Confirm_Header,

        [EnumMember(Value = "#DOTA_TeamInvite_Confirm_Message")]
        DOTA_TeamInvite_Confirm_Message,

        [EnumMember(Value = "#DOTA_TeamInvite_Error_Header")]
        DOTA_TeamInvite_Error_Header,

        [EnumMember(Value = "#DOTA_TeamInvite_Error_InsufficientLevel")]
        DOTA_TeamInvite_Error_InsufficientLevel,

        [EnumMember(Value = "#DOTA_TeamInvite_Error_MemberLimit")]
        DOTA_TeamInvite_Error_MemberLimit,

        [EnumMember(Value = "#DOTA_TeamInvite_Error_NotAvailable")]
        DOTA_TeamInvite_Error_NotAvailable,

        [EnumMember(Value = "#DOTA_TeamInvite_Error_TooManyTeams")]
        DOTA_TeamInvite_Error_TooManyTeams,

        [EnumMember(Value = "#DOTA_TeamInvite_Failure_Header")]
        DOTA_TeamInvite_Failure_Header,

        [EnumMember(Value = "#DOTA_TeamInvite_Failure_Rejection")]
        DOTA_TeamInvite_Failure_Rejection,

        [EnumMember(Value = "#DOTA_TeamInvite_Failure_Timeout")]
        DOTA_TeamInvite_Failure_Timeout,

        [EnumMember(Value = "#DOTA_TeamInvite_Failure_Unspecified")]
        DOTA_TeamInvite_Failure_Unspecified,

        [EnumMember(Value = "#DOTA_TeamInvite_Success_Header")]
        DOTA_TeamInvite_Success_Header,

        [EnumMember(Value = "#DOTA_TeamInvite_Success_Message")]
        DOTA_TeamInvite_Success_Message,

        [EnumMember(Value = "#DOTA_TeamKickLabel")]
        DOTA_TeamKickLabel,

        [EnumMember(Value = "#DOTA_TeamKickMember")]
        DOTA_TeamKickMember,

        [EnumMember(Value = "#DOTA_TeamKick_Failure_Header")]
        DOTA_TeamKick_Failure_Header,

        [EnumMember(Value = "#DOTA_TeamKick_Failure_InvalidAccount")]
        DOTA_TeamKick_Failure_InvalidAccount,

        [EnumMember(Value = "#DOTA_TeamKick_Failure_KickeeNotMember")]
        DOTA_TeamKick_Failure_KickeeNotMember,

        [EnumMember(Value = "#DOTA_TeamKick_Failure_KickerNotAdmin")]
        DOTA_TeamKick_Failure_KickerNotAdmin,

        [EnumMember(Value = "#DOTA_TeamKick_Failure_Message")]
        DOTA_TeamKick_Failure_Message,

        [EnumMember(Value = "#DOTA_TeamKick_Failure_TeamLocked")]
        DOTA_TeamKick_Failure_TeamLocked,

        [EnumMember(Value = "#DOTA_TeamKick_Success_Header")]
        DOTA_TeamKick_Success_Header,

        [EnumMember(Value = "#DOTA_TeamKick_Success_Message")]
        DOTA_TeamKick_Success_Message,

        [EnumMember(Value = "#DOTA_TeamKicked_Header")]
        DOTA_TeamKicked_Header,

        [EnumMember(Value = "#DOTA_TeamKicked_Message")]
        DOTA_TeamKicked_Message,

        [EnumMember(Value = "#DOTA_TeamLeave_Confirmation_Disband")]
        DOTA_TeamLeave_Confirmation_Disband,

        [EnumMember(Value = "#DOTA_TeamLeave_Confirmation_FailAdmin")]
        DOTA_TeamLeave_Confirmation_FailAdmin,

        [EnumMember(Value = "#DOTA_TeamLeave_Confirmation_FailDisbandPro")]
        DOTA_TeamLeave_Confirmation_FailDisbandPro,

        [EnumMember(Value = "#DOTA_TeamLeave_Confirmation_Header")]
        DOTA_TeamLeave_Confirmation_Header,

        [EnumMember(Value = "#DOTA_TeamLeave_Confirmation_Message")]
        DOTA_TeamLeave_Confirmation_Message,

        [EnumMember(Value = "#DOTA_TeamLeft_Failure_Header")]
        DOTA_TeamLeft_Failure_Header,

        [EnumMember(Value = "#DOTA_TeamLeft_Failure_Message")]
        DOTA_TeamLeft_Failure_Message,

        [EnumMember(Value = "#DOTA_TeamLeft_Failure_NotAMember")]
        DOTA_TeamLeft_Failure_NotAMember,

        [EnumMember(Value = "#DOTA_TeamLeft_Failure_TeamLocked")]
        DOTA_TeamLeft_Failure_TeamLocked,

        [EnumMember(Value = "#DOTA_TeamLeft_Header")]
        DOTA_TeamLeft_Header,

        [EnumMember(Value = "#DOTA_TeamLeft_Message")]
        DOTA_TeamLeft_Message,

        [EnumMember(Value = "#DOTA_TeamMakeAdmin")]
        DOTA_TeamMakeAdmin,

        [EnumMember(Value = "#DOTA_TeamMatchmake_AlreadyFinding")]
        DOTA_TeamMatchmake_AlreadyFinding,

        [EnumMember(Value = "#DOTA_TeamMatchmake_AlreadyGame")]
        DOTA_TeamMatchmake_AlreadyGame,

        [EnumMember(Value = "#DOTA_TeamMatchmake_AlreadyMatch")]
        DOTA_TeamMatchmake_AlreadyMatch,

        [EnumMember(Value = "#DOTA_TeamMatchmake_FailAdd")]
        DOTA_TeamMatchmake_FailAdd,

        [EnumMember(Value = "#DOTA_TeamMatchmake_FailAddCurrent")]
        DOTA_TeamMatchmake_FailAddCurrent,

        [EnumMember(Value = "#DOTA_TeamMatchmake_FailGetParty")]
        DOTA_TeamMatchmake_FailGetParty,

        [EnumMember(Value = "#DOTA_TeamMatchmake_Fail_Header")]
        DOTA_TeamMatchmake_Fail_Header,

        [EnumMember(Value = "#DOTA_TeamMatchmake_FailedTeamMember")]
        DOTA_TeamMatchmake_FailedTeamMember,

        [EnumMember(Value = "#DOTA_TeamMatchmake_Full")]
        DOTA_TeamMatchmake_Full,

        [EnumMember(Value = "#DOTA_TeamTeamRankCalibrating")]
        DOTA_TeamTeamRankCalibrating,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Confirm_Header")]
        DOTA_TeamTransferAdmin_Confirm_Header,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Confirm_Message")]
        DOTA_TeamTransferAdmin_Confirm_Message,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Failure_Header")]
        DOTA_TeamTransferAdmin_Failure_Header,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Failure_InvalidAccountType")]
        DOTA_TeamTransferAdmin_Failure_InvalidAccountType,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Failure_Message_Unspecified")]
        DOTA_TeamTransferAdmin_Failure_Message_Unspecified,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Failure_NotAdmin")]
        DOTA_TeamTransferAdmin_Failure_NotAdmin,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Failure_NotMemember")]
        DOTA_TeamTransferAdmin_Failure_NotMemember,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Failure_SameAccount")]
        DOTA_TeamTransferAdmin_Failure_SameAccount,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Notification_Header")]
        DOTA_TeamTransferAdmin_Notification_Header,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Notification_Message")]
        DOTA_TeamTransferAdmin_Notification_Message,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Success_Header")]
        DOTA_TeamTransferAdmin_Success_Header,

        [EnumMember(Value = "#DOTA_TeamTransferAdmin_Success_Message")]
        DOTA_TeamTransferAdmin_Success_Message,

        [EnumMember(Value = "#DOTA_TeamUnsetHeader")]
        DOTA_TeamUnsetHeader,

        [EnumMember(Value = "#DOTA_TeamUnsetMessage")]
        DOTA_TeamUnsetMessage,

        [EnumMember(Value = "#DOTA_Team_Invite_Accepted")]
        DOTA_Team_Invite_Accepted,

        [EnumMember(Value = "#DOTA_Teams_NoInfo")]
        DOTA_Teams_NoInfo,

        [EnumMember(Value = "#DOTA_ThousandSuffix")]
        DOTA_ThousandSuffix,

        [EnumMember(Value = "#DOTA_Tip_Chat")]
        DOTA_Tip_Chat,

        [EnumMember(Value = "#DOTA_ToolTip_Ability")]
        DOTA_ToolTip_Ability,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_Aura")]
        DOTA_ToolTip_Ability_Aura,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_AutoCast")]
        DOTA_ToolTip_Ability_AutoCast,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_Channeled")]
        DOTA_ToolTip_Ability_Channeled,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_NoTarget")]
        DOTA_ToolTip_Ability_NoTarget,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_Passive")]
        DOTA_ToolTip_Ability_Passive,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_Point")]
        DOTA_ToolTip_Ability_Point,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_Target")]
        DOTA_ToolTip_Ability_Target,

        [EnumMember(Value = "#DOTA_ToolTip_Ability_Toggle")]
        DOTA_ToolTip_Ability_Toggle,

        [EnumMember(Value = "#DOTA_ToolTip_Damage")]
        DOTA_ToolTip_Damage,

        [EnumMember(Value = "#DOTA_ToolTip_Damage_HP_Removal")]
        DOTA_ToolTip_Damage_HP_Removal,

        [EnumMember(Value = "#DOTA_ToolTip_Damage_Magical")]
        DOTA_ToolTip_Damage_Magical,

        [EnumMember(Value = "#DOTA_ToolTip_Damage_Physical")]
        DOTA_ToolTip_Damage_Physical,

        [EnumMember(Value = "#DOTA_ToolTip_Damage_Pure")]
        DOTA_ToolTip_Damage_Pure,

        [EnumMember(Value = "#DOTA_ToolTip_Dispellable")]
        DOTA_ToolTip_Dispellable,

        [EnumMember(Value = "#DOTA_ToolTip_Dispellable_Item_Yes_Soft")]
        DOTA_ToolTip_Dispellable_Item_Yes_Soft,

        [EnumMember(Value = "#DOTA_ToolTip_Dispellable_Item_Yes_Strong")]
        DOTA_ToolTip_Dispellable_Item_Yes_Strong,

        [EnumMember(Value = "#DOTA_ToolTip_Dispellable_No")]
        DOTA_ToolTip_Dispellable_No,

        [EnumMember(Value = "#DOTA_ToolTip_Dispellable_Yes_Soft")]
        DOTA_ToolTip_Dispellable_Yes_Soft,

        [EnumMember(Value = "#DOTA_ToolTip_Dispellable_Yes_Strong")]
        DOTA_ToolTip_Dispellable_Yes_Strong,

        [EnumMember(Value = "#DOTA_ToolTip_PiercesSpellImmunity")]
        DOTA_ToolTip_PiercesSpellImmunity,

        [EnumMember(Value = "#DOTA_ToolTip_PiercesSpellImmunity_No")]
        DOTA_ToolTip_PiercesSpellImmunity_No,

        [EnumMember(Value = "#DOTA_ToolTip_PiercesSpellImmunity_Yes")]
        DOTA_ToolTip_PiercesSpellImmunity_Yes,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting")]
        DOTA_ToolTip_Targeting,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_AlliedCreeps")]
        DOTA_ToolTip_Targeting_AlliedCreeps,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_AlliedHeroes")]
        DOTA_ToolTip_Targeting_AlliedHeroes,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_AlliedHeroesAndBuildings")]
        DOTA_ToolTip_Targeting_AlliedHeroesAndBuildings,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_AlliedUnits")]
        DOTA_ToolTip_Targeting_AlliedUnits,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_AlliedUnitsAndBuildings")]
        DOTA_ToolTip_Targeting_AlliedUnitsAndBuildings,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_Allies")]
        DOTA_ToolTip_Targeting_Allies,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_Enemy")]
        DOTA_ToolTip_Targeting_Enemy,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_EnemyCreeps")]
        DOTA_ToolTip_Targeting_EnemyCreeps,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_EnemyHero")]
        DOTA_ToolTip_Targeting_EnemyHero,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_EnemyHeroesAndBuildings")]
        DOTA_ToolTip_Targeting_EnemyHeroesAndBuildings,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_EnemyUnits")]
        DOTA_ToolTip_Targeting_EnemyUnits,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_EnemyUnitsAndBuildings")]
        DOTA_ToolTip_Targeting_EnemyUnitsAndBuildings,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_Trees")]
        DOTA_ToolTip_Targeting_Trees,

        [EnumMember(Value = "#DOTA_ToolTip_Targeting_Units")]
        DOTA_ToolTip_Targeting_Units,

        [EnumMember(Value = "#DOTA_Tooltip_{0}")]
        DOTA_Tooltip_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_{0}_Description")]
        DOTA_Tooltip_STRING_Description,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}")]
        DOTA_Tooltip_Ability_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}{1}")]
        DOTA_Tooltip_Ability_STRING_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}L")]
        DOTA_Tooltip_Ability_STRING_L,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}_Description")]
        DOTA_Tooltip_Ability_STRING_Description,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}_Lore")]
        DOTA_Tooltip_Ability_STRING_Lore,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}_Note{1}")]
        DOTA_Tooltip_Ability_STRING_Note_NUM,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_{0}_aghanim_description")]
        DOTA_Tooltip_Ability_STRING_aghanim_description,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_Item_{0}")]
        DOTA_Tooltip_Ability_Item_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_Next_Upgrade_Level")]
        DOTA_Tooltip_Ability_Next_Upgrade_Level,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_NoScepter")]
        DOTA_Tooltip_Ability_NoScepter,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_{0}{1}")]
        DOTA_Tooltip_Ability_item_STRING_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_{0}_Description")]
        DOTA_Tooltip_Ability_item_STRING_Description,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_{0}_Lore")]
        DOTA_Tooltip_Ability_item_STRING_Lore,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_{0}_Note{1}")]
        DOTA_Tooltip_Ability_item_STRING_Note_NUM,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_arcane")]
        DOTA_Tooltip_Ability_item_bottle_arcane,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_bounty")]
        DOTA_Tooltip_Ability_item_bottle_bounty,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_double_damage")]
        DOTA_Tooltip_Ability_item_bottle_double_damage,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_haste")]
        DOTA_Tooltip_Ability_item_bottle_haste,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_illusion")]
        DOTA_Tooltip_Ability_item_bottle_illusion,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_invisible")]
        DOTA_Tooltip_Ability_item_bottle_invisible,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_bottle_regeneration")]
        DOTA_Tooltip_Ability_item_bottle_regeneration,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_power_treads_agi")]
        DOTA_Tooltip_Ability_item_power_treads_agi,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_power_treads_int")]
        DOTA_Tooltip_Ability_item_power_treads_int,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_item_power_treads_str")]
        DOTA_Tooltip_Ability_item_power_treads_str,

        [EnumMember(Value = "#DOTA_Tooltip_Ability_recipe")]
        DOTA_Tooltip_Ability_recipe,

        [EnumMember(Value = "#DOTA_Tooltip_Player_Card_Description")]
        DOTA_Tooltip_Player_Card_Description,

        [EnumMember(Value = "#DOTA_Tooltip_Player_Card_Featured_Match")]
        DOTA_Tooltip_Player_Card_Featured_Match,

        [EnumMember(Value = "#DOTA_Tooltip_RiverVial_Active")]
        DOTA_Tooltip_RiverVial_Active,

        [EnumMember(Value = "#DOTA_Tooltip_RiverVial_Locked")]
        DOTA_Tooltip_RiverVial_Locked,

        [EnumMember(Value = "#DOTA_Tooltip_RoshanTimer_BaseTime")]
        DOTA_Tooltip_RoshanTimer_BaseTime,

        [EnumMember(Value = "#DOTA_Tooltip_RoshanTimer_VisibleTime")]
        DOTA_Tooltip_RoshanTimer_VisibleTime,

        [EnumMember(Value = "#DOTA_Tooltip_Targeting_All_Heroes")]
        DOTA_Tooltip_Targeting_All_Heroes,

        [EnumMember(Value = "#DOTA_Tooltip_ability_{0}_{1}")]
        DOTA_Tooltip_ability_STRING_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_glyph_cooldown")]
        DOTA_Tooltip_glyph_cooldown,

        [EnumMember(Value = "#DOTA_Tooltip_glyph_cooldown_ready")]
        DOTA_Tooltip_glyph_cooldown_ready,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_item_moon_shard_consumed")]
        DOTA_Tooltip_modifier_item_moon_shard_consumed,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_item_moon_shard_consumed_Description_Dashboard")]
        DOTA_Tooltip_modifier_item_moon_shard_consumed_Description_Dashboard,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_item_ultimate_scepter_consumed")]
        DOTA_Tooltip_modifier_item_ultimate_scepter_consumed,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_item_ultimate_scepter_consumed_Description")]
        DOTA_Tooltip_modifier_item_ultimate_scepter_consumed_Description,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_legion_commander_duel_damage_boost")]
        DOTA_Tooltip_modifier_legion_commander_duel_damage_boost,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_legion_commander_duel_damage_boost_Description")]
        DOTA_Tooltip_modifier_legion_commander_duel_damage_boost_Description,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_pudge_flesh_heap")]
        DOTA_Tooltip_modifier_pudge_flesh_heap,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_pudge_flesh_heap_Description_Dashboard")]
        DOTA_Tooltip_modifier_pudge_flesh_heap_Description_Dashboard,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_silencer_int_steal")]
        DOTA_Tooltip_modifier_silencer_int_steal,

        [EnumMember(Value = "#DOTA_Tooltip_modifier_silencer_int_steal_Description")]
        DOTA_Tooltip_modifier_silencer_int_steal_Description,

        [EnumMember(Value = "#DOTA_Tooltip_rune_{0}")]
        DOTA_Tooltip_rune_STRING,

        [EnumMember(Value = "#DOTA_Tooltip_rune_{0}_description")]
        DOTA_Tooltip_rune_STRING_description,

        [EnumMember(Value = "#DOTA_TotalExperienceEarned")]
        DOTA_TotalExperienceEarned,

        [EnumMember(Value = "#DOTA_TournamentBracket_MatchList_TBD")]
        DOTA_TournamentBracket_MatchList_TBD,

        [EnumMember(Value = "#DOTA_TournamentBracket_TBD")]
        DOTA_TournamentBracket_TBD,

        [EnumMember(Value = "#DOTA_TournamentDropCountMultiple")]
        DOTA_TournamentDropCountMultiple,

        [EnumMember(Value = "#DOTA_TournamentDropCountSingle")]
        DOTA_TournamentDropCountSingle,

        [EnumMember(Value = "#DOTA_TournamentGameHasLobbyID")]
        DOTA_TournamentGameHasLobbyID,

        [EnumMember(Value = "#DOTA_TournamentGameHasMatchID")]
        DOTA_TournamentGameHasMatchID,

        [EnumMember(Value = "#DOTA_TournamentGameHasNoDireTeam")]
        DOTA_TournamentGameHasNoDireTeam,

        [EnumMember(Value = "#DOTA_TournamentGameHasNoRadiantTeam")]
        DOTA_TournamentGameHasNoRadiantTeam,

        [EnumMember(Value = "#DOTA_TournamentGameNotFound")]
        DOTA_TournamentGameNotFound,

        [EnumMember(Value = "#DOTA_TournamentGameSQLFailed")]
        DOTA_TournamentGameSQLFailed,

        [EnumMember(Value = "#DOTA_TournamentPassport_Confirm")]
        DOTA_TournamentPassport_Confirm,

        [EnumMember(Value = "#DOTA_TournamentProgram_AlreadyActivated")]
        DOTA_TournamentProgram_AlreadyActivated,

        [EnumMember(Value = "#DOTA_TournamentProgram_Confirm")]
        DOTA_TournamentProgram_Confirm,

        [EnumMember(Value = "#DOTA_TournamentProgram_ConfirmAsPoints")]
        DOTA_TournamentProgram_ConfirmAsPoints,

        [EnumMember(Value = "#DOTA_Trading_Response_Busy")]
        DOTA_Trading_Response_Busy,

        [EnumMember(Value = "#DOTA_Trading_UI_Body_Outgoing")]
        DOTA_Trading_UI_Body_Outgoing,

        [EnumMember(Value = "#DOTA_Trading_UI_Body_Waiting")]
        DOTA_Trading_UI_Body_Waiting,

        [EnumMember(Value = "#DOTA_Trading_UI_Header_Failed")]
        DOTA_Trading_UI_Header_Failed,

        [EnumMember(Value = "#DOTA_Trading_UI_Header_Outgoing")]
        DOTA_Trading_UI_Header_Outgoing,

        [EnumMember(Value = "#DOTA_Trading_UI_Header_Waiting")]
        DOTA_Trading_UI_Header_Waiting,

        [EnumMember(Value = "#DOTA_Treasure_AutographName")]
        DOTA_Treasure_AutographName,

        [EnumMember(Value = "#DOTA_Treasure_BonusReward")]
        DOTA_Treasure_BonusReward,

        [EnumMember(Value = "#DOTA_Treasure_FilterAvailable")]
        DOTA_Treasure_FilterAvailable,

        [EnumMember(Value = "#DOTA_Treasure_HelpDupe")]
        DOTA_Treasure_HelpDupe,

        [EnumMember(Value = "#DOTA_Treasure_HelpPW")]
        DOTA_Treasure_HelpPW,

        [EnumMember(Value = "#DOTA_Treasure_HelpPWNotPurchasable")]
        DOTA_Treasure_HelpPWNotPurchasable,

        [EnumMember(Value = "#DOTA_Treasure_Infusable")]
        DOTA_Treasure_Infusable,

        [EnumMember(Value = "#DOTA_Treasure_OpenDupe")]
        DOTA_Treasure_OpenDupe,

        [EnumMember(Value = "#DOTA_Treasure_OpenFailed_NoItem")]
        DOTA_Treasure_OpenFailed_NoItem,

        [EnumMember(Value = "#DOTA_Treasure_OpenFailed_NoItemsReceived")]
        DOTA_Treasure_OpenFailed_NoItemsReceived,

        [EnumMember(Value = "#DOTA_Treasure_OpenNoDupe")]
        DOTA_Treasure_OpenNoDupe,

        [EnumMember(Value = "#DOTA_Treasure_OpenSuccess")]
        DOTA_Treasure_OpenSuccess,

        [EnumMember(Value = "#DOTA_Treasure_QuantityOption")]
        DOTA_Treasure_QuantityOption,

        [EnumMember(Value = "#DOTA_Treasure_Received")]
        DOTA_Treasure_Received,

        [EnumMember(Value = "#DOTA_Treasure_SearchPlaceholder")]
        DOTA_Treasure_SearchPlaceholder,

        [EnumMember(Value = "#DOTA_Treasure_UnclaimedEventRewards_Singular")]
        DOTA_Treasure_UnclaimedEventRewards_Singular,

        [EnumMember(Value = "#DOTA_Treasure_ViewTreasure")]
        DOTA_Treasure_ViewTreasure,

        [EnumMember(Value = "#DOTA_Treausre_HelpNoDupe")]
        DOTA_Treausre_HelpNoDupe,

        [EnumMember(Value = "#DOTA_Trivia_AnswerLetter{0}")]
        DOTA_Trivia_AnswerLetter_UNUM,

        [EnumMember(Value = "#DOTA_Trivia_AnswerLetterNone")]
        DOTA_Trivia_AnswerLetterNone,

        [EnumMember(Value = "#DOTA_TrophiesCount")]
        DOTA_TrophiesCount,

        [EnumMember(Value = "#DOTA_Trophies_ViewAll")]
        DOTA_Trophies_ViewAll,

        [EnumMember(Value = "#DOTA_TrophyCount")]
        DOTA_TrophyCount,

        [EnumMember(Value = "#DOTA_TrophyDetails_PointsPerInterval")]
        DOTA_TrophyDetails_PointsPerInterval,

        [EnumMember(Value = "#DOTA_TrophyDetails_PointsPerLevel")]
        DOTA_TrophyDetails_PointsPerLevel,

        [EnumMember(Value = "#DOTA_TrophyTier")]
        DOTA_TrophyTier,

        [EnumMember(Value = "#DOTA_TrophyTierNoMax")]
        DOTA_TrophyTierNoMax,

        [EnumMember(Value = "#DOTA_TrophyXP")]
        DOTA_TrophyXP,

        [EnumMember(Value = "#DOTA_Tutorial_Archronicus_Page{0}")]
        DOTA_Tutorial_Archronicus_Page_NUM,

        [EnumMember(Value = "#DOTA_Tutorial_Archronicus_Page_Description{0}")]
        DOTA_Tutorial_Archronicus_Page_Description_NUM,

        [EnumMember(Value = "#DOTA_Tutorial_FINISHED_Positive")]
        DOTA_Tutorial_FINISHED_Positive,

        [EnumMember(Value = "#DOTA_Tutorial_Finished_BotMatches")]
        DOTA_Tutorial_Finished_BotMatches,

        [EnumMember(Value = "#DOTA_Tutorial_Finished_LimitedHeroPoolGames")]
        DOTA_Tutorial_Finished_LimitedHeroPoolGames,

        [EnumMember(Value = "#DOTA_Tutorial_Finished_ScriptedDemo")]
        DOTA_Tutorial_Finished_ScriptedDemo,

        [EnumMember(Value = "#DOTA_Tutorial_TaskCompleted")]
        DOTA_Tutorial_TaskCompleted,

        [EnumMember(Value = "#DOTA_UI_Cancel")]
        DOTA_UI_Cancel,

        [EnumMember(Value = "#DOTA_UI_Ok")]
        DOTA_UI_Ok,

        [EnumMember(Value = "#DOTA_UI_Play")]
        DOTA_UI_Play,

        [EnumMember(Value = "#DOTA_UI_check_out_builds")]
        DOTA_UI_check_out_builds,

        [EnumMember(Value = "#DOTA_UI_check_out_builds_desc")]
        DOTA_UI_check_out_builds_desc,

        [EnumMember(Value = "#DOTA_UI_default_build")]
        DOTA_UI_default_build,

        [EnumMember(Value = "#DOTA_UI_guide_copy")]
        DOTA_UI_guide_copy,

        [EnumMember(Value = "#DOTA_UnbundleInProgress_Text")]
        DOTA_UnbundleInProgress_Text,

        [EnumMember(Value = "#DOTA_UnbundleInProgress_Title")]
        DOTA_UnbundleInProgress_Title,

        [EnumMember(Value = "#DOTA_UnusualPaint_Confirm")]
        DOTA_UnusualPaint_Confirm,

        [EnumMember(Value = "#DOTA_Unverified_User_Chat")]
        DOTA_Unverified_User_Chat,

        [EnumMember(Value = "#DOTA_UseItemNotification_Title")]
        DOTA_UseItemNotification_Title,

        [EnumMember(Value = "#DOTA_UseItem_NotHighEnoughLevel")]
        DOTA_UseItem_NotHighEnoughLevel,

        [EnumMember(Value = "#DOTA_VAC_Verification_Button1")]
        DOTA_VAC_Verification_Button1,

        [EnumMember(Value = "#DOTA_VAC_Verification_Button2")]
        DOTA_VAC_Verification_Button2,

        [EnumMember(Value = "#DOTA_VAC_Verification_Header")]
        DOTA_VAC_Verification_Header,

        [EnumMember(Value = "#DOTA_VAC_Verification_Header_Party")]
        DOTA_VAC_Verification_Header_Party,

        [EnumMember(Value = "#DOTA_VAC_Verification_Header_Solo")]
        DOTA_VAC_Verification_Header_Solo,

        [EnumMember(Value = "#DOTA_VR_OpenMic")]
        DOTA_VR_OpenMic,

        [EnumMember(Value = "#DOTA_VR_PurchasingUnavailable_Message")]
        DOTA_VR_PurchasingUnavailable_Message,

        [EnumMember(Value = "#DOTA_VR_PurchasingUnavailable_Title")]
        DOTA_VR_PurchasingUnavailable_Title,

        [EnumMember(Value = "#DOTA_VR_PushToTalkSplit")]
        DOTA_VR_PushToTalkSplit,

        [EnumMember(Value = "#DOTA_ViewEventReward")]
        DOTA_ViewEventReward,

        [EnumMember(Value = "#DOTA_ViewInArmory")]
        DOTA_ViewInArmory,

        [EnumMember(Value = "#DOTA_ViolatorNew")]
        DOTA_ViolatorNew,

        [EnumMember(Value = "#DOTA_VoiceTextBanned_Reports")]
        DOTA_VoiceTextBanned_Reports,

        [EnumMember(Value = "#DOTA_VoiceTextBanned_Reports_Avoid_Overflow")]
        DOTA_VoiceTextBanned_Reports_Avoid_Overflow,

        [EnumMember(Value = "#DOTA_Warning")]
        DOTA_Warning,

        [EnumMember(Value = "#DOTA_WatchLivePreGame")]
        DOTA_WatchLivePreGame,

        [EnumMember(Value = "#DOTA_WatchLive_Broadcaster{0}")]
        DOTA_WatchLive_Broadcaster_NUM,

        [EnumMember(Value = "#DOTA_WatchLive_Search")]
        DOTA_WatchLive_Search,

        [EnumMember(Value = "#DOTA_WatchReplayError")]
        DOTA_WatchReplayError,

        [EnumMember(Value = "#DOTA_WatchReplayError_Incompatible")]
        DOTA_WatchReplayError_Incompatible,

        [EnumMember(Value = "#DOTA_WatchReplaysConextMenu_DownloadWatch")]
        DOTA_WatchReplaysConextMenu_DownloadWatch,

        [EnumMember(Value = "#DOTA_WatchReplaysConextMenu_DownloadWatchLater")]
        DOTA_WatchReplaysConextMenu_DownloadWatchLater,

        [EnumMember(Value = "#DOTA_WatchReplaysConextMenu_Watch")]
        DOTA_WatchReplaysConextMenu_Watch,

        [EnumMember(Value = "#DOTA_WatchReplaysConextMenu_WatchLater")]
        DOTA_WatchReplaysConextMenu_WatchLater,

        [EnumMember(Value = "#DOTA_WatchReplaysConextMenu_WatchLaterClear")]
        DOTA_WatchReplaysConextMenu_WatchLaterClear,

        [EnumMember(Value = "#DOTA_WatchTabName_Downloads")]
        DOTA_WatchTabName_Downloads,

        [EnumMember(Value = "#DOTA_WatchTabName_Featured")]
        DOTA_WatchTabName_Featured,

        [EnumMember(Value = "#DOTA_WatchTabName_Live")]
        DOTA_WatchTabName_Live,

        [EnumMember(Value = "#DOTA_WatchTabName_Replays")]
        DOTA_WatchTabName_Replays,

        [EnumMember(Value = "#DOTA_WatchTabName_Tournaments")]
        DOTA_WatchTabName_Tournaments,

        [EnumMember(Value = "#DOTA_WatchTournament_BestOf1")]
        DOTA_WatchTournament_BestOf1,

        [EnumMember(Value = "#DOTA_WatchTournament_BestOf3")]
        DOTA_WatchTournament_BestOf3,

        [EnumMember(Value = "#DOTA_WatchTournament_BestOf5")]
        DOTA_WatchTournament_BestOf5,

        [EnumMember(Value = "#DOTA_WatchTournaments_Search")]
        DOTA_WatchTournaments_Search,

        [EnumMember(Value = "#DOTA_Watch_GameServerNotFound")]
        DOTA_Watch_GameServerNotFound,

        [EnumMember(Value = "#DOTA_Watch_GameStatus")]
        DOTA_Watch_GameStatus,

        [EnumMember(Value = "#DOTA_Watch_LobbyNotFound")]
        DOTA_Watch_LobbyNotFound,

        [EnumMember(Value = "#DOTA_Watch_MissingLeagueSubscription")]
        DOTA_Watch_MissingLeagueSubscription,

        [EnumMember(Value = "#DOTA_Watch_Pending")]
        DOTA_Watch_Pending,

        [EnumMember(Value = "#DOTA_Watch_Streams")]
        DOTA_Watch_Streams,

        [EnumMember(Value = "#DOTA_Watch_Unavailable")]
        DOTA_Watch_Unavailable,

        [EnumMember(Value = "#DOTA_Wheel_Sorry")]
        DOTA_Wheel_Sorry,

        [EnumMember(Value = "#DOTA_Wheel_Spin_Later")]
        DOTA_Wheel_Spin_Later,

        [EnumMember(Value = "#DOTA_Wins_Plural")]
        DOTA_Wins_Plural,

        [EnumMember(Value = "#DOTA_Wins_Singular")]
        DOTA_Wins_Singular,

        [EnumMember(Value = "#DOTA_Workshop_Publish_SuccessTitle")]
        DOTA_Workshop_Publish_SuccessTitle,

        [EnumMember(Value = "#DOTA_XP_Alert_Ally")]
        DOTA_XP_Alert_Ally,

        [EnumMember(Value = "#DOTA_XP_Alert_Ally_Capped")]
        DOTA_XP_Alert_Ally_Capped,

        [EnumMember(Value = "#DOTA_XP_Alert_Enemy")]
        DOTA_XP_Alert_Enemy,

        [EnumMember(Value = "#DOTA_XP_Alert_Enemy_Capped")]
        DOTA_XP_Alert_Enemy_Capped,

        [EnumMember(Value = "#DOTA_XP_Alert_Self")]
        DOTA_XP_Alert_Self,

        [EnumMember(Value = "#DOTA_XP_Alert_Self_Capped")]
        DOTA_XP_Alert_Self_Capped,

        [EnumMember(Value = "#DOTA_XP_Graph_Title")]
        DOTA_XP_Graph_Title,

        [EnumMember(Value = "#DOTA_XP_Graph_Total")]
        DOTA_XP_Graph_Total,

        [EnumMember(Value = "#DOTA_Yearbeast_Header")]
        DOTA_Yearbeast_Header,

        [EnumMember(Value = "#DOTA_econ_item_details_add_gem")]
        DOTA_econ_item_details_add_gem,

        [EnumMember(Value = "#DOTA_econ_item_details_extract_gem")]
        DOTA_econ_item_details_extract_gem,

        [EnumMember(Value = "#DOTA_econ_item_details_replace_gem")]
        DOTA_econ_item_details_replace_gem,

        [EnumMember(Value = "#DOTA_lobby_type_name_bot_match")]
        DOTA_lobby_type_name_bot_match,

        [EnumMember(Value = "#DOTA_practice_vs_bots_team_dire")]
        DOTA_practice_vs_bots_team_dire,

        [EnumMember(Value = "#DOTA_practice_vs_bots_team_radiant")]
        DOTA_practice_vs_bots_team_radiant,

        [EnumMember(Value = "#DOTA_tooltip_econ_item_gift_allowed")]
        DOTA_tooltip_econ_item_gift_allowed,

        [EnumMember(Value = "#DOTA_tooltip_econ_item_gift_restriction_date")]
        DOTA_tooltip_econ_item_gift_restriction_date,

        [EnumMember(Value = "#DOTA_tooltip_econ_item_trade_cooldown_date")]
        DOTA_tooltip_econ_item_trade_cooldown_date,

        [EnumMember(Value = "#DOTA_tooltip_econ_item_trade_restriction_date")]
        DOTA_tooltip_econ_item_trade_restriction_date,

        [EnumMember(Value = "#DOTA_tooltip_econ_item_trade_restriction_permanent")]
        DOTA_tooltip_econ_item_trade_restriction_permanent,

        [EnumMember(Value = "#DOTA_tooltip_econ_item_unknown_team")]
        DOTA_tooltip_econ_item_unknown_team,

        [EnumMember(Value = "#Dota_Economy_Effigy_Animation_{0}_{1}")]
        Dota_Economy_Effigy_Animation_STRING_STRING,

        [EnumMember(Value = "#Dota_shared_content_available")]
        Dota_shared_content_available,

        [EnumMember(Value = "#Dota_shared_content_credit")]
        Dota_shared_content_credit,

        [EnumMember(Value = "#Dota_shared_content_credit_long")]
        Dota_shared_content_credit_long,

        [EnumMember(Value = "#Dota_shared_content_credit_long_plural")]
        Dota_shared_content_credit_long_plural,

        [EnumMember(Value = "#dota_abandon_dialog_confirm")]
        dota_abandon_dialog_confirm,

        [EnumMember(Value = "#dota_abandon_dialog_title")]
        dota_abandon_dialog_title,

        [EnumMember(Value = "#dota_abandon_dialog_yes")]
        dota_abandon_dialog_yes,

        [EnumMember(Value = "#dota_accept_match_accept")]
        dota_accept_match_accept,

        [EnumMember(Value = "#dota_all_chat_label_prefix")]
        dota_all_chat_label_prefix,

        [EnumMember(Value = "#dota_broadcaster_recommendation_new_players")]
        dota_broadcaster_recommendation_new_players,

        [EnumMember(Value = "#dota_cannot_request_steamdatagram_ticket")]
        dota_cannot_request_steamdatagram_ticket,

        [EnumMember(Value = "#dota_chat_battlecup_tab")]
        dota_chat_battlecup_tab,

        [EnumMember(Value = "#dota_chat_cafe_tab")]
        dota_chat_cafe_tab,

        [EnumMember(Value = "#dota_chat_fantasy_draft")]
        dota_chat_fantasy_draft,

        [EnumMember(Value = "#dota_chat_game_tab")]
        dota_chat_game_tab,

        [EnumMember(Value = "#dota_chat_guild_tab")]
        dota_chat_guild_tab,

        [EnumMember(Value = "#dota_chat_is_muted")]
        dota_chat_is_muted,

        [EnumMember(Value = "#dota_chat_is_muted_newplayer")]
        dota_chat_is_muted_newplayer,

        [EnumMember(Value = "#dota_chat_lobby_tab")]
        dota_chat_lobby_tab,

        [EnumMember(Value = "#dota_chat_party_tab")]
        dota_chat_party_tab,

        [EnumMember(Value = "#dota_chat_post_game")]
        dota_chat_post_game,

        [EnumMember(Value = "#dota_chat_team_tab")]
        dota_chat_team_tab,

        [EnumMember(Value = "#dota_chat_trivia")]
        dota_chat_trivia,

        [EnumMember(Value = "#dota_chatwheel_message_MissingHero")]
        dota_chatwheel_message_MissingHero,

        [EnumMember(Value = "#dota_chatwheel_message_ReturnedHero")]
        dota_chatwheel_message_ReturnedHero,

        [EnumMember(Value = "#dota_cm_bonus_time")]
        dota_cm_bonus_time,

        [EnumMember(Value = "#dota_combat_log_ability_cast")]
        dota_combat_log_ability_cast,

        [EnumMember(Value = "#dota_combat_log_ability_cast_target")]
        dota_combat_log_ability_cast_target,

        [EnumMember(Value = "#dota_combat_log_ability_toggle_off")]
        dota_combat_log_ability_toggle_off,

        [EnumMember(Value = "#dota_combat_log_ability_toggle_on")]
        dota_combat_log_ability_toggle_on,

        [EnumMember(Value = "#dota_combat_log_ability_triggered")]
        dota_combat_log_ability_triggered,

        [EnumMember(Value = "#dota_combat_log_ability_triggered_target")]
        dota_combat_log_ability_triggered_target,

        [EnumMember(Value = "#dota_combat_log_all")]
        dota_combat_log_all,

        [EnumMember(Value = "#dota_combat_log_healed")]
        dota_combat_log_healed,

        [EnumMember(Value = "#dota_combat_log_heals")]
        dota_combat_log_heals,

        [EnumMember(Value = "#dota_combat_log_hits")]
        dota_combat_log_hits,

        [EnumMember(Value = "#dota_combat_log_hits_with")]
        dota_combat_log_hits_with,

        [EnumMember(Value = "#dota_combat_log_illusion")]
        dota_combat_log_illusion,

        [EnumMember(Value = "#dota_combat_log_is_hit")]
        dota_combat_log_is_hit,

        [EnumMember(Value = "#dota_combat_log_is_hit_with")]
        dota_combat_log_is_hit_with,

        [EnumMember(Value = "#dota_combat_log_item_cast")]
        dota_combat_log_item_cast,

        [EnumMember(Value = "#dota_combat_log_killed")]
        dota_combat_log_killed,

        [EnumMember(Value = "#dota_combat_log_killed_by")]
        dota_combat_log_killed_by,

        [EnumMember(Value = "#dota_combat_log_loses_buff")]
        dota_combat_log_loses_buff,

        [EnumMember(Value = "#dota_combat_log_loses_debuff")]
        dota_combat_log_loses_debuff,

        [EnumMember(Value = "#dota_combat_log_minion_heals")]
        dota_combat_log_minion_heals,

        [EnumMember(Value = "#dota_combat_log_playerstats")]
        dota_combat_log_playerstats,

        [EnumMember(Value = "#dota_combat_log_purchase")]
        dota_combat_log_purchase,

        [EnumMember(Value = "#dota_combat_log_receives_buff")]
        dota_combat_log_receives_buff,

        [EnumMember(Value = "#dota_combat_log_receives_buff_from")]
        dota_combat_log_receives_buff_from,

        [EnumMember(Value = "#dota_combat_log_receives_debuff")]
        dota_combat_log_receives_debuff,

        [EnumMember(Value = "#dota_combat_log_receives_debuff_from")]
        dota_combat_log_receives_debuff_from,

        [EnumMember(Value = "#dota_commends_remaining")]
        dota_commends_remaining,

        [EnumMember(Value = "#dota_commends_total")]
        dota_commends_total,

        [EnumMember(Value = "#dota_create_team_error_bad_characters")]
        dota_create_team_error_bad_characters,

        [EnumMember(Value = "#dota_create_team_error_empty_name")]
        dota_create_team_error_empty_name,

        [EnumMember(Value = "#dota_create_team_error_empty_tag")]
        dota_create_team_error_empty_tag,

        [EnumMember(Value = "#dota_create_team_error_name_taken")]
        dota_create_team_error_name_taken,

        [EnumMember(Value = "#dota_create_team_error_tag_taken")]
        dota_create_team_error_tag_taken,

        [EnumMember(Value = "#dota_create_team_error_too_long")]
        dota_create_team_error_too_long,

        [EnumMember(Value = "#dota_create_team_name_adjusted")]
        dota_create_team_name_adjusted,

        [EnumMember(Value = "#dota_cursor_cooldown")]
        dota_cursor_cooldown,

        [EnumMember(Value = "#dota_cursor_cooldown_no_time")]
        dota_cursor_cooldown_no_time,

        [EnumMember(Value = "#dota_cursor_muted")]
        dota_cursor_muted,

        [EnumMember(Value = "#dota_cursor_no_mana")]
        dota_cursor_no_mana,

        [EnumMember(Value = "#dota_cursor_silenced")]
        dota_cursor_silenced,

        [EnumMember(Value = "#dota_desc_pennant_dire")]
        dota_desc_pennant_dire,

        [EnumMember(Value = "#dota_desc_pennant_radiant")]
        dota_desc_pennant_radiant,

        [EnumMember(Value = "#dota_dire")]
        dota_dire,

        [EnumMember(Value = "#dota_finding_1v1_match_casual")]
        dota_finding_1v1_match_casual,

        [EnumMember(Value = "#dota_finding_match_bot")]
        dota_finding_match_bot,

        [EnumMember(Value = "#dota_finding_match_bot_easy")]
        dota_finding_match_bot_easy,

        [EnumMember(Value = "#dota_finding_match_bot_hard")]
        dota_finding_match_bot_hard,

        [EnumMember(Value = "#dota_finding_match_bot_medium")]
        dota_finding_match_bot_medium,

        [EnumMember(Value = "#dota_finding_match_bot_passive")]
        dota_finding_match_bot_passive,

        [EnumMember(Value = "#dota_finding_match_bot_unfair")]
        dota_finding_match_bot_unfair,

        [EnumMember(Value = "#dota_finding_match_event")]
        dota_finding_match_event,

        [EnumMember(Value = "#dota_finding_match_generic")]
        dota_finding_match_generic,

        [EnumMember(Value = "#dota_finding_match_practice_lobby")]
        dota_finding_match_practice_lobby,

        [EnumMember(Value = "#dota_finding_match_ranked")]
        dota_finding_match_ranked,

        [EnumMember(Value = "#dota_finding_match_seasonal_ranked")]
        dota_finding_match_seasonal_ranked,

        [EnumMember(Value = "#dota_finding_match_steam_group")]
        dota_finding_match_steam_group,

        [EnumMember(Value = "#dota_finding_match_team")]
        dota_finding_match_team,

        [EnumMember(Value = "#dota_finding_match_weekend_tourney")]
        dota_finding_match_weekend_tourney,

        [EnumMember(Value = "#dota_finding_weekendtourney_finals")]
        dota_finding_weekendtourney_finals,

        [EnumMember(Value = "#dota_finding_weekendtourney_generic")]
        dota_finding_weekendtourney_generic,

        [EnumMember(Value = "#dota_finding_weekendtourney_quarterfinals")]
        dota_finding_weekendtourney_quarterfinals,

        [EnumMember(Value = "#dota_finding_weekendtourney_semifinals")]
        dota_finding_weekendtourney_semifinals,

        [EnumMember(Value = "#dota_fountain")]
        dota_fountain,

        [EnumMember(Value = "#dota_game_end_signout_pending_title")]
        dota_game_end_signout_pending_title,

        [EnumMember(Value = "#dota_game_end_victory_title_dire")]
        dota_game_end_victory_title_dire,

        [EnumMember(Value = "#dota_game_end_victory_title_radiant")]
        dota_game_end_victory_title_radiant,

        [EnumMember(Value = "#dota_game_end_victory_title_team")]
        dota_game_end_victory_title_team,

        [EnumMember(Value = "#dota_gift_popup_footer")]
        dota_gift_popup_footer,

        [EnumMember(Value = "#dota_gsb_attack_damage")]
        dota_gsb_attack_damage,

        [EnumMember(Value = "#dota_gsb_attack_speed")]
        dota_gsb_attack_speed,

        [EnumMember(Value = "#dota_gsb_block_chance")]
        dota_gsb_block_chance,

        [EnumMember(Value = "#dota_gsb_critical_strike_amount")]
        dota_gsb_critical_strike_amount,

        [EnumMember(Value = "#dota_gsb_critical_strike_chance")]
        dota_gsb_critical_strike_chance,

        [EnumMember(Value = "#dota_gsb_health")]
        dota_gsb_health,

        [EnumMember(Value = "#dota_gsb_magical_armor")]
        dota_gsb_magical_armor,

        [EnumMember(Value = "#dota_gsb_move_speed")]
        dota_gsb_move_speed,

        [EnumMember(Value = "#dota_gsb_physical_armor")]
        dota_gsb_physical_armor,

        [EnumMember(Value = "#dota_hud_error_ability_cant_upgrade_at_max")]
        dota_hud_error_ability_cant_upgrade_at_max,

        [EnumMember(Value = "#dota_hud_error_ability_cant_upgrade_hero_level")]
        dota_hud_error_ability_cant_upgrade_hero_level,

        [EnumMember(Value = "#dota_hud_error_ability_cant_upgrade_no_points")]
        dota_hud_error_ability_cant_upgrade_no_points,

        [EnumMember(Value = "#dota_hud_error_ability_disabled_by_root")]
        dota_hud_error_ability_disabled_by_root,

        [EnumMember(Value = "#dota_hud_error_ability_in_cooldown")]
        dota_hud_error_ability_in_cooldown,

        [EnumMember(Value = "#dota_hud_error_ability_inactive")]
        dota_hud_error_ability_inactive,

        [EnumMember(Value = "#dota_hud_error_ability_is_hidden")]
        dota_hud_error_ability_is_hidden,

        [EnumMember(Value = "#dota_hud_error_ability_not_learned")]
        dota_hud_error_ability_not_learned,

        [EnumMember(Value = "#dota_hud_error_buyback_disabled_reapers_scythe")]
        dota_hud_error_buyback_disabled_reapers_scythe,

        [EnumMember(Value = "#dota_hud_error_can_only_use_on_river")]
        dota_hud_error_can_only_use_on_river,

        [EnumMember(Value = "#dota_hud_error_cannot_dominate")]
        dota_hud_error_cannot_dominate,

        [EnumMember(Value = "#dota_hud_error_cannot_transmute")]
        dota_hud_error_cannot_transmute,

        [EnumMember(Value = "#dota_hud_error_cant_cast_creep_level")]
        dota_hud_error_cant_cast_creep_level,

        [EnumMember(Value = "#dota_hud_error_cant_cast_enemy_hero")]
        dota_hud_error_cant_cast_enemy_hero,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_ally")]
        dota_hud_error_cant_cast_on_ally,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_ancient")]
        dota_hud_error_cant_cast_on_ancient,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_building")]
        dota_hud_error_cant_cast_on_building,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_considered_hero")]
        dota_hud_error_cant_cast_on_considered_hero,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_courier")]
        dota_hud_error_cant_cast_on_courier,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_creep")]
        dota_hud_error_cant_cast_on_creep,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_dominated")]
        dota_hud_error_cant_cast_on_dominated,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_enemy")]
        dota_hud_error_cant_cast_on_enemy,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_hero")]
        dota_hud_error_cant_cast_on_hero,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_illusion")]
        dota_hud_error_cant_cast_on_illusion,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_mechanical")]
        dota_hud_error_cant_cast_on_mechanical,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_non_tree")]
        dota_hud_error_cant_cast_on_non_tree,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_non_tree_ward")]
        dota_hud_error_cant_cast_on_non_tree_ward,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_other")]
        dota_hud_error_cant_cast_on_other,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_roshan")]
        dota_hud_error_cant_cast_on_roshan,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_self")]
        dota_hud_error_cant_cast_on_self,

        [EnumMember(Value = "#dota_hud_error_cant_cast_on_summoned")]
        dota_hud_error_cant_cast_on_summoned,

        [EnumMember(Value = "#dota_hud_error_cant_cast_scepter_buff")]
        dota_hud_error_cant_cast_scepter_buff,

        [EnumMember(Value = "#dota_hud_error_cant_deny_health_too_high")]
        dota_hud_error_cant_deny_health_too_high,

        [EnumMember(Value = "#dota_hud_error_cant_disassemble_stash_out_of_range")]
        dota_hud_error_cant_disassemble_stash_out_of_range,

        [EnumMember(Value = "#dota_hud_error_cant_drag_channeling_item")]
        dota_hud_error_cant_drag_channeling_item,

        [EnumMember(Value = "#dota_hud_error_cant_give_item_to_enemy")]
        dota_hud_error_cant_give_item_to_enemy,

        [EnumMember(Value = "#dota_hud_error_cant_glyph")]
        dota_hud_error_cant_glyph,

        [EnumMember(Value = "#dota_hud_error_cant_move_item_to_stash")]
        dota_hud_error_cant_move_item_to_stash,

        [EnumMember(Value = "#dota_hud_error_cant_pick_up_item")]
        dota_hud_error_cant_pick_up_item,

        [EnumMember(Value = "#dota_hud_error_cant_pick_up_items")]
        dota_hud_error_cant_pick_up_items,

        [EnumMember(Value = "#dota_hud_error_cant_pick_up_runes")]
        dota_hud_error_cant_pick_up_runes,

        [EnumMember(Value = "#dota_hud_error_cant_place_near_mine")]
        dota_hud_error_cant_place_near_mine,

        [EnumMember(Value = "#dota_hud_error_cant_purchase_inventory_full")]
        dota_hud_error_cant_purchase_inventory_full,

        [EnumMember(Value = "#dota_hud_error_cant_quick_cast_at_location")]
        dota_hud_error_cant_quick_cast_at_location,

        [EnumMember(Value = "#dota_hud_error_cant_radar")]
        dota_hud_error_cant_radar,

        [EnumMember(Value = "#dota_hud_error_cant_resummon_now")]
        dota_hud_error_cant_resummon_now,

        [EnumMember(Value = "#dota_hud_error_cant_sell_item")]
        dota_hud_error_cant_sell_item,

        [EnumMember(Value = "#dota_hud_error_cant_sell_item_while_dead")]
        dota_hud_error_cant_sell_item_while_dead,

        [EnumMember(Value = "#dota_hud_error_cant_sell_shop_not_in_range")]
        dota_hud_error_cant_sell_shop_not_in_range,

        [EnumMember(Value = "#dota_hud_error_cant_shop_auto_buy_enabled")]
        dota_hud_error_cant_shop_auto_buy_enabled,

        [EnumMember(Value = "#dota_hud_error_cant_steal_spell")]
        dota_hud_error_cant_steal_spell,

        [EnumMember(Value = "#dota_hud_error_cant_target_item")]
        dota_hud_error_cant_target_item,

        [EnumMember(Value = "#dota_hud_error_cant_target_rune")]
        dota_hud_error_cant_target_rune,

        [EnumMember(Value = "#dota_hud_error_cant_target_shop")]
        dota_hud_error_cant_target_shop,

        [EnumMember(Value = "#dota_hud_error_cant_target_unexplored")]
        dota_hud_error_cant_target_unexplored,

        [EnumMember(Value = "#dota_hud_error_cant_target_units")]
        dota_hud_error_cant_target_units,

        [EnumMember(Value = "#dota_hud_error_cant_toss")]
        dota_hud_error_cant_toss,

        [EnumMember(Value = "#dota_hud_error_courier_cant_use_item")]
        dota_hud_error_courier_cant_use_item,

        [EnumMember(Value = "#dota_hud_error_disallowed_item")]
        dota_hud_error_disallowed_item,

        [EnumMember(Value = "#dota_hud_error_doom_already_devouring")]
        dota_hud_error_doom_already_devouring,

        [EnumMember(Value = "#dota_hud_error_ember_spirit_no_active_remnants")]
        dota_hud_error_ember_spirit_no_active_remnants,

        [EnumMember(Value = "#dota_hud_error_ember_spirit_no_charges")]
        dota_hud_error_ember_spirit_no_charges,

        [EnumMember(Value = "#dota_hud_error_game_is_paused")]
        dota_hud_error_game_is_paused,

        [EnumMember(Value = "#dota_hud_error_glimmmer_building")]
        dota_hud_error_glimmmer_building,

        [EnumMember(Value = "#dota_hud_error_hero_cant_be_denied")]
        dota_hud_error_hero_cant_be_denied,

        [EnumMember(Value = "#dota_hud_error_hero_off_screen")]
        dota_hud_error_hero_off_screen,

        [EnumMember(Value = "#dota_hud_error_infest_ancient")]
        dota_hud_error_infest_ancient,

        [EnumMember(Value = "#dota_hud_error_item_cant_be_dropped")]
        dota_hud_error_item_cant_be_dropped,

        [EnumMember(Value = "#dota_hud_error_item_cant_be_used_from_stash")]
        dota_hud_error_item_cant_be_used_from_stash,

        [EnumMember(Value = "#dota_hud_error_item_in_cooldown")]
        dota_hud_error_item_in_cooldown,

        [EnumMember(Value = "#dota_hud_error_item_muted")]
        dota_hud_error_item_muted,

        [EnumMember(Value = "#dota_hud_error_item_not_in_unit_inventory")]
        dota_hud_error_item_not_in_unit_inventory,

        [EnumMember(Value = "#dota_hud_error_item_out_of_stock")]
        dota_hud_error_item_out_of_stock,

        [EnumMember(Value = "#dota_hud_error_must_target_tree")]
        dota_hud_error_must_target_tree,

        [EnumMember(Value = "#dota_hud_error_no_charges")]
        dota_hud_error_no_charges,

        [EnumMember(Value = "#dota_hud_error_no_corpses")]
        dota_hud_error_no_corpses,

        [EnumMember(Value = "#dota_hud_error_no_courier")]
        dota_hud_error_no_courier,

        [EnumMember(Value = "#dota_hud_error_no_items_to_deliver")]
        dota_hud_error_no_items_to_deliver,

        [EnumMember(Value = "#dota_hud_error_no_items_to_retrieve")]
        dota_hud_error_no_items_to_retrieve,

        [EnumMember(Value = "#dota_hud_error_no_target")]
        dota_hud_error_no_target,

        [EnumMember(Value = "#dota_hud_error_no_trees_here")]
        dota_hud_error_no_trees_here,

        [EnumMember(Value = "#dota_hud_error_no_wards_here")]
        dota_hud_error_no_wards_here,

        [EnumMember(Value = "#dota_hud_error_not_enough_gold")]
        dota_hud_error_not_enough_gold,

        [EnumMember(Value = "#dota_hud_error_not_enough_mana")]
        dota_hud_error_not_enough_mana,

        [EnumMember(Value = "#dota_hud_error_only_cast_mana_units")]
        dota_hud_error_only_cast_mana_units,

        [EnumMember(Value = "#dota_hud_error_only_deliberate_channel_cancel")]
        dota_hud_error_only_deliberate_channel_cancel,

        [EnumMember(Value = "#dota_hud_error_queue_full")]
        dota_hud_error_queue_full,

        [EnumMember(Value = "#dota_hud_error_secret_shop_not_in_range")]
        dota_hud_error_secret_shop_not_in_range,

        [EnumMember(Value = "#dota_hud_error_side_shop_not_in_range")]
        dota_hud_error_side_shop_not_in_range,

        [EnumMember(Value = "#dota_hud_error_stronger_paint")]
        dota_hud_error_stronger_paint,

        [EnumMember(Value = "#dota_hud_error_target_attack_immune")]
        dota_hud_error_target_attack_immune,

        [EnumMember(Value = "#dota_hud_error_target_cant_be_denied")]
        dota_hud_error_target_cant_be_denied,

        [EnumMember(Value = "#dota_hud_error_target_cant_take_items")]
        dota_hud_error_target_cant_take_items,

        [EnumMember(Value = "#dota_hud_error_target_has_disable_help")]
        dota_hud_error_target_has_disable_help,

        [EnumMember(Value = "#dota_hud_error_target_invulnerable")]
        dota_hud_error_target_invulnerable,

        [EnumMember(Value = "#dota_hud_error_target_magic_immune")]
        dota_hud_error_target_magic_immune,

        [EnumMember(Value = "#dota_hud_error_target_out_of_range")]
        dota_hud_error_target_out_of_range,

        [EnumMember(Value = "#dota_hud_error_telekinesis")]
        dota_hud_error_telekinesis,

        [EnumMember(Value = "#dota_hud_error_toss_no_charges")]
        dota_hud_error_toss_no_charges,

        [EnumMember(Value = "#dota_hud_error_tree_dance_tree_target_out_of_range")]
        dota_hud_error_tree_dance_tree_target_out_of_range,

        [EnumMember(Value = "#dota_hud_error_treejump_tree_target_same_tree")]
        dota_hud_error_treejump_tree_target_same_tree,

        [EnumMember(Value = "#dota_hud_error_tutorial_shop_not_in_range")]
        dota_hud_error_tutorial_shop_not_in_range,

        [EnumMember(Value = "#dota_hud_error_unit_cant_attack")]
        dota_hud_error_unit_cant_attack,

        [EnumMember(Value = "#dota_hud_error_unit_cant_move")]
        dota_hud_error_unit_cant_move,

        [EnumMember(Value = "#dota_hud_error_unit_command_restricted")]
        dota_hud_error_unit_command_restricted,

        [EnumMember(Value = "#dota_hud_error_unit_dead")]
        dota_hud_error_unit_dead,

        [EnumMember(Value = "#dota_hud_error_unit_disarmed")]
        dota_hud_error_unit_disarmed,

        [EnumMember(Value = "#dota_hud_error_unit_muted")]
        dota_hud_error_unit_muted,

        [EnumMember(Value = "#dota_hud_error_unit_silenced")]
        dota_hud_error_unit_silenced,

        [EnumMember(Value = "#dota_item_build_player_suggestions")]
        dota_item_build_player_suggestions,

        [EnumMember(Value = "#dota_item_build_save_failed")]
        dota_item_build_save_failed,

        [EnumMember(Value = "#dota_item_build_save_failed_filename")]
        dota_item_build_save_failed_filename,

        [EnumMember(Value = "#dota_item_build_save_success")]
        dota_item_build_save_success,

        [EnumMember(Value = "#dota_item_build_title_default")]
        dota_item_build_title_default,

        [EnumMember(Value = "#dota_leave_dialog_confirm")]
        dota_leave_dialog_confirm,

        [EnumMember(Value = "#dota_leave_dialog_title")]
        dota_leave_dialog_title,

        [EnumMember(Value = "#dota_leave_dialog_yes")]
        dota_leave_dialog_yes,

        [EnumMember(Value = "#dota_leaver_consequence_ForcedLossForLeaversTeam")]
        dota_leaver_consequence_ForcedLossForLeaversTeam,

        [EnumMember(Value = "#dota_leaver_consequence_MMRLossForLeaversTeam")]
        dota_leaver_consequence_MMRLossForLeaversTeam,

        [EnumMember(Value = "#dota_leaver_consequence_header_failure_to_reconnect")]
        dota_leaver_consequence_header_failure_to_reconnect,

        [EnumMember(Value = "#dota_leaver_consequence_header_leave_match")]
        dota_leaver_consequence_header_leave_match,

        [EnumMember(Value = "#dota_leaver_consequence_tooltip_header")]
        dota_leaver_consequence_tooltip_header,

        [EnumMember(Value = "#dota_lobby_browser_all_game_modes")]
        dota_lobby_browser_all_game_modes,

        [EnumMember(Value = "#dota_lobby_browser_all_regions")]
        dota_lobby_browser_all_regions,

        [EnumMember(Value = "#dota_lobby_lan_title")]
        dota_lobby_lan_title,

        [EnumMember(Value = "#dota_lobby_no_team")]
        dota_lobby_no_team,

        [EnumMember(Value = "#dota_lobby_practice_vs_bots")]
        dota_lobby_practice_vs_bots,

        [EnumMember(Value = "#dota_lobby_settings_local_host")]
        dota_lobby_settings_local_host,

        [EnumMember(Value = "#dota_lobby_title_default")]
        dota_lobby_title_default,

        [EnumMember(Value = "#dota_lobby_title_local_game")]
        dota_lobby_title_local_game,

        [EnumMember(Value = "#dota_lobby_type_casual_1v1")]
        dota_lobby_type_casual_1v1,

        [EnumMember(Value = "#dota_lobby_type_competitive")]
        dota_lobby_type_competitive,

        [EnumMember(Value = "#dota_lobby_type_competitive_seasonal")]
        dota_lobby_type_competitive_seasonal,

        [EnumMember(Value = "#dota_lobby_type_coop_bot")]
        dota_lobby_type_coop_bot,

        [EnumMember(Value = "#dota_lobby_type_general")]
        dota_lobby_type_general,

        [EnumMember(Value = "#dota_lobby_type_practice")]
        dota_lobby_type_practice,

        [EnumMember(Value = "#dota_lobby_type_practice_game_mode_custom")]
        dota_lobby_type_practice_game_mode_custom,

        [EnumMember(Value = "#dota_lobby_type_tournament")]
        dota_lobby_type_tournament,

        [EnumMember(Value = "#dota_lobby_type_weekend_tourney")]
        dota_lobby_type_weekend_tourney,

        [EnumMember(Value = "#dota_lobby_type_weekend_tourney_finals")]
        dota_lobby_type_weekend_tourney_finals,

        [EnumMember(Value = "#dota_lobby_type_weekend_tourney_quarterfinals")]
        dota_lobby_type_weekend_tourney_quarterfinals,

        [EnumMember(Value = "#dota_lobby_type_weekend_tourney_semifinals")]
        dota_lobby_type_weekend_tourney_semifinals,

        [EnumMember(Value = "#dota_lobby_unknown")]
        dota_lobby_unknown,

        [EnumMember(Value = "#dota_matchmaking_language_{0}")]
        dota_matchmaking_language_STRINGDOTA_Tooltip_Ability_,

        [EnumMember(Value = "#dota_matchmaking_language_no_selection")]
        dota_matchmaking_language_no_selection,

        [EnumMember(Value = "#dota_matchmaking_region_all_unavailable")]
        dota_matchmaking_region_all_unavailable,

        [EnumMember(Value = "#dota_matchmaking_region_all_unavailable_across_party")]
        dota_matchmaking_region_all_unavailable_across_party,

        [EnumMember(Value = "#dota_matchmaking_region_all_unavailable_party_member")]
        dota_matchmaking_region_all_unavailable_party_member,

        [EnumMember(Value = "#dota_matchmaking_region_check_internet")]
        dota_matchmaking_region_check_internet,

        [EnumMember(Value = "#dota_matchmaking_region_no_good_region")]
        dota_matchmaking_region_no_good_region,

        [EnumMember(Value = "#dota_matchmaking_region_no_good_region_party")]
        dota_matchmaking_region_no_good_region_party,

        [EnumMember(Value = "#dota_matchmaking_region_ping_calculating")]
        dota_matchmaking_region_ping_calculating,

        [EnumMember(Value = "#dota_matchmaking_region_ping_failed")]
        dota_matchmaking_region_ping_failed,

        [EnumMember(Value = "#dota_matchmaking_region_ping_time")]
        dota_matchmaking_region_ping_time,

        [EnumMember(Value = "#dota_matchmaking_region_ping_time_approx")]
        dota_matchmaking_region_ping_time_approx,

        [EnumMember(Value = "#dota_matchmaking_region_ping_time_min")]
        dota_matchmaking_region_ping_time_min,

        [EnumMember(Value = "#dota_matchmaking_region_poor_choices")]
        dota_matchmaking_region_poor_choices,

        [EnumMember(Value = "#dota_matchmaking_region_poor_choices_party")]
        dota_matchmaking_region_poor_choices_party,

        [EnumMember(Value = "#dota_matchmaking_region_selected_offline")]
        dota_matchmaking_region_selected_offline,

        [EnumMember(Value = "#dota_matchmaking_region_selected_unreachable")]
        dota_matchmaking_region_selected_unreachable,

        [EnumMember(Value = "#dota_matchmaking_region_selected_unreachable_party")]
        dota_matchmaking_region_selected_unreachable_party,

        [EnumMember(Value = "#dota_matchmaking_region_title_confirm")]
        dota_matchmaking_region_title_confirm,

        [EnumMember(Value = "#dota_matchmaking_region_too_picky")]
        dota_matchmaking_region_too_picky,

        [EnumMember(Value = "#dota_matchmaking_region_unknown_ping")]
        dota_matchmaking_region_unknown_ping,

        [EnumMember(Value = "#dota_matchmaking_regions_refresh")]
        dota_matchmaking_regions_refresh,

        [EnumMember(Value = "#dota_page_post_game_replay_available_toast")]
        dota_page_post_game_replay_available_toast,

        [EnumMember(Value = "#dota_party_forfeited_match")]
        dota_party_forfeited_match,

        [EnumMember(Value = "#dota_play")]
        dota_play,

        [EnumMember(Value = "#dota_play_connecting_to_server")]
        dota_play_connecting_to_server,

        [EnumMember(Value = "#dota_play_host_loading")]
        dota_play_host_loading,

        [EnumMember(Value = "#dota_play_language")]
        dota_play_language,

        [EnumMember(Value = "#dota_play_language_many")]
        dota_play_language_many,

        [EnumMember(Value = "#dota_play_lobby_find_server")]
        dota_play_lobby_find_server,

        [EnumMember(Value = "#dota_play_no_language_selected")]
        dota_play_no_language_selected,

        [EnumMember(Value = "#dota_play_searching")]
        dota_play_searching,

        [EnumMember(Value = "#dota_post_game_bot")]
        dota_post_game_bot,

        [EnumMember(Value = "#dota_post_game_dark_moon_defeat")]
        dota_post_game_dark_moon_defeat,

        [EnumMember(Value = "#dota_post_game_dark_moon_victory")]
        dota_post_game_dark_moon_victory,

        [EnumMember(Value = "#dota_post_game_dire_team_name")]
        dota_post_game_dire_team_name,

        [EnumMember(Value = "#dota_post_game_dire_team_name_victory")]
        dota_post_game_dire_team_name_victory,

        [EnumMember(Value = "#dota_post_game_dire_victory")]
        dota_post_game_dire_victory,

        [EnumMember(Value = "#dota_post_game_radiant_team_name")]
        dota_post_game_radiant_team_name,

        [EnumMember(Value = "#dota_post_game_radiant_team_name_victory")]
        dota_post_game_radiant_team_name_victory,

        [EnumMember(Value = "#dota_post_game_radiant_victory")]
        dota_post_game_radiant_victory,

        [EnumMember(Value = "#dota_preferred_username_with_nickname")]
        dota_preferred_username_with_nickname,

        [EnumMember(Value = "#dota_preferred_username_without_nickname")]
        dota_preferred_username_without_nickname,

        [EnumMember(Value = "#dota_profile_hero")]
        dota_profile_hero,

        [EnumMember(Value = "#dota_profile_hero_with_index")]
        dota_profile_hero_with_index,

        [EnumMember(Value = "#dota_profile_name_locked")]
        dota_profile_name_locked,

        [EnumMember(Value = "#dota_profile_recent_game_result_abandon")]
        dota_profile_recent_game_result_abandon,

        [EnumMember(Value = "#dota_profile_recent_game_result_abandon_ranked")]
        dota_profile_recent_game_result_abandon_ranked,

        [EnumMember(Value = "#dota_profile_recent_game_result_loss")]
        dota_profile_recent_game_result_loss,

        [EnumMember(Value = "#dota_profile_recent_game_result_notscored")]
        dota_profile_recent_game_result_notscored,

        [EnumMember(Value = "#dota_profile_recent_game_result_win")]
        dota_profile_recent_game_result_win,

        [EnumMember(Value = "#dota_profile_team_locked")]
        dota_profile_team_locked,

        [EnumMember(Value = "#dota_profile_team_locked_pro")]
        dota_profile_team_locked_pro,

        [EnumMember(Value = "#dota_radiant")]
        dota_radiant,

        [EnumMember(Value = "#dota_ready_fail")]
        dota_ready_fail,

        [EnumMember(Value = "#dota_ready_fail_desc")]
        dota_ready_fail_desc,

        [EnumMember(Value = "#dota_region_automatic")]
        dota_region_automatic,

        [EnumMember(Value = "#dota_replay_manager_error_already_pending")]
        dota_replay_manager_error_already_pending,

        [EnumMember(Value = "#dota_replay_manager_error_chunk_failed")]
        dota_replay_manager_error_chunk_failed,

        [EnumMember(Value = "#dota_replay_manager_error_creation_failure")]
        dota_replay_manager_error_creation_failure,

        [EnumMember(Value = "#dota_replay_manager_error_disk_full")]
        dota_replay_manager_error_disk_full,

        [EnumMember(Value = "#dota_replay_manager_error_existing_partial")]
        dota_replay_manager_error_existing_partial,

        [EnumMember(Value = "#dota_replay_manager_error_partial_decompression_failure")]
        dota_replay_manager_error_partial_decompression_failure,

        [EnumMember(Value = "#dota_replay_manager_error_write_failure")]
        dota_replay_manager_error_write_failure,

        [EnumMember(Value = "#dota_report_title_commend")]
        dota_report_title_commend,

        [EnumMember(Value = "#dota_report_title_report")]
        dota_report_title_report,

        [EnumMember(Value = "#dota_reports_remaining")]
        dota_reports_remaining,

        [EnumMember(Value = "#dota_reports_total")]
        dota_reports_total,

        [EnumMember(Value = "#dota_role_core")]
        dota_role_core,

        [EnumMember(Value = "#dota_role_offlane")]
        dota_role_offlane,

        [EnumMember(Value = "#dota_role_support")]
        dota_role_support,

        [EnumMember(Value = "#dota_safe_to_abandon")]
        dota_safe_to_abandon,

        [EnumMember(Value = "#dota_safe_to_abandon_match_not_scored")]
        dota_safe_to_abandon_match_not_scored,

        [EnumMember(Value = "#dota_safe_to_abandon_match_not_scored_network")]
        dota_safe_to_abandon_match_not_scored_network,

        [EnumMember(Value = "#dota_select_friend_title")]
        dota_select_friend_title,

        [EnumMember(Value = "#dota_settings_help_tips_reset_success_text")]
        dota_settings_help_tips_reset_success_text,

        [EnumMember(Value = "#dota_settings_help_tips_reset_success_title")]
        dota_settings_help_tips_reset_success_title,

        [EnumMember(Value = "#dota_settings_no_mode_resolutions")]
        dota_settings_no_mode_resolutions,

        [EnumMember(Value = "#dota_social_feed_loading")]
        dota_social_feed_loading,

        [EnumMember(Value = "#dota_stat_dropdown")]
        dota_stat_dropdown,

        [EnumMember(Value = "#dota_stat_dropdown_buyback")]
        dota_stat_dropdown_buyback,

        [EnumMember(Value = "#dota_stat_dropdown_fantasy")]
        dota_stat_dropdown_fantasy,

        [EnumMember(Value = "#dota_stat_dropdown_fantasy_tooltip")]
        dota_stat_dropdown_fantasy_tooltip,

        [EnumMember(Value = "#dota_stat_dropdown_gold")]
        dota_stat_dropdown_gold,

        [EnumMember(Value = "#dota_stat_dropdown_gold_per_min")]
        dota_stat_dropdown_gold_per_min,

        [EnumMember(Value = "#dota_stat_dropdown_kda")]
        dota_stat_dropdown_kda,

        [EnumMember(Value = "#dota_stat_dropdown_lasthits_denies")]
        dota_stat_dropdown_lasthits_denies,

        [EnumMember(Value = "#dota_stat_dropdown_level")]
        dota_stat_dropdown_level,

        [EnumMember(Value = "#dota_stat_dropdown_networth")]
        dota_stat_dropdown_networth,

        [EnumMember(Value = "#dota_stat_dropdown_xp_per_min")]
        dota_stat_dropdown_xp_per_min,

        [EnumMember(Value = "#dota_suggest_invite_format")]
        dota_suggest_invite_format,

        [EnumMember(Value = "#dota_suggest_invite_format_leader")]
        dota_suggest_invite_format_leader,

        [EnumMember(Value = "#dota_suggest_invite_format_lobby")]
        dota_suggest_invite_format_lobby,

        [EnumMember(Value = "#dota_suggest_invite_format_lobby_leader")]
        dota_suggest_invite_format_lobby_leader,

        [EnumMember(Value = "#dota_team_average_mmr")]
        dota_team_average_mmr,

        [EnumMember(Value = "#dota_team_dire")]
        dota_team_dire,

        [EnumMember(Value = "#dota_team_mmr")]
        dota_team_mmr,

        [EnumMember(Value = "#dota_team_radiant")]
        dota_team_radiant,

        [EnumMember(Value = "#dota_ti7_purchase_preview")]
        dota_ti7_purchase_preview,

        [EnumMember(Value = "#dota_tip_beginner_15")]
        dota_tip_beginner_15,

        [EnumMember(Value = "#dota_tip_beginner_4")]
        dota_tip_beginner_4,

        [EnumMember(Value = "#dota_tip_introduction_3")]
        dota_tip_introduction_3,

        [EnumMember(Value = "#dota_title_pennant")]
        dota_title_pennant,

        [EnumMember(Value = "#dota_waitingforplayers_boost_earn_local_player")]
        dota_waitingforplayers_boost_earn_local_player,

        [EnumMember(Value = "#dota_watchtab_unread_tooltip")]
        dota_watchtab_unread_tooltip,

        [EnumMember(Value = "#dota_weekend_tourney_hub_tier_all")]
        dota_weekend_tourney_hub_tier_all,

        [EnumMember(Value = "#dota_weekend_tourney_hub_tier_n")]
        dota_weekend_tourney_hub_tier_n,

        [EnumMember(Value = "#dota_year_beast_ending_kicked")]
        dota_year_beast_ending_kicked,

        [EnumMember(Value = "#dota_year_beast_starting_kicked")]
        dota_year_beast_starting_kicked,

        [EnumMember(Value = "#DOTA_Scoreboard_Abandoned")]
        DOTA_Scoreboard_Abandoned,

        [EnumMember(Value = "#DOTA_Scoreboard_Disconnected")]
        DOTA_Scoreboard_Disconnected,

        [EnumMember(Value = "#DOTA_Chat_ActionItemReason_Cooldown")]
        DOTA_Chat_ActionItemReason_Cooldown,

        [EnumMember(Value = "#DOTA_HeroGuide_Role_Core")]
        DOTA_HeroGuide_Role_Core,

        [EnumMember(Value = "#DOTA_Hero_Selection_ComboBox_Title_Attack")]
        DOTA_Hero_Selection_ComboBox_Title_Attack,

        [EnumMember(Value = "#DOTA_Hero_Selection_ComboBox_Title_ByMine")]
        DOTA_Hero_Selection_ComboBox_Title_ByMine,

        [EnumMember(Value = "#DOTA_Hero_Selection_ComboBox_Title_Role")]
        DOTA_Hero_Selection_ComboBox_Title_Role,

        [EnumMember(Value = "#DOTA_Popup_Item_Received_Header_Unknown")]
        DOTA_Popup_Item_Received_Header_Unknown,

        [EnumMember(Value = "#dota_tip_introduction_4")]
        dota_tip_introduction_4,

        [EnumMember(Value = "#Dota_Aura_Active")]
        Dota_Aura_Active,

        [EnumMember(Value = "#Dota_Aura_Inactive")]
        Dota_Aura_Inactive,
    }
}