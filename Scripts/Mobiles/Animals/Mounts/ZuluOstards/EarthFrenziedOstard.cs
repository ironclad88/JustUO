using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.Animals.Mounts.ZuluOstards
{
    [CorpseName("an earth frenzied ostard corpse")]
    public class EarthFrenziedOstard : BaseMount
    {
        [Constructable]
        public EarthFrenziedOstard()
            : this("a earth frenzied ostardd")
        {
        }

        [Constructable]
        public EarthFrenziedOstard(string name)
            : base(name, 0xDA, 0x3EA5, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Hue = 1183;

            this.BaseSoundID = 0x270;

            this.SetStr(250, 300);
            this.SetDex(250, 300);
            this.SetInt(300, 400);

            this.SetHits(250, 300);
            this.SetMana(450);

            this.SetDamage(35, 50);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 15, 20);

            this.SetSkill(SkillName.MagicResist, 100, 120);
            this.SetSkill(SkillName.Tactics, 100, 120);
            this.SetSkill(SkillName.Wrestling, 100, 120);

            this.Fame = 450;
            this.Karma = 0;

            this.Tamable = true;
            this.ControlSlots = 1;
            this.MinTameSkill = 120;
        }

        public EarthFrenziedOstard(Serial serial)
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
                return FoodType.Meat;
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