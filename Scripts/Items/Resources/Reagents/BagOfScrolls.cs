using Server.Items.Resources.Reagents.ZuluReags;
using Server.Items.Skill_Items.Magical.Scrolls.ZuluScrolls.Necro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Resources.Reagents
{
    class BagOfScrolls : Bag
    {
    [Constructable]
        public BagOfScrolls()
            : this(50)
        {
        }

        [Constructable]
        public BagOfScrolls(int amount)
        {
            this.DropItem(new AntidoteScroll(50));
            this.DropItem(new CallLightningScroll(50));
            this.DropItem(new EarthPortalScroll(50));
            this.DropItem(new EarthBlessScroll(50));
            this.DropItem(new GustofairScroll(50));
            this.DropItem(new NaturesTouchScroll(50));
            this.DropItem(new OwlsightScroll(50));
            this.DropItem(new ShiftingEarthScroll(50));
            this.DropItem(new SummonMammalsScroll(50));
        }

        public BagOfScrolls(Serial serial)
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