	    ////////////////////////////////////////////////////////////////////////////////////////
	   /////                                                                            ////////
	  //////    Version: 1.0   Original Author: Vorspire    Shard: Alternate-PK         ////////
	 ///////                                                                            ////////
	////////    QuakeNet: #Alternate-PK		MSN: alere_flammas666@hotmail.com           ////////
	////////                                                                            ////////
	////////                       ***Event Bandages***                                 ////////
	////////                                                                            ////////
	////////    Distribution: This script can be freely distributed, as long as the     ////////
	////////                  credit notes are left intact.	This script can also be     ////////
	////////                  modified, as long as the credit notes are left intact.    ///////
	////////                                                                            //////
	/////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

using System;

using Server;

namespace Server.Items
{
	public class EventAids : Bandage
	{
		

		public EventAids(int amount, int hue) : base (amount)
		{
			
			Name = "Bandage";
			LootType = LootType.Blessed;
			Hue = hue;
		}


		public EventAids( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
		}


	}
}
