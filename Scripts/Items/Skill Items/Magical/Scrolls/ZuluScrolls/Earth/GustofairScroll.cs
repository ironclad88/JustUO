using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Skill_Items.Magical.Scrolls.ZuluScrolls.Necro
{
    public class GustofairScroll : SpellScroll
    {
        [Constructable]
        public GustofairScroll()
            : this(1)
        {
        }

        [Constructable]
        public GustofairScroll(int amount)
            : base(89, 0x1f3c, amount)
        {
            this.Hue = 1159;
            this.Name = "Gust of air scroll";
        }

        public GustofairScroll(Serial serial)
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