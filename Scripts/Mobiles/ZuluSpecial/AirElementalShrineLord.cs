using Server.Gumps.Zulugumps.ElementalLords;
using Server.Items;
using Server.Items.ZuluIems.Pentagram.Water;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.ZuluSpecial
{

    [CorpseName("a air elemental lord corpse")]
    public class AirElementalShrineLord : BaseCreature
    {
        [Constructable]
        public AirElementalShrineLord()
            : base(AIType.AI_Elemental, FightMode.None, 0, 0, 0, 0)
        {
            this.Name = "the Air Element Shrine Lord";
            this.Body = 0x0d;
            this.BaseSoundID = 278;

            this.Hue = 1346;

            this.SetStr(126, 155);
            this.SetDex(66, 85);
            this.SetInt(101, 125);

            this.SetHits(760000, 9300000);

            this.SetDamage(5000, 9000);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 150, 150);
            this.SetResistance(ResistanceType.Fire, 150, 150);
            this.SetResistance(ResistanceType.Cold, 150, 150);
            this.SetResistance(ResistanceType.Poison, 150, 150);
            this.SetResistance(ResistanceType.Energy, 150, 150);

            this.SetSkill(SkillName.EvalInt, 60.1, 75.0);
            this.SetSkill(SkillName.Magery, 60.1, 75.0);
            this.SetSkill(SkillName.MagicResist, 150, 150);
            this.SetSkill(SkillName.Tactics, 50.1, 70.0);
            this.SetSkill(SkillName.Wrestling, 50.1, 70.0);

            this.VirtualArmor = 4000;

        }

        public AirElementalShrineLord(Serial serial)
            : base(serial)
        {
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {

            if (from == null || dropped == null)
                return false;

            if (dropped is AirPent1 && !from.AirPent1) { from.AirPent1 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent2 && !from.AirPent2) { from.AirPent2 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent3 && !from.AirPent3) { from.AirPent3 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent4 && !from.AirPent4) { from.AirPent4 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent5 && !from.AirPent5) { from.AirPent5 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent6 && !from.AirPent6) { from.AirPent6 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent7 && !from.AirPent7) { from.AirPent7 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent8 && !from.AirPent8) { from.AirPent8 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is AirPent9 && !from.AirPent9) { from.AirPent9 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            //   else { this.Say("You have already given me that pentagram piece."); }

            return base.OnDragDrop(from, dropped);
        }


        private bool checkIfDone(Mobile from, Item dropped)
        {
            if (from.AirPent1 == true && from.AirPent2 == true && from.AirPent3 == true && from.AirPent4 == true && from.AirPent5 == true && from.AirPent6 == true && from.AirPent7 == true && from.AirPent8 == true && from.AirPent9 == true)
            {
                return true;
            }
            return false;
        }

        private void allTurnedIn(Mobile from)
        {
            from.CloseGump(typeof(AirLordGump));
            from.SendGump(new AirLordGump(from));
        }

        public override void GenerateLoot()
        {
        }

        public override bool DisallowAllMoves
        {
            get
            {
                return base.DisallowAllMoves;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }
    }
}