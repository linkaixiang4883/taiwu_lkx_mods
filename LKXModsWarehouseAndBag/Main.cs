using HarmonyLib;
using System;
using TaiwuModdingLib.Core.Plugin;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Taiwu;

namespace LKXModsWarehouseAndBag
{
    /// <summary>
    /// 
    /// </summary>
    [PluginConfig("LKXModsWarehouseAndBag", "LKX", "0.1.0")]
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

        private static bool npcInventoryEnable;
        private static int inventoryCount;
        private static int warehouseInventory;
        private static int resourceCount;
        public override void OnModSettingUpdate()
        {
            DomainManager.Mod.GetSetting(ModIdStr, "npcInventoryEnable", ref npcInventoryEnable);
            DomainManager.Mod.GetSetting(ModIdStr, "inventoryCount", ref inventoryCount);
            DomainManager.Mod.GetSetting(ModIdStr, "warehouseInventory", ref warehouseInventory);
            DomainManager.Mod.GetSetting(ModIdStr, "resourceCount", ref resourceCount);
            Config.CombatSkill.Instance.GetAllKeys();
        }

        /// <summary>
        /// patch太吾的负重
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(Character), "GetMaxInventoryLoad")]
        public static void Character_GetMaxInventoryLoad_Patch(Character __instance, ref int __result)
        {
            if (npcInventoryEnable)
            {
                __result += (inventoryCount * 100);
            } else
            {
                if (__instance.GetId() == DomainManager.Taiwu.GetTaiwuCharId())
                {
                    __result += (inventoryCount * 100);
                }
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
            __result += (warehouseInventory * 100);
        }

        /// <summary>
        /// 资源最大
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(TaiwuDomain), "GetMaterialResourceMaxCount")]
        public static void TaiwuDomain_GetMaterialResourceMaxCount_Patch(ref int __result)
        {
            __result += resourceCount;
        }
    }
}
