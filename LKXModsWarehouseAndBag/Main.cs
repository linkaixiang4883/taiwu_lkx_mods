using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Taiwu;
using System;

namespace LKXModsWarehouseAndBag
{
    /// <summary>
    /// 
    /// </summary>
    [PluginConfig("LKXModsWarehouseAndBag", "LKX", "1.0.49.1")]
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

        private static int targetType; // 0=所有角色, 1=太吾+同道, 2=仅太吾
        private static int inventoryCount;
        private static int warehouseInventory;
        private static int resourceCount;

        private const int InventoryMin = 0;
        private const int InventoryMax = 30000;
        private const int WarehouseMin = 0;
        private const int WarehouseMax = 50000;
        private const int ResourceMin = 0;
        private const int ResourceMax = 500000;

        public override void OnModSettingUpdate()
        {
            DomainManager.Mod.GetSetting(ModIdStr, "targetType", ref targetType);

            string invStr = "";
            DomainManager.Mod.GetSetting(ModIdStr, "inventoryCount", ref invStr);
            int.TryParse(invStr, out inventoryCount);

            string whStr = "";
            DomainManager.Mod.GetSetting(ModIdStr, "warehouseInventory", ref whStr);
            int.TryParse(whStr, out warehouseInventory);

            string resStr = "";
            DomainManager.Mod.GetSetting(ModIdStr, "resourceCount", ref resStr);
            int.TryParse(resStr, out resourceCount);

            inventoryCount = Math.Clamp(inventoryCount, InventoryMin, InventoryMax);
            warehouseInventory = Math.Clamp(warehouseInventory, WarehouseMin, WarehouseMax);
            resourceCount = Math.Clamp(resourceCount, ResourceMin, ResourceMax);
        }

        /// <summary>
        /// patch太吾的负重
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(Character), "GetMaxInventoryLoad")]
        public static void Character_GetMaxInventoryLoad_Patch(Character __instance, ref int __result)
        {
            switch (targetType)
            {
                case 0:
                default: // 所有角色
                    __result += (inventoryCount * 100);
                    break;
                case 1: // 太吾+同道
                    if (__instance.IsTaiwu() || DomainManager.Taiwu.IsInGroup(__instance.GetId()))
                        __result += (inventoryCount * 100);
                    break;
                case 2: // 仅太吾
                    if (__instance.IsTaiwu())
                        __result += (inventoryCount * 100);
                    break;
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
