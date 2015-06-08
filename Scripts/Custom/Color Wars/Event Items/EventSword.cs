	    ////////////////////////////////////////////////////////////////////////////////////////
	   /////                                                                            ////////
	  //////    Version: 1.0   Original Author: Vorspire    Shard: Alternate-PK         ////////
	 ///////                                                                            ////////
	////////    QuakeNet: #Alternate-PK		MSN: alere_flammas666@hotmail.com           ////////
	////////                                                                            ////////
	////////                       ***Event Weapon Sword***                             ////////
	////////                                                                            ////////
	////////    Distribution: This script can be freely distributed, as long as the     ////////
	////////                  credit notes are left intact.	This script can also be     ////////
	////////                  modified, as long as the credit notes are left intact.    ///////
	////////                                                                            //////
	/////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

using System;
using Server.Mobiles;
//using Server.Engines.Staff;

using Server;

namespace Server.Items
{
	/// <summary>
	/// Summary description for EventSword.
	/// </summary>
	public class EventSword : VikingSword {
	
		public override int AosMinDamage{ get{ return 40; } }
		public override int AosMaxDamage{ get{ return 50; } }
		public override int AosSpeed{ get{ return 55; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public EventSword(int hue) {
			//Hue = 0x79d;
			Hue = hue;
			Name = "an event sword";
			Weight = 1.0;
			LootType = LootType.Blessed;
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
					} else {
						return true;
					}


			} else {
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
					} else {
						return true;
					}


			} else {
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

					} else {
						return true;
					}


			} else {
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

					} else {
						return true;
					}


			} else {
			return false;
				}

		}
// End :>

        //public override void OnHit(Mobile attacker, Mobile defender) {
        //    defender.FixedParticles( 0x374A, 1, 15, 5054, 0x7c9, 7, EffectLayer.Head );
        //    defender.PlaySound( 0x1F4 );

        //    base.OnHit (attacker, defender);
        //}


		public EventSword( Serial serial ) : base( serial ) { }

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
