using Server.Items.ZuluIems.Pentagram.Water;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Pentagram
{
    public class BagOfAirPentagrams : Bag
    {
        [Constructable]
        public BagOfAirPentagrams()
        {
            // Air
            this.Hue = 1161;
            this.DropItem(new AirPent1());
            this.DropItem(new AirPent2());
            this.DropItem(new AirPent3());
            this.DropItem(new AirPent4());
            this.DropItem(new AirPent5());
            this.DropItem(new AirPent6());
            this.DropItem(new AirPent7());
            this.DropItem(new AirPent8());
            this.DropItem(new AirPent9());

        }

        public BagOfAirPentagrams(Serial serial)
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