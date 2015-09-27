using System;
using Server.Network;
using Server.Gumps;
using Server.Spells;
using Server.Mobiles;
using Server.Gumps.Zulugumps;

namespace Server.Items
{
    public class EarthBook : Spellbook
    {
        public override SpellbookType SpellbookType { get { return SpellbookType.Earth; } }

        private bool[] spellArray = new bool[17]; // 1-16, 0 is reserved for closing book event

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Antidote
        {
            get
            {
                return spellArray[1];
            }
            set
            {
                spellArray[1] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool OwlSight
        {
            get
            {
                return spellArray[2];
            }
            set
            {
                spellArray[2] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShiftingEarth
        {
            get
            {
                return spellArray[3];
            }
            set
            {
                spellArray[3] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SummonMammals
        {
            get
            {
                return spellArray[4];
            }
            set
            {
                spellArray[4] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CallLightning
        {
            get
            {
                return spellArray[5];
            }
            set
            {
                spellArray[5] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool EarthBlessing
        {
            get
            {
                return spellArray[6];
            }
            set
            {
                spellArray[6] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool EarthPortal
        {
            get
            {
                return spellArray[7];
            }
            set
            {
                spellArray[7] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool NaturesTouch
        {
            get
            {
                return spellArray[8];
            }
            set
            {
                spellArray[8] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GustofAir
        {
            get
            {
                return spellArray[9];
            }
            set
            {
                spellArray[9] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool RisingFire
        {
            get
            {
                return spellArray[10];
            }
            set
            {
                spellArray[10] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Shapeshift
        {
            get
            {
                return spellArray[11];
            }
            set
            {
                spellArray[11] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IceStrike
        {
            get
            {
                return spellArray[12];
            }
            set
            {
                spellArray[12] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool EarthSpirit
        {
            get
            {
                return spellArray[13];
            }
            set
            {
                spellArray[13] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool FireSpirit
        {
            get
            {
                return spellArray[14];
            }
            set
            {
                spellArray[14] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool StormSpirit
        {
            get
            {
                return spellArray[15];
            }
            set
            {
                spellArray[15] = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool WaterSpirit
        {
            get
            {
                return spellArray[16];
            }
            set
            {
                spellArray[16] = value;
            }
        }

       
        [Constructable]
        public EarthBook()
            : this((ulong)0)
        {
        }

        [Constructable]
        public EarthBook(ulong content)
            : base(content, 0x2D50)
        {
            Hue = 0x48a;
            Name = "book of the Earth";
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (from == null || dropped == null)
                return false;

            if (dropped is SpellScroll && dropped.Amount == 1)
            {
                SpellScroll scroll = (SpellScroll)dropped;

                SpellbookType type = GetTypeForSpell(scroll.SpellID);

                if (type != SpellbookType)
                {
                    return false;
                }
                addSpell(scroll, from);
                
            }
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;

           
            return base.OnDragDrop(from, dropped);
        }

        private bool SkillCheck(SpellScroll scroll, Mobile player)
        {
            return true;
        }

        private void addSpell(SpellScroll scroll, Mobile player){

            if (!SkillCheck(scroll, player)) { return; } // gotta fix later

            switch (scroll.Name)
            {
                case "Earth bless scroll":
                    {
                        if (EarthBlessing == true) { player.SendMessage("The Earth book already contains this spell"); } else { EarthBlessing = true; scroll.Consume(1); }
                        break;
                    }
                    // Add more
            }

        }

        public override void OnDoubleClick(Mobile from)
        {

            Container pack = from.Backpack;
            if (Parent == from || (pack != null && Parent == pack))
            {
                from.CloseGump(typeof(ebookgump));
                from.SendGump(new ebookgump(from, spellArray));
            }
            else from.SendLocalizedMessage(500207); // The spellbook must be in your backpack (and not in a container within) to open.         
        }



        public EarthBook(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
            for (int k = 0; k < spellArray.Length; k++)
            {
                writer.Write(spellArray[k]);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 1:
                    for (int k = 0; k < spellArray.Length; k++)
                    {
                        spellArray[k] = reader.ReadBool();
                    }
                    goto case 0;
                case 0:
                    break;
            }
        }
    }
}