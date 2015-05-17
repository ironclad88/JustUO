using Server.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;

namespace Server.Items.__Misc.Zuluitems
{
    public class TrashcanofWonder : Container
    {
        [Constructable]
        public TrashcanofWonder()
            : base(0xE77)
        {
            this.Name = "Trashcan of wonders";
            this.Hue = 0x512;
            this.Movable = false;
        }

        public TrashcanofWonder(Serial serial)
            : base(serial)
        {
        }

        public override int DefaultMaxWeight
        {
            get
            {
                return 0;
            }
        }// A value of 0 signals unlimited weight
        public override bool IsDecoContainer
        {
            get
            {
                return false;
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (!base.OnDragDrop(from, dropped))
                return false;

            RandomClass rnd = new RandomClass();
            var eastereggroll = rnd.D100Roll(1);
            if (eastereggroll >= 5)
            {
                from.SendMessage("I found something!");
                var D6Roll = rnd.D6Roll(1);
                switch (D6Roll)
                {
                    case 1:
                        from.AddItem(new FrenziedOstardEgg());
                        break;
                    case 2:
                        from.AddItem(new BigFish());
                        break;
                    case 3:
                        from.AddItem(new OstardEgg());
                        break;
                    case 4:
                        from.AddItem(new Gold(Utility.RandomMinMax(300, 2000)));
                        break;
                    case 5:
                        from.AddItem(new Gold(Utility.RandomMinMax(900, 4000)));
                        break;
                    case 6:
                        from.AddItem(new Bottle());
                        break;
                    default:
                        break;
                }
            }
            dropped.Delete();

            return true;
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {
            if (!base.OnDragDropInto(from, item, p))
                return false;

            RandomClass rnd = new RandomClass();
            var eastereggroll = rnd.D100Roll(1);
            if (eastereggroll == 66)
            {
                from.SendMessage("I found something!");
                var D6Roll = rnd.D6Roll(1);
                switch (D6Roll)
                {
                    case 1:
                        from.AddItem(new FrenziedOstardEgg());
                        break;
                    case 2:
                        from.AddItem(new BigFish());
                        break;
                    case 3:
                        from.AddItem(new OstardEgg());
                        break;
                    case 4:
                        from.AddItem(new Gold(Utility.RandomMinMax(300, 2000)));
                        break;
                    case 5:
                        from.AddItem(new Gold(Utility.RandomMinMax(900, 4000)));
                        break;
                    case 6:
                        from.AddItem(new Bottle());
                        break;
                    default:
                        break;
                }
            }
            item.Delete();

            return true;
        }
    }
}