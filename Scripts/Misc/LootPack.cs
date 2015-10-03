#region References
using System;

using Server.Items;
using Server.Mobiles;
using Server.Misc;
#endregion

namespace Server
{
	public class LootPack
	{
		public static int GetLuckChance(Mobile killer, Mobile victim)
		{
			if (!Core.AOS)
			{
				return 0;
			}

			int luck = killer.Luck;

			PlayerMobile pmKiller = killer as PlayerMobile;
			if (pmKiller != null && pmKiller.SentHonorContext != null && pmKiller.SentHonorContext.Target == victim)
			{
				luck += pmKiller.SentHonorContext.PerfectionLuckBonus;
			}

			if (luck < 0)
			{
				return 0;
			}

			if (!Core.SE && luck > 1200)
			{
				luck = 1200;
			}

			return (int)(Math.Pow(luck, 1 / 1.8) * 100);
		}

		public static int GetLuckChanceForKiller(Mobile dead)
		{
			var list = BaseCreature.GetLootingRights(dead.DamageEntries, dead.HitsMax);

			DamageStore highest = null;

			for (int i = 0; i < list.Count; ++i)
			{
				DamageStore ds = list[i];

				if (ds.m_HasRight && (highest == null || ds.m_Damage > highest.m_Damage))
				{
					highest = ds;
				}
			}

			if (highest == null)
			{
				return 0;
			}

			return GetLuckChance(highest.m_Mobile, dead);
		}

		public static bool CheckLuck(int chance)
		{
			return (chance > Utility.Random(10000));
		}

		private readonly LootPackEntry[] m_Entries;

		public LootPack(LootPackEntry[] entries)
		{
			m_Entries = entries;
		}

		public void Generate(Mobile from, Container cont, bool spawning, int luckChance)
		{
           // Console.WriteLine("Generate");
            //Console.WriteLine(level);
			if (cont == null)
			{
				return;
			}

		//	bool checkLuck = Core.AOS;

			for (int i = 0; i < m_Entries.Length; ++i)
			{
				LootPackEntry entry = m_Entries[i];

				/*bool shouldAdd = (entry.Chance > Utility.Random(10000));

				if (!shouldAdd && checkLuck)
				{
					checkLuck = false;

					if (CheckLuck(luckChance))
					{
						shouldAdd = (entry.Chance > Utility.Random(10000));
					}
				}

				if (!shouldAdd)
				{
					continue;
				}
                */
				Item item = entry.Construct(from, luckChance, spawning);

				if (item != null)
				{
					if (!item.Stackable || !cont.TryDropItem(from, item, false))
					{
						cont.DropItem(item);
					}
				}
			}
		}

		public static readonly LootPackItem[] Gold = new[] {new LootPackItem(typeof(Gold), 1)};

		public static readonly LootPackItem[] Instruments = new[] {new LootPackItem(typeof(BaseInstrument), 1)};

		public static readonly LootPackItem[] LowScrollItems = new[]
		{new LootPackItem(typeof(ClumsyScroll), 1)/*, new LootPackItem(typeof(NetherBoltScroll), 1)*/};

		public static readonly LootPackItem[] MedScrollItems = new[]
		{
			new LootPackItem(typeof(ArchCureScroll), 1)/*, new LootPackItem(typeof(AnimatedWeaponScroll), 1),
			new LootPackItem(typeof(PurgeMagicScroll), 1), new LootPackItem(typeof(SleepScroll), 1),
			new LootPackItem(typeof(MassSleepScroll), 1)*/
		};

		public static readonly LootPackItem[] HighScrollItems = new[]
		{
			new LootPackItem(typeof(SummonAirElementalScroll), 1)/*, new LootPackItem(typeof(SpellPlagueScroll), 1),
			new LootPackItem(typeof(HailStormScroll), 1), new LootPackItem(typeof(CleansingWindsScroll), 1),
			new LootPackItem(typeof(BombardScroll), 1)*/
		};

		public static readonly LootPackItem[] GemItems = new[] {new LootPackItem(typeof(Amber), 1)};

		public static readonly LootPackItem[] PotionItems = new[]
		{
			new LootPackItem(typeof(AgilityPotion), 1), new LootPackItem(typeof(StrengthPotion), 1),
			new LootPackItem(typeof(RefreshPotion), 1), new LootPackItem(typeof(LesserCurePotion), 1),
			new LootPackItem(typeof(LesserHealPotion), 1), new LootPackItem(typeof(LesserPoisonPotion), 1)
		};

		#region Old Magic Items
		public static readonly LootPackItem[] OldMagicItems = new[]
		{
			new LootPackItem(typeof(BaseJewel), 1), new LootPackItem(typeof(BaseArmor), 4),
			new LootPackItem(typeof(BaseWeapon), 3), new LootPackItem(typeof(BaseRanged), 1),
			new LootPackItem(typeof(BaseShield), 1)
		};
		#endregion

		#region AOS Magic Items
		public static readonly LootPackItem[] AosMagicItemsPoor = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 3), new LootPackItem(typeof(BaseRanged), 1),
			new LootPackItem(typeof(BaseArmor), 4), new LootPackItem(typeof(BaseShield), 1),
			new LootPackItem(typeof(BaseJewel), 2), new LootPackItem(typeof(BaseClothing), 2)
		};

		public static readonly LootPackItem[] AosMagicItemsMeagerType1 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 56), new LootPackItem(typeof(BaseRanged), 14),
			new LootPackItem(typeof(BaseArmor), 81), new LootPackItem(typeof(BaseShield), 11),
			new LootPackItem(typeof(BaseJewel), 42), new LootPackItem(typeof(BaseClothing), 50)
		};

		public static readonly LootPackItem[] AosMagicItemsMeagerType2 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 28), new LootPackItem(typeof(BaseRanged), 7),
			new LootPackItem(typeof(BaseArmor), 40), new LootPackItem(typeof(BaseShield), 5),
			new LootPackItem(typeof(BaseJewel), 21), new LootPackItem(typeof(BaseClothing), 30)
		};

		public static readonly LootPackItem[] AosMagicItemsAverageType1 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 90), new LootPackItem(typeof(BaseRanged), 23),
			new LootPackItem(typeof(BaseArmor), 130), new LootPackItem(typeof(BaseShield), 17),
			new LootPackItem(typeof(BaseJewel), 68), new LootPackItem(typeof(BaseClothing), 80)
		};

		public static readonly LootPackItem[] AosMagicItemsAverageType2 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 54), new LootPackItem(typeof(BaseRanged), 13),
			new LootPackItem(typeof(BaseArmor), 77), new LootPackItem(typeof(BaseShield), 10),
			new LootPackItem(typeof(BaseJewel), 40), new LootPackItem(typeof(BaseClothing), 50)
		};

		public static readonly LootPackItem[] AosMagicItemsRichType1 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 211), new LootPackItem(typeof(BaseRanged), 53),
			new LootPackItem(typeof(BaseArmor), 303), new LootPackItem(typeof(BaseShield), 39),
			new LootPackItem(typeof(BaseJewel), 158), new LootPackItem(typeof(BaseClothing), 170)
		};

		public static readonly LootPackItem[] AosMagicItemsRichType2 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 170), new LootPackItem(typeof(BaseRanged), 43),
			new LootPackItem(typeof(BaseArmor), 245), new LootPackItem(typeof(BaseShield), 32),
			new LootPackItem(typeof(BaseJewel), 128), new LootPackItem(typeof(BaseClothing), 150)
		};

		public static readonly LootPackItem[] AosMagicItemsFilthyRichType1 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 219), new LootPackItem(typeof(BaseRanged), 55),
			new LootPackItem(typeof(BaseArmor), 315), new LootPackItem(typeof(BaseShield), 41),
			new LootPackItem(typeof(BaseJewel), 164), new LootPackItem(typeof(BaseClothing), 100),
            new LootPackItem(typeof(BaseHat), 180)
		};

		public static readonly LootPackItem[] AosMagicItemsFilthyRichType2 = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 239), new LootPackItem(typeof(BaseRanged), 60),
			new LootPackItem(typeof(BaseArmor), 343), new LootPackItem(typeof(BaseShield), 90),
			new LootPackItem(typeof(BaseJewel), 45), new LootPackItem(typeof(BaseClothing), 100),
            new LootPackItem(typeof(BaseHat), 80)
		};

		public static readonly LootPackItem[] AosMagicItemsUltraRich = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 276), new LootPackItem(typeof(BaseRanged), 69),
			new LootPackItem(typeof(BaseArmor), 397), new LootPackItem(typeof(BaseShield), 52),
			new LootPackItem(typeof(BaseJewel), 207), new LootPackItem(typeof(BaseClothing), 305),
            new LootPackItem(typeof(BaseHat), 400)//, new LootPackItem(RandomCloth(), 400) // test
		};

        public static readonly LootPackItem[] AosMagicItemsWeaponTest = new[]
		{
			new LootPackItem(typeof(BaseWeapon), 276)
		};

        public static readonly LootPackItem[] OnlyCloth = new[]
		{
			new LootPackItem(typeof(PlainDress), 1), new LootPackItem(typeof(BodySash), 1), new LootPackItem(typeof(Robe), 1),
            new LootPackItem(typeof(Doublet), 1), new LootPackItem(typeof(Surcoat), 1), new LootPackItem(typeof(MonkRobe), 1),
            new LootPackItem(typeof(FullApron), 1), new LootPackItem(typeof(Tunic), 1), new LootPackItem(typeof(FormalShirt), 1),
            new LootPackItem(typeof(JesterSuit), 1), new LootPackItem(typeof(ShortPants), 1), new LootPackItem(typeof(LongPants), 1),
            new LootPackItem(typeof(HalfApron), 1), new LootPackItem(typeof(FancyShirt), 1), new LootPackItem(typeof(FancyDress), 1),
            new LootPackItem(typeof(Boots), 1), new LootPackItem(typeof(FurBoots), 1), new LootPackItem(typeof(Boots), 1),
            new LootPackItem(typeof(ThighBoots), 1), new LootPackItem(typeof(Shoes), 1), new LootPackItem(typeof(Sandals), 1)
		};

        public static readonly LootPackItem[] OnlyArmor = new[]
		{
			new LootPackItem(typeof(BaseArmor), 397)
		};

        public static readonly LootPackItem[] OnlyShield = new[]
		{
			new LootPackItem(typeof(BaseShield), 397)
		};

        public static readonly LootPackItem[] OnlyJewel = new[]
		{
			new LootPackItem(typeof(BaseJewel), 397)
		};

        public static readonly LootPackItem[] OnlyHats = new[]
		{
			new LootPackItem(typeof(Bonnet), 1), new LootPackItem(typeof(FloppyHat), 1), new LootPackItem(typeof(FeatheredHat), 1),
            new LootPackItem(typeof(JesterHat), 1), new LootPackItem(typeof(WizardsHat), 1), new LootPackItem(typeof(SkullCap), 1),
            new LootPackItem(typeof(FloppyHat), 1), new LootPackItem(typeof(WideBrimHat), 1), new LootPackItem(typeof(Cap), 1),
            new LootPackItem(typeof(SkullCap), 1), new LootPackItem(typeof(Bandana), 1), new LootPackItem(typeof(BearMask), 1),
            new LootPackItem(typeof(DeerMask), 1), new LootPackItem(typeof(HornedTribalMask), 1), new LootPackItem(typeof(TribalMask), 1),
            new LootPackItem(typeof(TallStrawHat), 1), new LootPackItem(typeof(StrawHat), 1), new LootPackItem(typeof(SavageMask), 1),
            new LootPackItem(typeof(TricorneHat), 1)
		};

        public static readonly LootPackItem[] AllClothing = new[]
		{
			new LootPackItem(typeof(PlainDress), 1), new LootPackItem(typeof(BodySash), 1), new LootPackItem(typeof(Robe), 1),
            new LootPackItem(typeof(Doublet), 1), new LootPackItem(typeof(Surcoat), 1), new LootPackItem(typeof(MonkRobe), 1),
            new LootPackItem(typeof(FullApron), 1), new LootPackItem(typeof(Tunic), 1), new LootPackItem(typeof(FormalShirt), 1),
            new LootPackItem(typeof(JesterSuit), 1), new LootPackItem(typeof(ShortPants), 1), new LootPackItem(typeof(LongPants), 1),
            new LootPackItem(typeof(HalfApron), 1), new LootPackItem(typeof(FancyShirt), 1), new LootPackItem(typeof(FancyDress), 1),
            new LootPackItem(typeof(Boots), 1), new LootPackItem(typeof(FurBoots), 1), new LootPackItem(typeof(Boots), 1),
            new LootPackItem(typeof(ThighBoots), 1), new LootPackItem(typeof(Shoes), 1), new LootPackItem(typeof(Sandals), 1),
            new LootPackItem(typeof(Bonnet), 1), new LootPackItem(typeof(FloppyHat), 1), new LootPackItem(typeof(FeatheredHat), 1),
            new LootPackItem(typeof(JesterHat), 1), new LootPackItem(typeof(WizardsHat), 1), new LootPackItem(typeof(SkullCap), 1),
            new LootPackItem(typeof(FloppyHat), 1), new LootPackItem(typeof(WideBrimHat), 1), new LootPackItem(typeof(Cap), 1),
            new LootPackItem(typeof(SkullCap), 1), new LootPackItem(typeof(Bandana), 1), new LootPackItem(typeof(BearMask), 1),
            new LootPackItem(typeof(DeerMask), 1), new LootPackItem(typeof(HornedTribalMask), 1), new LootPackItem(typeof(TribalMask), 1),
            new LootPackItem(typeof(TallStrawHat), 1), new LootPackItem(typeof(StrawHat), 1), new LootPackItem(typeof(SavageMask), 1),
            new LootPackItem(typeof(TricorneHat), 1)
		};

        public static readonly LootPackItem[] LootTesterz = new[] // seems broken, dunno why
		{
			new LootPackItem(typeof(BaseClothing), 400), new LootPackItem(typeof(BaseHat), 400)
		};

		#endregion

		#region ML definitions
		public static readonly LootPack MlRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "4d50+450"),
					new LootPackEntry(false, AosMagicItemsRichType1, 100.00, 1, 3, 0, 75),
					new LootPackEntry(false, AosMagicItemsRichType1, 80.00, 1, 3, 0, 75),
					new LootPackEntry(false, AosMagicItemsRichType1, 60.00, 1, 5, 0, 100),
					//new LootPackEntry(false, Instruments, 1.00, 1)
				});
		#endregion

		#region SE definitions
		public static readonly LootPack SePoor =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "2d10+20"), new LootPackEntry(false, AosMagicItemsPoor, 1.00, 1, 5, 0, 100)//,
					//new LootPackEntry(false, Instruments, 0.02, 1)
				});

		public static readonly LootPack SeMeager =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "4d10+40"),
					new LootPackEntry(false, AosMagicItemsMeagerType1, 20.40, 1, 2, 0, 50),
					new LootPackEntry(false, AosMagicItemsMeagerType2, 10.20, 1, 5, 0, 100)//,
					//new LootPackEntry(false, Instruments, 0.10, 1)
				});

		public static readonly LootPack SeAverage =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "8d10+100"),
					new LootPackEntry(false, AosMagicItemsAverageType1, 32.80, 1, 3, 0, 50),
					new LootPackEntry(false, AosMagicItemsAverageType1, 32.80, 1, 4, 0, 75),
					new LootPackEntry(false, AosMagicItemsAverageType2, 19.50, 1, 5, 0, 100)//,
					//new LootPackEntry(false, Instruments, 0.40, 1)
				});

		public static readonly LootPack SeRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "15d10+225"),
					new LootPackEntry(false, AosMagicItemsRichType1, 76.30, 1, 4, 0, 75),
					new LootPackEntry(false, AosMagicItemsRichType1, 76.30, 1, 4, 0, 75),
					new LootPackEntry(false, AosMagicItemsRichType2, 61.70, 1, 5, 0, 100)//,
					//new LootPackEntry(false, Instruments, 1.00, 1)
				});

		public static readonly LootPack SeFilthyRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "3d100+400"),
					new LootPackEntry(false, AosMagicItemsFilthyRichType1, 79.50, 1, 5, 0, 100),
					new LootPackEntry(false, AosMagicItemsFilthyRichType1, 79.50, 1, 5, 0, 100),
					new LootPackEntry(false, AosMagicItemsFilthyRichType2, 77.60, 1, 5, 25, 100)//,
					//new LootPackEntry(false, Instruments, 2.00, 1)
				});

		public static readonly LootPack SeUltraRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "6d100+600"),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100)//,
					//new LootPackEntry(false, Instruments, 2.00, 1)
				});

		public static readonly LootPack SeSuperBoss =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "10d100+800"),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 50, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 50, 100)//,
					//new LootPackEntry(false, Instruments, 2.00, 1)
				});
		#endregion

		#region AOS definitions
		public static readonly LootPack AosPoor =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "1d10+10"), new LootPackEntry(false, AosMagicItemsPoor, 0.02, 1, 5, 0, 90),
					//new LootPackEntry(false, Instruments, 0.02, 1)
				});

		public static readonly LootPack AosMeager =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "3d10+20"),
					new LootPackEntry(false, AosMagicItemsMeagerType1, 1.00, 1, 2, 0, 10),
					new LootPackEntry(false, AosMagicItemsMeagerType2, 0.20, 1, 5, 0, 90),
					//new LootPackEntry(false, Instruments, 0.10, 1)
				});

		public static readonly LootPack AosAverage =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "5d10+50"),
					new LootPackEntry(false, AosMagicItemsAverageType1, 5.00, 1, 4, 0, 20),
					new LootPackEntry(false, AosMagicItemsAverageType1, 2.00, 1, 3, 0, 50),
					new LootPackEntry(false, AosMagicItemsAverageType2, 0.50, 1, 5, 0, 90),
					//new LootPackEntry(false, Instruments, 0.40, 1)
				});

		public static readonly LootPack AosRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "10d10+150"),
					new LootPackEntry(false, AosMagicItemsRichType1, 20.00, 1, 4, 0, 40),
					new LootPackEntry(false, AosMagicItemsRichType1, 10.00, 1, 5, 0, 60),
					new LootPackEntry(false, AosMagicItemsRichType2, 1.00, 1, 5, 0, 90) //new LootPackEntry(false, Instruments, 1.00, 1)
				});

		public static readonly LootPack AosFilthyRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "2d100+200"),
					new LootPackEntry(false, AosMagicItemsFilthyRichType1, 33.00, 1, 4, 0, 50),
					new LootPackEntry(false, AosMagicItemsFilthyRichType1, 33.00, 1, 4, 0, 60),
					new LootPackEntry(false, AosMagicItemsFilthyRichType2, 20.00, 1, 5, 0, 75),
					new LootPackEntry(false, AosMagicItemsFilthyRichType2, 5.00, 1, 5, 0, 100),
				});

		public static readonly LootPack AosUltraRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 35, 100),
				});

        public static readonly LootPack WeaponTest =
    new LootPack(
        new[]
				{
					//new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, AosMagicItemsWeaponTest, 100.00, 1, 5, 25, 100),
				});

        public static readonly LootPack HatTest =
new LootPack(
new[]
				{
					//new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, OnlyHats, 100.00, 1, 5, 25, 100),
				});

        public static readonly LootPack JewelTest =
new LootPack(
new[]
				{
					//new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, OnlyHats, 100.00, 1, 5, 25, 100),
				});
        
        public static readonly LootPack ClothTest =
new LootPack(
new[]
				{
					//new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, OnlyCloth, 100.00, 1, 5, 25, 100),
				});

        public static readonly LootPack ArmorTest =
new LootPack(
new[]
				{
					//new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, OnlyArmor, 100.00, 1, 5, 25, 100),
				});

        public static readonly LootPack ShieldTest =
new LootPack(
new[]
				{
					//new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, OnlyShield, 100.00, 1, 5, 25, 100),
				});

		public static readonly LootPack AosSuperBoss =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "5d100+500"),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 25, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 33, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 50, 100),
					new LootPackEntry(false, AosMagicItemsUltraRich, 100.00, 1, 5, 50, 100),
					//new LootPackEntry(false, Instruments, 2.00, 1)
				});
		#endregion

		#region Pre-AOS definitions
		public static readonly LootPack OldPoor =
			new LootPack(new[] {new LootPackEntry(true, Gold, 100.00, "1d25")//, new LootPackEntry(false, Instruments, 0.02, 1)
            });

		public static readonly LootPack OldMeager =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "5d10+25"), //new LootPackEntry(false, Instruments, 0.10, 1),
					new LootPackEntry(false, OldMagicItems, 1.00, 1, 1, 0, 60),
					new LootPackEntry(false, OldMagicItems, 0.20, 1, 1, 10, 70)
				});

		public static readonly LootPack OldAverage =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "10d10+50"), //new LootPackEntry(false, Instruments, 0.40, 1),
					new LootPackEntry(false, OldMagicItems, 5.00, 1, 1, 20, 80),
					new LootPackEntry(false, OldMagicItems, 2.00, 1, 1, 30, 90),
					new LootPackEntry(false, OldMagicItems, 0.50, 1, 1, 40, 100)
				});

		public static readonly LootPack OldRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "10d10+250"), //new LootPackEntry(false, Instruments, 1.00, 1),
					new LootPackEntry(false, OldMagicItems, 20.00, 1, 1, 60, 100),
					new LootPackEntry(false, OldMagicItems, 10.00, 1, 1, 65, 100),
					new LootPackEntry(false, OldMagicItems, 1.00, 1, 1, 70, 100)
				});

		public static readonly LootPack OldFilthyRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "2d125+400"), //new LootPackEntry(false, Instruments, 2.00, 1),
					new LootPackEntry(false, OldMagicItems, 33.00, 1, 1, 50, 100),
					new LootPackEntry(false, OldMagicItems, 33.00, 1, 1, 60, 100),
					new LootPackEntry(false, OldMagicItems, 20.00, 1, 1, 70, 100),
					new LootPackEntry(false, OldMagicItems, 5.00, 1, 1, 80, 100)
				});

		public static readonly LootPack OldUltraRich =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "5d100+500"), //new LootPackEntry(false, Instruments, 2.00, 1),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 40, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 40, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 50, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 50, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 60, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 60, 100)
				});

		public static readonly LootPack OldSuperBoss =
			new LootPack(
				new[]
				{
					new LootPackEntry(true, Gold, 100.00, "5d100+500"), //new LootPackEntry(false, Instruments, 2.00, 1),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 40, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 40, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 40, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 50, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 50, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 50, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 60, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 60, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 60, 100),
					new LootPackEntry(false, OldMagicItems, 100.00, 1, 1, 70, 100)
				});
		#endregion

		#region Generic accessors
		public static LootPack Poor { get { return Core.SE ? SePoor : Core.AOS ? AosPoor : OldPoor; } }
		public static LootPack Meager { get { return Core.SE ? SeMeager : Core.AOS ? AosMeager : OldMeager; } }
		public static LootPack Average { get { return Core.SE ? SeAverage : Core.AOS ? AosAverage : OldAverage; } }
		public static LootPack Rich { get { return Core.SE ? SeRich : Core.AOS ? AosRich : OldRich; } }
		public static LootPack FilthyRich { get { return Core.SE ? SeFilthyRich : Core.AOS ? AosFilthyRich : OldFilthyRich; } }
		public static LootPack UltraRich { get { return Core.SE ? SeUltraRich : Core.AOS ? AosUltraRich : OldUltraRich; } }
		public static LootPack SuperBoss { get { return Core.SE ? SeSuperBoss : Core.AOS ? AosSuperBoss : OldSuperBoss; } }
		#endregion

		public static readonly LootPack LowScrolls = new LootPack(new[] {new LootPackEntry(false, LowScrollItems, 100.00, 1)});

		public static readonly LootPack MedScrolls = new LootPack(new[] {new LootPackEntry(false, MedScrollItems, 100.00, 1)});

		public static readonly LootPack HighScrolls =
			new LootPack(new[] {new LootPackEntry(false, HighScrollItems, 100.00, 1)});

		public static readonly LootPack Gems = new LootPack(new[] {new LootPackEntry(false, GemItems, 100.00, 1)});

		public static readonly LootPack Potions = new LootPack(new[] {new LootPackEntry(false, PotionItems, 100.00, 1)});

		#region Mondain's Legacy
		public static readonly LootPackItem[] ParrotItem = new[] {new LootPackItem(typeof(ParrotItem), 1)};

		public static readonly LootPack Parrot = new LootPack(new[] {new LootPackEntry(false, ParrotItem, 10.00, 1)});
		#endregion
	}

	public class LootPackEntry
	{
		private LootPackDice m_Quantity;

		private int m_MaxProps, m_MinIntensity, m_MaxIntensity;

		private readonly bool m_AtSpawnTime;

		private LootPackItem[] m_Items;

		public int Chance { get; set; }

		public LootPackDice Quantity { get { return m_Quantity; } set { m_Quantity = value; } }

		public int MaxProps { get { return m_MaxProps; } set { m_MaxProps = value; } }

		public int MinIntensity { get { return m_MinIntensity; } set { m_MinIntensity = value; } }

		public int MaxIntensity { get { return m_MaxIntensity; } set { m_MaxIntensity = value; } }

		public LootPackItem[] Items { get { return m_Items; } set { m_Items = value; } }

		private static bool IsInTokuno(Mobile m)
		{
			if (m == null)
			{
				return false;
			}

			if (m.Region.IsPartOf("Fan Dancer's Dojo"))
			{
				return true;
			}

			if (m.Region.IsPartOf("Yomotsu Mines"))
			{
				return true;
			}

			return m.Map == Map.Tokuno;
		}

		#region Mondain's Legacy
		private static bool IsMondain(Mobile m)
		{
			return MondainsLegacy.IsMLRegion(m.Region);
		}
		#endregion

		#region Stygian Abyss
		private static bool IsStygianAbyss(Mobile m)
		{
			if (m == null)
			{
				return false;
			}

			if (m.Region.IsPartOf("Ter Mur"))
			{
				return true;
			}

			if (m.Region.IsPartOf("AbyssEntrance"))
			{
				return true;
			}

			if (m.Region.IsPartOf("Abyss"))
			{
				return true;
			}

			if (m.Region.IsPartOf("StygianDragonLair"))
			{
				return true;
			}

			if (m.Region.IsPartOf("MedusasLair"))
			{
				return true;
			}

			if (m.Region.IsPartOf("NPC Encampment"))
			{
				return true;
			}

			return m.Map == Map.TerMur;
		}
		#endregion

		public Item Construct(Mobile from, int luckChance, bool spawning)
		{
			if (m_AtSpawnTime != spawning)
			{
				return null;
			}

			int totalChance = 0;

			for (int i = 0; i < m_Items.Length; ++i)
			{
				totalChance += m_Items[i].Chance;
			}

			int rnd = Utility.Random(totalChance);

			for (int i = 0; i < m_Items.Length; ++i)
			{
				LootPackItem item = m_Items[i];

				if (rnd < item.Chance)
				{
                    Item gen_item = item.Construct(IsInTokuno(from), IsMondain(from));
                    //JustZH: moved this here instead of Mutate to be able to have Mutate static to be called from chest classes etc.
                    if (gen_item != null && gen_item.Stackable)
                    {
                        gen_item.Amount = m_Quantity.Roll();
                    }
                    return Mutate(from, luckChance, gen_item);
				}

				rnd -= item.Chance;
			}

			return null;
		}

		private int GetRandomOldBonus()
		{
			int rnd = Utility.RandomMinMax(m_MinIntensity, m_MaxIntensity);

			if (50 > rnd)
			{
				return 1;
			}
			else
			{
				rnd -= 50;
			}

			if (25 > rnd)
			{
				return 2;
			}
			else
			{
				rnd -= 25;
			}

			if (14 > rnd)
			{
				return 3;
			}
			else
			{
				rnd -= 14;
			}

			if (8 > rnd)
			{
				return 4;
			}

			return 5;
		}
        public static Item Mutate(Mobile from, int luckChance, Item item, int magicLevel)
        {
            int magic_level = magicLevel;
            if(from != null)
            {
                //JustZH: mobile's magiclevel overwrites parameter
                magic_level = ((BaseCreature)from).MagicLevel;
            }
            if (item != null)
            {
                //if (item is BaseWeapon && 1 > Utility.Random(100))
                //{
                //    item.Delete();
                //    item = new FireHorn();
                //    return item;
                //}

                if (item is BaseWeapon || item is BaseArmor || item is BaseJewel || item is BaseClothing || item is BaseHat)
                {
                    if (Core.AOS)
                    {
                        //int bonusProps = GetBonusProperties();
                        //int min = m_MinIntensity;
                        //int max = m_MaxIntensity;

                        //if (bonusProps < m_MaxProps && LootPack.CheckLuck(luckChance))
                        //{
                        //    ++bonusProps;
                        //}

                        //int props = 1 + bonusProps;

                        // Make sure we're not spawning items with 6 properties.
                        //if (props > m_MaxProps)
                        //{
                        //    props = m_MaxProps;
                        //}

                        if (item is BaseWeapon)
                        {
                            BaseRunicToolRewrite.ApplyEffectWeapon((BaseWeapon)item, magic_level);
                            //BaseRunicTool.ApplyAttributesTo((BaseWeapon)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity);
                        }
                        else if (item is BaseArmor)
                        {
                            BaseRunicToolRewrite.ApplyEffectArmor((BaseArmor)item, magic_level);
                            //	BaseRunicTool.ApplyAttributesTo((BaseArmor)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity);
                        }
                        else if (item is BaseJewel)
                        {
                          //  Console.WriteLine("Jewel!");
                            BaseRunicToolRewrite.ApplyEffectJewlery((BaseJewel)item, magic_level);
                            //BaseRunicTool.ApplyAttributesTo((BaseJewel)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity);
                        }
                        else if (item is BaseClothing)
                        {
                           // Console.WriteLine("Cloth!");
                            BaseRunicToolRewrite.ApplyEffectClothing((BaseClothing)item, magic_level);
                            //  BaseRunicTool.ApplyAttributesTo((BaseClothing)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity);
                        }
                        else if (item is BaseHat)
                        {
                          //  Console.WriteLine("Hat!");
                            BaseRunicToolRewrite.ApplyEffectClothing((BaseHat)item, magic_level);
                            //  BaseRunicTool.ApplyAttributesTo((BaseClothing)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity);
                        }
                    }
                    //else // not aos
                    //{
                    //    if (item is BaseWeapon)
                    //    {
                    //        BaseWeapon weapon = (BaseWeapon)item;

                    //        if (80 > Utility.Random(100))
                    //        {
                    //            weapon.AccuracyLevel = (WeaponAccuracyLevel)GetRandomOldBonus();
                    //        }

                    //        if (60 > Utility.Random(100))
                    //        {
                    //            weapon.DamageLevel = (WeaponDamageLevel)GetRandomOldBonus();
                    //        }

                    //        if (40 > Utility.Random(100))
                    //        {
                    //            weapon.DurabilityLevel = (WeaponDurabilityLevel)GetRandomOldBonus();
                    //        }

                    //        if (5 > Utility.Random(100))
                    //        {
                    //            weapon.Slayer = SlayerName.Silver;
                    //        }

                    //        if (from != null && weapon.AccuracyLevel == 0 && weapon.DamageLevel == 0 && weapon.DurabilityLevel == 0 &&
                    //            weapon.Slayer == SlayerName.None && 5 > Utility.Random(100))
                    //        {
                    //            //JustZH: only possible to get slayer if mob drops wepon like this.. should modify later!
                    //            weapon.Slayer = SlayerGroup.GetLootSlayerType(from.GetType());
                    //        }
                    //    }
                    //    else if (item is BaseArmor)
                    //    {
                    //        BaseArmor armor = (BaseArmor)item;

                    //        if (80 > Utility.Random(100))
                    //        {
                    //            armor.ProtectionLevel = (ArmorProtectionLevel)GetRandomOldBonus();
                    //        }

                    //        if (40 > Utility.Random(100))
                    //        {
                    //            armor.Durability = (ArmorDurabilityLevel)GetRandomOldBonus();
                    //        }
                    //    }
                    //}
                }
                else if (item is BaseInstrument)
                {
                    SlayerName slayer = SlayerName.None;

                    if (Core.AOS)
                    {
                        slayer = BaseRunicTool.GetRandomSlayer();
                    }
                    else
                    {
                        slayer = SlayerGroup.GetLootSlayerType(from.GetType());
                    }

                    if (slayer == SlayerName.None)
                    {
                        item.Delete();
                        return null;
                    }

                    BaseInstrument instr = (BaseInstrument)item;

                    instr.Quality = InstrumentQuality.Regular;
                    instr.Slayer = slayer;
                }

                //JustZH: moved to Construct()..
                //if (item.Stackable)
                //{
                //    item.Amount = m_Quantity.Roll();
                //}
            }

            return item;
        }
		public Item Mutate(Mobile from, int luckChance, Item item)
		{
            return Mutate(from, luckChance, item, 0);
		}

		public LootPackEntry(bool atSpawnTime, LootPackItem[] items, double chance, string quantity)
			: this(atSpawnTime, items, chance, new LootPackDice(quantity), 0, 0, 0)
		{ }

		public LootPackEntry(bool atSpawnTime, LootPackItem[] items, double chance, int quantity)
			: this(atSpawnTime, items, chance, new LootPackDice(0, 0, quantity), 0, 0, 0)
		{ }

		public LootPackEntry(
			bool atSpawnTime,
			LootPackItem[] items,
			double chance,
			string quantity,
			int maxProps,
			int minIntensity,
			int maxIntensity)
			: this(atSpawnTime, items, chance, new LootPackDice(quantity), maxProps, minIntensity, maxIntensity)
		{ }

		public LootPackEntry(
			bool atSpawnTime, LootPackItem[] items, double chance, int quantity, int maxProps, int minIntensity, int maxIntensity)
			: this(atSpawnTime, items, chance, new LootPackDice(0, 0, quantity), maxProps, minIntensity, maxIntensity)
		{ }

		public LootPackEntry(
			bool atSpawnTime,
			LootPackItem[] items,
			double chance,
			LootPackDice quantity,
			int maxProps,
			int minIntensity,
			int maxIntensity)
		{
			m_AtSpawnTime = atSpawnTime;
			m_Items = items;
			Chance = (int)(100 * chance);
			m_Quantity = quantity;
			m_MaxProps = maxProps;
			m_MinIntensity = minIntensity;
			m_MaxIntensity = maxIntensity;
		}

		public int GetBonusProperties()
		{
			int p0 = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0;

			switch (m_MaxProps)
			{
				case 1:
					p0 = 3;
					p1 = 1;
					break;
				case 2:
					p0 = 6;
					p1 = 3;
					p2 = 1;
					break;
				case 3:
					p0 = 10;
					p1 = 6;
					p2 = 3;
					p3 = 1;
					break;
				case 4:
					p0 = 16;
					p1 = 12;
					p2 = 6;
					p3 = 5;
					p4 = 1;
					break;
				case 5:
					p0 = 30;
					p1 = 25;
					p2 = 20;
					p3 = 15;
					p4 = 9;
					p5 = 1;
					break;
			}

			int pc = p0 + p1 + p2 + p3 + p4 + p5;

			int rnd = Utility.Random(pc);

			if (rnd < p5)
			{
				return 5;
			}
			else
			{
				rnd -= p5;
			}

			if (rnd < p4)
			{
				return 4;
			}
			else
			{
				rnd -= p4;
			}

			if (rnd < p3)
			{
				return 3;
			}
			else
			{
				rnd -= p3;
			}

			if (rnd < p2)
			{
				return 2;
			}
			else
			{
				rnd -= p2;
			}

			if (rnd < p1)
			{
				return 1;
			}

			return 0;
		}
	}

	public class LootPackItem
	{
		private Type m_Type;

		public Type Type { get { return m_Type; } set { m_Type = value; } }

		public int Chance { get; set; }

		private static readonly Type[] m_BlankTypes = new[] {typeof(BlankScroll)};

		private static readonly Type[][] m_NecroTypes = new[]
		{
			new[] // low
			{
				typeof(AnimateDeadScroll), typeof(BloodOathScroll), typeof(CorpseSkinScroll), typeof(CurseWeaponScroll),
				typeof(EvilOmenScroll), typeof(HorrificBeastScroll), typeof(MindRotScroll), typeof(PainSpikeScroll),
				typeof(SummonFamiliarScroll), typeof(WraithFormScroll)
			},
			new[] // med
			{typeof(LichFormScroll), typeof(PoisonStrikeScroll), typeof(StrangleScroll), typeof(WitherScroll)},
			((Core.SE)
				 ? new[] // high
				 {typeof(VengefulSpiritScroll), typeof(VampiricEmbraceScroll), typeof(ExorcismScroll)}
				 : new[] // high
				 {typeof(VengefulSpiritScroll), typeof(VampiricEmbraceScroll)})
		};

		public static Item RandomScroll(int index, int minCircle, int maxCircle)
		{
			--minCircle;
			--maxCircle;

			int scrollCount = ((maxCircle - minCircle) + 1) * 8;

			if (index == 0)
			{
				scrollCount += m_BlankTypes.Length;
			}

			if (Core.AOS)
			{
				scrollCount += m_NecroTypes[index].Length;
			}

			int rnd = Utility.Random(scrollCount);

			if (index == 0 && rnd < m_BlankTypes.Length)
			{
				return Loot.Construct(m_BlankTypes);
			}
			else if (index == 0)
			{
				rnd -= m_BlankTypes.Length;
			}

			if (Core.AOS && rnd < m_NecroTypes.Length)
			{
				return Loot.Construct(m_NecroTypes[index]);
			}
			else if (Core.AOS)
			{
				rnd -= m_NecroTypes[index].Length;
			}

			return Loot.RandomScroll(minCircle * 8, (maxCircle * 8) + 7, SpellbookType.Regular);
		}

		public Item Construct(bool inTokuno, bool isMondain)
		{
			try
			{
				Item item;

				if (m_Type == typeof(BaseRanged))
				{
					item = Loot.RandomRangedWeapon(inTokuno, isMondain);
				}
				else if (m_Type == typeof(BaseWeapon))
				{
					item = Loot.RandomWeapon(inTokuno, isMondain);
				}
				else if (m_Type == typeof(BaseArmor))
				{
					item = Loot.RandomArmorOrHat(inTokuno, isMondain);
				}
				else if (m_Type == typeof(BaseShield))
				{
					item = Loot.RandomShield();
				}
				else if (m_Type == typeof(BaseJewel))
				{
					item = Core.AOS ? Loot.RandomJewelry() : Loot.RandomArmorOrShieldOrWeapon();
				}
				else if (m_Type == typeof(BaseInstrument))
				{
					item = Loot.RandomInstrument();
				}
				else if (m_Type == typeof(Amber)) // gem
				{
					item = Loot.RandomGem();
				}
				else if (m_Type == typeof(ClumsyScroll)) // low scroll
				{
					item = RandomScroll(0, 1, 3);
				}
				else if (m_Type == typeof(ArchCureScroll)) // med scroll
				{
					item = RandomScroll(1, 4, 7);
				}
				else if (m_Type == typeof(SummonAirElementalScroll)) // high scroll
				{
					item = RandomScroll(2, 8, 8);
				}
				else
				{
					item = Activator.CreateInstance(m_Type) as Item;
				}

				return item;
			}
			catch
			{ }

			return null;
		}

		public LootPackItem(Type type, int chance)
		{
			m_Type = type;
			Chance = chance;
		}
	}

	public class LootPackDice
	{
		private int m_Count, m_Sides, m_Bonus;

		public int Count { get { return m_Count; } set { m_Count = value; } }

		public int Sides { get { return m_Sides; } set { m_Sides = value; } }

		public int Bonus { get { return m_Bonus; } set { m_Bonus = value; } }

		public int Roll()
		{
			int v = m_Bonus;

			for (int i = 0; i < m_Count; ++i)
			{
				v += Utility.Random(1, m_Sides);
			}

			return v;
		}

		public LootPackDice(string str)
		{
			int start = 0;
			int index = str.IndexOf('d', start);

			if (index < start)
			{
				return;
			}

			m_Count = Utility.ToInt32(str.Substring(start, index - start));

			bool negative;

			start = index + 1;
			index = str.IndexOf('+', start);

			if (negative = (index < start))
			{
				index = str.IndexOf('-', start);
			}

			if (index < start)
			{
				index = str.Length;
			}

			m_Sides = Utility.ToInt32(str.Substring(start, index - start));

			if (index == str.Length)
			{
				return;
			}

			start = index + 1;
			index = str.Length;

			m_Bonus = Utility.ToInt32(str.Substring(start, index - start));

			if (negative)
			{
				m_Bonus *= -1;
			}
		}

		public LootPackDice(int count, int sides, int bonus)
		{
			m_Count = count;
			m_Sides = sides;
			m_Bonus = bonus;
		}
	}
}
