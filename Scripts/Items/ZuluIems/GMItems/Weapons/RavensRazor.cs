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
            Attributes.WeaponSpeed = 50; // dunno what is better, is lower faster or what?
            Attributes.WeaponDamage = 65;
            this.Attributes.SpellChanneling = 1;
            this.DamageLevel = WeaponDamageLevel.DMG40;
        }

        public RavensRazor(Serial serial)
            : base(serial)
        {
        }

        public override int AosHitSound
        {
            get
            {
                return 0x238;
            }
        }

        public override int DefHitSound
        {
            get
            {
                return 0x238;
            }
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