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
    public class CW4TeamG : Item
    {
        private bool m_Bandages = true;
        private int m_BandageAmount = 500;

        private bool m_EventBox = true;
        private bool m_Armor = true;
        private bool m_Weapons = true;
        private bool m_EtherealMount = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool A_Bandages
        {
            get { return m_Bandages; }
            set { m_Bandages = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int B_BandageAmount
        {
            get { return m_BandageAmount; }
            set { m_BandageAmount = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool C_EventBox
        {
            get { return m_EventBox; }
            set { m_EventBox = value; }
        }
        

        [CommandProperty(AccessLevel.GameMaster)]
        public bool D_Armor
        {
            get { return m_Armor; }
            set { m_Armor = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool E_Weapons
        {
            get { return m_Weapons; }
            set { m_Weapons = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool F_EtherealMount
        {
            get { return m_EtherealMount; }
            set { m_EtherealMount = value; }
        }

        [Constructable]
        public CW4TeamG()
            : base(0xF6C)
        {
            Movable = false;
            Light = LightType.Circle300;
            Hue = 962;
            Name = "Color Wars 4 Team Gate";
        }

        public CW4TeamG(Serial serial)
            : base(serial)
        {
        }

        

        public override bool OnMoveOver(Mobile m)
        {
            Mobile from = m;
            PlayerMobile pm = m as PlayerMobile;
            Backpack bag = new Backpack();
            Container pack = m.Backpack;
            BankBox box = m.BankBox;
            ArrayList equipitems = new ArrayList(m.Items);
            ArrayList bagitems = new ArrayList(pack.Items);
            foreach (Item item in equipitems)
            {
                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
                {
                    pack.DropItem(item);

                }
            }
            Container pouch = m.Backpack;
            ArrayList finalitems = new ArrayList(pouch.Items);
            foreach (Item items in finalitems)
            {
                bag.DropItem(items);
            }
            box.DropItem(bag);



            
            m.PlaySound(0x1FE);

            if (m_Weapons != false)
            {
                pm.AddToBackpack(new EventSword(Hue));
                pm.AddToBackpack(new EventBow(Hue));
                pm.AddToBackpack(new Spellbook(UInt64.MaxValue));
            }


            if (m_Bandages != false)
            {
                m.AddToBackpack(new EventAids(m_BandageAmount, Hue));
            }

            if (m_EventBox != false)
            {
                m.AddToBackpack(new EventBox(Hue));
            }

            if (m_Armor != false)
            {
                pm.AddToBackpack(new EventChest(Hue));
                pm.AddToBackpack(new EventArms(Hue));
                pm.AddToBackpack(new EventGloves(Hue));
                pm.AddToBackpack(new EventLegs(Hue));
                pm.AddToBackpack(new EventHelm(Hue));
                pm.AddToBackpack(new EventGorget(Hue));
                pm.AddToBackpack(new EventShield(Hue));
                pm.AddToBackpack(new EventRobe(Hue));
            }

            if (m_EtherealMount != false)
            {
                pm.AddToBackpack(new EventEthereal(Hue));
            }
            if (pm != null)
            {
                PlayerMobile PVP = from as PlayerMobile;
               // PVP.PVM = PVM.Null;
                pm.IsInEvent = true;
                pm.Title = "[Event]";
                // JustZH 4Team Color Wars has not gotten fixed yet
                switch (Utility.Random(4))
                    {
                        case 0:
                            m.SendMessage("You joined the Red Team");
                            pm.ColorWarRed = true;
                            m.Map = Map.Felucca;
                            m.X = 5990;
                            m.Y = 485;
                            m.Z = -22;
                            m.HueMod = 32;
                            break;
                        case 1:
                            m.SendMessage("You joined the Blue Team");
                            pm.ColorWarBlue = true;
                            m.Map = Map.Felucca;
                            m.X = 5911;
                            m.Y = 406;
                            m.Z = -22;
                            
                            m.HueMod = 3;
                            break;


                        case 2:
                            m.SendMessage("You joined the White Team");
                            pm.ColorWarWhite = true;
                            m.Map = Map.Felucca;
                            m.X = 5991;
                            m.Y = 406;
                            m.Z = -22;
                            m.HueMod = 1150;
                            break;

                        case 3:

                            m.SendMessage("You joined the Black Team");
                            pm.ColorWarBlack = true;
                            m.Map = Map.Felucca;
                            m.X = 5911;
                            m.Y = 486;
                            m.Z = -22;
                            m.HueMod = 1175;
                            break;
                    }
                }
                return false;
            }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);

            //version 0
            writer.Write(m_Bandages);
            writer.Write(m_BandageAmount);
            writer.Write(m_EventBox);
            writer.Write(m_Armor);
            writer.Write(m_Weapons);

            //version 1
            writer.Write(m_EtherealMount);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Bandages = reader.ReadBool();
            m_BandageAmount = reader.ReadInt();
            m_EventBox = reader.ReadBool();
            m_Armor = reader.ReadBool();
            m_Weapons = reader.ReadBool();

            if (version >= 1)
            {
                m_EtherealMount = reader.ReadBool();
            }
        }
    }
}