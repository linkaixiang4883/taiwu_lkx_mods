using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using Config;
using System.Collections.Generic;
using GameData.Utilities;
using GameData.Domains;
using GameData.Domains.Combat;
using GameData.Domains.CombatSkill;
using System;

namespace LKXModsGongFaGridCostBackend
{
    [PluginConfig("LKXModsGongFaGridCostBackend", "LKX", "0.0.1")]
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

        private static int gridCost;
        private static int genericGrid;
        private static int baseNeigongGrid;
        public override void OnModSettingUpdate()
        {
            Loaded = false;
            DomainManager.Mod.GetSetting(ModIdStr, "gridCost", ref gridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "genericGrid", ref genericGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseNeigongGrid", ref baseNeigongGrid);

            Config.CombatSkill.Instance.GetAllKeys();
            GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[0] = (sbyte)baseNeigongGrid;
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
                typeof(CombatSkillItem).GetField("GridCost").SetValue(item, (sbyte)gridCost);
                if (item.Type == GameData.Domains.CombatSkill.CombatSkillType.Neigong)
                {
                    typeof(CombatSkillItem).GetField("GenericGrid").SetValue(item, (sbyte)genericGrid);
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
                bool flag = !combatSkillCollection.ContainsKey(combatSkillKey) && !character.IsAlly;
                if (flag)
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
        public unsafe static void CharacterDomain_GetCombatSkillSlotCounts_Patch(GameData.Domains.Character.CharacterDomain __instance, ref sbyte[] __result)
        {
            __result[0] = (sbyte)baseNeigongGrid;
            if (__result[0] > GameData.Domains.Character.CombatSkillHelper.MaxSlotCounts[0])
            {
                __result[0] = GameData.Domains.Character.CombatSkillHelper.MaxSlotCounts[0];
            }
        }
    }
}
