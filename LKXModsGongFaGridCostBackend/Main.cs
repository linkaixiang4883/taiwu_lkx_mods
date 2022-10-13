using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using Config;
using System.Collections.Generic;
using GameData.Utilities;
using GameData.Domains;
using GameData.Domains.Combat;
using GameData.Domains.CombatSkill;
using System;
using GameData.Common;

namespace LKXModsGongFaGridCostBackend
{
    [PluginConfig("LKXModsGongFaGridCostBackend", "LKX", "0.2.3")]
    public class Run : TaiwuRemakePlugin
    {
        private Harmony harmony;

        /// <summary>
        /// 避免多次处理
        /// </summary>
        public static bool Loaded = false;

        public override void Dispose()
        {
            if (harmony != null)
            {
                harmony.UnpatchSelf();
                Loaded = false;
                harmony = null;
            }
        }

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(Run));
        }

        private static bool enableGridCost;
        private static int gridCost;

        //private static bool enableGenericGrid;
        //private static int genericGrid;

        private static bool enableBaseGrid;
        private static int baseNeigongGrid;
        private static int baseCuipoGrid;
        private static int baseQingyingGrid;
        private static int baseHutiGrid;
        private static int baseQiqiaoGrid;
        public override void OnModSettingUpdate()
        {
            Loaded = false;
            DomainManager.Mod.GetSetting(ModIdStr, "enableGridCost", ref enableGridCost);
            //DomainManager.Mod.GetSetting(ModIdStr, "enableGenericGrid", ref enableGenericGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "gridCost", ref gridCost);
            //DomainManager.Mod.GetSetting(ModIdStr, "genericGrid", ref genericGrid);

            DomainManager.Mod.GetSetting(ModIdStr, "enableBaseGrid", ref enableBaseGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseNeigongGrid", ref baseNeigongGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseCuipoGrid", ref baseCuipoGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseQingyingGrid", ref baseQingyingGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseHutiGrid", ref baseHutiGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseQiqiaoGrid", ref baseQiqiaoGrid);

            Config.CombatSkill.Instance.GetAllKeys();
            if (enableBaseGrid)
            {
                GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Neigong] = (sbyte)baseNeigongGrid;
                GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Attack] = (sbyte)baseCuipoGrid;
                GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Agile] = (sbyte)baseQingyingGrid;
                GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Defense] = (sbyte)baseHutiGrid;
                GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Assist] = (sbyte)baseQiqiaoGrid;
            }
        }

        /// <summary>
        /// patch功法修改
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(Config.CombatSkill), "GetAllKeys")]
        public static void CombatSkill_GetAllKeys_Patch(Config.CombatSkill __instance, ref List<CombatSkillItem> ____dataArray)
        {
            if (Loaded)
            {
                return;
            }
            foreach (CombatSkillItem item in ____dataArray)
            {
                if(enableGridCost && item.GridCost > gridCost)
                {
                    typeof(CombatSkillItem).GetField("GridCost").SetValue(item, (sbyte)gridCost);
                }
            }

            Loaded = true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CombatDomain), "UpdateSkillNeedMobilityCanUse")]
        public static void CombatDomain_UpdateSkillNeedMobilityCanUse_Patch(CombatCharacter character, ref Dictionary<CombatSkillKey, CombatSkillData> ____selfSkillDataDict, ref Dictionary<CombatSkillKey, CombatSkillData> ____enemySkillDataDict)
        {
            CombatSkillCollection combatSkillCollection = (CombatSkillCollection)AccessTools.Field(typeof(CombatSkillDomain), "_combatSkills").GetValue(DomainManager.CombatSkill);
            foreach (CombatSkillKey combatSkillKey in (character.IsAlly ? ____selfSkillDataDict : ____enemySkillDataDict).Keys)
            {
                if (!combatSkillCollection.ContainsKey(combatSkillKey) && !character.IsAlly)
                {
                    ____enemySkillDataDict.Remove(combatSkillKey);
                }
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CombatDomain), "UpdateSkillCanUse", new Type[] {typeof(CombatCharacter), typeof(short), typeof(DataContext) })]
        public static void CombatDomain_UpdateSkillCanUse_Patch(CombatCharacter character, short skillId, DataContext context, ref Dictionary<CombatSkillKey, CombatSkillData> ____selfSkillDataDict, ref Dictionary<CombatSkillKey, CombatSkillData> ____enemySkillDataDict)
        {
            CombatSkillCollection combatSkillCollection = (CombatSkillCollection)AccessTools.Field(typeof(CombatSkillDomain), "_combatSkills").GetValue(DomainManager.CombatSkill);
            foreach (CombatSkillKey combatSkillKey in (character.IsAlly ? ____selfSkillDataDict : ____enemySkillDataDict).Keys)
            {
                if (!combatSkillCollection.ContainsKey(combatSkillKey) && !character.IsAlly)
                {
                    ____enemySkillDataDict.Remove(combatSkillKey);
                }
            }
        }

        /// <summary>
        /// 获取技能格子数量
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(GameData.Domains.Character.CharacterDomain), "GetCombatSkillSlotCounts")]
        public static void CharacterDomain_GetCombatSkillSlotCounts_Patch(GameData.Domains.Character.CharacterDomain __instance, ref sbyte[] __result)
        {
            //只修改技能格子会导致报错。这次更新后不在出现这样的问题，取消该处修改。

            /*if (enableBaseGrid)
            {
                __result[CombatSkillEquipType.Neigong] = (sbyte)baseNeigongGrid;
                __result[CombatSkillEquipType.Attack] = (sbyte)baseCuipoGrid;
                __result[CombatSkillEquipType.Agile] = (sbyte)baseQingyingGrid;
                __result[CombatSkillEquipType.Defense] = (sbyte)baseHutiGrid;
                __result[CombatSkillEquipType.Assist] = (sbyte)baseQiqiaoGrid;
                for(int i = 0; i < GameData.Domains.Character.CombatSkillHelper.MaxSlotCounts.Length; i++)
                {
                    __result[i] = GameData.Domains.Character.CombatSkillHelper.MaxSlotCounts[i];
                }
            }*/
        }
    }
}
