using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Skill_Items.Magical.Scrolls.ZuluScrolls.Holy
{
    public class TrialbyFireScroll : SpellScroll
    {
        [Constructable]
        public TrialbyFireScroll()
            : this(1)
        {
        }

        [Constructable]
        public TrialbyFireScroll(int amount)
            : base(701, 0x1f3c, amount)
        {
            this.Hue = 0x49E;
            this.Name = "Trial by Fire scroll";
        }

        public TrialbyFireScroll(Serial serial)
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