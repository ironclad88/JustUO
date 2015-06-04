using System;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public abstract class BaseOre : Item
    {
        protected virtual CraftResource DefaultResource { get { return CraftResource.Iron; } }

        private CraftResource m_Resource;

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
                this.InvalidateProperties();
            }
        }

        public abstract BaseIngot GetIngot();

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)this.m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2: // Reset from Resource System
                    this.m_Resource = this.DefaultResource;
                    reader.ReadString();
                    break;
                case 1:
                    {
                        this.m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
                case 0:
                    {
                        OreInfo info;

                        switch (reader.ReadInt())
                        {
                            case 0:
                                info = OreInfo.Iron;
                                break;
                            case 1:
                                info = OreInfo.DullCopper;
                                break;
                            case 2:
                                info = OreInfo.ShadowIron;
                                break;
                            case 3:
                                info = OreInfo.Copper;
                                break;
                            case 4:
                                info = OreInfo.Bronze;
                                break;
                            case 5:
                                info = OreInfo.Gold;
                                break;
                            case 6:
                                info = OreInfo.Agapite;
                                break;
                            case 7:
                                info = OreInfo.Verite;
                                break;
                            case 8:
                                info = OreInfo.Valorite;
                                break;
                            case 9:
                                info = OreInfo.Zulu;
                                break;
                            case 10:
                                info = OreInfo.Onyx;
                                break;
                            case 11:
                                info = OreInfo.Pyrite;
                                break;
                            case 12:
                                info = OreInfo.Malachite;
                                break;
                            case 13:
                                info = OreInfo.Azurite;
                                break;
                            case 14:
                                info = OreInfo.Platinum;
                                break;
                            case 15:
                                info = OreInfo.LavaRock;
                                break;
                            case 16:
                                info = OreInfo.Mystic;
                                break;
                            case 17:
                                info = OreInfo.Spike;
                                break;
                            case 18:
                                info = OreInfo.Fruity;
                                break;
                            case 19:
                                info = OreInfo.IceRock;
                                break;
                            case 20:
                                info = OreInfo.SilverRock;
                                break;
                            case 21:
                                info = OreInfo.Spectral;
                                break;
                            case 22:
                                info = OreInfo.Undead;
                                break;
                            case 23:
                                info = OreInfo.DarkPagan;
                                break;
                            case 24:
                                info = OreInfo.OldBritain;
                                break;
                            case 25:
                                info = OreInfo.Virginity;
                                break;
                            case 26:
                                info = OreInfo.BlackDwarf;
                                break;
                            case 27:
                                info = OreInfo.RedElven;
                                break;
                            case 28:
                                info = OreInfo.Dripstone;
                                break;
                            case 29:
                                info = OreInfo.Executor;
                                break;
                            case 30:
                                info = OreInfo.Peachblue;
                                break;
                            case 31:
                                info = OreInfo.Destruction;
                                break;
                            case 32:
                                info = OreInfo.Anra;
                                break;
                            case 33:
                                info = OreInfo.Goddess;
                                break;
                            case 34:
                                info = OreInfo.Crystal;
                                break;
                            case 35:
                                info = OreInfo.Doom;
                                break;
                            case 36:
                                info = OreInfo.ETS;
                                break;
                            case 37:
                                info = OreInfo.DSR;
                                break;
                            case 38:
                                info = OreInfo.RND;
                                break;
                            default:
                                info = null;
                                break;
                        }

                        this.m_Resource = CraftResources.GetFromOreInfo(info);
                        break;
                    }
            }
        }

        private static int RandomSize()
        {
            // double rand = Utility.RandomDouble();

            /*if (rand < 0.12)
                return 0x19B7;
            else if (rand < 0.18)       // fixes so that the server dont add stupid graphic ore.
                return 0x19B8;
            else if (rand < 0.25)
                return 0x19BA;
            else*/
            return 0x19B9;
        }

        public BaseOre(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseOre(CraftResource resource, int amount)
            : base(RandomSize())
        {
            this.Stackable = true;
            this.Amount = amount;
            this.Hue = CraftResources.GetHue(resource);

            this.m_Resource = resource;
        }

        public BaseOre(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = CraftResources.GetInfo(this.Resource).Name;
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name + " Ore");
            else
                list.Add(name + " Ore");
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            //if (!CraftResources.IsStandard(this.m_Resource))
            //{
            //    int num = CraftResources.GetLocalizationNumber(this.m_Resource);

            //    if (num > 0)
            //        list.Add(num);
            //    else
            //        list.Add(CraftResources.GetName(this.m_Resource));
            //}
        }

        public override int LabelNumber
        {
            get
            {
                if (this.m_Resource >= CraftResource.DullCopper && this.m_Resource <= CraftResource.RND)
                {
                    if (this.m_Resource == CraftResource.Zulu) { return 1063269; };
                    if (this.m_Resource == CraftResource.Onyx) { return 1098656; };
                    if (this.m_Resource == CraftResource.Pyrite) { return 1098657; };
                    if (this.m_Resource == CraftResource.Malachite) { return 1098658; };
                    if (this.m_Resource == CraftResource.Azurite) { return 1098659; };
                    if (this.m_Resource == CraftResource.Platinum) { return 1098660; };
                    if (this.m_Resource == CraftResource.Lavarock) { return 1098661; };
                    if (this.m_Resource == CraftResource.Mystic) { return 1098662; };
                    if (this.m_Resource == CraftResource.Spike) { return 1098663; };
                    if (this.m_Resource == CraftResource.Fruity) { return 1098664; };
                    if (this.m_Resource == CraftResource.IceRock) { return 1098665; };
                    if (this.m_Resource == CraftResource.SilverRock) { return 1098666; };
                    if (this.m_Resource == CraftResource.Spectral) { return 1098667; };
                    if (this.m_Resource == CraftResource.Undead) { return 1098668; };
                    if (this.m_Resource == CraftResource.DarkPagan) { return 1098669; };
                    if (this.m_Resource == CraftResource.OldBritain) { return 1098670; };
                    if (this.m_Resource == CraftResource.Virginity) { return 1098671; };
                    if (this.m_Resource == CraftResource.BlackDwarf) { return 1098672; };
                    if (this.m_Resource == CraftResource.RedElven) { return 1098673; };
                    if (this.m_Resource == CraftResource.Dripstone) { return 1098674; };
                    if (this.m_Resource == CraftResource.Executor) { return 1098675; };
                    if (this.m_Resource == CraftResource.Peachblue) { return 1098676; };
                    if (this.m_Resource == CraftResource.Destruction) { return 1098677; };
                    if (this.m_Resource == CraftResource.Anra) { return 1098678; };
                    if (this.m_Resource == CraftResource.Goddess) { return 1098679; };
                    if (this.m_Resource == CraftResource.Crystal) { return 1098680; };
                    if (this.m_Resource == CraftResource.Doom) { return 1098681; };
                    if (this.m_Resource == CraftResource.ETS) { return 1098685; };
                    if (this.m_Resource == CraftResource.DSR) { return 1098683; };
                    if (this.m_Resource == CraftResource.RND) { return 1098684; };
                    return 1042845 + (int)(this.m_Resource - CraftResource.DullCopper);
                }

                return 1042853; // iron ore;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Movable)
                return;

            if (this.RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
            }
            else if (from.InRange(this.GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(501971); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
            }
        }

        private class InternalTarget : Target
        {
            private readonly BaseOre m_Ore;

            public InternalTarget(BaseOre ore)
                : base(2, false, TargetFlags.None)
            {
                this.m_Ore = ore;
            }

            private bool IsForge(object obj)
            {
                if (Core.ML && obj is Mobile && ((Mobile)obj).IsDeadBondedPet)
                    return false;

                if (obj.GetType().IsDefined(typeof(ForgeAttribute), false))
                    return true;

                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;
                else if (obj is StaticTarget)
                    itemID = ((StaticTarget)obj).ItemID;

                return (itemID == 4017 || (itemID >= 6522 && itemID <= 6569));
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (this.m_Ore.Deleted)
                    return;

                if (!from.InRange(this.m_Ore.GetWorldLocation(), 2))
                {
                    from.SendLocalizedMessage(501976); // The ore is too far away.
                    return;
                }

                #region Combine Ore
                if (targeted is BaseOre)
                {
                    BaseOre ore = (BaseOre)targeted;

                    if (!ore.Movable)
                    {
                        return;
                    }
                    else if (this.m_Ore == ore)
                    {
                        from.SendLocalizedMessage(501972); // Select another pile or ore with which to combine this.
                        from.Target = new InternalTarget(ore);
                        return;
                    }
                    else if (ore.Resource != this.m_Ore.Resource)
                    {
                        from.SendLocalizedMessage(501979); // You cannot combine ores of different metals.
                        return;
                    }

                    int worth = ore.Amount;

                    if (ore.ItemID == 0x19B9)
                        worth *= 8;
                    else if (ore.ItemID == 0x19B7)
                        worth *= 2;
                    else
                        worth *= 4;

                    int sourceWorth = this.m_Ore.Amount;

                    if (this.m_Ore.ItemID == 0x19B9)
                        sourceWorth *= 8;
                    else if (this.m_Ore.ItemID == 0x19B7)
                        sourceWorth *= 2;
                    else
                        sourceWorth *= 4;

                    worth += sourceWorth;

                    int plusWeight = 0;
                    int newID = ore.ItemID;

                    if (ore.DefaultWeight != this.m_Ore.DefaultWeight)
                    {
                        if (ore.ItemID == 0x19B7 || this.m_Ore.ItemID == 0x19B7)
                        {
                            newID = 0x19B7;
                        }
                        else if (ore.ItemID == 0x19B9)
                        {
                            newID = this.m_Ore.ItemID;
                            plusWeight = ore.Amount * 2;
                        }
                        else
                        {
                            plusWeight = this.m_Ore.Amount * 2;
                        }
                    }

                    if ((ore.ItemID == 0x19B9 && worth > 120000) || ((ore.ItemID == 0x19B8 || ore.ItemID == 0x19BA) && worth > 60000) || (ore.ItemID == 0x19B7 && worth > 30000))
                    {
                        from.SendLocalizedMessage(1062844); // There is too much ore to combine.
                        return;
                    }
                    else if (ore.RootParent is Mobile && (plusWeight + ((Mobile)ore.RootParent).Backpack.TotalWeight) > ((Mobile)ore.RootParent).Backpack.MaxWeight)
                    {
                        from.SendLocalizedMessage(501978); // The weight is too great to combine in a container.
                        return;
                    }

                    ore.ItemID = newID;

                    if (ore.ItemID == 0x19B9)
                        ore.Amount = worth / 8;
                    else if (ore.ItemID == 0x19B7)
                        ore.Amount = worth / 2;
                    else
                        ore.Amount = worth / 4;

                    this.m_Ore.Delete();
                    return;
                }
                #endregion

                if (this.IsForge(targeted))
                {
                    double difficulty;

                    switch (this.m_Ore.Resource)
                    {
                        default:
                            difficulty = 50.0;
                            break;
                        case CraftResource.DullCopper:
                            difficulty = 65.0;
                            break;
                        case CraftResource.ShadowIron:
                            difficulty = 70.0;
                            break;
                        case CraftResource.Copper:
                            difficulty = 75.0;
                            break;
                        case CraftResource.Bronze:
                            difficulty = 80.0;
                            break;
                        case CraftResource.Gold:
                            difficulty = 85.0;
                            break;
                        case CraftResource.Agapite:
                            difficulty = 90.0;
                            break;
                        case CraftResource.Verite:
                            difficulty = 95.0;
                            break;
                        case CraftResource.Valorite:
                            difficulty = 99.0;
                            break;
                        case CraftResource.Zulu:
                            difficulty = 99.0;
                            break;
                    }

                    double minSkill = difficulty - 25.0;
                    double maxSkill = difficulty + 25.0;

                    if (difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value)
                    {
                        from.SendLocalizedMessage(501986); // You have no idea how to smelt this strange ore!
                        return;
                    }

                    if (this.m_Ore.ItemID == 0x19B7 && this.m_Ore.Amount < 2)
                    {
                        from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        return;
                    }

                    if (from.CheckTargetSkill(SkillName.Mining, targeted, minSkill, maxSkill))
                    {
                        int toConsume = this.m_Ore.Amount;

                        if (toConsume <= 0)
                        {
                            from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        }
                        else
                        {
                            if (toConsume > 30000)
                                toConsume = 30000;

                            int ingotAmount;

                            if (this.m_Ore.ItemID == 0x19B7)
                            {
                                ingotAmount = toConsume / 2;

                                if (toConsume % 2 != 0)
                                    --toConsume;
                            }
                            else if (this.m_Ore.ItemID == 0x19B9)
                            {
                                ingotAmount = toConsume * 2;
                            }
                            else
                            {
                                ingotAmount = toConsume;
                            }

                            BaseIngot ingot = this.m_Ore.GetIngot();
                            ingot.Amount = ingotAmount;

                            this.m_Ore.Consume(toConsume);
                            from.AddToBackpack(ingot);
                            //from.PlaySound( 0x57 );

                            from.SendLocalizedMessage(501988); // You smelt the ore removing the impurities and put the metal in your backpack.
                        }
                    }
                    else
                    {
                        if (this.m_Ore.Amount < 2)
                        {
                            if (this.m_Ore.ItemID == 0x19B9)
                                this.m_Ore.ItemID = 0x19B8;
                            else
                                this.m_Ore.ItemID = 0x19B7;
                        }
                        else
                        {
                            this.m_Ore.Amount /= 2;
                        }

                        from.SendLocalizedMessage(501990); // You burn away the impurities but are left with less useable metal.
                    }
                }
            }
        }
    }

    public class IronOre : BaseOre
    {
        [Constructable]
        public IronOre()
            : this(1)
        {
        }

        [Constructable]
        public IronOre(int amount)
            : base(CraftResource.Iron, amount)
        {
        }

        public IronOre(bool fixedSize)
            : this(1)
        {
            if (fixedSize)
                this.ItemID = 0x19B8;
        }

        public IronOre(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new IronIngot();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }
    }

    public class DullCopperOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DullCopper; } }

        [Constructable]
        public DullCopperOre()
            : this(1)
        {
        }

        [Constructable]
        public DullCopperOre(int amount)
            : base(CraftResource.DullCopper, amount)
        {
        }

        public DullCopperOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DullCopperIngot();
        }

    }

    public class ShadowIronOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.ShadowIron; } }

        [Constructable]
        public ShadowIronOre()
            : this(1)
        {
        }

        [Constructable]
        public ShadowIronOre(int amount)
            : base(CraftResource.ShadowIron, amount)
        {
        }

        public ShadowIronOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new ShadowIronIngot();
        }
    }

    public class CopperOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Copper; } }

        [Constructable]
        public CopperOre()
            : this(1)
        {
        }

        [Constructable]
        public CopperOre(int amount)
            : base(CraftResource.Copper, amount)
        {
        }

        public CopperOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new CopperIngot();
        }

    }

    public class BronzeOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Bronze; } }

        [Constructable]
        public BronzeOre()
            : this(1)
        {
        }

        [Constructable]
        public BronzeOre(int amount)
            : base(CraftResource.Bronze, amount)
        {
        }

        public BronzeOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new BronzeIngot();
        }

    }

    public class GoldOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Gold; } }

        [Constructable]
        public GoldOre()
            : this(1)
        {
        }

        [Constructable]
        public GoldOre(int amount)
            : base(CraftResource.Gold, amount)
        {
        }

        public GoldOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new GoldIngot();
        }
    }

    public class AgapiteOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Agapite; } }

        [Constructable]
        public AgapiteOre()
            : this(1)
        {
        }

        [Constructable]
        public AgapiteOre(int amount)
            : base(CraftResource.Agapite, amount)
        {
        }

        public AgapiteOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new AgapiteIngot();
        }
    }

    public class VeriteOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Verite; } }

        [Constructable]
        public VeriteOre()
            : this(1)
        {
        }

        [Constructable]
        public VeriteOre(int amount)
            : base(CraftResource.Verite, amount)
        {
        }

        public VeriteOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new VeriteIngot();
        }
    }

    public class ValoriteOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Valorite; } }

        [Constructable]
        public ValoriteOre()
            : this(1)
        {
        }

        [Constructable]
        public ValoriteOre(int amount)
            : base(CraftResource.Valorite, amount)
        {
        }

        public ValoriteOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new ValoriteIngot();
        }
    }

    public class ZuluOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Zulu; } }

        [Constructable]
        public ZuluOre()
            : this(1)
        {
        }

        [Constructable]
        public ZuluOre(int amount)
            : base(CraftResource.Zulu, amount)
        {
        }

        public ZuluOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new ZuluIngot();
        }


    }

    public class OnyxOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Onyx; } }

        [Constructable]
        public OnyxOre()
            : this(1)
        {
        }

        [Constructable]
        public OnyxOre(int amount)
            : base(CraftResource.Onyx, amount)
        {
        }

        public OnyxOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new OnyxIngot();
        }


    }

    public class PyriteOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Pyrite; } }

        [Constructable]
        public PyriteOre()
            : this(1)
        {
        }

        [Constructable]
        public PyriteOre(int amount)
            : base(CraftResource.Pyrite, amount)
        {
        }

        public PyriteOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new PyriteIngot();
        }

    }

    public class MalachiteOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Malachite; } }

        [Constructable]
        public MalachiteOre()
            : this(1)
        {
        }

        [Constructable]
        public MalachiteOre(int amount)
            : base(CraftResource.Malachite, amount)
        {
        }

        public MalachiteOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new MalachiteIngot();
        }

    }

    public class AzuriteOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Azurite; } }

        [Constructable]
        public AzuriteOre()
            : this(1)
        {
        }

        [Constructable]
        public AzuriteOre(int amount)
            : base(CraftResource.Azurite, amount)
        {
        }

        public AzuriteOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new AzuriteIngot();
        }

    }

    public class PlatinumOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Platinum; } }

        [Constructable]
        public PlatinumOre()
            : this(1)
        {
        }

        [Constructable]
        public PlatinumOre(int amount)
            : base(CraftResource.Platinum, amount)
        {
        }

        public PlatinumOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new PlatinumIngot();
        }

    }

    public class LavarockOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Lavarock; } }

        [Constructable]
        public LavarockOre()
            : this(1)
        {
        }

        [Constructable]
        public LavarockOre(int amount)
            : base(CraftResource.Lavarock, amount)
        {
        }

        public LavarockOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new LavarockIngot();
        }

    }

    public class MysticOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Mystic; } }

        [Constructable]
        public MysticOre()
            : this(1)
        {
        }

        [Constructable]
        public MysticOre(int amount)
            : base(CraftResource.Mystic, amount)
        {
        }

        public MysticOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new MysticIngot();
        }

    }

    public class SpikeOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Spike; } }

        [Constructable]
        public SpikeOre()
            : this(1)
        {
        }

        [Constructable]
        public SpikeOre(int amount)
            : base(CraftResource.Spike, amount)
        {
        }

        public SpikeOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new SpikeIngot();
        }

    }

    public class FruityOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Fruity; } }

        [Constructable]
        public FruityOre()
            : this(1)
        {
        }

        [Constructable]
        public FruityOre(int amount)
            : base(CraftResource.Fruity, amount)
        {
        }

        public FruityOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new FruityIngot();
        }

    }

    public class IceRockOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.IceRock; } }

        [Constructable]
        public IceRockOre()
            : this(1)
        {
        }

        [Constructable]
        public IceRockOre(int amount)
            : base(CraftResource.IceRock, amount)
        {
        }

        public IceRockOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new IceRockIngot();
        }

    }

    public class SilverRockOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SilverRock; } }

        [Constructable]
        public SilverRockOre()
            : this(1)
        {
        }

        [Constructable]
        public SilverRockOre(int amount)
            : base(CraftResource.SilverRock, amount)
        {
        }

        public SilverRockOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new SilverRockIngot();
        }

    }

    public class SpectralOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Spectral; } }

        [Constructable]
        public SpectralOre()
            : this(1)
        {
        }

        [Constructable]
        public SpectralOre(int amount)
            : base(CraftResource.Spectral, amount)
        {
        }

        public SpectralOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new SpectralIngot();
        }

    }

    public class UndeadOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Undead; } }

        [Constructable]
        public UndeadOre()
            : this(1)
        {
        }

        [Constructable]
        public UndeadOre(int amount)
            : base(CraftResource.Undead, amount)
        {
        }

        public UndeadOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new UndeadIngot();
        }

    }

    public class DarkPaganOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DarkPagan; } }

        [Constructable]
        public DarkPaganOre()
            : this(1)
        {
        }

        [Constructable]
        public DarkPaganOre(int amount)
            : base(CraftResource.DarkPagan, amount)
        {
        }

        public DarkPaganOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DarkPaganIngot();
        }

    }

    public class OldBritainOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.OldBritain; } }

        [Constructable]
        public OldBritainOre()
            : this(1)
        {
        }

        [Constructable]
        public OldBritainOre(int amount)
            : base(CraftResource.OldBritain, amount)
        {
        }

        public OldBritainOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new OldBritainIngot();
        }

    }

    public class VirginityOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Virginity; } }

        [Constructable]
        public VirginityOre()
            : this(1)
        {
        }

        [Constructable]
        public VirginityOre(int amount)
            : base(CraftResource.Virginity, amount)
        {
        }

        public VirginityOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new VirginityIngot();
        }

    }

    public class BlackDwarfOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BlackDwarf; } }

        [Constructable]
        public BlackDwarfOre()
            : this(1)
        {
        }

        [Constructable]
        public BlackDwarfOre(int amount)
            : base(CraftResource.BlackDwarf, amount)
        {
        }

        public BlackDwarfOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new BlackDwarfIngot();
        }

    }

    public class RedElvenOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.RedElven; } }

        [Constructable]
        public RedElvenOre()
            : this(1)
        {
        }

        [Constructable]
        public RedElvenOre(int amount)
            : base(CraftResource.RedElven, amount)
        {
        }

        public RedElvenOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new RedElvenIngot();
        }

    }

    public class DripstoneOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Dripstone; } }

        [Constructable]
        public DripstoneOre()
            : this(1)
        {
        }

        [Constructable]
        public DripstoneOre(int amount)
            : base(CraftResource.Dripstone, amount)
        {
        }

        public DripstoneOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DripstoneIngot();
        }

    }

    public class ExecutorOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Executor; } }

        [Constructable]
        public ExecutorOre()
            : this(1)
        {
        }

        [Constructable]
        public ExecutorOre(int amount)
            : base(CraftResource.Executor, amount)
        {
        }

        public ExecutorOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new ExecutorIngot();
        }

    }

    public class PeachblueOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Peachblue; } }

        [Constructable]
        public PeachblueOre()
            : this(1)
        {
        }

        [Constructable]
        public PeachblueOre(int amount)
            : base(CraftResource.Peachblue, amount)
        {
        }

        public PeachblueOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new PeachblueIngot();
        }

    }

    public class DestructionOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Destruction; } }

        [Constructable]
        public DestructionOre()
            : this(1)
        {
        }

        [Constructable]
        public DestructionOre(int amount)
            : base(CraftResource.Destruction, amount)
        {
        }

        public DestructionOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DestructionIngot();
        }

    }

    public class AnraOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Anra; } }

        [Constructable]
        public AnraOre()
            : this(1)
        {
        }

        [Constructable]
        public AnraOre(int amount)
            : base(CraftResource.Anra, amount)
        {
        }

        public AnraOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new AnraIngot();
        }

    }

    public class GoddessOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Goddess; } }

        [Constructable]
        public GoddessOre()
            : this(1)
        {
        }

        [Constructable]
        public GoddessOre(int amount)
            : base(CraftResource.Goddess, amount)
        {
        }

        public GoddessOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new GoddessIngot();
        }

    }

    public class CrystalOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Crystal; } }

        [Constructable]
        public CrystalOre()
            : this(1)
        {
        }

        [Constructable]
        public CrystalOre(int amount)
            : base(CraftResource.Crystal, amount)
        {
        }

        public CrystalOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new CrystalIngot();
        }

    }

    public class DoomOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Doom; } }

        [Constructable]
        public DoomOre()
            : this(1)
        {
        }

        [Constructable]
        public DoomOre(int amount)
            : base(CraftResource.Doom, amount)
        {
        }

        public DoomOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DoomIngot();
        }

    }

    public class ETSOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.ETS; } }

        [Constructable]
        public ETSOre()
            : this(1)
        {
        }

        [Constructable]
        public ETSOre(int amount)
            : base(CraftResource.ETS, amount)
        {
            this.ItemID = 3885;
        }

        public ETSOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new ETSIngot();
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = CraftResources.GetInfo(this.Resource).Name;
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name);
            else
                list.Add(name);
        }
    }

    public class DSROre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DSR; } }

        [Constructable]
        public DSROre()
            : this(1)
        {
        }

        [Constructable]
        public DSROre(int amount)
            : base(CraftResource.DSR, amount)
        {
            this.ItemID = 3877;
        }

        public DSROre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DSRIngot();
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = CraftResources.GetInfo(this.Resource).Name;
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name);
            else
                list.Add(name);
        }
    }

    public class RNDOre : BaseOre
    {
        protected override CraftResource DefaultResource { get { return CraftResource.RND; } }

        [Constructable]
        public RNDOre()
            : this(1)
        {
        }

        [Constructable]
        public RNDOre(int amount)
            : base(CraftResource.RND, amount)
        {
            this.ItemID = 3873;
        }

        public RNDOre(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new RNDIngot();
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = CraftResources.GetInfo(this.Resource).Name;
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name);
            else
                list.Add(name);
        }
    }

}