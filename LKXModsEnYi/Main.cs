using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using Config;
using System.Collections.Generic;
using GameData.Utilities;
using GameData.Domains;
using GameData.Domains.Combat;
using GameData.Domains.CombatSkill;
using System;
using GameData.Common;
using GameData.Domains.Character;
using GameData.Domains.Map;

namespace LKXModsEnYi
{
    [PluginConfig("LKXModsEnYi", "LKX", "3.1.0.0")]
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
            harmony = Harmony.CreateAndPatchAll(typeof(Run));
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

        /// <summary>
        /// patch恩义修改1
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPrefix, HarmonyPatch(typeof(GameData.Domains.Extra.ExtraDomain), "ChangeAreaSpiritualDebt")]
        public static void ExtraDomain_ChangeAreaSpiritualDebt_Patch(GameData.Domains.Extra.ExtraDomain __instance, DataContext context, short areaId, ref int delta)
        {
            //AdaptableLog.Info("进入ChangeAreaSpiritualDebt方法！");
            //AdaptableLog.Info("delta：" + delta.ToString());
            if (enableAll)
            {
                delta = 1000;
            }

            MapAreaData areaData = DomainManager.Map.GetElement_Areas(areaId);
            if (_fulongAreaTemplateId > 0 && enableFuLong && areaData.GetTemplateId() == _fulongAreaTemplateId)
            {
                //AdaptableLog.Info("设置赤明岛100%恩义。");
                delta = 1000;
            }
            if (_ranshanAreaTemplateId > 0 && enableRanShan && areaData.GetTemplateId() == _ranshanAreaTemplateId)
            {
                //AdaptableLog.Info("设置然山100%恩义。");
                delta = 1000;
            }
            if (_kongsangAreaTemplateId > 0 && enableKongSangShan && areaData.GetTemplateId() == _kongsangAreaTemplateId)
            {
                //AdaptableLog.Info("设置空桑100%恩义。");
                delta = 1000;
            }
        }

        /// <summary>
        /// patch恩义修改2
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPrefix, HarmonyPatch(typeof(GameData.Domains.Extra.ExtraDomain), "SetAreaSpiritualDebt")]
        public static void ExtraDomain_SetAreaSpiritualDebt_Patch(GameData.Domains.Extra.ExtraDomain __instance, DataContext context, short areaId, ref int value)
        {
            //AdaptableLog.Info("进入SetAreaSpiritualDebt方法！");
            //AdaptableLog.Info("delta：" + value.ToString());
            if (enableAll)
            {
                value = 1000;
            }

            MapAreaData areaData = DomainManager.Map.GetElement_Areas(areaId);
            if (_fulongAreaTemplateId > 0 && enableFuLong && areaData.GetTemplateId() == _fulongAreaTemplateId)
            {
                //AdaptableLog.Info("设置赤明岛100%恩义。");
                value = 1000;
            }
            if (_ranshanAreaTemplateId > 0 && enableRanShan && areaData.GetTemplateId() == _ranshanAreaTemplateId)
            {
                //AdaptableLog.Info("设置然山100%恩义。");
                value = 1000;
            }
            if (_kongsangAreaTemplateId > 0 && enableKongSangShan && areaData.GetTemplateId() == _kongsangAreaTemplateId)
            {
                //AdaptableLog.Info("设置空桑100%恩义。");
                value = 1000;
            }

        }

    }
}
