using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an behemoth corpse")]
    public class Behemoth : BaseCreature
    {
        [Constructable]
        public Behemoth()
            : base(AIType.AI_Melee, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            this.Name = "an behemoth";
            this.Body = 0x0e;
            this.BaseSoundID = 268;

            this.Hue = 0x488;

            this.SetStr(200, 230);
            this.SetDex(200, 250);
            this.SetInt(45, 50);

            this.SetHits(210, 211);

            this.SetDamage(20, 70);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 50, 60);
            this.SetResistance(ResistanceType.Fire, 50, 60);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 50, 60);
            this.SetResistance(ResistanceType.Energy, 50, 60);

            this.SetSkill(SkillName.MagicResist, 60, 65);
            this.SetSkill(SkillName.Tactics, 150, 155);
            this.SetSkill(SkillName.Wrestling, 175, 180);

            this.Fame = 3500;
            this.Karma = -3500;

            this.VirtualArmor = 50;
            this.ControlSlots = 2;
        }

        public Behemoth(Serial serial)
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
            this.AddLoot(LootPack.AosUltraRich);
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