// dotnet $DOTNET_CSC_DLL -nologo -t:library -r:"../../The Scroll of Taiwu_Data/Managed/System.dll" -r:"../../The Scroll of Taiwu_Data/Managed/0Harmony.dll" -r:"../../The Scroll of Taiwu_Data/Managed/mscorlib.dll" -r:"../../The Scroll of Taiwu_Data/Managed/Assembly-CSharp.dll" -r:"../../The Scroll of Taiwu_Data/Managed/TaiwuModdingLib.dll" -r:"../../The Scroll of Taiwu_Data/Managed/Unity.TextMeshPro.dll" -r:"../../The Scroll of Taiwu_Data/Managed/UnityEngine.CoreModule.dll" -r:"../../The Scroll of Taiwu_Data/Managed/UnityEngine.UI.dll" -r:"../../The Scroll of Taiwu_Data/Managed/UnityEngine.dll" -optimize -deterministic NeutronFrontend.cs -out:NeutronFrontend.dll
// -r:"../../The Scroll of Taiwu_Data/Managed/Mono.Cecil.dll" -r:"../../The Scroll of Taiwu_Data/Managed/System.Core.dll"   -r:"../../The Scroll of Taiwu_Data/Managed/System.Composition.AttributedModel.dll"
/**
 *  Neutron's Taiwu Collections
 *  Copyright (C) 2022 Neutron3529
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License as
 *  published by the Free Software Foundation, either version 3 of the
 *  License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
#define FRONTEND
#define Fix_0_0_20

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using HarmonyLib;

[assembly: AssemblyVersion("0.0.0.3529")]
namespace ASmallCollectionsFromNeutron
{
    [TaiwuModdingLib.Core.Plugin.PluginConfig("ASmallCollectionsFromNeutron", "Neutron3529", "0.4.1")]
#if TaiwuRemakeHarmonyPlugin
public class ASmallCollectionsFromNeutron : TaiwuModdingLib.Core.Plugin.TaiwuRemakeHarmonyPlugin {
    // 应该干掉这里的初始化函数，不过不干掉也差不多，反正初始化之后会直接执行更新参数，把全部初始化的补丁unpatch掉
#else
    public class ASmallCollectionsFromNeutron : TaiwuModdingLib.Core.Plugin.TaiwuRemakePlugin
    {
        public ASmallCollectionsFromNeutron()
        {
            this.HarmonyInstance = new HarmonyLib.Harmony("TaiwuRemake.Plugin.Neutron3529.ASmallCollectionsFromNeutron");
        }
        public Harmony HarmonyInstance;
#endif
        public override void Initialize()
        {
            //this.OnModSettingUpdate(); 每次游戏加载之后会自动调用更新参数的patch
            //GameData.Domains.Mod.ModInfo modInfo;
            //((Dictionary<string, GameData.Domains.Mod.ModInfo>)(typeof(ModManager).GetField("_localMods",(BindingFlags)(-1))).GetValue(null)).TryGetValue(this.ModIdStr, out modInfo);
            //modInfo.ModSettingEntries.Add(new FrameWork.ModSystem.ToggleSetting("key", "测试", "说明", false));
        }
        public override void Dispose()
        {
            this.HarmonyInstance.UnpatchSelf();
            //modInfo.ModSettingEntries.
        }
        public static bool dirty = false;
        public static int ciTangAdd = 120;
        public static int mapSight = 5;
        public static int favorStr = 1;
        public static int N17 = 0;
        public static string InActiveGongFaColor = "808080";

        public void buildingMaxLv(string key)
        {
            //building_maxLv.Clear();
            string Additionals = "";
            ModManager.GetSetting(this.ModIdStr, key, ref Additionals);
            if (Additionals.Length > 0) try
                {
                    foreach (string gc in Additionals.Split(','))
                    {
                        var tst = gc.Split('=');
                        sbyte count = (sbyte)int.Parse(tst[1].Trim());
                        short bonusType = -1;
                        var id = tst[0].Trim();
                        try
                        {
                            bonusType = (short)int.Parse(id);
                        }
                        catch
                        {
                            foreach (var x in Config.BuildingBlock.Instance)
                            {
                                if (x.Name == id)
                                {
                                    bonusType = x.TemplateId;
                                    break;
                                }
                            }
                        }
                        if (bonusType >= 0) typeof(Config.BuildingBlockItem).GetField("MaxLevel", (BindingFlags)(-1)).SetValue(Config.BuildingBlock.Instance[bonusType], (sbyte)count);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("建筑名称字符串（" + key + "）值有误，请修改字符串格式，或者将字符串置空\n请注意，这个错误的发生会导致部分建筑数据被修改，如果希望使用原版数据，需重启游戏。\n错误信息为：\n" + e.Message);
                }
        }

        private bool enable(string key)
        {
            bool enable = false;
            if (key == "N09S")
            {
                ModManager.GetSetting(this.ModIdStr, key, ref ciTangAdd);
                return ciTangAdd != 0;
            }
            else if (key == "N14_2")
            {
                ModManager.GetSetting(this.ModIdStr, key, ref mapSight);
                return mapSight > 0;
            }
            else if (key == "N17")
            {
                ModManager.GetSetting(this.ModIdStr, key, ref N17);
                return N17 != 3;
            }
            else if (key == "N19")
            {
                ModManager.GetSetting(this.ModIdStr, key, ref favorStr);
                return favorStr > 0;
            }
            else if (key == "N23")
            {
                ModManager.GetSetting(this.ModIdStr, key, ref InActiveGongFaColor);
                if (InActiveGongFaColor == null || InActiveGongFaColor.Length == 6) InActiveGongFaColor = "#808080";
                return InActiveGongFaColor.Length == 7;
            }
            else if (key == "N37")
            {
                buildingMaxLv(key);
            }
            ModManager.GetSetting(this.ModIdStr, key, ref enable);
            return enable;
        }
        public override void OnModSettingUpdate()
        {
            this.HarmonyInstance.UnpatchSelf();
            if (enable("NForcePatch")) this.HarmonyInstance.PatchAll(typeof(Alive));
            if (enable("NForceOFF")) { return; }
            if (enable("N00")) this.HarmonyInstance.PatchAll(typeof(RestrictionsMustDie));
            if (enable("N08")) this.HarmonyInstance.PatchAll(typeof(CatchAll));
            if (enable("N09")) this.HarmonyInstance.PatchAll(typeof(CiTang_NoAgeRestrict));
            if (enable("N09S")) this.HarmonyInstance.PatchAll(typeof(CiTang_BetterEffect_NoAuthCost));
            if (enable("N11_2")) this.HarmonyInstance.PatchAll(typeof(Speedup)); // display only
            if (enable("N14_2")) this.HarmonyInstance.PatchAll(typeof(RevealMap));
            if (enable("N15")) this.HarmonyInstance.PatchAll(typeof(TraitCost));
            if (enable("N17")) this.HarmonyInstance.PatchAll(typeof(RevealHealth));
            if (enable("N18")) this.HarmonyInstance.PatchAll(typeof(CricketAttackFirst));
            if (enable("N19")) this.HarmonyInstance.PatchAll(typeof(AppendFavor));
            if (enable("N20")) this.HarmonyInstance.PatchAll(typeof(AppendCharm));
            if (enable("N21")) this.HarmonyInstance.PatchAll(typeof(TransGenderGetCharm));
            if (enable("N22")) this.HarmonyInstance.PatchAll(typeof(FastSave));
            if (enable("N23")) this.HarmonyInstance.PatchAll(typeof(ShowCombat));
            if (enable("N24"))
            {
                this.HarmonyInstance.PatchAll(typeof(BuildResource));
                //            for(short i=0;i<Config.BuildingBlock.Instance.Count,i++){
                //                Config.BuildingBlock.Instance[i].
                //            }
            }
            if (enable("N26")) this.HarmonyInstance.PatchAll(typeof(AutoFast));
            if (enable("N32")) this.HarmonyInstance.PatchAll(typeof(UnlockShow));
            //        if(enable("N33"))this.HarmonyInstance.PatchAll(typeof(Printing));
            if (enable("N34")) this.HarmonyInstance.PatchAll(typeof(FastCricketDeploy));
            if (enable("N35")) this.HarmonyInstance.PatchAll(typeof(Showququ));
            enable("N37");
        }
        [HarmonyPatch(typeof(UI_ModPanel), "OnModEntryRender")]
        public class Alive
        {
            public static void Postfix(UI_ModPanel __instance, int index, Refers refers, List<GameData.Domains.Mod.ModId> ____modIdList, InfinityScroll ____modsScroll)
            {
                GameData.Domains.Mod.ModId modId = ____modIdList[index];
                GameData.Domains.Mod.ModInfo modInfo = ModManager.GetModInfo(modId);
                if (modInfo.Author == "Neutron3529" && modInfo.Title == "<color=#888800>平衡</color>与<color=#008888>不平衡</color>的模组集")
                {
                    CToggle ctoggle = refers.CGet<CToggle>("IsModEnabledToggle");
                    typeof(UnityEngine.UI.Toggle).GetField("m_IsOn", (BindingFlags)(-1)).SetValue((UnityEngine.UI.Toggle)ctoggle, true);
                }
            }
        }
        // [HarmonyPatch(typeof(ModManager),"LoadMod")]
        // public class Alive {
        // public static bool Prefix(GameData.Domains.Mod.ModInfo modInfo){
        // if(modInfo.Author=="Neutron3529"){
        // return false;
        // }
        // return true;
        // }

        //[HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "CanBuild")]
        [HarmonyPatch(typeof(UI_BuildingOverview), "InitData")]
        public class BuildResource
        {
            public static void Postfix(Dictionary<EBuildingBlockClass, List<Config.BuildingBlockItem>> ____buildingMap)
            {
                Config.BuildingBlock.Instance.Iterate(delegate (Config.BuildingBlockItem item)
                {
                    if (item.Class == EBuildingBlockClass.BornResource)
                    {
                        ____buildingMap[EBuildingBlockClass.Resource].Add(item);
                    }
                    else if (item.Class == EBuildingBlockClass.Function || item.Class == EBuildingBlockClass.Static /*// 超高概率红字*/)
                    {
                        ____buildingMap[EBuildingBlockClass.Kungfu].Add(item);
                    }
                    return true;
                });
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(UI_BuildingManage), "UpdateToggles")]
            public static void Postfix(CToggleGroup ____pageTogGroup, Config.BuildingBlockItem ____configData, bool ____isAtTaiwuVillage)
            {
                ____pageTogGroup.Get(3).gameObject.SetActive(/* isBuilding &&  */____isAtTaiwuVillage);
                //this._pageTogGroup.Get(3).interactable = this._isBuildingManagementUnlocked && this._blockData.Level < this._configData.MaxLevel && (this._blockData.OperationType == 1 || this._blockData.OperationType == -1);
                ____pageTogGroup.Get(4).gameObject.SetActive(____configData.Class != EBuildingBlockClass.Static /* && this._configData.OperationTotalProgress[2] >= 0 */&& ____isAtTaiwuVillage);
            }
        }

        [HarmonyPatch(typeof(MouseTipLifeRecords), "RenderAllRecords")]// dead code
        public class FloatInformation
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Callvirt, typeof(TMPro.TextMeshProUGUI).GetMethod("SetText", new Type[] { typeof(string), typeof(bool) }))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldarg_0),
                            new CodeInstruction(OpCodes.Ldfld, typeof(MouseTipLifeRecords).GetField("_charId", (BindingFlags)(-1))),
                            new CodeInstruction(OpCodes.Ldarg_0)
                        ).SetAndAdvance(
                            OpCodes.Call, typeof(FloatInformation).GetMethod("More")
                        ).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ret)
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
            public static void More(TMPro.TextMeshProUGUI desc, string content, bool T, int charId, MouseTipLifeRecords instance)
            {
                instance.AsynchMethodCall<int, int>(13, 2, charId, 5, delegate (int offset, GameData.Utilities.RawDataPool dataPool)
                {
                    desc.SetText(content, T);
                    if (instance.Element != null)
                    {
                        instance.Element.ShowAfterRefresh();
                    }
                });
            }
        }
        [HarmonyPatch(typeof(MouseTipCombatSkill), "OnGetSkillDisplayData")]
        public class ShowCombat
        {
            public static MethodInfo UpdateSpecialEffectText = typeof(MouseTipCombatSkill).GetMethod("UpdateSpecialEffectText", (BindingFlags)(-1));
            public static void Prefix(MouseTipCombatSkill __instance, Config.CombatSkillItem ____configData)
            {
#if Fix_0_0_20
                if (UpdateSpecialEffectText == null)
                {
                    __instance.CGet<TMPro.TextMeshProUGUI>("DirectEffectDesc").text = " <color=" + InActiveGongFaColor + ">     " + Config.SpecialEffect.Instance[____configData.DirectEffectID].Desc[0] + "</color>";
                    __instance.CGet<TMPro.TextMeshProUGUI>("ReverseEffectDesc").text = " <color=" + InActiveGongFaColor + ">     " + Config.SpecialEffect.Instance[____configData.ReverseEffectID].Desc[0] + "</color>";
                    return;
                }
#endif
                UpdateSpecialEffectText.Invoke(__instance, new object[] { __instance.CGet<TMPro.TextMeshProUGUI>("DirectEffectDesc"), "<color=" + InActiveGongFaColor + ">" + Config.SpecialEffect.Instance[____configData.DirectEffectID].Desc[0] + "</color>" });
                UpdateSpecialEffectText.Invoke(__instance, new object[] { __instance.CGet<TMPro.TextMeshProUGUI>("ReverseEffectDesc"), "<color=" + InActiveGongFaColor + ">" + Config.SpecialEffect.Instance[____configData.ReverseEffectID].Desc[0] + "</color>" });
            }
            public static void Postfix(MouseTipCombatSkill __instance, Config.CombatSkillItem ____configData)
            {
                if (!__instance.CGet<UnityEngine.GameObject>("SpecialEffect").activeSelf)
                {
                    __instance.CGet<UnityEngine.GameObject>("SpecialEffect").SetActive(true);
                    ShowCombat.Prefix(__instance, ____configData);
                }
                __instance.CGet<UnityEngine.GameObject>("DirectEffectTitle").SetActive(true);
                __instance.CGet<UnityEngine.GameObject>("DirectDesc").SetActive(true);
                __instance.CGet<UnityEngine.GameObject>("ReverseEffectTitle").SetActive(true);
                __instance.CGet<UnityEngine.GameObject>("ReverseDesc").SetActive(true);
            }
        }
        [HarmonyPatch(typeof(UI_Bottom), "DoAdvance")]
        public class FastSave
        {
            public static void Postfix()
            {
                if (SingletonObject.getInstance<TimeManager>().GetLeftDaysInCurrMonth() > 0)
                {
                    DialogCmd dialogCmd4 = new DialogCmd
                    {
                        Title = "快速存档",
                        Content = "即将使用GM面板的快速存档功能，是否确认？",
                        Type = 1,
                        Yes = GMFunc.Save
                    };
                    UIElement.Dialog.SetOnInitArgs(FrameWork.EasyPool.Get<FrameWork.ArgumentBox>().SetObject("Cmd", dialogCmd4));
                    UIManager.Instance.ShowUI(UIElement.Dialog);
                }
            }
        }
        [HarmonyPatch(typeof(AvatarAdjustController), "SetTransGender")]
        public class TransGenderGetCharm
        {
            public static void Postfix(AvatarAdjustController __instance, short ____age)
            {
                //var ct=__instance.AvatarData.GetCharm(____age,__instance.AvatarData.ClothDisplayId).ToString();
                var cx = __instance.AvatarData.CalCharmRate().ToString();
                var c0 = __instance.AvatarData.GetBaseCharm().ToString();
                var c1 = __instance.AvatarData.GetEyebrowsCharm().ToString();
                var c2 = __instance.AvatarData.GetEyesCharm().ToString();
                var c3 = __instance.AvatarData.GetNoseCharm().ToString();
                var c4 = __instance.AvatarData.GetMouthCharm().ToString();
                var c5 = __instance.AvatarData.GetFeatureCharm().Item1.ToString();
                var f0 = __instance.AvatarData.Feature1Id.ToString();
                var f1 = __instance.AvatarData.Beard1Id.ToString();
                var f2 = __instance.AvatarData.Beard2Id.ToString();
                var f3 = __instance.AvatarData.Wrinkle1Id.ToString();
                var f4 = __instance.AvatarData.Wrinkle2Id.ToString();
                var f5 = __instance.AvatarData.Wrinkle3Id.ToString();
                var m0 = __instance.AvatarData.MouthId.ToString();
                var m1 = __instance.AvatarData.MouthHeightPercent.ToString();
                var n0 = __instance.AvatarData.NoseId.ToString();
                var n1 = __instance.AvatarData.NoseHeightPercent.ToString();
                var e0 = __instance.AvatarData.EyebrowId.ToString();
                var e1 = __instance.AvatarData.EyebrowHeight.ToString();
                var e2 = __instance.AvatarData.EyebrowAngle.ToString();
                var e3 = __instance.AvatarData.EyebrowDistancePercent.ToString();
                var e4 = __instance.AvatarData.EyesMainId.ToString();
                var e5 = __instance.AvatarData.EyesHeightPercent.ToString();
                var e6 = __instance.AvatarData.EyesAngle.ToString();
                var e7 = __instance.AvatarData.EyesDistancePercent.ToString();
                var bd = __instance.AvatarData.AvatarId;
                DialogCmd dialogCmd4 = new DialogCmd
                {
                    Title = "魅力展示",
                    Content = ("\n\n\n\n\n\n当前人物体形id为" + bd + "，魅力如下：\n  <color=#AttractionType_Godlike>    基础魅力：" + c0 + "\n        眉毛魅力：" + c1 + "\n        眼睛魅力：" + c2 + "\n        鼻子魅力：" + c3 + "\n        嘴巴魅力：" + c4 + "\n        特征魅力：" + c5 + "\n    魅力比率：" + cx + "\n      特征，胡须与皱纹：" + f0 + " " + f1 + " " + f2 + " " + f3 + " " + f4 + " " + f5 + "\n      嘴与id和比例:" + m0 + " " + m1 + "\n      鼻子的:" + n0 + " " + n1 + "\n      眉毛id，高度，角度，间距" + e0 + " " + e1 + " " + e2 + " " + e3 + "\n      眼睛id，高度，角度，间距" + e4 + " " + e5 + " " + e6 + " " + e7 + "\n</color>").ColorReplace(),
                    Type = 1,
                    Yes = delegate () { }
                };
                UIElement.Dialog.SetOnInitArgs(FrameWork.EasyPool.Get<FrameWork.ArgumentBox>().SetObject("Cmd", dialogCmd4));
                UIManager.Instance.ShowUI(UIElement.Dialog);
            }
        }
        [HarmonyPatch(typeof(CommonUtils), "GetFavorString")]
        public class AppendFavor
        {
            public static string Postfix(string result, short favorability)
            {
                if (favorability == short.MinValue) return result;
                string res;
                if (favorability <= 0) { res = "\n<size=85%><color=#FavorabilityType_Hateful6>"; } else { res = "\n<size=85%><color=#FavorabilityType_Favorite6>"; }
                if (favorStr == 2) return "<size=85%>" + result + (res + (favorability / 3).ToString("D4") + "</color></size>").ColorReplace();
                int favorabi;
                if (favorability <= -26000) { favorabi = -(26000 + favorability) * 5 / 2; }
                else if (favorability <= -22000) { favorabi = -(22000 + favorability) * 5 / 2; }
                else if (favorability <= -18000) { favorabi = -(18000 + favorability) * 5 / 2; }
                else if (favorability <= -14000) { favorabi = -(14000 + favorability) * 5 / 2; }
                else if (favorability <= -10000) { favorabi = -(10000 + favorability) * 5 / 2; }
                else if (favorability <= -6000) { favorabi = -(6000 + favorability) * 5 / 2; }
                else if (favorability <= -2000) { favorabi = -(2000 + favorability) * 5 / 2; }
                else if (favorability <= 2000) { favorabi = (2000 + favorability) * 5 / 2; res = "\n<size=85%><color=#FavorabilityType_Unfamiliar>"; }
                else if (favorability < 6000) { favorabi = (favorability - 2000) * 5 / 2; }
                else if (favorability < 10000) { favorabi = (favorability - 6000) * 5 / 2; }
                else if (favorability < 14000) { favorabi = (favorability - 10000) * 5 / 2; }
                else if (favorability < 18000) { favorabi = (favorability - 14000) * 5 / 2; }
                else if (favorability < 22000) { favorabi = (favorability - 18000) * 5 / 2; }
                else if (favorability < 26000) { favorabi = (favorability - 22000) * 5 / 2; }
                else { favorabi = (favorability - 26000) * 5 / 2; }
                return result + (res + favorabi.ToString("D4") + "</color></size>").ColorReplace();
            }
        }
        [HarmonyDebug]
        [HarmonyPatch(typeof(CommonUtils), "GetCharmLevelText")]
        public class AppendCharm
        {
            public static string Postfix(string result, short charm)
            {
                return result.Replace("</color>", "\n" + charm + "</color>");
            }
        }
        [HarmonyPatch(typeof(UI_NewGame), "OnOverallDifficultyChange")]
        public class RestrictionsMustDie
        {
            public static void Prefix(UI_NewGame __instance)
            {
                Refers refers = __instance.CGet<Refers>("SettingView");
                CToggleGroup ctoggleGroup = refers.CGet<CToggleGroup>("SetDifficultyHolder");
                refers.CGet<CToggleGroup>("SetValueHolder2").Get(3).interactable = true;
            }
        }
        [HarmonyPatch(typeof(UI_BuildingTaiwuShrine), "InitTaiwuInfo")]
        public class CiTang_NoAgeRestrict
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(GameData.Domains.Character.Display.GroupCharDisplayData).GetField("CurrAge"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .SetAndAdvance(
                            OpCodes.Pop, null
                        ).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_S, 100)
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(UI_BuildingTaiwuShrine), "OnSelectSkill")]
        public class CiTang_BetterEffect_NoAuthCost
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Conv_I1),
                        new CodeMatch(OpCodes.Ldarg_2),
                        new CodeMatch(OpCodes.Call, typeof(GameData.Domains.Character.SkillQualificationBonus).GetConstructor(new Type[] { typeof(sbyte), typeof(sbyte), typeof(sbyte), typeof(short) }))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .SetAndAdvance(
                            OpCodes.Pop, null
                        ).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_S, ciTangAdd)
                        ).Advance(3)
                    ).InstructionEnumeration();
                return instructions;
            }
            //public static void Prefix(ref ushort ____shrineBuyTimes){
            //    ____shrineBuyTimes=65535;
            //}
            public static void Postfix(ref ushort ____shrineBuyTimes)
            {
                ____shrineBuyTimes = 0;
            }
        }
        [HarmonyPatch(typeof(WorldMapModel), "GetNeighborList")]
        public class RevealMap
        {
            public static void Prefix(ref int maxSteps)
            {
                maxSteps = Math.Max(maxSteps, mapSight);
            }
        }
        [HarmonyPatch(typeof(UI_CricketCombat), "OnListenerIdReady")]
        public static class CricketAttackFirst
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldc_I4_S, 50),
                        new CodeMatch(OpCodes.Ldc_I4_S, 100)
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .SetAndAdvance(
                            OpCodes.Ldc_I4_S, 100
                        ).Advance(1)
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(UI_CatchCricket), "OnClickCatchPlace")]
        public static class CatchAll
        {
            //public static UI_CatchCricket.CricketPlaceInfo[] catchPlaceList=new UI_CatchCricket.CricketPlaceInfo[21];
            //public static void Prefix(UI_CatchCricket.CricketPlaceInfo[] ____catchPlaceList){catchPlaceList=____catchPlaceList;}
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                foreach (CodeInstruction i in instructions)
                {
                    if (i.opcode == OpCodes.Call && ((MethodInfo)(i.operand)).Name.Contains("AddMethodCall"))
                    {
                        i.operand = typeof(CatchAll).GetMethod("catch_all");
                    }
                    yield return i;
                }
            }
            public static void catch_all(int listenerId, ushort domainId, ushort methodId, short arg1, short arg2, short arg3, sbyte arg4)
            {
                //    foreach(var place in catchPlaceList){
                //        GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, place.CricketColorId, place.CricketPartsId, 100, place.PlaceId);
                //GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, 18, 0, 100, arg4);// 天蓝青
                //GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, 19, 0, 100, arg4);// 三段锦
                //GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, 21, 0, 100, arg4);// 八败
                GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, arg1, arg2, 100, arg4);//应该捉到的蛐蛐，将声量放大到100以保证捕捉效果
                GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, 21, 19, 100, arg4);// 八败三段锦
                GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, 21, 19, 100, arg4);// 八败三段锦
                GameData.GameDataBridge.GameDataBridge.AddMethodCall<short, short, short, sbyte>(listenerId, domainId, methodId, 21, 19, 100, arg4);// 八败三段锦
                                                                                                                                                    //    }
            }
        }
        [HarmonyPatch(typeof(UI_Make), "CheckMakeCondition")]
        public class Speedup
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Stfld, typeof(UI_Make).GetField("_makeTime", (BindingFlags)(-1)))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Pop),
                            new CodeInstruction(OpCodes.Ldc_I4_0)
                        //                        new CodeInstruction(OpCodes.Ldc_I4_1),
                        //                        new CodeInstruction(OpCodes.Call,typeof(Math).GetMethod("Min",new Type[]{typeof(short),typeof(short)}))
                        ).Advance(1)
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        public static class TraitCost
        {
            static IEnumerable<MethodBase> TargetMethods()
            {
                yield return typeof(UI_NewGame).GetMethod("OnAbilityClick", (BindingFlags)(-1));
                yield return typeof(UI_NewGame).GetMethod("OnCellItemRender", (BindingFlags)(-1));
                yield return typeof(UI_NewGame).GetMethod("UpdatePoints", (BindingFlags)(-1));
                yield return typeof(UI_NewGame).GetMethod("OnStartNewGame", (BindingFlags)(-1));
            }
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(Config.ProtagonistFeatureItem).GetField("Cost"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Pop)
                        )
                        .SetAndAdvance(
                            OpCodes.Ldc_I4_0, null
                        )
                    ).InstructionEnumeration();
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(Config.ProtagonistFeatureItem).GetField("PrerequisiteCost"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Pop)
                        )
                        .SetAndAdvance(
                            OpCodes.Ldc_I4_0, null
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(CommonUtils), "GetCharacterHealthInfo")]
        public static class RevealHealth
        {
            public static void Postfix(ref ValueTuple<string, float> __result, short health, short leftHealth)
            {
                if (N17 == 1)
                {
                    __result.Item1 += "<color=#aec9e3>\n(" + health.ToString() + "/" + leftHealth.ToString() + ")</color>";
                }
                else if (N17 == 0)
                {
                    __result.Item1 += "<color=#aec9e3>(" + health.ToString() + "/" + leftHealth.ToString() + ")</color>";
                }
                else
                {
                    __result.Item1 = health.ToString() + "<color=#aec9e3>/" + leftHealth.ToString() + "</color>";
                }
            }
        }
        [HarmonyPatch(typeof(UI_Combat), "OnInit")]
        public static class AutoFast
        {
            public static void Postfix(ref bool ____autoCombat, UI_Combat __instance, float ____displayTimeScale)
            {
                ____autoCombat = true;
                ____displayTimeScale = 2f; // 或许有效或许无效但我不准备管了。
                                           //GameData.GameDataBridge.GameDataBridge.AddMethodCall<float>(__instance.Element.GameDataListenerId, 8, 39, 5f);
                                           //typeof(UI_Combat).GetMethod("SetDisplayTimeScale",(BindingFlags)(-1)).Invoke(__instance,new object[]{2f});
                                           //__result.Item1+="<color=#aec9e3>\n("+health.ToString()+"/"+leftHealth.ToString()+"</color>)";
            }
        }
        [HarmonyPatch(typeof(UI_BuildingArea), "CanBuildAnywhere")]
        public static class UnlockShow
        {
            public static bool Postfix(bool result)
            {
                return true;
            }
        }
        [HarmonyPatch(typeof(UI_CricketCombat), "SetEnemyCricketsVisible")]
        public static class Showququ
        {
            public static void Prefix(ref bool visible)
            {
                visible = true;
            }
        }
        [HarmonyPatch]
        public static class FastCricketDeploy
        {
            public static GameData.Domains.Item.ItemKey[] keys = new GameData.Domains.Item.ItemKey[] { GameData.Domains.Item.ItemKey.Invalid, GameData.Domains.Item.ItemKey.Invalid, GameData.Domains.Item.ItemKey.Invalid };
            public static MethodInfo OnClickSelectCricket = typeof(UI_CricketCombat).GetMethod("OnClickSelectCricket", (BindingFlags)(-1));
            [HarmonyPrefix]
            [HarmonyPatch(typeof(UI_CricketCombat), "StartBattleRound")]
            public static void Prfx(ref GameData.Domains.Item.ItemKey[] ____selfCricketKeys)
            {
                keys = ____selfCricketKeys;
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(UI_CricketCombat), "UpdateCurrWagerValue")]
            public static void Pstfx(UI_CricketCombat __instance, ref GameData.Domains.Item.ItemKey[] ____selfCricketKeys)
            {
                for (var index = 0; index < 3; index++) if (keys[index].IsValid()) OnClickSelectCricket.Invoke(__instance, new object[] { keys[index] });
            }
        }
        //    [HarmonyPatch(typeof(UI_CharacterMenuCombatSkill),"UpdateCurrPracticeSkillData")]
        //    public static class Printing {
        //        public static void Prefix(short ____currPracticeSelectedSkillId, CToggleGroup ____outlinePageTogGroup, CToggleGroup ____otherPageTogGroup){
        //            if(____outlinePageTogGroup.GetActive().Key<0)return;
        //            short state=1<<____outlinePageTogGroup.GetActive().Key;
        //            for (int b = 5; b < 15; b ++) {
        //                if(____otherPageTogGroup.Get(b).isOn)state|=1<<b;
        //            }            
        //        }
        //    }
    }
}