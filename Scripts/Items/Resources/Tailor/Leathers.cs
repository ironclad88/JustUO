using System;

namespace Server.Items
{
    public abstract class BaseLeather : Item, ICommodity
    {
        protected virtual CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

        private CraftResource m_Resource;
        public BaseLeather(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseLeather(CraftResource resource, int amount)
            : base(0x1081)
        {
            this.Stackable = true;
            this.Weight = 1.0;
            this.Amount = amount;
            this.Hue = CraftResources.GetHue(resource);

            this.m_Resource = resource;
        }

        public BaseLeather(Serial serial)
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
        public override int LabelNumber
        {
            get
            {
                if (this.m_Resource >= CraftResource.SpinedLeather && this.m_Resource <= CraftResource.DragonLeather)
                {
                    return 1049684 + (int)(this.m_Resource - CraftResource.DragonLeather);
                }
                if (this.m_Resource == CraftResource.DragonLeather)
                {
                    return 1063505;
                }
                return 1047022;
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
                        OreInfo info = new OreInfo(reader.ReadInt(), reader.ReadInt(), reader.ReadString());

                        this.m_Resource = CraftResources.GetFromOreInfo(info);
                        break;
                    }
            }
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = CraftResources.GetInfo(this.Resource).Name + " Leather";
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name);
            else
                list.Add(name);
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class Leather : BaseLeather
    {
        [Constructable]
        public Leather()
            : this(1)
        {
        }

        [Constructable]
        public Leather(int amount)
            : base(CraftResource.RegularLeather, amount)
        {
        }

        public Leather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class SpinedLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SpinedLeather; } }

        [Constructable]
        public SpinedLeather()
            : this(1)
        {
        }

        [Constructable]
        public SpinedLeather(int amount)
            : base(CraftResource.SpinedLeather, amount)
        {
        }

        public SpinedLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class HornedLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.HornedLeather; } }

        [Constructable]
        public HornedLeather()
            : this(1)
        {
        }

        [Constructable]
        public HornedLeather(int amount)
            : base(CraftResource.HornedLeather, amount)
        {
        }

        public HornedLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class BarbedLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BarbedLeather; } }

        [Constructable]
        public BarbedLeather()
            : this(1)
        {
        }

        [Constructable]
        public BarbedLeather(int amount)
            : base(CraftResource.BarbedLeather, amount)
        {
        }

        public BarbedLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class DragonLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DragonLeather; } }

        [Constructable]
        public DragonLeather()
            : this(1)
        {
        }

        [Constructable]
        public DragonLeather(int amount)
            : base(CraftResource.DragonLeather, amount)
        {
        }

        public DragonLeather(Serial serial)
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


    [FlipableAttribute(0x1081, 0x1082)]
    public class RatLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.RatLeather; } }

        [Constructable]
        public RatLeather()
            : this(1)
        {
        }

        [Constructable]
        public RatLeather(int amount)
            : base(CraftResource.RatLeather, amount)
        {
        }

        public RatLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class WolfLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.WolfLeather; } }

        [Constructable]
        public WolfLeather()
            : this(1)
        {
        }

        [Constructable]
        public WolfLeather(int amount)
            : base(CraftResource.WolfLeather, amount)
        {
        }

        public WolfLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class BearLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BearLeather; } }

        [Constructable]
        public BearLeather()
            : this(1)
        {
        }

        [Constructable]
        public BearLeather(int amount)
            : base(CraftResource.BearLeather, amount)
        {
        }

        public BearLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class OrcLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.OrcLeather; } }

        [Constructable]
        public OrcLeather()
            : this(1)
        {
        }

        [Constructable]
        public OrcLeather(int amount)
            : base(CraftResource.OrcLeather, amount)
        {
        }

        public OrcLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class SerpentLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SerpentLeather; } }

        [Constructable]
        public SerpentLeather()
            : this(1)
        {
        }

        [Constructable]
        public SerpentLeather(int amount)
            : base(CraftResource.SerpentLeather, amount)
        {
        }

        public SerpentLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class LizardLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.LizardLeather; } }

        [Constructable]
        public LizardLeather()
            : this(1)
        {
        }

        [Constructable]
        public LizardLeather(int amount)
            : base(CraftResource.LizardLeather, amount)
        {
        }

        public LizardLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class TrollLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.TrollLeather; } }

        [Constructable]
        public TrollLeather()
            : this(1)
        {
        }

        [Constructable]
        public TrollLeather(int amount)
            : base(CraftResource.TrollLeather, amount)
        {
        }

        public TrollLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class OstardLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.OstardLeather; } }

        [Constructable]
        public OstardLeather()
            : this(1)
        {
        }

        [Constructable]
        public OstardLeather(int amount)
            : base(CraftResource.OstardLeather, amount)
        {
        }

        public OstardLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class NecromancerLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.NecromancerLeather; } }

        [Constructable]
        public NecromancerLeather()
            : this(1)
        {
        }

        [Constructable]
        public NecromancerLeather(int amount)
            : base(CraftResource.NecromancerLeather, amount)
        {
        }

        public NecromancerLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class TerathanLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.TerathanLeather; } }

        [Constructable]
        public TerathanLeather()
            : this(1)
        {
        }

        [Constructable]
        public TerathanLeather(int amount)
            : base(CraftResource.TerathanLeather, amount)
        {
        }

        public TerathanLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class LavaLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.LavaLeather; } }

        [Constructable]
        public LavaLeather()
            : this(1)
        {
        }

        [Constructable]
        public LavaLeather(int amount)
            : base(CraftResource.LavaLeather, amount)
        {
        }

        public LavaLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class LicheLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.LicheLeather; } }

        [Constructable]
        public LicheLeather()
            : this(1)
        {
        }

        [Constructable]
        public LicheLeather(int amount)
            : base(CraftResource.LicheLeather, amount)
        {
        }

        public LicheLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class DaemonLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DaemonLeather; } }

        [Constructable]
        public DaemonLeather()
            : this(1)
        {
        }

        [Constructable]
        public DaemonLeather(int amount)
            : base(CraftResource.DaemonLeather, amount)
        {
        }

        public DaemonLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class IceCrystalLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.IceCrystalLeather; } }

        [Constructable]
        public IceCrystalLeather()
            : this(1)
        {
        }

        [Constructable]
        public IceCrystalLeather(int amount)
            : base(CraftResource.IceCrystalLeather, amount)
        {
        }

        public IceCrystalLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class WyrmLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.WyrmLeather; } }

        [Constructable]
        public WyrmLeather()
            : this(1)
        {
        }

        [Constructable]
        public WyrmLeather(int amount)
            : base(CraftResource.WyrmLeather, amount)
        {
        }

        public WyrmLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class BalronLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BalronLeather; } }

        [Constructable]
        public BalronLeather()
            : this(1)
        {
        }

        [Constructable]
        public BalronLeather(int amount)
            : base(CraftResource.BalronLeather, amount)
        {
        }

        public BalronLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class GoldenDragonLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.GoldenDragonLeather; } }

        [Constructable]
        public GoldenDragonLeather()
            : this(1)
        {
        }

        [Constructable]
        public GoldenDragonLeather(int amount)
            : base(CraftResource.GoldenDragonLeather, amount)
        {
        }

        public GoldenDragonLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class SilverDragonLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SilverDragonLeather; } }

        [Constructable]
        public SilverDragonLeather()
            : this(1)
        {
        }

        [Constructable]
        public SilverDragonLeather(int amount)
            : base(CraftResource.SilverDragonLeather, amount)
        {
        }

        public SilverDragonLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class BalrogLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BalrogLeather; } }

        [Constructable]
        public BalrogLeather()
            : this(1)
        {
        }

        [Constructable]
        public BalrogLeather(int amount)
            : base(CraftResource.BalrogLeather, amount)
        {
        }

        public BalrogLeather(Serial serial)
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

    [FlipableAttribute(0x1081, 0x1082)]
    public class AngelLeather : BaseLeather
    {
        protected override CraftResource DefaultResource { get { return CraftResource.AngelLeather; } }

        [Constructable]
        public AngelLeather()
            : this(1)
        {
        }

        [Constructable]
        public AngelLeather(int amount)
            : base(CraftResource.AngelLeather, amount)
        {
        }

        public AngelLeather(Serial serial)
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