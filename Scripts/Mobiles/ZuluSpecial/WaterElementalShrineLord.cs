using Server.Gumps.Zulugumps.ElementalLords;
using Server.Items;
using Server.Items.ZuluIems.Pentagram.Water;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.ZuluSpecial
{

    [CorpseName("a water elemental lord corpse")]
    public class WaterElementalShrineLord : BaseCreature
    {
        [Constructable]
        public WaterElementalShrineLord()
            : base(AIType.AI_Elemental, FightMode.None, 0, 0, 0, 0)
        {
            this.Name = "the Water Element Shrine Lord";
            this.Body = 0x10;
            this.BaseSoundID = 278;

            this.Hue = 1167;

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

        public WaterElementalShrineLord(Serial serial)
            : base(serial)
        {
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {

            if (from == null || dropped == null)
                return false;

            if (dropped is WaterPent1 && !from.WaterPent1) { from.WaterPent1 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } } 
            if (dropped is WaterPent2 && !from.WaterPent2) { from.WaterPent2 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent3 && !from.WaterPent3) { from.WaterPent3 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent4 && !from.WaterPent4) { from.WaterPent4 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent5 && !from.WaterPent5) { from.WaterPent5 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent6 && !from.WaterPent6) { from.WaterPent6 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent7 && !from.WaterPent7) { from.WaterPent7 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent8 && !from.WaterPent8) { from.WaterPent8 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
            if (dropped is WaterPent9 && !from.WaterPent9) { from.WaterPent9 = true; dropped.Consume(1); if (checkIfDone(from, dropped)) { allTurnedIn(from); } else { this.Say("Thank you for this part"); } }
         //   else { this.Say("You have already given me that pentagram piece."); }

            return base.OnDragDrop(from, dropped);
        }


        private bool checkIfDone(Mobile from, Item dropped)
        {
            if (from.WaterPent1 == true && from.WaterPent2 == true && from.WaterPent3 == true && from.WaterPent4 == true && from.WaterPent5 == true && from.WaterPent6 == true && from.WaterPent7 == true && from.WaterPent8 == true && from.WaterPent9 == true)
            {
                return true;
            }
            return false;
        }

        private void allTurnedIn(Mobile from)
        {
            from.CloseGump(typeof(WaterLordGump));
            from.SendGump(new WaterLordGump(from));
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