using Server.Items;
using System;
using Server.Spells;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.MagicFish
{
    public class DispelFish : BaseMagicFish
    {
        [Constructable]
        public DispelFish()
            : base(1175)
        {
            this.Stackable = true;
            this.Hue = 1175;

            this.Name = "magic fish of dispel";
        }

        public DispelFish(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1041073;
            }
        }// prized fish

        public override void OnDoubleClick(Mobile m)
        {
            if (!this.IsChildOf(m.Backpack))
            {
                m.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else if (this.Apply(m))
            {

                Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                Effects.PlaySound(m, m.Map, 0x201);
                m.DispelMagicMods();
                //from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 501774); // You swallow the fish whole!
                this.Consume();
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

            if (this.Hue == 151)
                this.Hue = 51;
        }
    }
}