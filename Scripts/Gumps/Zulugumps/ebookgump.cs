using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;

/*
 *  Author Oscar Ternström
 */

namespace Server.Gumps.Zulugumps
{

    public class ebookgump : Gump
    {

        public ebookgump(Mobile owner)
            : this(owner, ResurrectMessage.Generic, false)
        {
        }


        public ebookgump(Mobile owner, ResurrectMessage msg, bool fromSacrifice)
            : this(owner, msg)
        {
        }


        public ebookgump(Mobile owner, ResurrectMessage msg)
            : base(100, 0)
        {

            int xName1 = 90;
            int xName2 = 295;
            int yName1 = 90;
            int yName2 = 90;

            int btnX1 = 70;
            int btnX2 = 275;
            int btnY1 = 94;
            int btnY2 = 94;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddImage(27, 79, 500);
            this.AddLabel(xName1, yName1, 66, @"Antidote");
            this.AddLabel(xName1, yName1 += 25, 66, @"Owl Sight");
            this.AddLabel(xName1, yName1 += 25, 66, @"Shifting Earth");
            this.AddLabel(xName1, yName1 += 25, 66, @"Summon Mammals");
            this.AddLabel(xName1, yName1 += 25, 66, @"Call Lightning");
            this.AddLabel(xName1, yName1 += 25, 66, @"Earth Blessing");
            this.AddLabel(xName1, yName1 += 25, 66, @"Earth Portal");
            this.AddLabel(xName1, yName1 += 25, 66, @"Nature´s Touch");
            this.AddLabel(xName2, yName2, 66, @"Gust of Air");
            this.AddLabel(xName2, yName2 += 25, 66, @"Rising Fire");
            this.AddLabel(xName2, yName2 += 25, 66, @"Shapeshift");
            this.AddLabel(xName2, yName2 += 25, 66, @"Ice Strike");
            this.AddLabel(xName2, yName2 += 25, 66, @"Earth Spirit");
            this.AddLabel(xName2, yName2 += 25, 66, @"Fire Spirit");
            this.AddLabel(xName2, yName2 += 25, 66, @"Storm Spirit");
            this.AddLabel(xName2, yName2 += 25, 66, @"Water Spirit");
            this.AddButton(btnX1, btnY1, 216, 216, 1, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 2, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 3, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 4, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 5, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 6, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 7, GumpButtonType.Reply, 0);
            this.AddButton(btnX1, btnY1 += 25, 216, 216, 8, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2, 216, 216, 9, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 10, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 11, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 12, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 13, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 14, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 15, GumpButtonType.Reply, 0);
            this.AddButton(btnX2, btnY2 += 25, 216, 216, 16, GumpButtonType.Reply, 0);

        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            switch (info.ButtonID)
            {
                case 1:
                    Console.WriteLine("Casting Antidote");
                    break;
                case 2:
                    Console.WriteLine("Casting Owl Sight");
                    break;
                case 3:
                    Console.WriteLine("Casting Shifting Earth");
                    break;
                case 4:
                    Console.WriteLine("Casting Summon Mammals");
                    break;
                case 5:
                    Console.WriteLine("Casting Call Lightning");
                    break;
                case 6:
                    Console.WriteLine("Casting Earth Blessing");
                    break;
                case 7:
                    Console.WriteLine("Casting Earth Portal");
                    break;
                case 8:
                    Console.WriteLine("Casting Nature´s Touch");
                    break;
                case 9:
                    Console.WriteLine("Casting Gust of Air");
                    break;
                case 10:
                    Console.WriteLine("Casting Rising Fire");
                    break;
                case 11:
                    Console.WriteLine("Casting Shapeshift");
                    break;
                case 12:
                    Console.WriteLine("Casting Ice Striket");
                    break;
                case 13:
                    Console.WriteLine("Casting Earth Spirit");
                    break;
                case 14:
                    Console.WriteLine("Casting Fire Spirit");
                    break;
                case 15:
                    Console.WriteLine("Casting Storm Spirit");
                    break;
                case 16:
                    Console.WriteLine("Casting Water Spirit");
                    break;

            }
        }
    }
}