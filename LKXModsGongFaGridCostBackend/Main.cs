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
using GameData.Domains.Character;
using CombatSkillType = GameData.Domains.CombatSkill.CombatSkillType;
using CombatSkillHelper = GameData.Domains.Character.CombatSkillHelper;
using HarmonyLib.Tools;
using System.Diagnostics;
using System.Reflection;

namespace LKXModsGongFaGridCostBackend
{
    [PluginConfig("LKXModsGongFaGridCostBackend", "LKX", "0.0.76.30")]
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

        private const sbyte NewMaxSlotCount = 99;

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(Run));
        }

        private static bool enableGridCost;
        private static bool enableAllGridCost;
        private static int gridCost;

        private static int neigongGridCost;
        private static int posingGridCost;
        private static int stuntGridCost;
        private static int fistAndPalmGridCost;
        private static int fingerGridCost;
        private static int legGridCost;
        private static int throwGridCost;
        private static int swordGridCost;
        private static int bladeGridCost;
        private static int polearmGridCost;
        private static int specialGridCost;
        private static int whipGridCost;
        private static int controllableShotGridCost;
        private static int combatMusicGridCost;

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
            DomainManager.Mod.GetSetting(ModIdStr, "enableAllGridCost", ref enableAllGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "gridCost", ref gridCost);

            DomainManager.Mod.GetSetting(ModIdStr, "neigongGridCost", ref neigongGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "posingGridCost", ref posingGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "stuntGridCost", ref stuntGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "fistAndPalmGridCost", ref fistAndPalmGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "fingerGridCost", ref fingerGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "legGridCost", ref legGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "throwGridCost", ref throwGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "swordGridCost", ref swordGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "bladeGridCost", ref bladeGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "polearmGridCost", ref polearmGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "specialGridCost", ref specialGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "whipGridCost", ref whipGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "controllableShotGridCost", ref controllableShotGridCost);
            DomainManager.Mod.GetSetting(ModIdStr, "combatMusicGridCost", ref combatMusicGridCost);

            DomainManager.Mod.GetSetting(ModIdStr, "enableBaseGrid", ref enableBaseGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseNeigongGrid", ref baseNeigongGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseCuipoGrid", ref baseCuipoGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseQingyingGrid", ref baseQingyingGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseHutiGrid", ref baseHutiGrid);
            DomainManager.Mod.GetSetting(ModIdStr, "baseQiqiaoGrid", ref baseQiqiaoGrid);

            Config.CombatSkill.Instance.GetAllKeys();
            if (enableBaseGrid)
            {
                Type CSH = typeof(GameData.Domains.Character.CombatSkillHelper);
                sbyte[] maxSlots = (sbyte[])CSH.GetField("MaxSlotCounts", BindingFlags.Static | BindingFlags.Public).GetValue(null);
                sbyte[] beginIndex = (sbyte[])CSH.GetField("SlotBeginIndexes", BindingFlags.Static | BindingFlags.Public).GetValue(null);
                sbyte[] endIndex = (sbyte[])CSH.GetField("SlotEndIndexes", BindingFlags.Static | BindingFlags.Public).GetValue(null);
                
                maxSlots[CombatSkillEquipType.Neigong] = 24;
                beginIndex[CombatSkillEquipType.Neigong] = 0;
                endIndex[CombatSkillEquipType.Neigong] = 24;

                maxSlots[CombatSkillEquipType.Attack] = 24;
                beginIndex[CombatSkillEquipType.Attack] = 24;
                endIndex[CombatSkillEquipType.Attack] = 48;

                maxSlots[CombatSkillEquipType.Agile] = 24;
                beginIndex[CombatSkillEquipType.Agile] = 48;
                endIndex[CombatSkillEquipType.Agile] = 72;

                maxSlots[CombatSkillEquipType.Defense] = 24;
                beginIndex[CombatSkillEquipType.Defense] = 72;
                endIndex[CombatSkillEquipType.Defense] = 96;

                maxSlots[CombatSkillEquipType.Assist] = 24;
                beginIndex[CombatSkillEquipType.Assist] = 96;
                endIndex[CombatSkillEquipType.Assist] = 120;

                if (baseNeigongGrid > 0)
                {
                    GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Neigong] = (sbyte)baseNeigongGrid;
                }
                if (baseCuipoGrid > 0)
                {
                    GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Attack] = (sbyte)baseCuipoGrid;
                }
                if (baseQingyingGrid > 0)
                {
                    GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Agile] = (sbyte)baseQingyingGrid;
                }
                if (baseHutiGrid > 0)
                {
                    GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Defense] = (sbyte)baseHutiGrid;
                }
                if (baseQiqiaoGrid > 0)
                {
                    GlobalConfig.Instance.CombatSkillInitialEquipSlotCounts[CombatSkillEquipType.Assist] = (sbyte)baseQiqiaoGrid;
                }
            }

            AdaptableLog.Info("变更后的MaxSlotCounts。");
            AdaptableLog.Info(CombatSkillHelper.MaxSlotCounts[0].ToString());
            AdaptableLog.Info(CombatSkillHelper.MaxSlotCounts[1].ToString());
            AdaptableLog.Info(CombatSkillHelper.MaxSlotCounts[2].ToString());
            AdaptableLog.Info(CombatSkillHelper.MaxSlotCounts[3].ToString());
            AdaptableLog.Info(CombatSkillHelper.MaxSlotCounts[4].ToString());
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
            if (!enableGridCost)
            {
                return;
            }
            foreach (CombatSkillItem item in ____dataArray)
            {
                if (enableAllGridCost)
                {
                    ModifGridCost(item, gridCost);
                }
                else
                {
                    switch (item.Type)
                    {
                        case CombatSkillType.Neigong:
                            if (neigongGridCost > 0) ModifGridCost(item, neigongGridCost);
                            break;
                        case CombatSkillType.Posing:
                            if (posingGridCost > 0) ModifGridCost(item, posingGridCost);
                            break;
                        case CombatSkillType.Stunt:
                            if (stuntGridCost > 0) ModifGridCost(item, stuntGridCost);
                            break;
                        case CombatSkillType.FistAndPalm:
                            if (fistAndPalmGridCost > 0) ModifGridCost(item, fistAndPalmGridCost);
                            break;
                        case CombatSkillType.Finger:
                            if (fingerGridCost > 0) ModifGridCost(item, fingerGridCost);
                            break;
                        case CombatSkillType.Leg:
                            if (legGridCost > 0) ModifGridCost(item, legGridCost);
                            break;
                        case CombatSkillType.Throw:
                            if (throwGridCost > 0) ModifGridCost(item, throwGridCost);
                            break;
                        case CombatSkillType.Sword:
                            if (swordGridCost > 0) ModifGridCost(item, swordGridCost);
                            break;
                        case CombatSkillType.Blade:
                            if (bladeGridCost > 0) ModifGridCost(item, bladeGridCost);
                            break;
                        case CombatSkillType.Polearm:
                            if (polearmGridCost > 0) ModifGridCost(item, polearmGridCost);
                            break;
                        case CombatSkillType.Special:
                            if (specialGridCost > 0) ModifGridCost(item, specialGridCost);
                            break;
                        case CombatSkillType.Whip:
                            if (whipGridCost > 0) ModifGridCost(item, whipGridCost);
                            break;
                        case CombatSkillType.ControllableShot:
                            if (controllableShotGridCost > 0) ModifGridCost(item, controllableShotGridCost);
                            break;
                        case CombatSkillType.CombatMusic:
                            if (combatMusicGridCost > 0) ModifGridCost(item, combatMusicGridCost);
                            break;
                        default:
                            //TODO:不知道为什么没有
                            break;
                    }
                }
            }

            Loaded = true;
        }

        /// <summary>
        /// 修改格子
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cost"></param>
        private static void ModifGridCost(CombatSkillItem item, int cost)
        {
            if (item.GridCost > cost)
            {
                typeof(CombatSkillItem).GetField("GridCost").SetValue(item, (sbyte)cost);
            }
        }

        /*[HarmonyPrefix, HarmonyPatch(typeof(CombatDomain), "UpdateSkillNeedMobilityCanUse")]
        public static void CombatDomain_UpdateSkillNeedMobilityCanUse_Patch(CombatCharacter character, ref Dictionary<CombatSkillKey, CombatSkillData> ____selfSkillDataDict, ref Dictionary<CombatSkillKey, CombatSkillData> ____enemySkillDataDict)
        {

            //尝试解决队友功法报错
            CombatSkillCollection combatSkillCollection = (CombatSkillCollection)AccessTools.Field(typeof(CombatSkillDomain), "_combatSkills").GetValue(DomainManager.CombatSkill);
            foreach (CombatSkillKey combatSkillKey in (character.IsAlly ? ____selfSkillDataDict : ____enemySkillDataDict).Keys)
            {
                if (!combatSkillCollection.ContainsKey(combatSkillKey))
                {
                    (character.IsAlly ? ____selfSkillDataDict : ____enemySkillDataDict).Remove(combatSkillKey);
                }
            }

        }*/

        /*[HarmonyPrefix, HarmonyPatch(typeof(CombatDomain), "UpdateSkillCanUse", new Type[] { typeof(DataContext), typeof(CombatCharacter), typeof(short) })]
        public static void CombatDomain_UpdateSkillCanUse_Patch(DataContext context, CombatCharacter character, short skillId, ref Dictionary<CombatSkillKey, CombatSkillData> ____selfSkillDataDict, ref Dictionary<CombatSkillKey, CombatSkillData> ____enemySkillDataDict)
        {
            
            //尝试解决队友功法报错
            CombatSkillCollection combatSkillCollection = (CombatSkillCollection)AccessTools.Field(typeof(CombatSkillDomain), "_combatSkills").GetValue(DomainManager.CombatSkill);
            foreach (CombatSkillKey combatSkillKey in (character.IsAlly ? ____selfSkillDataDict : ____enemySkillDataDict).Keys)
            {
                if (!combatSkillCollection.ContainsKey(combatSkillKey))
                {
                    (character.IsAlly ? ____selfSkillDataDict : ____enemySkillDataDict).Remove(combatSkillKey);
                }
            }
        }*/
    }
}