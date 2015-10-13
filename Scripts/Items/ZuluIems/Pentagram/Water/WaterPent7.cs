using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Pentagram.Water
{
    public class WaterPent7 : Item
    {
        [Constructable]
        public WaterPent7()
            : base(4076)
        {
            this.Weight = 1.0;
            this.Name = "Water Pentagram Piece 7";
            this.Hue = 1167;
        }

        public WaterPent7(Serial serial)
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