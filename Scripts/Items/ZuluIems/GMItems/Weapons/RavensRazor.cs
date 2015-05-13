using Server.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.GMItems.Weapons
{
    public class RavensRazor : Katana
    {
        [Constructable]
        public RavensRazor()
        {
            Name = ("Raven`s Razor");

            IdHue = 1179;
            Attributes.WeaponSpeed = 40; // dunno what is better, is lower faster or what?
            Attributes.WeaponDamage = 60;
        }

        public RavensRazor(Serial serial)
            : base(serial)
        {
        }

        public override int InitMinHits
        {
            get
            {
                return 200;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 200;
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