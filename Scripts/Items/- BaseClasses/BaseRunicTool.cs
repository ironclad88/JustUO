using System;
using System.Collections;

namespace Server.Items
{
    public abstract class BaseRunicTool : BaseTool
    {

        private static bool debug = false;

        private static readonly SkillName[] m_PossibleBonusSkills = new SkillName[]
        {
            SkillName.Swords,
            SkillName.Fencing,
            SkillName.Macing,
            SkillName.Archery,
            SkillName.Wrestling,
            SkillName.Parry,
            SkillName.Tactics,
            SkillName.Anatomy,
            SkillName.Healing,
            SkillName.Magery,
            SkillName.Meditation,
            SkillName.MagicResist,
            SkillName.AnimalTaming,
            SkillName.AnimalLore,
            SkillName.Veterinary,
            SkillName.Musicianship,
            SkillName.Provocation,
            SkillName.Discordance,
            SkillName.Peacemaking,
          //  SkillName.Chivalry,
          //  SkillName.Focus,
          //  SkillName.Necromancy,
            SkillName.Stealing,
            SkillName.Stealth,
            SkillName.SpiritSpeak,
           // SkillName.Bushido,
           // SkillName.Ninjitsu
        };
        private static readonly SkillName[] m_PossibleSpellbookSkills = new SkillName[]
        {
            SkillName.Magery,
            SkillName.Meditation,
            SkillName.EvalInt,
            SkillName.MagicResist
        };
        private static readonly BitArray m_Props = new BitArray(MaxProperties);
        private static readonly int[] m_Possible = new int[MaxProperties];
        private static bool m_IsRunicTool;
        private static int m_LuckChance;
        private const int MaxProperties = 32;
        private CraftResource m_Resource;
        public BaseRunicTool(CraftResource resource, int itemID)
            : base(itemID)
        {
            this.m_Resource = resource;
        }

        public BaseRunicTool(CraftResource resource, int uses, int itemID)
            : base(uses, itemID)
        {
            this.m_Resource = resource;
        }

        public BaseRunicTool(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get
            {
                return this.m_Resource;
            }
            set
            {
                this.m_Resource = value;
                this.Hue = CraftResources.GetHue(this.m_Resource);
                this.InvalidateProperties();
            }
        }
        public static int GetUniqueRandom(int count)
        {
            int avail = 0;

            for (int i = 0; i < count; ++i)
            {
                if (!m_Props[i])
                    m_Possible[avail++] = i;
            }

            if (avail == 0)
                return -1;

            int v = m_Possible[Utility.Random(avail)];

            m_Props.Set(v, true);

            return v;
        }

        public static void ApplyAttributesTo(BaseWeapon weapon, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(weapon, false, 0, attributeCount, min, max);
        }

        public static void ApplyAttributesTo(BaseWeapon weapon, bool isRunicTool, int luckChance, int attributeCount, int min, int max)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;

            if (attributeCount != 0)
            {
                weapon.Unidentified = true;
            }

            if (weapon is Pickaxe)
            {
                AosSkillBonuses skills = weapon.SkillBonuses;
                ApplySkillBonus(skills, min, max, 0, 1, 18);
                return;
            }

            AosAttributes primary = weapon.Attributes;
            AosWeaponAttributes secondary = weapon.WeaponAttributes;
            Console.WriteLine("generating damage for item with attributeCount: {0}, min: {1} max: {2}", attributeCount, min, max);
            int maxDamage = min;
            int minDamage = min;
            for (int i = 0; maxDamage < max; i++)
            {
                maxDamage = min + (i * 5);
                if (maxDamage >= max)
                {
                    maxDamage = max;
                    break;
                }
                if (Utility.Random(5) < 1)
                {
                    break;
                }
            }
            //apply scalar, change this when trying to balance
            double scalar = 2.5;
            minDamage = (int)(minDamage * scalar);
            maxDamage = (int)(maxDamage * scalar);
            ApplyAttribute(primary, min, max, AosAttribute.WeaponDamage, minDamage, maxDamage);
            Console.WriteLine("min + {1} = {0} ({2})", primary[AosAttribute.WeaponDamage], (primary[AosAttribute.WeaponDamage] - min), maxDamage);

            m_Props.SetAll(false);

            m_Props.Set(2, true); //use best skill and mage wep
            m_Props.Set(3, true); //wep damage, handled above
            m_Props.Set(7, true); //luck
            m_Props.Set(15, true); //stam leech
            m_Props.Set(16, true); //stat req
            m_Props.Set(17, true); //old chaos/direct dmg
            m_Props.Set(19, true); //old ele dmg
            m_Props.Set(20, true); //old ele dmg
            m_Props.Set(21, true); //old ele dmg
            m_Props.Set(24, true); //old elemental dmg

            if (weapon is BaseRanged)
            {
                m_Props.Set(2, true); // ranged weapons cannot be ubws or mageweapon
            }
            else
            {
                m_Props.Set(25, true); // Only bows can be Balanced
                m_Props.Set(26, true); // Only bows have Velocity
            }

            for (int i = 0; i < attributeCount; ++i)
            {
                int random = GetUniqueRandom(27);

                if (random == -1)
                    break;

                switch (random)
                {
                    case 0:
                        {
                            switch (Utility.Random(5))
                            {
                                case 0:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitPhysicalArea, 2, 50, 2);
                                    break;
                                case 1:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitFireArea, 2, 50, 2);
                                    break;
                                case 2:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitColdArea, 2, 50, 2);
                                    break;
                                case 3:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitPoisonArea, 2, 50, 2);
                                    break;
                                case 4:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitEnergyArea, 2, 50, 2);
                                    break;
                            }

                            break;
                        }
                    case 1:
                        {
                            switch (Utility.Random(4))
                            {
                                case 0:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitMagicArrow, 2, 50, 2);
                                    break;
                                case 1:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitHarm, 2, 50, 2);
                                    break;
                                case 2:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitFireball, 2, 50, 2);
                                    break;
                                case 3:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitLightning, 2, 50, 2);
                                    break;
                            }

                            break;
                        }
                    case 2:
                        {
                            switch (Utility.Random(2))
                            {
                                case 0:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.UseBestSkill, 1, 1);
                                    break;
                                case 1:
                                    ApplyAttribute(secondary, min, max, AosWeaponAttribute.MageWeapon, 1, 10);
                                    break;
                            }

                            break;
                        }
                    case 3:
                        ApplyAttribute(primary, min, max, AosAttribute.WeaponDamage, 1, 50);
                        break;
                    case 4:
                        ApplyAttribute(primary, min, max, AosAttribute.DefendChance, 1, 15);
                        break;
                    case 5:
                        ApplyAttribute(primary, min, max, AosAttribute.CastSpeed, 1, 1);
                        break;
                    case 6:
                        ApplyAttribute(primary, min, max, AosAttribute.AttackChance, 1, 15);
                        break;
                    case 7:
                        ApplyAttribute(primary, min, max, AosAttribute.Luck, 1, 100);
                        break;
                    case 8:
                        ApplyAttribute(primary, min, max, AosAttribute.WeaponSpeed, 5, 50, 5);
                        break;
                    case 9:
                        ApplyAttribute(primary, min, max, AosAttribute.SpellChanneling, 1, 1);
                        break;
                    case 10:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitDispel, 2, 50, 2);
                        break;
                    case 11:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitLeechHits, 2, 50, 2);
                        break;
                    case 12:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitLowerAttack, 2, 50, 2);
                        break;
                    case 13:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitLowerDefend, 2, 50, 2);
                        break;
                    case 14:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitLeechMana, 2, 50, 2);
                        break;
                    case 15:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.HitLeechStam, 2, 50, 2);
                        break;
                    case 16:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.LowerStatReq, 10, 100, 10);
                        break;
                    case 17:
                        //ApplyAttribute(secondary, min, max, AosWeaponAttribute.ResistPhysicalBonus, 1, 15);
                        switch (Utility.Random(2))
                        {
                            case 0:
                                GetElementalDamages(weapon, AosElementAttribute.Chaos);
                                break;
                            case 1:
                                GetElementalDamages(weapon, AosElementAttribute.Direct);
                                break;
                        }
                        break;
                    case 18:
                        switch (Utility.Random(10))
                        {
                            case 0:
                                GetElementalDamages(weapon, AosElementAttribute.Physical);
                                break;
                            case 1:
                                GetElementalDamages(weapon, AosElementAttribute.Fire);
                                break;
                            case 2:
                                GetElementalDamages(weapon, AosElementAttribute.Water);
                                break;
                            case 3:
                                GetElementalDamages(weapon, AosElementAttribute.Poison);
                                break;
                            case 4:
                                GetElementalDamages(weapon, AosElementAttribute.Air);
                                break;
                            case 5:
                                GetElementalDamages(weapon, AosElementAttribute.Earth);
                                break;
                            case 6:
                                GetElementalDamages(weapon, AosElementAttribute.Necro);
                                break;
                            case 7:
                                GetElementalDamages(weapon, AosElementAttribute.Holy);
                                break;
                            case 8:
                                GetElementalDamages(weapon, AosElementAttribute.Chaos);
                                break;
                            case 9:
                                GetElementalDamages(weapon, AosElementAttribute.Direct);
                                break;
                        }
                        //ApplyAttribute(secondary, min, max, AosWeaponAttribute.ResistFireBonus, 1, 15);
                        //GetElementalDamages(weapon, AosElementAttribute.Fire);
                        break;
                    case 19:
                        //ApplyAttribute(secondary, min, max, AosWeaponAttribute.ResistColdBonus, 1, 15);
                        GetElementalDamages(weapon, AosElementAttribute.Water);
                        break;
                    case 20:
                        //ApplyAttribute(secondary, min, max, AosWeaponAttribute.ResistPoisonBonus, 1, 15);
                        GetElementalDamages(weapon, AosElementAttribute.Poison);
                        break;
                    case 21:
                        //ApplyAttribute(secondary, min, max, AosWeaponAttribute.ResistEnergyBonus, 1, 15);
                        GetElementalDamages(weapon, AosElementAttribute.Air);
                        break;
                    case 22:
                        ApplyAttribute(secondary, min, max, AosWeaponAttribute.DurabilityBonus, 10, 100, 10);
                        break;
                    case 23:
                        weapon.Slayer = GetRandomSlayer();
                        break;
                    case 24:
                        GetElementalDamages(weapon);
                        break;
                    case 25:
                        BaseRanged brb = weapon as BaseRanged;
                        brb.Balanced = true;
                        break;
                    case 26:
                        BaseRanged brv = weapon as BaseRanged;
                        brv.Velocity = (Utility.RandomMinMax(2, 50));
                        break;
                }
            }
        }

        public static void GetElementalDamages(BaseWeapon weapon)
        {
            GetElementalDamages(weapon, true);
        }

        public static void GetElementalDamages(BaseWeapon weapon, AosElementAttribute element)
        {
            int fire, phys, cold, nrgy, pois, earth, necro, holy, chaos, direct;

            weapon.GetDamageTypes(null, out phys, out fire, out cold, out pois, out nrgy, out earth, out necro, out holy, out chaos, out direct);

            AssignElementalDamage(weapon, element, phys);
        }

        public static void GetElementalDamages(BaseWeapon weapon, bool randomizeOrder)
        {
            int fire, phys, cold, nrgy, pois, earth, necro, holy, chaos, direct;

            weapon.GetDamageTypes(null, out phys, out fire, out cold, out pois, out nrgy, out earth, out necro, out holy, out chaos, out direct);

            int totalDamage = phys;

            AosElementAttribute[] attrs = new AosElementAttribute[]
            {
                AosElementAttribute.Water,
                AosElementAttribute.Air,
                AosElementAttribute.Fire,
                AosElementAttribute.Poison,
                AosElementAttribute.Earth,
                AosElementAttribute.Necro,
                AosElementAttribute.Holy
            };

            if (randomizeOrder)
            {
                for (int i = 0; i < attrs.Length; i++)
                {
                    int rand = Utility.Random(attrs.Length);
                    AosElementAttribute temp = attrs[i];

                    attrs[i] = attrs[rand];
                    attrs[rand] = temp;
                }
            }

            /*
            totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Cold,		totalDamage );
            totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Energy,	totalDamage );
            totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Fire,		totalDamage );
            totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Poison,	totalDamage );

            weapon.AosElementDamages[AosElementAttribute.Physical] = 100 - totalDamage;
            * */

            for (int i = 0; i < attrs.Length; i++)
                totalDamage = AssignElementalDamage(weapon, attrs[i], totalDamage);

            //Order is Cold, Energy, Fire, Poison -> Physical left
            //Cannot be looped, AoselementAttribute is 'out of order'

        }

        public static SlayerName GetRandomSlayer()
        {
            // TODO: Check random algorithm on OSI
            SlayerGroup[] groups = SlayerGroup.Groups;

            if (groups.Length == 0)
                return SlayerName.None;

            SlayerGroup group = groups[Utility.Random(groups.Length - 1)]; //-1 To Exclude the Fey Slayer which appears ONLY on a certain artifact.
            SlayerEntry entry;

            if (group.Entries.Length == 0 || 10 > Utility.Random(100)) // 10% chance to do super slayer
            {
                entry = group.Super;
            }
            else
            {
                SlayerEntry[] entries = group.Entries;
                entry = entries[Utility.Random(entries.Length)];
            }

            return entry.Name;
        }

        public static void ApplyAttributesTo(BaseArmor armor, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(armor, false, 0, attributeCount, min, max);
        }

        public static void ApplyAttributesTo(BaseArmor armor, bool isRunicTool, int luckChance, int attributeCount, int min, int max)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;

            if (attributeCount != 0)
            {
                armor.Unidentified = true;
            }

            AosAttributes primary = armor.Attributes;
            AosArmorAttributes secondary = armor.ArmorAttributes;

            m_Props.SetAll(false);

            bool isShield = (armor is BaseShield);
            int baseCount = (isShield ? 7 : 23);
            int baseOffset = (isShield ? 0 : 4);

            if (!isShield && armor.MeditationAllowance == ArmorMeditationAllowance.All)
                m_Props.Set(3, true); // remove mage armor from possible properties
            if (armor.Resource >= CraftResource.RegularLeather && armor.Resource <= CraftResource.BarbedLeather)
            {
                m_Props.Set(0, true); // remove lower requirements from possible properties for leather armor
                m_Props.Set(2, true); // remove durability bonus from possible properties
            }
            if (armor.RequiredRace == Race.Elf)
                m_Props.Set(7, true); // elves inherently have night sight and elf only armor doesn't get night sight as a mod

            //remove attributes we dont want here
            m_Props.Set(4 - baseOffset, true); //lower stat req
            m_Props.Set(9 - baseOffset, true); //stam reg
            m_Props.Set(11 - baseOffset, true); //night sight
            m_Props.Set(13 - baseOffset, true); //stamina bonus
            m_Props.Set(15 - baseOffset, true); //lower mana
            m_Props.Set(16 - baseOffset, true); //lower regs
            m_Props.Set(17 - baseOffset, true); //luck
            m_Props.Set(21 - baseOffset, true); //old resist
            m_Props.Set(22 - baseOffset, true); //old resist
            m_Props.Set(23 - baseOffset, true); //old resist
            m_Props.Set(24 - baseOffset, true); //old resist
            m_Props.Set(25 - baseOffset, true); //old resist
            m_Props.Set(26 - baseOffset, true); //old resist

            for (int i = 0; i < attributeCount; ++i)
            {
                int random = GetUniqueRandom(baseCount);

                if (random == -1)
                    break;

                random += baseOffset;

                switch (random)
                {
                    /* Begin Sheilds */
                    case 0:
                        ApplyAttribute(primary, min, max, AosAttribute.SpellChanneling, 1, 1);
                        break;
                    case 1:
                        ApplyAttribute(primary, min, max, AosAttribute.DefendChance, 1, 15);
                        break;
                    case 2:
                        if (Core.ML)
                        {
                            ApplyAttribute(primary, min, max, AosAttribute.ReflectPhysical, 1, 15);
                        }
                        else
                        {
                            ApplyAttribute(primary, min, max, AosAttribute.AttackChance, 1, 15);
                        }
                        break;
                    case 3:
                        ApplyAttribute(primary, min, max, AosAttribute.CastSpeed, 1, 1);
                        break;
                    /* Begin Armor */
                    case 4:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.LowerStatReq, 10, 100, 10);
                        break;
                    case 5:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.SelfRepair, 1, 5);
                        break;
                    case 6:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.DurabilityBonus, 10, 100, 10);
                        break;
                    /* End Shields */
                    case 7:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.MageArmor, 1, 1);
                        break;
                    case 8:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenHits, 1, 2);
                        break;
                    case 9:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenStam, 1, 3);
                        break;
                    case 10:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenMana, 1, 2);
                        break;
                    case 11:
                        ApplyAttribute(primary, min, max, AosAttribute.NightSight, 1, 1);
                        break;
                    case 12:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusHits, 1, 5);
                        break;
                    case 13:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusStam, 1, 8);
                        break;
                    case 14:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusMana, 1, 8);
                        break;
                    case 15:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerManaCost, 1, 8);
                        break;
                    case 16:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerRegCost, 1, 20);
                        break;
                    case 17:
                        ApplyAttribute(primary, min, max, AosAttribute.Luck, 1, 100);
                        break;
                    case 18:
                        ApplyAttribute(primary, min, max, AosAttribute.ReflectPhysical, 1, 15);
                        break;
                    case 19:
                        {
                            bool found = false;
                            while (found == false)
                            {
                                Console.WriteLine("loop");
                                switch (Utility.Random(8))
                                {
                                    case 0:
                                        if (armor.PhysicalBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Physical, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 1:
                                        if (armor.FireBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Fire, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 2:
                                        if (armor.ColdBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Cold, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 3:
                                        if (armor.PoisonBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Poison, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 4:
                                        if (armor.EnergyBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Energy, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 5:
                                        if (armor.EarthBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Earth, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 6:
                                        if (armor.NecroBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Necro, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 7:
                                        if (armor.HolyBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Holy, 1, 40);
                                            found = true;
                                        }
                                        break;
                                }
                            }
                            //ApplyResistance(armor, min, max, ResistanceType.Physical, 1, 15);
                            break;
                        }
                    case 20:
                        {
                            bool found = false;
                            while (found == false)
                            {
                                Console.WriteLine("loop");
                                switch (Utility.Random(8))
                                {
                                    case 0:
                                        if (armor.PhysicalBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Physical, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 1:
                                        if (armor.FireBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Fire, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 2:
                                        if (armor.ColdBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Cold, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 3:
                                        if (armor.PoisonBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Poison, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 4:
                                        if (armor.EnergyBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Energy, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 5:
                                        if (armor.EarthBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Earth, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 6:
                                        if (armor.NecroBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Necro, 1, 40);
                                            found = true;
                                        }
                                        break;
                                    case 7:
                                        if (armor.HolyBonus == 0)
                                        {
                                            ApplyResistance(armor, min, max, ResistanceType.Holy, 1, 40);
                                            found = true;
                                        }
                                        break;
                                }
                            }
                            //ApplyResistance(armor, min, max, ResistanceType.Fire, 1, 15);
                            break;
                        }
                    case 21:
                        ApplyResistance(armor, min, max, ResistanceType.Cold, 1, 15);
                        break;
                    case 22:
                        ApplyResistance(armor, min, max, ResistanceType.Poison, 1, 15);
                        break;
                    case 23:
                        ApplyResistance(armor, min, max, ResistanceType.Energy, 1, 15);
                        break;
                    case 24:
                        ApplyResistance(armor, min, max, ResistanceType.Earth, 1, 15);
                        break;
                    case 25:
                        ApplyResistance(armor, min, max, ResistanceType.Necro, 1, 15);
                        break;
                    case 26:
                        ApplyResistance(armor, min, max, ResistanceType.Holy, 1, 15);
                        break;
                    /* End Armor */
                }
            }
            armor.IdHue = armor.GetElementalProtectionHue();
        }

        public static void ApplyAttributesTo(BaseHat hat, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(hat, false, 0, attributeCount, min, max);
        }

        public static void ApplyAttributesTo(BaseHat hat, bool isRunicTool, int luckChance, int attributeCount, int min, int max)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;
            if (attributeCount != 0)
            {
                hat.Unidentified = true;
            }

            AosAttributes primary = hat.Attributes;
            AosArmorAttributes secondary = hat.ClothingAttributes;
            AosElementAttributes resists = hat.Resistances;
            AosSkillBonuses skills = hat.SkillBonuses; // added possible skill bonus on hats

            if (Utility.Random(3) == 1)
            {
                ApplySkillBonus(skills, min, max, 0, 1, 6);
            }

            m_Props.SetAll(false);


            //remove attributes we dont want here
            m_Props.Set(11, true); //lower stat req
            m_Props.Set(2, true); //stam reg
            m_Props.Set(4, true); //night sight
            m_Props.Set(6, true); //stamina bonus
            m_Props.Set(10, true); //luck
            m_Props.Set(8, true); //lower mana
            m_Props.Set(0, true); //ReflectPhysical
            m_Props.Set(9, true); //lower regs
            m_Props.Set(12, true); //SelfRepair
            m_Props.Set(13, true); // DurabilityBonus
            m_Props.Set(14, true); //resist, disable resist on head gear
            
            for (int i = 0; i < attributeCount; ++i)
            {
                int random = GetUniqueRandom(15);
                if (random == -1)
                    break;

                switch (random)
                {
                    case 0:
                        ApplyAttribute(primary, min, max, AosAttribute.ReflectPhysical, 1, 15);
                        break;
                    case 1:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenHits, 1, 2);
                        break;
                    case 2:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenStam, 1, 3);
                        break;
                    case 3:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenMana, 1, 2);
                        break;
                    case 4:
                        ApplyAttribute(primary, min, max, AosAttribute.NightSight, 1, 1);
                        break;
                    case 5:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusHits, 1, 5);
                        break;
                    case 6:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusStam, 1, 8);
                        break;
                    case 7:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusMana, 1, 8);
                        break;
                    case 8:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerManaCost, 1, 8);
                        break;
                    case 9:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerRegCost, 1, 20);
                        break;
                    case 10:
                        ApplyAttribute(primary, min, max, AosAttribute.Luck, 1, 100);
                        break;
                    case 11:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.LowerStatReq, 10, 100, 10);
                        break;
                    case 12:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.SelfRepair, 1, 5);
                        break;
                    case 13:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.DurabilityBonus, 10, 100, 10);
                        break;
                    case 14:
                        switch (Utility.Random(8)) // hats now only gives one large resist type
                        {
                            case 0:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Physical, 1, 100);
                                break;
                            case 1:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Fire, 1, 100);
                                break;
                            case 2:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Water, 1, 100);
                                break;
                            case 3:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Poison, 1, 100);
                                break;
                            case 4:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Air, 1, 100);
                                break;
                            case 5:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Earth, 1, 100);
                                break;
                            case 6:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Necro, 1, 100);
                                break;
                            case 7:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Holy, 1, 100);
                                break;
                        }
                        break;
                }
            }
            RenameItemToZuluStandard(hat);
        }

        public static void ApplyAttributesTo(BaseClothing clothing, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(clothing, false, 0, attributeCount, min, max);
        }

        public static void ApplyAttributesTo(BaseClothing clothing, bool isRunicTool, int luckChance, int attributeCount, int min, int max) // need to add "Steel" property to clothing, this disables spells (perhaps this needs to be discussed with sebbe)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;

            if (attributeCount != 0)
            {
                clothing.Unidentified = true;
                clothing.IdHue = Utility.Random(3000) + 1; //any hue id, for now
            }
            Console.WriteLine("ItemName: " + clothing.ItemData.Name);
            Console.WriteLine("MIN: " + min);
            Console.WriteLine("MAX: " + max);
            AosAttributes primary = clothing.Attributes;
            AosArmorAttributes secondary = clothing.ClothingAttributes;
            AosElementAttributes resists = clothing.Resistances;
            AosSkillBonuses skills = clothing.SkillBonuses;

            if (Utility.Random(3) == 1)
            {
                ApplySkillBonus(skills, min, max, 0, 1, 6);
            }

            m_Props.SetAll(false);

            //remove attributes we dont want here
            m_Props.Set(11, true); //lower stat req
            m_Props.Set(2, true); //stam reg
            m_Props.Set(4, true); //night sight
            m_Props.Set(6, true); //stamina bonus
            m_Props.Set(10, true); //luck
            m_Props.Set(8, true); //lower mana
            m_Props.Set(9, true); //lower regs
            m_Props.Set(14, true); //resist
           // m_Props.Set(15, true); //stat
            m_Props.Set(16, true); //resist
            m_Props.Set(17, true); //resist
            m_Props.Set(18, true); //resist
            
            for (int i = 0; i < attributeCount; ++i)
            {
                int random = GetUniqueRandom(16);

                if (random == -1)
                    break;

                switch (random)
                {
                    case 0:
                        ApplyAttribute(primary, min, max, AosAttribute.ReflectPhysical, 1, 15);
                        break;
                    case 1:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenHits, 1, 2);
                        break;
                    case 2:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenStam, 1, 3);
                        break;
                    case 3:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenMana, 1, 2);
                        break;
                    case 4:
                        ApplyAttribute(primary, min, max, AosAttribute.NightSight, 1, 1);
                        break;
                    case 5:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusHits, 1, 5);
                        break;
                    case 6:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusStam, 1, 8);
                        break;
                    case 7:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusMana, 1, 8);
                        break;
                    case 8:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerManaCost, 1, 8);
                        break;
                    case 9:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerRegCost, 1, 20);
                        break;
                    case 10:
                        ApplyAttribute(primary, min, max, AosAttribute.Luck, 1, 100);
                        break;
                    case 11:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.LowerStatReq, 10, 100, 10);
                        break;
                    case 12:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.SelfRepair, 1, 5);
                        break;
                    case 13:
                        ApplyAttribute(secondary, min, max, AosArmorAttribute.DurabilityBonus, 10, 100, 10);
                        break;
                    case 14:
                        switch (Utility.Random(8)) // clothing now only gives one large resist type
                        {
                            case 0:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Physical, 1, 100);
                                break;
                            case 1:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Fire, 1, 100);
                                break;
                            case 2:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Water, 1, 100);
                                break;
                            case 3:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Poison, 1, 100);
                                break;
                            case 4:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Air, 1, 100);
                                break;
                            case 5:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Earth, 1, 100);
                                break;
                            case 6:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Necro, 1, 100);
                                break;
                            case 7:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Holy, 1, 100);
                                break;
                        }
                        break;
                    /*case 15:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Fire, 1, 15);
                        break;
                    case 16:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Cold, 1, 15);
                        break;
                    case 17:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Poison, 1, 15);
                        break;
                    case 18:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Energy, 1, 15);
                        break; */
                    case 15:
                        switch (Utility.Random(3))
                        {
                            case 0:
                                ApplyAttribute(primary, min, max, AosAttribute.BonusStr, 1, 20);
                                break;
                            case 1:
                                ApplyAttribute(primary, min, max, AosAttribute.BonusDex, 1, 20);
                                break;
                            case 2:
                                ApplyAttribute(primary, min, max, AosAttribute.BonusInt, 1, 20);
                                break;
                        }
                        break;
                }
            }
            RenameItemToZuluStandard(clothing);
            Console.WriteLine("ItemName RENAMED: " + clothing.Name);

        }

        public static void ApplyAttributesTo(BaseJewel jewelry, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(jewelry, false, 0, attributeCount, min, max);
        }

        public static void ApplyAttributesTo(BaseJewel jewelry, bool isRunicTool, int luckChance, int attributeCount, int min, int max)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;

            if (attributeCount != 0)
            {
                jewelry.Unidentified = true;
            }


            Console.WriteLine(jewelry.ItemData.Name);

            AosAttributes primary = jewelry.Attributes;
            AosElementAttributes resists = jewelry.Resistances;
            AosSkillBonuses skills = jewelry.SkillBonuses;
            Console.WriteLine("ItemName: " + jewelry.ItemData.Name);
            Console.WriteLine("MIN: " + min);
            Console.WriteLine("MAX: " + max);
            if (Utility.Random(2) == 1)
            {
                ApplySkillBonus(skills, min, max, 0, 1, 6);
            }

            m_Props.SetAll(false);

            //remove attributes we dont want here
            m_Props.Set(1, true); //resist
            m_Props.Set(2, true); //resist
            m_Props.Set(3, true); //resist
            m_Props.Set(4, true); //stat
            m_Props.Set(11, true); //enhance pots
            m_Props.Set(8, true); //night sight
            m_Props.Set(9, true); //old dex
            m_Props.Set(10, true); //old int
            m_Props.Set(12, true); //cast speed
            m_Props.Set(13, true); //old int
            m_Props.Set(16, true); //luck
            m_Props.Set(14, true); //lower mana
            m_Props.Set(15, true); //lower regs
            m_Props.Set(17, true); //SpellDamage
            m_Props.Set(18, true); //night sight

            Console.WriteLine("Rolling Jewlery BEGIN");
            for (int i = 0; i < attributeCount; ++i)
            {
                int random = GetUniqueRandom(19);

                if (random == -1)
                    break;

                switch (random)
                {
                        
                    case 0:
                        switch (Utility.Random(8))
                        {
                            case 0:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Physical, 1, 100);
                                break;
                            case 1:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Fire, 1, 100);
                                break;
                            case 2:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Water, 1, 100);
                                break;
                            case 3:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Poison, 1, 100);
                                break;
                            case 4:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Air, 1, 100);
                                break;
                            case 5:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Earth, 1, 100);
                                break;
                            case 6:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Necro, 1, 100);
                                break;
                            case 7:
                                ApplyAttribute(resists, min, max, AosElementAttribute.Holy, 1, 100);
                                break;
                        }
                        break;
                   /* case 1:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Fire, 1, 15);
                        break;
                    case 2:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Cold, 1, 15);
                        break;
                    case 3:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Poison, 1, 15);
                        break;
                    case 4:
                        ApplyAttribute(resists, min, max, AosElementAttribute.Energy, 1, 15);
                        break;*/
                    case 1:
                        ApplyAttribute(primary, min, max, AosAttribute.WeaponDamage, 1, 25);
                        break;
                    case 2:
                        ApplyAttribute(primary, min, max, AosAttribute.DefendChance, 1, 15);
                        break;
                    case 3:
                        ApplyAttribute(primary, min, max, AosAttribute.AttackChance, 1, 15);
                        break;
                    case 4:
                        switch (Utility.Random(3))
                        {
                            case 0:
                                ApplyAttribute(primary, min, max, AosAttribute.BonusStr, 1, 20);
                                break;
                            case 1:
                                ApplyAttribute(primary, min, max, AosAttribute.BonusDex, 1, 20);
                                break;
                            case 2:
                                ApplyAttribute(primary, min, max, AosAttribute.BonusInt, 1, 20);
                                break;
                        }
                        break;
                    /*case 5:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusDex, 1, 8);
                        break;
                    case 10:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusInt, 1, 8);
                        break;*/
                    case 11:
                        ApplyAttribute(primary, min, max, AosAttribute.EnhancePotions, 5, 25, 5);
                        break;
                    case 12:
                        ApplyAttribute(primary, min, max, AosAttribute.CastSpeed, 1, 1);
                        break;
                    case 13:
                        ApplyAttribute(primary, min, max, AosAttribute.CastRecovery, 1, 3);
                        break;
                    case 14:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerManaCost, 1, 8);
                        break;
                    case 15:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerRegCost, 1, 20);
                        break;
                    case 16:
                        ApplyAttribute(primary, min, max, AosAttribute.Luck, 1, 100);
                        break;
                    case 17:
                        ApplyAttribute(primary, min, max, AosAttribute.SpellDamage, 1, 12);
                        break;
                    case 18:
                        ApplyAttribute(primary, min, max, AosAttribute.NightSight, 1, 1);
                        break;
                    //case 19:
                    //    ApplySkillBonus(skills, min, max, 0, 1, 15);
                    //    break;
                    //case 20:
                    //    ApplySkillBonus(skills, min, max, 1, 1, 15);
                    //    break;
                    //case 21:
                    //    ApplySkillBonus(skills, min, max, 2, 1, 15);
                    //    break;
                    //case 22:
                    //    ApplySkillBonus(skills, min, max, 3, 1, 15);
                    //    break;
                    //case 23:
                    //    ApplySkillBonus(skills, min, max, 4, 1, 15);
                    //    break;
                }
            }
           /*Console.WriteLine(jewelry.Resistances.Chaos);
            Console.WriteLine(jewelry.SkillBonuses.Skill_1_Name);
            Console.WriteLine(jewelry.Attributes.CastSpeed);
            Console.WriteLine(jewelry.Attributes.CastRecovery);
            Console.WriteLine(jewelry.Attributes.BonusMana);
            Console.WriteLine(jewelry.Attributes.SpellDamage);
            */
            //Console.WriteLine(jewelry.GetProperties(this));
            Console.WriteLine("Rolling Jewlery DONE");
            RenameItemToZuluStandard(jewelry);
        }

        public static void ApplyAttributesTo(Spellbook spellbook, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(spellbook, false, 0, attributeCount, min, max);
        }

        public static void ApplyAttributesTo(Spellbook spellbook, bool isRunicTool, int luckChance, int attributeCount, int min, int max)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;

            if (attributeCount != 0)
            {
                spellbook.Unidentified = true;
            }

            AosAttributes primary = spellbook.Attributes;
            AosSkillBonuses skills = spellbook.SkillBonuses;

            m_Props.SetAll(false);

            m_Props.Set(12, true); //lower mana req
            m_Props.Set(13, true); //lower regs req


            for (int i = 0; i < attributeCount; ++i)
            {
                int random = GetUniqueRandom(16);

                if (random == -1)
                    break;

                switch (random)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        {
                            ApplyAttribute(primary, min, max, AosAttribute.BonusInt, 1, 8);

                            for (int j = 0; j < 4; ++j)
                                m_Props.Set(j, true);

                            break;
                        }
                    case 4:
                        ApplyAttribute(primary, min, max, AosAttribute.BonusMana, 1, 8);
                        break;
                    case 5:
                        ApplyAttribute(primary, min, max, AosAttribute.CastSpeed, 1, 1);
                        break;
                    case 6:
                        ApplyAttribute(primary, min, max, AosAttribute.CastRecovery, 1, 3);
                        break;
                    case 7:
                        ApplyAttribute(primary, min, max, AosAttribute.SpellDamage, 1, 12);
                        break;
                    case 8:
                        ApplySkillBonus(skills, min, max, 0, 1, 15);
                        break;
                    case 9:
                        ApplySkillBonus(skills, min, max, 1, 1, 15);
                        break;
                    case 10:
                        ApplySkillBonus(skills, min, max, 2, 1, 15);
                        break;
                    case 11:
                        ApplySkillBonus(skills, min, max, 3, 1, 15);
                        break;
                    case 12:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerRegCost, 1, 20);
                        break;
                    case 13:
                        ApplyAttribute(primary, min, max, AosAttribute.LowerManaCost, 1, 8);
                        break;
                    case 14:
                        ApplyAttribute(primary, min, max, AosAttribute.RegenMana, 1, 2);
                        break;
                    case 15:
                        spellbook.Slayer = GetRandomSlayer();
                        break;
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)this.m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        this.m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
            }
        }

        public void ApplyAttributesTo(BaseWeapon weapon)
        {
            CraftResourceInfo resInfo = CraftResources.GetInfo(this.m_Resource);

            if (resInfo == null)
                return;

            CraftAttributeInfo attrs = resInfo.AttributeInfo;

            if (attrs == null)
                return;

            int attributeCount = Utility.RandomMinMax(attrs.RunicMinAttributes, attrs.RunicMaxAttributes);
            int min = attrs.RunicMinIntensity;
            int max = attrs.RunicMaxIntensity;

            ApplyAttributesTo(weapon, true, 0, attributeCount, min, max);
        }

        public void ApplyAttributesTo(BaseArmor armor)
        {
            CraftResourceInfo resInfo = CraftResources.GetInfo(this.m_Resource);

            if (resInfo == null)
                return;

            CraftAttributeInfo attrs = resInfo.AttributeInfo;

            if (attrs == null)
                return;

            int attributeCount = Utility.RandomMinMax(attrs.RunicMinAttributes, attrs.RunicMaxAttributes);
            int min = attrs.RunicMinIntensity;
            int max = attrs.RunicMaxIntensity;

            ApplyAttributesTo(armor, true, 0, attributeCount, min, max);
        }

        private static int Scale(int min, int max, int low, int high)
        {
            int percent;

            if (m_IsRunicTool)
            {
                percent = Utility.RandomMinMax(min, max);
            }
            else
            {
                // Behold, the worst system ever!
                int v = Utility.RandomMinMax(0, 10000);

                v = (int)Math.Sqrt(v);
                v = 100 - v;

                if (LootPack.CheckLuck(m_LuckChance))
                    v += 10;

                if (v < min)
                    v = min;
                else if (v > max)
                    v = max;

                percent = v;
            }

            int scaledBy = Math.Abs(high - low) + 1;

            if (scaledBy != 0)
                scaledBy = 10000 / scaledBy;

            percent *= (10000 + scaledBy);

            return low + (((high - low) * percent) / 1000001);
        }

        private static void ApplyAttribute(AosAttributes attrs, int min, int max, AosAttribute attr, int low, int high)
        {
            ApplyAttribute(attrs, min, max, attr, low, high, 1);
        }

        private static void ApplyAttribute(AosAttributes attrs, int min, int max, AosAttribute attr, int low, int high, int scale)
        {
            if (attr == AosAttribute.CastSpeed)
                attrs[attr] += Scale(min, max, low / scale, high / scale) * scale;
            else
                attrs[attr] = Scale(min, max, low / scale, high / scale) * scale;

            if (attr == AosAttribute.SpellChanneling)
                attrs[AosAttribute.CastSpeed] -= 1;
        }

        private static void ApplyAttribute(AosArmorAttributes attrs, int min, int max, AosArmorAttribute attr, int low, int high)
        {
            attrs[attr] = Scale(min, max, low, high);
        }

        private static void ApplyAttribute(AosArmorAttributes attrs, int min, int max, AosArmorAttribute attr, int low, int high, int scale)
        {
            attrs[attr] = Scale(min, max, low / scale, high / scale) * scale;
        }

        private static void ApplyAttribute(AosWeaponAttributes attrs, int min, int max, AosWeaponAttribute attr, int low, int high)
        {
            attrs[attr] = Scale(min, max, low, high);
        }

        private static void ApplyAttribute(AosWeaponAttributes attrs, int min, int max, AosWeaponAttribute attr, int low, int high, int scale)
        {
            attrs[attr] = Scale(min, max, low / scale, high / scale) * scale;
        }

        private static void ApplyAttribute(AosElementAttributes attrs, int min, int max, AosElementAttribute attr, int low, int high)
        {
            attrs[attr] = Scale(min, max, low, high);
        }

        private static void ApplyAttribute(AosElementAttributes attrs, int min, int max, AosElementAttribute attr, int low, int high, int scale)
        {
            attrs[attr] = Scale(min, max, low / scale, high / scale) * scale;
        }

        private static void ApplySkillBonus(AosSkillBonuses attrs, int min, int max, int index, int low, int high)
        {
            SkillName[] possibleSkills;// = (attrs.Owner is Spellbook ? m_PossibleSpellbookSkills : m_PossibleBonusSkills);
            if (attrs.Owner is Spellbook)
            {
                possibleSkills = m_PossibleSpellbookSkills;
            }
            else if (attrs.Owner is Pickaxe)
            {
                possibleSkills = new SkillName[] { SkillName.Mining };
            }
            else if (attrs.Owner is SmithHammer)
            {
                possibleSkills = new SkillName[] { SkillName.Blacksmith };
            }
            else
            {
                possibleSkills = m_PossibleBonusSkills;
            }
            int count = (Core.SE ? possibleSkills.Length : possibleSkills.Length - 2);

            SkillName sk, check;
            double bonus;
            bool found;
            int laps = 0;
            do
            {
                found = false;
                sk = possibleSkills[Utility.Random(count)];

                if (laps >= count)
                {
                    Console.WriteLine("Warning: Assigning random skill mod reached end of possible skill list, probably resulting in reassignment of old skill mod, count: " + count);
                    break;
                }

                for (int i = 0; !found && i < 5; ++i)
                    found = (attrs.GetValues(i, out check, out bonus) && check == sk);
                laps++;
            }
            while (found);

            attrs.SetValues(index, sk, Scale(min, max, low, high));
        }

        private static void ApplyResistance(BaseArmor ar, int min, int max, ResistanceType res, int low, int high)
        {
            switch (res)
            {
                case ResistanceType.Physical:
                    ar.PhysicalBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Fire:
                    ar.FireBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Cold:
                    ar.ColdBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Poison:
                    ar.PoisonBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Energy:
                    ar.EnergyBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Earth:
                    ar.EarthBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Necro:
                    ar.NecroBonus += Scale(min, max, low, high);
                    break;
                case ResistanceType.Holy:
                    ar.HolyBonus += Scale(min, max, low, high);
                    break;
            }
        }

        private static int AssignElementalDamage(BaseWeapon weapon, AosElementAttribute attr, int totalDamage)
        {
            if (totalDamage <= 0)
                return 0;

            int random = (Utility.Random(5) + 1) * 5;
            Console.WriteLine("got random % ele dmg: " + random);
            random = (random > totalDamage) ? totalDamage : random;
            weapon.AosElementDamages[attr] = random;
            weapon.IdHue = weapon.GetElementalDamageHue();
            return (totalDamage - random);
        }

        private static void RenameItemToZuluStandard(Item item)
        {
            if (debug) Console.WriteLine("Starting to rename item! Item: " + item.ItemData.Name);

            string newPrefix = "";
            string newSuffix = "";

            if (item is BaseJewel)
            {
                newPrefix += GetStatPrefix((item as BaseJewel).Attributes);
                newPrefix += GetSkillPrefix((item as BaseJewel).SkillBonuses);
            }
            else if(item is BaseClothing)
            {
                newPrefix += GetStatPrefix((item as BaseClothing).Attributes);
                newPrefix += GetSkillPrefix((item as BaseClothing).SkillBonuses);
            }
            
            // Since 
            newSuffix += GetProtectionSuffix(item);


            item.IdPrefix = newPrefix;
            item.IdSuffix = newSuffix;
            if (debug) Console.WriteLine("Renaming DONE Item: " + item.Name);
        }
        

        private static string GetSkillPrefix(AosSkillBonuses AosS){
            if (AosS.Skill_1_Value != 0)
            {
                return GetSkillNameSuffix(AosS.Skill_1_Name, AosS.Skill_1_Value);
            }
            if (AosS.Skill_2_Value != 0)
            {
                return GetSkillNameSuffix(AosS.Skill_2_Name, AosS.Skill_2_Value);
            }
            if (AosS.Skill_3_Value != 0)
            {
                return GetSkillNameSuffix(AosS.Skill_3_Name, AosS.Skill_3_Value);
            }
            if (AosS.Skill_4_Value != 0)
            {
                return GetSkillNameSuffix(AosS.Skill_4_Name, AosS.Skill_4_Value);
            }
            if (AosS.Skill_5_Value != 0)
            {
                return GetSkillNameSuffix(AosS.Skill_5_Name, AosS.Skill_5_Value);
            }
            return "";
        }

        private static string GetProtectionSuffix(Item aosE)
        {
            const int Curse_lv2_limit = -17;
            const int Curse_lv3_limit = -33; // not sure if needed yet, needs to be discussed.... it큦 a good gold sink
            const int Curse_lv4_limit = -50;
            const int Curse_lv5_limit = -65;
            const int Curse_lv6_limit = -85;

            const int lv2_limit = 17;
            const int lv3_limit = 33;
            const int lv4_limit = 50;
            const int lv5_limit = 65;
            const int lv6_limit = 85;

            int curr_resist = aosE.FireResistance;
            if (curr_resist > 0) 
            {
                if ( curr_resist <= lv2_limit) return " of Elemental Fire Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Fire Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Fire Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Fire Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Fire Attunement";
                else return " of Elemental Fire Absorbsion";
            }

            curr_resist = aosE.EarthResistance;
            if (curr_resist > 0) 
            {
                if ( curr_resist <= lv2_limit) return " of Elemental Earth Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Earth Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Earth Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Earth Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Earth Attunement";
                else return " of Elemental Earth Absorbsion";
            }

            curr_resist = aosE.ColdResistance;
            if (curr_resist > 0) 
            {
                if ( curr_resist <= lv2_limit) return " of Elemental Water Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Water Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Water Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Water Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Water Attunement";
                else return " of Elemental Water Absorbsion";
            }

            curr_resist = aosE.EnergyResistance;
            if (curr_resist > 0) 
            {
                if ( curr_resist <= lv2_limit) return " of Elemental Air Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Air Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Air Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Air Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Air Attunement";
                else return " of Elemental Air Absorbsion";
            }

            curr_resist = aosE.PhysicalResistance;
            if (curr_resist > 0)
            {
                aosE.IdHue = 1160;
                if (curr_resist <= lv2_limit) return " of Protection";
                else if (curr_resist < lv3_limit) return " of Stoneskin";
                else if (curr_resist < lv4_limit) return " of Unmovable Stone";
                else if (curr_resist < lv5_limit) return " of Adamantine Shielding";
                else if (curr_resist < lv6_limit) return " of Mystical Cloaks";
                else return " of Holy Auras";
            }

            curr_resist = aosE.NecroResistance;
            if (curr_resist > 0)
            {
                aosE.IdHue = 1170;
                if ( curr_resist <= lv2_limit) return " of Mystic Barrier";
                else if (curr_resist < lv3_limit) return " of Divine Shielding";
                else if (curr_resist < lv4_limit) return " of Heavenly Sanctuary";
                else if (curr_resist < lv5_limit) return " of Angelic Protection";
                else if (curr_resist < lv6_limit) return " of Arch-Angel's Guidance";
                else return " of Seraphim's Warding";
            }

            curr_resist = aosE.HolyResistance;
            if (curr_resist > 0) // not done yet.
            {
                aosE.IdHue = 1172;
                if (curr_resist <= lv2_limit) return " of Dark Barriers";
                else if (curr_resist < lv3_limit)  return " of Infernal Shielding";
                else if (curr_resist < lv4_limit) return " of Hellish Sanctuary";
                else if (curr_resist < lv5_limit) return " of Daemonic Protection";
                else if (curr_resist < lv6_limit) return " of Arch-Fiend's Guidance";
                else return " of Seraphim's Warding";
            }

            curr_resist = aosE.PoisonResistance;
            if (curr_resist > 0) // not done yet.
            {
                aosE.IdHue = 783;
                if (curr_resist <= lv2_limit) return " of Lesser Poison Protection";
                else if (curr_resist < lv3_limit) return " of Medium Poison Protection";
                else if (curr_resist < lv4_limit) return " of Greater Poison Protection";
                else if (curr_resist < lv5_limit) return " of Deadly Poison Protection";
                else if (curr_resist < lv6_limit) return " of the Snake Handler";
                else return " of Poison Absorbsion";
            }

            return "";
        }

        private static string GetStatPrefix(AosAttributes aosA)
        {
            // Havent fixed much, need to change the > < = ^^
            /*if (aosA.BonusDex > 0 && aosA.BonusDex <= 3) return "Heavy큦 ";
            else if (aosA.BonusDex > 3 && aosA.BonusDex <= 6) return "Leaden큦 ";
            else if (aosA.BonusDex > 6 && aosA.BonusDex <= 9) return "Encumbering큦 ";
            else if (aosA.BonusDex > 9 && aosA.BonusDex <= 12) return "Binding큦 ";
            else if (aosA.BonusDex > 12 && aosA.BonusDex <= 15) return "Fumbling큦 ";
            else if (aosA.BonusDex > 15) return "Blundering큦 ";*/

            if (aosA.BonusDex > 0 && aosA.BonusDex <= 3) return "Cutpuse큦 ";
            else if (aosA.BonusDex > 3 && aosA.BonusDex <= 6) return "Thief큦 ";
            else if (aosA.BonusDex > 6 && aosA.BonusDex <= 9) return "Catburgler큦 ";
            else if (aosA.BonusDex > 9 && aosA.BonusDex <= 12) return "Tumbler큦 ";
            else if (aosA.BonusDex > 12 && aosA.BonusDex <= 15) return "Acrobat큦 ";
            else if (aosA.BonusDex > 15) return "Escape Artist큦 ";

            else if (aosA.BonusInt > 0)
            {
                // Havent fixed much, need to change the > < = ^^
                /*if (aosA.BonusInt > 0 && aosA.BonusInt <= 3) return "Fool큦 ";
                else if (aosA.BonusInt > 3 && aosA.BonusInt <= 6) return "Simpletons큦 ";
                else if (aosA.BonusInt > 6 && aosA.BonusInt <= 9) return "Infantile ";
                else if (aosA.BonusInt > 9 && aosA.BonusInt <= 12) return "Senile ";
                else if (aosA.BonusInt > 12 && aosA.BonusInt <= 15) return "Demented ";
                else if (aosA.BonusInt > 15) return "Madman큦 ";*/

                if (aosA.BonusInt > 0 && aosA.BonusInt <= 3) return "Apprentice큦 ";
                else if (aosA.BonusInt > 3 && aosA.BonusInt <= 6) return "Adept큦 ";
                else if (aosA.BonusInt > 6 && aosA.BonusInt <= 9) return "Wizard큦 ";
                else if (aosA.BonusInt > 9 && aosA.BonusInt <= 12) return "Archmage큦 ";
                else if (aosA.BonusInt > 12 && aosA.BonusInt <= 15) return "Magister큦 ";
                else if (aosA.BonusInt > 15) return "Oracle큦 ";

            }

            else if (aosA.BonusStr > 0)
            {
                // Havent fixed much, need to change the > < = ^^
                /*if (aosA.BonusStr > 0 && aosA.BonusStr <= 3) return "Weakling큦 ";
                else if (aosA.BonusStr > 3 && aosA.BonusStr <= 6) return "Enfeebling ";
                else if (aosA.BonusStr > 6 && aosA.BonusStr <= 9) return "Powerless ";
                else if (aosA.BonusStr > 9 && aosA.BonusStr <= 12) return "Frail ";
                else if (aosA.BonusStr > 12 && aosA.BonusStr <= 15) return "Diseased ";
                else if (aosA.BonusStr > 15) return "Leper큦 ";*/

                if (aosA.BonusStr > 0 && aosA.BonusStr <= 3) return "Warrior큦 ";
                else if (aosA.BonusStr > 3 && aosA.BonusStr <= 6) return "Veteran큦 ";
                else if (aosA.BonusStr > 6 && aosA.BonusStr <= 9) return "Champion큦 ";
                else if (aosA.BonusStr > 9 && aosA.BonusStr <= 12) return "Hero큦 ";
                else if (aosA.BonusStr > 12 && aosA.BonusStr <= 15) return "Warlord큦 ";
                else if (aosA.BonusStr > 15) return "King큦 ";
            }
            return "";
        }

        private static string GetSkillNameSuffix(SkillName skillName, double skillVal)
        {

            switch (skillName)
            {
                case SkillName.Alchemy:
                    return GetSkillValueSuffix(skillVal) + "Alchemist's ";
                case SkillName.Anatomy:
                    return GetSkillValueSuffix(skillVal) + "Physician's ";
                case SkillName.AnimalLore:
                    return GetSkillValueSuffix(skillVal) + "Naturalist's ";
                case SkillName.AnimalTaming:
                    return GetSkillValueSuffix(skillVal) + "Druid's ";
                case SkillName.ArmsLore: 
                    return GetSkillValueSuffix(skillVal) + "Arms Dealer's ";
                case SkillName.Begging: 
                    return GetSkillValueSuffix(skillVal) + "Beggar's ";
                case SkillName.Blacksmith:
                    return GetSkillValueSuffix(skillVal) + "Blacksmith's ";
                case SkillName.Camping:
                    return GetSkillValueSuffix(skillVal) + "Camper's ";
                case SkillName.Carpentry:
                    return GetSkillValueSuffix(skillVal) + "Carpenter's ";
                case SkillName.Cartography:
                    return GetSkillValueSuffix(skillVal) + "Cartographer's ";
                case SkillName.Cooking:
                    return GetSkillValueSuffix(skillVal) + "Chef's ";
                case SkillName.DetectHidden:
                    return GetSkillValueSuffix(skillVal) + "Scout's ";
                case SkillName.Discordance: // wonder if we can change this skillname to the old "Enticement" instead...
                    return GetSkillValueSuffix(skillVal) + "Commander's ";
                case SkillName.EvalInt: 
                    return GetSkillValueSuffix(skillVal) + "Scholar's ";
                case SkillName.Fishing:
                    return GetSkillValueSuffix(skillVal) + "Fisherman's ";
                case SkillName.Fletching:
                    return GetSkillValueSuffix(skillVal) + "Fletcher's ";
                case SkillName.Forensics:
                    return GetSkillValueSuffix(skillVal) + "Coroner's ";
                case SkillName.Healing:
                    return GetSkillValueSuffix(skillVal) + "Healer's ";
                case SkillName.Herding:
                    return GetSkillValueSuffix(skillVal) + "Shepherd's ";
                case SkillName.Inscribe:
                    return GetSkillValueSuffix(skillVal) + "Scribe's ";
                case SkillName.ItemID:
                    return GetSkillValueSuffix(skillVal) + "Merchant's ";
                case SkillName.Lockpicking:
                    return GetSkillValueSuffix(skillVal) + "Locksmith's ";
                case SkillName.Lumberjacking:
                    return GetSkillValueSuffix(skillVal) + "Lumberjack's ";
                case SkillName.Meditation:
                    return GetSkillValueSuffix(skillVal) + "Stoic's ";
                case SkillName.Mining:
                    return GetSkillValueSuffix(skillVal) + "Miner's ";
                case SkillName.Musicianship:
                    return GetSkillValueSuffix(skillVal) + "Bard's ";
                case SkillName.Parry:
                    return GetSkillValueSuffix(skillVal) + "Shield Fighter's ";
                case SkillName.Peacemaking:
                    return GetSkillValueSuffix(skillVal) + "Peacemaker's ";
                case SkillName.Poisoning:
                    return GetSkillValueSuffix(skillVal) + "Assassin's ";
                case SkillName.Provocation:
                    return GetSkillValueSuffix(skillVal) + "Provoker's ";
                case SkillName.RemoveTrap:
                    return GetSkillValueSuffix(skillVal) + "Trap Remover's ";
                case SkillName.Snooping:
                    return GetSkillValueSuffix(skillVal) + "Pickpocket's ";
                case SkillName.SpiritSpeak:
                    return GetSkillValueSuffix(skillVal) + "Channeler's ";
                case SkillName.Stealing:
                    return GetSkillValueSuffix(skillVal) + "Thief's ";
                case SkillName.Stealth:
                    return GetSkillValueSuffix(skillVal) + "Spy's ";
                case SkillName.Tailoring:
                    return GetSkillValueSuffix(skillVal) + "Tailor's ";
                case SkillName.TasteID:
                    return GetSkillValueSuffix(skillVal) + "Taste Tester's ";
                case SkillName.Tinkering:
                    return GetSkillValueSuffix(skillVal) + "Tinker's ";
                case SkillName.Tracking:
                    return GetSkillValueSuffix(skillVal) + "Ranger's ";
                case SkillName.Veterinary:
                    return GetSkillValueSuffix(skillVal) + "Veterinarian's ";
                case SkillName.Magery:
                    return GetSkillValueSuffix(skillVal) + "Mage's ";
                    /* Skills that require function to rename */
                case SkillName.Wrestling: 
                    return GetWeaponSkillValue(skillVal);
                case SkillName.Archery:
                    return GetArcherySkillValue(skillVal);
                case SkillName.Swords: 
                    return GetWeaponSkillValue(skillVal);
                case SkillName.Tactics:
                    return GetTacticsSkillValue(skillVal);
                case SkillName.Macing:
                    return GetWeaponSkillValue(skillVal); 
                case SkillName.MagicResist: 
                    return GetMagicResistSkillValue(skillVal);
                case SkillName.Fencing: 
                    return GetWeaponSkillValue(skillVal);
                case SkillName.Hiding:  
                    return GetHidingSkillValue(skillVal);
                    /* End of custom naming skills */
            }

            return "";
        }

        // JustZH just added a base frame for +ar/-ar gear on cloth/jewlery
        private static string GetArmorPrefix(int arValue)
        {

            switch (arValue)
            {
                /*case -5:
                    return "Glass ";
                case -10:
                    return "Rusty ";
                case -15:
                    return "Aluminium ";
                case -20:
                    return "Pitted ";
                case -25:
                    return "Dirty ";
                case -30:
                    return "Tarnised ";*/
                case 5:
                    return "Iron ";
                case 10:
                    return "Steel ";
                case 15:
                    return "Meteoric Steel ";
                case 20:
                    return "Obsidian ";
                case 25:
                    return "Onyx ";
                case 30:
                    return "Adamantium ";
                default:
                    return "";
            }
        }

        private static string GetSkillValueSuffix(double skillVal) // added - stats, not really used yet, dunno if ever going to be used
        {
            int skillValInt = (int)skillVal;
            if (skillVal >= 7) // fix for pickaxe and smithys hammer
            {
                return "Grandmaster ";
            }
            switch (skillValInt)
            {
                /*case -1:
                    return "Novice ";
                case -2:
                    return "Neophyte ";
                case -3:
                    return "Inept ";
                case -4:
                    return "Incompetent ";
                case -5:
                    return "Failed ";
                case -6:
                    return "Blundering ";*/
                case 1:
                    return "Apprentice ";
                case 2:
                    return "Journeyman ";
                case 3:
                    return "Expert ";
                case 4:
                    return "Adept ";
                case 5:
                    return "Master ";
                case 6:
                    return "Grandmaster ";
                case 7:
                    return "Legendary "; // not used 
                default:
                    return "Grandmaster";
            }
        }

        private static string GetArcherySkillValue(double skillVal)
        {
            int skillValInt = (int)skillVal;
            switch (skillValInt)
            {
                /*case -1:
                    return "Water damaged ";
                case -2:
                    return "Crooked ";
                case -3:
                    return "Frayed ";
                case -4:
                    return "Warped ";
                case -5:
                    return "Decaying ";
                case -6:
                    return "Unstrung ";*/
                case 1:
                    return "Large ";
                case 2:
                    return "Great ";
                case 3:
                    return "Composite ";
                case 4:
                    return "Archer's ";
                case 5:
                    return "Ranger's ";
                case 6:
                    return "Marksman's ";
            }
            return " GetArcherySkillValue() ERROR ";
        }

        private static string GetTacticsSkillValue(double skillVal)
        {
            int skillValInt = (int)skillVal;
            switch (skillValInt)
            {
                /*case -1:
                    return "Poor ";
                case -2:
                    return "Dull ";
                case -3:
                    return "Inferior ";
                case -4:
                    return "Tainted ";
                case -5:
                    return "Pitifull ";
                case -6:
                    return "Worthless ";*/
                case 1:
                    return "Fine ";
                case 2:
                    return "Superior ";
                case 3:
                    return "Superb ";
                case 4:
                    return "Magnificent ";
                case 5:
                    return "Elegant ";
                case 6:
                    return "Peerless ";
            }
            return " GetTacticsSkillValue() ERROR ";
        }

        private static string GetMagicResistSkillValue(double skillVal)
        {
            int skillValInt = (int)skillVal;
            switch (skillValInt)
            {
                /*case -1:
                    return "Conducting ";
                case -2:
                    return "Sensitive ";
                case -3:
                    return "Focusing ";
                case -4:
                    return "Warped ";
                case -5:
                    return "Translucent ";
                case -6:
                    return "Amplifying ";*/
                case 1:
                    return "Shielded ";
                case 2:
                    return "Warded ";
                case 3:
                    return "Sanctified ";
                case 4:
                    return "Defiant ";
                case 5:
                    return "Guardian's ";
                case 6:
                    return "Deflecting ";
            }
            return " GetMagicResistSkillValue() ERROR ";
        }

        private static string GetHidingSkillValue(double skillVal)
        {
            int skillValInt = (int)skillVal;
            switch (skillValInt)
            {
                /*case -1:
                    return "Shiny ";
                case -2:
                    return "Gleaming ";
                case -3:
                    return "Sparkling ";
                case -4:
                    return "Brilliant ";
                case -5:
                    return "Illuminating ";
                case -6:
                    return "Dazzeling ";*/
                case 1:
                    return "Concealing ";
                case 2:
                    return "Camouflaged ";
                case 3:
                    return "Shadowed's ";
                case 4:
                    return "Undetectable ";
                case 5:
                    return "Obscuring ";
                case 6:
                    return "Obfuscating ";
            }
            return " GetHidingSkillValue() ERROR ";
        }

        private static string GetWeaponSkillValue(double skillVal)
        {
            int skillValInt = (int)skillVal;
            switch (skillValInt)
            {
                /*case -1:
                    return "Unbalanced ";
                case -2:
                    return "Fragile ";
                case -3:
                    return "Rusted ";
                case -4:
                    return "Cracked ";
                case -5:
                    return "Decaying ";
                case -6:
                    return "Misshappen ";*/
                case 1:
                    return "Competitor's ";
                case 2:
                    return "Duelist's ";
                case 3:
                    return "Gladiator's ";
                case 4:
                    return "Knight's ";
                case 5:
                    return "Noble's ";
                case 6:
                    return "Arms Master's ";
            }
            return " GetWeaponSkillValue() ERROR ";
        }
    }
}
