using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using CharacterHelper = GameData.Domains.Character.CombatSkillHelper;
using GameData.Domains.Character;
using GameData.Utilities;

namespace LKXModsGongFaLibFrontend
{
    [PluginConfig("LKXModsGongFaLibFrontend", "LKX", "1.0.49.0")]
    public class Run : TaiwuRemakePlugin
    {
        private Harmony harmony;

        /// <summary>日志前缀，包含版本号，方便排查问题</summary>
        private const string LogPrefix = "[LKXModsGongFaLibFrontend v1.0.49.0]";

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

        #region 配置项

        private static bool enableMaxSlot;
        private static int neigongMaxSlot = 9;
        private static int cuipoMaxSlot = 9;
        private static int qingyingMaxSlot = 9;
        private static int hutiMaxSlot = 9;
        private static int qiqiaoMaxSlot = 9;

        private const int MinSlot = 9;
        private const int MaxSlot = 99;

        #endregion

        public override void OnModSettingUpdate()
        {
            ModManager.GetSetting(ModIdStr, "enableMaxSlot", ref enableMaxSlot);

            ModManager.GetSetting(ModIdStr, "neigongMaxSlot", ref neigongMaxSlot);
            ModManager.GetSetting(ModIdStr, "cuipoMaxSlot", ref cuipoMaxSlot);
            ModManager.GetSetting(ModIdStr, "qingyingMaxSlot", ref qingyingMaxSlot);
            ModManager.GetSetting(ModIdStr, "hutiMaxSlot", ref hutiMaxSlot);
            ModManager.GetSetting(ModIdStr, "qiqiaoMaxSlot", ref qiqiaoMaxSlot);

            neigongMaxSlot = System.Math.Max(System.Math.Min(neigongMaxSlot, MaxSlot), MinSlot);
            cuipoMaxSlot = System.Math.Max(System.Math.Min(cuipoMaxSlot, MaxSlot), MinSlot);
            qingyingMaxSlot = System.Math.Max(System.Math.Min(qingyingMaxSlot, MaxSlot), MinSlot);
            hutiMaxSlot = System.Math.Max(System.Math.Min(hutiMaxSlot, MaxSlot), MinSlot);
            qiqiaoMaxSlot = System.Math.Max(System.Math.Min(qiqiaoMaxSlot, MaxSlot), MinSlot);

            if (enableMaxSlot)
            {
                ApplyMaxSlotCounts();
            }
        }

        /// <summary>
        /// 修改前端(Managed版)的 MaxSlotCounts 数组
        /// </summary>
        private static void ApplyMaxSlotCounts()
        {
            var maxSlotCounts = GameData.Domains.Character.CombatSkillHelper.MaxSlotCounts;
            maxSlotCounts[0] = (sbyte)neigongMaxSlot;
            maxSlotCounts[1] = (sbyte)cuipoMaxSlot;
            maxSlotCounts[2] = (sbyte)qingyingMaxSlot;
            maxSlotCounts[3] = (sbyte)hutiMaxSlot;
            maxSlotCounts[4] = (sbyte)qiqiaoMaxSlot;
        }

        #region 数组越界保护（前端）

        /// <summary>
        /// 前端 ConvertPartialSegmentList 用 MaxSlotCounts 当长度从 short[48] 切片，
        /// offset + length 可能超过数组边界，导致视图重叠（技能同时出现在多个栏）
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CombatSkillEquipment), "ConvertPartialSegmentList")]
        static bool PrefixConvertPartialSegmentList(short[] equippedSkills, sbyte equipType, ref ArraySegmentList<short> __result)
        {
            sbyte offset = CharacterHelper.SlotBeginIndexes[(int)equipType];
            sbyte maxLength = CharacterHelper.MaxSlotCounts[(int)equipType];
            // 用 SlotEndIndexes 做安全边界，防止视图跨区段读到相邻类型的数据
            int safeLength = System.Math.Min((int)maxLength, (int)CharacterHelper.SlotEndIndexes[(int)equipType] - (int)offset);

            if (safeLength <= 0)
            {
                __result = new ArraySegmentList<short>();
                return false;
            }
            int count = CombatSkillEquipmentGetNextIndex(equippedSkills, (int)offset, safeLength);
            __result = new ArraySegmentList<short>(equippedSkills, (int)offset, safeLength, count, new short?((short)(-1)));
            return false;
        }

        private static int CombatSkillEquipmentGetNextIndex(short[] array, int offset, int maxLength)
        {
            for (int i = 0; i < maxLength; i++)
            {
                if (array[offset + i] < 0)
                    return i;
            }
            return maxLength;
        }

        /// <summary>
        /// 前端 GetEquippedSkill 越界保护（与后端一致）
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CharacterHelper), "GetEquippedSkill")]
        static bool PrefixGetEquippedSkill(short[] equippedSkills, sbyte equipType, sbyte index, ref short __result)
        {
            int realIndex = CharacterHelper.SlotBeginIndexes[(int)equipType] + (int)index;
            // SlotEndIndexes 边界：防止 MaxSlotCounts 改大后 Record() 跨类型读取
            if (realIndex >= equippedSkills.Length || realIndex >= CharacterHelper.SlotEndIndexes[(int)equipType])
            {
                __result = -1;
                return false;
            }
            return true;
        }

        #endregion
    }
}
