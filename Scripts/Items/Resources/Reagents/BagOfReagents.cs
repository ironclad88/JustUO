using Server.Items.Resources.Reagents.ZuluReags;
using System;

namespace Server.Items
{
    public class BagOfReagents : Bag
    {
        [Constructable]
        public BagOfReagents()
            : this(50)
        {
        }

        [Constructable]
        public BagOfReagents(int amount)
        {
            this.DropItem(new BlackPearl(amount));
            this.DropItem(new Bloodmoss(amount));
            this.DropItem(new Garlic(amount));
            this.DropItem(new Ginseng(amount));
            this.DropItem(new MandrakeRoot(amount));
            this.DropItem(new Nightshade(amount));
            this.DropItem(new SulfurousAsh(amount));
            this.DropItem(new SpidersSilk(amount));

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

        public BagOfReagents(Serial serial)
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