	    ////////////////////////////////////////////////////////////////////////////////////////
	   /////                                                                            ////////
	  //////    Version: 1.0   Original Author: Vorspire    Shard: Alternate-PK         ////////
	 ///////                                                                            ////////
	////////    QuakeNet: #Alternate-PK		MSN: alere_flammas666@hotmail.com           ////////
	////////                                                                            ////////
	////////                       ***Event Ethereal Ride***                            ////////
	////////                                                                            ////////
	////////    Distribution: This script can be freely distributed, as long as the     ////////
	////////                  credit notes are left intact.	This script can also be     ////////
	////////                  modified, as long as the credit notes are left intact.    ///////
	////////                                                                            //////
	/////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	public class EventEthereal : EtherealMount 
	{ 			
		[Constructable] 
		public EventEthereal(int hue) : base( 0x2615, 0x3E9A ) 
		{ 
			Name = "an event ethereal statuette";
			LootType = LootType.Blessed;
			Hue = hue;	
		} 

		public EventEthereal( Serial serial ) : base( serial ) 
		{ 
		} 

		// Event Item Steal Protection
		public override bool OnEquip( Mobile from ) 
		{
			if(from.Player) 
			{
				PlayerMobile pm = from as PlayerMobile;

				if(pm.IsInEvent != true) 
				{
					if(pm.AccessLevel >= AccessLevel.GameMaster)
					{
						return true;
					}
					else

						from.SendMessage("Oh cool one of these event Items!");
					this.Delete();
					
					return false;
				} 
				else 
				{
					return true;
				}


			} 
			else 
			{
				return false;
			}
		}

		public override bool OnDroppedToMobile( Mobile from, Mobile target )
		{
			if(from.Player) 
			{
				PlayerMobile pm = from as PlayerMobile;

				if(pm.IsInEvent != true) 
				{ 
					if(pm.AccessLevel >= AccessLevel.GameMaster)
					{
						return true;
					}
					else

						from.SendMessage("Oh cool one of these Event Items!");
					this.Delete();
					return false;
				} 
				else 
				{
					return true;
				}


			} 
			else 
			{
				return false;
			}
		}

		public override bool OnDroppedToWorld( Mobile from, Point3D p )
		{
			if(from.Player) 
			{
				PlayerMobile pm = from as PlayerMobile;

				if(pm.IsInEvent != true) 
				{ 
					if(pm.AccessLevel >= AccessLevel.GameMaster)
					{
						return true;
					}
					else

						from.SendMessage("Oh cool one of these Event Items!");
					this.Delete();
					return false;

				} 
				else 
				{
					return true;
				}


			} 
			else 
			{
				return false;
			}
		}

		// And to really make sure nothing will ever happen....

		public override bool OnDragLift( Mobile from )
		{

			if(from.Player) 
			{
				PlayerMobile pm = from as PlayerMobile;

				if(pm.IsInEvent != true) 
				{ 
					if(pm.AccessLevel >= AccessLevel.GameMaster)
					{
						return true;
					}
					else

						from.SendMessage("Oh cool one of these Event Items!");
					this.Delete();
					return false;

				} 
				else 
				{
					return true;
				}


			} 
			else 
			{
				return false;
			}

		}
		// End :>

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