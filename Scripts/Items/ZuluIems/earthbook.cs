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

            if (dropped is SpellScroll /*&& dropped.Amount == 1*/) // the amount needed is stupid
            {
                SpellScroll scroll = (SpellScroll)dropped;

               /* SpellbookType type = GetTypeForSpell(scroll.SpellID);

                if (type != SpellbookType)
                {
                    return false;
                }*/
                addSpell(scroll, from);
                return true;
            }
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;


            return base.OnDragDrop(from, dropped);
        }

        private bool SkillCheck(Mobile player, double reqSkill)
        {
            if (player.Skills.Inscribe.Value >= reqSkill)
            {
                return true;
            }
            else
            {
                player.SendMessage("Your inscription is not high enough");
                return false;
            }
        }

        private void addSpell(SpellScroll scroll, Mobile player)
        {
            switch (scroll.SpellID) // SpellID == the registered ID in Initializer.cs 
            {
                case 87:
                    {
                        if (SkillCheck(player, 90)){ if (EarthBlessing == true){ player.SendMessage("The Earth book already contains this spell"); } else  { EarthBlessing = true; scroll.Consume(1); }}
                        break;
                    }
                case 85:
                    {
                        if (SkillCheck(player, 90)) { if (Antidote == true) { player.SendMessage("The Earth book already contains this spell"); } else { Antidote = true; scroll.Consume(1); } }
                        break;
                    }
                case 86:
                    {
                        if (SkillCheck(player, 90)) { if (CallLightning == true) { player.SendMessage("The Earth book already contains this spell"); } else { CallLightning = true; scroll.Consume(1); } }
                        break;
                    }
                case 88:
                    {
                        if (SkillCheck(player, 90)) { if (EarthPortal == true) { player.SendMessage("The Earth book already contains this spell"); } else { EarthPortal = true; scroll.Consume(1); } }
                        break;
                    }
                case 89:
                    {
                        if (SkillCheck(player, 90)) { if (GustofAir == true) { player.SendMessage("The Earth book already contains this spell"); } else { GustofAir = true; scroll.Consume(1); } }
                        break;
                    }
                case 90:
                    {
                        if (SkillCheck(player, 90)) { if (NaturesTouch == true) { player.SendMessage("The Earth book already contains this spell"); } else { NaturesTouch = true; scroll.Consume(1); } }
                        break;
                    }
                case 91:
                    {
                        if (SkillCheck(player, 90)) { if (OwlSight == true) { player.SendMessage("The Earth book already contains this spell"); } else { OwlSight = true; scroll.Consume(1); } }
                        break;
                    }
                case 92:
                    {
                        if (SkillCheck(player, 90)) { if (ShiftingEarth == true) { player.SendMessage("The Earth book already contains this spell"); } else { ShiftingEarth = true; scroll.Consume(1); } }
                        break;
                    }
                case 93:
                    {
                        if (SkillCheck(player, 90)) { if (SummonMammals == true) { player.SendMessage("The Earth book already contains this spell"); } else { SummonMammals = true; scroll.Consume(1); } }
                        break;
                    }
                case 94:
                    {
                        if (SkillCheck(player, 90)) { if (IceStrike == true) { player.SendMessage("The Earth book already contains this spell"); } else { IceStrike = true; scroll.Consume(1); } }
                        break;
                    }
                case 95:
                    {
                        if (SkillCheck(player, 90)) { if (RisingFire == true) { player.SendMessage("The Earth book already contains this spell"); } else { RisingFire = true; scroll.Consume(1); } }
                        break;
                    }
                case 96:
                    {
                        if (SkillCheck(player, 90)) { if (StormSpirit == true) { player.SendMessage("The Earth book already contains this spell"); } else { StormSpirit = true; scroll.Consume(1); } }
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