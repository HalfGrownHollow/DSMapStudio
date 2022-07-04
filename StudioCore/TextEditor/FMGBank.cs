﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using SoulsFormats;
using System.Threading.Tasks;
using StudioCore.Editor;
using System.Windows.Forms;
namespace StudioCore.TextEditor
{
    /// <summary>
    /// Static class that stores all the strings for a Souls game
    /// </summary>
    public static class FMGBank
    {
        public enum ItemCategory
        {
            None,
            Goods,
            Weapons,
            Armor,
            Rings,
            Spells,
            Characters,
            Locations,
            Gem,
            Message,
            SwordArts,
            Effect,
            Misc,
        }

        public enum ItemType
        {
            None,
            Title,
            Summary,
            Description,
        }

        public enum FMGTypes
        {
            Item = 0,
            Menu = 1
        }

        /// <summary>
        /// BND IDs for menu fmg files usually in menu.msgbnd
        /// </summary>
        public enum MenuFMGTypes
        {
            None = -1,

            NpcDialog = 1,
            BloodMessage = 2,
            MovieSubtitle = 3,
            Event = 30,
            MenuInGame = 70,
            MenuCommon = 76,
            MenuOther = 77,
            MenuDialog = 78,
            MenuKeyGuide = 79,
            MenuOneLine = 80,
            MenuContext = 81,
            MenuTags = 90,

            //DS1
            Win32Tags = 91,
            Win32Messages = 92,
            EventPatch = 101,
            MenuDialogPatch = 102,
            Win32MessagesPatch = 103,
            NpcDialogPatch = 104,
            BloodMessagePatch = 107,
            MenuOneLinePatch = 121,
            MenuKeyGuidePatch = 122,
            MenuOtherPatch = 123,
            MenuCommonPatch = 124,

            //DS3 DLC1
            FDP_メニューテキスト = 200,
            FDP_一行ヘルプ = 201,
            FDP_キーガイド = 202,
            FDP_システムメッセージ_win64 = 203,
            FDP_ダイアログ = 204,
            FDP_システムメッセージ_ps4 = 205,
            FDP_システムメッセージ_xboxone = 206,
            会話_dlc1 = 230,
            イベントテキスト_dlc1 = 231,
            FDP_メニューテキスト_dlc1 = 232,
            FDP_一行ヘルプ_dlc1 = 233,
            FDP_システムメッセージ_win64_dlc1 = 235,
            FDP_ダイアログ_dlc1 = 236,
            FDP_システムメッセージ_ps4_dlc1 = 237,
            FDP_システムメッセージ_xboxone_dlc1 = 238,
            血文字_dlc1 = 239,
            //DS3 DLC2
            会話_dlc2 = 270,
            イベントテキスト_dlc2 = 271,
            FDP_メニューテキスト_dlc2 = 272,
            FDP_一行ヘルプ_dlc2 = 273,
            FDP_システムメッセージ_win64_dlc2 = 275,
            FDP_ダイアログ_dlc2 = 276,
            FDP_システムメッセージ_ps4_dlc2 = 277,
            FDP_システムメッセージ_xboxone_dlc2 = 278,
            血文字_dlc2 = 279,

            //ER
            NetworkMessage = 31,
            ActionButtonText = 32,
            EventTextForTalk = 33,
            EventTextForMap = 34,
            TutorialTitle = 207,
            TutorialBody = 208,
            TextEmbedImageName_win64 = 209,
            ToS_win64 = 210,
        }

        /// <summary>
        /// BND IDs for item fmg files usually in item.msgbnd
        /// </summary>
        public enum ItemFMGTypes
        {
            TitleGoods = 10,
            TitleWeapons = 11,
            TitleArmor = 12,
            TitleRings = 13,
            TitleSpells = 14,
            TitleTest = 15,
            TitleTest2 = 16,
            TitleTest3 = 17,
            TitleCharacters = 18,
            TitleLocations = 19,
            SummaryGoods = 20,
            SummaryWeapons = 21,
            SummaryArmor = 22,
            SummaryRings = 23,
            DescriptionGoods = 24,
            DescriptionWeapons = 25,
            DescriptionArmor = 26,
            DescriptionRings = 27,
            SummarySpells = 28,
            DescriptionSpells = 29,
            DescriptionSkills = 40,

            // DS1 DLC
            DescriptionGoodsPatch = 100,
            DescriptionSpellsPatch = 105,
            DescriptionWeaponsPatch = 106,
            DescriptionArmorPatch = 108,
            DescriptionRingsPatch = 109,
            SummaryGoodsPatch = 110,
            TitleGoodsPatch = 111,
            SummaryRingsPatch = 112,
            TitleRingsPatch = 113,
            SummaryWeaponsPatch = 114,
            TitleWeaponsPatch = 115,
            SummaryArmorPatch = 116,
            TitleArmorPatch = 117,
            TitleSpellsPatch = 118,
            TitleCharactersPatch = 119,
            TitleLocationsPatch = 120,

            // DS3 DLC1
            TitleGoodsDLC1 = 210,
            TitleWeaponsDLC1 = 211,
            TitleArmorDLC1 = 212,
            TitleRingsDLC1 = 213,
            TitleSpellsDLC1 = 214,
            TitleCharactersDLC1 = 215,
            TitleLocationsDLC1 = 216,
            SummaryGoodsDLC1 = 217,
            SummaryRingsDLC1 = 220,
            DescriptionGoodsDLC1 = 221,
            DescriptionWeaponsDLC1 = 222,
            DescriptionArmorDLC1 = 223,
            DescriptionRingsDLC1 = 224,
            SummarySpellsDLC1 = 225,
            DescriptionSpellsDLC1 = 226,

            // DS3 DLC2
            TitleGoodsDLC2 = 250,
            TitleWeaponsDLC2 = 251,
            TitleArmorDLC2 = 252,
            TitleRingsDLC2 = 253,
            TitleSpellsDLC2 = 254,
            TitleCharactersDLC2 = 255,
            TitleLocationsDLC2 = 256,
            SummaryGoodsDLC2 = 257,
            SummaryRingsDLC2 = 260,
            DescriptionGoodsDLC2 = 261,
            DescriptionWeaponsDLC2 = 262,
            DescriptionArmorDLC2 = 263,
            DescriptionRingsDLC2 = 264,
            SummarySpellsDLC2 = 265,
            DescriptionSpellsDLC2 = 266,

            // ER
            TitleGem = 35,
            SummaryGem = 36,
            DescriptionGem = 37,
            TitleMessage = 41,
            TitleSwordArts = 42,
            SummarySwordArts = 43,
            SummaryEffect = 44,
            ERUnk45 = 45,
            SummaryMiscER = 46,
        }

        public static AssetLocator AssetLocator = null;

        private static Dictionary<ItemFMGTypes, FMG> _itemFMGs = null;
        private static Dictionary<MenuFMGTypes, FMG> _menuFMGs = null;
        private static string _languageFolder = "";
        /// <summary>
        /// DS2 uses a different system with loose fmgs instead of IDs
        /// </summary>
        private static Dictionary<string, FMG> _ds2fmgs = null;

        public static bool IsLoaded { get; private set; } = false;
        public static bool IsLoading { get; private set; } = false;

        public static IReadOnlyDictionary<string, FMG> DS2Fmgs
        {
            get
            {
                return _ds2fmgs;
            }
        }

        public static ItemCategory ItemCategoryOf(ItemFMGTypes ftype)
        {
            switch (ftype)
            {
                case ItemFMGTypes.TitleTest:
                case ItemFMGTypes.TitleTest2:
                case ItemFMGTypes.TitleTest3:
                case ItemFMGTypes.DescriptionSkills:
                case ItemFMGTypes.ERUnk45:
                    return ItemCategory.None;
                
                case ItemFMGTypes.DescriptionGoods:
                case ItemFMGTypes.DescriptionGoodsDLC1:
                case ItemFMGTypes.DescriptionGoodsDLC2:
                case ItemFMGTypes.SummaryGoods:
                case ItemFMGTypes.SummaryGoodsDLC1:
                case ItemFMGTypes.SummaryGoodsDLC2:
                case ItemFMGTypes.TitleGoods:
                case ItemFMGTypes.TitleGoodsDLC1:
                case ItemFMGTypes.TitleGoodsDLC2:
                case ItemFMGTypes.DescriptionGoodsPatch:
                case ItemFMGTypes.SummaryGoodsPatch:
                case ItemFMGTypes.TitleGoodsPatch:
                    return ItemCategory.Goods;

                case ItemFMGTypes.DescriptionWeapons:
                case ItemFMGTypes.DescriptionWeaponsDLC1:
                case ItemFMGTypes.DescriptionWeaponsDLC2:
                case ItemFMGTypes.SummaryWeapons:
                case ItemFMGTypes.TitleWeapons:
                case ItemFMGTypes.TitleWeaponsDLC1:
                case ItemFMGTypes.TitleWeaponsDLC2:
                case ItemFMGTypes.DescriptionWeaponsPatch:
                case ItemFMGTypes.SummaryWeaponsPatch:
                case ItemFMGTypes.TitleWeaponsPatch:
                    return ItemCategory.Weapons;

                case ItemFMGTypes.DescriptionArmor:
                case ItemFMGTypes.DescriptionArmorDLC1:
                case ItemFMGTypes.DescriptionArmorDLC2:
                case ItemFMGTypes.SummaryArmor:
                case ItemFMGTypes.TitleArmor:
                case ItemFMGTypes.TitleArmorDLC1:
                case ItemFMGTypes.TitleArmorDLC2:
                case ItemFMGTypes.DescriptionArmorPatch:
                case ItemFMGTypes.SummaryArmorPatch:
                case ItemFMGTypes.TitleArmorPatch:
                    return ItemCategory.Armor;

                case ItemFMGTypes.DescriptionRings:
                case ItemFMGTypes.DescriptionRingsDLC1:
                case ItemFMGTypes.DescriptionRingsDLC2:
                case ItemFMGTypes.SummaryRings:
                case ItemFMGTypes.SummaryRingsDLC1:
                case ItemFMGTypes.SummaryRingsDLC2:
                case ItemFMGTypes.TitleRings:
                case ItemFMGTypes.TitleRingsDLC1:
                case ItemFMGTypes.TitleRingsDLC2:
                case ItemFMGTypes.DescriptionRingsPatch:
                case ItemFMGTypes.SummaryRingsPatch:
                case ItemFMGTypes.TitleRingsPatch:
                    return ItemCategory.Rings;

                case ItemFMGTypes.DescriptionSpells:
                case ItemFMGTypes.DescriptionSpellsDLC1:
                case ItemFMGTypes.DescriptionSpellsDLC2:
                case ItemFMGTypes.SummarySpells:
                case ItemFMGTypes.SummarySpellsDLC1:
                case ItemFMGTypes.SummarySpellsDLC2:
                case ItemFMGTypes.TitleSpells:
                case ItemFMGTypes.TitleSpellsDLC1:
                case ItemFMGTypes.TitleSpellsDLC2:
                case ItemFMGTypes.DescriptionSpellsPatch:
                case ItemFMGTypes.TitleSpellsPatch:
                    return ItemCategory.Spells;

                case ItemFMGTypes.TitleCharacters:
                case ItemFMGTypes.TitleCharactersDLC1:
                case ItemFMGTypes.TitleCharactersDLC2:
                case ItemFMGTypes.TitleCharactersPatch:
                    return ItemCategory.Characters;

                case ItemFMGTypes.TitleLocations:
                case ItemFMGTypes.TitleLocationsDLC1:
                case ItemFMGTypes.TitleLocationsDLC2:
                case ItemFMGTypes.TitleLocationsPatch:
                    return ItemCategory.Locations;

                case ItemFMGTypes.TitleGem:
                case ItemFMGTypes.SummaryGem:
                case ItemFMGTypes.DescriptionGem:
                    return ItemCategory.Gem;

                case ItemFMGTypes.TitleSwordArts:
                case ItemFMGTypes.SummarySwordArts:
                    return ItemCategory.SwordArts;

                case ItemFMGTypes.TitleMessage:
                    return ItemCategory.Message;

                case ItemFMGTypes.SummaryEffect:
                    return ItemCategory.Effect;

                case ItemFMGTypes.SummaryMiscER:
                    return ItemCategory.Misc;
                default:
                    //throw new Exception("Unrecognized FMG type");
                    return ItemCategory.None;
            }
        }

        public static ItemType ItemTypeOf(ItemFMGTypes ftype)
        {
            switch (ftype)
            {
                case ItemFMGTypes.DescriptionGoods:
                case ItemFMGTypes.DescriptionGoodsDLC1:
                case ItemFMGTypes.DescriptionGoodsDLC2:
                case ItemFMGTypes.DescriptionWeapons:
                case ItemFMGTypes.DescriptionWeaponsDLC1:
                case ItemFMGTypes.DescriptionWeaponsDLC2:
                case ItemFMGTypes.DescriptionArmor:
                case ItemFMGTypes.DescriptionArmorDLC1:
                case ItemFMGTypes.DescriptionArmorDLC2:
                case ItemFMGTypes.DescriptionRings:
                case ItemFMGTypes.DescriptionRingsDLC1:
                case ItemFMGTypes.DescriptionRingsDLC2:
                case ItemFMGTypes.DescriptionSpells:
                case ItemFMGTypes.DescriptionSpellsDLC1:
                case ItemFMGTypes.DescriptionSpellsDLC2:
                case ItemFMGTypes.DescriptionSkills:
                case ItemFMGTypes.DescriptionArmorPatch:
                case ItemFMGTypes.DescriptionGoodsPatch:
                case ItemFMGTypes.DescriptionRingsPatch:
                case ItemFMGTypes.DescriptionSpellsPatch:
                case ItemFMGTypes.DescriptionWeaponsPatch:
                case ItemFMGTypes.DescriptionGem:
                    return ItemType.Description;
                case ItemFMGTypes.SummaryGoods:
                case ItemFMGTypes.SummaryGoodsDLC1:
                case ItemFMGTypes.SummaryGoodsDLC2:
                case ItemFMGTypes.SummaryWeapons:
                case ItemFMGTypes.SummaryArmor:
                case ItemFMGTypes.SummaryRings:
                case ItemFMGTypes.SummaryRingsDLC1:
                case ItemFMGTypes.SummaryRingsDLC2:
                case ItemFMGTypes.SummarySpells:
                case ItemFMGTypes.SummarySpellsDLC1:
                case ItemFMGTypes.SummarySpellsDLC2:
                case ItemFMGTypes.SummaryArmorPatch:
                case ItemFMGTypes.SummaryGoodsPatch:
                case ItemFMGTypes.SummaryRingsPatch:
                case ItemFMGTypes.SummaryWeaponsPatch:
                case ItemFMGTypes.SummaryGem:
                case ItemFMGTypes.SummarySwordArts:
                    return ItemType.Summary;
                case ItemFMGTypes.TitleGoods:
                case ItemFMGTypes.TitleGoodsDLC1:
                case ItemFMGTypes.TitleGoodsDLC2:
                case ItemFMGTypes.TitleWeapons:
                case ItemFMGTypes.TitleWeaponsDLC1:
                case ItemFMGTypes.TitleWeaponsDLC2:
                case ItemFMGTypes.TitleArmor:
                case ItemFMGTypes.TitleArmorDLC1:
                case ItemFMGTypes.TitleArmorDLC2:
                case ItemFMGTypes.TitleRings:
                case ItemFMGTypes.TitleRingsDLC1:
                case ItemFMGTypes.TitleRingsDLC2:
                case ItemFMGTypes.TitleSpells:
                case ItemFMGTypes.TitleSpellsDLC1:
                case ItemFMGTypes.TitleSpellsDLC2:
                case ItemFMGTypes.TitleCharacters:
                case ItemFMGTypes.TitleCharactersDLC1:
                case ItemFMGTypes.TitleCharactersDLC2:  
                case ItemFMGTypes.TitleLocations:
                case ItemFMGTypes.TitleLocationsDLC1:
                case ItemFMGTypes.TitleLocationsDLC2:
                case ItemFMGTypes.TitleTest:
                case ItemFMGTypes.TitleTest2:
                case ItemFMGTypes.TitleTest3:
                case ItemFMGTypes.TitleArmorPatch:
                case ItemFMGTypes.TitleCharactersPatch:
                case ItemFMGTypes.TitleGoodsPatch:
                case ItemFMGTypes.TitleLocationsPatch:
                case ItemFMGTypes.TitleRingsPatch:
                case ItemFMGTypes.TitleSpellsPatch:
                case ItemFMGTypes.TitleWeaponsPatch:
                case ItemFMGTypes.TitleGem:
                case ItemFMGTypes.TitleMessage:
                case ItemFMGTypes.TitleSwordArts:
                //Summaries without titles
                case ItemFMGTypes.SummaryEffect:
                case ItemFMGTypes.SummaryMiscER:
                    return ItemType.Title;
                case ItemFMGTypes.ERUnk45:
                    return ItemType.None;
                default:
                    //throw new Exception("Unrecognized FMG type");
                    return ItemType.None;
            }
        }

        public static List<FMG.Entry> GetItemFMGEntriesByType(ItemCategory cat, ItemType type)
        {
            var list = new List<FMG.Entry>();
            foreach (var fmg in _itemFMGs)
            {
                if (ItemCategoryOf(fmg.Key) == cat && ItemTypeOf(fmg.Key) == type)
                {
                    foreach (var entry in fmg.Value.Entries)
                    {
                        var oldEntry = list.Find(e => e.ID == entry.ID);
                        if (oldEntry != null)
                        {
                            //List already has this ID, so this is probably a patch entry. Replace the old one if the new one isn't null
                            if (entry.Text != null)
                            {
                                list.Remove(oldEntry);
                                list.Add(entry);
                            }
                        }
                        else
                        {
                            list.Add(entry);
                        }
                    }
                }
            }
            list = list.OrderBy(e => e.ID).ToList();
            return list;
        }

        public static Dictionary<ItemFMGTypes, FMG> GetItemFMGs()
        {
            return _itemFMGs;
        }

        public static Dictionary<MenuFMGTypes,FMG> GetMenuFMGs()
        {
            return _menuFMGs;
        }
        public static List<FMG.Entry> GetMenuFMGEntries(FMG fmg)
        {
            var list = new List<FMG.Entry>();
            foreach (var entry in fmg.Entries)
            {
                var oldEntry = list.Find(e => e.ID == entry.ID);
                if (oldEntry != null)
                {
                    //List already has this ID, so this is probably a patch entry. Replace the old one if the new one isn't null
                    if (entry.Text != null)
                    {
                        list.Remove(oldEntry);
                        list.Add(entry);
                    }
                }
                else
                {
                    list.Add(entry);
                }
            }
            list = list.OrderBy(e => e.ID).ToList();
            return list;
        }


        public static void LookupItemID(int id, ItemCategory cat, out FMG.Entry title, out FMG.Entry summary, out FMG.Entry description)
        {
            title = null;
            summary = null;
            description = null;

            foreach (var item in GetItemFMGEntriesByType(cat, ItemType.Title))
            {
                if (item.ID == id)
                {
                    title = item;
                }
            }

            foreach (var item in GetItemFMGEntriesByType(cat, ItemType.Summary))
            {
                if (item.ID == id)
                {
                    summary = item;
                }
            }

            foreach (var item in GetItemFMGEntriesByType(cat, ItemType.Description))
            {
                if (item.ID == id)
                {
                    description = item;
                }
            }
        }

        public static FMG.Entry LookupItemID(int id, ItemCategory cat)
        {
            if (!IsLoaded || IsLoading)
                return null;
            foreach (var item in GetItemFMGEntriesByType(cat, ItemType.Title))
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static void ReloadFMGsDS2()
        {
            var desc = AssetLocator.GetItemMsgbnd(null, true);
            var files = Directory.GetFileSystemEntries($@"{AssetLocator.GameRootDirectory}\{desc.AssetPath}", @"*.fmg").ToList();
            _ds2fmgs = new Dictionary<string, FMG>();
            foreach (var file in files)
            {
                var modfile = $@"{AssetLocator.GameModDirectory}\{desc.AssetPath}\{Path.GetFileName(file)}";
                if (AssetLocator.GameModDirectory != null && File.Exists(modfile))
                {
                    var fmg = FMG.Read(modfile);
                    _ds2fmgs.Add(Path.GetFileNameWithoutExtension(modfile), fmg);
                }
                else
                {
                    var fmg = FMG.Read(file);
                    _ds2fmgs.Add(Path.GetFileNameWithoutExtension(file), fmg);
                }
            }
        }

        public static bool ExportFMGs()
        {
            FolderBrowserDialog folderBrowserDialog1 = new();
            folderBrowserDialog1.UseDescriptionForTitle = true;
            folderBrowserDialog1.Description = "Choose Export Folder";
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            var path = folderBrowserDialog1.SelectedPath;//$@"{AssetLocator.GameModDirectory}\Export";

            var itemPath = $@"{path}\Item Text";
            var menuPath = $@"{path}\Menu Text";
            Directory.CreateDirectory(itemPath);
            Directory.CreateDirectory(menuPath);
            int filecount = 0;

            foreach (var fmg in _itemFMGs)
            {
                var fmgPair = KeyValuePair.Create(fmg.Key.ToString(), fmg.Value);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(fmgPair, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText($@"{itemPath}\{fmg.Key.ToString()}.fmg.json", json);
                filecount++;
            }

            foreach (var fmg in _menuFMGs)
            {
                var fmgPair = KeyValuePair.Create(fmg.Key.ToString(), fmg.Value);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(fmgPair, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText($@"{menuPath}\{fmg.Key.ToString()}.fmg.json", json);
                filecount++;
            }
            MessageBox.Show($"Exported {filecount} text files", "Finished", MessageBoxButtons.OK);
            return true;
        }
        public static bool ImportFMGs()
        {
            OpenFileDialog openFileDialog1 = new();
            //openFileDialog1.InitialDirectory = $@"{AssetLocator.GameModDirectory}\Export\Text";
            openFileDialog1.Title = "Choose Files to Import";
            openFileDialog1.Filter ="Exported FMGs|*.fmg.json|All files|*.*";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            var files = openFileDialog1.FileNames;

            if (files.Length == 0)
            {
                return false;
            }

            int filecount = 0;

            foreach (var filePath in files)
            {
                var file = File.ReadAllText(filePath);
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyValuePair<string, FMG>>(@file);
                foreach (var fmg in _itemFMGs)
                {
                    if (fmg.Key.ToString() == json.Key)
                    {
                        _itemFMGs[fmg.Key] = json.Value;
                        filecount++;
                        goto Continue;
                    }
                }
                foreach (var fmg in _menuFMGs)
                {
                    if (fmg.Key.ToString() == json.Key)
                    {
                        _menuFMGs[fmg.Key] = json.Value;
                        filecount++;
                        goto Continue;
                    }
                }
                Continue:;
            }
            MessageBox.Show($"Imported {filecount} text files", "Finished", MessageBoxButtons.OK);
            return true;
        }

        public static void ReloadFMGs(string languageFolder = "")
        {
            _languageFolder = languageFolder;
            IsLoaded = false;
            IsLoading = true;
            if (AssetLocator.Type == GameType.Undefined)
            {
                return;
            }

            if (AssetLocator.Type == GameType.DarkSoulsIISOTFS)
            {
                ReloadFMGsDS2();
                IsLoading = false;
                IsLoaded = true;
                return;
            }

            IBinder fmgBinderItem;
            IBinder fmgBinderMenu;
            var itemMsgPath = AssetLocator.GetItemMsgbnd(languageFolder);
            var menuMsgPath = AssetLocator.GetMenuMsgbnd(languageFolder);
            if (AssetLocator.Type == GameType.DemonsSouls || AssetLocator.Type == GameType.DarkSoulsPTDE || AssetLocator.Type == GameType.DarkSoulsRemastered)
            {
                fmgBinderItem = BND3.Read(itemMsgPath.AssetPath);
                fmgBinderMenu = BND3.Read(menuMsgPath.AssetPath);
            }
            else
            {
                fmgBinderItem = BND4.Read(itemMsgPath.AssetPath);
                fmgBinderMenu = BND4.Read(menuMsgPath.AssetPath);
            }

            _itemFMGs = new Dictionary<ItemFMGTypes, FMG>();
            _menuFMGs = new Dictionary<MenuFMGTypes, FMG>();
            foreach (var file in fmgBinderItem.Files)
            {
                _itemFMGs.Add((ItemFMGTypes)file.ID, FMG.Read(file.Bytes));
            }

            foreach (var file in fmgBinderMenu.Files)
            {
                if (Enum.IsDefined(typeof(ItemFMGTypes), file.ID)) //catch item FMGs in menu msgbnd 
                    _itemFMGs.Add((ItemFMGTypes)file.ID, FMG.Read(file.Bytes));
                else
                    _menuFMGs.Add((MenuFMGTypes)file.ID, FMG.Read(file.Bytes));
            }

            IsLoaded = true;
            IsLoading = false;
        }

        public static void SaveFMGsDS2()
        {
            foreach (var fmg in _ds2fmgs)
            {
                Utils.WriteWithBackup(AssetLocator.GameRootDirectory, AssetLocator.GameModDirectory, $@"menu\text\english\{fmg.Key}.fmg", fmg.Value);
            }
        }

        public static void SaveFMGs()
        {
            if (!IsLoaded)
                return;
            if (AssetLocator.Type == GameType.Undefined)
            {
                return;
            }

            if (AssetLocator.Type == GameType.DarkSoulsIISOTFS)
            {
                SaveFMGsDS2();
                return;
            }

            // Load the fmg bnd, replace fmgs, and save
            IBinder fmgBinderItem;
            IBinder fmgBinderMenu;
            var itemMsgPath = AssetLocator.GetItemMsgbnd();
            var menuMsgPath = AssetLocator.GetMenuMsgbnd();
            if (AssetLocator.Type == GameType.DemonsSouls || AssetLocator.Type == GameType.DarkSoulsPTDE || AssetLocator.Type == GameType.DarkSoulsRemastered)
            {
                fmgBinderItem = BND3.Read(itemMsgPath.AssetPath);
                fmgBinderMenu = BND3.Read(menuMsgPath.AssetPath);
            }
            else
            {
                fmgBinderItem = BND4.Read(itemMsgPath.AssetPath);
                fmgBinderMenu = BND4.Read(menuMsgPath.AssetPath);
            }

            foreach (var file in fmgBinderItem.Files)
            {
                if (_itemFMGs.ContainsKey((ItemFMGTypes)file.ID))
                {
                    file.Bytes = _itemFMGs[(ItemFMGTypes)file.ID].Write();
                }
            }

            foreach (var file in fmgBinderMenu.Files)
            {
                if (_menuFMGs.ContainsKey((MenuFMGTypes)file.ID))
                    file.Bytes = _menuFMGs[(MenuFMGTypes)file.ID].Write();
                else if (Enum.IsDefined(typeof(ItemFMGTypes), file.ID))
                    file.Bytes = _itemFMGs[(ItemFMGTypes)file.ID].Write();
            }

            var itemMsgPathDest = AssetLocator.GetItemMsgbnd(null, true);
            var menuMsgPathDest = AssetLocator.GetMenuMsgbnd(null, true);
            if (fmgBinderItem is BND3 bnd3)
            {
                Utils.WriteWithBackup(AssetLocator.GameRootDirectory,
                    AssetLocator.GameModDirectory, itemMsgPathDest.AssetPath, bnd3);
                //Some item FMGs are in item.msgbnd
                Utils.WriteWithBackup(AssetLocator.GameRootDirectory,
                    AssetLocator.GameModDirectory, menuMsgPathDest.AssetPath, (BND3)fmgBinderMenu);
            }
            else if (fmgBinderItem is BND4 bnd4)
            {
                Utils.WriteWithBackup(AssetLocator.GameRootDirectory,
                    AssetLocator.GameModDirectory, itemMsgPathDest.AssetPath, bnd4);
                //Some item FMGs are in item.msgbnd
                Utils.WriteWithBackup(AssetLocator.GameRootDirectory,
                    AssetLocator.GameModDirectory, menuMsgPathDest.AssetPath, (BND4)fmgBinderMenu);
            }
        }

        public static void SetAssetLocator(AssetLocator l)
        {
            AssetLocator = l;
            //ReloadFMGs();
        }
    }
}
