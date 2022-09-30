using Config;
using HarmonyLib;
using System.Collections.Generic;
using TaiwuModdingLib.Core.Plugin;

namespace LKXModsGongFaGridCost
{
    [PluginConfig("LKXModsGongFaGridCost", "LKX", "0.0.1")]
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
            //执行
            CombatSkill.Instance.GetAllKeys();

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