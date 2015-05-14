using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Resources.Reagents.ZuluReags
{
    public class BlackMoor : BaseReagent, ICommodity
    {
        [Constructable]
        public BlackMoor()
            : this(1)
        {
        }

        [Constructable]
        public BlackMoor(int amount)
            : base(0x0F79, amount)
        {
        }

        public BlackMoor(Serial serial)
            : base(serial)
        {
        }

        int ICommodity.DescriptionNumber
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return true;
            }
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