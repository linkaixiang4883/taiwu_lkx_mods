using Config;
using GameData.Domains;
using GameData.Domains.CombatSkill;
using GameData.Utilities;
using HarmonyLib;
using System.Reflection;
using TaiwuModdingLib.Core.Plugin;
using TaiwuModdingLib.Core.Utils;
using CombatSkillType = GameData.Domains.CombatSkill.CombatSkillType;

namespace LKXModsGongFaGridCostBackend
{
    [PluginConfig("LKXModsGongFaGridCostBackend", "LKX", "1.0.56.0")]
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
            //AdaptableLog.Info("执行了init");
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

        private static int targetType; // 0=所有角色, 1=太吾+同道, 2=仅太吾
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

            DomainManager.Mod.GetSetting(ModIdStr, "targetType", ref targetType);

            // 所有角色：直接改 GlobalConfig，全局生效
            if (enableBaseGrid && targetType == 0)
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
            PatchGongfa();
            
        }

        /// <summary>
        /// patch功法修改
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        /// [HarmonyPostfix, HarmonyPatch(typeof(Config.CombatSkill), "GetAllKeys")]
        public static void PatchGongfa()
        {
            if (Loaded)
            {
                return;
            }
            if (!enableGridCost)
            {
                return;
            }
            List<CombatSkillItem> dataArray = Config.CombatSkill.Instance.GetFieldValue("_dataArray", BindingFlags.NonPublic | BindingFlags.Instance) as List<CombatSkillItem>;
            foreach (CombatSkillItem item in dataArray)
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

        #region 太吾/同道生效

        /// <summary>
        /// 内功格子 Postfix — 叠加差值
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(GameData.Domains.Character.Character), "GetCombatSkillSlotCountNeigong")]
        static void PostfixNeigong(GameData.Domains.Character.Character __instance, ref sbyte __result)
        {
            if (!enableBaseGrid) return;
            if (targetType == 0) return; // 所有角色走 GlobalConfig，不需要 Postfix

            bool shouldApply = targetType == 2
                ? __instance.IsTaiwu()
                : __instance.IsTaiwu() || DomainManager.Taiwu.IsInGroup(__instance.GetId());

            if (!shouldApply) return;

            int delta = baseNeigongGrid - 6;
            if (delta <= 0) return;

            __result = (sbyte)System.Math.Min(System.Math.Max(__result + delta, 0), 99);
        }

        /// <summary>
        /// 非内功基础格子 Postfix — 叠加差值
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameData.Domains.Character.Character), "GetCombatSkillBasicSlotCount",
            new System.Type[] { typeof(sbyte), typeof(ArraySegmentList<short>) })]
        static void PostfixBasicSlot(GameData.Domains.Character.Character __instance, sbyte equipType, ref sbyte __result)
        {
            if (!enableBaseGrid) return;
            if (targetType == 0) return; // 所有角色走 GlobalConfig，不需要 Postfix
            if (equipType == 0) return;

            bool shouldApply = targetType == 2
                ? __instance.IsTaiwu()
                : __instance.IsTaiwu() || DomainManager.Taiwu.IsInGroup(__instance.GetId());

            if (!shouldApply) return;

            int delta;
            switch (equipType)
            {
                case 1: delta = baseCuipoGrid - 1; break;
                case 2: delta = baseQingyingGrid - 1; break;
                case 3: delta = baseHutiGrid - 1; break;
                case 4: delta = baseQiqiaoGrid - 1; break;
                default: return;
            }
            if (delta <= 0) return;

            __result = (sbyte)System.Math.Min(System.Math.Max(__result + delta, 0), 99);
        }

        #endregion

    }
}