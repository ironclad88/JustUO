using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public abstract class SBInfo
    {
        protected readonly IShopSellInfo m_BaseSellInfo = new InternalBaseSellInfo();
        public SBInfo()
        {
        }

        public virtual IShopSellInfo SellInfo
        {
            get
            {
                return m_BaseSellInfo;
            }
        }
        public abstract List<GenericBuyInfo> BuyInfo { get; }

        public class InternalBaseSellInfo : GenericSellInfo
        {
            public InternalBaseSellInfo()
            {
                this.Add(typeof(BlackPearl), 3);
                this.Add(typeof(Bloodmoss), 3);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(SpidersSilk), 2);
                this.Add(typeof(SulfurousAsh), 2);
                this.Add(typeof(Bottle), 3);
                this.Add(typeof(MortarPestle), 4);
                this.Add(typeof(HairDye), 19);

                this.Add(typeof(NightSightPotion), 7);
                this.Add(typeof(AgilityPotion), 7);
                this.Add(typeof(StrengthPotion), 7);
                this.Add(typeof(RefreshPotion), 7);
                this.Add(typeof(LesserCurePotion), 7);
                this.Add(typeof(LesserHealPotion), 7);
                this.Add(typeof(LesserPoisonPotion), 7);
                this.Add(typeof(LesserExplosionPotion), 10);

                this.Add(typeof(BreadLoaf), 3);
                this.Add(typeof(FrenchBread), 1);
                this.Add(typeof(Cake), 5);
                this.Add(typeof(Cookies), 3);
                this.Add(typeof(Muffins), 2);
                this.Add(typeof(CheesePizza), 4);
                this.Add(typeof(ApplePie), 5);
                this.Add(typeof(PeachCobbler), 5);
                this.Add(typeof(Quiche), 6);
                this.Add(typeof(Dough), 4);
                this.Add(typeof(JarHoney), 1);
                this.Add(typeof(Pitcher), 5);
                this.Add(typeof(SackFlour), 1);
                this.Add(typeof(Eggs), 1);

                this.Add(typeof(LapHarp), 10);
                this.Add(typeof(Lute), 10);
                this.Add(typeof(Drums), 10);
                this.Add(typeof(Harp), 10);
                this.Add(typeof(Tambourine), 10);

                this.Add(typeof(WoodenBowlOfCarrots), 1);
                this.Add(typeof(WoodenBowlOfCorn), 1);
                this.Add(typeof(WoodenBowlOfLettuce), 1);
                this.Add(typeof(WoodenBowlOfPeas), 1);
                this.Add(typeof(EmptyPewterBowl), 1);
                this.Add(typeof(PewterBowlOfCorn), 1);
                this.Add(typeof(PewterBowlOfLettuce), 1);
                this.Add(typeof(PewterBowlOfPeas), 1);
                this.Add(typeof(PewterBowlOfPotatos), 1);
                this.Add(typeof(WoodenBowlOfStew), 1);
                this.Add(typeof(WoodenBowlOfTomatoSoup), 1);
                this.Add(typeof(BeverageBottle), 3);
                this.Add(typeof(Jug), 6);
                this.Add(typeof(Pitcher), 5);
                this.Add(typeof(GlassMug), 1);
                this.Add(typeof(BreadLoaf), 3);
                this.Add(typeof(CheeseWheel), 12);
                this.Add(typeof(Ribs), 6);
                this.Add(typeof(Peach), 1);
                this.Add(typeof(Pear), 1);
                this.Add(typeof(Grapes), 1);
                this.Add(typeof(Apple), 1);
                this.Add(typeof(Banana), 1);
                this.Add(typeof(Candle), 3);
                this.Add(typeof(Chessboard), 1);
                this.Add(typeof(CheckerBoard), 1);
                this.Add(typeof(Backgammon), 1);
                this.Add(typeof(Dices), 1);
                this.Add(typeof(ContractOfEmployment), 626);

                this.Add(typeof(JarHoney), 1);
                this.Add(typeof(Beeswax), 1);

                this.Add(typeof(Tongs), 7);
                this.Add(typeof(IronIngot), 4);

                this.Add(typeof(Buckler), 25);
                this.Add(typeof(BronzeShield), 33);
                this.Add(typeof(MetalShield), 60);
                this.Add(typeof(MetalKiteShield), 62);
                this.Add(typeof(HeaterShield), 115);
                this.Add(typeof(WoodenKiteShield), 35);

                this.Add(typeof(WoodenShield), 15);

                this.Add(typeof(PlateArms), 94);
                this.Add(typeof(PlateChest), 121);
                this.Add(typeof(PlateGloves), 72);
                this.Add(typeof(PlateGorget), 52);
                this.Add(typeof(PlateLegs), 109);

                this.Add(typeof(FemalePlateChest), 113);
                this.Add(typeof(FemaleLeatherChest), 18);
                this.Add(typeof(FemaleStuddedChest), 25);
                this.Add(typeof(LeatherShorts), 14);
                this.Add(typeof(LeatherSkirt), 11);
                this.Add(typeof(LeatherBustierArms), 11);
                this.Add(typeof(StuddedBustierArms), 27);

                this.Add(typeof(Bascinet), 9);
                this.Add(typeof(CloseHelm), 9);
                this.Add(typeof(Helmet), 9);
                this.Add(typeof(NorseHelm), 9);
                this.Add(typeof(PlateHelm), 10);

                this.Add(typeof(ChainCoif), 6);
                this.Add(typeof(ChainChest), 71);
                this.Add(typeof(ChainLegs), 74);

                this.Add(typeof(RingmailArms), 42);
                this.Add(typeof(RingmailChest), 60);
                this.Add(typeof(RingmailGloves), 26);
                this.Add(typeof(RingmailLegs), 45);

                this.Add(typeof(BattleAxe), 13);
                this.Add(typeof(DoubleAxe), 26);
                this.Add(typeof(ExecutionersAxe), 15);
                this.Add(typeof(LargeBattleAxe), 16);
                this.Add(typeof(Pickaxe), 11);
                this.Add(typeof(TwoHandedAxe), 16);
                this.Add(typeof(WarAxe), 14);
                this.Add(typeof(Axe), 20);

                this.Add(typeof(Bardiche), 30);
                this.Add(typeof(Halberd), 21);

                this.Add(typeof(ButcherKnife), 7);
                this.Add(typeof(Cleaver), 7);
                this.Add(typeof(Dagger), 10);
                this.Add(typeof(SkinningKnife), 7);

                this.Add(typeof(Club), 8);
                this.Add(typeof(HammerPick), 13);
                this.Add(typeof(Mace), 14);
                this.Add(typeof(Maul), 10);
                this.Add(typeof(WarHammer), 12);
                this.Add(typeof(WarMace), 15);

                this.Add(typeof(HeavyCrossbow), 27);
                this.Add(typeof(Bow), 17);
                this.Add(typeof(Crossbow), 23);

                if (Core.AOS)
                {
                    this.Add(typeof(CompositeBow), 25);
                    this.Add(typeof(RepeatingCrossbow), 28);
                    this.Add(typeof(Scepter), 20);
                    this.Add(typeof(BladedStaff), 20);
                    this.Add(typeof(Scythe), 19);
                    this.Add(typeof(BoneHarvester), 17);
                    this.Add(typeof(Scepter), 18);
                    this.Add(typeof(BladedStaff), 16);
                    this.Add(typeof(Pike), 19);
                    this.Add(typeof(DoubleBladedStaff), 17);
                    this.Add(typeof(Lance), 17);
                    this.Add(typeof(CrescentBlade), 18);
                }

                this.Add(typeof(Spear), 15);
                this.Add(typeof(Pitchfork), 9);
                this.Add(typeof(ShortSpear), 11);

                this.Add(typeof(BlackStaff), 11);
                this.Add(typeof(GnarledStaff), 8);
                this.Add(typeof(QuarterStaff), 9);
                this.Add(typeof(ShepherdsCrook), 10);

                this.Add(typeof(SmithHammer), 10);

                this.Add(typeof(Broadsword), 17);
                this.Add(typeof(Cutlass), 12);
                this.Add(typeof(Katana), 16);
                this.Add(typeof(Kryss), 16);
                this.Add(typeof(Longsword), 27);
                this.Add(typeof(Scimitar), 18);
                this.Add(typeof(ThinLongsword), 13);
                this.Add(typeof(VikingSword), 27);

                this.Add(typeof(FletcherTools), 1);

                this.Add(typeof(RawRibs), 8);
                this.Add(typeof(RawLambLeg), 4);
                this.Add(typeof(RawChickenLeg), 3);
                this.Add(typeof(RawBird), 4);
                this.Add(typeof(Bacon), 3);
                this.Add(typeof(Sausage), 9);
                this.Add(typeof(Ham), 13);
                this.Add(typeof(ButcherKnife), 7);
                this.Add(typeof(Cleaver), 7);
                this.Add(typeof(SkinningKnife), 7);

                this.Add(typeof(WoodenBox), 7);
                this.Add(typeof(SmallCrate), 5);
                this.Add(typeof(MediumCrate), 6);
                this.Add(typeof(LargeCrate), 7);
                this.Add(typeof(WoodenChest), 15);

                this.Add(typeof(LargeTable), 10);
                this.Add(typeof(Nightstand), 7);
                this.Add(typeof(YewWoodTable), 10);

                this.Add(typeof(Throne), 24);
                this.Add(typeof(WoodenThrone), 6);
                this.Add(typeof(Stool), 6);
                this.Add(typeof(FootStool), 6);

                this.Add(typeof(FancyWoodenChairCushion), 12);
                this.Add(typeof(WoodenChairCushion), 10);
                this.Add(typeof(WoodenChair), 8);
                this.Add(typeof(BambooChair), 6);
                this.Add(typeof(WoodenBench), 6);

                this.Add(typeof(Saw), 9);
                this.Add(typeof(Scorp), 6);
                this.Add(typeof(SmoothingPlane), 6);
                this.Add(typeof(DrawKnife), 6);
                this.Add(typeof(Froe), 6);
                this.Add(typeof(Hammer), 14);
                this.Add(typeof(Inshave), 6);
                this.Add(typeof(JointingPlane), 6);
                this.Add(typeof(MouldingPlane), 6);
                this.Add(typeof(DovetailSaw), 7);
                this.Add(typeof(Board), 2);
                this.Add(typeof(Axle), 1);

                this.Add(typeof(Club), 13);

                this.Add(typeof(Lute), 10);
                this.Add(typeof(LapHarp), 10);
                this.Add(typeof(Tambourine), 10);
                this.Add(typeof(Drums), 10);

                this.Add(typeof(Log), 1);

                this.Add(typeof(Shoes), 4);
                this.Add(typeof(Boots), 5);
                this.Add(typeof(ThighBoots), 7);
                this.Add(typeof(Sandals), 2);

                this.Add(typeof(CheeseWheel), 12);
                this.Add(typeof(CookedBird), 8);
                this.Add(typeof(RoastPig), 53);
                this.Add(typeof(Cake), 5);
                this.Add(typeof(JarHoney), 1);
                this.Add(typeof(SackFlour), 1);
                this.Add(typeof(BreadLoaf), 2);
                this.Add(typeof(ChickenLeg), 3);
                this.Add(typeof(LambLeg), 4);
                this.Add(typeof(Skillet), 1);
                this.Add(typeof(FlourSifter), 1);
                this.Add(typeof(RollingPin), 1);
                this.Add(typeof(Muffins), 1);
                this.Add(typeof(ApplePie), 3);

                this.Add(typeof(WoodenBowlOfCarrots), 1);
                this.Add(typeof(WoodenBowlOfCorn), 1);
                this.Add(typeof(WoodenBowlOfLettuce), 1);
                this.Add(typeof(WoodenBowlOfPeas), 1);
                this.Add(typeof(EmptyPewterBowl), 1);
                this.Add(typeof(PewterBowlOfCorn), 1);
                this.Add(typeof(PewterBowlOfLettuce), 1);
                this.Add(typeof(PewterBowlOfPeas), 1);
                this.Add(typeof(PewterBowlOfPotatos), 1);
                this.Add(typeof(WoodenBowlOfStew), 1);
                this.Add(typeof(WoodenBowlOfTomatoSoup), 1);

                this.Add(typeof(Pitcher), 5);
                this.Add(typeof(Eggs), 1);
                this.Add(typeof(Apple), 1);
                this.Add(typeof(Grapes), 1);
                this.Add(typeof(Watermelon), 3);
                this.Add(typeof(YellowGourd), 1);
                this.Add(typeof(GreenGourd), 1);
                this.Add(typeof(Pumpkin), 5);
                this.Add(typeof(Onion), 1);
                this.Add(typeof(Lettuce), 2);
                this.Add(typeof(Squash), 1);
                this.Add(typeof(Carrot), 1);
                this.Add(typeof(HoneydewMelon), 3);
                this.Add(typeof(Cantaloupe), 3);
                this.Add(typeof(Cabbage), 2);
                this.Add(typeof(Lemon), 1);
                this.Add(typeof(Lime), 1);
                this.Add(typeof(Peach), 1);
                this.Add(typeof(Pear), 1);
                this.Add(typeof(SheafOfHay), 1);

                this.Add(typeof(RawFishSteak), 1);
                this.Add(typeof(Fish), 1);
                //TODO: Add( typeof( SmallFish ), 1 );
                this.Add(typeof(FishingPole), 7);

                this.Add(typeof(Bandage), 1);

                this.Add(typeof(Hides), 2);

                this.Add(typeof(BlackPearl), 3);
                this.Add(typeof(Bloodmoss), 3);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(SpidersSilk), 2);
                this.Add(typeof(SulfurousAsh), 2);
                this.Add(typeof(Bottle), 3);
                this.Add(typeof(MortarPestle), 4);

                this.Add(typeof(NightSightPotion), 7);
                this.Add(typeof(AgilityPotion), 7);
                this.Add(typeof(StrengthPotion), 7);
                this.Add(typeof(RefreshPotion), 7);
                this.Add(typeof(LesserCurePotion), 7);
                this.Add(typeof(LesserHealPotion), 7);
                this.Add(typeof(LesserPoisonPotion), 7);
                this.Add(typeof(LesserExplosionPotion), 10);

                this.Add(typeof(GlassblowingBook), 5000);
                this.Add(typeof(SandMiningBook), 5000);
                this.Add(typeof(Blowpipe), 10);

                this.Add(typeof(HairDye), 30);
                this.Add(typeof(SpecialBeardDye), 250000);
                this.Add(typeof(SpecialHairDye), 250000);

                this.Add(typeof(Bandage), 1);
                this.Add(typeof(LesserHealPotion), 7);
                this.Add(typeof(RefreshPotion), 7);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);

                this.Add(typeof(Bloodmoss), 3);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(Bottle), 3);
                this.Add(typeof(MortarPestle), 4);

                this.Add(typeof(BlackPearl), 3);
                this.Add(typeof(Bloodmoss), 3);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(SpidersSilk), 2);
                this.Add(typeof(SulfurousAsh), 2);
                this.Add(typeof(RecallRune), 8);
                this.Add(typeof(Spellbook), 9);
                this.Add(typeof(BlankScroll), 3);

                this.Add(typeof(NightSightPotion), 7);
                this.Add(typeof(AgilityPotion), 7);
                this.Add(typeof(StrengthPotion), 7);
                this.Add(typeof(RefreshPotion), 7);
                this.Add(typeof(LesserCurePotion), 7);
                this.Add(typeof(LesserHealPotion), 7);

                Type[] types = Loot.RegularScrollTypes;

                for (int i = 0; i < types.Length; ++i)
                    this.Add(types[i], ((i / 8) + 2) * 2);

                this.Add(typeof(BeverageBottle), 3);
                this.Add(typeof(Jug), 6);
                this.Add(typeof(Pitcher), 5);
                this.Add(typeof(GlassMug), 1);
                this.Add(typeof(BreadLoaf), 3);
                this.Add(typeof(CheeseWheel), 12);
                this.Add(typeof(Ribs), 6);
                this.Add(typeof(Peach), 1);
                this.Add(typeof(Pear), 1);
                this.Add(typeof(Grapes), 1);
                this.Add(typeof(Apple), 1);
                this.Add(typeof(Banana), 1);
                this.Add(typeof(Torch), 3);
                this.Add(typeof(Candle), 3);
                this.Add(typeof(Chessboard), 1);
                this.Add(typeof(CheckerBoard), 1);
                this.Add(typeof(Backgammon), 1);
                this.Add(typeof(Dices), 1);
                this.Add(typeof(ContractOfEmployment), 626);
                this.Add(typeof(Beeswax), 1);
                this.Add(typeof(WoodenBowlOfCarrots), 1);
                this.Add(typeof(WoodenBowlOfCorn), 1);
                this.Add(typeof(WoodenBowlOfLettuce), 1);
                this.Add(typeof(WoodenBowlOfPeas), 1);
                this.Add(typeof(EmptyPewterBowl), 1);
                this.Add(typeof(PewterBowlOfCorn), 1);
                this.Add(typeof(PewterBowlOfLettuce), 1);
                this.Add(typeof(PewterBowlOfPeas), 1);
                this.Add(typeof(PewterBowlOfPotatos), 1);
                this.Add(typeof(WoodenBowlOfStew), 1);
                this.Add(typeof(WoodenBowlOfTomatoSoup), 1);

                this.Add(typeof(Amber), 25);
                this.Add(typeof(Amethyst), 50);
                this.Add(typeof(Citrine), 25);
                this.Add(typeof(Diamond), 100);
                this.Add(typeof(Emerald), 50);
                this.Add(typeof(Ruby), 37);
                this.Add(typeof(Sapphire), 50);
                this.Add(typeof(StarSapphire), 62);
                this.Add(typeof(Tourmaline), 47);
                this.Add(typeof(GoldRing), 13);
                this.Add(typeof(SilverRing), 10);
                this.Add(typeof(Necklace), 13);
                this.Add(typeof(GoldNecklace), 13);
                this.Add(typeof(GoldBeadNecklace), 13);
                this.Add(typeof(SilverNecklace), 10);
                this.Add(typeof(SilverBeadNecklace), 10);
                this.Add(typeof(Beads), 13);
                this.Add(typeof(GoldBracelet), 13);
                this.Add(typeof(SilverBracelet), 10);
                this.Add(typeof(GoldEarrings), 13);
                this.Add(typeof(SilverEarrings), 10);

                this.Add(typeof(Hides), 2);
                this.Add(typeof(ThighBoots), 28);

                this.Add(typeof(WizardsHat), 15);
                this.Add(typeof(BlackPearl), 3);
                this.Add(typeof(Bloodmoss), 4);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(SpidersSilk), 2);
                this.Add(typeof(SulfurousAsh), 2);

                if (Core.AOS)
                {
                    this.Add(typeof(BatWing), 1);
                    this.Add(typeof(DaemonBlood), 3);
                    this.Add(typeof(PigIron), 2);
                    this.Add(typeof(NoxCrystal), 3);
                    this.Add(typeof(GraveDust), 1);
                }

                this.Add(typeof(RecallRune), 13);
                this.Add(typeof(Spellbook), 25);

                if (Core.SE)
                {
                    this.Add(typeof(ExorcismScroll), 3);
                    this.Add(typeof(AnimateDeadScroll), 8);
                    this.Add(typeof(BloodOathScroll), 8);
                    this.Add(typeof(CorpseSkinScroll), 8);
                    this.Add(typeof(CurseWeaponScroll), 8);
                    this.Add(typeof(EvilOmenScroll), 8);
                    this.Add(typeof(PainSpikeScroll), 8);
                    this.Add(typeof(SummonFamiliarScroll), 8);
                    this.Add(typeof(HorrificBeastScroll), 8);
                    this.Add(typeof(MindRotScroll), 10);
                    this.Add(typeof(PoisonStrikeScroll), 10);
                    this.Add(typeof(WraithFormScroll), 15);
                    this.Add(typeof(LichFormScroll), 16);
                    this.Add(typeof(StrangleScroll), 16);
                    this.Add(typeof(WitherScroll), 16);
                    this.Add(typeof(VampiricEmbraceScroll), 20);
                    this.Add(typeof(VengefulSpiritScroll), 20);
                }

                this.Add(typeof(BlankScroll), 6);
                this.Add(typeof(MapmakersPen), 4);
                this.Add(typeof(BlankMap), 2);
                this.Add(typeof(CityMap), 3);
                this.Add(typeof(LocalMap), 3);
                this.Add(typeof(WorldMap), 3);
                this.Add(typeof(PresetMapEntry), 3);

                this.Add(typeof(SackFlour), 1);
                this.Add(typeof(SheafOfHay), 1);

                this.Add(typeof(Pickaxe), 12);
                this.Add(typeof(Shovel), 6);
                this.Add(typeof(Lantern), 1);
                //Add( typeof( OilFlask ), 4 );
                this.Add(typeof(Torch), 3);
                this.Add(typeof(Bag), 3);
                this.Add(typeof(Candle), 3);

                this.Add(typeof(Arrow), 1);
                this.Add(typeof(Bolt), 2);
                this.Add(typeof(Backpack), 7);
                this.Add(typeof(Pouch), 3);
                this.Add(typeof(Bag), 3);
                this.Add(typeof(Candle), 3);
                this.Add(typeof(Torch), 4);
                this.Add(typeof(Lantern), 1);
                this.Add(typeof(Lockpick), 6);
                this.Add(typeof(FloppyHat), 3);
                this.Add(typeof(WideBrimHat), 4);
                this.Add(typeof(Cap), 5);
                this.Add(typeof(TallStrawHat), 4);
                this.Add(typeof(StrawHat), 3);
                this.Add(typeof(WizardsHat), 5);
                this.Add(typeof(LeatherCap), 5);
                this.Add(typeof(FeatheredHat), 5);
                this.Add(typeof(TricorneHat), 4);
                this.Add(typeof(Bandana), 3);
                this.Add(typeof(SkullCap), 3);
                this.Add(typeof(Bottle), 3);
                this.Add(typeof(RedBook), 7);
                this.Add(typeof(BlueBook), 7);
                this.Add(typeof(TanBook), 7);
                this.Add(typeof(WoodenBox), 7);
                this.Add(typeof(Kindling), 1);
                this.Add(typeof(HairDye), 30);
                this.Add(typeof(Chessboard), 1);
                this.Add(typeof(CheckerBoard), 1);
                this.Add(typeof(Backgammon), 1);
                this.Add(typeof(Dices), 1);

                this.Add(typeof(Beeswax), 1);

                this.Add(typeof(Amber), 25);
                this.Add(typeof(Amethyst), 50);
                this.Add(typeof(Citrine), 25);
                this.Add(typeof(Diamond), 100);
                this.Add(typeof(Emerald), 50);
                this.Add(typeof(Ruby), 37);
                this.Add(typeof(Sapphire), 50);
                this.Add(typeof(StarSapphire), 62);
                this.Add(typeof(Tourmaline), 47);
                this.Add(typeof(GoldRing), 13);
                this.Add(typeof(SilverRing), 10);
                this.Add(typeof(Necklace), 13);
                this.Add(typeof(GoldNecklace), 13);
                this.Add(typeof(GoldBeadNecklace), 13);
                this.Add(typeof(SilverNecklace), 10);
                this.Add(typeof(SilverBeadNecklace), 10);
                this.Add(typeof(Beads), 13);
                this.Add(typeof(GoldBracelet), 13);
                this.Add(typeof(SilverBracelet), 10);
                this.Add(typeof(GoldEarrings), 13);
                this.Add(typeof(SilverEarrings), 10);

                this.Add(typeof(ScribesPen), 4);
                this.Add(typeof(BlankScroll), 2);

                this.Add(typeof(ScribesPen), 4);
                this.Add(typeof(BrownBook), 7);
                this.Add(typeof(TanBook), 7);
                this.Add(typeof(BlueBook), 7);
                this.Add(typeof(BlankScroll), 3);

                this.Add(typeof(Wasabi), 1);
                this.Add(typeof(BentoBox), 3);
                this.Add(typeof(GreenTea), 1);
                this.Add(typeof(SushiRolls), 1);
                this.Add(typeof(SushiPlatter), 2);
                this.Add(typeof(MisoSoup), 1);
                this.Add(typeof(RedMisoSoup), 1);
                this.Add(typeof(WhiteMisoSoup), 1);
                this.Add(typeof(AwaseMisoSoup), 1);

                this.Add(typeof(Kasa), 15);
                this.Add(typeof(LeatherJingasa), 5);
                this.Add(typeof(ClothNinjaHood), 16);

                this.Add(typeof(Tongs), 7);
                this.Add(typeof(IronIngot), 4);

                this.Add(typeof(MasonryBook), 5000);
                this.Add(typeof(StoneMiningBook), 5000);
                this.Add(typeof(MalletAndChisel), 1);

                this.Add(typeof(WoodenBox), 7);
                this.Add(typeof(SmallCrate), 5);
                this.Add(typeof(MediumCrate), 6);
                this.Add(typeof(LargeCrate), 7);
                this.Add(typeof(WoodenChest), 15);

                this.Add(typeof(LargeTable), 10);
                this.Add(typeof(Nightstand), 7);
                this.Add(typeof(YewWoodTable), 10);

                this.Add(typeof(Throne), 24);
                this.Add(typeof(WoodenThrone), 6);
                this.Add(typeof(Stool), 6);
                this.Add(typeof(FootStool), 6);

                this.Add(typeof(FancyWoodenChairCushion), 12);
                this.Add(typeof(WoodenChairCushion), 10);
                this.Add(typeof(WoodenChair), 8);
                this.Add(typeof(BambooChair), 6);
                this.Add(typeof(WoodenBench), 6);

                this.Add(typeof(Saw), 9);
                this.Add(typeof(Scorp), 6);
                this.Add(typeof(SmoothingPlane), 6);
                this.Add(typeof(DrawKnife), 6);
                this.Add(typeof(Froe), 6);
                this.Add(typeof(Hammer), 14);
                this.Add(typeof(Inshave), 6);
                this.Add(typeof(JointingPlane), 6);
                this.Add(typeof(MouldingPlane), 6);
                this.Add(typeof(DovetailSaw), 7);
                this.Add(typeof(Board), 2);
                this.Add(typeof(Axle), 1);

                this.Add(typeof(WoodenShield), 31);
                this.Add(typeof(BlackStaff), 24);
                this.Add(typeof(GnarledStaff), 12);
                this.Add(typeof(QuarterStaff), 15);
                this.Add(typeof(ShepherdsCrook), 12);
                this.Add(typeof(Club), 13);

                this.Add(typeof(Log), 1);

                this.Add(typeof(Scissors), 6);
                this.Add(typeof(SewingKit), 1);
                this.Add(typeof(Dyes), 4);
                this.Add(typeof(DyeTub), 4);

                this.Add(typeof(BoltOfCloth), 50);

                this.Add(typeof(FancyShirt), 10);
                this.Add(typeof(Shirt), 6);

                this.Add(typeof(ShortPants), 3);
                this.Add(typeof(LongPants), 5);

                this.Add(typeof(Cloak), 4);
                this.Add(typeof(FancyDress), 12);
                this.Add(typeof(Robe), 9);
                this.Add(typeof(PlainDress), 7);

                this.Add(typeof(Skirt), 5);
                this.Add(typeof(Kilt), 5);

                this.Add(typeof(Doublet), 7);
                this.Add(typeof(Tunic), 9);
                this.Add(typeof(JesterSuit), 13);

                this.Add(typeof(FullApron), 5);
                this.Add(typeof(HalfApron), 5);

                this.Add(typeof(JesterHat), 6);
                this.Add(typeof(FloppyHat), 3);
                this.Add(typeof(WideBrimHat), 4);
                this.Add(typeof(Cap), 5);
                this.Add(typeof(SkullCap), 3);
                this.Add(typeof(Bandana), 3);
                this.Add(typeof(TallStrawHat), 4);
                this.Add(typeof(StrawHat), 4);
                this.Add(typeof(WizardsHat), 5);
                this.Add(typeof(Bonnet), 4);
                this.Add(typeof(FeatheredHat), 5);
                this.Add(typeof(TricorneHat), 4);

                this.Add(typeof(SpoolOfThread), 9);

                this.Add(typeof(Flax), 51);
                this.Add(typeof(Cotton), 51);
                this.Add(typeof(Wool), 31);

                this.Add(typeof(Bag), 3);
                this.Add(typeof(Pouch), 3);
                this.Add(typeof(Backpack), 7);

                this.Add(typeof(Leather), 5);

                this.Add(typeof(SkinningKnife), 7);

                this.Add(typeof(LeatherArms), 18);
                this.Add(typeof(LeatherChest), 23);
                this.Add(typeof(LeatherGloves), 15);
                this.Add(typeof(LeatherGorget), 15);
                this.Add(typeof(LeatherLegs), 18);
                this.Add(typeof(LeatherCap), 5);

                this.Add(typeof(StuddedArms), 43);
                this.Add(typeof(StuddedChest), 37);
                this.Add(typeof(StuddedGloves), 39);
                this.Add(typeof(StuddedGorget), 22);
                this.Add(typeof(StuddedLegs), 33);

                this.Add(typeof(FemaleStuddedChest), 31);
                this.Add(typeof(StuddedBustierArms), 23);
                this.Add(typeof(FemalePlateChest), 103);
                this.Add(typeof(FemaleLeatherChest), 18);
                this.Add(typeof(LeatherBustierArms), 12);
                this.Add(typeof(LeatherShorts), 14);
                this.Add(typeof(LeatherSkirt), 12);

                this.Add(typeof(WoodenBowlOfCarrots), 1);
                this.Add(typeof(WoodenBowlOfCorn), 1);
                this.Add(typeof(WoodenBowlOfLettuce), 1);
                this.Add(typeof(WoodenBowlOfPeas), 1);
                this.Add(typeof(EmptyPewterBowl), 1);
                this.Add(typeof(PewterBowlOfCorn), 1);
                this.Add(typeof(PewterBowlOfLettuce), 1);
                this.Add(typeof(PewterBowlOfPeas), 1);
                this.Add(typeof(PewterBowlOfPotatos), 1);
                this.Add(typeof(WoodenBowlOfStew), 1);
                this.Add(typeof(WoodenBowlOfTomatoSoup), 1);
                this.Add(typeof(BeverageBottle), 3);
                this.Add(typeof(Jug), 6);
                this.Add(typeof(Pitcher), 5);
                this.Add(typeof(GlassMug), 1);
                this.Add(typeof(BreadLoaf), 3);
                this.Add(typeof(CheeseWheel), 12);
                this.Add(typeof(Ribs), 6);
                this.Add(typeof(Peach), 1);
                this.Add(typeof(Pear), 1);
                this.Add(typeof(Grapes), 1);
                this.Add(typeof(Apple), 1);
                this.Add(typeof(Banana), 1);
                this.Add(typeof(Candle), 3);
                this.Add(typeof(Chessboard), 1);
                this.Add(typeof(CheckerBoard), 1);
                this.Add(typeof(Backgammon), 1);
                this.Add(typeof(Dices), 1);
                this.Add(typeof(ContractOfEmployment), 626);

                this.Add(typeof(Backpack), 7);
                this.Add(typeof(Pouch), 3);
                this.Add(typeof(Torch), 3);
                this.Add(typeof(Lantern), 1);
                //Add( typeof( OilFlask ), 4 );
                this.Add(typeof(Lockpick), 6);
                this.Add(typeof(WoodenBox), 7);
                this.Add(typeof(HairDye), 19);

                this.Add(typeof(Drums), 10);
                this.Add(typeof(Tambourine), 10);
                this.Add(typeof(LapHarp), 10);
                this.Add(typeof(Lute), 10);

                this.Add(typeof(Shovel), 6);
                this.Add(typeof(SewingKit), 1);
                this.Add(typeof(Scissors), 6);
                this.Add(typeof(Tongs), 7);
                this.Add(typeof(Key), 1);

                this.Add(typeof(DovetailSaw), 6);
                this.Add(typeof(MouldingPlane), 6);
                this.Add(typeof(Nails), 1);
                this.Add(typeof(JointingPlane), 6);
                this.Add(typeof(SmoothingPlane), 6);
                this.Add(typeof(Saw), 7);

                this.Add(typeof(Clock), 11);
                this.Add(typeof(ClockParts), 1);
                this.Add(typeof(AxleGears), 1);
                this.Add(typeof(Gears), 1);
                this.Add(typeof(Hinge), 1);
                this.Add(typeof(Sextant), 6);
                this.Add(typeof(SextantParts), 2);
                this.Add(typeof(Axle), 1);
                this.Add(typeof(Springs), 1);

                this.Add(typeof(DrawKnife), 5);
                this.Add(typeof(Froe), 5);
                this.Add(typeof(Inshave), 5);
                this.Add(typeof(Scorp), 5);

                this.Add(typeof(Lockpick), 6);
                this.Add(typeof(TinkerTools), 3);

                this.Add(typeof(Board), 1);
                this.Add(typeof(Log), 1);

                this.Add(typeof(Pickaxe), 16);
                this.Add(typeof(Hammer), 3);
                this.Add(typeof(SmithHammer), 11);
                this.Add(typeof(ButcherKnife), 6);

                this.Add(typeof(Board), 1);
                this.Add(typeof(IronIngot), 3);

                this.Add(typeof(Amber), 25);
                this.Add(typeof(Amethyst), 50);
                this.Add(typeof(Citrine), 25);
                this.Add(typeof(Diamond), 100);
                this.Add(typeof(Emerald), 50);
                this.Add(typeof(Ruby), 37);
                this.Add(typeof(Sapphire), 50);
                this.Add(typeof(StarSapphire), 62);
                this.Add(typeof(Tourmaline), 47);
                this.Add(typeof(GoldRing), 13);
                this.Add(typeof(SilverRing), 10);
                this.Add(typeof(Necklace), 13);
                this.Add(typeof(GoldNecklace), 13);
                this.Add(typeof(GoldBeadNecklace), 13);
                this.Add(typeof(SilverNecklace), 10);
                this.Add(typeof(SilverBeadNecklace), 10);
                this.Add(typeof(Beads), 13);
                this.Add(typeof(GoldBracelet), 13);
                this.Add(typeof(SilverBracelet), 10);
                this.Add(typeof(GoldEarrings), 13);
                this.Add(typeof(SilverEarrings), 10);

                this.Add(typeof(Bandage), 1);

                this.Add(typeof(BlankScroll), 3);

                this.Add(typeof(NightSightPotion), 7);
                this.Add(typeof(AgilityPotion), 7);
                this.Add(typeof(StrengthPotion), 7);
                this.Add(typeof(RefreshPotion), 7);
                this.Add(typeof(LesserCurePotion), 7);
                this.Add(typeof(LesserHealPotion), 7);
                this.Add(typeof(LesserPoisonPotion), 7);
                this.Add(typeof(LesserExplosionPotion), 10);

                this.Add(typeof(Bolt), 3);
                this.Add(typeof(Arrow), 2);

                this.Add(typeof(BlackPearl), 3);
                this.Add(typeof(Bloodmoss), 3);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(SpidersSilk), 2);
                this.Add(typeof(SulfurousAsh), 2);

                this.Add(typeof(BreadLoaf), 3);
                this.Add(typeof(Backpack), 7);
                this.Add(typeof(RecallRune), 8);
                this.Add(typeof(Spellbook), 9);
                this.Add(typeof(BlankScroll), 3);

                if (Core.AOS)
                {
                    this.Add(typeof(BatWing), 2);
                    this.Add(typeof(GraveDust), 2);
                    this.Add(typeof(DaemonBlood), 3);
                    this.Add(typeof(NoxCrystal), 3);
                    this.Add(typeof(PigIron), 3);
                }

                this.Add(typeof(BattleAxe), 13);
                this.Add(typeof(DoubleAxe), 26);
                this.Add(typeof(ExecutionersAxe), 15);
                this.Add(typeof(LargeBattleAxe), 16);
                this.Add(typeof(Pickaxe), 11);
                this.Add(typeof(TwoHandedAxe), 16);
                this.Add(typeof(WarAxe), 14);
                this.Add(typeof(Axe), 20);

                this.Add(typeof(Bardiche), 30);
                this.Add(typeof(Halberd), 21);

                this.Add(typeof(ButcherKnife), 7);
                this.Add(typeof(Cleaver), 7);
                this.Add(typeof(Dagger), 10);
                this.Add(typeof(SkinningKnife), 7);

                this.Add(typeof(Club), 8);
                this.Add(typeof(HammerPick), 13);
                this.Add(typeof(Mace), 14);
                this.Add(typeof(Maul), 10);
                this.Add(typeof(WarHammer), 12);
                this.Add(typeof(WarMace), 15);

                this.Add(typeof(HeavyCrossbow), 27);
                this.Add(typeof(Bow), 17);
                this.Add(typeof(Crossbow), 23);

                if (Core.AOS)
                {
                    this.Add(typeof(CompositeBow), 25);
                    this.Add(typeof(RepeatingCrossbow), 28);
                    this.Add(typeof(Scepter), 20);
                    this.Add(typeof(BladedStaff), 20);
                    this.Add(typeof(Scythe), 19);
                    this.Add(typeof(BoneHarvester), 17);
                    this.Add(typeof(Scepter), 18);
                    this.Add(typeof(BladedStaff), 16);
                    this.Add(typeof(Pike), 19);
                    this.Add(typeof(DoubleBladedStaff), 17);
                    this.Add(typeof(Lance), 17);
                    this.Add(typeof(CrescentBlade), 18);
                }

                this.Add(typeof(Spear), 15);
                this.Add(typeof(Pitchfork), 9);
                this.Add(typeof(ShortSpear), 11);

                this.Add(typeof(BlackStaff), 11);
                this.Add(typeof(GnarledStaff), 8);
                this.Add(typeof(QuarterStaff), 9);
                this.Add(typeof(ShepherdsCrook), 10);

                this.Add(typeof(SmithHammer), 10);

                this.Add(typeof(Broadsword), 17);
                this.Add(typeof(Cutlass), 12);
                this.Add(typeof(Katana), 16);
                this.Add(typeof(Kryss), 16);
                this.Add(typeof(Longsword), 27);
                this.Add(typeof(Scimitar), 18);
                this.Add(typeof(ThinLongsword), 13);
                this.Add(typeof(VikingSword), 27);

                this.Add(typeof(Hatchet), 13);
                this.Add(typeof(WarFork), 16);

                this.Add(typeof(Scissors), 6);
                this.Add(typeof(Dyes), 4);
                this.Add(typeof(DyeTub), 4);
                this.Add(typeof(UncutCloth), 1);
                this.Add(typeof(BoltOfCloth), 50);
                this.Add(typeof(LightYarnUnraveled), 9);
                this.Add(typeof(LightYarn), 9);
                this.Add(typeof(DarkYarn), 9);
            }
        }
    }
}