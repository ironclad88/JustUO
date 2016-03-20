using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Weapons.Ranged
{
    public class ThunderBolt : Item, ICommodity
    {
        [Constructable]
        public ThunderBolt()
            : this(1)
        {
        }

        [Constructable]
        public ThunderBolt(int amount)
            : base(0x1BFB)
        {
            this.Name = "Thunder Bolt";
            this.Hue = 0x502;
            this.Stackable = true;
            this.Amount = amount;
        }

        public ThunderBolt(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }
        int ICommodity.DescriptionNumber
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return true;
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