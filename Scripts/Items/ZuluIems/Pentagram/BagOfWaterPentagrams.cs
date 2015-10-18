using Server.Items.ZuluIems.Pentagram.Water;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Pentagram
{
    public class BagOfWaterPentagrams : Bag
    {
        [Constructable]
        public BagOfWaterPentagrams()
        {
            // Water
            this.Hue = 1167;
            this.DropItem(new WaterPent1());
            this.DropItem(new WaterPent2());
            this.DropItem(new WaterPent3());
            this.DropItem(new WaterPent4());
            this.DropItem(new WaterPent5());
            this.DropItem(new WaterPent6());
            this.DropItem(new WaterPent7());
            this.DropItem(new WaterPent8());
            this.DropItem(new WaterPent9());

        }

        public BagOfWaterPentagrams(Serial serial)
            : base(serial)
        {
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