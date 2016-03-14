using System;
using Server.Items.Resources.Reagents.ZuluReags;

namespace Server.Items
{
    public class BagOfNecroReagents : Bag
    {
        [Constructable]
        public BagOfNecroReagents()
            : this(50)
        {
        }

        [Constructable]
        public BagOfNecroReagents(int amount)
        {
            this.DropItem(new BatWing(amount));
            this.DropItem(new VolcanicAsh(amount));
            this.DropItem(new DaemonBlood(amount));
            this.DropItem(new NoxCrystal(amount));
            this.DropItem(new PigIron(amount));

            // new
            this.DropItem(new BlackMoor(amount));
            this.DropItem(new EyeofNewt(amount));
            this.DropItem(new BrimStone(amount));
            this.DropItem(new DragonsBlood(amount));
            this.DropItem(new Obsidian(amount));
            this.DropItem(new Pumice(amount));
            this.DropItem(new SerpentsScales(amount));
            this.DropItem(new VialofBlood(amount));
            this.DropItem(new VolcanicAsh(amount));
            this.DropItem(new WyrmsHeart(amount));
            this.DropItem(new BloodSpawn(amount));
        }

        public BagOfNecroReagents(Serial serial)
            : base(serial)
        {
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