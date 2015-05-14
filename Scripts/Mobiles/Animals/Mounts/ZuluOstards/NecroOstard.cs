using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.Animals.Mounts.ZuluOstards
{
    [CorpseName("an necro ostard corpse")]
    public class NecroOstard : BaseMount
    {
        [Constructable]
        public NecroOstard()
            : this("a necro ostardd")
        {
        }

        [Constructable]
        public NecroOstard(string name)
            : base(name, 0xDB, 0x3EA5, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Hue = 1282;

            this.BaseSoundID = 0x270;

            this.SetStr(200, 250);
            this.SetDex(200, 250);
            this.SetInt(200, 250);

            this.SetHits(200, 250);
            this.SetMana(500);

            this.SetDamage(16, 35);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 15, 20);

            this.SetSkill(SkillName.MagicResist, 27.1, 32.0);
            this.SetSkill(SkillName.Tactics, 29.3, 44.0);
            this.SetSkill(SkillName.Wrestling, 29.3, 44.0);

            this.Fame = 450;
            this.Karma = 0;

            this.Tamable = true;
            this.ControlSlots = 1;
            this.MinTameSkill = 110;
        }

        public NecroOstard(Serial serial)
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