using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class BaseLog : Item, ICommodity, IAxe
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get { return m_Resource; }
			set { m_Resource = value; InvalidateProperties(); }
		}

		int ICommodity.DescriptionNumber {
            get
            {
                return CraftResources.IsStandard( m_Resource ) ? LabelNumber : 1075062 + ( (int)m_Resource - (int)CraftResource.RegularWood );
            }
        }
		bool ICommodity.IsDeedable { get { return true; } }
		[Constructable]
		public BaseLog() : this( 1 )
		{
		}

		[Constructable]
		public BaseLog( int amount ) : this( CraftResource.RegularWood, amount )
		{
		}

		[Constructable]
		public BaseLog( CraftResource resource )
			: this( resource, 1 )
		{
		}
		[Constructable]
		public BaseLog( CraftResource resource, int amount )
			: base( 0x1BDD )
		{
			Stackable = true;
			Weight = 2.0;
			Amount = amount;

			m_Resource = resource;
			Hue = CraftResources.GetHue( resource );
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = "";
            if (this.Resource != CraftResource.RegularWood)
            {
                // Only display name for special logs
                name = CraftResources.GetInfo(this.Resource).Name;
            }
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name + " Logs");
            else
                list.Add(name + " Log");
        }

        public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			//if ( !CraftResources.IsStandard( m_Resource ) )
			//{
			//	int num = CraftResources.GetLocalizationNumber( m_Resource );

			//	if ( num > 0 )
			//		list.Add( num );
			//	else
			//		list.Add( CraftResources.GetName( m_Resource ) );
			//}
		}
		public BaseLog( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (int)m_Resource );
		}

		public static bool UpdatingBaseLogClass;
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if (version == 1)
				UpdatingBaseLogClass = true;
			m_Resource = (CraftResource)reader.ReadInt();

			if ( version == 0 )
				m_Resource = CraftResource.RegularWood;
		}

		public virtual bool TryCreateBoards( Mobile from, double skill, Item item )
		{
			if ( Deleted || !from.CanSee( this ) ) 
				return false;
			else if ( from.Skills.Carpentry.Value < skill &&
				from.Skills.Lumberjacking.Value < skill )
			{
				item.Delete();
				from.SendLocalizedMessage( 1072652 ); // You cannot work this strange and unusual wood.
				return false;
			}
			base.ScissorHelper( from, item, 1, false );
			return true;
		}

		public virtual bool Axe( Mobile from, BaseAxe axe )
		{
            return false; // JustZH: disable boards...
			//if ( !TryCreateBoards( from , 0, new Board() ) )
			//	return false;
			
			//return true;
		}
	}

	public class Log : BaseLog
	{
		[Constructable]
		public Log()
			: this(1)
		{
		}

		[Constructable]
		public Log(int amount)
			: base(CraftResource.RegularWood, amount)
		{
		}

		public Log(Serial serial)
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
			//don't deserialize anything on update
			if (BaseLog.UpdatingBaseLogClass)
				return;

			int version = reader.ReadInt();
		}

		public override bool Axe(Mobile from, BaseAxe axe)
		{
			if (!TryCreateBoards(from, 95, new Board()))
				return false;

			return true;
		}
	}

    
public class HeartwoodLog : BaseLog
    {
        [Constructable]
        public HeartwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public HeartwoodLog(int amount)
            : base(CraftResource.Heartwood, amount)
        {
        }

        public HeartwoodLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!this.TryCreateBoards(from, 100, new HeartwoodBoard()))
                return false;

            return true;
        }
    }

    public class BloodwoodLog : BaseLog
    {
        [Constructable]
        public BloodwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public BloodwoodLog(int amount)
            : base(CraftResource.Bloodwood, amount)
        {
        }

        public BloodwoodLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!this.TryCreateBoards(from, 100, new BloodwoodBoard()))
                return false;

            return true;
        }
    }

    public class FrostwoodLog : BaseLog
    {
        [Constructable]
        public FrostwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public FrostwoodLog(int amount)
            : base(CraftResource.Frostwood, amount)
        {
        }

        public FrostwoodLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!this.TryCreateBoards(from, 100, new FrostwoodBoard()))
                return false;

            return true;
        }
    }

    //public class OakLog : BaseLog
    //{
    //    [Constructable]
    //    public OakLog()
    //        : this(1)
    //    {
    //    }

    //    [Constructable]
    //    public OakLog(int amount)
    //        : base(CraftResource.OakWood, amount)
    //    {
    //    }

    //    public OakLog(Serial serial)
    //        : base(serial)
    //    {
    //    }

    //    public override void Serialize(GenericWriter writer)
    //    {
    //        base.Serialize(writer);

    //        writer.Write((int)0); // version
    //    }

    //    public override void Deserialize(GenericReader reader)
    //    {
    //        base.Deserialize(reader);

    //        int version = reader.ReadInt();
    //    }

    //    public override bool Axe(Mobile from, BaseAxe axe)
    //    {
    //        if (!this.TryCreateBoards(from, 65, new OakBoard()))
    //            return false;

    //        return true;
    //    }
    //}

    public class AshLog : BaseLog
    {
        [Constructable]
        public AshLog()
            : this(1)
        {
        }

        [Constructable]
        public AshLog(int amount)
            : base(CraftResource.AshWood, amount)
        {
        }

        public AshLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!this.TryCreateBoards(from, 80, new AshBoard()))
                return false;

            return true;
        }
    }

    public class YewLog : BaseLog
    {
        [Constructable]
        public YewLog()
            : this(1)
        {
        }

        [Constructable]
        public YewLog(int amount)
            : base(CraftResource.YewWood, amount)
        {
        }

        public YewLog(Serial serial)
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

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!this.TryCreateBoards(from, 95, new YewBoard()))
                return false;

            return true;
        }
    }

    public class PinetreeLog : BaseLog
    {
        [Constructable]
        public PinetreeLog()
            : this(1)
        {
        }

        [Constructable]
        public PinetreeLog(int amount)
            : base(CraftResource.Pinetree, amount)
        {
        }

        public PinetreeLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 100, new PinetreeBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class CherryLog : BaseLog
    {
        [Constructable]
        public CherryLog()
            : this(1)
        {
        }

        [Constructable]
        public CherryLog(int amount)
            : base(CraftResource.Cherry, amount)
        {
        }

        public CherryLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new CherryBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class OakLog : BaseLog
    {
        [Constructable]
        public OakLog()
            : this(1)
        {
        }

        [Constructable]
        public OakLog(int amount)
            : base(CraftResource.Oak, amount)
        {
        }

        public OakLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new OakBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class PurplePassionLog : BaseLog
    {
        [Constructable]
        public PurplePassionLog()
            : this(1)
        {
        }

        [Constructable]
        public PurplePassionLog(int amount)
            : base(CraftResource.PurplePassion, amount)
        {
        }

        public PurplePassionLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new PurplePassionBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class GoldenReflectionLog : BaseLog
    {
        [Constructable]
        public GoldenReflectionLog()
            : this(1)
        {
        }

        [Constructable]
        public GoldenReflectionLog(int amount)
            : base(CraftResource.GoldenReflection, amount)
        {
        }

        public GoldenReflectionLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new GoldenReflectionBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class HardrangerLog : BaseLog
    {
        [Constructable]
        public HardrangerLog()
            : this(1)
        {
        }

        [Constructable]
        public HardrangerLog(int amount)
            : base(CraftResource.Hardranger, amount)
        {
        }

        public HardrangerLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new HardrangerBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class JadeLog : BaseLog
    {
        [Constructable]
        public JadeLog()
            : this(1)
        {
        }

        [Constructable]
        public JadeLog(int amount)
            : base(CraftResource.Jade, amount)
        {
        }

        public JadeLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new JadeBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class DarkwoodLog : BaseLog
    {
        [Constructable]
        public DarkwoodLog()
            : this(1)
        {
        }

        [Constructable]
        public DarkwoodLog(int amount)
            : base(CraftResource.Darkwood, amount)
        {
        }

        public DarkwoodLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new DarkwoodBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class StonewoodLog : BaseLog
    {
        [Constructable]
        public StonewoodLog()
            : this(1)
        {
        }

        [Constructable]
        public StonewoodLog(int amount)
            : base(CraftResource.Stonewood, amount)
        {
        }

        public StonewoodLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new StonewoodBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class SunLog : BaseLog
    {
        [Constructable]
        public SunLog()
            : this(1)
        {
        }

        [Constructable]
        public SunLog(int amount)
            : base(CraftResource.Sun, amount)
        {
        }

        public SunLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new SunBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class GauntletLog : BaseLog
    {
        [Constructable]
        public GauntletLog()
            : this(1)
        {
        }

        [Constructable]
        public GauntletLog(int amount)
            : base(CraftResource.Gauntlet, amount)
        {
        }

        public GauntletLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new GauntletBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class SwampLog : BaseLog
    {
        [Constructable]
        public SwampLog()
            : this(1)
        {
        }

        [Constructable]
        public SwampLog(int amount)
            : base(CraftResource.Swamp, amount)
        {
        }

        public SwampLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new SwampBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class StardustLog : BaseLog
    {
        [Constructable]
        public StardustLog()
            : this(1)
        {
        }

        [Constructable]
        public StardustLog(int amount)
            : base(CraftResource.Stardust, amount)
        {
        }

        public StardustLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new StardustBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class SilverLeafLog : BaseLog
    {
        [Constructable]
        public SilverLeafLog()
            : this(1)
        {
        }

        [Constructable]
        public SilverLeafLog(int amount)
            : base(CraftResource.SilverLeaf, amount)
        {
        }

        public SilverLeafLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new SilverLeafBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class StormtealLog : BaseLog
    {
        [Constructable]
        public StormtealLog()
            : this(1)
        {
        }

        [Constructable]
        public StormtealLog(int amount)
            : base(CraftResource.Stormteal, amount)
        {
        }

        public StormtealLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new StormtealBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class EmeraldLog : BaseLog
    {
        [Constructable]
        public EmeraldLog()
            : this(1)
        {
        }

        [Constructable]
        public EmeraldLog(int amount)
            : base(CraftResource.Emerald, amount)
        {
        }

        public EmeraldLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new EmeraldBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class BloodLog : BaseLog
    {
        [Constructable]
        public BloodLog()
            : this(1)
        {
        }

        [Constructable]
        public BloodLog(int amount)
            : base(CraftResource.Blood, amount)
        {
        }

        public BloodLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new BloodBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class CrystalLog : BaseLog
    {
        [Constructable]
        public CrystalLog()
            : this(1)
        {
        }

        [Constructable]
        public CrystalLog(int amount)
            : base(CraftResource.CrystalLog, amount)
        {
        }

        public CrystalLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new CrystalBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class BloodhorseLog : BaseLog
    {
        [Constructable]
        public BloodhorseLog()
            : this(1)
        {
        }

        [Constructable]
        public BloodhorseLog(int amount)
            : base(CraftResource.Bloodhorse, amount)
        {
        }

        public BloodhorseLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new BloodhorseBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class DoomLog : BaseLog
    {
        [Constructable]
        public DoomLog()
            : this(1)
        {
        }

        [Constructable]
        public DoomLog(int amount)
            : base(CraftResource.DoomLog, amount)
        {
        }

        public DoomLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new DoomBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class GoddessLog : BaseLog
    {
        [Constructable]
        public GoddessLog()
            : this(1)
        {
        }

        [Constructable]
        public GoddessLog(int amount)
            : base(CraftResource.GoddessLog, amount)
        {
        }

        public GoddessLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new GoddessBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class ZuluLog : BaseLog
    {
        [Constructable]
        public ZuluLog()
            : this(1)
        {
        }

        [Constructable]
        public ZuluLog(int amount)
            : base(CraftResource.ZuluLog, amount)
        {
        }

        public ZuluLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new ZuluBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class DarknessLog : BaseLog
    {
        [Constructable]
        public DarknessLog()
            : this(1)
        {
        }

        [Constructable]
        public DarknessLog(int amount)
            : base(CraftResource.Darkness, amount)
        {
        }

        public DarknessLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new DarknessBoard()))
        //        return false;

        //    return true;
        //}
    }

    public class ElvenLog : BaseLog
    {
        [Constructable]
        public ElvenLog()
            : this(1)
        {
        }

        [Constructable]
        public ElvenLog(int amount)
            : base(CraftResource.Elven, amount)
        {
        }

        public ElvenLog(Serial serial)
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

        //public override bool Axe(Mobile from, BaseAxe axe)
        //{
        //    if (!this.TryCreateBoards(from, 95, new ElvenBoard()))
        //        return false;

        //    return true;
        //}
    }
}