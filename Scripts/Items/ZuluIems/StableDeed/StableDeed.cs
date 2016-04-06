using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.ZuluIems.AnimalTicket
{
    class StableDeed : Item
    {
        public StableDeed(BaseCreature pet)
            : base(0x14F0)
        {
            this.Name = "Pet claim ticket - " + pet.stableName; // name is not done
            this.Weight = 0.1;
            this.Hue = 1160;
            this.petz = pet;

            pet.IsStabled = true;

            pet.Internalize();
            pet.SetControlMaster(null);
            pet.ControlOrder = OrderType.Stay;
            pet.SummonMaster = null;
            pet.IsStabled = true;
        }

        public StableDeed(Serial serial)
            : base(serial)
        {
        }
        
        private BaseCreature petz;

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseCreature pet
        {
            get
            {
                return this.petz;
            }
            set
            {
                this.petz = value;
            }
        }

      
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write((Mobile)pet);
        }

        public void redeed(Mobile from)
        {
            petz.SetControlMaster(from);
            petz.IsStabled = false;
            petz.MoveToWorld(from.Location, from.Map);

            petz.ControlTarget = from;
            petz.ControlOrder = OrderType.Follow;


            petz.StabledBy = null;

            petz = null;
            this.Delete(); // deletes the deed
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    pet = (BaseCreature)reader.ReadMobile();
                    break;
            }
            
            if (this.Weight == 0.0)
                this.Weight = 1.0;
        }

        public override void OnDoubleClick(Mobile from)
        {

        }

    }
}