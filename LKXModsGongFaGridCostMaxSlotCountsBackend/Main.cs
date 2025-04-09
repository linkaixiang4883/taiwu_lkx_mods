using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using CombatSkillHelper = GameData.Domains.Character.CombatSkillHelper;

namespace LKXModsGongFaGridCostMaxSlotCountsBackend
{
    [PluginConfig("LKXModsGongFaGridCostMaxSlotCountsBackend", "LKX", "0.0.76.30")]
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
            AdaptableLog.Info("执行加载。");
            AdaptableLog.Error("执行加载。");
            Console.WriteLine("执行加载！");
        }

        private static bool enableBaseGrid;
        private static int baseNeigongGrid;
        private static int baseCuipoGrid;
        private static int baseQingyingGrid;
        private static int baseHutiGrid;
        private static int baseQiqiaoGrid;
        public override void OnModSettingUpdate()
        {
            Loaded = false;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatSkillHelper), nameof(CombatSkillHelper.MaxSlotCounts), MethodType.Getter)]
        public static void MaxSlotCounts_Patch(ref sbyte[] __result)
        {
            AdaptableLog.Info("执行修改。" + __result.ToString());
            __result = new sbyte[] { 99, 99, 99, 99, 99 };
        }
    }
}
