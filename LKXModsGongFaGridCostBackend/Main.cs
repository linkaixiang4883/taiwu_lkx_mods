using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using Config;
using System.Collections.Generic;
using GameData.Utilities;

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
            AdaptableLog.Info("测试1");
            CombatSkill.Instance.GetAllKeys();
        }

        /// <summary>
        /// patch功法修改
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(CombatSkill), "GetAllKeys")]
        public static void CombatSkill_GetAllKeys_Patch(CombatSkill __instance, ref List<CombatSkillItem> ____dataArray)
        {
            if (Loaded)
            {
                return;
            }
            AdaptableLog.Info("测试2");
            foreach (CombatSkillItem item in ____dataArray)
            {
                AdaptableLog.Info(item.Name + ":" + item.GridCost + "-" + item.GenericGrid);
                /*typeof(CombatSkillItem).GetField("GridCost").SetValue(item, (sbyte)1);
                if (item.Type == GameData.Domains.CombatSkill.CombatSkillType.Neigong)
                {
                    typeof(CombatSkillItem).GetField("GenericGrid").SetValue(item, (sbyte)12);
                }*/
            }

            Loaded = true;
        }
    }
}
