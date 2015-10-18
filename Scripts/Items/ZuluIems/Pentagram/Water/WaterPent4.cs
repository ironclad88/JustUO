﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Pentagram.Water
{
    public class WaterPent4 : Item
    {
        [Constructable]
        public WaterPent4()
            : base(4073)
        {
            this.Weight = 1.0;
            this.Name = "Water Pentagram Piece 4";
            this.Hue = 1167;
        }

        public WaterPent4(Serial serial)
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