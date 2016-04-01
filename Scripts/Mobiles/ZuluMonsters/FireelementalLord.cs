using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an fire elemental lord corpse")]
    public class FireelementalLord : BaseCreature
    {
        [Constructable]
        public FireelementalLord()   // using this mob as an template right now (All stats/skills and everything is taken from Fantasia scripts)
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4) // 0.2 is standard apparently
        //  : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.4) // not sure about 0.4 or 02, havent had time to test
        {
            this.Name = "an fire elemental lord";
            this.Body = 0x0f;
            this.BaseSoundID = 268;

            this.Hue = 137;

            this.SetStr(300);
            this.SetDex(300);
            this.SetInt(400);

            this.SetHits(200);
            this.SetMana(900);
            this.SetStam(200);

            this.SetDiceDmg(5, 6);

            this.SetResistanceLevel(ResistanceType.Physical, 3);
            this.SetResistanceLevel(ResistanceType.Earth, 0);
            this.SetResistanceLevel(ResistanceType.Fire, 8);
            this.SetResistanceLevel(ResistanceType.Cold, 0); // Cold == Water
            this.SetResistanceLevel(ResistanceType.Necro, 0);

            
            this.MagicLevel = 4;
            this.LootIndex = 4;

            this.SetSkill(SkillName.Magery, 100);
            this.SetSkill(SkillName.MagicResist, 75);
            this.SetSkill(SkillName.Tactics, 130);
            this.SetSkill(SkillName.Wrestling, 175);

            this.Fame = 3500; // Fame and Karma are not defined in npcdesc.cfg (the cfg file which contains all mobs), gotta find the script for this somewhere.
            this.Karma = -3500;

            this.VirtualArmor = 50;
            //  this.ControlSlots = 2;
        }

        public FireelementalLord(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 117.5;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 45.0;
            }
        }
        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune // hmmm didnt know this existed, added
        {
            get
            {
                return Poison.Deadly;
            }
        }
        public override int TreasureMapLevel // not done (dunno what it even means)
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot() // not done
        {
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
        }
    }
}