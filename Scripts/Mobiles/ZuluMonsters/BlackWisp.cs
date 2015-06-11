using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an black wisp corpse")]
    public class BlackWisp : BaseCreature
    {
        [Constructable]
        public BlackWisp()
            : base(AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            this.Name = "an black wisp";
            this.Body = 0x3a;
            this.BaseSoundID = 466;

            this.Hue = 0x0455;

            this.SetStr(200, 230);
            this.SetDex(200, 250);
            this.SetInt(100, 120);

            this.SetMana(120, 150);
            this.SetHits(210, 211);

            this.SetDamage(20, 70);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 50, 60);
            this.SetResistance(ResistanceType.Fire, 50, 60);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 50, 60);
            this.SetResistance(ResistanceType.Energy, 50, 60);

            this.SetSkill(SkillName.MagicResist, 200, 210);
            this.SetSkill(SkillName.Tactics, 100, 101);
            this.SetSkill(SkillName.Wrestling, 100, 110);
            this.SetSkill(SkillName.EvalInt, 200, 210);
            this.SetSkill(SkillName.Magery, 200, 210);

            this.Fame = 3500;
            this.Karma = -3500;

            this.VirtualArmor = 35;
            this.ControlSlots = 2;
        }

        public BlackWisp(Serial serial)
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
        public override int TreasureMapLevel // not done (dunno what it even means)
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot() // not done
        {
            this.AddLoot(LootPack.Average);
            this.AddLoot(LootPack.Meager);
            this.AddLoot(LootPack.Gems);
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