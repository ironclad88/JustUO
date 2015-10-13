using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.ElementalGear.Water.Plate
{
    [FlipableAttribute(0x1411, 0x141a)]
    public class WaterPlateLegs : BaseArmor
    {
        [Constructable]
        public WaterPlateLegs()
            : base(0x1411)
        {
            this.Weight = 7.0;
            this.Hue = 1167;
            this.ColdBonus = 20;
            this.Name = "Plate legs of the Water Element";
        }

        public WaterPlateLegs(Serial serial)
            : base(serial)
        {
        }
        public override int Dexpenalty
        {
            get
            {
                return 0;
            }
        }

        public override int BasePhysicalResistance
        {
            get
            {
                return 5;
            }
        }
        public override int BaseFireResistance
        {
            get
            {
                return 3;
            }
        }
        public override int BaseColdResistance
        {
            get
            {
                return 2;
            }
        }
        public override int BasePoisonResistance
        {
            get
            {
                return 3;
            }
        }
        public override int BaseEnergyResistance
        {
            get
            {
                return 2;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 50;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 65;
            }
        }
        public override int AosStrReq
        {
            get
            {
                return 90;
            }
        }
        public override int OldStrReq
        {
            get
            {
                return 60;
            }
        }
        public override int OldDexBonus
        {
            get
            {
                return 0;
            }
        }
        public override int ArmorBase
        {
            get
            {
                return 50;
            }
        }
        public override ArmorMaterialType MaterialType
        {
            get
            {
                return ArmorMaterialType.Plate;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}