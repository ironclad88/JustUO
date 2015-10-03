using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a golden dragon corpse")]
    public class GoldenDragon : BaseCreature
    {
        [Constructable]
        public GoldenDragon()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a golden dragon";
            this.Hue = 48;
           // this.Body = Utility.RandomList(12, 59);
            this.Body = 59;
            this.BaseSoundID = 362;

            this.SetStr(1450);
            this.SetDex(650);
            this.SetInt(700);

            this.SetHits(2000);
            this.SetMana(1000);
            this.SetStam(200);

            this.SetDamage(30, 40);

            this.SetDamageType(ResistanceType.Physical, 70);
            this.SetDamageType(ResistanceType.Fire, 30);

            this.SetResistance(ResistanceType.Physical, 30, 40);
            this.SetResistance(ResistanceType.Fire, 100, 120);
            this.SetResistance(ResistanceType.Cold, 30, 40);
            this.SetResistance(ResistanceType.Poison, 80, 95);
            this.SetResistance(ResistanceType.Energy, 80, 95);

            this.SetSkill(SkillName.Anatomy, 100, 150);
            this.SetSkill(SkillName.EvalInt, 100, 150);
            this.SetSkill(SkillName.Poisoning, 100, 150);
            this.SetSkill(SkillName.MagicResist, 100, 150);
            this.SetSkill(SkillName.Tactics, 100, 150);
            this.SetSkill(SkillName.Wrestling, 100, 150);

            this.Fame = 24000;
            this.Karma = -24000;

            this.VirtualArmor = 130;

            this.PackItem(new LesserPoisonPotion());

            this.MagicLevel = 8;

        }

        public GoldenDragon(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override bool HasBreath
        {
            get
            {
                return true;
            }
        }// fire breath enabled
        public override bool AutoDispel
        {
            get
            {
                return !this.Controlled;
            }
        }
        public override bool CanAngerOnTame
        {
            get
            {
                return true;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 6;
            }
        }
        public override int Meat
        {
            get
            {
                return 10;
            }
        }
        public override int Hides
        {
            get
            {
                return 20;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Horned;
            }
        }
        public override bool StatLossAfterTame
        {
            get
            {
                return true;
            }
        }
        public override bool CanFly
        {
            get
            {
                return true;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich, 6);
        }

        public override int GetAttackSound()
        {
            return 713;
        }

        public override int GetAngerSound()
        {
            return 718;
        }

        public override int GetDeathSound()
        {
            return 716;
        }

        public override int GetHurtSound()
        {
            return 721;
        }

        public override int GetIdleSound()
        {
            return 725;
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