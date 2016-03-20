using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.Weapons.Ranged
{
    [FlipableAttribute(0x13B2, 0x13B1)]
    public class ThunderHeavyCrossbow : BaseRanged
    {
        [Constructable]
        public ThunderHeavyCrossbow()
            : base(0x13FD)
        {
            this.Name = "Thunder Heavy Crossbow";
            this.Weight = 3.0;
            this.Hue = 0x502;
            this.Layer = Layer.TwoHanded;
            Dice_Num = 5;
            Dice_Sides = 5;
            Dice_Offset = 14;
        }

        public ThunderHeavyCrossbow(Serial serial)
            : base(serial)
        {
        }

        public override int EffectID
        {
            get
            {
                return 0x1BFE;
            }
        }
        public override Type AmmoType
        {
            get
            {
                return typeof(ThunderBolt);
            }
        }
        public override Item Ammo
        {
            get
            {
                return new ThunderBolt();
            }
        }
        public override WeaponAbility PrimaryAbility
        {
            get
            {
                return WeaponAbility.MovingShot;
            }
        }
        public override WeaponAbility SecondaryAbility
        {
            get
            {
                return WeaponAbility.Dismount;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 80;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return Core.ML ? 20 : 20;
            }
        }
        public override int AosMaxDamage
        {
            get
            {
                return Core.ML ? 24 : 24;
            }
        }
        public override int AosSpeed
        {
            get
            {
                return 22;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 5.00f;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 40;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 11;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 56;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 10;
            }
        }
        public override int DefMaxRange
        {
            get
            {
                return 8;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 31;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 100;
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