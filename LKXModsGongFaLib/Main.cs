using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.CombatSkill;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using CharacterHelper = GameData.Domains.Character.CombatSkillHelper;

namespace LKXModsGongFaLib
{
    [PluginConfig("LKXModsGongFaLib", "LKX", "1.0.49.0")]
    public class Run : TaiwuRemakePlugin
    {
        private Harmony harmony;

        /// <summary>日志前缀，包含版本号，方便排查问题</summary>
        private const string LogPrefix = "[LKXModsGongFaLib v1.0.49.0]";

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
            AdaptableLog.Info(LogPrefix + " 初始化完成");
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
            DomainManager.Mod.GetSetting(ModIdStr, "enableMaxSlot", ref enableMaxSlot);

            DomainManager.Mod.GetSetting(ModIdStr, "neigongMaxSlot", ref neigongMaxSlot);
            DomainManager.Mod.GetSetting(ModIdStr, "cuipoMaxSlot", ref cuipoMaxSlot);
            DomainManager.Mod.GetSetting(ModIdStr, "qingyingMaxSlot", ref qingyingMaxSlot);
            DomainManager.Mod.GetSetting(ModIdStr, "hutiMaxSlot", ref hutiMaxSlot);
            DomainManager.Mod.GetSetting(ModIdStr, "qiqiaoMaxSlot", ref qiqiaoMaxSlot);

            // 限制范围
            neigongMaxSlot = Math.Clamp(neigongMaxSlot, MinSlot, MaxSlot);
            cuipoMaxSlot = Math.Clamp(cuipoMaxSlot, MinSlot, MaxSlot);
            qingyingMaxSlot = Math.Clamp(qingyingMaxSlot, MinSlot, MaxSlot);
            hutiMaxSlot = Math.Clamp(hutiMaxSlot, MinSlot, MaxSlot);
            qiqiaoMaxSlot = Math.Clamp(qiqiaoMaxSlot, MinSlot, MaxSlot);

            if (enableMaxSlot)
            {
                ApplyMaxSlotCounts();
            }
        }

        /// <summary>
        /// 修改 MaxSlotCounts 数组
        /// </summary>
        private static void ApplyMaxSlotCounts()
        {
            var maxSlotCounts = GameData.Domains.Character.CombatSkillHelper.MaxSlotCounts;
            maxSlotCounts[CombatSkillEquipType.Neigong] = (sbyte)neigongMaxSlot;
            maxSlotCounts[CombatSkillEquipType.Attack] = (sbyte)cuipoMaxSlot;
            maxSlotCounts[CombatSkillEquipType.Agile] = (sbyte)qingyingMaxSlot;
            maxSlotCounts[CombatSkillEquipType.Defense] = (sbyte)hutiMaxSlot;
            maxSlotCounts[CombatSkillEquipType.Assist] = (sbyte)qiqiaoMaxSlot;
        }

        #region 数组越界保护

        /// <summary>
        /// MaxSlotCounts 改大后，Record/Equal 调用 GetEquippedSkill 时
        /// SlotBeginIndexes + index 可能超过 short[48] 长度，越界时返回 -1
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

        /// <summary>
        /// ConvertPartialSegmentList 用 MaxSlotCounts 当长度从 short[48] 切片，
        /// offset + length 可能超过数组边界。加 min 保护。
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CombatSkillEquipment), "ConvertPartialSegmentList")]
        static bool PrefixConvertPartialSegmentList(short[] equippedSkills, sbyte equipType, ref ArraySegmentList<short> __result)
        {
            sbyte offset = CharacterHelper.SlotBeginIndexes[(int)equipType];
            sbyte maxLength = CharacterHelper.MaxSlotCounts[(int)equipType];
            // 用 SlotEndIndexes 做安全边界，防止视图跨区段读到相邻类型的数据
            int safeLength = Math.Min((int)maxLength, (int)CharacterHelper.SlotEndIndexes[(int)equipType] - (int)offset);

            if (safeLength <= 0)
            {
                __result = new ArraySegmentList<short>();
                return false;
            }
            int count = CombatSkillEquipmentGetNextIndex(equippedSkills, (int)offset, safeLength);
            __result = new ArraySegmentList<short>(equippedSkills, (int)offset, safeLength, count, new short?((short)(-1)));
            return false;
        }

        /// <summary>
        /// 反射调用 private static GetNextIndex(short[], int, int)
        /// </summary>
        private static int CombatSkillEquipmentGetNextIndex(short[] array, int offset, int maxLength)
        {
            for (int i = 0; i < maxLength; i++)
            {
                if (array[offset + i] < 0)
                    return i;
            }
            return maxLength;
        }

        #endregion
    }
}
