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
    public class ColorWarGate2Team : Item
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

        [Constructable] // JustZH need a better way to use this, automate it. its just stupid to use it manually
        public ColorWarGate2Team() // Added a stone instead of moongate
            : base(0x0ed4)
        {
            Movable = false;
            Light = LightType.Circle300;
            Hue = 1627;
            Name = "Color Wars Arena Stone";
        }

        public ColorWarGate2Team(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile m)
        {
            //Mobile from = m;
            bool eligable = true;
            if (m.Mounted == true)
            {
                m.SendMessage("You must dismount to play Color Wars.");
                eligable = false;
            }
            else if (m.RawInt < 90 || m.RawDex < 90 || m.RawStr < 90)
            {
                m.SendMessage("Sorry, your base stats are not high enough.");
                eligable = false;
            }
            if(eligable == true)
            { 
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
            box.DropItem(bag); // adds all items on player and players backpack to a new pack, and stores it in a bank, pretty sweet.


            if (pm != null)
            {
                PlayerMobile PVP = m as PlayerMobile;
                // PVP.PVM = PVM.Null; // wtf is dis? gave me alotta errors
                pm.IsInEvent = true;
                

                switch (Utility.Random(2)) // JustZH need a better system for this, the teams can become unbalanced
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
                }
            }
            // Fixed by pillzan
            int EventItemHue = 0;
            if (pm.ColorWarRed == true) EventItemHue = 32;
            if (pm.ColorWarBlue == true) EventItemHue = 2;

            m.PlaySound(0x1FE);
            // JustZH maybe add for bards later (theifs needs balancing i think)
            if (m.SpecClasse == SpecClasse.Mage)
            {
                pm.AddToBackpack(new Spellbook(UInt64.MaxValue));
                m.AddToBackpack(new EventBox(EventItemHue)); // dunno what this is used for.... will add it anyways
                pm.AddToBackpack(new EventRobe(EventItemHue));
                pm.Title = "[Color Wars Mage]";
            }
            else if (m.SpecClasse == SpecClasse.Warrior)
            {
                pm.AddToBackpack(new EventSword(EventItemHue)); // maybe make it power?
                m.AddToBackpack(new EventAids(m_BandageAmount, EventItemHue));
                pm.AddToBackpack(new EventChest(EventItemHue)); // buff armor stats?
                pm.AddToBackpack(new EventArms(EventItemHue));
                pm.AddToBackpack(new EventGloves(EventItemHue));
                pm.AddToBackpack(new EventLegs(EventItemHue));
                pm.AddToBackpack(new EventHelm(EventItemHue));
                pm.AddToBackpack(new EventGorget(EventItemHue));
                pm.AddToBackpack(new EventShield(EventItemHue));
                pm.AddToBackpack(new EventRobe(EventItemHue));
                m.AddToBackpack(new EventBox(EventItemHue)); // dunno what this is used for.... will add it anyways
                pm.Title = "[Color Wars Warrior]";
            }
            else if (m.SpecClasse == SpecClasse.Ranger)
            {
                pm.AddToBackpack(new EventBow(EventItemHue));  // maybe make it power?
                pm.AddToBackpack(new EventChainChest(EventItemHue));
                pm.AddToBackpack(new EventChainCoif(EventItemHue));
                pm.AddToBackpack(new EventChainLegs(EventItemHue));
                pm.AddToBackpack(new EventRobe(EventItemHue));
                pm.AddToBackpack(new GreaterPoisonPotion());
                pm.AddToBackpack(new GreaterPoisonPotion()); // needs to be balanced, not really sure about this
                pm.AddToBackpack(new GreaterPoisonPotion());
                m.AddToBackpack(new EventBox(EventItemHue)); // dunno what this is used for.... will add it anyways
                pm.Title = "[Color Wars Ranger]";
            }
            else if (m.SpecClasse == SpecClasse.Thief)
            {
                pm.AddToBackpack(new EventStuddedArms(EventItemHue));
                pm.AddToBackpack(new EventStuddedChest(EventItemHue));
                pm.AddToBackpack(new EventStuddedGloves(EventItemHue));
                pm.AddToBackpack(new EventStuddedGorget(EventItemHue));
                pm.AddToBackpack(new EventBow(EventItemHue));
                pm.AddToBackpack(new EventRobe(EventItemHue));
                m.AddToBackpack(new EventBox(EventItemHue)); // dunno what this is used for.... will add it anyways
                pm.Title = "[Color Wars Theif]";
            }
            else
            {
                pm.AddToBackpack(new Spellbook(UInt64.MaxValue));
                m.AddToBackpack(new EventBox(EventItemHue)); // dunno what this is used for.... will add it anyways
                pm.AddToBackpack(new EventRobe(EventItemHue));
                pm.AddToBackpack(new EventSword(EventItemHue)); // maybe make it power?
                m.AddToBackpack(new EventAids(m_BandageAmount, EventItemHue));
                pm.AddToBackpack(new EventChest(EventItemHue)); // buff armor stats?
                pm.AddToBackpack(new EventArms(EventItemHue));
                pm.AddToBackpack(new EventGloves(EventItemHue));
                pm.AddToBackpack(new EventLegs(EventItemHue));
                pm.AddToBackpack(new EventHelm(EventItemHue));
                pm.AddToBackpack(new EventGorget(EventItemHue));
                pm.AddToBackpack(new EventShield(EventItemHue));
                pm.Title = "[Color Wars PP]";
            }
            if (m_EtherealMount != false)
            {
                pm.AddToBackpack(new EventEthereal(Hue));
            }

            }
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