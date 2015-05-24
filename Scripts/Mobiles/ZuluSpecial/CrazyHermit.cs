using System;
using Server.Items;

namespace Server.Mobiles
{
    public class CrazyHermit : BaseCreature
    {
        [Constructable]
        public CrazyHermit()
            : base(AIType.AI_Hermit, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            
            this.Hue = Utility.RandomSkinHue();

            this.Body = 0x190;
            this.Name = "crazy hermit";
            this.AddItem(new Robe(Utility.RandomNeutralHue()));
            this.AddItem(new QuarterStaff());
            this.AddItem(new Boots(Utility.RandomNeutralHue()));
          
            this.SetStr(216, 305);
            this.SetDex(96, 115);
            this.SetInt(966, 1045);

            this.SetHits(560, 595);

            this.SetDamage(15, 27);
            
            this.SetDamageType(ResistanceType.Physical, 20);
            this.SetDamageType(ResistanceType.Cold, 40);
            this.SetDamageType(ResistanceType.Energy, 40);

            this.SetResistance(ResistanceType.Physical, 55, 65);
            this.SetResistance(ResistanceType.Fire, 25, 30);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 50, 60);
            this.SetResistance(ResistanceType.Energy, 25, 30);

            this.SetSkill(SkillName.EvalInt, 120.1, 130.0);
            this.SetSkill(SkillName.Magery, 120.1, 130.0);
            this.SetSkill(SkillName.Meditation, 100.1, 101.0);
            this.SetSkill(SkillName.Poisoning, 100.1, 101.0);
            this.SetSkill(SkillName.MagicResist, 175.2, 200.0);
            this.SetSkill(SkillName.Tactics, 90.1, 100.0);
            this.SetSkill(SkillName.Wrestling, 75.1, 100.0);
            this.SetSkill(SkillName.Necromancy, 120.1, 130.0);
            this.SetSkill(SkillName.SpiritSpeak, 120.1, 130.0);

            this.Fame = 0;
            this.Karma = 0;



            Utility.AssignRandomFacialHair(this);
            Utility.AssignRandomHair(this);
        }

        public CrazyHermit(Serial serial)
            : base(serial)
        {
        }

        public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return false;
            }
        }
        public override void GenerateLoot()
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}