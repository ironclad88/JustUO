	    ////////////////////////////////////////////////////////////////////////////////////////
	   /////                                                                            ////////
	  //////    Version: 1.0   Original Author: Vorspire    Shard: Alternate-PK         ////////
	 ///////                                                                            ////////
	////////    QuakeNet: #Alternate-PK		MSN: alere_flammas666@hotmail.com           ////////
	////////                                                                            ////////
	////////                       ***Event Prize Box***                                ////////
	////////                                                                            ////////
	////////    Distribution: This script can be freely distributed, as long as the     ////////
	////////                  credit notes are left intact.	This script can also be     ////////
	////////                  modified, as long as the credit notes are left intact.    ///////
	////////                                                                            //////
	/////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using System;

namespace Server.Items
{
	[FlipableAttribute( 0xE80, 0x9A8 )] 
	public class EventBox : BaseTreasureChest 
	{ 
		public override int DefaultGumpID{ get{ return 0x4A; } }

		[Constructable] 
		public EventBox(int hue) : base( 0x9a8 ) 
		{ 
			Name = "an event box";
			LootType = LootType.Blessed;
			Movable = true;
			TrapType = 0;
			LockLevel = 0;
			Hue = hue;
			Locked = false;
		} 

		public EventBox( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
}