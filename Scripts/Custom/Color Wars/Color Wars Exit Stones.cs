using System; 
using Server.Items;
using System.Collections;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Regions;

namespace Server.Items
{ 
	public class RedWon : Item 
	{ 
		[Constructable] 
		public RedWon() : base( 3803 ) 
		{ 
			Movable = false; 
			Name = "Red Team Won!";
			Hue = 32;
		} 

		public override void OnDoubleClickDead( Mobile m )
		{
			DoDoubleClick(m);
		}
		
		public override void OnDoubleClick( Mobile m ) 
		{
			DoDoubleClick(m);
		}

        private void DoDoubleClick(Mobile m)
        {
            if (m.InRange(this.GetWorldLocation(), 1))
            {
                if (m is PlayerMobile)
                {
                    if (m.Player == true)
                    {
                        PlayerMobile pm = m as PlayerMobile;
                        //BaseMobile bm = m as BaseMobile;

                        if (pm.IsInEvent == false)
                        { m.SendMessage("You don't have any Event Flags. Please Page!"); }

                        else if (pm.Mounted == true)
                        {
                            pm.SendMessage("Please Dismount!");

                            Backpack bag = new Backpack();
                            Container pack = m.Backpack;
                            BankBox box = m.BankBox;
                            ArrayList equipitems = new ArrayList(m.Items);
                            ArrayList bagitems = new ArrayList(pack.Items);
                            foreach (Item item in equipitems)
                            {
                                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.Mount) && (item.Layer != Layer.FacialHair))
                                {
                                    pack.DropItem(item); // JustZH needs to remove all items instead, this is a stupid system
                                }
                            }
                            Container pouch = m.Backpack;
                            ArrayList finalitems = new ArrayList(pouch.Items);
                            foreach (Item items in finalitems)
                            {


                                if (items.Name == "an event box") // dunno about this.
                                {
                                    Item bi = items as Item;
                                    bi.LootType = LootType.Regular;
                                }

                                if (items.LootType == LootType.Blessed) // this dont seem to work atm
                                {
                                    Item bi = items as Item;
                                    items.Delete();
                                }



                            }


                            if (pm.ColorWarRed == true)
                            m.BankBox.DropItem(new Token(1)); // this dont seem to be working either
                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;



                            m.Kill();
                            m.Map = Map.Felucca;
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 2000;
                            m.Mana = 2000;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;

                        }
                        else
                        {

                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;


                            m.Map = Map.Felucca;
                            m.Kill();
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 2000;
                            m.Mana = 2000;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;
                        }
                    }
                }
            }
        }

		public RedWon( Serial serial ) : base( serial ) 
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

namespace Server.Items
{ 
	public class BlueWon : Item 
	{ 
		[Constructable] 
		public BlueWon() : base( 3803 ) 
		{ 
			Movable = false; 
			Name = "Blue Team Won!";
			Hue = 3;
		} 

		public override void OnDoubleClickDead( Mobile m )
		{
			DoDoubleClick(m);
		}
		
		public override void OnDoubleClick( Mobile m ) 
		{
			DoDoubleClick(m);
		}

		private void DoDoubleClick( Mobile m )
		{

            if (m.InRange(this.GetWorldLocation(), 1))
            {
                if (m is PlayerMobile)
                {
                    if (m.Player == true)
                    {
                        PlayerMobile pm = m as PlayerMobile;
                        //BaseMobile bm = m as BaseMobile;

                        if (pm.IsInEvent == false)
                        { m.SendMessage("You don't have any Event Flags. Please Page!"); }

                        else if (pm.Mounted == true)
                        {
                            pm.SendMessage("Please Dismount!");




                            Backpack bag = new Backpack();
                            Container pack = m.Backpack;
                            BankBox box = m.BankBox;
                            ArrayList equipitems = new ArrayList(m.Items);
                            ArrayList bagitems = new ArrayList(pack.Items);
                            foreach (Item item in equipitems)
                            {
                                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.Mount) && (item.Layer != Layer.FacialHair))
                                {
                                    pack.DropItem(item);
                                }
                            }
                            Container pouch = m.Backpack;
                            ArrayList finalitems = new ArrayList(pouch.Items);
                            foreach (Item items in finalitems)
                            {


                                if (items.Name == "an event box")
                                {
                                    Item bi = items as Item;
                                    bi.LootType = LootType.Regular;
                                }

                                if (items.LootType == LootType.Blessed)
                                {
                                    Item bi = items as Item;
                                    items.Delete();
                                }



                            }

                            foreach (Item items in bagitems)
                            {
                                Console.WriteLine(items.ItemData.Name);
                                if (items.Name.Contains("event") || items.Name.Contains("book"))
                                {
                                    items.Delete();
                                }

                            }

                            if (pm.ColorWarBlue == true)
                                m.BankBox.DropItem(new Token(1));
                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;


                            m.Kill();
                            m.Map = Map.Felucca;
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 200;
                            m.Mana = 200;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;

                        }
                        else
                        {

                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;


                            m.Map = Map.Felucca;
                            m.Kill();
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 200;
                            m.Mana = 200;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;
                        }

                    }
                }
            }
        }
		public BlueWon( Serial serial ) : base( serial ) 
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

namespace Server.Items
{ 
	public class WhiteWon : Item 
	{ 
		[Constructable] 
		public WhiteWon() : base( 3803 ) 
		{ 
			Movable = false; 
			Name = "White Team Won!";
			Hue = 1150;
		} 

		public override void OnDoubleClickDead( Mobile m )
		{
			DoDoubleClick(m);
		}
		
		public override void OnDoubleClick( Mobile m ) 
		{
			DoDoubleClick(m);
		}

		private void DoDoubleClick( Mobile m )
		{
            if (m.InRange(this.GetWorldLocation(), 1))
            {
                if (m is PlayerMobile)
                {
                    if (m.Player == true)
                    {
                        PlayerMobile pm = m as PlayerMobile;
                        //BaseMobile bm = m as BaseMobile;

                        if (pm.IsInEvent == false)
                        { m.SendMessage("You don't have any Event Flags. Please Page!"); }

                        else if (pm.Mounted == true)
                        {
                            pm.SendMessage("Please Dismount!");




                            Backpack bag = new Backpack();
                            Container pack = m.Backpack;
                            BankBox box = m.BankBox;
                            ArrayList equipitems = new ArrayList(m.Items);
                            ArrayList bagitems = new ArrayList(pack.Items);
                            foreach (Item item in equipitems)
                            {
                                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.Mount) && (item.Layer != Layer.FacialHair))
                                {
                                    pack.DropItem(item);
                                }
                            }
                            Container pouch = m.Backpack;
                            ArrayList finalitems = new ArrayList(pouch.Items);
                            foreach (Item items in finalitems)
                            {


                                if (items.Name == "an event box")
                                {
                                    Item bi = items as Item;
                                    bi.LootType = LootType.Regular;
                                }

                                if (items.LootType == LootType.Blessed)
                                {
                                    Item bi = items as Item;
                                    items.Delete();
                                }
                                if (items.Name.Contains("event") || items.Name.Contains("book"))
                                {
                                    items.Delete();
                                }



                            }

                            foreach (Item items in bagitems)
                            {
                                if (items.Name.Contains("event") || items.Name.Contains("book"))
                                {
                                    items.Delete();
                                }

                            }

                            if (pm.ColorWarWhite == true)
                                m.BankBox.DropItem(new Token(1));
                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;




                            m.Kill();
                            m.Map = Map.Felucca;
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 200;
                            m.Mana = 200;

                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;
                        }
                        else
                        {

                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;


                            m.Map = Map.Felucca;
                            m.Kill();
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 200;
                            m.Mana = 200;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;
                        }
                    }
                }
            }
        }

		public WhiteWon( Serial serial ) : base( serial ) 
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

namespace Server.Items
{ 
	public class BlackWon : Item 
	{ 
		[Constructable] 
		public BlackWon() : base( 3803 ) 
		{ 
			Movable = false; 
			Name = "Black Team Won!";
			Hue = 1175;
		} 

		public override void OnDoubleClickDead( Mobile m )
		{
			DoDoubleClick(m);
		}
		
		public override void OnDoubleClick( Mobile m ) 
		{
			DoDoubleClick(m);
		}

		private void DoDoubleClick( Mobile m )
		{
            if (m.InRange(this.GetWorldLocation(), 1))
            {
                if (m is PlayerMobile)
                {
                    if (m.Player == true)
                    {
                        PlayerMobile pm = m as PlayerMobile;
                        //BaseMobile bm = m as BaseMobile;

                        if (pm.IsInEvent == false)
                        { m.SendMessage("You don't have any Event Flags. Please Page!"); }

                        else if (pm.Mounted == true)
                        {
                            pm.SendMessage("Please Dismount!");




                            Backpack bag = new Backpack();
                            Container pack = m.Backpack;
                            BankBox box = m.BankBox;
                            ArrayList equipitems = new ArrayList(m.Items);
                            ArrayList bagitems = new ArrayList(pack.Items);
                            foreach (Item item in equipitems)
                            {
                                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.Mount) && (item.Layer != Layer.FacialHair))
                                {
                                    pack.DropItem(item);
                                }
                            }
                            Container pouch = m.Backpack;
                            ArrayList finalitems = new ArrayList(pouch.Items);
                            foreach (Item items in finalitems)
                            {


                                if (items.Name == "an event box")
                                {
                                    Item bi = items as Item;
                                    bi.LootType = LootType.Regular;
                                }

                                if (items.LootType == LootType.Blessed)
                                {
                                    Item bi = items as Item;
                                    items.Delete();
                                }



                            }

                            if (pm.ColorWarBlack == true)
                                m.BankBox.DropItem(new Token(1));
                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;


                            m.Kill();
                            m.Map = Map.Felucca;
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 200;
                            m.Mana = 200;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;

                        }
                        else
                        {

                            pm.ColorWarRed = false;
                            pm.ColorWarBlue = false;
                            pm.ColorWarWhite = false;
                            pm.ColorWarBlack = false;

                            m.Map = Map.Felucca;
                            m.Kill();
                            m.X = 1412;
                            m.Y = 1716;
                            m.Z = 40;
                            m.HueMod = -1;
                            m.Resurrect();
                            m.Hits = 200;
                            m.Mana = 200;
                            m.SendMessage("Thank you for coming!");
                            pm.IsInEvent = false;
                            m.Title = null;
                        }
                    }
                }
            }
        }
		public BlackWon( Serial serial ) : base( serial ) 
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

