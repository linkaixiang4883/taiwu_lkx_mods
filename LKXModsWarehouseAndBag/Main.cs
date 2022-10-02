using HarmonyLib;
using System;
using TaiwuModdingLib.Core.Plugin;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Taiwu;

namespace LKXModsWarehouseAndBag
{
    [PluginConfig("LKXModsWarehouseAndBag", "LKX", "0.0.1")]
    public class Run : TaiwuRemakePlugin
    {
        private Harmony harmony;
        public override void Dispose()
        {
            // Mod被关闭时
            if (harmony != null)
            {
                harmony.UnpatchSelf();
                harmony = null;
            }
        }

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(Run));
        }

        /// <summary>
        /// patch太吾的负重
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(Character), "GetMaxInventoryLoad")]
        public static void Character_GetMaxInventoryLoad_Patch(Character __instance, ref int __result)
        {
            if (__instance.GetId() == DomainManager.Taiwu.GetTaiwuCharId())
            {
                __result += 1000 * 100;
            }
        }

        /// <summary>
        /// 仓库负重
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(TaiwuDomain), "GetWarehouseMaxLoad")]
        public static void TaiwuDomain_GetWarehouseMaxLoad_Patch(ref int __result)
        {
            __result += (10000 * 100);
        }

        /// <summary>
        /// 资源最大
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(TaiwuDomain), "GetMaterialResourceMaxCount")]
        public static void TaiwuDomain_GetMaterialResourceMaxCount_Patch(ref int __result)
        {
            __result += 50000;
        }
    }
}
