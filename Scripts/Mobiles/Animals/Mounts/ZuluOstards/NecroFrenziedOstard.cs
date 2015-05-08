using System;

namespace Server.Mobiles
{
    [CorpseName("an necro ostard corpse")]
    public class NecroFrenziedOstard : BaseMount
    {
        [Constructable]
        public NecroFrenziedOstard()
            : this("a necro frenzied ostard")
        {
        }

        [Constructable]
        public NecroFrenziedOstard(string name)
            : base(name, 0xDA, 0x3EA4, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Hue = 1282;

            this.BaseSoundID = 0x275;

            this.SetStr(300, 335);
            this.SetDex(300, 350);
            this.SetInt(200, 250);

            this.SetHits(300, 335);
            this.SetMana(250);

            this.SetDamage(40, 60);

            this.SetDamageType(ResistanceType.Physical, 130);

            this.SetResistance(ResistanceType.Physical, 50, 70);
            this.SetResistance(ResistanceType.Fire, 50, 80);
            this.SetResistance(ResistanceType.Poison, 50, 80);
            this.SetResistance(ResistanceType.Energy, 50, 80);

            this.SetSkill(SkillName.MagicResist, 120, 120);
            this.SetSkill(SkillName.Tactics, 110, 110);
            this.SetSkill(SkillName.Wrestling, 120, 120);
            this.SetSkill(SkillName.Magery, 130, 130);
            this.SetSkill(SkillName.Necromancy, 130, 130);
            this.SetSkill(SkillName.EvalInt, 130, 130);

            this.Fame = 1500;
            this.Karma = -1500;

            this.Tamable = true;
            this.ControlSlots = 1;
            this.MinTameSkill = 125;
        }

        public NecroFrenziedOstard(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 3;
            }
        }
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies;
            }
        }
        public override PackInstinct PackInstinct
        {
            get
            {
                return PackInstinct.Ostard;
            }
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