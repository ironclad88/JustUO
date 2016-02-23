using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.ZuluMonsters
{
    [CorpseName("an dracoliche corpse")]
    public class Dracoliche : BaseCreature
    {
        [Constructable]
        public Dracoliche()
            : base(AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            this.Name = "a dracoliche wisp";
            this.Body = 0xc;
            this.BaseSoundID = 466;

            this.Hue = 1282;

            this.SetStr(600, 650);
            this.SetDex(700, 750);
            this.SetInt(150, 160);

            this.SetMana(700, 750);
            this.SetHits(600, 650);

            this.SetDamage(50, 80);

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

            this.MagicLevel = 5;
            this.LootIndex = 4;

            this.VirtualArmor = 60;
            this.ControlSlots = 2;
        }

        public Dracoliche(Serial serial)
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