using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.GMItems.Clothing
{
    public class RobeoftheCleric : BaseOuterTorso
    {
        [Constructable]
        public RobeoftheCleric()
            : base(12217)
        {
            this.Name = "Robe of the Cleric";
            this.Weight = 10.0;

            this.SkillBonuses.SetValues(0, SkillName.SpiritSpeak, 6.0);
            this.SkillBonuses.SetValues(1, SkillName.Healing, 6.0);
        }

        public RobeoftheCleric(Serial serial)
            : base(serial)
        {
        }

        public override int ArmorBase
        {
            get
            {
                return 20;
            }
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

            if (this.Weight == 1.0)
                this.Weight = 10.0;
        }
    }
}