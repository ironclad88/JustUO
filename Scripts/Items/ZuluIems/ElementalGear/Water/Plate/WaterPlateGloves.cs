﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.ElementalGear.Water.Plate
{
    [FlipableAttribute(0x1414, 0x1418)]
    public class WaterPlateGloves : BaseArmor
    {
        [Constructable]
        public WaterPlateGloves()
            : base(0x1414)
        {
            this.Weight = 2.0;
            this.Hue = 1167;
            this.ColdBonus = 20;
            this.Name = "Plate gloves of the Water Element";
            Dexpenalty = 0;
        }

        public WaterPlateGloves(Serial serial)
            : base(serial)
        {
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
                return 70;
            }
        }
        public override int OldStrReq
        {
            get
            {
                return 30;
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

            if (this.Weight == 1.0)
                this.Weight = 2.0;
        }
    }
}