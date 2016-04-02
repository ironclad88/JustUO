using Server.Gumps.Zulugumps;
using System;

namespace Server.Items
{
    public class songBook : Item
    {
        [Constructable]
        public songBook()
            : base(7187) //0xFF2
        {
            this.Movable = true;
            this.Hue = 1165;
            this.Name = "Book of Songs";
            this.LootType = LootType.Blessed;
        }

        public songBook(Serial serial)
            : base(serial)
        {
        }

        private bool[] spellArray = new bool[17]; // 1-16, 0 is reserved for closing book event

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ControlUndead
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
        public bool VolcanicEruption
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
        public bool DecayingRay
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
        public bool SpectresTouch
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
        public bool AbyssalFlame
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
        public bool Wither // Animate Dead
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
        public bool VengefulSpirit
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
        public bool WraithBreath
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
        public bool SorcerersBane
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
        public bool SummonSpirit
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
        public bool AnimateDead
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
        public bool WyvernStrike
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
        public bool Kill
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
        public bool Liche
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
        public bool Plague
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
        public bool Spellbind
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

        public override string DefaultName
        {
            get
            {
                return "Book of Songs";
            }
        }
        public override void OnDoubleClick(Mobile from)
        {

            from.CloseGump(typeof(songBookGump));
            from.SendGump(new songBookGump(from, spellArray));
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