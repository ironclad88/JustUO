using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.GMItems.Armor.Ryous
{
    public class GlovesofRyous : BaseArmor
    {
        [Constructable]
        public GlovesofRyous() // stats don´t work... YET
            : base(0x1414)
        {
            this.Name = "Platemail Gloves of Ryous";
            this.Weight = 10.0;
            this.IdHue = 1413;
            this.PhysicalBonus = this.BasePhysicalResistance;
            this.FireBonus = this.BaseFireResistance;
            this.PoisonBonus = this.BasePoisonResistance;
            this.ColdBonus = this.BaseColdResistance;
            this.EnergyBonus = this.BaseEnergyResistance;
            this.EarthBonus = this.BaseEarthResistance;
            this.NecroBonus = this.BaseNecroResistance;
            this.HolyBonus = this.BaseHolyResistance;
        }

        public GlovesofRyous(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance
        {
            get
            {
                return 10;
            }
        }
        public override int BaseFireResistance
        {
            get
            {
                return 0;
            }
        }
        public override int BaseColdResistance
        {
            get
            {
                return 0;
            }
        }
        public override int BasePoisonResistance
        {
            get
            {
                return 0;
            }
        }
        public override int BaseEnergyResistance
        {
            get
            {
                return 0;
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
                return 100;
            }
        }
        public override int AosStrReq
        {
            get
            {
                return 95;
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
                return -6;
            }
        }
        public override int ArmorBase
        {
            get
            {
                return 70;
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
                this.Weight = 10.0;
        }
    }
}