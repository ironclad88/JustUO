using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.MagicFish
{
    public class DexFish : BaseMagicFish
    {
        [Constructable]
        public DexFish()
            : base(41)
        {
            this.Stackable = true;
            this.Hue = 41;

            this.Name = "magic fish of agility";
        }

        public DexFish(Serial serial)
            : base(serial)
        {
        }

        public override int Bonus
        {
            get
            {
                return 20;
            }
        }
        public override StatType Type
        {
            get
            {
                return StatType.Dex;
            }
        }
        public override int LabelNumber
        {
            get
            {
                return 1041073;
            }
        }// prized fish
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (this.Hue == 151)
                this.Hue = 51;
        }
    }
}