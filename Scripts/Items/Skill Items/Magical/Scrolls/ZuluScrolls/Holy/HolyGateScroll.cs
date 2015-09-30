using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Skill_Items.Magical.Scrolls.ZuluScrolls.Holy
{
    public class HolyGateScroll : SpellScroll
    {
        [Constructable]
        public HolyGateScroll()
            : this(1)
        {
        }

        [Constructable]
        public HolyGateScroll(int amount)
            : base(700, 0x1f3c, amount)
        {
            this.Hue = 0x49E;
            this.Name = "Holy gate scroll";
        }

        public HolyGateScroll(Serial serial)
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