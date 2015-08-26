using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Weapons.Ranged
{
    public class IceArrow : Item, ICommodity
    {
        [Constructable]
        public IceArrow()
            : this(1)
        {
            this.Name = "Ice arrow";
        }

        [Constructable]
        public IceArrow(int amount)
            : base(0xF3F)
        {
            this.Name = "Ice arrows";
            this.Hue = 0x0492;
            this.Stackable = true;
            this.Amount = amount;
        }

        public IceArrow(Serial serial)
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