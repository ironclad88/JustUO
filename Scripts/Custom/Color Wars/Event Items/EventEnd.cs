	    ////////////////////////////////////////////////////////////////////////////////////////
	   /////                                                                            ////////
	  //////    Version: 1.0   Original Author: Vorspire    Shard: Alternate-PK         ////////
	 ///////                                                                            ////////
	////////    QuakeNet: #Alternate-PK		MSN: alere_flammas666@hotmail.com           ////////
	////////                                                                            ////////
	////////                       ***Event Ending Pad***                               ////////
	////////                                                                            ////////
	////////    Distribution: This script can be freely distributed, as long as the     ////////
	////////                  credit notes are left intact.	This script can also be     ////////
	////////                  modified, as long as the credit notes are left intact.    ///////
	////////                                                                            //////
	/////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

using System;
using Server;
using System.IO;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class EventEnd : Item
	{

		private Point3D m_Target;
		private Map m_TargetMap;
		


		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Target
		{
			get
			{
				return m_Target;
			}
			set
			{
				m_Target = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map TargetMap
		{
			get
			{
				return m_TargetMap;
			}
			set
			{
				m_TargetMap = value;
			}
		}


		[Constructable]
		public EventEnd() : base( 0x17e5 )
		{
			Movable = false;
			Hue = 110;
			Name = "Event End";
		}

		public EventEnd( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m)
		{
			if(m.Player == true) 
			{
				PlayerMobile pm = m as PlayerMobile;
				//BaseMobile bm = m as BaseMobile;
		
				if(pm.IsInEvent == false)
				{	m.SendMessage("You don't have any Event Flags. Please Page!"); }

				else if (pm.Mounted == true)
				{								  
					pm.SendMessage( "Please Dismount!" );
				}
				else if (m_TargetMap != null && m_TargetMap != Map.Internal && pm.IsInEvent == true)
				{
								

					Backpack bag = new Backpack();
					Container pack = m.Backpack;
					BankBox box = m.BankBox;
					ArrayList equipitems = new ArrayList(m.Items);
					ArrayList bagitems = new ArrayList( pack.Items );
					foreach (Item item in equipitems)
								
					{
						if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.Mount) && (item.Layer != Layer.FacialHair))
						{
							pack.DropItem( item );
						}
					}
					Container pouch = m.Backpack;
					ArrayList finalitems = new ArrayList( pouch.Items );
					foreach (Item items in finalitems)
					{


						if(items.Name == "an event box")
						{
							Item bi = items as Item;
							bi.LootType = LootType.Regular;			
						}

						if(items.LootType == LootType.Blessed)
						{
							Item bi = items as Item;
							items.Delete();
						}
              



					}


					m.SendMessage("Thank you for coming!");
					m.MoveToWorld(m_Target, m_TargetMap);
					m.PlaySound(0x1FE);
					pm.IsInEvent = false;
					m.Title = null;



				}
				}
			return false;
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Target );
			writer.Write( m_TargetMap );
	
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Target = reader.ReadPoint3D();
			m_TargetMap = reader.ReadMap();
	
			
			
		}
	}
}
