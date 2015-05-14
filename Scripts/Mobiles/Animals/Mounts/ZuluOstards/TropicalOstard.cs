using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.Animals.Mounts.ZuluOstards
{
    [CorpseName("an tropical ostard corpse")]
    public class TropicalOstard : BaseMount
    {
        [Constructable]
        public TropicalOstard()
            : this("a tropical ostardd")
        {
        }

        [Constructable]
        public TropicalOstard(string name)
            : base(name, 0xDB, 0x3EA5, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Hue = 1155;

            this.BaseSoundID = 0x270;

            this.SetStr(75, 95);
            this.SetDex(110, 140);
            this.SetInt(100, 150);

            this.SetHits(120, 130);
            this.SetMana(300);

            this.SetDamage(8, 14);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 15, 20);

            this.SetSkill(SkillName.MagicResist, 27.1, 32.0);
            this.SetSkill(SkillName.Tactics, 29.3, 44.0);
            this.SetSkill(SkillName.Wrestling, 29.3, 44.0);

            this.Fame = 450;
            this.Karma = 0;

            this.Tamable = true;
            this.ControlSlots = 1;
            this.MinTameSkill = 105;
        }

        public TropicalOstard(Serial serial)
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