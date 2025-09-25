using GameData.Common;
using GameData.Domains;
using GameData.Domains.Map;
using GameData.Utilities;
using HarmonyLib;
using System;
using System.Reflection;
using TaiwuModdingLib.Core.Plugin;

namespace LKXModsEnYi
{
    [PluginConfig("LKXModsEnYi", "LKX", "0.0.79.43")]
    public class Run : TaiwuRemakePlugin
    {
        private Harmony harmony;

        private const short FULONG_AREA_TEMPLATE_ID = 29;
        private const short RANSHAN_AREA_TEMPLATE_ID = 22;
        private const short KONGSANG_AREA_TEMPLATE_ID = 25;

        private static short _fulongAreaTemplateId = 29;
        private static short _ranshanAreaTemplateId = 22;
        private static short _kongsangAreaTemplateId = 25;

        //private static Dictionary<short, bool> _areaDict = new Dictionary<short, bool>();

        public override void Dispose()
        {
            if (harmony != null)
            {
                harmony.UnpatchSelf();
                harmony = null;
            }
        }

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(ExtraDomain_SetAreaSpiritualDebt_Patch));
        }

        private static bool enableAll;

        private static bool enableRanShan;
        private static bool enableFuLong;
        private static bool enableKongSangShan;
        public override void OnModSettingUpdate()
        {
            DomainManager.Mod.GetSetting(ModIdStr, "enableAll", ref enableAll);

            DomainManager.Mod.GetSetting(ModIdStr, "enableRanShan", ref enableRanShan);
            DomainManager.Mod.GetSetting(ModIdStr, "enableFuLong", ref enableFuLong);
            DomainManager.Mod.GetSetting(ModIdStr, "enableKongSangShan", ref enableKongSangShan);
        }

        public override void OnLoadedArchiveData()
        {
            base.OnLoadedArchiveData();

            /*List<short> mapAreaKeys = Config.MapArea.Instance.GetAllKeys();
            foreach (short areaKey in mapAreaKeys)
            {
                MapAreaItem item = Config.MapArea.Instance[areaKey];
                AdaptableLog.Info(item.Name + ":" + item.TemplateId);
            }

            AdaptableLog.Info("成功加载存档");*/
        }
        public static void ModifyValue(short areaId, ref int value)
        {
            if (Run.enableAll)
            {
                value = GlobalConfig.Instance.SpiritualDebtLimit[1];
            }

            MapAreaData areaData = DomainManager.Map.GetElement_Areas(areaId);
            if (_fulongAreaTemplateId > 0 && enableFuLong && areaData.GetTemplateId() == _fulongAreaTemplateId)
            {
                //AdaptableLog.Info("设置赤明岛100%恩义。");
                value = GlobalConfig.Instance.SpiritualDebtLimit[1];
            }
            if (_ranshanAreaTemplateId > 0 && enableRanShan && areaData.GetTemplateId() == _ranshanAreaTemplateId)
            {
                //AdaptableLog.Info("设置然山100%恩义。");
                value = GlobalConfig.Instance.SpiritualDebtLimit[1];
            }
            if (_kongsangAreaTemplateId > 0 && enableKongSangShan && areaData.GetTemplateId() == _kongsangAreaTemplateId)
            {
                //AdaptableLog.Info("设置空桑100%恩义。");
                value = GlobalConfig.Instance.SpiritualDebtLimit[1];
            }
        }
    }

    /// <summary>
    /// patch恩义修改1
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="__result"></param>
    [HarmonyPatch]
    public static class ExtraDomain_SetAreaSpiritualDebt_Patch
    {
        static MethodBase TargetMethod()
        {
            var type = typeof(GameData.Domains.Extra.ExtraDomain);

            // 石牢三魔版本
            var m = type.GetMethod("SetAreaSpiritualDebt", new Type[]
            {
                    typeof(GameData.Common.DataContext),
                    typeof(short),
                    typeof(int),
                    typeof(bool),
                    typeof(bool)
            });
            if (m != null) return m;

            // 老版本
            m = type.GetMethod("SetAreaSpiritualDebt", new Type[]
            {
                    typeof(GameData.Common.DataContext),
                    typeof(short),
                    typeof(int),
                    typeof(bool)
            });
            return m;
        }

        static void Prefix(short areaId, ref int value)
        {
            Run.ModifyValue(areaId, ref value);
        }
    }
}
