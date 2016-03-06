using System;

namespace Server.Items
{
    public abstract class BaseIngot : Item, ICommodity
    {
        protected virtual CraftResource DefaultResource { get { return CraftResource.Iron; } }

        private CraftResource m_Resource;
        public BaseIngot(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseIngot(CraftResource resource, int amount)
            : base(0x1BF2)
        {
            this.Stackable = true;
            this.Amount = amount;
            this.Hue = CraftResources.GetHue(resource);

            this.m_Resource = resource;
        }

        public BaseIngot(Serial serial)
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
                this.InvalidateProperties();
            }
        }
        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }
        public override int LabelNumber
        {
            get
            {

                if (this.m_Resource >= CraftResource.DullCopper && this.m_Resource <= CraftResource.RND)
                {
                    if (this.m_Resource == CraftResource.ZuluMetal) { return 1063272; };
                    if (this.m_Resource == CraftResource.Onyx) { return 1063273; };
                    if (this.m_Resource == CraftResource.Pyrite) { return 1063274; };
                    if (this.m_Resource == CraftResource.Malachite) { return 1063275; };
                    if (this.m_Resource == CraftResource.Azurite) { return 1063276; };
                    if (this.m_Resource == CraftResource.Platinum) { return 1063277; };
                    if (this.m_Resource == CraftResource.Lavarock) { return 1063278; };
                    if (this.m_Resource == CraftResource.Mystic) { return 1063287; };
                    if (this.m_Resource == CraftResource.Spike) { return 1063288; };
                    if (this.m_Resource == CraftResource.Fruity) { return 1063289; };
                    if (this.m_Resource == CraftResource.IceRock) { return 1063290; };
                    if (this.m_Resource == CraftResource.SilverRock) { return 1063291; };
                    if (this.m_Resource == CraftResource.Spectral) { return 1063292; };
                    if (this.m_Resource == CraftResource.Undead) { return 1063293; };
                    if (this.m_Resource == CraftResource.DarkPagan) { return 1063294; };
                    if (this.m_Resource == CraftResource.OldBritain) { return 1063490; };
                    if (this.m_Resource == CraftResource.Virginity) { return 1063491; };
                    if (this.m_Resource == CraftResource.BlackDwarf) { return 1063492; };
                    if (this.m_Resource == CraftResource.RedElven) { return 1063493; };
                    if (this.m_Resource == CraftResource.Dripstone) { return 1063494; };
                    if (this.m_Resource == CraftResource.Executor) { return 1063495; };
                    if (this.m_Resource == CraftResource.Peachblue) { return 1063496; };
                    if (this.m_Resource == CraftResource.Destruction) { return 1063497; };
                    if (this.m_Resource == CraftResource.Anra) { return 1063498; };
                    if (this.m_Resource == CraftResource.GoddessMetal) { return 1063499; };
                    if (this.m_Resource == CraftResource.CrystalMetal) { return 1063500; };
                    if (this.m_Resource == CraftResource.DoomMetal) { return 1063501; };
                    if (this.m_Resource == CraftResource.ETS) { return 1063502; };
                    if (this.m_Resource == CraftResource.DSR) { return 1063503; };
                    if (this.m_Resource == CraftResource.RND) { return 1063504; };
                    return 1042684 + (int)(this.m_Resource - CraftResource.DullCopper);
                }

                return 1042692;
            }
        }
        int ICommodity.DescriptionNumber
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return true;
            }
        }
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
                                info = OreInfo.Platinum;
                                break;
                            case 14:
                                info = OreInfo.LavaRock;
                                break;
                            case 15:
                                info = OreInfo.Mystic;
                                break;
                            case 16:
                                info = OreInfo.Spike;
                                break;
                            case 17:
                                info = OreInfo.Fruity;
                                break;
                            case 18:
                                info = OreInfo.IceRock;
                                break;
                            case 19:
                                info = OreInfo.SilverRock;
                                break;
                            case 20:
                                info = OreInfo.Spectral;
                                break;
                            case 21:
                                info = OreInfo.Undead;
                                break;
                            case 22:
                                info = OreInfo.DarkPagan;
                                break;
                            case 23:
                                info = OreInfo.OldBritain;
                                break;
                            case 24:
                                info = OreInfo.Virginity;
                                break;
                            case 25:
                                info = OreInfo.BlackDwarf;
                                break;
                            case 26:
                                info = OreInfo.RedElven;
                                break;
                            case 27:
                                info = OreInfo.Dripstone;
                                break;
                            case 28:
                                info = OreInfo.Executor;
                                break;
                            case 29:
                                info = OreInfo.Peachblue;
                                break;
                            case 30:
                                info = OreInfo.Destruction;
                                break;
                            case 31:
                                info = OreInfo.Anra;
                                break;
                            case 32:
                                info = OreInfo.Goddess;
                                break;
                            case 33:
                                info = OreInfo.Crystal;
                                break;
                            case 34:
                                info = OreInfo.Doom;
                                break;
                            case 35:
                                info = OreInfo.ETS;
                                break;
                            case 36:
                                info = OreInfo.DSR;
                                break;
                            case 37:
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

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = CraftResources.GetInfo(this.Resource).Name;
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name + " Ingots");
            else
                list.Add(name + " Ingot");
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class IronIngot : BaseIngot
    {
        [Constructable]
        public IronIngot()
            : this(1)
        {
        }

        [Constructable]
        public IronIngot(int amount)
            : base(CraftResource.Iron, amount)
        {
        }

        public IronIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DullCopperIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DullCopper; } }

        [Constructable]
        public DullCopperIngot()
            : this(1)
        {
        }

        [Constructable]
        public DullCopperIngot(int amount)
            : base(CraftResource.DullCopper, amount)
        {
        }

        public DullCopperIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ShadowIronIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.ShadowIron; } }

        [Constructable]
        public ShadowIronIngot()
            : this(1)
        {
        }

        [Constructable]
        public ShadowIronIngot(int amount)
            : base(CraftResource.ShadowIron, amount)
        {
        }

        public ShadowIronIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class CopperIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Copper; } }

        [Constructable]
        public CopperIngot()
            : this(1)
        {
        }

        [Constructable]
        public CopperIngot(int amount)
            : base(CraftResource.Copper, amount)
        {
        }

        public CopperIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class BronzeIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Bronze; } }

        [Constructable]
        public BronzeIngot()
            : this(1)
        {
        }

        [Constructable]
        public BronzeIngot(int amount)
            : base(CraftResource.Bronze, amount)
        {
        }

        public BronzeIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class GoldIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Gold; } }

        [Constructable]
        public GoldIngot()
            : this(1)
        {
        }

        [Constructable]
        public GoldIngot(int amount)
            : base(CraftResource.Gold, amount)
        {
        }

        public GoldIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class AgapiteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Agapite; } }

        [Constructable]
        public AgapiteIngot()
            : this(1)
        {
        }

        [Constructable]
        public AgapiteIngot(int amount)
            : base(CraftResource.Agapite, amount)
        {
        }

        public AgapiteIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class VeriteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Verite; } }

        [Constructable]
        public VeriteIngot()
            : this(1)
        {
        }

        [Constructable]
        public VeriteIngot(int amount)
            : base(CraftResource.Verite, amount)
        {
        }

        public VeriteIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ValoriteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Valorite; } }

        [Constructable]
        public ValoriteIngot()
            : this(1)
        {
        }

        [Constructable]
        public ValoriteIngot(int amount)
            : base(CraftResource.Valorite, amount)
        {
        }

        public ValoriteIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ZuluIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.ZuluMetal; } }

        [Constructable]
        public ZuluIngot()
            : this(1)
        {
        }

        [Constructable]
        public ZuluIngot(int amount)
            : base(CraftResource.ZuluMetal, amount)
        {
        }

        public ZuluIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class OnyxIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Onyx; } }

        [Constructable]
        public OnyxIngot()
            : this(1)
        {
        }

        [Constructable]
        public OnyxIngot(int amount)
            : base(CraftResource.Onyx, amount)
        {
        }

        public OnyxIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class RedElvenIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.RedElven; } }

        [Constructable]
        public RedElvenIngot()
            : this(1)
        {
        }

        [Constructable]
        public RedElvenIngot(int amount)
            : base(CraftResource.RedElven, amount)
        {
        }

        public RedElvenIngot(Serial serial)
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
    }


    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class PyriteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Pyrite; } }

        [Constructable]
        public PyriteIngot()
            : this(1)
        {
        }

        [Constructable]
        public PyriteIngot(int amount)
            : base(CraftResource.Pyrite, amount)
        {
        }

        public PyriteIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class MalachiteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Malachite; } }

        [Constructable]
        public MalachiteIngot()
            : this(1)
        {
        }

        [Constructable]
        public MalachiteIngot(int amount)
            : base(CraftResource.Malachite, amount)
        {
        }

        public MalachiteIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class AzuriteIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Azurite; } }

        [Constructable]
        public AzuriteIngot()
            : this(1)
        {
        }

        [Constructable]
        public AzuriteIngot(int amount)
            : base(CraftResource.Azurite, amount)
        {
        }

        public AzuriteIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class PlatinumIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Platinum; } }

        [Constructable]
        public PlatinumIngot()
            : this(1)
        {
        }

        [Constructable]
        public PlatinumIngot(int amount)
            : base(CraftResource.Platinum, amount)
        {
        }

        public PlatinumIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class LavarockIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Lavarock; } }

        [Constructable]
        public LavarockIngot()
            : this(1)
        {
        }

        [Constructable]
        public LavarockIngot(int amount)
            : base(CraftResource.Lavarock, amount)
        {
        }

        public LavarockIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class MysticIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Mystic; } }

        [Constructable]
        public MysticIngot()
            : this(1)
        {
        }

        [Constructable]
        public MysticIngot(int amount)
            : base(CraftResource.Mystic, amount)
        {
        }

        public MysticIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class SpikeIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Spike; } }

        [Constructable]
        public SpikeIngot()
            : this(1)
        {
        }

        [Constructable]
        public SpikeIngot(int amount)
            : base(CraftResource.Spike, amount)
        {
        }

        public SpikeIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class FruityIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Fruity; } }

        [Constructable]
        public FruityIngot()
            : this(1)
        {
        }

        [Constructable]
        public FruityIngot(int amount)
            : base(CraftResource.Fruity, amount)
        {
        }

        public FruityIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class IceRockIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.IceRock; } }

        [Constructable]
        public IceRockIngot()
            : this(1)
        {
        }

        [Constructable]
        public IceRockIngot(int amount)
            : base(CraftResource.IceRock, amount)
        {
        }

        public IceRockIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class SilverRockIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SilverRock; } }

        [Constructable]
        public SilverRockIngot()
            : this(1)
        {
        }

        [Constructable]
        public SilverRockIngot(int amount)
            : base(CraftResource.SilverRock, amount)
        {
        }

        public SilverRockIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class SpectralIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Spectral; } }

        [Constructable]
        public SpectralIngot()
            : this(1)
        {
        }

        [Constructable]
        public SpectralIngot(int amount)
            : base(CraftResource.Spectral, amount)
        {
        }

        public SpectralIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class UndeadIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Undead; } }

        [Constructable]
        public UndeadIngot()
            : this(1)
        {
        }

        [Constructable]
        public UndeadIngot(int amount)
            : base(CraftResource.Undead, amount)
        {
        }

        public UndeadIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DarkPaganIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DarkPagan; } }

        [Constructable]
        public DarkPaganIngot()
            : this(1)
        {
        }

        [Constructable]
        public DarkPaganIngot(int amount)
            : base(CraftResource.DarkPagan, amount)
        {
        }

        public DarkPaganIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class OldBritainIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.OldBritain; } }

        [Constructable]
        public OldBritainIngot()
            : this(1)
        {
        }

        [Constructable]
        public OldBritainIngot(int amount)
            : base(CraftResource.OldBritain, amount)
        {
        }

        public OldBritainIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class VirginityIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Virginity; } }

        [Constructable]
        public VirginityIngot()
            : this(1)
        {
        }

        [Constructable]
        public VirginityIngot(int amount)
            : base(CraftResource.Virginity, amount)
        {
        }

        public VirginityIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class BlackDwarfIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BlackDwarf; } }

        [Constructable]
        public BlackDwarfIngot()
            : this(1)
        {
        }

        [Constructable]
        public BlackDwarfIngot(int amount)
            : base(CraftResource.BlackDwarf, amount)
        {
        }

        public BlackDwarfIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DripstoneIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Dripstone; } }

        [Constructable]
        public DripstoneIngot()
            : this(1)
        {
        }

        [Constructable]
        public DripstoneIngot(int amount)
            : base(CraftResource.Dripstone, amount)
        {
        }

        public DripstoneIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ExecutorIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Executor; } }

        [Constructable]
        public ExecutorIngot()
            : this(1)
        {
        }

        [Constructable]
        public ExecutorIngot(int amount)
            : base(CraftResource.Executor, amount)
        {
        }

        public ExecutorIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class PeachblueIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Peachblue; } }

        [Constructable]
        public PeachblueIngot()
            : this(1)
        {
        }

        [Constructable]
        public PeachblueIngot(int amount)
            : base(CraftResource.Peachblue, amount)
        {
        }

        public PeachblueIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DestructionIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Destruction; } }

        [Constructable]
        public DestructionIngot()
            : this(1)
        {
        }

        [Constructable]
        public DestructionIngot(int amount)
            : base(CraftResource.Destruction, amount)
        {
        }

        public DestructionIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class AnraIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.Anra; } }

        [Constructable]
        public AnraIngot()
            : this(1)
        {
        }

        [Constructable]
        public AnraIngot(int amount)
            : base(CraftResource.Anra, amount)
        {
        }

        public AnraIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class GoddessIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.GoddessMetal; } }

        [Constructable]
        public GoddessIngot()
            : this(1)
        {
        }

        [Constructable]
        public GoddessIngot(int amount)
            : base(CraftResource.GoddessMetal, amount)
        {
        }

        public GoddessIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class CrystalIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.CrystalMetal; } }

        [Constructable]
        public CrystalIngot()
            : this(1)
        {
        }

        [Constructable]
        public CrystalIngot(int amount)
            : base(CraftResource.CrystalMetal, amount)
        {
        }

        public CrystalIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DoomIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DoomMetal; } }

        [Constructable]
        public DoomIngot()
            : this(1)
        {
        }

        [Constructable]
        public DoomIngot(int amount)
            : base(CraftResource.DoomMetal, amount)
        {
        }

        public DoomIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ETSIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.ETS; } }

        [Constructable]
        public ETSIngot()
            : this(1)
        {
        }

        [Constructable]
        public ETSIngot(int amount)
            : base(CraftResource.ETS, amount)
        {
        }

        public ETSIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DSRIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DSR; } }

        [Constructable]
        public DSRIngot()
            : this(1)
        {
        }

        [Constructable]
        public DSRIngot(int amount)
            : base(CraftResource.DSR, amount)
        {
        }

        public DSRIngot(Serial serial)
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
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class RNDIngot : BaseIngot
    {
        protected override CraftResource DefaultResource { get { return CraftResource.RND; } }

        [Constructable]
        public RNDIngot()
            : this(1)
        {
        }

        [Constructable]
        public RNDIngot(int amount)
            : base(CraftResource.RND, amount)
        {
        }

        public RNDIngot(Serial serial)
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
    }
}