using Server.Custom;
using System;
using System.Collections;

namespace Server.Items
{
    public abstract class BaseRunicToolRewrite : BaseTool
    {
        private static Random rnd = new Random(Guid.NewGuid().GetHashCode());
        private static bool debug = false;

        private static readonly SkillName[] m_PossibleBonusSkills = new SkillName[]
        {
            SkillName.Herding,
            SkillName.Snooping,
            SkillName.Alchemy,
            SkillName.RemoveTrap,
            SkillName.EvalInt,
            SkillName.AnimalLore,
            SkillName.Lockpicking,
            SkillName.Poisoning,
            SkillName.Cooking,
            SkillName.Inscribe,
            SkillName.DetectHidden,
            SkillName.ItemID,
            SkillName.Tracking,
            SkillName.Tinkering,
            SkillName.Fishing,
            SkillName.Tailoring,
            SkillName.Lumberjacking,
            SkillName.Carpentry,
            SkillName.Fletching,
            SkillName.Blacksmith,
            SkillName.ArmsLore,
            SkillName.TasteID,
            SkillName.Begging,
            SkillName.Cartography,
            SkillName.Swords,
            SkillName.Fencing,
            SkillName.Macing,
            SkillName.Archery,
            SkillName.Wrestling,
            SkillName.Parry,
            SkillName.Tactics,
            SkillName.Hiding,
            SkillName.Anatomy,
            SkillName.Mining,
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

        private static readonly SkillName[] m_ArmorSkills = new SkillName[]
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
            SkillName.Stealth,
            SkillName.SpiritSpeak,
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
        private static int wepEnchant = 0;
        private static int m_LuckChance;
        private const int MaxProperties = 32;

        private static int armorLevel = 0;

        private CraftResource m_Resource;
        public BaseRunicToolRewrite(CraftResource resource, int itemID)
            : base(itemID)
        {
            this.m_Resource = resource;
        }

        public BaseRunicToolRewrite(CraftResource resource, int uses, int itemID)
            : base(uses, itemID)
        {
            this.m_Resource = resource;
        }

        public BaseRunicToolRewrite(Serial serial)
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
            if (debug) Console.WriteLine("generating damage for item with attributeCount: {0}, min: {1} max: {2}", attributeCount, min, max);
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
            if (debug) Console.WriteLine("min + {1} = {0} ({2})", primary[AosAttribute.WeaponDamage], (primary[AosAttribute.WeaponDamage] - min), maxDamage);

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
                                if (debug) Console.WriteLine("loop");
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
                                if (debug) Console.WriteLine("loop");
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
            if (debug) Console.WriteLine("ItemName: " + clothing.ItemData.Name);
            if (debug) Console.WriteLine("MIN: " + min);
            //   Console.WriteLine("MAX: " + max);
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
            if (debug) Console.WriteLine("ItemName RENAMED: " + clothing.Name);

        }


        public static void ApplyAttributesTo(BaseJewel jewelry, bool isRunicTool, int luckChance, int attributeCount, int min, int max)
        {
            m_IsRunicTool = isRunicTool;
            m_LuckChance = luckChance;

            if (attributeCount != 0)
            {
                jewelry.Unidentified = true;
            }

            if (debug) Console.WriteLine(jewelry.ItemData.Name);

            AosAttributes primary = jewelry.Attributes;
            AosElementAttributes resists = jewelry.Resistances;
            AosSkillBonuses skills = jewelry.SkillBonuses;
            if (debug) Console.WriteLine("ItemName: " + jewelry.ItemData.Name);
            if (debug) Console.WriteLine("MIN: " + min);
            //   Console.WriteLine("MAX: " + max);
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

            if (debug) Console.WriteLine("Rolling Jewlery BEGIN");
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

        private static void ApplyAttribute(AosAttributes attrs, AosAttribute attr, int low, int high)
        {
            ApplyAttribute(attrs, attr, low, high, 0);
        }

        private static void ApplyAttribute(AosAttributes attrs, AosAttribute attr, int low, int high, int scale)
        {
            var temp = rnd.Next(low, high);

            if (attr == AosAttribute.CastSpeed)
                attrs[attr] += temp;
            else
                attrs[attr] = temp;

            if (attr == AosAttribute.SpellChanneling)
                attrs[AosAttribute.CastSpeed] -= 1;

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

        private static void ApplyAttribute(AosWeaponAttributes attrs, AosWeaponAttribute attr, int low, int high)
        {
            var temp = rnd.Next(low, high);
            attrs[attr] = temp;
        }

        private static void ApplyAttribute(AosElementAttributes attrs, int min, int max, AosElementAttribute attr, int low, int high)
        {
            attrs[attr] = Scale(min, max, low, high);
        }

        private static void ApplyAttribute(AosElementAttributes attrs, AosElementAttribute attr, int percentage)
        {
            attrs[attr] = percentage;
        }

        /* private static void ApplyAttribute(AosAttributes attrs, AosAttribute attr, int percentage)
         {
             ApplyAttribute(attrs, attr, percentage);
         }*/

        private static void ApplyAttribute(AosAttributes attrs, AosAttribute attr, int percentage)
        {
            attrs[attr] = percentage;
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
                    if (debug) Console.WriteLine("Warning: Assigning random skill mod reached end of possible skill list, probably resulting in reassignment of old skill mod, count: " + count);
                    break;
                }

                for (int i = 0; !found && i < 5; ++i)
                    found = (attrs.GetValues(i, out check, out bonus) && check == sk);
                laps++;
            }
            while (found);

            attrs.SetValues(index, sk, Scale(min, max, low, high));
        }

        private static void ApplySkillBonus(AosSkillBonuses attrs, int index, int level)
        {
            int SkillRoll = Utility.Random(m_PossibleBonusSkills.Length);

            var RolledSkill = m_PossibleBonusSkills[SkillRoll];

            if (attrs.Owner is Pickaxe)
            {
                RolledSkill = SkillName.Mining;
            }
            else if (attrs.Owner is SmithHammer)
            {
                RolledSkill = SkillName.Blacksmith;
            }

            attrs.SetValues(index, RolledSkill, level);
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

            random = (random > totalDamage) ? totalDamage : random;
            weapon.AosElementDamages[attr] = random;
            weapon.IdHue = weapon.GetElementalDamageHue();
            return (totalDamage - random);
        }

        private static void RenameItemToZuluStandard(Item item)
        {
            string newPrefix = "";
            string newSuffix = "";

            if (item is BaseJewel)
            {
                newPrefix += GetStatPrefix((item as BaseJewel).Attributes);
                if ((item as BaseJewel).BaseArmorRating != 0)
                {
                    item.IdHue = 1109;
                    newPrefix += GetArmorPrefix((item as BaseJewel).BaseArmorRating);
                }
                newPrefix += GetSkillPrefix((item as BaseJewel).SkillBonuses);
                if ((item as BaseJewel).Resistances.FreeAction >= 1) { newSuffix += getFreeActionString(); item.IdHue = 590; }
                newSuffix += GetProtectionSuffix(item);
            }
            else if (item is BaseClothing)
            {
                newPrefix += GetStatPrefix((item as BaseClothing).Attributes);
                if ((item as BaseClothing).BaseArmorRating != 0)
                {
                    item.IdHue = 1109;
                    newPrefix += GetArmorPrefix((item as BaseClothing).BaseArmorRating);
                }
                else
                {
                    ApplyClothingColor(item as BaseClothing);
                }
                newPrefix += GetSkillPrefix((item as BaseClothing).SkillBonuses);
            }
            else if (item is BaseWeapon)
            {
                newPrefix += GetDurabilityPrefix(item as BaseWeapon);
                newPrefix += GetEnchantPrefix(item as BaseWeapon); // not sure about the order
                if ((item as BaseWeapon).SkillBonuses.Skill_1_Value != 0)
                {
                    if (item is Pickaxe) { newPrefix += GetSkillNameSuffix(SkillName.Mining, (item as BaseWeapon).SkillBonuses.Skill_1_Value); }
                    else if (item is SmithHammer) { newPrefix += GetSkillNameSuffix(SkillName.Blacksmith, (item as BaseWeapon).SkillBonuses.Skill_1_Value); }
                    else { newPrefix += GetSkillNameSuffix((item as BaseWeapon).Skill, (item as BaseWeapon).SkillBonuses.Skill_1_Value); }
                }
                if ((item as BaseWeapon).PermaPoison == true)
                {
                    newPrefix += "Poisoned ";
                    item.IdHue = 0x491;
                }

                // GetTacticsSkillValue
                newSuffix += GetDmgSuffix(item as BaseWeapon);
                newSuffix += GetOnHitEnchant(item as BaseWeapon);
                if ((item as BaseWeapon).Blackrock == true)
                {
                    newSuffix += " of Blackrock";
                    item.IdHue = 0x485;
                }
            }
            else if (item is BaseArmor)
            {
                newPrefix += GetStatPrefix((item as BaseArmor).Attributes);

                if ((item as BaseArmor).SkillBonuses.Skill_1_Name == SkillName.Hiding)
                {
                    if (debug) Console.WriteLine("HIDING!");
                    newPrefix += GetHidingSkillValue((item as BaseArmor).SkillBonuses.Skill_1_Value);
                }
                else if ((item as BaseArmor).SkillBonuses.Skill_1_Name == SkillName.MagicResist)
                {
                    if (debug) Console.WriteLine("RESIST!");
                    newPrefix += GetMagicResistSkillValue((item as BaseArmor).SkillBonuses.Skill_1_Value);
                }

                newPrefix += GetDurabilityPrefix(item as BaseArmor);
                newSuffix += GetArmorSuffix(item as BaseArmor);
                // not done
            }
            else if (item is BaseShield)
            {

                newPrefix += GetStatPrefix((item as BaseArmor).Attributes);

                if ((item as BaseArmor).SkillBonuses.Skill_1_Name == SkillName.Hiding)
                {
                    if (debug) Console.WriteLine("HIDING!");
                    newPrefix += GetHidingSkillValue((item as BaseArmor).SkillBonuses.Skill_1_Value);
                }
                else if ((item as BaseArmor).SkillBonuses.Skill_1_Name == SkillName.MagicResist)
                {
                    if (debug) Console.WriteLine("RESIST!");
                    newPrefix += GetMagicResistSkillValue((item as BaseArmor).SkillBonuses.Skill_1_Value);
                }

                newPrefix += GetDurabilityPrefix(item as BaseArmor);
                newSuffix += GetArmorSuffix(item as BaseArmor);
            }

            item.IdPrefix = newPrefix;
            item.IdSuffix = newSuffix;

        }


        private static string GetSkillPrefix(AosSkillBonuses AosS)
        {
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

        private static string getFreeActionString()
        {
            Console.WriteLine("HIT!");
            return " of Free Action";
        }

        private static string GetProtectionSuffix(Item aosE)
        {
            //const int Curse_lv2_limit = -17;
            //const int Curse_lv3_limit = -33; // not sure if needed yet, needs to be discussed.... it´s a good gold sink
            //const int Curse_lv4_limit = -50;
            //const int Curse_lv5_limit = -65;
            //const int Curse_lv6_limit = -85;

            const int lv2_limit = 17;
            const int lv3_limit = 33;
            const int lv4_limit = 50;
            const int lv5_limit = 65;
            const int lv6_limit = 85;

            int curr_resist = aosE.FireResistance;
            if (curr_resist > 0)
            {
                aosE.IdHue = 240;
                if (curr_resist <= lv2_limit) return " of Elemental Fire Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Fire Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Fire Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Fire Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Fire Attunement";
                else return " of Elemental Fire Absorbsion";
            }

            curr_resist = aosE.EarthResistance;
            if (curr_resist > 0)
            {
                aosE.IdHue = 343;
                if (curr_resist <= lv2_limit) return " of Elemental Earth Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Earth Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Earth Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Earth Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Earth Attunement";
                else return " of Elemental Earth Absorbsion";
            }

            curr_resist = aosE.ColdResistance;
            if (curr_resist > 0)
            {
                aosE.IdHue = 206;
                if (curr_resist <= lv2_limit) return " of Elemental Water Bane";
                else if (curr_resist < lv3_limit) return " of Elemental Water Warding";
                else if (curr_resist < lv4_limit) return " of Elemental Water Protection";
                else if (curr_resist < lv5_limit) return " of Elemental Water Immunity";
                else if (curr_resist < lv6_limit) return " of Elemental Water Attunement";
                else return " of Elemental Water Absorbsion";
            }

            curr_resist = aosE.EnergyResistance;
            if (curr_resist > 0)
            {
                aosE.IdHue = 1001;
                if (curr_resist <= lv2_limit) return " of Elemental Air Bane";
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
                if (curr_resist <= lv2_limit) return " of Mystic Barrier";
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
                else if (curr_resist < lv3_limit) return " of Infernal Shielding";
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

        private static string GetDmgSuffix(BaseWeapon wep)
        {
            if (wep.DamageLevel == WeaponDamageLevel.Regular) return "";
            switch (wep.DamageLevel)
            {
                case WeaponDamageLevel.Ruin:
                case WeaponDamageLevel.Might:
                case WeaponDamageLevel.Force:
                case WeaponDamageLevel.Power:
                    return " of " + wep.DamageLevel;
                case WeaponDamageLevel.Vanq:
                    return " of Vanquishing";
                case WeaponDamageLevel.Deva:
                    return " of Devastation";
                default:
                    return "";
            }
        }
        private static string GetArmorSuffix(BaseArmor armor)
        {
            var temp = armorLevel;
            armorLevel = 0;
            if (temp >= 1 && temp <= 5) return " of Defence ";
            else if (temp > 6 && temp <= 10) return " of Guarding ";
            else if (temp > 11 && temp <= 15) return " of Hardening ";
            else if (temp > 16 && temp <= 20) return " of Fortification ";
            else if (temp > 21 && temp <= 25) return " of Invulnerability ";
            else if (temp > 26) return " of Invincibility ";

            return "";
        }

        private static string GetDurabilityPrefix(BaseWeapon weapon)
        {
            var temp = Convert.ToInt32(weapon.DurabilityLevel);
            if (temp > 10) return "Durable ";
            else if (temp > 20) return "Substantial ";
            else if (temp > 30) return "Massive ";
            else if (temp > 40) return "Fortified ";
            else if (temp > 50) return "Tempered ";
            else if (temp > 60) return "Indestructible ";

            return "";
        }

        private static string GetDurabilityPrefix(BaseArmor armor)
        {
            var temp = Convert.ToInt32(armor.Durability);
            if (temp > 10) return "Durable ";
            else if (temp > 20) return "Substantial ";
            else if (temp > 30) return "Massive ";
            else if (temp > 40) return "Fortified ";
            else if (temp > 50) return "Tempered ";
            else if (temp > 60) return "Indestructible ";

            return "";
        }

        private static string GetEnchantPrefix(BaseWeapon weapon)
        {
            if (wepEnchant == 1) { wepEnchant = 0; return "Stygian "; }
            if (wepEnchant == 2) { wepEnchant = 0; return "Swift "; }
            if (wepEnchant == 3) { wepEnchant = 0; return "Mystical "; }
            else { wepEnchant = 0; return ""; }
        }

        private static string GetStatPrefix(AosAttributes aosA)
        {
            // Havent fixed much, need to change the > < = ^^
            /*if (aosA.BonusDex > 0 && aosA.BonusDex <= 3) return "Heavy´s ";
            else if (aosA.BonusDex > 3 && aosA.BonusDex <= 6) return "Leaden´s ";
            else if (aosA.BonusDex > 6 && aosA.BonusDex <= 9) return "Encumbering´s ";
            else if (aosA.BonusDex > 9 && aosA.BonusDex <= 12) return "Binding´s ";
            else if (aosA.BonusDex > 12 && aosA.BonusDex <= 15) return "Fumbling´s ";
            else if (aosA.BonusDex > 15) return "Blundering´s ";*/

            if (aosA.BonusDex > 0 && aosA.BonusDex <= 3) return "Cutpuse´s ";
            else if (aosA.BonusDex > 3 && aosA.BonusDex <= 6) return "Thief´s ";
            else if (aosA.BonusDex > 6 && aosA.BonusDex <= 9) return "Catburgler´s ";
            else if (aosA.BonusDex > 9 && aosA.BonusDex <= 12) return "Tumbler´s ";
            else if (aosA.BonusDex > 12 && aosA.BonusDex <= 15) return "Acrobat´s ";
            else if (aosA.BonusDex > 15) return "Escape Artist´s ";

            else if (aosA.BonusInt > 0)
            {
                // Havent fixed much, need to change the > < = ^^
                /*if (aosA.BonusInt > 0 && aosA.BonusInt <= 3) return "Fool´s ";
                else if (aosA.BonusInt > 3 && aosA.BonusInt <= 6) return "Simpletons´s ";
                else if (aosA.BonusInt > 6 && aosA.BonusInt <= 9) return "Infantile ";
                else if (aosA.BonusInt > 9 && aosA.BonusInt <= 12) return "Senile ";
                else if (aosA.BonusInt > 12 && aosA.BonusInt <= 15) return "Demented ";
                else if (aosA.BonusInt > 15) return "Madman´s ";*/

                if (aosA.BonusInt > 0 && aosA.BonusInt <= 3) return "Apprentice´s ";
                else if (aosA.BonusInt > 3 && aosA.BonusInt <= 6) return "Adept´s ";
                else if (aosA.BonusInt > 6 && aosA.BonusInt <= 9) return "Wizard´s ";
                else if (aosA.BonusInt > 9 && aosA.BonusInt <= 12) return "Archmage´s ";
                else if (aosA.BonusInt > 12 && aosA.BonusInt <= 15) return "Magister´s ";
                else if (aosA.BonusInt > 15) return "Oracle´s ";

            }

            else if (aosA.BonusStr > 0)
            {
                // Havent fixed much, need to change the > < = ^^
                /*if (aosA.BonusStr > 0 && aosA.BonusStr <= 3) return "Weakling´s ";
                else if (aosA.BonusStr > 3 && aosA.BonusStr <= 6) return "Enfeebling ";
                else if (aosA.BonusStr > 6 && aosA.BonusStr <= 9) return "Powerless ";
                else if (aosA.BonusStr > 9 && aosA.BonusStr <= 12) return "Frail ";
                else if (aosA.BonusStr > 12 && aosA.BonusStr <= 15) return "Diseased ";
                else if (aosA.BonusStr > 15) return "Leper´s ";*/

                if (aosA.BonusStr > 0 && aosA.BonusStr <= 3) return "Warrior´s ";
                else if (aosA.BonusStr > 3 && aosA.BonusStr <= 6) return "Veteran´s ";
                else if (aosA.BonusStr > 6 && aosA.BonusStr <= 9) return "Champion´s ";
                else if (aosA.BonusStr > 9 && aosA.BonusStr <= 12) return "Hero´s ";
                else if (aosA.BonusStr > 12 && aosA.BonusStr <= 15) return "Warlord´s ";
                else if (aosA.BonusStr > 15) return "King´s ";
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
            if (debug) Console.WriteLine("IN GETARMORPREFIX");
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
                case 1:
                    return "Iron ";
                case 2:
                    return "Steel ";
                case 3:
                    return "Meteoric Steel ";
                case 4:
                    return "Obsidian ";
                case 5:
                    return "Onyx ";
                case 6:
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

            if (skillVal >= 1)
            {
                return "Competitor's ";
            }
            else if (skillVal > 1 && skillVal < 2)
            {
                return "Duelist's ";
            }
            else if (skillVal > 2 && skillVal < 3)
            {
                return "Gladiator's ";
            }
            else if (skillVal > 4 && skillVal < 5)
            {
                return "Knight's ";
            }
            else if (skillVal > 5 && skillVal < 6)
            {
                return "Noble's ";
            }
            else if (skillVal > 6)
            {
                return "Arms Master's ";
            }

            return " GetWeaponSkillValue() ERROR ";
        }



        private static int GetChanceLevel(int MagicLevel)
        {
            var chances = rnd.Next(1, 100) * MagicLevel;

            // var chances = rnd.Next(1, 100) * MagicLevel;

            var level = 0;
            if (chances < 200)
                level = 1;
            else if (chances < 400)
                level = 2;
            else if (chances < 500)
                level = 3;
            else if (chances < 550)
                level = 4;
            else if (chances < 630)
                level = 5;
            else
                level = 6;


            if (MagicLevel >= 7 && level <= 2) { level++; }
            else if (MagicLevel >= 8 && level <= 2) { level += 2; }
            else if (MagicLevel >= 9 && level <= 2) { level += 2; }

            if (debug) Console.WriteLine("Apprentice Debug: MagicLevel = " + MagicLevel + " Chance = " + chances + " Final Level = " + level);
            return level;
        }

        // JustZH NEW STUFF //


        public static void ApplyAttributesTo(BaseJewel jewelry, int attributeCount, int min, int max)
        {
            ApplyAttributesTo(jewelry, false, 0, attributeCount, min, max);
        }

        private static void ApplyWeaponSkill(BaseWeapon weapon, int MagicLevel)
        {
            if (weapon is Pickaxe || weapon is SmithHammer)
            {
                AosSkillBonuses skills = weapon.SkillBonuses; // added possible skill bonus on hats
                int numb = rnd.Next(1, 50) * MagicLevel * 2;

                switch (MagicLevel / 3)
                {
                    case 0:
                        break;
                    case 1:
                        if (numb < 200)
                            numb = 200;
                        break;
                    case 2:
                        if (numb < 300)
                            numb = 300;
                        break;
                }
                int level;
                if (numb < 200)
                    level = 1;
                else if (numb < 300)
                    level = 3;
                else if (numb < 400)
                    level = 3;
                else if (numb < 500)
                    level = 4;
                else if (numb < 600)
                    level = 5;
                else
                    level = 6;

                if (weapon is Pickaxe)
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Mining);
                }

                else if (weapon is SmithHammer)
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Blacksmith);
                }
            }
            else
            {
                AosSkillBonuses skills = weapon.SkillBonuses; // added possible skill bonus on hats
                var chance = rnd.Next(1, 1000);


                int numb = rnd.Next(1, 50) * MagicLevel * 2;

                switch (MagicLevel / 3)
                {
                    case 0:
                        break;
                    case 1:
                        if (numb < 200)
                            numb = 200;
                        break;
                    case 2:
                        if (numb < 300)
                            numb = 300;
                        break;
                }
                int level;
                if (numb < 200)
                    level = 1;
                else if (numb < 300)
                    level = 3;
                else if (numb < 400)
                    level = 3;
                else if (numb < 500)
                    level = 4;
                else if (numb < 600)
                    level = 5;
                else
                    level = 6;

                if (weapon.DefSkill == SkillName.Archery)
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Archery);
                }
                else if (weapon.DefSkill == SkillName.Swords)
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Swords);
                }
                else if (weapon.DefSkill == SkillName.Fencing)
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Fencing);
                }
                else if (weapon.DefSkill == SkillName.Macing)
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Macing);
                }
                else if (weapon.DefSkill == SkillName.Wrestling) // is there a wrestling item?
                {
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Wrestling);
                }

            }

        }

        public static void ApplyEffectWeapon(BaseWeapon weapon, int MagicLevel)
        {

            weapon.Unidentified = true;

            if (weapon is Pickaxe || weapon is SmithHammer)
            {
                ApplyWeaponSkill(weapon, MagicLevel);
            }
            else
            {
                if (debug) Console.WriteLine("Started parsing weapon");
                var chances = (rnd.Next(1, 75) + 25) * ((MagicLevel / 2) + 1);

                if (chances >= 450)
                {
                    if (debug) Console.WriteLine("ApplyWepEnchant");
                    //   Console.WriteLine("ApplyWepEnchant");
                    ApplyWepEnchant(weapon, MagicLevel);
                }

                if (chances < 75)
                {
                    if (debug) Console.WriteLine("ApplyHPModWeapon");
                    ApplyHPModWeapon(weapon, MagicLevel);
                }

                else if (chances < 350)
                {
                    if (debug) Console.WriteLine("ApplyDmgMod");
                    ApplyDmgMod(weapon, MagicLevel);
                    if (rnd.Next(1, 100) < 15)
                    {
                        ApplyWeaponSkill(weapon, MagicLevel);
                    }
                }
                else
                {
                    if (debug) Console.WriteLine("ApplyHitScript");
                    ApplyHitScript(weapon, MagicLevel);
                }
            }
            RenameItemToZuluStandard(weapon);
        }

        public static void ApplyEffectArmor(BaseShield shield, int MagicLevel)
        {

            shield.Unidentified = true;

            if (debug) Console.WriteLine("Started parsing weapon");
            var chances = (rnd.Next(1, 75) + 25) * ((MagicLevel / 2) + 1);

            if (chances < 150)
            {
                if (debug) Console.WriteLine("Apply AR skillmod");
                ApplyArmorSkill(shield, MagicLevel);
                // Apply AR skillmod
            }

            if (chances < 300)
            {
                if (debug) Console.WriteLine("Durability MOD");
                ApplyHPModArmor(shield, MagicLevel);
                // HP MOD
            }

            else
            {
                if (debug) Console.WriteLine("Apply AR mod");
                ApplyArmorSkill(shield, MagicLevel);
                // Apply AR mod
            }


            RenameItemToZuluStandard(shield);
        }

        public static void ApplyEffectArmor(BaseArmor armor, int MagicLevel)
        {

            armor.Unidentified = true;

            if (debug) Console.WriteLine("Started parsing Armor");
            var chances = (rnd.Next(1, 75) + 25) * ((MagicLevel / 2) + 1);

            if (chances < 75)
            {
                if (debug) Console.WriteLine("Durability MOD");
                ApplyHPModArmor(armor, MagicLevel);
                // HP MOD
            }

            if (chances < 150)
            {
                ApplyArmorSkill(armor, MagicLevel);
                if (debug) Console.WriteLine("Apply AR skillmod");
                // Apply AR skillmod
            }

            else
            {
                ApplyArmorMod(armor, MagicLevel);
                //   Console.WriteLine("Apply AR mod");
                // Apply AR mod
            }


            RenameItemToZuluStandard(armor);
        }

        private static void ApplyArmorMod(BaseArmor armor, int MagicLevel)
        {

            var numb = rnd.Next(1, 50) * MagicLevel * 2;

            switch (MagicLevel / 3)
            {
                case 0:
                    break;
                case 1:
                    if (numb < 150)
                        numb = 150;
                    break;
                case 2:
                    if (numb < 300)
                        numb = 300;
                    break;
            }

            int arLevel;
            if (numb < 150)
                arLevel = 5;
            else if (numb < 300)
                arLevel = 10;
            else if (numb < 400)
                arLevel = 15;
            else if (numb < 500)
                arLevel = 20;
            else if (numb < 600)
                arLevel = 25;
            else
                arLevel = 30;

       //     armor.ProtectionLevel = (ArmorProtectionLevel)arLevel;
            armor.BaseArmorRating += arLevel;
            armorLevel = arLevel;

            if (rnd.Next(1, 100) <= (10 * MagicLevel))
            {
                if (rnd.Next(1, 100) <= 75)
                    ApplyHPModArmor(armor, MagicLevel);
            }
            else
            {
                ApplyArmorSkill(armor, MagicLevel);
            }
        }

        private static void ApplyArmorSkill(BaseArmor armor, int MagicLevel)
        {
            AosSkillBonuses skills = armor.SkillBonuses; // added possible skill bonus on hats
            var chance = rnd.Next(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(armor, MagicLevel);
            }

            int numb = rnd.Next(1, 50) * MagicLevel * 2;

            switch (MagicLevel / 3)
            {
                case 0:
                    break;
                case 1:
                    if (numb < 200)
                        numb = 200;
                    break;
                case 2:
                    if (numb < 300)
                        numb = 300;
                    break;
            }
            int level;
            if (numb < 200)
                level = 1;
            else if (numb < 300)
                level = 3;
            else if (numb < 400)
                level = 3;
            else if (numb < 500)
                level = 4;
            else if (numb < 600)
                level = 5;
            else
                level = 6;

            switch (rnd.Next(1, 2))
            {
                case 1:
                    ZuluApplySkillBonus(skills, 0, level, SkillName.MagicResist);
                    break;
                case 2:
                    ZuluApplySkillBonus(skills, 0, level, SkillName.Hiding);
                    break;
            }

            if (rnd.Next(1, 100) <= 5 * MagicLevel)
            {
                ApplyHPModArmor(armor, MagicLevel);
            }


        }

        private static void ApplyStatMod(BaseArmor armor, int MagicLevel)
        {
            AosAttributes primary = armor.Attributes;

            var level = (GetChanceLevel(MagicLevel) * 5);
            var percentage = LevelToPercantageStats(level);

            switch (Utility.Random(3))
            {
                case 1:
                    ApplyAttribute(primary, AosAttribute.BonusStr, percentage);
                    break;
                case 2:
                    ApplyAttribute(primary, AosAttribute.BonusInt, percentage);
                    break;
                case 3:
                    ApplyAttribute(primary, AosAttribute.BonusDex, percentage);
                    break;
            }
        }

        private static void ApplyArSkillMod(BaseArmor armor, int MagicLevel) // not done
        {
            if (rnd.Next(1, 1000) <= 5)
            {
                ApplyStatMod(armor, MagicLevel);
            }

            int numb = rnd.Next(1, 2);

            switch (numb)
            {
                case 1:
                    
                    // Magicresist
                    break;
                case 2:
                    // Tactics
                    break;
            }

            if (rnd.Next(1, 100) <= (10 * MagicLevel))
            {
                if (debug) Console.WriteLine("HP MOD");
                ApplyHPModArmor(armor, MagicLevel);
                // HP MOD
            }
        }


        private static string GetOnHitEnchant(BaseWeapon weapon)
        {
            if (weapon.WeaponAttributes.HitFireball != 0)
            {
                return " of Daemon's Breath";
            }
            else if (weapon.WeaponAttributes.HitLightning != 0)
            {
                return " of Thunder";
            }
            else if (weapon.WeaponAttributes.HitMagicArrow != 0)
            {
                return " of Burning";
            }
            else if (weapon.WeaponAttributes.HitCurse != 0)
            {
                return " of Evil";
            }
            else if (weapon.WeaponAttributes.HitManaDrain != 0)
            {
                return " of the Vampire"; // mana drain
            }
            else if (weapon.WeaponAttributes.HitHarm != 0)
            {
                return " of Wounding";
            }
            else if (weapon.WeaponAttributes.HitFatigue != 0)
            {
                return " of Bungling";
            }
            else if (weapon.WeaponAttributes.HitColdArea != 0) // sounds cool, dunno what it does
            {
                return " of Frost";
            }
            else if (weapon.WeaponAttributes.HitManaDrain != 0) // sounds cool, dunno what it does
            {
                return " of Mages Bane";
            }
            else if (weapon.WeaponAttributes.HitElementalFury != 0)
            {
                return " of Elemental Fury";
            }
            else { return ""; }
        }

        private static void ApplyHitScript(BaseWeapon weapon, int MagicLevel)
        {
            AosWeaponAttributes secondary = weapon.WeaponAttributes;
            var rand = rnd.Next(1, 10);
            // doesnt take magiclevel into consideration yet, maybe have to add efficiency too..
            switch (rand)
            {
                case 1:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitMagicArrow, 2, 100);
                    break;
                case 2:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitLightning, 2, 100);
                    break;
                case 3:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitFireball, 2, 100);
                    break;
                case 4:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitCurse, 2, 100);
                    break;
                case 5:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitManaDrain, 2, 100);
                    break;
                case 6:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitHarm, 2, 100);
                    break;
                case 7:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitFatigue, 2, 100);
                    break;
                case 8:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitColdArea, 2, 100);
                    break;
                case 9:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitElementalFury, 2, 100);
                    break;
                case 10:
                    ApplyAttribute(secondary, AosWeaponAttribute.HitManaDrain, 2, 100);
                    break;
            }
            var another = rnd.Next(1, 100);
            if (another <= (10 * MagicLevel))
            {
                ApplyWepEnchant(weapon, MagicLevel);
            }

        }

        private static void ApplyDmgMod(BaseWeapon weapon, int MagicLevel)
        {
            var chances = GetChanceLevel(MagicLevel * 5);
            var another = rnd.Next(1, 100);
            AosAttributes primary = weapon.Attributes;
            AosWeaponAttributes secondary = weapon.WeaponAttributes;
            if (rnd.Next(1, 1000) <= 2)
            {
                weapon.Blackrock = true;

                if (another <= (10 * MagicLevel))
                {
                    ApplyHPModWeapon(weapon, MagicLevel);
                    if (rnd.Next(1, 100) <= 75)
                    {
                        ApplyWepEnchant(weapon, MagicLevel);
                    }
                    else
                    {
                        // ApplyWeapSkillMod
                    }
                }
            }
            else
            {
                var numb = rnd.Next(50 + 1) * (MagicLevel * 2);
                if (rnd.Next(1, 1000) <= 2)
                {
                    weapon.PermaPoison = true;
                    if (numb < 150) { weapon.Poison = Poison.Lesser; }
                    else if (numb < 300) { weapon.Poison = Poison.Lesser; }
                    else if (numb < 400) { weapon.Poison = Poison.Regular; }
                    else if (numb < 500) { weapon.Poison = Poison.Greater; }
                    else if (numb < 600) { weapon.Poison = Poison.Deadly; }
                    else { weapon.Poison = Poison.Lethal; }

                    if (another <= (10 * MagicLevel))
                    {
                        ApplyHPModWeapon(weapon, MagicLevel);
                        if (rnd.Next(1, 100) <= 75)
                        {
                            ApplyWepEnchant(weapon, MagicLevel);
                        }
                        else
                        {
                            // ApplyWeapSkillMod
                        }
                    }
                }
                else
                {

                    switch (MagicLevel / 3)
                    {
                        case 0:
                            break;
                        case 1:
                            if (numb < 300)
                                numb = 300;
                            break;
                        case 2:
                            if (numb < 150)
                                numb = 150;
                            break;
                    }



                    if (numb < 150) { weapon.DamageLevel = WeaponDamageLevel.Ruin; }
                    else if (numb < 300) { weapon.DamageLevel = WeaponDamageLevel.Force; }
                    else if (numb < 400) { weapon.DamageLevel = WeaponDamageLevel.Might; }
                    else if (numb < 500) { weapon.DamageLevel = WeaponDamageLevel.Power; }
                    else if (numb < 600) { weapon.DamageLevel = WeaponDamageLevel.Vanq; }
                    else { weapon.DamageLevel = WeaponDamageLevel.Deva; }

                    another = rnd.Next(1, 85);
                    if (another <= (10 * MagicLevel))
                    {
                        ApplyHPModWeapon(weapon, MagicLevel);
                        if (rnd.Next(1, 100) <= 75)
                        {
                            ApplyWepEnchant(weapon, MagicLevel);
                        }
                        else
                        {
                            // ApplyWeapSkillMod
                        }
                    }
                }
            }
        }

        private static void ApplyClothingColor(BaseClothing clothing)
        {
            var chance = rnd.Next(1, 100);

            int[] CoolhueArray = { 1151, 1154, 1086, 1068, 1070, 1167, 1168, 1177, 1178, 1179, 1196, 1270, 1288, 1296, 1297, 1304, 1305, 1354, 1367, 1368, 1370, 1374,
            1399, 1455, 1481, 1568, 1690, 1691, 1760, 1763, 1775, 1772, 1787, 1916, 1960, 1961, 1969, 1991, 2025, 2054, 2055, 2056, 2057, 2058, 2059 };

            if(chance >= 75) // only 25% chance to color cloth
            {
                if(chance >= 95) // 5% chance to get cool hue 
                {
                    var coolhue = rnd.Next(0, CoolhueArray.Length - 1);
                    clothing.SetHue = coolhue;
                }
                else
                {
                    var ordinaryHue = rnd.Next(1, 860);
                    clothing.SetHue = ordinaryHue;
                    // 860
                }
            }
        }

        
        private static void ApplyWepEnchant(BaseWeapon weapon, int MagicLevel)
        {

            AosAttributes primary = weapon.Attributes;
            AosAttributes primary2 = weapon.Attributes;
            AosWeaponAttributes secondary = weapon.WeaponAttributes;

            var numb = rnd.Next(1, 100);
            if (debug) Console.WriteLine("numb: " + numb);
            if (numb >= 90)
            {
                // styg
                ApplyAttribute(primary, AosAttribute.SpellChanneling, 1, 1);
                ApplyAttribute(primary2, AosAttribute.WeaponSpeed, 50, 75, 0);
                weapon.IdHue = 1174;
                wepEnchant = 1;
                if (debug) Console.WriteLine("Stygian");
            }
            else if (numb >= 75)
            {
                // swift
                if (debug) Console.WriteLine("swift");
                ApplyAttribute(primary, AosAttribute.WeaponSpeed, 50, 75, 0);
                weapon.IdHue = 621;
                wepEnchant = 2;
            }
            else if (numb >= 65)
            {
                // mystical
                if (debug) Console.WriteLine("mystical");
                ApplyAttribute(primary, AosAttribute.SpellChanneling, 1, 1);
                weapon.IdHue = 6;
                wepEnchant = 3;
            }
            else
            {
                //   Console.WriteLine("nothing");
            }
        }

        private static void ApplyHPModArmor(BaseArmor armor, int MagicLevel)
        {

            var numb = rnd.Next(1, 50) * MagicLevel * 2;

            switch (MagicLevel / 3)
            {
                case 0:
                    break;
                case 1:
                    if (numb < 75)
                        numb = 75;
                    break;
                case 2:
                    if (numb < 150)
                        numb = 150;
                    break;
            }

            if (numb <= 75) { armor.Durability = (ArmorDurabilityLevel)10; }
            else if (numb <= 150) { armor.Durability = (ArmorDurabilityLevel)20; }
            else if (numb <= 300) { armor.Durability = (ArmorDurabilityLevel)30; }
            else if (numb <= 400) { armor.Durability = (ArmorDurabilityLevel)40; }
            else if (numb <= 550) { armor.Durability = (ArmorDurabilityLevel)50; }
            else { armor.Durability = (ArmorDurabilityLevel)60; }
        }

        private static void ApplyHPModWeapon(BaseWeapon weapon, int MagicLevel)
        {

            var numb = rnd.Next(1, 50) * MagicLevel * 2;

            switch (MagicLevel / 3)
            {
                case 0:
                    break;
                case 1:
                    if (numb < 75)
                        numb = 75;
                    break;
                case 2:
                    if (numb < 150)
                        numb = 150;
                    break;
            }

            if (numb <= 75) { weapon.DurabilityLevel = (WeaponDurabilityLevel)10; }
            else if (numb <= 150) { weapon.DurabilityLevel = (WeaponDurabilityLevel)20; }
            else if (numb <= 300) { weapon.DurabilityLevel = (WeaponDurabilityLevel)30; }
            else if (numb <= 400) { weapon.DurabilityLevel = (WeaponDurabilityLevel)40; }
            else if (numb <= 550) { weapon.DurabilityLevel = (WeaponDurabilityLevel)50; }
            else { weapon.DurabilityLevel = (WeaponDurabilityLevel)60; }
        }

        private static void ApplyJewelEnchant(BaseJewel jewel, int MagicLevel)
        {

            var chance = rnd.Next(1, 100) * MagicLevel;
            //   Console.WriteLine("Chance: " + chance);
            if (chance < 300)
            {
                ApplyProtection(jewel, MagicLevel); // charges reflect, absorb and so on, not constant immunity (NOT YET IMPLEMENTED)
            }
            else if (chance < 500)
            {
                ApplyImmunity(jewel, MagicLevel);
                //Apply magic immunity // (NOT YET IMPLEMENTED)
            }
            else
            {
                ApplyElementalProtection(jewel, MagicLevel);
            }

            if (rnd.Next(1, 100) <= (5 * MagicLevel))
            {
                var secchance = rnd.Next(1, 100);
                if (secchance < 75)
                {
                    ApplyMiscSkillMod(jewel, MagicLevel);
                }
                else
                {
                    ApplyMiscArMod(jewel, MagicLevel);
                }
            }

        }

        public static void ApplyEnchant(BaseJewel jewelry, int MagicLevel) // Fantasia DONE 
        {

            var chance = rnd.Next(1, 100) * MagicLevel;

            if (chance < 400)
            {
                ApplyProtection(jewelry, MagicLevel);
            }
            if (chance < 500)
            {
                if (rnd.Next(1, 100) <= 25)
                { // made Immunity items even more rare
                    ApplyImmunity(jewelry, MagicLevel);
                }
                else
                {
                    ApplyElementalProtection(jewelry, MagicLevel); // gotta buff this
                }
            }
            else
                ApplyElementalProtection(jewelry, MagicLevel); // gotta buff this

            // Roll for another enchantment
            if (rnd.Next(1, 100) <= (10 * MagicLevel))
                ApplyMiscSkillMod(jewelry, MagicLevel);
            else
                ApplyMiscArMod(jewelry, MagicLevel);

        }


        private static void ApplyProtection(BaseJewel jewelry, int MagicLevel)
        {
            var chanceCase = rnd.Next(2) + 1;

            AosElementAttributes resists = jewelry.Resistances;

            var level = GetChanceLevel(MagicLevel);
            int resistLevel = LevelToPercantage(level);

            var charges = GetChanceLevel(MagicLevel) * 5;

            switch (Utility.Random(3)) // NOT YET IMPLEMENTED
            {
                case 1:
                    // SpellReflection
                    break;
                case 2:
                    // MagicProtection
                    break;
                case 3:
                    ApplyAttribute(resists, AosElementAttribute.Poison, resistLevel);
                    // PoisonProtection
                    break;
            }

        }

        private static void ApplyImmunity(BaseJewel jewelry, int MagicLevel)
        {
            if (debug) Console.WriteLine("Adding Immunity");
            var level = (GetChanceLevel(MagicLevel) * 5);

            int resistLevel = LevelToPercantage(level);

            AosElementAttributes resists = jewelry.Resistances;

            switch (rnd.Next(1, 3))
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    ApplyAttribute(resists, AosElementAttribute.Poison, resistLevel);
                    break;
            }
            /*
            var chance_case := RandomInt(2)+1,
            level := GetChanceLevel(),
            element;
            
            case( chance_case )
                1:	element := "PermSpellReflection";
                    break;

                2:	element := "PermMagicProtection";
                    break;

                3:	element := "PermPoisonProtection";
                    break;
            endcase
        */

            //   Console.WriteLine("Adding Immunity"); // Not yet implemented
        }

        public static void ApplyEffectJewlery(BaseJewel jewelry, int MagicLevel) // Fantasia DONE
        {
            /*	var enchant := (Random(75)+26) * Cint(magiclevel/2 + 1);                
             * if(enchant < 250)
                               ApplyMiscSkillMod( item );
                           elseif(enchant < 300 )
                               ApplyEnchant ( who , item );
                           else
                               ApplyMiscArMod( item );
                           endif
						
                           AddName( item );
                           break;*/

            jewelry.Unidentified = true;

            var chances = (rnd.Next(1, 75) + 25) * ((MagicLevel / 2) + 1);
            if (debug) Console.WriteLine("ApplyEffectJewlery: " + chances);
            if (chances < 250)
                ApplyMiscSkillMod(jewelry, MagicLevel);
            else if (chances < 300)
                ApplyEnchant(jewelry, MagicLevel);
            else
                ApplyMiscArMod(jewelry, MagicLevel);

            RenameItemToZuluStandard(jewelry);
        }

        public static void ApplyEffectClothing(BaseClothing cloth, int MagicLevel)
        {
            //   Console.WriteLine("IN CLOTHING!");
            cloth.Unidentified = true;

            var chances = (rnd.Next(1, 75) + 25) * ((MagicLevel / 2) + 1);

            if (chances < 250)
                ApplyMiscSkillMod(cloth, MagicLevel);
            else
                ApplyMiscArModClothing(cloth, MagicLevel);

            // ApplyRandomMiscColor();

            RenameItemToZuluStandard(cloth);
        }

        private static void ApplyMiscSkillMod(BaseJewel jewelry, int MagicLevel) // Fantasia DONE
        {

            AosSkillBonuses skills = jewelry.SkillBonuses;

            if (rnd.Next(1000) <= 5) // maybe an small buff needed
                ApplyStatMod(jewelry, MagicLevel);
            var level = GetChanceLevel(MagicLevel);

            ApplySkillBonus(skills, 0, level);

        }

        private static void ApplyMiscSkillMod(BaseClothing cloth, int MagicLevel)
        {
            if (debug) Console.WriteLine("ApplyMiscSkillMod");
            AosSkillBonuses skills = cloth.SkillBonuses;
            var level = GetChanceLevel(MagicLevel);

            if (rnd.Next(1, 1000) <= 5)
            {
                ApplyStatModCloth(cloth, MagicLevel);
            }

            ApplySkillBonus(skills, 0, level);

        }

        private static void ApplyStatMod(BaseJewel jewelry, int MagicLevel)
        {
            AosAttributes primary = jewelry.Attributes;

            var level = (GetChanceLevel(MagicLevel) * 5);
            var percentage = LevelToPercantageStats(level);

            switch (Utility.Random(3))
            {
                case 1:
                    ApplyAttribute(primary, AosAttribute.BonusStr, percentage);
                    break;
                case 2:
                    ApplyAttribute(primary, AosAttribute.BonusInt, percentage);
                    break;
                case 3:
                    ApplyAttribute(primary, AosAttribute.BonusDex, percentage);
                    break;
            }
        }

        private static void ApplyStatModCloth(BaseClothing cloth, int MagicLevel)
        {
            if (debug) Console.WriteLine("ApplyStatModCloth");
            AosAttributes primary = cloth.Attributes;

            var level = (GetChanceLevel(MagicLevel) * 5);
            var percentage = LevelToPercantageStats(level);

            switch (Utility.Random(3))
            {
                case 1:
                    ApplyAttribute(primary, AosAttribute.BonusStr, percentage);
                    break;
                case 2:
                    ApplyAttribute(primary, AosAttribute.BonusInt, percentage);
                    break;
                case 3:
                    ApplyAttribute(primary, AosAttribute.BonusDex, percentage);
                    break;
            }
        }

        private static void ZuluApplySkillBonus(AosSkillBonuses attrs, int index, int level, SkillName skillname)
        {

            SkillName sk = skillname;

            attrs.SetValues(index, sk, level);
        }

        private static void ApplyMiscArMod(BaseJewel jewelry, int MagicLevel) // there currently is no base armor on jewels. gotta change
        {
            if (debug) Console.WriteLine("ApplyMiscArMod");
            var level = GetChanceLevel(MagicLevel);

            if (level == 1) { jewelry.BaseArmorRating = 1; }
            else if (level == 2) { jewelry.BaseArmorRating = 2; }
            else if (level == 3) { jewelry.BaseArmorRating = 3; }
            else if (level == 4) { jewelry.BaseArmorRating = 4; }
            else if (level == 5) { jewelry.BaseArmorRating = 5; }
            else if (level == 6) { jewelry.BaseArmorRating = 6; }

            if (rnd.Next(1, 100) <= (10 * MagicLevel))
            {
                //   Console.WriteLine("ApplyMiscSkillMod");
                ApplyMiscSkillMod(jewelry, MagicLevel);
            }
        }

        private static void ApplyMiscArModClothing(BaseClothing cloth, int MagicLevel) // there currently is no base armor on jewels. gotta change
        {
            if (debug) Console.WriteLine("ApplyMiscArMod");
            var level = GetChanceLevel(MagicLevel);

            if (level == 1) { cloth.BaseArmorRating = 1; }
            else if (level == 2) { cloth.BaseArmorRating = 2; }
            else if (level == 3) { cloth.BaseArmorRating = 3; }
            else if (level == 4) { cloth.BaseArmorRating = 4; }
            else if (level == 5) { cloth.BaseArmorRating = 5; }
            else if (level == 6) { cloth.BaseArmorRating = 6; }

            if (rnd.Next(1, 100) <= (10 * MagicLevel))
            {
                //   Console.WriteLine("ApplyMiscSkillMod");
                ApplyMiscSkillMod(cloth, MagicLevel);
            }

        }

        private static int LevelToPercantage(int level)
        {
            if (level == 1)
                return rnd.Next(1, 16);
            else if (level == 2)
                return rnd.Next(17, 33);
            else if (level == 3)
                return rnd.Next(33, 50);
            else if (level == 4)
                return rnd.Next(50, 65);
            else if (level == 5)
                return rnd.Next(65, 85);
            else
                return rnd.Next(85, 100);
        }

        private static int LevelToPercantageStats(int level)
        {
            if (level == 1)
                return rnd.Next(1, 3);
            else if (level == 2)
                return rnd.Next(3, 6);
            else if (level == 3)
                return rnd.Next(6, 9);
            else if (level == 4)
                return rnd.Next(9, 12);
            else if (level == 5)
                return rnd.Next(12, 15);
            else
                return rnd.Next(15, 20);
        }

        private static void ApplyElementalProtection(BaseJewel jewelry, int MagicLevel)
        {
            AosElementAttributes resists = jewelry.Resistances;

            var level = GetChanceLevel(MagicLevel);
            int resistLevel = LevelToPercantage(level);



            if (debug) Console.WriteLine("In ApplyElementalProtection");

            switch (Utility.Random(8))
            {
                case 0:
                    ApplyAttribute(resists, AosElementAttribute.Fire, resistLevel);
                    break;
                case 1:
                    ApplyAttribute(resists, AosElementAttribute.Water, resistLevel);
                    break;
                case 2:
                    ApplyAttribute(resists, AosElementAttribute.Air, resistLevel);
                    break;
                case 3:
                    ApplyAttribute(resists, AosElementAttribute.Earth, resistLevel);
                    break;
                case 4:
                    ApplyAttribute(resists, AosElementAttribute.Necro, resistLevel);
                    break;
                case 5:
                    ApplyAttribute(resists, AosElementAttribute.Holy, resistLevel);

                    break;
                // case 7:
                // ApplyAttribute(resists, AosElementAttribute.Healing, resistLevel); // Healing mod not yet implemented :(
                //       goto DoAgain; // remove later
                //break;
                case 6:
                    ApplyAttribute(resists, AosElementAttribute.Physical, resistLevel);
                    break;
                case 7:
                    ApplyAttribute(resists, AosElementAttribute.FreeAction, 1);
                    break;
                //break;
            }

        }

    }
}
