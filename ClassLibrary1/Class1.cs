// dotnet $DOTNET_CSC_DLL -nologo -t:library -r:"../../../../compatdata/838350/pfx/drive_c/Program Files/dotnet/shared/Microsoft.NETCore.App/5.0.17/System.dll" -r:"../../../../compatdata/838350/pfx/drive_c/Program Files/dotnet/shared/Microsoft.NETCore.App/5.0.17/System.Collections.dll" -r:"../../The Scroll of Taiwu_Data/Managed/0Harmony.dll" -r:"../../../../compatdata/838350/pfx/drive_c/Program Files/dotnet/shared/Microsoft.NETCore.App/5.0.17/mscorlib.dll" -r:"../../Backend/GameData.dll" -r:"../../Backend/Redzen.dll" -r:"../../The Scroll of Taiwu_Data/Managed/TaiwuModdingLib.dll" -r:"../../../../compatdata/838350/pfx/drive_c/Program Files/dotnet/shared/Microsoft.NETCore.App/5.0.17/System.Private.CoreLib.dll" -r:"../../../../compatdata/838350/pfx/drive_c/Program Files/dotnet/shared/Microsoft.NETCore.App/5.0.17/System.Runtime.dll" -unsafe -optimize -deterministic Backend.cs -out:Backend.dll
// -r:"../../The Scroll of Taiwu_Data/Managed/Mono.Cecil.dll" -r:"../../The Scroll of Taiwu_Data/Managed/System.Core.dll"   -r:"../../The Scroll of Taiwu_Data/Managed/System.Composition.AttributedModel.dll" -r:"../../../../compatdata/838350/pfx/drive_c/Program Files/dotnet/shared/Microsoft.NETCore.App/5.0.17/System.Runtime.dll"
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

// Backend: ProcessMethodCall -> CallMethod -> ...
#define BACKEND

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using HarmonyLib;

namespace ASmallCollectionsFromNeutron
{
    [TaiwuModdingLib.Core.Plugin.PluginConfig("ASmallCollectionsFromNeutron", "Neutron3529", "0.4.1")]
    public class ASmallCollectionsFromNeutron : TaiwuModdingLib.Core.Plugin.TaiwuRemakeHarmonyPlugin
    {
        public static int fpv = 100;
        public static int mulF = 32000;
        public static int maxV = 32000;
        public static int divN = 125;
        public static int minLife = 1000;
        public static int minCombat = 95;
        public static int minMain = 200;
        public static int mapSight = 200;
        public static int gbasc = 100;
        public static int AFBegin = 1;
        public static int N01APC = 30;
        public static int N01ANC = 0;
        public static int HuanYueNine = 12000;
        public static int knowOther = 1000;
        public static int WuLinTraitLevel = 3;
        public static bool enterWorldGrandVision = false;
        public static short StoryHuanyue = Config.Character.DefKey.StoryHuanyue;
        public static short DefKey_VirginityFalse = Config.CharacterFeature.DefKey.VirginityFalse;
        public static short DefKey_Pregnant = Config.CharacterFeature.DefKey.Pregnant;
        public static object[] MakeLove_param;
        public static short DefKey_HaveElixir2 = Config.CharacterFeature.DefKey.HaveElixir2;
        public static short DefKey_GreenMotherSpiderPoison2 = Config.CharacterFeature.DefKey.GreenMotherSpiderPoison2;
        public static short DefKey_StarsRegulateBreath2 = Config.CharacterFeature.DefKey.StarsRegulateBreath2;
        public static short DefKey_SupportFromShixiang2 = Config.CharacterFeature.DefKey.SupportFromShixiang2;
        public static short DefKey_FulongServant = Config.CharacterFeature.DefKey.FulongServant;
        public static int pRegen = 100;
        public static int pRegen2 = 100;
        public static int pLife = 100;
        public static int pLife2 = 100;
        public static int pCombat = 100;
        public static int pCombat2 = 100;
        public static int cRegen = 1;
        public static int cLife = 1;
        public static int cCombat = 1;
        public static bool LSF_Show = true;
        public static bool LSF_Lucky = true;
        public static bool finishAllYellow = true;
        public static ushort RelationType_SwornBrotherOrSister = GameData.Domains.Character.Relation.RelationType.SwornBrotherOrSister;
        public static ushort RelationType_Mentor = GameData.Domains.Character.Relation.RelationType.Mentor;
        public static ushort RelationType_Mentee = GameData.Domains.Character.Relation.RelationType.Mentee;
        public static ushort RelationType_Friend = GameData.Domains.Character.Relation.RelationType.Friend;
        public static bool twoverwrite = false;
        public static int inhertmain = 10000;
        public static int inhertcombat = 10000;
        public static int inhertlife = 10000;
        public static int pValue = 10000;
        public static int pLevel = 3;
        public static bool fulongCF = true;
        public static int totalTeachCount = 100;
        public static int YiSu_ID = 14;
        public static int GongSu_ID = 19;
        public static int NeiGong_ID = 20;
        public static int MiJue_ID = 39;
        public static List<Config.BreakGrid> AdditionalGrids0 = new List<Config.BreakGrid>();
        public static List<Config.BreakGrid> AdditionalGrids1 = new List<Config.BreakGrid>();
        public static List<Config.BreakGrid> AdditionalGrids2 = new List<Config.BreakGrid>();
        public static List<Config.BreakGrid> AdditionalGrids3 = new List<Config.BreakGrid>();
        public static List<Config.BreakGrid> AdditionalGrids4 = new List<Config.BreakGrid>();
        // public static List<Config.BreakGrid> AdditionalGridsOnce0=new List<Config.BreakGrid>();
        // public static List<Config.BreakGrid> AdditionalGridsOnce1=new List<Config.BreakGrid>();
        // public static List<Config.BreakGrid> AdditionalGridsOnce2=new List<Config.BreakGrid>();
        // public static List<Config.BreakGrid> AdditionalGridsOnce3=new List<Config.BreakGrid>();
        // public static List<Config.BreakGrid> AdditionalGridsOnce4=new List<Config.BreakGrid>();
        public static Config.BreakGrid yellowFill = new Config.BreakGrid(-1, 0);
        public static OpCode Op_instruction = OpCodes.Ldc_I4_1;
        public static bool N03_2 = false;
        public void ReadAdditionals(string key, List<Config.BreakGrid> AdditionalGrids)
        {
            AdditionalGrids.Clear();
            string Additionals = "";
            GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref Additionals);
            if (Additionals.Length > 0) try
                {
                    foreach (string gc in Additionals.Split(","))
                    {
                        var tst = gc.Split("x");
                        sbyte count = (sbyte)int.Parse(tst[1].Trim());
                        short bonusType = -1;
                        var id = tst[0].Trim();
                        try
                        {
                            bonusType = (short)int.Parse(id);
                        }
                        catch
                        {
                            foreach (var x in Config.SkillBreakPlateGridBonusType.Instance)
                            {
                                if (x.Name == id)
                                {
                                    bonusType = x.TemplateId;
                                    break;
                                }
                            }
                        }
                        if (bonusType >= 0) AdditionalGrids.Add(new Config.BreakGrid(bonusType, count));
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("额外黄点格子（" + key + "）值有误，请修改字符串格式，或者将字符串置空，错误信息为：\n" + e.Message);
                }
        }

        public static List<short> manual_feature_id = new List<short>();
        public void MFid(string key)
        {
            manual_feature_id.Clear();
            string Additionals = "";
            GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref Additionals);
            if (Additionals.Length > 0) try
                {
                    foreach (string txt in Additionals.Split(","))
                    {
                        var cf = txt.Trim();
                        short cfid = -1;
                        try
                        {
                            cfid = (short)int.Parse(cf);
                        }
                        catch
                        {
                            foreach (var x in Config.CharacterFeature.Instance)
                            {
                                if (x.Name == cf)
                                {
                                    cfid = x.TemplateId;
                                    break;
                                }
                            }
                        }
                        if (cfid >= 0) manual_feature_id.Add(cfid);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("人物特性字符串（" + key + "）值有误，请修改字符串格式，或者将字符串置空，错误信息为：\n" + e.Message);
                }
        }

        //public static List<sbyte> building_maxLv=new List<sbyte>();
        public void buildingMaxLv(string key)
        {
            //building_maxLv.Clear();
            string Additionals = "";
            GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref Additionals);
            if (Additionals.Length > 0) try
                {
                    foreach (string gc in Additionals.Split(","))
                    {
                        var tst = gc.Split("=");
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
            bool enabled = false;
            if (key == "N01")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01WuLin", ref WuLinTraitLevel);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01APC", ref N01APC);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01ANC", ref N01ANC);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref AFBegin);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01minLife", ref minLife);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01minCombat", ref minCombat);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01minMain", ref minMain);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "HuanYueNine", ref HuanYueNine);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N01FuLongFriend", ref fulongCF);
                if (HuanYueNine > 0)
                {
                    StoryHuanyue = (short)typeof(Config.Character.DefKey).GetField("StoryHuanyue").GetValue(null);
                    this.HarmonyInstance.PatchAll(typeof(HuanYue));
                }

                DefKey_HaveElixir2 = (short)typeof(Config.CharacterFeature.DefKey).GetField("HaveElixir2").GetValue(null);
                DefKey_GreenMotherSpiderPoison2 = (short)typeof(Config.CharacterFeature.DefKey).GetField("GreenMotherSpiderPoison2").GetValue(null);
                DefKey_StarsRegulateBreath2 = (short)typeof(Config.CharacterFeature.DefKey).GetField("StarsRegulateBreath2").GetValue(null);
                DefKey_SupportFromShixiang2 = (short)typeof(Config.CharacterFeature.DefKey).GetField("SupportFromShixiang2").GetValue(null);
                DefKey_FulongServant = (short)typeof(Config.CharacterFeature.DefKey).GetField("FulongServant").GetValue(null);
                return AFBegin > -1;
            }
            else if (key == "N02")
            {
                bool N02 = false;
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref N02);
                if (N02)
                {
                    this.HarmonyInstance.PatchAll(typeof(AllYellowPerk));
                    this.ReadAdditionals("N02-0", AdditionalGrids0);
                    this.ReadAdditionals("N02-1", AdditionalGrids1);
                    this.ReadAdditionals("N02-2", AdditionalGrids2);
                    this.ReadAdditionals("N02-3", AdditionalGrids3);
                    this.ReadAdditionals("N02-4", AdditionalGrids4);
                    // this.ReadAdditionals("N02-00", AdditionalGridsOnce0);
                    // this.ReadAdditionals("N02-01", AdditionalGridsOnce1);
                    // this.ReadAdditionals("N02-02", AdditionalGridsOnce2);
                    // this.ReadAdditionals("N02-03", AdditionalGridsOnce3);
                    // this.ReadAdditionals("N02-04", AdditionalGridsOnce4);
                    string Fill = "";
                    GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N02-Fill", ref Fill);
                    short bonusType = -1;
                    try
                    {
                        bonusType = (short)int.Parse(Fill);
                    }
                    catch
                    {
                        foreach (var x in Config.SkillBreakPlateGridBonusType.Instance)
                        {
                            if (x.Name == Fill)
                            {
                                bonusType = x.TemplateId;
                                break;
                            }
                        }
                    }
                    if (bonusType >= 0)
                    {
                        yellowFill = new Config.BreakGrid(bonusType, 100);
                        Op_instruction = OpCodes.Ldc_I4_0;
                    }
                }
                else return false;
                return true;
            }
            else if (key == "N03")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref enabled);
                if (enabled) GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N03-2", ref N03_2);
                return enabled;
            }
            if (key == "N04")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref enabled);
                if (enabled)
                {
                    GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "NmulF", ref mulF);
                    GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "NmaxV", ref maxV);
                    GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "NdivN", ref divN);
                    GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N04pLevel", ref pLevel);
                    GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N04pValue", ref pValue);
                    //GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N04poison", ref poison);
                    //GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N04plevel", ref plevel);
                    return mulF > divN && maxV > 0;
                }
            }
            else if (key == "N05")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref LSF_Show);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N05-2", ref LSF_Lucky);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N05-3", ref finishAllYellow);
                return LSF_Show || LSF_Lucky || finishAllYellow;
            }
            else if (key == "N06")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref gbasc);
                return gbasc > 0;
            }
            else if (key == "N09S")
            {
                int ciTangAdd = 120;
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref ciTangAdd);
                return ciTangAdd != 0;
            }
            else if (key == "N10")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref fpv);
                return fpv > 0;
            }
            else if (key == "N13")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref knowOther);
                return knowOther > 0;
            }
            else if (key == "N14")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref mapSight);
                return mapSight > 0;
            }
            else if (key == "N27")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref totalTeachCount);
                if (totalTeachCount == 0) return false;
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "pRegen", ref pRegen);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "pRegen2", ref pRegen2);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "pLife", ref pLife);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "pLife2", ref pLife2);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "pCombat", ref pCombat);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "pCombat2", ref pCombat2);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "cRegen", ref cRegen);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "cLife", ref cLife);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "cCombat", ref cCombat);
                return true;
            }
            else if (key == "N28")
            {
                int switcher = 0;
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref switcher);
                twoverwrite = switcher == 2;
                return switcher > 0;
            }
            else if (key == "N29")
            {
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N29inhertmain", ref inhertmain);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N29inhertcombat", ref inhertcombat);
                GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "N29inhertlife", ref inhertlife);
                return inhertmain + inhertcombat + inhertlife > 0;
            }
            else if (key == "N37")
            {
                buildingMaxLv(key);
            }
            GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, key, ref enabled);
            return enabled;
        }
        public override void OnEnterNewWorld()
        { // 试图修复开局地图红字
            enterWorldGrandVision = false;
        }
        public override void OnModSettingUpdate()
        {
            this.HarmonyInstance.UnpatchSelf();
            if (enable("NForceOFF")) { return; }
            if (enable("NPatchPregnant"))
            {
                DefKey_VirginityFalse = (short)typeof(Config.CharacterFeature.DefKey).GetField("VirginityFalse", (BindingFlags)(-1)).GetValue(null);
                DefKey_Pregnant = (short)typeof(Config.CharacterFeature.DefKey).GetField("Pregnant", (BindingFlags)(-1)).GetValue(null);
                MakeLove_param = new object[] { DefKey_VirginityFalse, true };
                this.HarmonyInstance.PatchAll(typeof(PatchPregnant));
            }
            if (enable("N01"))
            {
                this.HarmonyInstance.PatchAll(typeof(AllFeature));
                MFid("N01-2");
            }
            enable("N02");
            if (enable("N02-Ex")) this.HarmonyInstance.PatchAll(typeof(ExtendBreak));
            if (enable("N02-YaEx")) this.HarmonyInstance.PatchAll(typeof(YaExtendBreak));
            if (enable("N03")) this.HarmonyInstance.PatchAll(typeof(AlwaysCapture));
            if (enable("N04")) this.HarmonyInstance.PatchAll(typeof(WeaponModifier));
            if (enable("N05")) this.HarmonyInstance.PatchAll(typeof(LuckyShow));
            if (enable("N05x"))
            {
                this.HarmonyInstance.PatchAll(typeof(YaN05x));
                //this.HarmonyInstance.PatchAll(typeof(N05x)); //works with  Op_instruction=OpCodes.Ldc_I4_1;
            }
            if (enable("N06")) this.HarmonyInstance.PatchAll(typeof(BreakPlus));
            if (enable("N07")) this.HarmonyInstance.PatchAll(typeof(TransNext));
            if (enable("N09S")) this.HarmonyInstance.PatchAll(typeof(CiTang_Backend_NoAuthCost));
            if (enable("N10")) this.HarmonyInstance.PatchAll(typeof(FastPractice));
            if (enable("N11")) this.HarmonyInstance.PatchAll(typeof(MakeSpeedup));
            if (enable("N12")) this.HarmonyInstance.PatchAll(typeof(Speedup));
            if (enable("N13")) this.HarmonyInstance.PatchAll(typeof(TaiwuDomainUpdateCombatSkillBookReadingProgress));
            if (enable("N14")) this.HarmonyInstance.PatchAll(typeof(RevealMap));
            if (enable("N16")) this.HarmonyInstance.PatchAll(typeof(ZhouBaPi));
            if (enable("N24")) this.HarmonyInstance.PatchAll(typeof(BuildResource));
            if (enable("N25")) this.HarmonyInstance.PatchAll(typeof(Qinggong_Move));
            if (enable("N27"))
            {
                RelationType_Mentor = (ushort)typeof(GameData.Domains.Character.Relation.RelationType).GetField("Mentor", (BindingFlags)(-1)).GetValue(null);
                RelationType_Mentee = (ushort)typeof(GameData.Domains.Character.Relation.RelationType).GetField("Mentee", (BindingFlags)(-1)).GetValue(null);
                RelationType_SwornBrotherOrSister = (ushort)typeof(GameData.Domains.Character.Relation.RelationType).GetField("SwornBrotherOrSister", (BindingFlags)(-1)).GetValue(null);
                RelationType_Friend = (ushort)typeof(GameData.Domains.Character.Relation.RelationType).GetField("Friend", (BindingFlags)(-1)).GetValue(null);
                this.HarmonyInstance.PatchAll(typeof(MustLearn));
            }
            if (enable("N28")) { this.HarmonyInstance.PatchAll(typeof(TaiwuLegency)); }
            if (enable("N29")) { this.HarmonyInstance.PatchAll(typeof(TaiwuLegencyAttribute)); }
            if (enable("N30")) { this.HarmonyInstance.PatchAll(typeof(AutoUpgrade)); }
            if (enable("N31")) { this.HarmonyInstance.PatchAll(typeof(LuckyReading)); }
            if (enable("N36")) { this.HarmonyInstance.PatchAll(typeof(LuckyReadingReadsAll)); }
            if (enable("N33")) { this.HarmonyInstance.PatchAll(typeof(Printing)); }
            enable("N37");
        }
        public override void Initialize()
        {
            //this.OnModSettingUpdate();
        }
        public class BuildResource
        {
            public static short Max1(short a)
            {
                return Math.Max(a, (short)1);
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "OfflineUpdateOperation")]
            public static IEnumerable<CodeInstruction> Transpiler_Max(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(Config.BuildingBlockItem).GetField("OperationType")),
                        new CodeMatch(OpCodes.Ldelem_I2)
                    ).Repeat(matcher => // Do the following for each match
                        matcher.Advance(2).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Call, typeof(BuildResource).GetMethod("Max1"))
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "CanBuild")]
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldloc_S),
                        new CodeMatch(OpCodes.Ldfld, typeof(Config.BuildingBlockItem).GetField("Type"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .SetAndAdvance(
                            OpCodes.Nop, null
                        )
                        .SetAndAdvance(
                            OpCodes.Ldc_I4_S, (int)EBuildingBlockType.Building
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "CanUpgrade")] // 逻辑与CanBuild一致，但这里的ldloc是ldloc_1
            public static IEnumerable<CodeInstruction> Transpiler_Upgrade(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(Config.BuildingBlockItem).GetField("Type"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .SetAndAdvance(
                            OpCodes.Pop, null
                        )
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_S, (int)EBuildingBlockType.Building)
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "ParallelUpdate")]
        public class AutoUpgrade
        {
            public static Type[] args = new Type[]{
            typeof(GameData.Domains.Building.ParallelBuildingModification),
            typeof(short),
            typeof(Config.BuildingBlockItem),
            typeof(GameData.Domains.Building.BuildingBlockKey),
            typeof(GameData.Domains.Building.BuildingBlockData)
        };
            public static MethodInfo OfflineUpdateOperation = typeof(GameData.Domains.Building.BuildingDomain).GetMethod("OfflineUpdateOperation", (BindingFlags)(-1), null, args, null);
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Call, OfflineUpdateOperation)
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldarg_1), // 多传一个context能死是么

                            new CodeInstruction(OpCodes.Ldarg_0),
                            new CodeInstruction(OpCodes.Ldfld, typeof(GameData.Domains.Building.BuildingDomain).GetField("_buildingOperatorDict", (BindingFlags)(-1)))
                        // _buildingOperatorDict
                        //                        new CodeInstruction(OpCodes.Ldarg_0), // GameData.Domains.Building.BuildingDomain
                        //                        new CodeInstruction(OpCodes.Ldarg_S,4), // GameData.Domains.Building.BuildingBlockKey
                        //                        new CodeInstruction(OpCodes.Ldarg_S,5), // GameData.Domains.Building.BuildingBlockData
                        //                        new CodeInstruction(OpCodes.Ldarg_0),
                        //                        new CodeInstruction(OpCodes.Ldfld,typeof(GameData.Domains.Building.BuildingDomain).GetField("_buildingOperatorDict",(BindingFlags)(-1))),
                        //                        new CodeInstruction(OpCodes.Call,typeof(AutoUpgrade).GetMethod("Process"))
                        ).SetAndAdvance(
                            OpCodes.Call, typeof(AutoUpgrade).GetMethod("Process")
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
            public static void Process(
                GameData.Domains.Building.BuildingDomain that,
                GameData.Domains.Building.ParallelBuildingModification modification,
                short settlementId,
                Config.BuildingBlockItem configData,
                GameData.Domains.Building.BuildingBlockKey blockKey,
                GameData.Domains.Building.BuildingBlockData blockData,
                GameData.Common.DataContext context,
                Dictionary<GameData.Domains.Building.BuildingBlockKey, GameData.Domains.Character.CharacterList> _buildingOperatorDict
            )
            {
                bool should_keep = blockData.OperationType != 2/*AddBuildingDemolitionCompleted*/; // blockData.OperationType==0 /*AddBuildingConstructionCompleted*/|| blockData.OperationType==1/*AddBuildingUpgradingCompleted*/
                OfflineUpdateOperation.Invoke(that, new object[] { modification, settlementId, configData, blockKey, blockData });
                GameData.Domains.Character.CharacterList currworker;
                if (should_keep && _buildingOperatorDict.TryGetValue(blockKey, out currworker) && that.CanUpgrade(blockKey))
                {
                    var tmp = currworker.GetCollection().ToArray();
                    int counter = 0;
                    foreach (int cid in tmp)
                    {
                        counter += Convert.ToInt32(cid > 0);
                    }
                    if (counter > 0)
                    {
                        that.Upgrade(context, blockKey, tmp);
                        modification.FreeOperator = false;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Map.MapDomain), "Move", new Type[] { typeof(GameData.Common.DataContext), typeof(short), typeof(bool) })]
        public class Qinggong_Move
        {
            public static void Prefix(GameData.Domains.Map.MapDomain __instance, ref bool notCostTime, bool ____teleportMove, GameData.Common.DataContext context, short destBlockId)
            {
                //GameData.Utilities.AdaptableLog.Info("开始移动");
                if (notCostTime || ____teleportMove)
                {
                    return;
                }
                //GameData.Utilities.AdaptableLog.Info("开始处理内力逻辑");
                var taiwu = GameData.Domains.DomainManager.Taiwu.GetTaiwu();
                var loc = taiwu.GetLocation();
                loc.BlockId = destBlockId;
                int num = taiwu.GetCurrNeili() - (int)(__instance.GetBlock(loc).GetConfig().MoveCost) * GameData.Domains.DomainManager.Taiwu.GetMoveTimeCostPercent() / 100;
                if (num >= 0)
                {
                    taiwu.SetCurrNeili(num, context);
                    notCostTime = true;
                }
                //GameData.Utilities.AdaptableLog.Info("结束处理流程，目前notCostTime="+notCostTime);
                // TODO : 可以在这里加入自动存取物品逻辑。
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "TeachSkill")]
        public class CiTang_Backend_NoAuthCost
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldc_I4_1),
                        new CodeMatch(OpCodes.Add),
                        new CodeMatch(OpCodes.Conv_U2),
                        new CodeMatch(OpCodes.Ldarg_1),
                        new CodeMatch(OpCodes.Call, typeof(GameData.Domains.Building.BuildingDomain).GetMethod("SetShrineBuyTimes"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .SetAndAdvance(
                            OpCodes.Ldc_I4_0, null
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Map.MapDomain), "GetNeighborBlocks")]
        public class RevealMap
        {
            public static void Prefix(ref int maxSteps)
            {
                if (enterWorldGrandVision)
                {
                    maxSteps = Math.Max(maxSteps, mapSight);
                }
                else
                {
                    enterWorldGrandVision = true;
                }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "GetMakeItems")]
        public class WeaponModifier
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(i => i.opcode == OpCodes.Call && ((MethodInfo)i.operand).Name == "GetElement_MakeItemDict")
                    ).Repeat(matcher => // Do the following for each match
                        matcher
                        .Advance(1)
                        .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Call, typeof(WeaponModifier).GetMethod("MZhujianGA"))
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
            public static unsafe GameData.Domains.Building.MakeItemData MZhujianGA(
                GameData.Domains.Building.MakeItemData makeItemData
            )
            {
                fixed (short* p = makeItemData.MaterialResources.Items)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        *(p + i) = (short)Math.Min(((int)(*(p + i)) * mulF) / divN, maxV);
                    }
                }
                return makeItemData;
            }
            // public static void Postfix(List<GameData.Domains.Item.Display.ItemDisplayData> __result, GameData.Common.DataContext context) {
            // if(plevel>0){
            // GameData.Domains.Item.PoisonsAndLevels prevAttachedPoison = new GameData.Domains.Item.PoisonsAndLevels(new short[]{(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel});
            // for(int i=0;i<__result.Count;i++) {
            // var key=__result[i].Key;
            // if(key.ItemType<4 && GameData.Domains.Item.ItemTemplateHelper.IsPoisonable(key.ItemType, key.TemplateId)) {
            // GameData.Domains.Item.ItemBase item=;
            // byte state = item.GetModificationState();
            // state = ModificationStateHelper.Activate(state, 1);
            // item.SetModificationState(currState2, context);
            // }
            // }
            // }
            // }
            public static MethodInfo SetElement_PoisonItems = typeof(GameData.Domains.Item.ItemDomain).GetMethod("SetElement_PoisonItems", (BindingFlags)(-1));
            public static FieldInfo _poisonItems = typeof(GameData.Domains.Item.ItemDomain).GetField("_poisonItems", (BindingFlags)(-1));
            public static FieldInfo _poisons = typeof(GameData.Domains.Item.PoisonEffects).GetField("_poisons", (BindingFlags)(-1));
            [HarmonyPostfix]
            [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "AddItemPoison")]
            public unsafe static void Pstfx(GameData.Common.DataContext context, ValueTuple<bool, GameData.Domains.Item.Display.ItemDisplayData> __result)
            {
                var dict = (Dictionary<int, GameData.Domains.Item.PoisonEffects>)_poisonItems.GetValue(GameData.Domains.DomainManager.Item);
                var itemId = __result.Item2.Key.Id;
                GameData.Domains.Item.PoisonEffects effect;
                if (dict.TryGetValue(itemId, out effect))
                { // 1 是淬毒的意思
                  // PoisonsAndLevels.SetValue(effect, new GameData.Domains.Item.PoisonsAndLevels(new short[]{(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel,(short)poison,(short)plevel}) );
                    var pal = effect.GetAllPoisonsAndLevels();
                    //                for (int i = 0; i < 6; i++) {
                    //                    GameData.Utilities.AdaptableLog.Info("pre:"+*(pal.Values + i)+" "+*(pal.Levels+ i)+">="+pValue +""+pLevel);
                    //                }
                    //                GameData.Utilities.AdaptableLog.Info("淬毒开始");

                    for (int i = 0; i < 6; i++)
                    {
                        *(pal.Values + i) = Math.Max(*(pal.Values + i), (short)pValue);
                        *(pal.Levels + i) = Math.Max(*(pal.Levels + i), (sbyte)pLevel);
                        //                    GameData.Utilities.AdaptableLog.Info("process:"+*(pal.Values + i)+" "+*(pal.Levels+ i)+">="+pValue +""+pLevel);
                    }


                    TypedReference reference = __makeref(effect);
                    _poisons.SetValueDirect(reference, pal);
                    //                pal=effect.GetAllPoisonsAndLevels();
                    //                for (int i = 0; i < 6; i++) {
                    //                    GameData.Utilities.AdaptableLog.Info("post:"+*(pal.Values + i)+" "+*(pal.Levels+ i)+">="+pValue +""+pLevel);
                    //                }

                    var args = new object[] { itemId, effect, context };
                    SetElement_PoisonItems.Invoke(GameData.Domains.DomainManager.Item, args);
                }
            }
        }
        public class Speedup
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "GetBaseReadingSpeed")]
            public static bool Prefix(ref sbyte __result)
            {
                __result = 100;
                return false;
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "GetReadingSpeedBonus")]
            public static int Postfix(int res)
            {
                return Math.Max(res * 50, 600);
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Building.BuildingDomain), "StartMakeItem")]
        public class MakeSpeedup
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(Config.MakeItemSubTypeItem).GetField("Time"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.SetAndAdvance(
                            OpCodes.Pop, null
                        ).InsertAndAdvance(new CodeInstruction(OpCodes.Ldc_I4_0))
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "InitSkillBreakPlate")]
        public class LuckyShow
        {
            public static void Postfix(GameData.Domains.Taiwu.SkillBreakPlate plate)
            {
                for (int row = 0; row < plate.Grids.Length; row++) for (int col = 0; col < plate.Grids[row].Length - (1 & ~row); col++)
                    {
                        plate.Grids[row][col].SuccessRateFix = 120;
                        if (finishAllYellow && plate.Grids[row][col].BonusType >= 0)
                        {
                            plate.Grids[row][col].State = 2;
                        }
                        else
                        {
                            plate.Grids[row][col].State = Math.Max(plate.Grids[row][col].State, (sbyte)0);
                        }
                    }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "InitSkillBreakPlate")]
        public class N05x
        {
            public static void Postfix(GameData.Domains.Taiwu.SkillBreakPlate plate)
            {
                var t = plate.Grids[0][0];
                plate.Grids[0][0] = plate.Grids[plate.ExtraPoint.Item1][plate.ExtraPoint.Item2];
                plate.Grids[plate.ExtraPoint.Item1][plate.ExtraPoint.Item2] = t;
                var tmp = plate.ExtraPoint;
                plate.ExtraPoint = (0, 0);
                if (plate.StartPoint == (0, 0)) plate.StartPoint = tmp; else if (plate.EndPoint == (0, 0)) plate.EndPoint = tmp;
                t = plate.Grids[0][1];
                plate.Grids[0][1] = plate.Grids[plate.StartPoint.Item1][plate.StartPoint.Item2];
                plate.Grids[plate.StartPoint.Item1][plate.StartPoint.Item2] = t;
                if (plate.EndPoint == (0, 1)) plate.EndPoint = plate.StartPoint;
                plate.StartPoint = (0, 1);
                t = plate.Grids[1][0];
                plate.Grids[1][0] = plate.Grids[plate.EndPoint.Item1][plate.EndPoint.Item2];
                plate.Grids[plate.EndPoint.Item1][plate.EndPoint.Item2] = t;
                plate.EndPoint = (1, 0);
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.BreakPlateRegions), "GetRandomPointInRegion")]
        public class YaN05x
        {
            static int _YaN05x_status = 0;
            public static bool Prefix(ref ValueTuple<int, int> __result)
            {
                switch (_YaN05x_status)
                {
                    case 0: __result = (0, 1); _YaN05x_status++; break;
                    case 1: __result = (1, 0); _YaN05x_status++; break;
                    case 2: __result = (0, 0); _YaN05x_status = 0; break;
                    default: throw new Exception("YaN05x 被错误修改");
                }
                return false;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "InitSkillBreakPlate")]
        public class TransNext
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldloc_S), // match method 2
                        new CodeMatch(OpCodes.Callvirt, typeof(Redzen.Random.IRandomSource).GetMethod("Next", new Type[] { typeof(int) })),
                        new CodeMatch(OpCodes.Ldloc_S), // match method 2
                        new CodeMatch(i => i.opcode == OpCodes.Callvirt && ((MethodInfo)i.operand).Name == "get_Count")
                    ).Repeat(matcher => // Do the following for each match
                        matcher.SetAndAdvance(
                            OpCodes.Ldc_I4_1, null
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Combat.CombatDomain), "CalcEvaluationList")]
        public class LuckyReading
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Callvirt, typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("GetReadInCombatCount", new Type[0]))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.SetAndAdvance(
                            OpCodes.Pop, null
                        ).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_8)
                        )
                    ).InstructionEnumeration();
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Call, typeof(GameData.Utilities.RedzenHelper).GetMethod("CheckPercentProb"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.InsertAndAdvance(
                            new CodeInstruction(OpCodes.Pop),
                            new CodeInstruction(OpCodes.Ldc_I4_S, 100)
                        ).Advance(1)
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "UpdateReadingProgressInCombat")]
        public class LuckyReadingReadsAll
        {
            public static bool block = true;
            public static sbyte SkillBooks = (sbyte)((ushort)typeof(GameData.Domains.Item.ItemDomainHelper.DataIds).GetField("SkillBooks").GetValue(null));//(sbyte)GameData.Domains.Item.ItemDomainHelper.DataIds.SkillBooks=10;
            public static void Prefix(GameData.Domains.Taiwu.TaiwuDomain __instance, GameData.Common.DataContext context, ref GameData.Domains.Item.ItemKey ____curReadingBook, ref Dictionary<GameData.Domains.Item.ItemKey, GameData.Domains.Taiwu.ReadingBookStrategies> ____readingBooks, GameData.Domains.Item.ItemKey[] ____referenceBooks)
            {
                if (block)
                {
                    block = false;

                    // pre

                    var taiwu = __instance.GetTaiwu();
                    var curr = __instance.GetCurReadingBook();
                    var inventory = taiwu.GetInventory().Items;
                    var lst = new List<GameData.Domains.Item.ItemKey>();

                    GameData.Domains.Item.ItemKey[] keys = new GameData.Domains.Item.ItemKey[____referenceBooks.Length];
                    for (int i = 0; i < ____referenceBooks.Length; i++)
                    {
                        keys[i] = ____referenceBooks[i];
                        ____referenceBooks[i] = GameData.Domains.Item.ItemKey.Invalid;
                    }

                    // process

                    foreach (var key in inventory.Keys)
                    { // 防止迭代器失效
                        if (key.ItemType == SkillBooks)
                        {
                            if (key.Id != curr.Id) lst.Add(key);
                        }
                    }
                    foreach (var item in lst)
                    {
                        ____curReadingBook = item;
                        if (!____readingBooks.ContainsKey(____curReadingBook)) ____readingBooks[____curReadingBook] = default(GameData.Domains.Taiwu.ReadingBookStrategies); // 如果没有读书策略，会红字
                        __instance.UpdateReadingProgressInCombat(context); // 因为现在block是false，所以可以直接通过
                    }

                    // post

                    for (int i = 0; i < ____referenceBooks.Length; i++)
                    {
                        if (keys[i].IsValid() && GameData.Domains.DomainManager.Item.ItemExists(keys[i]))
                            ____referenceBooks[i] = keys[i];
                    }
                    ____curReadingBook = curr;
                    if (!____readingBooks.ContainsKey(curr)) ____readingBooks[curr] = default(GameData.Domains.Taiwu.ReadingBookStrategies); // 不知道为什么这个东西会红
                    block = true;
                }
            }
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                return instructions.MethodReplacer(typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("SetReadingEventTriggered"), typeof(LuckyReadingReadsAll).GetMethod("SetReadingEventTriggered"));
            }
            public static void SetReadingEventTriggered(GameData.Domains.Taiwu.TaiwuDomain __instance, bool value, GameData.Common.DataContext context) { }
        }
        public class YaExtendBreak
        {
            [HarmonyPatch(typeof(GameData.Domains.Taiwu.BreakPlateRegions), "IsBonusGridBornRegionPos")]
            public static bool Postfix(bool __result, int plateWidth, int plateHeight, ValueTuple<byte, byte> pos)
            {
                return pos.Item2 < plateHeight - (1 & ~pos.Item1);
            }
            [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "InitSkillBreakPlate")]
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldc_I4_3),
                        new CodeMatch(OpCodes.Stloc_2)
                    ).Repeat(matcher => // Do the following for each match
                        matcher.SetAndAdvance(
                            Op_instruction, null
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "InitSkillBreakPlate")]
        public class ExtendBreak
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(i => i.opcode == OpCodes.Ldfld && ((FieldInfo)i.operand == typeof(Config.SkillBreakPlateItem).GetField("PlateWidth") || (FieldInfo)i.operand == typeof(Config.SkillBreakPlateItem).GetField("PlateHeight")))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.Advance(1).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_3),
                            new CodeInstruction(OpCodes.Add)
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }

        [HarmonyPatch(typeof(GameData.Domains.CombatSkill.CombatSkillDomain), "GetBonusBreakGrids")]
        public class AllYellowPerk
        {
            public static bool Prefix(ref List<Config.BreakGrid> __result, short skillTemplateId, sbyte behaviorType)
            {
                Config.SkillBreakGridListItem configData = Config.SkillBreakGridList.Instance[skillTemplateId];
                switch (behaviorType)
                {
                    case 0:
                        __result = configData.BreakGridListJust;
                        break;
                    case 1:
                        __result = configData.BreakGridListKind;
                        break;
                    case 2:
                        __result = configData.BreakGridListEven;
                        break;
                    case 3:
                        __result = configData.BreakGridListRebel;
                        break;
                    case 4:
                        __result = configData.BreakGridListEgoistic;
                        break;
                    default:
                        __result = null;
                        return false;
                }
                __result = new List<Config.BreakGrid>(__result);

                foreach (List<Config.BreakGrid> list in new List<Config.BreakGrid>[] { configData.BreakGridListJust, configData.BreakGridListKind, configData.BreakGridListEven, configData.BreakGridListRebel, configData.BreakGridListEgoistic })
                {
                    foreach (Config.BreakGrid item in list)
                    {
                        bool flag = true;
                        for (int i = 0; i < __result.Count; i++)
                        {
                            if (__result[i].BonusType == item.BonusType)
                            {
                                if (__result[i].GridCount < item.GridCount)
                                {
                                    __result[i] = item;
                                }
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            __result.Add(item);
                        }
                    }
                }
                switch (Config.CombatSkill.Instance[skillTemplateId].EquipType)
                {
                    case 0:
                        for (int i = Math.Max(Config.CombatSkill.Instance[skillTemplateId].GridCost - 1, (sbyte)0); i >= 0; i--) __result.AddRange(AdditionalGrids0);
                        break;
                    case 1:
                        for (int i = Math.Max(Config.CombatSkill.Instance[skillTemplateId].GridCost - 1, (sbyte)0); i >= 0; i--) __result.AddRange(AdditionalGrids1);
                        break;
                    case 2:
                        for (int i = Math.Max(Config.CombatSkill.Instance[skillTemplateId].GridCost - 1, (sbyte)0); i >= 0; i--) __result.AddRange(AdditionalGrids2);
                        break;
                    case 3:
                        for (int i = Math.Max(Config.CombatSkill.Instance[skillTemplateId].GridCost - 1, (sbyte)0); i >= 0; i--) __result.AddRange(AdditionalGrids3);
                        break;
                    case 4:
                        for (int i = Math.Max(Config.CombatSkill.Instance[skillTemplateId].GridCost - 1, (sbyte)0); i >= 0; i--) __result.AddRange(AdditionalGrids4);
                        break;
                    default:
                        break;
                }
                if (yellowFill.BonusType >= 0)
                {
                    __result.Add(yellowFill);
                }
                return false;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "UpdateCombatSkillBookReadingProgress")]
        public class TaiwuDomainUpdateCombatSkillBookReadingProgress
        {
            public static MethodInfo GetTaiwuCombatSkill = typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("GetTaiwuCombatSkill", (BindingFlags)(-1));
            public static MethodInfo SetCombatSkillPageComplete = typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("SetCombatSkillPageComplete", (BindingFlags)(-1));
            public static MethodInfo SetTaiwuCombatSkill = typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("SetTaiwuCombatSkill", (BindingFlags)(-1));
            public static void Postfix(GameData.Domains.Taiwu.TaiwuDomain __instance, GameData.Common.DataContext context, GameData.Domains.Item.SkillBook book)
            {
                short skillTemplateId = book.GetCombatSkillTemplateId();
                GameData.Domains.Taiwu.TaiwuCombatSkill taiwuCombatSkill = (GameData.Domains.Taiwu.TaiwuCombatSkill)GetTaiwuCombatSkill.Invoke(__instance, new object[] { skillTemplateId });
                for (byte i = 0; i < 15; i++)
                {
                    if ((bool)typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("OfflineAddReadingProgress", (BindingFlags)(-1)).Invoke(__instance, new object[] { taiwuCombatSkill, i, knowOther }))
                    {
                        SetTaiwuCombatSkill.Invoke(__instance, new object[] { context, skillTemplateId, taiwuCombatSkill });
                        //                    taiwuCombatSkill.SetReadingState(taiwuCombatSkill.GetReadingState() | (((ushort)1)<<i),context);
                        SetCombatSkillPageComplete.Invoke(__instance, new object[] { context, book, i });
                        //                    taiwuCombatSkill.SetBookPageReadingProgress(i,(sbyte)100);
                        //                    GameData.Domains.CombatSkill.CombatSkill skillItem = GameData.Domains.DomainManager.CombatSkill.GetElement_CombatSkills(new GameData.Domains.CombatSkill.CombatSkillKey(__instance.GetTaiwuCharId(), skillTemplateId));
                        //                    skillItem.SetReadingState(GameData.Domains.CombatSkill.CombatSkillStateHelper.SetPageRead(skillItem.GetReadingState(), i), context);
                    }
                    else
                    {
                        SetTaiwuCombatSkill.Invoke(__instance, new object[] { context, skillTemplateId, taiwuCombatSkill });
                    }
                }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Combat.CombatDomain), "CalcLootItem")]
        public class ZhouBaPi
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Call, typeof(GameData.Utilities.RedzenHelper).GetMethod("CheckPercentProb"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.InsertAndAdvance(
                            new CodeInstruction(OpCodes.Pop),
                            new CodeInstruction(OpCodes.Ldc_I4_S, 100)
                        ).Advance(1)
                    ).InstructionEnumeration();
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Call, typeof(Math).GetMethod("Min", new Type[] { typeof(int), typeof(int) }))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.SetAndAdvance(
                            OpCodes.Pop, null
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Character.Character), "AdjustLifespan")]
        public class HuanYue
        {
            public static void Prefix(ref short ____health, ref short ____baseMaxHealth, short ____templateId)
            {
                if (____templateId == StoryHuanyue)
                {
                    ____baseMaxHealth = Math.Max((short)HuanYueNine, ____baseMaxHealth);
                    ____health = 32767; // 反正后面有一手clamp
                }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Character.Character), "GenerateRandomBasicFeatures")]
        //    [HarmonyPatch(typeof(GameData.Domains.Character.Character),"OfflineCreateProtagonistRandomFeatures")]
        public class AllFeature
        {
            //        public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions) {
            //            return Transpilers.MethodReplacer(instructions,typeof(GameData.Domains.Character.Character).GetMethod("GenerateRandomBasicFeatures",(BindingFlags)(-1)),typeof(AllFeature).GetMethod("MG"));
            //        }
            public static unsafe bool Prefix(Dictionary<short, short> featureGroup2Id, bool isProtagonist, bool allGoodBasicFeatures, ref GameData.Domains.Character.LifeSkillShorts ____baseLifeSkillQualifications, ref GameData.Domains.Character.CombatSkillShorts ____baseCombatSkillQualifications, ref GameData.Domains.Character.MainAttributes ____baseMainAttributes, ref GameData.Domains.Character.LifeSkillShorts ____lifeSkillQualifications, ref GameData.Domains.Character.CombatSkillShorts ____combatSkillQualifications)
            {
                //            if(____templateId==StoryHuanyue){
                //                ____maxHealth=Math.Max((short)HuanYueNine,____maxHealth);
                //                return true;
                //            }else
                if (!(isProtagonist || allGoodBasicFeatures))
                {
                    return true;
                }
                fixed (short* ptr = ____baseLifeSkillQualifications.Items) for (int i = 0; i < 16; i++)
                    {
                        *(ptr + i) = Math.Max(*(ptr + i), (short)minLife);
                    }
                fixed (short* ptr = ____baseCombatSkillQualifications.Items) for (int i = AFBegin; i < 14; i++)
                    { // avoid finger check
                        *(ptr + i) = Math.Max(*(ptr + i), (short)minCombat);
                    }
                fixed (short* ptr = ____baseMainAttributes.Items) for (int i = 0; i < 6; i++)
                    {
                        *(ptr + i) = Math.Max(*(ptr + i), (short)minMain);
                    }
                ____lifeSkillQualifications = ____baseLifeSkillQualifications;
                ____combatSkillQualifications = ____baseCombatSkillQualifications;

                var alist = new List<short>();
                var yalist = new List<short>();
                foreach (Config.CharacterFeatureItem item in ((IEnumerable<Config.CharacterFeatureItem>)Config.CharacterFeature.Instance))
                {
                    if (item != null)
                    {
                        if (item.Basic)
                        {
                            if (item.CandidateGroupId == 0)
                            {
                                alist.Add(item.TemplateId);
                            }
                            else if (item.CandidateGroupId == 1)
                            {
                                yalist.Add(item.TemplateId);
                                //for (int k = 0; k < (int)item.AppearProb; k++)
                                //{
                                //    CharacterDomain._normalNegativeBasicFeaturesPool.Add(item.TemplateId);
                                //}
                                //for (int l = 0; l < (int)item.ProtagonistAppearProb; l++)
                                //{
                                //    CharacterDomain._protagonistNegativeBasicFeaturesPool.Add(item.TemplateId);
                                //}
                            }
                            else
                            {
                                // alist.Add(item.TemplateId); // 无根之人 石芯玉女 阴阳一体
                            }
                        }
                    }
                }
                if (WuLinTraitLevel > 0)
                {
                    alist.Add((short)(DefKey_HaveElixir2 + WuLinTraitLevel - 3));
                    alist.Add((short)(DefKey_GreenMotherSpiderPoison2 + WuLinTraitLevel - 3));
                    alist.Add((short)(DefKey_StarsRegulateBreath2 + WuLinTraitLevel - 3));
                    alist.Add((short)(DefKey_SupportFromShixiang2 + WuLinTraitLevel - 3));
                }
                var pos = new Dictionary<short, short>();
                var neg = new Dictionary<short, short>();
                foreach (short featureId in alist)
                {
                    Config.CharacterFeatureItem template = Config.CharacterFeature.Instance[featureId];
                    if ((!pos.ContainsKey(template.MutexGroupId)) || featureId > pos[template.MutexGroupId])
                    {
                        pos[template.MutexGroupId] = featureId;
                    }
                }
                foreach (short featureId in yalist)
                {
                    Config.CharacterFeatureItem template = Config.CharacterFeature.Instance[featureId];
                    if ((!neg.ContainsKey(template.MutexGroupId)) || featureId > neg[template.MutexGroupId])
                    {
                        neg[template.MutexGroupId] = featureId;
                    }
                }
                // 如果你准备删除多余三级特性

                int posc = N01APC == 30 ? pos.Count : Math.Min(pos.Count, N01APC);
                int negc = N01ANC == 30 ? neg.Count : Math.Min(neg.Count, N01ANC);
                Random random = new Random();
                while (posc > 0 || negc > 0)
                {
                    Dictionary<short, short> dict;
                    if (posc >= negc)
                    {
                        dict = pos;
                        posc--;
                    }
                    else
                    {
                        dict = neg;
                        negc--;
                    }
                    if (dict.Count > 0)
                    {
                        int index = random.Next(dict.Count);
                        using (var enumerator = dict.Keys.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                if (index <= 0)
                                {
                                    featureGroup2Id[enumerator.Current] = dict[enumerator.Current];
                                    pos.Remove(enumerator.Current);
                                    neg.Remove(enumerator.Current);
                                    break;
                                }
                                index--;
                            }
                        }
                    }
                }
                if (fulongCF && allGoodBasicFeatures)
                {
                    featureGroup2Id[Config.CharacterFeature.Instance[DefKey_FulongServant].MutexGroupId] = DefKey_FulongServant;
                }
                foreach (var ids in manual_feature_id)
                {
                    featureGroup2Id[Config.CharacterFeature.Instance[ids].MutexGroupId] = ids;
                }
                return false;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "CalcPracticeResult")]
        public class FastPractice
        {
            public static int Postfix(int result)
            {
                return fpv;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.CombatSkill.CombatSkill), "GetBreakoutAvailableStepsCount")]
        public class BreakPlus
        {
            public static sbyte Postfix(sbyte result)
            {
                return (sbyte)gbasc;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Combat.CombatDomain), "CheckRopeOrSwordHit")]
        public class AlwaysCapture
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase __originalMethod, IEnumerable<CodeInstruction> instructions)
            {
                instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Call, typeof(GameData.Utilities.RedzenHelper).GetMethod("CheckPercentProb"))
                    ).Repeat(matcher => // Do the following for each match
                        matcher.InsertAndAdvance(
                            new CodeInstruction(OpCodes.Pop),
                            new CodeInstruction(OpCodes.Ldc_I4_S, 100)
                        ).Advance(1)
                    ).InstructionEnumeration();
                if (N03_2) instructions = new CodeMatcher(instructions)
                    .MatchForward(false, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldfld, typeof(GameData.Domains.Combat.CombatDomain).GetField("_combatType", (BindingFlags)(-1))),
                        new CodeMatch(OpCodes.Ldelem_I1)
                    ).Repeat(matcher => // Do the following for each match
                        matcher.SetAndAdvance(
                            OpCodes.Pop, null
                        ).SetAndAdvance(
                            OpCodes.Pop, null
                        ).InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldc_I4_0)
                        )
                    ).InstructionEnumeration();
                return instructions;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Character.Character), "OfflineMakeLove")]
        public class PatchPregnant
        {
            public static MethodInfo OfflineAddFeature = typeof(GameData.Domains.Character.Character).GetMethod("OfflineAddFeature", (BindingFlags)(-1));
            public static bool Prefix(ref bool __result, GameData.Domains.Character.Character father, GameData.Domains.Character.Character mother)
            {
                if (mother.GetFeatureIds().Contains(DefKey_Pregnant))
                {
                    __result = false;
                    OfflineAddFeature.Invoke(father, MakeLove_param);
                    //                GameData.Utilities.AdaptableLog.Info("id"+mother.GetId()+"已经怀孕");
                    return false;
                }
                else
                {
                    //                GameData.Utilities.AdaptableLog.Info("id"+mother.GetId()+"可以怀孕");
                    return true;
                }
            }
            //        public static void Postfix(ref bool __result, GameData.Domains.Character.Character father, GameData.Domains.Character.Character mother){
            //            GameData.Utilities.AdaptableLog.Info("id"+mother.GetId()+"的返回值是"+__result+"，受孕状态是"+mother.GetFeatureIds().Contains(DefKey_Pregnant));
            //        }
        }
        [HarmonyPatch(typeof(GameData.Domains.Character.Character), "OfflineCalcGeneralAction_RandomActions")]
        public class MustLearn
        {
            public static MethodInfo OfflineCalcGeneralAction_TeachSkill = typeof(GameData.Domains.Character.Character).GetMethod("OfflineCalcGeneralAction_TeachSkill", (BindingFlags)(-1));
            public static MethodInfo ApplyLucky_CombatSkillBook = typeof(GameData.Domains.Character.CharacterDomain).GetMethod("ApplyLucky_CombatSkillBook", (BindingFlags)(-1));
            public static MethodInfo ApplyLucky_LifeSkillBook = typeof(GameData.Domains.Character.CharacterDomain).GetMethod("ApplyLucky_LifeSkillBook", (BindingFlags)(-1));
            public static MethodInfo ApplyLucky_Regen = typeof(GameData.Domains.Character.CharacterDomain).GetMethod("ApplyLucky_Regen", (BindingFlags)(-1));
            public static int counter = 0;
            //public static MethodInfo OfflineCalcGeneralAction_LifeSkill=typeof(GameData.Domains.Character.Character).GetMethod("OfflineCalcGeneralAction_LifeSkill",(BindingFlags)(-1));
            public unsafe static void Prefix(GameData.Domains.Character.Character __instance, GameData.Common.DataContext context, GameData.Domains.Character.ParallelModifications.PeriAdvanceMonthGeneralActionModification mod, HashSet<int> currBlockChars, ref GameData.Domains.Character.Ai.ActionEnergySbytes ____actionEnergies)
            {
                if (currBlockChars.Contains(GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId()))
                {
                    var hs = new HashSet<int> { GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId() };
                    var hs2 = new HashSet<int> { GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId() };
                    GameData.Domains.Character.Relation.RelatedCharacter relation;
                    fixed (byte* ptr = ____actionEnergies.Items) if (GameData.Domains.DomainManager.Character.TryGetRelation(__instance.GetId(), GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId(), out relation) && ((relation.RelationType & RelationType_SwornBrotherOrSister) != 0))
                        {
                            for (int i = 0; i < totalTeachCount; i++)
                            {
                                *(ptr + 4) = 200;
                                OfflineCalcGeneralAction_TeachSkill.Invoke(__instance, new object[] { context, mod, hs, hs2 });
                                if (*(ptr + 4) == 200)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            GameData.Domains.DomainManager.Character.AddRelation(context, __instance.GetId(), GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId(), RelationType_SwornBrotherOrSister, GameData.Domains.DomainManager.World.GetCurrDate());
                            for (int i = 0; i < totalTeachCount; i++)
                            {
                                *(ptr + 4) = 200;
                                OfflineCalcGeneralAction_TeachSkill.Invoke(__instance, new object[] { context, mod, hs, hs2 });
                                if (*(ptr + 4) == 200)
                                {
                                    break;
                                }
                            }
                            GameData.Domains.DomainManager.Character.ChangeRelationType(context, __instance.GetId(), GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId(), RelationType_SwornBrotherOrSister, 0);
                            GameData.Domains.DomainManager.Character.ChangeRelationType(context, GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId(), __instance.GetId(), RelationType_SwornBrotherOrSister, 0);
                        }
                    counter++;
                    if (counter >= currBlockChars.Count - 1) { counter = 0; } else { return; }
                    var luckyChars = new List<int>() { GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId() };
                    if (GameData.Utilities.RedzenHelper.CheckPercentProb(context.Random, pCombat2)) for (int i = 0; i < cCombat; i++) ApplyLucky_CombatSkillBook.Invoke(GameData.Domains.DomainManager.Character, new object[] { context, luckyChars, 1, (sbyte)pCombat });
                    if (GameData.Utilities.RedzenHelper.CheckPercentProb(context.Random, pLife2)) for (int i = 0; i < cLife; i++) ApplyLucky_LifeSkillBook.Invoke(GameData.Domains.DomainManager.Character, new object[] { context, luckyChars, 1, (sbyte)pLife });
                    if (GameData.Utilities.RedzenHelper.CheckPercentProb(context.Random, pRegen2)) for (int i = 0; i < cRegen; i++) ApplyLucky_Regen.Invoke(GameData.Domains.DomainManager.Character, new object[] { context, luckyChars, 1, (sbyte)pRegen });
                }
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(GameData.Domains.Character.Ai.AiHelper.GeneralActionConstants), "GetAskToTeachSkillRespondChance")]
            public static sbyte HackChance(sbyte result)
            {
                //GameData.Utilities.AdaptableLog.Info("检查了传授概率，返回100.");
                return (sbyte)100;
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "SetTaiwu")]
        public class TaiwuLegency
        {
            public static void Prefix(GameData.Common.DataContext context, GameData.Domains.Character.Character newTaiwuChar, GameData.Domains.Character.Character ____taiwuChar)
            {
                if (____taiwuChar == null) return;
                foreach (var feature in ____taiwuChar.GetFeatureIds())
                {
                    if (Config.CharacterFeature.Instance[feature].GeneticProb > 0)
                    {
                        newTaiwuChar.AddFeature(context, feature, twoverwrite);
                    }
                }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "SetTaiwu")]
        public class TaiwuLegencyAttribute
        {
            public unsafe static void Prefix(GameData.Common.DataContext context, GameData.Domains.Character.Character newTaiwuChar, GameData.Domains.Character.Character ____taiwuChar)
            {
                if (____taiwuChar == null) return;
                {
                    var x = newTaiwuChar.GetBaseLifeSkillQualifications();
                    var y = ____taiwuChar.GetBaseLifeSkillQualifications();
                    for (int i = 0; i < 16; i++)
                    {
                        *(x.Items + i) = Math.Max(*(x.Items + i), (short)(((int)(*(y.Items + i))) * inhertlife / 10000));
                    }
                    newTaiwuChar.SetBaseLifeSkillQualifications(ref x, context);
                }
                {
                    var x = newTaiwuChar.GetBaseCombatSkillQualifications();
                    var y = ____taiwuChar.GetBaseCombatSkillQualifications();
                    for (int i = 0; i < 14; i++)
                    {
                        *(x.Items + i) = Math.Max(*(x.Items + i), (short)(((int)(*(y.Items + i))) * inhertcombat / 10000));
                    }
                    newTaiwuChar.SetBaseCombatSkillQualifications(ref x, context);
                }
                {
                    var x = newTaiwuChar.GetBaseMainAttributes();
                    var y = ____taiwuChar.GetBaseMainAttributes();
                    for (int i = 0; i < 16; i++)
                    {
                        *(x.Items + i) = Math.Max(*(x.Items + i), (short)(((int)(*(y.Items + i))) * inhertmain / 10000));
                    }
                    newTaiwuChar.SetBaseMainAttributes(x, context);
                }
            }
        }
        [HarmonyPatch(typeof(GameData.Domains.Taiwu.TaiwuDomain), "ClearBreakPlate")]
        public class Printing
        {
            //public static MethodInfo GetTaiwuCombatSkill=typeof(GameData.Domains.Taiwu.TaiwuDomain).GetMethod("GetTaiwuCombatSkill",(BindingFlags)(-1));
            public static void Postfix(short skillTemplateId, GameData.Common.DataContext context)
            {
                GameData.Domains.CombatSkill.CombatSkill cs;
                if (GameData.Domains.DomainManager.CombatSkill.TryGetElement_CombatSkills(new GameData.Domains.CombatSkill.CombatSkillKey(GameData.Domains.DomainManager.Taiwu.GetTaiwuCharId(), skillTemplateId), out cs))
                {
                    var state = cs.GetActivationState();
                    state -= (ushort)(state & 0x5555);
                    state = (ushort)((state >> 2) & 0x3333 + state & 0x3333);
                    state = (ushort)(((state >> 4) + state) & 0x0F0F);
                    state = (ushort)(((state >> 8) + state) & 0x00FF);
                    if (state == 6)
                    {
                        var key = GameData.Domains.DomainManager.Item.CreateSkillBook(context, skillTemplateId, cs.GetActivationState());
                        GameData.Domains.DomainManager.Taiwu.GetTaiwu().AddInventoryItem(context, key, 1);
                    }
                }
            }
        }
    }
}