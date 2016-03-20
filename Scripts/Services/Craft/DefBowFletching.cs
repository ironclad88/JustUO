using System;
using Server.Items;
using Server.Items.ZuluIems.Weapons.Ranged;

namespace Server.Engines.Craft
{
    #region Mondain's Legacy
    public enum BowRecipes
    {
        //magical
        BarbedLongbow = 200,
        SlayerLongbow = 201,
        FrozenLongbow = 202,
        LongbowOfMight = 203,
        RangersShortbow = 204,
        LightweightShortbow = 205,
        MysticalShortbow = 206,
        AssassinsShortbow = 207,

        // arties
        BlightGrippedLongbow = 250,
        FaerieFire = 251,
        SilvanisFeywoodBow = 252,
        MischiefMaker = 253,
        TheNightReaper = 254,
    }
    #endregion

    public class DefBowFletching : CraftSystem
    {
        public override SkillName MainSkill
        {
            get
            {
                return SkillName.Fletching;
            }
        }

        public override int GumpTitleNumber
        {
            get
            {
                return 1044006;
            }// <CENTER>BOWCRAFT AND FLETCHING MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefBowFletching();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.5; // 50%
        }

        private DefBowFletching()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            // no animation
            //if ( from.Body.Type == BodyType.Human && !from.Mounted )
            //	from.Animate( 33, 5, 1, true, false, 0 );
            from.PlaySound(0x55);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else 
                    return 1044154; // You create the item.
            }
        }

        public override CraftECA ECA
        {
            get
            {
                return CraftECA.FiftyPercentChanceMinusTenPercent;
            }
        }

        public override void InitCraftList()
        {
            int index = -1;

            // Materials
            this.AddCraft(typeof(Kindling), 1044457, 1023553, 0.0, 00.0, typeof(Log), 1044041, 1, 1044351);

            index = this.AddCraft(typeof(Shaft), 1044457, 1027124, 0.0, 40.0, typeof(Log), 1044041, 1, 1044351);
            this.SetUseAllRes(index, true);

            // Ammunition
            index = this.AddCraft(typeof(Arrow), 1044565, 1023903, 0.0, 40.0, typeof(Shaft), 1044560, 1, 1044561);
            this.AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            this.SetUseAllRes(index, true);

            index = this.AddCraft(typeof(FireArrow), 1044565, "Fire Arrow", 90, 130, typeof(Shaft), 1044560, 1);
            this.AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            this.AddRes(index, typeof(SulfurousAsh), "Sulfurous Ash", 1);
            this.SetUseAllRes(index, true);

            index = this.AddCraft(typeof(IceArrow), 1044565, "Ice Arrow", 90, 130, typeof(Shaft), 1044560, 1);
            this.AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            this.AddRes(index, typeof(SpidersSilk), "Spider's Silk", 1);
            this.SetUseAllRes(index, true);

            index = this.AddCraft(typeof(Bolt), 1044565, 1027163, 0.0, 40.0, typeof(Shaft), 1044560, 1, 1044561);
            this.AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            this.SetUseAllRes(index, true);

            index = this.AddCraft(typeof(ThunderBolt), 1044565, "Thunder Bolt", 95, 135, typeof(Shaft), 1044560, 1);
            this.AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            this.AddRes(index, typeof(BlackPearl), "Black Pearl", 1);
            this.SetUseAllRes(index, true);

            /*if (Core.SE)
            {
                index = this.AddCraft(typeof(FukiyaDarts), 1044565, 1030246, 50.0, 90.0, typeof(Log), 1044041, 1, 1044351);
                this.SetUseAllRes(index, true);
                this.SetNeededExpansion(index, Expansion.SE);
            }*/

            // Weapons
            this.AddCraft(typeof(Bow), 1044566, 1025042, 30.0, 70.0, typeof(Log), 1044041, 7, 1044351);
            this.AddCraft(typeof(Crossbow), 1044566, 1023919, 60.0, 100.0, typeof(Log), 1044041, 7, 1044351);
            this.AddCraft(typeof(HeavyCrossbow), 1044566, 1025117, 80.0, 120.0, typeof(Log), 1044041, 10, 1044351);

            index = this.AddCraft(typeof(Firebow), 1044566, "Fire Bow", 110.0, 140.0, typeof(Log), 1044041, 7);
            this.AddRes(index, typeof(SulfurousAsh), "Sulfurous Ash", 10);

            index = this.AddCraft(typeof(Icebow), 1044566, "Ice Bow", 110.0, 140.0, typeof(Log), 1044041, 7);
            this.AddRes(index, typeof(SpidersSilk), "Spider's Silk", 10);

            index = this.AddCraft(typeof(HeavyCrossbow), 1044566, "Thunder Heavy Crossbow", 115.0, 150.0, typeof(Log), 1044041, 10);
            this.AddRes(index, typeof(BlackPearl), "Black Pearl", 10);

            /*
            if (Core.AOS)
            {
                this.AddCraft(typeof(CompositeBow), 1044566, 1029922, 70.0, 110.0, typeof(Log), 1044041, 7, 1044351);
                this.AddCraft(typeof(RepeatingCrossbow), 1044566, 1029923, 90.0, 130.0, typeof(Log), 1044041, 10, 1044351);
            }

            if (Core.SE)
            {
                index = this.AddCraft(typeof(Yumi), 1044566, 1030224, 90.0, 130.0, typeof(Log), 1044041, 10, 1044351);
                this.SetNeededExpansion(index, Expansion.SE);
            }
            */
            #region Mondain's Legacy
            /*if (Core.ML)
            {
                index = this.AddCraft(typeof(ElvenCompositeLongbow), 1044566, 1031562, 95.0, 145.0, typeof(Log), 1044041, 20, 1044351);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(MagicalShortbow), 1044566, 1031551, 85.0, 135.0, typeof(Log), 1044041, 15, 1044351);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(BlightGrippedLongbow), 1044566, 1072907, 75.0, 125.0, typeof(Log), 1044041, 20, 1044351);
                this.AddRes(index, typeof(LardOfParoxysmus), 1032681, 1, 1053098);
                this.AddRes(index, typeof(Blight), 1032675, 10, 1053098);
                this.AddRes(index, typeof(Corruption), 1032676, 10, 1053098);
                this.AddRecipe(index, (int)BowRecipes.BlightGrippedLongbow);
                this.ForceNonExceptional(index);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(FaerieFire), 1044566, 1072908, 75.0, 125.0, typeof(Log), 1044041, 20, 1044351);
                this.AddRes(index, typeof(LardOfParoxysmus), 1032681, 1, 1053098);
                this.AddRes(index, typeof(Putrefication), 1032678, 10, 1053098);
                this.AddRes(index, typeof(Taint), 1032679, 10, 1053098);
                this.AddRecipe(index, (int)BowRecipes.FaerieFire);
                this.ForceNonExceptional(index);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(SilvanisFeywoodBow), 1044566, 1072955, 75.0, 125.0, typeof(Log), 1044041, 20, 1044351);
                this.AddRes(index, typeof(LardOfParoxysmus), 1032681, 1, 1053098);
                this.AddRes(index, typeof(Scourge), 1032677, 10, 1053098);
                this.AddRes(index, typeof(Muculent), 1032680, 10, 1053098);
                this.AddRecipe(index, (int)BowRecipes.SilvanisFeywoodBow);
                this.ForceNonExceptional(index);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(MischiefMaker), 1044566, 1072910, 75.0, 125.0, typeof(Log), 1044041, 15, 1044351);
                this.AddRes(index, typeof(DreadHornMane), 1032682, 1, 1053098);
                this.AddRes(index, typeof(Corruption), 1032676, 10, 1053098);
                this.AddRes(index, typeof(Putrefication), 1032678, 10, 1053098);
                this.AddRecipe(index, (int)BowRecipes.MischiefMaker);
                this.ForceNonExceptional(index);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(TheNightReaper), 1044566, 1072912, 75.0, 125.0, typeof(Log), 1044041, 10, 1044351);
                this.AddRes(index, typeof(DreadHornMane), 1032682, 1, 1053098);
                this.AddRes(index, typeof(Blight), 1032675, 10, 1053098);
                this.AddRes(index, typeof(Scourge), 1032677, 10, 1053098);
                this.AddRecipe(index, (int)BowRecipes.TheNightReaper);
                this.ForceNonExceptional(index);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(BarbedLongbow), 1044566, 1073505, 75.0, 125.0, typeof(Log), 1044041, 20, 1044351);
                this.AddRes(index, typeof(FireRuby), 1026254, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.BarbedLongbow);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(SlayerLongbow), 1044566, 1073506, 75.0, 125.0, typeof(Log), 1044041, 20, 1044351);
                this.AddRes(index, typeof(BrilliantAmber), 1026256, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.SlayerLongbow);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(FrozenLongbow), 1044566, 1073507, 75.0, 125.0, typeof(Log), 1044041, 20, 1044351);
                this.AddRes(index, typeof(Turquoise), 1026250, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.FrozenLongbow);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(LongbowOfMight), 1044566, 1073508, 75.0, 125.0, typeof(Log), 1044041, 10, 1044351);
                this.AddRes(index, typeof(BlueDiamond), 1026255, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.LongbowOfMight);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(RangersShortbow), 1044566, 1073509, 75.0, 125.0, typeof(Log), 1044041, 15, 1044351);
                this.AddRes(index, typeof(PerfectEmerald), 1026251, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.RangersShortbow);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(LightweightShortbow), 1044566, 1073510, 75.0, 125.0, typeof(Log), 1044041, 15, 1044351);
                this.AddRes(index, typeof(WhitePearl), 1026253, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.LightweightShortbow);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(MysticalShortbow), 1044566, 1073511, 75.0, 125.0, typeof(Log), 1044041, 15, 1044351);
                this.AddRes(index, typeof(EcruCitrine), 1026252, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.MysticalShortbow);
                this.SetNeededExpansion(index, Expansion.ML);

                index = this.AddCraft(typeof(AssassinsShortbow), 1044566, 1073512, 75.0, 125.0, typeof(Log), 1044041, 15, 1044351);
                this.AddRes(index, typeof(DarkSapphire), 1026249, 1, 1053098);
                this.AddRecipe(index, (int)BowRecipes.AssassinsShortbow);
                this.SetNeededExpansion(index, Expansion.ML);
            }*/

            #endregion

            this.MarkOption = true;
            this.Repair = Core.AOS;
			this.CanEnhance = false;

            #region Old Logs
#if false
            
            this.SetSubRes(typeof(Log), 1072643);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material	TODO: Verify the required skill amount
            this.AddSubRes(typeof(Log), 1072643, 00.0, 1044041, 1072652);
            this.AddSubRes(typeof(OakBoard), 1072644, 65.0, 1044041, 1072652);
            this.AddSubRes(typeof(AshBoard), 1072645, 80.0, 1044041, 1072652);
            this.AddSubRes(typeof(YewBoard), 1072646, 95.0, 1044041, 1072652);
            this.AddSubRes(typeof(HeartwoodBoard), 1072647, 100.0, 1044041, 1072652);
            this.AddSubRes(typeof(BloodwoodBoard), 1072648, 100.0, 1044041, 1072652);
            this.AddSubRes(typeof(FrostwoodBoard), 1072649, 100.0, 1044041, 1072652);
#endif
            #endregion


            SetSubRes(typeof(Log), 1072643);

            AddSubRes(typeof(Log), CraftResources.GetInfo(CraftResource.RegularWood).Name, CraftResources.GetInfo(CraftResource.RegularWood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(AshLog), CraftResources.GetInfo(CraftResource.AshWood).Name, CraftResources.GetInfo(CraftResource.AshWood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(YewLog), CraftResources.GetInfo(CraftResource.YewWood).Name, CraftResources.GetInfo(CraftResource.YewWood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(HeartwoodLog), CraftResources.GetInfo(CraftResource.Heartwood).Name, CraftResources.GetInfo(CraftResource.Heartwood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(BloodwoodLog), CraftResources.GetInfo(CraftResource.Bloodwood).Name, CraftResources.GetInfo(CraftResource.Bloodwood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(FrostwoodLog), CraftResources.GetInfo(CraftResource.Frostwood).Name, CraftResources.GetInfo(CraftResource.Frostwood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(PinetreeLog), CraftResources.GetInfo(CraftResource.Pinetree).Name, CraftResources.GetInfo(CraftResource.Pinetree).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(CherryLog), CraftResources.GetInfo(CraftResource.Cherry).Name, CraftResources.GetInfo(CraftResource.Cherry).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(OakLog), CraftResources.GetInfo(CraftResource.OakWood).Name, CraftResources.GetInfo(CraftResource.OakWood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(PurplePassionLog), CraftResources.GetInfo(CraftResource.PurplePassion).Name, CraftResources.GetInfo(CraftResource.PurplePassion).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(GoldenReflectionLog), CraftResources.GetInfo(CraftResource.GoldenReflection).Name, CraftResources.GetInfo(CraftResource.GoldenReflection).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(HardrangerLog), CraftResources.GetInfo(CraftResource.Hardranger).Name, CraftResources.GetInfo(CraftResource.Hardranger).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(JadeLog), CraftResources.GetInfo(CraftResource.Jade).Name, CraftResources.GetInfo(CraftResource.Jade).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(DarkwoodLog), CraftResources.GetInfo(CraftResource.Darkwood).Name, CraftResources.GetInfo(CraftResource.Darkwood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(StonewoodLog), CraftResources.GetInfo(CraftResource.Stonewood).Name, CraftResources.GetInfo(CraftResource.Stonewood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(SunLog), CraftResources.GetInfo(CraftResource.Sun).Name, CraftResources.GetInfo(CraftResource.Sun).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(GauntletLog), CraftResources.GetInfo(CraftResource.Gauntlet).Name, CraftResources.GetInfo(CraftResource.Gauntlet).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(SwampLog), CraftResources.GetInfo(CraftResource.Swamp).Name, CraftResources.GetInfo(CraftResource.Swamp).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(StardustLog), CraftResources.GetInfo(CraftResource.Stardust).Name, CraftResources.GetInfo(CraftResource.Stardust).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(SilverLeafLog), CraftResources.GetInfo(CraftResource.SilverLeaf).Name, CraftResources.GetInfo(CraftResource.SilverLeaf).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(StormtealLog), CraftResources.GetInfo(CraftResource.Stormteal).Name, CraftResources.GetInfo(CraftResource.Stormteal).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(EmeraldLog), CraftResources.GetInfo(CraftResource.Emerald).Name, CraftResources.GetInfo(CraftResource.Emerald).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(BloodLog), CraftResources.GetInfo(CraftResource.Blood).Name, CraftResources.GetInfo(CraftResource.Blood).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(CrystalLog), CraftResources.GetInfo(CraftResource.CrystalLog).Name, CraftResources.GetInfo(CraftResource.CrystalLog).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(BloodhorseLog), CraftResources.GetInfo(CraftResource.Bloodhorse).Name, CraftResources.GetInfo(CraftResource.Bloodhorse).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(DoomLog), CraftResources.GetInfo(CraftResource.DoomLog).Name, CraftResources.GetInfo(CraftResource.DoomLog).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(GoddessLog), CraftResources.GetInfo(CraftResource.GoddessLog).Name, CraftResources.GetInfo(CraftResource.GoddessLog).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(ZuluLog), CraftResources.GetInfo(CraftResource.ZuluLog).Name, CraftResources.GetInfo(CraftResource.ZuluLog).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(DarknessLog), CraftResources.GetInfo(CraftResource.Darkness).Name, CraftResources.GetInfo(CraftResource.Darkness).AttributeInfo.Difficulty, 1072652);
            AddSubRes(typeof(ElvenLog), CraftResources.GetInfo(CraftResource.Elven).Name, CraftResources.GetInfo(CraftResource.Elven).AttributeInfo.Difficulty, 1072652);
        }
    }
}