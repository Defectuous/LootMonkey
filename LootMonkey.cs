using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using ArcheBuddy.Bot.Classes;

namespace ArcheAgeLootMonkey{
   public class LootMonkey : Core
   {
       public static string GetPluginAuthor()
       { return "Defectuous";}
       public static string GetPluginVersion()
       { return "1.0.0.15"; }
       public static string GetPluginDescription()
       { return "Loot Monkey: Making Loot Management Easy"; }

        // [ Start Configuration ]
        // General Stuff [ true = Roll ] [ false = No Roll ]
        public bool _Unknown       = false;  // Unknown Items not Listed here
        public bool _Purses        = false;  // CoinPurses
        public bool _StolenBag     = false;  // Stolen Bags
        public bool _ScratchedSafe = false;  // Scratched Safe's
        public bool _Unid          = false;  // Unidentified items ( Knighblade items and stuff )

        // Auroria Stuff
        public bool _DivineCloth   = false;  // Auroria Cloth
        public bool _DivineLeather = false;  // Auroria Leather
        public bool _DivinePlate   = false;  // Auroria Plate
        public bool _HauntedChest  = false;  // Auroria Cape 
       
        // Hasla Stuff
        public bool _Conviction    = false;  // Staff
        public bool _Courage       = false;  // Bow
        public bool _Compassion    = false;  // Great Club
        public bool _Fortitude     = false;  // Shield
        public bool _Honor         = false;  // Sword
        public bool _Loyalty       = false;  // Great Axe
        public bool _Sacrifice     = false;  // Lute
       
        // Obsidian Stuff Weapons
        public bool _MalevolentOBS = false;  // Malevolent Obsidian
        public bool _HauntedOBS    = false;  // Haunted Obsidian
        public bool _TaintedOBS    = false;  // Tainted Jewel
        public bool _RitualOBS     = false;  // Ritual Aquamarine
       
        // Obsidian Stuff Armor
        public bool _OBSTopaz      = false;  // Possessed Topaz
        public bool _OBSBoneChip   = false;  // Bone Chip
        public bool _OBSSpellGem   = false;  // Forbidden Spellgem
        public bool _OBSOrb        = false;  // Shadow Orb
        
        // Library
        public bool _EternalLibraryWeapon = false;  // Eternal Library Weapon
        public bool _EternalLibraryArmor  = false;  // Eternal Library Armor
        public bool _EternalLibraryTome   = false;  // Eternal Library Tome
        public bool _Ayanad               = false;  // Ayanad Costume Design Scrap 
        public bool _CornerReadingRoomOrb = false;  // Corner Reading Room Orb
        
        // [ End Configuration ]
       
        //Call on plugin start
        public void PluginRun()
        {
            LootThread();
        }
       
        public void LootThread()
            {
            Random r = new Random();
            List<uint> _purses = new List<uint>() {29203, 29204, 29205, 29206, 29207, 32059, 34915, 34916, 35461 };
            List<uint> _bag = new List<uint>() { 34281, 34853, 35462, 35463, 35464, 35465, 35466, 35467, 35468 };
            List<uint> _safe = new List<uint>() { 35469, 35470, 35471, 35472, 35473, 35474, 35474, 35475, 35476, 35477 };

            try
            {
                BlockClientDice(true);
                while (true)
                {
                    List<Item> rolls = me.getDiceItems();
                    foreach (Item item in rolls)
                    {
                        bool doRoll = _Unknown;
                        
                        // Unidentified Armor & Weapons
                        if (item.name.StartsWith("Unidentified")) doRoll = _Unid;
                        
                        // Purses Bags & Safes
                        else if (_bag.Contains(item.id)) doRoll = _StolenBag;
                        else if (_safe.Contains(item.id)) doRoll = _ScratchedSafe;
                        else if (_purses.Contains(item.id)) doRoll = _Purses;

                        // Hasla Stuff
                        else if (item.id == 26056) doRoll = _Conviction;
                        else if (item.id == 26055) doRoll = _Courage;
                        else if (item.id == 35525) doRoll = _Compassion;
                        else if (item.id == 26057) doRoll = _Fortitude;
                        else if (item.id == 26053) doRoll = _Honor;
                        else if (item.id == 26054) doRoll = _Loyalty;
                        else if (item.id == 26058) doRoll = _Sacrifice;
                        
                        // Auroria Stuff
                        else if (item.id == 27468) doRoll = _DivineCloth;
                        else if (item.id == 27469) doRoll = _DivineLeather;
                        else if (item.id == 27470) doRoll = _DivinePlate;
                        else if (item.id == 32050) doRoll = _HauntedChest;
                        
                        // Obsidian Weapons
                        else if (item.id == 34724) doRoll = _MalevolentOBS;
                        else if (item.id == 34725) doRoll = _HauntedOBS;
                        else if (item.id == 34726) doRoll = _TaintedOBS;
                        else if (item.id == 34727) doRoll = _RitualOBS;
                        
                        // Obsidian Armor
                        else if (item.id == 35885) doRoll = _OBSTopaz;
                        else if (item.id == 35887) doRoll = _OBSBoneChip;
                        else if (item.id == 35888) doRoll = _OBSSpellGem;
                        else if (item.id == 35889) doRoll = _OBSOrb;
                        
                        // Library Items
                        else if (item.id == 32048) doRoll = _EternalLibraryWeapon;
                        else if (item.id == 32049) doRoll = _EternalLibraryArmor;
                        else if (item.id == 32050) doRoll = _EternalLibraryTome;
                        else if (item.id == 32060) doRoll = _Ayanad;
                        else if (item.id == 32221) doRoll = _CornerReadingRoomOrb;
                        
                        item.Dice(doRoll);

                        if (doRoll) { Log(Time() + "Rolled on : " + item.name + " (" + item.id + ")");}
                        else{ Log(Time() + "Passed on : " + item.name + " (" + item.id + ")"); }
                        Thread.Sleep(r.Next(1000, 1500));
                    }
                    Thread.Sleep(r.Next(1500, 2500));
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception e)
            {
                Log(Time() + "Loot Exception: " + e.Message);
            }
            finally
            {
                BlockClientDice(false);
            }
        }
        
        public string Time()
        {
            string A = DateTime.Now.ToString("[hh:mm:ss] ");
            return A;
        }
        
        //Call on plugin stop
        public void PluginStop()
        {
        }
    }
}
