﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Pentagram.Water
{
    public class AirPent6 : Item
    {
        [Constructable]
        public AirPent6()
            : base(4075)
        {
            this.Weight = 1.0;
            this.Name = "Air Pentagram Piece 6";
            this.Hue = 1161;
        }

        public AirPent6(Serial serial)
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