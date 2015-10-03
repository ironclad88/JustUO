using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Skill_Items.Magical.Scrolls.ZuluScrolls.Necro
{
    public class StormSpiritScroll : SpellScroll
    {
        [Constructable]
        public StormSpiritScroll()
            : this(1)
        {
        }

        [Constructable]
        public StormSpiritScroll(int amount)
            : base(96, 0x1f3c, amount)
        {
            this.Hue = 1159;
            this.Name = "Storm spirit scroll";
        }

        public StormSpiritScroll(Serial serial)
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