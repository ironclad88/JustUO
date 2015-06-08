	    ////////////////////////////////////////////////////////////////////////////////////////
	   /////                                                                            ////////
	  //////    Version: 1.0   Original Author: Vorspire    Shard: Alternate-PK         ////////
	 ///////                                                                            ////////
	////////    QuakeNet: #Alternate-PK		MSN: alere_flammas666@hotmail.com           ////////
	////////                                                                            ////////
	////////                       ***Event Weapon Bow***                               ////////
	////////                                                                            ////////
	////////    Distribution: This script can be freely distributed, as long as the     ////////
	////////                  credit notes are left intact.	This script can also be     ////////
	////////                  modified, as long as the credit notes are left intact.    ///////
	////////                                                                            //////
	/////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

using System;
using Server.Mobiles;

using Server;

namespace Server.Items
{
	/// <summary>
	/// Summary description for Eventbow.
	/// </summary>
	public class EventBow : Bow {
	
		public override int AosMinDamage{ get{ return 40; } }
		public override int AosMaxDamage{ get{ return 50; } }
		public override int AosSpeed{ get{ return 55; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		// lightning ray
		public override int EffectID { get { return 0x3818; } }

		public override int DefMaxRange { get { return 15; } }
		public override Type AmmoType {
			get {
				return null;
			}
		}


		[Constructable]
		public EventBow(int hue) {
			Hue = hue;
			Name = "an event crossbow";
			LootType = LootType.Blessed;
		}

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            direct = 0;
            chaos = 0;
			phys = 0;
			fire = 0;
			cold = 0;
			pois = 0;
			nrgy = 100;
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
		public EventBow( Serial serial ) : base( serial ) { }

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
