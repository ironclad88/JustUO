using Server.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.GMItems.Weapons
{
    public class BaldersDead : Bow
    {
        [Constructable]
        public BaldersDead()
        {
            Name = ("Balder`s Dead");
            
            IdHue = 1296;
            Attributes.WeaponSpeed = 15; // dunno what is better, is lower faster or what?
            Attributes.WeaponDamage = 45;
        }

        public BaldersDead(Serial serial)
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