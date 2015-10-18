using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps.Zulugumps
{

    public class ReagGump : Gump
    {


        public ReagGump(Mobile owner)
            : base(100, 0)
        {

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);

            this.AddBackground(34, 30, 350, 554, 9200);
            this.AddLabel(175, 40, 0, @"Reagents");
            this.AddLabel(175, 231, 0, @"Pagans");
            this.AddImage(51, 77, 3976);
            this.AddImage(51, 77, 3976);
            this.AddImage(51, 119, 3980);
            this.AddLabel(93, 78, 0, @"New Label");
            this.AddLabel(94, 115, 0, @"New Label");
            this.AddImage(52, 155, 3981);
            this.AddImage(50, 195, 3974);
            this.AddImage(241, 78, 3973);
            this.AddImage(251, 115, 3972);
            this.AddImage(250, 156, 3963);
            this.AddImage(247, 196, 3962);
            this.AddImage(254, 208, 3962);
            this.AddImage(55, 258, 3971);
            this.AddImage(58, 298, 3975);
            this.AddImage(60, 326, 3977);
            this.AddImage(58, 359, 3978);
            this.AddImage(246, 264, 3979);
            this.AddImage(255, 292, 3985);
            this.AddImage(255, 331, 3984);
            this.AddImage(250, 364, 3983);
            this.AddImage(66, 399, 3970);
            this.AddImage(255, 404, 3960);
            this.AddImage(57, 442, 3966);
            this.AddImage(261, 443, 3969);
            this.AddImage(252, 481, 3979);
            this.AddImage(68, 475, 3982);
            this.AddImage(68, 509, 3964);
            this.AddImage(254, 513, 3967);
            this.AddImage(253, 547, 3968);
            this.AddImage(63, 541, 3965);

            this.AddLabel(93, 153, 0, @"New Label");
            this.AddLabel(95, 194, 0, @"New Label");
            this.AddLabel(290, 76, 0, @"New Label");
            this.AddLabel(287, 113, 0, @"New Label");
            this.AddLabel(287, 153, 0, @"New Label");
            this.AddLabel(293, 194, 0, @"New Label");
            this.AddLabel(97, 258, 0, @"New Label");
            this.AddLabel(96, 292, 0, @"New Label");
            this.AddLabel(96, 324, 0, @"New Label");
            this.AddLabel(98, 361, 0, @"New Label");
            this.AddLabel(105, 402, 0, @"New Label");
            this.AddLabel(104, 436, 0, @"New Label");
            this.AddLabel(104, 468, 0, @"New Label");
            this.AddLabel(106, 505, 0, @"New Label");
            this.AddLabel(297, 263, 0, @"New Label");
            this.AddLabel(296, 297, 0, @"New Label");
            this.AddLabel(296, 329, 0, @"New Label");
            this.AddLabel(298, 366, 0, @"New Label");
            this.AddLabel(298, 403, 0, @"New Label");
            this.AddLabel(297, 437, 0, @"New Label");
            this.AddLabel(297, 469, 0, @"New Label");
            this.AddLabel(299, 506, 0, @"New Label");
            this.AddLabel(298, 544, 0, @"New Label");
            this.AddLabel(103, 542, 0, @"New Label");
        }

       /* private int getReagCount(BaseReagent reagName, Mobile from)
        {
            dynamic test = typeof(Nightshade);
            Item[] reag = from.Backpack.FindItemsByType(test, true);

            int reagsTotal = 0;

            for (int i = 0; i < reag.Length; i++)
            {
                Nightshade shard = (Nightshade)reag[i];

                if (shard.Stackable)
                    reagsTotal += shard.Amount;
                else
                    reagsTotal++;
            }

           // from.Backpack.FindItemByType(typeof(Nightshade)).Amount;
            return 0;
        }*/
    }
}