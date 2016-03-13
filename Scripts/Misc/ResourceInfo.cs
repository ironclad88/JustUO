using System;
using System.Collections;

namespace Server.Items
{
    public enum CraftResource
    {
        None = 0,
        Iron = 1,
        DullCopper,
        ShadowIron,
        Copper,
        Bronze,
        Gold,
        Agapite,
        Verite,
        Valorite,
        ZuluMetal,
        Onyx,
        Pyrite,
        Malachite,
        Azurite,
        Platinum,
        Lavarock,
        Mystic,
        Spike,
        Fruity,
        IceRock,
        SilverRock,
        Spectral,
        Undead,
        DarkPagan,
        OldBritain,
        Virginity,
        BlackDwarf,
        RedElven,
        Dripstone,
        Executor,
        Peachblue,
        Destruction,
        Anra,
        GoddessMetal,
        CrystalMetal,
        DoomMetal,
        ETS,
        DSR,
        RND,

        RegularLeather = 101,
        SpinedLeather,
        HornedLeather,
        BarbedLeather,
        DragonLeather,
        RatLeather,
        WolfLeather,
        BearLeather,
        OrcLeather,
        SerpentLeather,
        LizardLeather,
        TrollLeather,
        OstardLeather,
        NecromancerLeather,
        LavaLeather,
        LicheLeather,
        TerathanLeather,
        DaemonLeather,
        IceCrystalLeather,
        WyrmLeather,
        BalronLeather,
        GoldenDragonLeather,
        SilverDragonLeather,
        BalrogLeather,
        AngelLeather,


        RedScales = 201,
        YellowScales,
        BlackScales,
        GreenScales,
        WhiteScales,
        BlueScales,

        RegularWood = 301,
        OakWood,
        AshWood,
        YewWood,
        Heartwood,
        Bloodwood,
        Frostwood,
        Pinetree,
        Cherry,
        Oak,
        PurplePassion,
        GoldenReflection,
        Hardranger,
        Jade,
        Darkwood,
        Stonewood,
        Sun,
        Gauntlet,
        Swamp,
        Stardust,
        SilverLeaf,
        Stormteal,
        Emerald,
        Blood,
        CrystalLog,
        Bloodhorse,
        DoomLog,
        GoddessLog,
        ZuluLog,
        Darkness,
        Elven
    }

    public enum CraftResourceType
    {
        None,
        Metal,
        Leather,
        Scales,
        Wood
    }

    public class CraftAttributeInfo
    {
        private int m_CraftDifficulty;
        private double m_Quality = 1.0;
        private int m_Dexpen;

        private int m_WeaponFireDamage;
        private int m_WeaponColdDamage;
        private int m_WeaponPoisonDamage;
        private int m_WeaponEnergyDamage;
        private int m_WeaponChaosDamage;
        private int m_WeaponDirectDamage;
        private int m_WeaponDurability;
        private int m_WeaponLuck;
        private int m_WeaponGoldIncrease;
        private int m_WeaponLowerRequirements;

        private int m_ArmorPhysicalResist;
        private int m_ArmorFireResist;
        private int m_ArmorColdResist;
        private int m_ArmorPoisonResist;
        private int m_ArmorEnergyResist;
        private int m_ArmorEarthResist;
        private int m_ArmorNecroResist;
        private int m_ArmorMagicResist;
        private int m_ArmorHolyResist;
        private int m_ArmorDurability;
        private int m_ArmorLuck;
        private int m_ArmorGoldIncrease;
        private int m_ArmorLowerRequirements;

        private int m_RunicMinAttributes;
        private int m_RunicMaxAttributes;
        private int m_RunicMinIntensity;
        private int m_RunicMaxIntensity;

        public int Difficulty
        {
            get
            {
                return this.m_CraftDifficulty;
            }
            set
            {
                this.m_CraftDifficulty = value;
            }
        }

        public double Quality
        {
            get
            {
                return this.m_Quality;
            }
            set
            {
                this.m_Quality = value;
            }
        }

        public int DexPenReduction
        {
            get
            {
                return this.m_Dexpen;
            }
            set
            {
                this.m_Dexpen = value;
            }
        }

        public int WeaponFireDamage
        {
            get
            {
                return this.m_WeaponFireDamage;
            }
            set
            {
                this.m_WeaponFireDamage = value;
            }
        }
        public int WeaponColdDamage
        {
            get
            {
                return this.m_WeaponColdDamage;
            }
            set
            {
                this.m_WeaponColdDamage = value;
            }
        }
        public int WeaponPoisonDamage
        {
            get
            {
                return this.m_WeaponPoisonDamage;
            }
            set
            {
                this.m_WeaponPoisonDamage = value;
            }
        }
        public int WeaponEnergyDamage
        {
            get
            {
                return this.m_WeaponEnergyDamage;
            }
            set
            {
                this.m_WeaponEnergyDamage = value;
            }
        }
        public int WeaponChaosDamage
        {
            get
            {
                return this.m_WeaponChaosDamage;
            }
            set
            {
                this.m_WeaponChaosDamage = value;
            }
        }
        public int WeaponDirectDamage
        {
            get
            {
                return this.m_WeaponDirectDamage;
            }
            set
            {
                this.m_WeaponDirectDamage = value;
            }
        }
        public int WeaponDurability
        {
            get
            {
                return this.m_WeaponDurability;
            }
            set
            {
                this.m_WeaponDurability = value;
            }
        }
        public int WeaponLuck
        {
            get
            {
                return this.m_WeaponLuck;
            }
            set
            {
                this.m_WeaponLuck = value;
            }
        }
        public int WeaponGoldIncrease
        {
            get
            {
                return this.m_WeaponGoldIncrease;
            }
            set
            {
                this.m_WeaponGoldIncrease = value;
            }
        }
        public int WeaponLowerRequirements
        {
            get
            {
                return this.m_WeaponLowerRequirements;
            }
            set
            {
                this.m_WeaponLowerRequirements = value;
            }
        }

        public int ArmorPhysicalResist
        {
            get
            {
                return this.m_ArmorPhysicalResist;
            }
            set
            {
                this.m_ArmorPhysicalResist = value;
            }
        }
        public int ArmorFireResist
        {
            get
            {
                return this.m_ArmorFireResist;
            }
            set
            {
                this.m_ArmorFireResist = value;
            }
        }
        public int ArmorColdResist
        {
            get
            {
                return this.m_ArmorColdResist;
            }
            set
            {
                this.m_ArmorColdResist = value;
            }
        }
        public int ArmorPoisonResist
        {
            get
            {
                return this.m_ArmorPoisonResist;
            }
            set
            {
                this.m_ArmorPoisonResist = value;
            }
        }
        public int ArmorEnergyResist
        {
            get
            {
                return this.m_ArmorEnergyResist;
            }
            set
            {
                this.m_ArmorEnergyResist = value;
            }
        }
        public int ArmorEarthResist
        {
            get
            {
                return this.m_ArmorEarthResist;
            }
            set
            {
                this.m_ArmorEarthResist = value;
            }
        }
        public int ArmorMagicResist
        {
            get
            {
                return this.m_ArmorMagicResist;
            }
            set
            {
                this.m_ArmorMagicResist = value;
            }
        }
        public int ArmorNecroResist
        {
            get
            {
                return this.m_ArmorNecroResist;
            }
            set
            {
                this.m_ArmorNecroResist = value;
            }
        }
        public int ArmorHolyResist
        {
            get
            {
                return this.m_ArmorHolyResist;
            }
            set
            {
                this.m_ArmorHolyResist = value;
            }
        }
        public int ArmorDurability
        {
            get
            {
                return this.m_ArmorDurability;
            }
            set
            {
                this.m_ArmorDurability = value;
            }
        }
        public int ArmorLuck
        {
            get
            {
                return this.m_ArmorLuck;
            }
            set
            {
                this.m_ArmorLuck = value;
            }
        }
        public int ArmorGoldIncrease
        {
            get
            {
                return this.m_ArmorGoldIncrease;
            }
            set
            {
                this.m_ArmorGoldIncrease = value;
            }
        }
        public int ArmorLowerRequirements
        {
            get
            {
                return this.m_ArmorLowerRequirements;
            }
            set
            {
                this.m_ArmorLowerRequirements = value;
            }
        }

        public int RunicMinAttributes
        {
            get
            {
                return this.m_RunicMinAttributes;
            }
            set
            {
                this.m_RunicMinAttributes = value;
            }
        }
        public int RunicMaxAttributes
        {
            get
            {
                return this.m_RunicMaxAttributes;
            }
            set
            {
                this.m_RunicMaxAttributes = value;
            }
        }
        public int RunicMinIntensity
        {
            get
            {
                return this.m_RunicMinIntensity;
            }
            set
            {
                this.m_RunicMinIntensity = value;
            }
        }
        public int RunicMaxIntensity
        {
            get
            {
                return this.m_RunicMaxIntensity;
            }
            set
            {
                this.m_RunicMaxIntensity = value;
            }
        }

        #region Mondain's Legacy
        private int m_WeaponDamage;
        private int m_WeaponHitChance;
        private int m_WeaponHitLifeLeech;
        private int m_WeaponRegenHits;
        private int m_WeaponSwingSpeed;

        private int m_ArmorDamage;
        private int m_ArmorHitChance;
        private int m_ArmorRegenHits;
        private int m_ArmorMage;

        private int m_ShieldPhysicalResist;
        private int m_ShieldFireResist;
        private int m_ShieldColdResist;
        private int m_ShieldPoisonResist;
        private int m_ShieldEnergyResist;

        public int WeaponDamage
        {
            get
            {
                return this.m_WeaponDamage;
            }
            set
            {
                this.m_WeaponDamage = value;
            }
        }
        public int WeaponHitChance
        {
            get
            {
                return this.m_WeaponHitChance;
            }
            set
            {
                this.m_WeaponHitChance = value;
            }
        }
        public int WeaponHitLifeLeech
        {
            get
            {
                return this.m_WeaponHitLifeLeech;
            }
            set
            {
                this.m_WeaponHitLifeLeech = value;
            }
        }
        public int WeaponRegenHits
        {
            get
            {
                return this.m_WeaponRegenHits;
            }
            set
            {
                this.m_WeaponRegenHits = value;
            }
        }
        public int WeaponSwingSpeed
        {
            get
            {
                return this.m_WeaponSwingSpeed;
            }
            set
            {
                this.m_WeaponSwingSpeed = value;
            }
        }

        public int ArmorDamage
        {
            get
            {
                return this.m_ArmorDamage;
            }
            set
            {
                this.m_ArmorDamage = value;
            }
        }
        public int ArmorHitChance
        {
            get
            {
                return this.m_ArmorHitChance;
            }
            set
            {
                this.m_ArmorHitChance = value;
            }
        }
        public int ArmorRegenHits
        {
            get
            {
                return this.m_ArmorRegenHits;
            }
            set
            {
                this.m_ArmorRegenHits = value;
            }
        }
        public int ArmorMage
        {
            get
            {
                return this.m_ArmorMage;
            }
            set
            {
                this.m_ArmorMage = value;
            }
        }

        public int ShieldPhysicalResist
        {
            get
            {
                return this.m_ShieldPhysicalResist;
            }
            set
            {
                this.m_ShieldPhysicalResist = value;
            }
        }
        public int ShieldFireResist
        {
            get
            {
                return this.m_ShieldFireResist;
            }
            set
            {
                this.m_ShieldFireResist = value;
            }
        }
        public int ShieldColdResist
        {
            get
            {
                return this.m_ShieldColdResist;
            }
            set
            {
                this.m_ShieldColdResist = value;
            }
        }
        public int ShieldPoisonResist
        {
            get
            {
                return this.m_ShieldPoisonResist;
            }
            set
            {
                this.m_ShieldPoisonResist = value;
            }
        }
        public int ShieldEnergyResist
        {
            get
            {
                return this.m_ShieldEnergyResist;
            }
            set
            {
                this.m_ShieldEnergyResist = value;
            }
        }
        #endregion

        public CraftAttributeInfo()
        {
        }

        public static readonly CraftAttributeInfo Blank;
        public static readonly CraftAttributeInfo DullCopper, ShadowIron, Copper, Bronze, Golden, Agapite, Verite, Valorite, ZuluMetal, Onyx, Pyrite, Malachite, Azurite, Platinum, Lavarock, Mystic, Spike, Fruity, IceRock, SilverRock, Spectral, Undead, DarkPagan, OldBritain, Virginity, BlackDwarf, RedElven, DripStone, Executor, Peachblue, Destruction, Anra, GoddessMetal, CrystalMetal, DoomMetal, ETS, DSR, RND;
        public static readonly CraftAttributeInfo Spined, Horned, Barbed, Dragon;
        public static readonly CraftAttributeInfo Rat, Wolf, Bear, Orc, Serpent, Lizard, Troll, Ostard, Necromancer, Terathan, Lava, Liche, Daemon, IceCrystal, Wyrm, Balron, GoldenDragon, SilverDragon, Balrog, Angel;
        public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales;
        public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood;
        public static readonly CraftAttributeInfo Pinetree, Cherry, Oak, PurplePassion, GoldenReflection, Hardranger, Jade, Darkwood, Stonewood, Sun, Gauntlet, Swamp, Stardust, SilverLeaf, Stormteal, Emerald, Blood, CrystalLog, Bloodhorse, DoomLog, GoddessLog, ZuluLog, Darkness, Elven;

        static CraftAttributeInfo()
        {
            const int level1_prot = 15; //25% on pol zh
            const int level2_prot = 30; //50% on pol zh
            const int level3_prot = 45; //75% on pol zh
            Blank = new CraftAttributeInfo();
            #region OldMetals
            CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();

            dullCopper.Difficulty = 30;
            dullCopper.Quality = 1.15;
         //   dullCopper.
            //dullCopper.ArmorPhysicalResist = 6;
            //dullCopper.ArmorDurability = 50;
            //dullCopper.ArmorLowerRequirements = 20;
            //dullCopper.WeaponDurability = 100;
            //dullCopper.WeaponLowerRequirements = 50;
            //dullCopper.RunicMinAttributes = 1;
            //dullCopper.RunicMaxAttributes = 2;
            //if (Core.ML)
            //{
            //    dullCopper.RunicMinIntensity = 40;
            //    dullCopper.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    dullCopper.RunicMinIntensity = 10;
            //    dullCopper.RunicMaxIntensity = 35;
            //}

            CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();

            //shadowIron.ArmorPhysicalResist = 2;
            //shadowIron.ArmorFireResist = 1;
            //shadowIron.ArmorEnergyResist = 5;
            //shadowIron.ArmorDurability = 100;
            //shadowIron.WeaponColdDamage = 20;
            //shadowIron.WeaponDurability = 50;
            //shadowIron.RunicMinAttributes = 2;
            //shadowIron.RunicMaxAttributes = 2;
            //if (Core.ML)
            //{
            //    shadowIron.RunicMinIntensity = 45;
            //    shadowIron.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    shadowIron.RunicMinIntensity = 20;
            //    shadowIron.RunicMaxIntensity = 45;
            //}

            CraftAttributeInfo copper = Copper = new CraftAttributeInfo();

            copper.Difficulty = 50;
            copper.Quality = 1.2;
            //copper.ArmorPhysicalResist = 1;
            //copper.ArmorFireResist = 1;
            //copper.ArmorPoisonResist = 5;
            //copper.ArmorEnergyResist = 2;
            //copper.WeaponPoisonDamage = 10;
            //copper.WeaponEnergyDamage = 20;
            //copper.RunicMinAttributes = 2;
            //copper.RunicMaxAttributes = 3;
            //if (Core.ML)
            //{
            //    copper.RunicMinIntensity = 50;
            //    copper.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    copper.RunicMinIntensity = 25;
            //    copper.RunicMaxIntensity = 50;
            //}

            CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();

            bronze.Difficulty = 15;
            bronze.Quality = 1.1;
            //bronze.ArmorPhysicalResist = 3;
            //bronze.ArmorColdResist = 5;
            //bronze.ArmorPoisonResist = 1;
            //bronze.ArmorEnergyResist = 1;
            //bronze.WeaponFireDamage = 40;
            //bronze.RunicMinAttributes = 3;
            //bronze.RunicMaxAttributes = 3;
            //if (Core.ML)
            //{
            //    bronze.RunicMinIntensity = 55;
            //    bronze.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    bronze.RunicMinIntensity = 30;
            //    bronze.RunicMaxIntensity = 65;
            //}

            CraftAttributeInfo golden = Golden = new CraftAttributeInfo(); // Used as Iron difficulty and Quality in CraftGumpItem.cs Line 334

            golden.Difficulty = 1;
            golden.Quality = 1.0;

            //golden.ArmorPhysicalResist = 1;
            //golden.ArmorFireResist = 1;
            //golden.ArmorColdResist = 2;
            //golden.ArmorEnergyResist = 2;
            //golden.ArmorLuck = 40;
            //golden.ArmorLowerRequirements = 30;
            //golden.WeaponLuck = 40;
            //golden.WeaponLowerRequirements = 50;
            //golden.RunicMinAttributes = 3;
            //golden.RunicMaxAttributes = 4;
            //if (Core.ML)
            //{
            //    golden.RunicMinIntensity = 60;
            //    golden.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    golden.RunicMinIntensity = 35;
            //    golden.RunicMaxIntensity = 75;
            //}

            CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();

            //agapite.ArmorPhysicalResist = 2;
            //agapite.ArmorFireResist = 3;
            //agapite.ArmorColdResist = 2;
            //agapite.ArmorPoisonResist = 2;
            //agapite.ArmorEnergyResist = 2;
            //agapite.WeaponColdDamage = 30;
            //agapite.WeaponEnergyDamage = 20;
            //agapite.RunicMinAttributes = 4;
            //agapite.RunicMaxAttributes = 4;
            //if (Core.ML)
            //{
            //    agapite.RunicMinIntensity = 65;
            //    agapite.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    agapite.RunicMinIntensity = 40;
            //    agapite.RunicMaxIntensity = 80;
            //}

            CraftAttributeInfo verite = Verite = new CraftAttributeInfo();

            //verite.ArmorPhysicalResist = 3;
            //verite.ArmorFireResist = 3;
            //verite.ArmorColdResist = 2;
            //verite.ArmorPoisonResist = 3;
            //verite.ArmorEnergyResist = 1;
            //verite.WeaponPoisonDamage = 40;
            //verite.WeaponEnergyDamage = 20;
            //verite.RunicMinAttributes = 4;
            //verite.RunicMaxAttributes = 5;
            //if (Core.ML)
            //{
            //    verite.RunicMinIntensity = 70;
            //    verite.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    verite.RunicMinIntensity = 45;
            //    verite.RunicMaxIntensity = 90;
            //}

            CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();

            //valorite.ArmorPhysicalResist = 4;
            //valorite.ArmorColdResist = 3;
            //valorite.ArmorPoisonResist = 3;
            //valorite.ArmorEnergyResist = 3;
            //valorite.ArmorDurability = 50;
            //valorite.WeaponFireDamage = 10;
            //valorite.WeaponColdDamage = 20;
            //valorite.WeaponPoisonDamage = 10;
            //valorite.WeaponEnergyDamage = 20;
            //valorite.RunicMinAttributes = 5;
            //valorite.RunicMaxAttributes = 5;
            //if (Core.ML)
            //{
            //    valorite.RunicMinIntensity = 85;
            //    valorite.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    valorite.RunicMinIntensity = 50;
            //    valorite.RunicMaxIntensity = 100;
            //}

            #endregion

            #region JustZHMetals
            CraftAttributeInfo zulu = ZuluMetal = new CraftAttributeInfo();

            //JustZH : reduce spell effectiveness on wearer
            zulu.DexPenReduction = 2;
            zulu.Difficulty = 129;
            zulu.Quality = 2.0;

            CraftAttributeInfo onyx = Onyx = new CraftAttributeInfo();

            onyx.DexPenReduction = 1;
            onyx.Difficulty = 70;
            onyx.Quality = 1.25;
            
            CraftAttributeInfo pyrite = Pyrite = new CraftAttributeInfo();

            pyrite.DexPenReduction = 1;
            pyrite.Difficulty = 85;
            pyrite.Quality = 1.3;
            
            CraftAttributeInfo malachite = Malachite = new CraftAttributeInfo();

            malachite.Difficulty = 95;
            malachite.Quality = 1.5;

            CraftAttributeInfo azurite = Azurite = new CraftAttributeInfo();

            azurite.DexPenReduction = 1;
            azurite.ArmorEnergyResist = level2_prot;
            azurite.Difficulty = 98;
            azurite.Quality = 1.5;

            CraftAttributeInfo platinum = Platinum = new CraftAttributeInfo();

            //JustZH : reduce magic damage on wearer
            platinum.Difficulty = 35;
            platinum.Quality = 1.15;

            CraftAttributeInfo lavarock = Lavarock = new CraftAttributeInfo();

            lavarock.ArmorFireResist = level2_prot;
            lavarock.Difficulty = 97;
            lavarock.Quality = 1.35;

            CraftAttributeInfo mystic = Mystic = new CraftAttributeInfo();

            //JustZH : reduce magic damage on wearer
            mystic.Difficulty = 55;
            mystic.Quality = 1.05;

            CraftAttributeInfo spike = Spike = new CraftAttributeInfo();

            spike.Difficulty = 5;
            spike.Quality = 1.05;

            CraftAttributeInfo fruity = Fruity = new CraftAttributeInfo();

            fruity.DexPenReduction = 1;
            fruity.Difficulty = 10;
            fruity.Quality = 1.05;

            CraftAttributeInfo icerock = IceRock = new CraftAttributeInfo();

            icerock.ArmorColdResist = level1_prot;
            icerock.Difficulty = 20;
            icerock.Quality = 1.05;
            
            CraftAttributeInfo silverrock = SilverRock = new CraftAttributeInfo();

            silverrock.ArmorNecroResist = level1_prot;
            silverrock.Difficulty = 40;
            silverrock.Quality = 1.1;
            
            CraftAttributeInfo spectral = Spectral = new CraftAttributeInfo();

            spectral.DexPenReduction = 3;
            spectral.Difficulty = 60;
            spectral.Quality = 1.1;
            
            CraftAttributeInfo undead = Undead = new CraftAttributeInfo();

            undead.ArmorNecroResist = level2_prot;
            undead.Difficulty = 80;
            undead.Quality = 1.2;
            
            CraftAttributeInfo darkpagan = DarkPagan = new CraftAttributeInfo();

            darkpagan.Difficulty = 45;
            darkpagan.Quality = 1.15;
            
            CraftAttributeInfo oldbritain = OldBritain = new CraftAttributeInfo();

            oldbritain.Difficulty = 65;
            oldbritain.Quality = 1.25;

            CraftAttributeInfo virginity = Virginity = new CraftAttributeInfo();

            virginity.DexPenReduction = 1;
            virginity.ArmorNecroResist = level2_prot;
            virginity.Difficulty = 90;
            virginity.Quality = 1.25;
            
            CraftAttributeInfo blackdwarf = BlackDwarf = new CraftAttributeInfo();

            blackdwarf.Difficulty = 25;
            blackdwarf.Quality = 1.1;
            
            CraftAttributeInfo redelven = RedElven = new CraftAttributeInfo();

            //JustZH : reduce spell damage to wearer
            redelven.DexPenReduction = 2;
            redelven.Difficulty = 75;
            redelven.Quality = 1.15;

            CraftAttributeInfo dripstone = DripStone = new CraftAttributeInfo();

            //JustZH : reduce spell damage to wearer
            dripstone.ArmorColdResist = level1_prot;
            dripstone.Difficulty = 100;
            dripstone.Quality = 1.6;

            CraftAttributeInfo executor = Executor = new CraftAttributeInfo();

            //JustZH : reduce spell damage to wearer
            executor.Difficulty = 104;
            executor.Quality = 1.6;
            
            CraftAttributeInfo peachblue = Peachblue = new CraftAttributeInfo();

            peachblue.DexPenReduction = 2;
            peachblue.Difficulty = 108;
            peachblue.Quality = 1.7;
            
            CraftAttributeInfo destruction = Destruction = new CraftAttributeInfo();

            destruction.DexPenReduction = 1;
            destruction.ArmorEarthResist = level1_prot;
            destruction.Difficulty = 112;
            destruction.Quality = 1.7;

            CraftAttributeInfo anra = Anra = new CraftAttributeInfo();

            anra.DexPenReduction = 2;
            anra.Difficulty = 116;
            anra.Quality = 1.8;

            CraftAttributeInfo goddess = GoddessMetal = new CraftAttributeInfo();

            //JustZH : reduce spell damage to wearer
            goddess.ArmorEnergyResist = level1_prot;
            goddess.Difficulty = 125;
            goddess.Quality = 1.9;

            CraftAttributeInfo crystal = CrystalMetal = new CraftAttributeInfo();

            //JustZH : reduce spell damage to wearer
            crystal.ArmorEarthResist = level1_prot;
            crystal.Difficulty = 119;
            crystal.Quality = 1.8;

            CraftAttributeInfo doom = DoomMetal = new CraftAttributeInfo();

            doom.Difficulty = 122;
            doom.Quality = 1.9;

            CraftAttributeInfo ets = ETS = new CraftAttributeInfo();

            //JustZH : reduce spell damage to wearer
            //JustZH : blackrock to circle 2
            ets.DexPenReduction = 3;
            ets.ArmorColdResist = level3_prot;
            ets.Difficulty = 130;
            ets.Quality = 2.2;

            CraftAttributeInfo dsr = DSR = new CraftAttributeInfo();

            //JustZH : reduce damage to wearer
            //JustZH : blackrock to circle 2
            dsr.DexPenReduction = 3;
            dsr.ArmorFireResist = level3_prot;
            dsr.Difficulty = 130;
            dsr.Quality = 2.2;

            CraftAttributeInfo rnd = RND = new CraftAttributeInfo();

            //JustZH : blackrock to circle 4
            rnd.DexPenReduction = 3;
            rnd.ArmorEnergyResist = level3_prot;
            rnd.ArmorEarthResist = level3_prot;
            rnd.ArmorNecroResist = level3_prot;
            rnd.Difficulty = 140;
            rnd.Quality = 2.25;

            #endregion

            #region OldHides
            CraftAttributeInfo spined = Spined = new CraftAttributeInfo();

            spined.ArmorPhysicalResist = 5;
            spined.ArmorLuck = 40;
            spined.RunicMinAttributes = 1;
            spined.RunicMaxAttributes = 3;
            if (Core.ML)
            {
                spined.RunicMinIntensity = 40;
                spined.RunicMaxIntensity = 100;
            }
            else
            {
                spined.RunicMinIntensity = 20;
                spined.RunicMaxIntensity = 40;
            }

            CraftAttributeInfo horned = Horned = new CraftAttributeInfo();

            horned.ArmorPhysicalResist = 2;
            horned.ArmorFireResist = 3;
            horned.ArmorColdResist = 2;
            horned.ArmorPoisonResist = 2;
            horned.ArmorEnergyResist = 2;
            horned.RunicMinAttributes = 3;
            horned.RunicMaxAttributes = 4;
            if (Core.ML)
            {
                horned.RunicMinIntensity = 45;
                horned.RunicMaxIntensity = 100;
            }
            else
            {
                horned.RunicMinIntensity = 30;
                horned.RunicMaxIntensity = 70;
            }

            CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();

            barbed.ArmorPhysicalResist = 2;
            barbed.ArmorFireResist = 1;
            barbed.ArmorColdResist = 2;
            barbed.ArmorPoisonResist = 3;
            barbed.ArmorEnergyResist = 4;
            barbed.RunicMinAttributes = 4;
            barbed.RunicMaxAttributes = 5;
            if (Core.ML)
            {
                barbed.RunicMinIntensity = 50;
                barbed.RunicMaxIntensity = 100;
            }
            else
            {
                barbed.RunicMinIntensity = 40;
                barbed.RunicMaxIntensity = 100;
            }
            #endregion


            #region JustZHLogs
            //public static readonly CraftAttributeInfo Rat, Wolf, Bear, Orc, Serpent, Lizard, Troll, Ostard, Necromancer, Terathan, 
            //Lava, Liche, Daemon, IceCrystal, Wyrm, Balron, GoldenDragon, SilverDragon, Balrog, Angel;


            CraftAttributeInfo currentHide = Rat = new CraftAttributeInfo();
            currentHide.Difficulty = 10;
            currentHide.Quality = 1.1;

            currentHide = Wolf = new CraftAttributeInfo();
            currentHide.Difficulty = 20;
            currentHide.Quality = 1.2;

            currentHide = Bear = new CraftAttributeInfo();
            currentHide.Difficulty = 30;
            currentHide.Quality = 1.3;

            currentHide = Orc = new CraftAttributeInfo();
            currentHide.Difficulty = 30; // fill in, can't find in pol scripts
            currentHide.Quality = 1.3;

            currentHide = Serpent = new CraftAttributeInfo();
            currentHide.Difficulty = 40;
            currentHide.Quality = 1.4;

            currentHide = Lizard = new CraftAttributeInfo();
            currentHide.Difficulty = 50;
            currentHide.Quality = 1.5;
            
            currentHide = Ostard = new CraftAttributeInfo();
            currentHide.Difficulty = 70;
            currentHide.Quality = 1.7;

            currentHide = Necromancer = new CraftAttributeInfo();
            currentHide.Difficulty = 80;
            currentHide.Quality = 1.8;
            currentHide.ArmorNecroResist = level1_prot;
            //currentHide.armormagicpenaltyreduction 1 //JustZH for later

            currentHide = Terathan = new CraftAttributeInfo();
            currentHide.Difficulty = 65;
            currentHide.Quality = 1.6;

            currentHide = Lava = new CraftAttributeInfo();
            currentHide.Difficulty = 90;
            currentHide.Quality = 1.9;
            currentHide.ArmorFireResist = level2_prot;

            currentHide = Liche = new CraftAttributeInfo();
            currentHide.Difficulty = 100;
            currentHide.Quality = 2.0;
            currentHide.ArmorNecroResist = level1_prot;
            //currentHide.armormagicpenaltyreduction 2 //JustZH for later

            currentHide = Daemon = new CraftAttributeInfo();
            currentHide.Difficulty = 70; // set later, not in script
            currentHide.Quality = 1.7;
            currentHide.ArmorPhysicalResist = level2_prot;

            currentHide = IceCrystal = new CraftAttributeInfo();
            currentHide.Difficulty = 110;
            currentHide.Quality = 2.2;
            currentHide.ArmorColdResist = level2_prot;

            currentHide = Wyrm = new CraftAttributeInfo();
            currentHide.Difficulty = 120;
            currentHide.Quality = 2.5;
            currentHide.ArmorFireResist = level2_prot;
            currentHide.ArmorEarthResist = level1_prot;
            //currentHide.armormagicpenaltyreduction 1 //JustZH for later

            currentHide = Balron = new CraftAttributeInfo();
            currentHide.Difficulty = 125;
            currentHide.Quality = 2.9;
            currentHide.ArmorNecroResist = level1_prot;
            //currentHide.armormagicpenaltyreduction 2 //JustZH for later
            currentHide.ArmorMagicResist = 1;
            //level 1 blackrock prot

            currentHide = GoldenDragon = new CraftAttributeInfo();
            currentHide.Difficulty = 130;
            currentHide.Quality = 3.0;
            currentHide.ArmorFireResist = level3_prot;
            currentHide.ArmorEnergyResist = level1_prot;
            currentHide.ArmorEarthResist = level1_prot;
            //currentHide.armormagicpenaltyreduction 1 //JustZH for later

            currentHide = SilverDragon = new CraftAttributeInfo();
            currentHide.Difficulty = 140;
            currentHide.Quality = 3.5;
            currentHide.ArmorEnergyResist = level2_prot;
            currentHide.ArmorColdResist = level2_prot;
            //currentHide.armormagicpenaltyreduction 2 //JustZH for later

            currentHide = Balrog = new CraftAttributeInfo();
            currentHide.Difficulty = 135;
            currentHide.Quality = 3.1;
            currentHide.ArmorEnergyResist = level2_prot;
            currentHide.ArmorNecroResist = level2_prot;
            currentHide.ArmorEarthResist = level2_prot;
            //currentHide.armormagicpenaltyreduction 2 //JustZH for later
            currentHide.ArmorMagicResist = 2;
            //level 2 blackrock prot

            currentHide = Angel = new CraftAttributeInfo();
            // not in script, adding our own values, same as Balrog but with holy instead of necro
            currentHide.Difficulty = 135;
            currentHide.Quality = 3.1;
            currentHide.ArmorEnergyResist = level2_prot;
            currentHide.ArmorHolyResist = level3_prot;
            currentHide.ArmorEarthResist = level2_prot;

            currentHide.ArmorMagicResist = 2; //JustZH for later
            //level 2 blackrock prot

            #endregion

            #region OldScales
            CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

            red.ArmorFireResist = 10;
            red.ArmorColdResist = -3;

            CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

            yellow.ArmorPhysicalResist = -3;
            yellow.ArmorLuck = 20;

            CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

            black.ArmorPhysicalResist = 10;
            black.ArmorEnergyResist = -3;

            CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

            green.ArmorFireResist = -3;
            green.ArmorPoisonResist = 10;

            CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

            white.ArmorPhysicalResist = -3;
            white.ArmorColdResist = 10;

            CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

            blue.ArmorPoisonResist = -3;
            blue.ArmorEnergyResist = 10;

            #endregion



            #region Mondain's Legacy OldLogs
            //public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood;
            CraftAttributeInfo oak = OakWood = new CraftAttributeInfo();

            oak.ArmorPhysicalResist = 3;
            oak.ArmorFireResist = 3;
            oak.ArmorPoisonResist = 2;
            oak.ArmorEnergyResist = 3;
            oak.ArmorLuck = 40;
            oak.ShieldPhysicalResist = 1;
            oak.ShieldFireResist = 1;
            oak.ShieldColdResist = 1;
            oak.ShieldPoisonResist = 1;
            oak.ShieldEnergyResist = 1;
            oak.WeaponLuck = 40;
            oak.WeaponDamage = 5;
            oak.RunicMinAttributes = 1;
            oak.RunicMaxAttributes = 2;
            oak.RunicMinIntensity = 1;
            oak.RunicMaxIntensity = 50;

            CraftAttributeInfo ash = AshWood = new CraftAttributeInfo();

            ash.ArmorPhysicalResist = 4;
            ash.ArmorFireResist = 2;
            ash.ArmorColdResist = 4;
            ash.ArmorPoisonResist = 1;
            ash.ArmorEnergyResist = 6;
            ash.ArmorLowerRequirements = 20;
            ash.ShieldEnergyResist = 3;
            ash.WeaponSwingSpeed = 10;
            ash.WeaponLowerRequirements = 20;
            ash.RunicMinAttributes = 2;
            ash.RunicMaxAttributes = 3;
            ash.RunicMinIntensity = 35;
            ash.RunicMaxIntensity = 75;

            CraftAttributeInfo yew = YewWood = new CraftAttributeInfo();

            yew.ArmorPhysicalResist = 6;
            yew.ArmorFireResist = 3;
            yew.ArmorColdResist = 3;
            yew.ArmorEnergyResist = 3;
            yew.ArmorRegenHits = 1;
            yew.ShieldPhysicalResist = 3;
            yew.WeaponHitChance = 5;
            yew.WeaponDamage = 10;
            yew.RunicMinAttributes = 3;
            yew.RunicMaxAttributes = 3;
            yew.RunicMinIntensity = 40;
            yew.RunicMaxIntensity = 90;

            CraftAttributeInfo heartwood = Heartwood = new CraftAttributeInfo();

            heartwood.ArmorPhysicalResist = 2;
            heartwood.ArmorFireResist = 3;
            heartwood.ArmorColdResist = 2;
            heartwood.ArmorPoisonResist = 7;
            heartwood.ArmorEnergyResist = 2;

            // one of below
            heartwood.ArmorDamage = 10;
            heartwood.ArmorHitChance = 5;
            heartwood.ArmorLuck = 40;
            heartwood.ArmorLowerRequirements = 20;
            heartwood.ArmorMage = 1;

            // one of below
            heartwood.WeaponDamage = 10;
            heartwood.WeaponHitChance = 5;
            heartwood.WeaponHitLifeLeech = 13;
            heartwood.WeaponLuck = 40;
            heartwood.WeaponLowerRequirements = 20;
            heartwood.WeaponSwingSpeed = 10;

            heartwood.RunicMinAttributes = 4;
            heartwood.RunicMaxAttributes = 4;
            heartwood.RunicMinIntensity = 50;
            heartwood.RunicMaxIntensity = 100;

            CraftAttributeInfo bloodwood = Bloodwood = new CraftAttributeInfo();

            bloodwood.ArmorPhysicalResist = 3;
            bloodwood.ArmorFireResist = 8;
            bloodwood.ArmorColdResist = 1;
            bloodwood.ArmorPoisonResist = 3;
            bloodwood.ArmorEnergyResist = 3;
            bloodwood.ArmorRegenHits = 2;
            bloodwood.ShieldFireResist = 3;
            bloodwood.WeaponRegenHits = 2;
            bloodwood.WeaponHitLifeLeech = 16;

            CraftAttributeInfo frostwood = Frostwood = new CraftAttributeInfo();

            frostwood.ArmorPhysicalResist = 2;
            frostwood.ArmorFireResist = 1;
            frostwood.ArmorColdResist = 8;
            frostwood.ArmorPoisonResist = 3;
            frostwood.ArmorEnergyResist = 4;
            frostwood.ShieldColdResist = 3;
            frostwood.WeaponColdDamage = 40;
            frostwood.WeaponDamage = 12;
            #endregion

            #region JustZHLogs
            //public static readonly CraftAttributeInfo Pinetree, Cherry, Oak, PurplePassion, GoldenReflection, Hardranger, Jade, Darkwood, Stonewood, Sun, Gauntlet, Swamp,
            // Stardust, SilverLeaf, Stormteal, Emerald, Blood, CrystalLog, Bloodhorse, DoomLog, GoddessLog, ZuluLog, Darkness, Elven;

            CraftAttributeInfo currentLog = Pinetree = new CraftAttributeInfo();
            currentLog.Difficulty = 15;
            currentLog.Quality = 1.05;

            currentLog = Cherry = new CraftAttributeInfo();
            currentLog.Difficulty = 28;
            currentLog.Quality = 1.1;

            currentLog = Oak = new CraftAttributeInfo();
            currentLog.Difficulty = 39;
            currentLog.Quality = 1.15;

            currentLog = PurplePassion = new CraftAttributeInfo();
            currentLog.Difficulty = 50;
            currentLog.Quality = 1.2;

            currentLog = GoldenReflection = new CraftAttributeInfo();
            currentLog.Difficulty = 59;
            currentLog.Quality = 1.25;

            currentLog = Hardranger = new CraftAttributeInfo();
            currentLog.Difficulty = 65;
            currentLog.Quality = 1.25;

            currentLog = Jade = new CraftAttributeInfo();
            currentLog.Difficulty = 68;
            currentLog.Quality = 1.3;

            currentLog = Darkwood = new CraftAttributeInfo();
            currentLog.Difficulty = 77;
            currentLog.Quality = 1.35;

            currentLog = Stonewood = new CraftAttributeInfo();
            currentLog.Difficulty = 84;
            currentLog.Quality = 1.4;

            currentLog = Sun = new CraftAttributeInfo();
            currentLog.Difficulty = 91;
            currentLog.Quality = 1.45;

            currentLog = Gauntlet = new CraftAttributeInfo();
            currentLog.Difficulty = 95;
            currentLog.Quality = 1.45;

            currentLog = Swamp = new CraftAttributeInfo();
            currentLog.Difficulty = 98;
            currentLog.Quality = 1.5;

            currentLog = Stardust = new CraftAttributeInfo();
            currentLog.Difficulty = 105;
            currentLog.Quality = 1.55;

            currentLog = SilverLeaf = new CraftAttributeInfo();
            currentLog.Difficulty = 110;
            currentLog.Quality = 1.6;

            currentLog = Stormteal = new CraftAttributeInfo();
            currentLog.Difficulty = 114;
            currentLog.Quality = 1.65;

            currentLog = Emerald = new CraftAttributeInfo();
            currentLog.Difficulty = 118;
            currentLog.Quality = 1.7;

            currentLog = Blood = new CraftAttributeInfo();
            currentLog.Difficulty = 122;
            currentLog.Quality = 1.75;

            currentLog = CrystalLog = new CraftAttributeInfo();
            currentLog.Difficulty = 125;
            currentLog.Quality = 1.8;

            currentLog = Bloodhorse = new CraftAttributeInfo();
            currentLog.Difficulty = 127;
            currentLog.Quality = 1.85;

            currentLog = DoomLog = new CraftAttributeInfo();
            currentLog.Difficulty = 128;
            currentLog.Quality = 1.9;

            currentLog = GoddessLog = new CraftAttributeInfo();
            currentLog.Difficulty = 130;
            currentLog.Quality = 1.95;

            currentLog = ZuluLog = new CraftAttributeInfo();
            currentLog.Difficulty = 132;
            currentLog.Quality = 2;

            currentLog = Darkness = new CraftAttributeInfo();
            currentLog.Difficulty = 140;
            currentLog.Quality = 2.05;

            currentLog = Elven = new CraftAttributeInfo();
            currentLog.Difficulty = 145;
            currentLog.Quality = 2.1;
            #endregion
        }
    }

    public class CraftResourceInfo
    {
        private readonly int m_Hue;
        private readonly int m_Number;
        private readonly string m_Name;
        private readonly CraftAttributeInfo m_AttributeInfo;
        private readonly CraftResource m_Resource;
        private readonly Type[] m_ResourceTypes;

        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
        }
        public int Number
        {
            get
            {
                return this.m_Number;
            }
        }
        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }
        public CraftAttributeInfo AttributeInfo
        {
            get
            {
                return this.m_AttributeInfo;
            }
        }
        public CraftResource Resource
        {
            get
            {
                return this.m_Resource;
            }
        }
        public Type[] ResourceTypes
        {
            get
            {
                return this.m_ResourceTypes;
            }
        }

        public CraftResourceInfo(int hue, int number, string name, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes)
        {
            this.m_Hue = hue;
            this.m_Number = number;
            this.m_Name = name;
            this.m_AttributeInfo = attributeInfo;
            this.m_Resource = resource;
            this.m_ResourceTypes = resourceTypes;

            for (int i = 0; i < resourceTypes.Length; ++i)
                CraftResources.RegisterType(resourceTypes[i], resource);
        }
    }

    public class CraftResources
    {
        private static readonly CraftResourceInfo[] m_MetalInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x000, 1053109, "Iron", CraftAttributeInfo.Blank, CraftResource.Iron, typeof(IronIngot), typeof(IronOre), typeof(Granite)),
            new CraftResourceInfo(0x3ea, 1053108, "Dull Copper",	CraftAttributeInfo.DullCopper,	CraftResource.DullCopper, typeof(DullCopperIngot),	typeof(DullCopperOre),	typeof(DullCopperGranite)),
            new CraftResourceInfo(0x966, 1053107, "Shadow Iron",	CraftAttributeInfo.ShadowIron,	CraftResource.ShadowIron, typeof(ShadowIronIngot),	typeof(ShadowIronOre),	typeof(ShadowIronGranite)),  // remove later
            new CraftResourceInfo(0x602, 1053106, "Copper", CraftAttributeInfo.Copper, CraftResource.Copper, typeof(CopperIngot), typeof(CopperOre), typeof(CopperGranite)),
            new CraftResourceInfo(0x45e, 1053105, "Bronze", CraftAttributeInfo.Bronze, CraftResource.Bronze, typeof(BronzeIngot), typeof(BronzeOre), typeof(BronzeGranite)), 
            new CraftResourceInfo(0x8A5, 1053104, "Gold", CraftAttributeInfo.Golden, CraftResource.Gold, typeof(GoldIngot), typeof(GoldOre), typeof(GoldGranite)),  // remove later
            new CraftResourceInfo(0x979, 1053103, "Agapite", CraftAttributeInfo.Agapite, CraftResource.Agapite, typeof(AgapiteIngot), typeof(AgapiteOre), typeof(AgapiteGranite)),  // remove later
            new CraftResourceInfo(0x89F, 1053102, "Verite", CraftAttributeInfo.Verite, CraftResource.Verite, typeof(VeriteIngot), typeof(VeriteOre), typeof(VeriteGranite)),  // remove later
            new CraftResourceInfo(0x8AB, 1053101, "Valorite", CraftAttributeInfo.Valorite,	CraftResource.Valorite, typeof(ValoriteIngot),	typeof(ValoriteOre), typeof(ValoriteGranite)), // remove later
            new CraftResourceInfo(1160, 1063269, "New Zulu", CraftAttributeInfo.ZuluMetal,	CraftResource.ZuluMetal, typeof(ZuluIngot),	typeof(ZuluOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x455, 1098656, "Onyx", CraftAttributeInfo.Onyx,	CraftResource.Onyx, typeof(OnyxIngot),	typeof(OnyxOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x6b8, 1098657, "Pyrite", CraftAttributeInfo.Pyrite,	CraftResource.Pyrite, typeof(PyriteIngot),	typeof(PyriteOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1159, 1098658, "Malachite", CraftAttributeInfo.Malachite,	CraftResource.Malachite, typeof(MalachiteIngot),	typeof(MalachiteOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x4df, 1098659, "Azurite", CraftAttributeInfo.Azurite,	CraftResource.Azurite, typeof(AzuriteIngot),	typeof(AzuriteOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1111, 1098660, "Platinum", CraftAttributeInfo.Platinum,	CraftResource.Platinum, typeof(PlatinumIngot),	typeof(PlatinumOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1158, 1098661, "Lavarock", CraftAttributeInfo.Lavarock,	CraftResource.Lavarock, typeof(LavarockIngot),	typeof(LavarockOre), typeof(ZuluGranite)),
            new CraftResourceInfo(383, 1098662, "Mystic", CraftAttributeInfo.Mystic,	CraftResource.Mystic, typeof(MysticIngot),	typeof(MysticOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1223, 1098663, "Spike", CraftAttributeInfo.Spike,	CraftResource.Spike, typeof(SpikeIngot),	typeof(SpikeOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1134, 1098664, "Fruity", CraftAttributeInfo.Fruity,	CraftResource.Fruity, typeof(FruityIngot),	typeof(FruityOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1152, 1098665, "Icerock", CraftAttributeInfo.IceRock,	CraftResource.IceRock, typeof(IceRockIngot),	typeof(IceRockOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1001, 1098666, "Silver Rock", CraftAttributeInfo.SilverRock,	CraftResource.SilverRock, typeof(SilverRockIngot),	typeof(SilverRockOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1155, 1098667, "Spectral", CraftAttributeInfo.Spectral,	CraftResource.Spectral, typeof(SpectralIngot),	typeof(SpectralOre), typeof(ZuluGranite)),
            new CraftResourceInfo(633, 1098668, "Undead", CraftAttributeInfo.Undead,	CraftResource.Undead, typeof(UndeadIngot),	typeof(UndeadOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1131, 1098669, "Dark Pagan", CraftAttributeInfo.DarkPagan,	CraftResource.DarkPagan, typeof(DarkPaganIngot),	typeof(DarkPaganOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x852, 1098670, "Old Britain", CraftAttributeInfo.OldBritain,	CraftResource.OldBritain, typeof(OldBritainIngot),	typeof(OldBritainOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1154, 1098671, "Virginity", CraftAttributeInfo.Virginity,	CraftResource.Virginity, typeof(VirginityIngot),	typeof(VirginityOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1105, 1098672, "Black Dwarf", CraftAttributeInfo.BlackDwarf,	CraftResource.BlackDwarf, typeof(BlackDwarfIngot),	typeof(BlackDwarfOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1209, 1098673, "Red Elven", CraftAttributeInfo.RedElven,	CraftResource.RedElven, typeof(RedElvenIngot),	typeof(RedElvenOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x049e, 1098674, "Dripstone", CraftAttributeInfo.DripStone,	CraftResource.Dripstone, typeof(DripstoneIngot),	typeof(DripstoneOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x0499, 1098675, "Executor", CraftAttributeInfo.Executor,	CraftResource.Executor, typeof(ExecutorIngot),	typeof(ExecutorOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x049c, 1098676, "Peachblue", CraftAttributeInfo.Peachblue,	CraftResource.Peachblue, typeof(PeachblueIngot),	typeof(PeachblueOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x0500, 1098677, "Destruction", CraftAttributeInfo.Destruction,	CraftResource.Destruction, typeof(DestructionIngot),	typeof(DestructionOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x048b, 1098678, "Anra", CraftAttributeInfo.Anra,	CraftResource.Anra, typeof(AnraIngot),	typeof(AnraOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x0501, 1098679, "Goddess", CraftAttributeInfo.GoddessMetal,	CraftResource.GoddessMetal, typeof(GoddessIngot),	typeof(GoddessOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x0492, 1098680, "Crystal", CraftAttributeInfo.CrystalMetal,	CraftResource.CrystalMetal, typeof(CrystalIngot),	typeof(CrystalOre), typeof(ZuluGranite)),
            new CraftResourceInfo(0x049f, 1063281, "Doom", CraftAttributeInfo.DoomMetal,	CraftResource.DoomMetal, typeof(DoomIngot),	typeof(DoomOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1171, 1063285, "Ebon Twilight Saphire", CraftAttributeInfo.ETS,	CraftResource.ETS, typeof(ETSIngot),	typeof(ETSOre), typeof(ZuluGranite)),
            new CraftResourceInfo(1172, 1063283, "Dark Sable Ruby", CraftAttributeInfo.DSR,	CraftResource.DSR, typeof(DSRIngot),	typeof(DSROre), typeof(ZuluGranite)),
            new CraftResourceInfo(1176, 1063284, "Radiant Nimbus Diamond", CraftAttributeInfo.RND,	CraftResource.RND, typeof(RNDIngot),	typeof(RNDOre), typeof(ZuluGranite)),
        };

        private static readonly CraftResourceInfo[] m_ScaleInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x66D, 1053129, "Red Scales",	CraftAttributeInfo.RedScales, CraftResource.RedScales, typeof(RedScales)),
            new CraftResourceInfo(0x8A8, 1053130, "Yellow Scales",	CraftAttributeInfo.YellowScales,	CraftResource.YellowScales, typeof(YellowScales)),
            new CraftResourceInfo(0x455, 1053131, "Black Scales",	CraftAttributeInfo.BlackScales, CraftResource.BlackScales, typeof(BlackScales)),
            new CraftResourceInfo(0x851, 1053132, "Green Scales",	CraftAttributeInfo.GreenScales, CraftResource.GreenScales, typeof(GreenScales)),
            new CraftResourceInfo(0x8FD, 1053133, "White Scales",	CraftAttributeInfo.WhiteScales, CraftResource.WhiteScales, typeof(WhiteScales)),
            new CraftResourceInfo(0x8B0, 1053134, "Blue Scales",	CraftAttributeInfo.BlueScales, CraftResource.BlueScales, typeof(BlueScales))
        };

        private static readonly CraftResourceInfo[] m_LeatherInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x000, 1049353, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularLeather,	typeof(Leather), typeof(Hides)),
            new CraftResourceInfo(0x283, 1049354, "Spined", CraftAttributeInfo.Spined, CraftResource.SpinedLeather,	typeof(SpinedLeather),	typeof(SpinedHides)),
            new CraftResourceInfo(0x227, 1049355, "Horned", CraftAttributeInfo.Horned, CraftResource.HornedLeather,	typeof(HornedLeather),	typeof(HornedHides)),
            new CraftResourceInfo(0x1C1, 1049356, "Barbed", CraftAttributeInfo.Barbed, CraftResource.BarbedLeather,	typeof(BarbedLeather),	typeof(BarbedHides)),
            new CraftResourceInfo(0x0494, 1063505, "Dragon", CraftAttributeInfo.Dragon, CraftResource.DragonLeather,	typeof(DragonLeather),	typeof(DragonHides)),
            new CraftResourceInfo(0x07e2, 1063505, "Rat", CraftAttributeInfo.Rat, CraftResource.RatLeather,    typeof(RatLeather),  typeof(RatHides)),
            new CraftResourceInfo(1102, 1063505, "Wolf", CraftAttributeInfo.Wolf, CraftResource.WolfLeather,    typeof(WolfLeather),  typeof(WolfHides)),
            new CraftResourceInfo(44, 1063505, "Bear", CraftAttributeInfo.Bear, CraftResource.BearLeather,    typeof(BearLeather),  typeof(BearHides)),
            new CraftResourceInfo(1054, 1063505, "Orc", CraftAttributeInfo.Orc, CraftResource.OrcLeather,    typeof(OrcLeather),  typeof(OrcHides)),
            new CraftResourceInfo(0x08fd, 1063505, "Serpent", CraftAttributeInfo.Serpent, CraftResource.SerpentLeather,	typeof(SerpentLeather),	typeof(SerpentHides)),
            new CraftResourceInfo(0x0852, 1063505, "Lizard", CraftAttributeInfo.Lizard, CraftResource.LizardLeather,    typeof(LizardLeather),  typeof(LizardHides)),
            new CraftResourceInfo(0x054a, 1063505, "Troll", CraftAttributeInfo.Troll, CraftResource.TrollLeather,    typeof(TrollLeather),  typeof(TrollHides)),
            new CraftResourceInfo(0x415, 1063505, "Ostard", CraftAttributeInfo.Ostard, CraftResource.OstardLeather,    typeof(OstardLeather),  typeof(OstardHides)),
            new CraftResourceInfo(84, 1063505, "Necromancer", CraftAttributeInfo.Necromancer, CraftResource.NecromancerLeather,    typeof(NecromancerLeather),  typeof(NecromancerHides)),
            new CraftResourceInfo(1285, 1063505, "Terathan", CraftAttributeInfo.Terathan, CraftResource.TerathanLeather,    typeof(TerathanLeather),  typeof(TerathanHides)),
            new CraftResourceInfo(0x486, 1063505, "Lava", CraftAttributeInfo.Lava, CraftResource.LavaLeather,    typeof(LavaLeather),  typeof(LavaHides)),
            new CraftResourceInfo(0x0496, 1063505, "Liche", CraftAttributeInfo.Liche, CraftResource.LicheLeather,    typeof(LicheLeather),  typeof(LicheHides)),
            new CraftResourceInfo(1645, 1063505, "Daemon", CraftAttributeInfo.Daemon, CraftResource.DaemonLeather,    typeof(DaemonLeather),  typeof(DaemonHides)),
            new CraftResourceInfo(0x0492, 1063505, "Ice Crystal", CraftAttributeInfo.IceCrystal, CraftResource.IceCrystalLeather,    typeof(IceCrystalLeather),  typeof(IceCrystalHides)),
            new CraftResourceInfo(1159, 1063505, "Wyrm", CraftAttributeInfo.Wyrm, CraftResource.WyrmLeather,    typeof(WyrmLeather),  typeof(WyrmHides)),
            new CraftResourceInfo(1157, 1063505, "Balron", CraftAttributeInfo.Balron, CraftResource.BalronLeather,    typeof(BalronLeather),  typeof(BalronHides)),
            new CraftResourceInfo(48, 1063505, "Golden Dragon", CraftAttributeInfo.GoldenDragon, CraftResource.GoldenDragonLeather,    typeof(GoldenDragonLeather),  typeof(GoldenDragonHides)),
            new CraftResourceInfo(1154, 1063505, "Silver Dragon", CraftAttributeInfo.SilverDragon, CraftResource.SilverDragonLeather,    typeof(SilverDragonLeather),  typeof(SilverDragonHides)),
            new CraftResourceInfo(1160, 1063505, "Balrog", CraftAttributeInfo.Balrog, CraftResource.BalrogLeather,    typeof(BalrogLeather),  typeof(BalrogHides)),
            new CraftResourceInfo(1165, 1063505, "Angel", CraftAttributeInfo.Angel, CraftResource.AngelLeather,    typeof(AngelLeather),  typeof(AngelHides))

        };

        // JustZH: we don't need this
        //private static readonly CraftResourceInfo[] m_AOSLeatherInfo = new CraftResourceInfo[]
        //{
        //    new CraftResourceInfo(0x000, 1049353, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularLeather,	typeof(Leather), typeof(Hides)),
        //    new CraftResourceInfo(0x8AC, 1049354, "Spined", CraftAttributeInfo.Spined, CraftResource.SpinedLeather,	typeof(SpinedLeather),	typeof(SpinedHides)),
        //    new CraftResourceInfo(0x845, 1049355, "Horned", CraftAttributeInfo.Horned, CraftResource.HornedLeather,	typeof(HornedLeather),	typeof(HornedHides)),
        //    new CraftResourceInfo(0x851, 1049356, "Barbed", CraftAttributeInfo.Barbed, CraftResource.BarbedLeather,	typeof(BarbedLeather),	typeof(BarbedHides)),

        //};

        private static readonly CraftResourceInfo[] m_WoodInfo = new CraftResourceInfo[]
        {

            new CraftResourceInfo(0x000, 1011542, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularWood,	typeof(Log), typeof(Board)),
            new CraftResourceInfo(0x7DA, 1072533, "Oak", CraftAttributeInfo.OakWood, CraftResource.OakWood, typeof(OakLog), typeof(OakBoard)),
            new CraftResourceInfo(0x4A7, 1072534, "Ash", CraftAttributeInfo.AshWood, CraftResource.AshWood, typeof(AshLog), typeof(AshBoard)),
            new CraftResourceInfo(0x4A8, 1072535, "Yew", CraftAttributeInfo.YewWood, CraftResource.YewWood, typeof(YewLog), typeof(YewBoard)),
            new CraftResourceInfo(0x4A9, 1072536, "Heartwood", CraftAttributeInfo.Heartwood,    CraftResource.Heartwood,    typeof(HeartwoodLog),   typeof(HeartwoodBoard)),
            new CraftResourceInfo(0x4AA, 1072538, "Bloodwood", CraftAttributeInfo.Bloodwood,    CraftResource.Bloodwood,    typeof(BloodwoodLog),   typeof(BloodwoodBoard)),
            new CraftResourceInfo(0x47F, 1072539, "Frostwood", CraftAttributeInfo.Frostwood,    CraftResource.Frostwood,    typeof(FrostwoodLog),   typeof(FrostwoodBoard)),
            new CraftResourceInfo(1132, 1072533, "Pinetree", CraftAttributeInfo.Pinetree, CraftResource.Pinetree, typeof(PinetreeLog), typeof(Board)),
            new CraftResourceInfo(1136, 1072533, "Cherry", CraftAttributeInfo.Cherry, CraftResource.Cherry, typeof(CherryLog), typeof(Board)),
            new CraftResourceInfo(1045, 1072533, "Oak", CraftAttributeInfo.Oak, CraftResource.Oak, typeof(OakLog), typeof(Board)),
            new CraftResourceInfo(515, 1072533, "Purple Passion", CraftAttributeInfo.PurplePassion, CraftResource.PurplePassion, typeof(PurplePassionLog), typeof(Board)),
            new CraftResourceInfo(48, 1072533, "Golden Reflection", CraftAttributeInfo.GoldenReflection, CraftResource.GoldenReflection, typeof(GoldenReflectionLog), typeof(Board)),
            new CraftResourceInfo(1285, 1072533, "Hardranger", CraftAttributeInfo.Hardranger, CraftResource.Hardranger, typeof(HardrangerLog), typeof(Board)),
            new CraftResourceInfo(1162, 1072533, "Jade", CraftAttributeInfo.Jade, CraftResource.Jade, typeof(JadeLog), typeof(Board)),
            new CraftResourceInfo(1109, 1072533, "Darkwood", CraftAttributeInfo.Darkwood, CraftResource.Darkwood, typeof(DarkwoodLog), typeof(Board)),
            new CraftResourceInfo(1154, 1072533, "Stonewood", CraftAttributeInfo.Stonewood, CraftResource.Stonewood, typeof(StonewoodLog), typeof(Board)),
            new CraftResourceInfo(1176, 1072533, "Sun", CraftAttributeInfo.Sun, CraftResource.Sun, typeof(SunLog), typeof(Board)),
            new CraftResourceInfo(1284, 1072533, "Gauntlet", CraftAttributeInfo.Gauntlet, CraftResource.Gauntlet, typeof(GauntletLog), typeof(Board)),
            new CraftResourceInfo(1177, 1072533, "Swamp", CraftAttributeInfo.Swamp, CraftResource.Swamp, typeof(SwampLog), typeof(Board)),
            new CraftResourceInfo(1161, 1072533, "Stardust", CraftAttributeInfo.Stardust, CraftResource.Stardust, typeof(StardustLog), typeof(Board)),
            new CraftResourceInfo(2301, 1072533, "Silver Leaf", CraftAttributeInfo.SilverLeaf, CraftResource.SilverLeaf, typeof(SilverLeafLog), typeof(Board)),
            new CraftResourceInfo(1346, 1072533, "Stormteal", CraftAttributeInfo.Stormteal, CraftResource.Stormteal, typeof(StormtealLog), typeof(Board)),
            new CraftResourceInfo(1159, 1072533, "Emerald", CraftAttributeInfo.Emerald, CraftResource.Emerald, typeof(EmeraldLog), typeof(Board)),
            new CraftResourceInfo(1645, 1072533, "Blood", CraftAttributeInfo.Blood, CraftResource.Blood, typeof(BloodLog), typeof(Board)),
            new CraftResourceInfo(1170, 1072533, "Crystal", CraftAttributeInfo.CrystalLog, CraftResource.CrystalLog, typeof(CrystalLog), typeof(Board)),
            new CraftResourceInfo(1287, 1072533, "Bloodhorse", CraftAttributeInfo.Bloodhorse, CraftResource.Bloodhorse, typeof(BloodhorseLog), typeof(Board)),
            new CraftResourceInfo(1183, 1072533, "Doom", CraftAttributeInfo.DoomLog, CraftResource.DoomLog, typeof(DoomLog), typeof(Board)),
            new CraftResourceInfo(0x0501, 1072533, "Goddess", CraftAttributeInfo.GoddessLog, CraftResource.GoddessLog, typeof(GoddessLog), typeof(Board)),
            new CraftResourceInfo(1160, 1072533, "Zulu", CraftAttributeInfo.ZuluLog, CraftResource.ZuluLog, typeof(ZuluLog), typeof(Board)),
            new CraftResourceInfo(1157, 1072533, "Darkness", CraftAttributeInfo.Darkness, CraftResource.Darkness, typeof(DarknessLog), typeof(Board)),
            new CraftResourceInfo(1165, 1072533, "Elven", CraftAttributeInfo.Elven, CraftResource.Elven, typeof(ElvenLog), typeof(Board))
        };

        /// <summary>
        /// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or RegularWood. False if otherwise.
        /// </summary>
        public static bool IsStandard(CraftResource resource)
        {
            return (resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather || resource == CraftResource.RegularWood);
        }

        private static Hashtable m_TypeTable;

        /// <summary>
        /// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
        /// </summary>
        public static void RegisterType(Type resourceType, CraftResource resource)
        {
            if (m_TypeTable == null)
                m_TypeTable = new Hashtable();

            m_TypeTable[resourceType] = resource;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
        /// </summary>
        public static CraftResource GetFromType(Type resourceType)
        {
            if (m_TypeTable == null)
                return CraftResource.None;

            object obj = m_TypeTable[resourceType];

            if (!(obj is CraftResource))
                return CraftResource.None;

            return (CraftResource)obj;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
        /// </summary>
        public static CraftResourceInfo GetInfo(CraftResource resource)
        {
            CraftResourceInfo[] list = null;

            switch (GetType(resource))
            {
                case CraftResourceType.Metal:
                    list = m_MetalInfo;
                    break;
                case CraftResourceType.Leather:
                    list = /*Core.AOS ? m_AOSLeatherInfo :*/ m_LeatherInfo; // JustZH: we only want to keep one list..
                    break;
                case CraftResourceType.Scales:
                    list = m_ScaleInfo;
                    break;
                case CraftResourceType.Wood:
                    list = m_WoodInfo;
                    break;
            }

            if (list != null)
            {
                int index = GetIndex(resource);

                if (index >= 0 && index < list.Length)
                    return list[index];
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
        /// </summary>
        public static CraftResourceType GetType(CraftResource resource)
        {
            if (resource >= CraftResource.Iron && resource <= CraftResource.RND)
                return CraftResourceType.Metal;

            if (resource >= CraftResource.RegularLeather && resource <= CraftResource.AngelLeather)
                return CraftResourceType.Leather;

            if (resource >= CraftResource.RedScales && resource <= CraftResource.BlueScales)
                return CraftResourceType.Scales;

            if (resource >= CraftResource.RegularWood && resource <= CraftResource.Elven)
                return CraftResourceType.Wood;

            return CraftResourceType.None;
        }

        /// <summary>
        /// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
        /// </summary>
        public static CraftResource GetStart(CraftResource resource)
        {
            switch (GetType(resource))
            {
                case CraftResourceType.Metal:
                    return CraftResource.Iron;
                case CraftResourceType.Leather:
                    return CraftResource.RegularLeather;
                case CraftResourceType.Scales:
                    return CraftResource.RedScales;
                case CraftResourceType.Wood:
                    return CraftResource.RegularWood;
            }

            return CraftResource.None;
        }

        /// <summary>
        /// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
        /// </summary>
        public static int GetIndex(CraftResource resource)
        {
            CraftResource start = GetStart(resource);

            if (start == CraftResource.None)
                return 0;

            return (int)(resource - start);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetLocalizationNumber(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Number);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetHue(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Hue);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
        /// </summary>
        public static string GetName(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? String.Empty : info.Name);
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info)
        {
            if (info.Name.IndexOf("Spined") >= 0)
                return CraftResource.SpinedLeather;
            else if (info.Name.IndexOf("Horned") >= 0)
                return CraftResource.HornedLeather;
            else if (info.Name.IndexOf("Barbed") >= 0)
                return CraftResource.BarbedLeather;
            else if (info.Name.IndexOf("Leather") >= 0)
                return CraftResource.RegularLeather;
            else if (info.Name.IndexOf("Dragon") >= 0)
                return CraftResource.DragonLeather;

            if (info.Level == 0)
                return CraftResource.Iron;
            else if (info.Level == 1)
                return CraftResource.DullCopper;
            else if (info.Level == 2)
                return CraftResource.ShadowIron;
            else if (info.Level == 3)
                return CraftResource.Copper;
            else if (info.Level == 4)
                return CraftResource.Bronze;
            else if (info.Level == 5)
                return CraftResource.Gold;
            else if (info.Level == 6)
                return CraftResource.Agapite;
            else if (info.Level == 7)
                return CraftResource.Verite;
            else if (info.Level == 8)
                return CraftResource.Valorite;
            else if (info.Level == 9)
                return CraftResource.ZuluMetal;
            else if (info.Level == 10)
                return CraftResource.Onyx;
            else if (info.Level == 11)
                return CraftResource.Pyrite;
            else if (info.Level == 12)
                return CraftResource.Malachite;
            else if (info.Level == 13)
                return CraftResource.Azurite;
            else if (info.Level == 14)
                return CraftResource.Platinum;
            else if (info.Level == 15)
                return CraftResource.Lavarock;
            else if (info.Level == 16)
                return CraftResource.Mystic;
            else if (info.Level == 17)
                return CraftResource.Spike;
            else if (info.Level == 18)
                return CraftResource.Fruity;
            else if (info.Level == 19)
                return CraftResource.IceRock;
            else if (info.Level == 20)
                return CraftResource.SilverRock;
            else if (info.Level == 21)
                return CraftResource.Spectral;
            else if (info.Level == 22)
                return CraftResource.Undead;
            else if (info.Level == 23)
                return CraftResource.DarkPagan;
            else if (info.Level == 24)
                return CraftResource.OldBritain;
            else if (info.Level == 25)
                return CraftResource.Virginity;
            else if (info.Level == 26)
                return CraftResource.BlackDwarf;
            else if (info.Level == 27)
                return CraftResource.RedElven;
            else if (info.Level == 28)
                return CraftResource.Dripstone;
            else if (info.Level == 29)
                return CraftResource.Executor;
            else if (info.Level == 30)
                return CraftResource.Peachblue;
            else if (info.Level == 31)
                return CraftResource.Destruction;
            else if (info.Level == 32)
                return CraftResource.Anra;
            else if (info.Level == 33)
                return CraftResource.GoddessMetal;
            else if (info.Level == 34)
                return CraftResource.CrystalMetal;
            else if (info.Level == 35)
                return CraftResource.DoomMetal;
            else if (info.Level == 36)
                return CraftResource.ETS;
            else if (info.Level == 37)
                return CraftResource.DSR;
            else if (info.Level == 38)
                return CraftResource.RND;



            return CraftResource.None;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info, ArmorMaterialType material)
        {
            if (material == ArmorMaterialType.Studded || material == ArmorMaterialType.Leather || material == ArmorMaterialType.Spined ||
                material == ArmorMaterialType.Horned || material == ArmorMaterialType.Barbed || material == ArmorMaterialType.DragonL)
            {
                if (info.Level == 0)
                    return CraftResource.RegularLeather;
                else if (info.Level == 1)
                    return CraftResource.SpinedLeather;
                else if (info.Level == 2)
                    return CraftResource.HornedLeather;
                else if (info.Level == 3)
                    return CraftResource.BarbedLeather;
                else if (info.Level == 4)
                    return CraftResource.DragonLeather;

                return CraftResource.None;
            }

            return GetFromOreInfo(info);
        }
    }

    // NOTE: This class is only for compatability with very old RunUO versions.
    // No changes to it should be required for custom resources.
    public class OreInfo
    {
        public static readonly OreInfo Iron = new OreInfo(0, 0x000, "Iron");
        public static readonly OreInfo DullCopper = new OreInfo(1, 0x3ea, "Dull Copper");
        public static readonly OreInfo ShadowIron = new OreInfo(2, 0x966, "Shadow Iron");
        public static readonly OreInfo Copper = new OreInfo(3, 0x602, "Copper");
        public static readonly OreInfo Bronze = new OreInfo(4, 0x45e, "Bronze");
        public static readonly OreInfo Gold = new OreInfo(5, 0x8A5, "Gold");
        public static readonly OreInfo Agapite = new OreInfo(6, 0x979, "Agapite");
        public static readonly OreInfo Verite = new OreInfo(7, 0x89F, "Verite");
        public static readonly OreInfo Valorite = new OreInfo(8, 0x8AB, "Valorite");
        public static readonly OreInfo Zulu = new OreInfo(9, 1160, "Zulu");
        public static readonly OreInfo Onyx = new OreInfo(10, 0x455, "Onyx");
        public static readonly OreInfo Pyrite = new OreInfo(11, 0x6b8, "Pyrite");
        public static readonly OreInfo Malachite = new OreInfo(12, 1159, "Malachite");
        public static readonly OreInfo Azurite = new OreInfo(13, 0x4df, "Azurite");
        public static readonly OreInfo Platinum = new OreInfo(14, 1111, "Platinum");
        public static readonly OreInfo LavaRock = new OreInfo(15, 1158, "LavaRock");
        public static readonly OreInfo Mystic = new OreInfo(16, 383, "Mystic");
        public static readonly OreInfo Spike = new OreInfo(17, 1223, "Spike");
        public static readonly OreInfo Fruity = new OreInfo(18, 1134, "Fruity");
        public static readonly OreInfo IceRock = new OreInfo(19, 1152, "IceRock");
        public static readonly OreInfo SilverRock = new OreInfo(20, 1001, "SilverRock");
        public static readonly OreInfo Spectral = new OreInfo(21, 1155, "Spectral");
        public static readonly OreInfo Undead = new OreInfo(22, 633, "Undead");
        public static readonly OreInfo DarkPagan = new OreInfo(23, 1131, "DarkPagan");
        public static readonly OreInfo OldBritain = new OreInfo(24, 0x852, "OldBritain");
        public static readonly OreInfo Virginity = new OreInfo(25, 1154, "Virginity");
        public static readonly OreInfo BlackDwarf = new OreInfo(26, 1105, "BlackDwarf");
        public static readonly OreInfo RedElven = new OreInfo(27, 1209, "RedElven");
        public static readonly OreInfo Dripstone = new OreInfo(28, 0x049e, "Dripstone");
        public static readonly OreInfo Executor = new OreInfo(29, 0x0499, "Executor");
        public static readonly OreInfo Peachblue = new OreInfo(30, 0x049c, "Peachblue");
        public static readonly OreInfo Destruction = new OreInfo(31, 0x0500, "Destruction");
        public static readonly OreInfo Anra = new OreInfo(32, 0x048b, "Anra");
        public static readonly OreInfo Goddess = new OreInfo(33, 0x0501, "Goddess");
        public static readonly OreInfo Crystal = new OreInfo(34, 0x0492, "Crystal");
        public static readonly OreInfo Doom = new OreInfo(35, 0x049f, "Doom");
        public static readonly OreInfo ETS = new OreInfo(36, 1171, "Ebon Twilight Sapphire");
        public static readonly OreInfo DSR = new OreInfo(37, 1172, "Dark Sable Ruby");
        public static readonly OreInfo RND = new OreInfo(38, 1176, "Radiant Nimbus Diamond");

        private readonly int m_Level;
        private readonly int m_Hue;
        private readonly string m_Name;

        public OreInfo(int level, int hue, string name)
        {
            this.m_Level = level;
            this.m_Hue = hue;
            this.m_Name = name;
        }

        public int Level
        {
            get
            {
                return this.m_Level;
            }
        }

        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }
    }
}