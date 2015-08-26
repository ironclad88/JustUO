using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.ZuluMonsters
{
    [CorpseName("a skeletal archer corpse")]
    public class SkeletonArcher : BaseCreature
    {
        [Constructable]
        public SkeletonArcher()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a skeleton archer";
            this.Body = Utility.RandomList(50, 56);
            this.BaseSoundID = 0x48D;

            this.SetStr(100);
            this.SetDex(1500);
            this.SetInt(25);

            this.SetHits(50);
            this.SetStam(50);

            this.SetDiceDmg(4, 4);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetSkill(SkillName.Anatomy, 70, 90);
            this.SetSkill(SkillName.Archery, 50, 90);
            this.SetSkill(SkillName.MagicResist, 65.1, 90.0);
            this.SetSkill(SkillName.Tactics, 50.1, 75.0);
            this.SetSkill(SkillName.Wrestling, 50.1, 75.0);

            this.Fame = 450;
            this.Karma = -450;

            this.VirtualArmor = 16;

            this.AddItem(new Bow());
            this.PackItem(new Arrow(Utility.RandomMinMax(40, 60))); // added arrows to the archers loot
        }

        public SkeletonArcher(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune // hmmm didnt know this existed
        {
            get
            {
                return Poison.Deadly;
            }
        }
        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.FeyAndUndead;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Poor);
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