using System;
using Server.Misc;

namespace Server.Items
{
    public class Token : Item
    {
        [Constructable]
        public Token() : this(1) { }

        [Constructable]
        public Token(int amountFrom, int amountTo) : this(Utility.Random(amountFrom, amountTo)) { }

        [Constructable]
        public Token(int amount)
            : base(0xEED)
        {
            Stackable = true;
            Weight = 0;
            Amount = amount;
            Hue = 1171;
            Name = "CW Token"; // changed by pill
        }
        public Token(Serial serial) : base(serial) { }

        public override int GetDropSound()
        {
            if (Amount <= 1) return 0x2E4;
            else if (Amount <= 5) return 0x2E5;
            else return 0x2E6;
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