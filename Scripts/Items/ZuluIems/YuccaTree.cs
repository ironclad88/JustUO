using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems
{
    public class YuccaTree : Item
    {
        [Constructable]
        public YuccaTree()
            : base(0xd38)
        {
            this.Name = "Yucca Tree";
            this.Movable = true;
        }

        public YuccaTree(Serial serial)
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