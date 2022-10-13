using Config;
using Config.Common;
using GameData.Domains.CombatSkill;
using HarmonyLib;
using System.Collections.Generic;
using TaiwuModdingLib.Core.Plugin;

namespace LKXModsGongFaGridCost
{
    [PluginConfig("LKXModsGongFaGridCost", "LKX", "0.2.3")]
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
            ModManager.GetSetting(ModIdStr, "enableGridCost", ref enableGridCost);
            ModManager.GetSetting(ModIdStr, "enableBaseGrid", ref enableBaseGrid);
            //ModManager.GetSetting(ModIdStr, "enableGenericGrid", ref enableGenericGrid);
            ModManager.GetSetting(ModIdStr, "gridCost", ref gridCost);
            //ModManager.GetSetting(ModIdStr, "genericGrid", ref genericGrid);
            ModManager.GetSetting(ModIdStr, "baseNeigongGrid", ref baseNeigongGrid);
            ModManager.GetSetting(ModIdStr, "baseCuipoGrid", ref baseCuipoGrid);
            ModManager.GetSetting(ModIdStr, "baseQingyingGrid", ref baseQingyingGrid);
            ModManager.GetSetting(ModIdStr, "baseHutiGrid", ref baseHutiGrid);
            ModManager.GetSetting(ModIdStr, "baseQiqiaoGrid", ref baseQiqiaoGrid);

            CombatSkill.Instance.GetAllKeys();
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
            foreach (CombatSkillItem item in ____dataArray)
            {
                if (enableGridCost && item.GridCost > gridCost)
                {
                    typeof(CombatSkillItem).GetField("GridCost").SetValue(item, (sbyte)gridCost);
                }
            }

            Loaded = true;
        }
    }
}