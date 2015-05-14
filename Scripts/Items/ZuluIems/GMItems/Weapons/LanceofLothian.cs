using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.GMItems.Weapons
{
    public class LanceofLothian : Spear
    {
        [Constructable]
        public LanceofLothian()
        {
            Name = ("Lance of Lothian");

            IdHue = 1171;	
					
            Attributes.WeaponSpeed = 50; // dunno what is better, is lower faster or what?
            Attributes.WeaponDamage = 50;
        }

        public LanceofLothian(Serial serial)
            : base(serial)
        {
        }

        public override int InitMinHits
        {
            get
            {
                return 255;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 255;
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