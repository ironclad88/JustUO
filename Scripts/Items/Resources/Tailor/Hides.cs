using System;

namespace Server.Items
{
    public abstract class BaseHides : Item, ICommodity
    {
        protected virtual CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

        private CraftResource m_Resource;
        public BaseHides(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseHides(CraftResource resource, int amount)
            : base(0x1079)
        {
            this.Stackable = true;
            this.Weight = 5.0;
            this.Amount = amount;
            this.Hue = CraftResources.GetHue(resource);

            this.m_Resource = resource;
        }

        public BaseHides(Serial serial)
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
                if (this.m_Resource >= CraftResource.SpinedLeather && this.m_Resource <= CraftResource.BarbedLeather)
                    return 1049687 + (int)(this.m_Resource - CraftResource.SpinedLeather);
                if (this.m_Resource == CraftResource.DragonLeather) { return 1063506; }


                return 1047023;
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
            string name = CraftResources.GetInfo(this.Resource).Name;
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name + " Hides");
            else
                list.Add(name + " Hide");
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

    [FlipableAttribute(0x1079, 0x1078)]
    public class Hides : BaseHides, IScissorable
    {
        [Constructable]
        public Hides()
            : this(1)
        {
        }

        [Constructable]
        public Hides(int amount)
            : base(CraftResource.RegularLeather, amount)
        {
        }

        public Hides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }
            base.ScissorHelper(from, new Leather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class SpinedHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SpinedLeather; } }

        [Constructable]
        public SpinedHides()
            : this(1)
        {
        }

        [Constructable]
        public SpinedHides(int amount)
            : base(CraftResource.SpinedLeather, amount)
        {
        }

        public SpinedHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new SpinedLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class HornedHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.HornedLeather; } }

        [Constructable]
        public HornedHides()
            : this(1)
        {
        }

        [Constructable]
        public HornedHides(int amount)
            : base(CraftResource.HornedLeather, amount)
        {
        }

        public HornedHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new HornedLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class BarbedHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BarbedLeather; } }

        [Constructable]
        public BarbedHides()
            : this(1)
        {
        }

        [Constructable]
        public BarbedHides(int amount)
            : base(CraftResource.BarbedLeather, amount)
        {
        }

        public BarbedHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new BarbedLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class DragonHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DragonLeather; } }

        [Constructable]
        public DragonHides()
            : this(1)
        {
        }

        [Constructable]
        public DragonHides(int amount)
            : base(CraftResource.DragonLeather, amount)
        {
        }

        public DragonHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class RatHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.RatLeather; } }

        [Constructable]
        public RatHides()
            : this(1)
        {
        }

        [Constructable]
        public RatHides(int amount)
            : base(CraftResource.RatLeather, amount)
        {
        }

        public RatHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class WolfHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.WolfLeather; } }

        [Constructable]
        public WolfHides()
            : this(1)
        {
        }

        [Constructable]
        public WolfHides(int amount)
            : base(CraftResource.WolfLeather, amount)
        {
        }

        public WolfHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class BearHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BearLeather; } }

        [Constructable]
        public BearHides()
            : this(1)
        {
        }

        [Constructable]
        public BearHides(int amount)
            : base(CraftResource.BearLeather, amount)
        {
        }

        public BearHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class OrcHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.OrcLeather; } }

        [Constructable]
        public OrcHides()
            : this(1)
        {
        }

        [Constructable]
        public OrcHides(int amount)
            : base(CraftResource.OrcLeather, amount)
        {
        }

        public OrcHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class SerpentHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SerpentLeather; } }

        [Constructable]
        public SerpentHides()
            : this(1)
        {
        }

        [Constructable]
        public SerpentHides(int amount)
            : base(CraftResource.SerpentLeather, amount)
        {
        }

        public SerpentHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class LizardHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.LizardLeather; } }

        [Constructable]
        public LizardHides()
            : this(1)
        {
        }

        [Constructable]
        public LizardHides(int amount)
            : base(CraftResource.LizardLeather, amount)
        {
        }

        public LizardHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class TrollHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.TrollLeather; } }

        [Constructable]
        public TrollHides()
            : this(1)
        {
        }

        [Constructable]
        public TrollHides(int amount)
            : base(CraftResource.TrollLeather, amount)
        {
        }

        public TrollHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class OstardHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.OstardLeather; } }

        [Constructable]
        public OstardHides()
            : this(1)
        {
        }

        [Constructable]
        public OstardHides(int amount)
            : base(CraftResource.OstardLeather, amount)
        {
        }

        public OstardHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class NecromancerHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.NecromancerLeather; } }

        [Constructable]
        public NecromancerHides()
            : this(1)
        {
        }

        [Constructable]
        public NecromancerHides(int amount)
            : base(CraftResource.NecromancerLeather, amount)
        {
        }

        public NecromancerHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class TerathanHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.TerathanLeather; } }

        [Constructable]
        public TerathanHides()
            : this(1)
        {
        }

        [Constructable]
        public TerathanHides(int amount)
            : base(CraftResource.TerathanLeather, amount)
        {
        }

        public TerathanHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class LavaHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.LavaLeather; } }

        [Constructable]
        public LavaHides()
            : this(1)
        {
        }

        [Constructable]
        public LavaHides(int amount)
            : base(CraftResource.LavaLeather, amount)
        {
        }

        public LavaHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class LicheHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.LicheLeather; } }

        [Constructable]
        public LicheHides()
            : this(1)
        {
        }

        [Constructable]
        public LicheHides(int amount)
            : base(CraftResource.LicheLeather, amount)
        {
        }

        public LicheHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class DaemonHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.DaemonLeather; } }

        [Constructable]
        public DaemonHides()
            : this(1)
        {
        }

        [Constructable]
        public DaemonHides(int amount)
            : base(CraftResource.DaemonLeather, amount)
        {
        }

        public DaemonHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class IceCrystalHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.IceCrystalLeather; } }

        [Constructable]
        public IceCrystalHides()
            : this(1)
        {
        }

        [Constructable]
        public IceCrystalHides(int amount)
            : base(CraftResource.IceCrystalLeather, amount)
        {
        }

        public IceCrystalHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class WyrmHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.WyrmLeather; } }

        [Constructable]
        public WyrmHides()
            : this(1)
        {
        }

        [Constructable]
        public WyrmHides(int amount)
            : base(CraftResource.WyrmLeather, amount)
        {
        }

        public WyrmHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class BalronHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BalronLeather; } }

        [Constructable]
        public BalronHides()
            : this(1)
        {
        }

        [Constructable]
        public BalronHides(int amount)
            : base(CraftResource.BalronLeather, amount)
        {
        }

        public BalronHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class GoldenDragonHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.GoldenDragonLeather; } }

        [Constructable]
        public GoldenDragonHides()
            : this(1)
        {
        }

        [Constructable]
        public GoldenDragonHides(int amount)
            : base(CraftResource.GoldenDragonLeather, amount)
        {
        }

        public GoldenDragonHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class SilverDragonHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.SilverDragonLeather; } }

        [Constructable]
        public SilverDragonHides()
            : this(1)
        {
        }

        [Constructable]
        public SilverDragonHides(int amount)
            : base(CraftResource.SilverDragonLeather, amount)
        {
        }

        public SilverDragonHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class BalrogHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.BalrogLeather; } }

        [Constructable]
        public BalrogHides()
            : this(1)
        {
        }

        [Constructable]
        public BalrogHides(int amount)
            : base(CraftResource.BalrogLeather, amount)
        {
        }

        public BalrogHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class AngelHides : BaseHides, IScissorable
    {
        protected override CraftResource DefaultResource { get { return CraftResource.AngelLeather; } }

        [Constructable]
        public AngelHides()
            : this(1)
        {
        }

        [Constructable]
        public AngelHides(int amount)
            : base(CraftResource.AngelLeather, amount)
        {
        }

        public AngelHides(Serial serial)
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            if (Core.AOS && !this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 1);

            return true;
        }
    }
}