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
            CombatSkill.Instance.GetAllKeys();
        }

        /// <summary>
        /// 功法格子
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="____dataArray"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(CombatSkill), "GetAllKeys")]
        public static void CombatSkill_GetAllKeys_Patch(CombatSkill __instance, ref List<CombatSkillItem> ____dataArray)
        {
            if (Loaded)
            {
                return;
            }
            foreach (CombatSkillItem item in ____dataArray)
            {
                typeof(CombatSkillItem).GetField("GridCost").SetValue(item, (sbyte)1);
                if (item.Type == GameData.Domains.CombatSkill.CombatSkillType.Neigong)
                {
                    typeof(CombatSkillItem).GetField("GenericGrid").SetValue(item, (sbyte)12);
                }
            }

            Loaded = true;
        }
    }
}
