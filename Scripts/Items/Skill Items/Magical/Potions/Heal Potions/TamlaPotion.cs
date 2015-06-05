using System;

namespace Server.Items
{
    public class TamlaPotion : BaseHealPotion
    {

        [Constructable]
        public TamlaPotion()
            : base(PotionEffect.Tamla)
        {
            this.Hue = 155;
        }

        public TamlaPotion(Serial serial)
            : base(serial)
        {
            this.Hue = 155;
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            string name = "Tamla Potion";
            if (this.Amount > 1)
                list.Add(this.Amount + " " + name + "'s");
            else
                list.Add(name);
        }

        public override int MinHeal
        {
            get
            {
                return 50000;
            }
        }
        public override int MaxHeal
        {
            get
            {
                return 50000;
            }
        }
        public override double Delay
        {
            get
            {
                return 10.0;
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