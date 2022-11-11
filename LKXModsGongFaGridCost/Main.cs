using Config;
using Config.Common;
using GameData.Domains.CombatSkill;
using HarmonyLib;
using System.Collections.Generic;
using TaiwuModdingLib.Core.Plugin;
using CombatSkillType = GameData.Domains.CombatSkill.CombatSkillType;

namespace LKXModsGongFaGridCost
{
    [PluginConfig("LKXModsGongFaGridCost", "LKX", "0.13.0")]
    public class Run : TaiwuRemakePlugin
    {
        private Harmony harmony;

        /// <summary>
        /// 避免多次处理
        /// </summary>
        public static bool Loaded = false;

        /// <summary>
        /// Mod被关闭
        /// </summary>
        public override void Dispose()
        {
            // Mod被关闭时
            if (harmony != null)
            {
                harmony.UnpatchSelf();
                Loaded = false;
                harmony = null;
            }
        }

        /// <summary>
        /// Mod初始化
        /// </summary>
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
            ModManager.GetSetting(ModIdStr, "enableGridCost", ref enableGridCost);
            ModManager.GetSetting(ModIdStr, "enableAllGridCost", ref enableAllGridCost);
            ModManager.GetSetting(ModIdStr, "gridCost", ref gridCost);

            ModManager.GetSetting(ModIdStr, "neigongGridCost", ref neigongGridCost);
            ModManager.GetSetting(ModIdStr, "posingGridCost", ref posingGridCost);
            ModManager.GetSetting(ModIdStr, "stuntGridCost", ref stuntGridCost);
            ModManager.GetSetting(ModIdStr, "fistAndPalmGridCost", ref fistAndPalmGridCost);
            ModManager.GetSetting(ModIdStr, "fingerGridCost", ref fingerGridCost);
            ModManager.GetSetting(ModIdStr, "legGridCost", ref legGridCost);
            ModManager.GetSetting(ModIdStr, "throwGridCost", ref throwGridCost);
            ModManager.GetSetting(ModIdStr, "swordGridCost", ref swordGridCost);
            ModManager.GetSetting(ModIdStr, "bladeGridCost", ref bladeGridCost);
            ModManager.GetSetting(ModIdStr, "polearmGridCost", ref polearmGridCost);
            ModManager.GetSetting(ModIdStr, "specialGridCost", ref specialGridCost);
            ModManager.GetSetting(ModIdStr, "whipGridCost", ref whipGridCost);
            ModManager.GetSetting(ModIdStr, "controllableShotGridCost", ref controllableShotGridCost);
            ModManager.GetSetting(ModIdStr, "combatMusicGridCost", ref combatMusicGridCost);

            ModManager.GetSetting(ModIdStr, "enableBaseGrid", ref enableBaseGrid);
            ModManager.GetSetting(ModIdStr, "baseNeigongGrid", ref baseNeigongGrid);
            ModManager.GetSetting(ModIdStr, "baseCuipoGrid", ref baseCuipoGrid);
            ModManager.GetSetting(ModIdStr, "baseQingyingGrid", ref baseQingyingGrid);
            ModManager.GetSetting(ModIdStr, "baseHutiGrid", ref baseHutiGrid);
            ModManager.GetSetting(ModIdStr, "baseQiqiaoGrid", ref baseQiqiaoGrid);

            CombatSkill.Instance.GetAllKeys();
            if (enableBaseGrid)
            {
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
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="____dataArray"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(CombatSkill), "GetAllKeys")]
        public static void PatchSkill(ref List<CombatSkillItem> ____dataArray)
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
                            if(neigongGridCost > 0) ModifGridCost(item, neigongGridCost);
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
    }
}