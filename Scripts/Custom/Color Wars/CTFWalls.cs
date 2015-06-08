//Created by ashftw - ashley_bamforth@hotmail.com
//Downloaded from www.runuo.com

using System;

namespace Server.Items
{
	public class CWwWallEast : Item
	{
		[Constructable]
		public CWwWallEast() : this( 1 )
		{
		}

		[Constructable]
		public CWwWallEast( int amount ) : base( 0x242 )
		{
			Stackable = false;
			Weight = 1.0;
			Movable = false;
	
		}

		public CWwWallEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class CWwWallSouth : Item
	{
		[Constructable]
		public CWwWallSouth() : this( 1 )
		{
		}

		[Constructable]
		public CWwWallSouth( int amount ) : base( 0x241 )
		{
			Stackable = false;
			Weight = 1.0;
			Movable = false;
	
		}

		public CWwWallSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
