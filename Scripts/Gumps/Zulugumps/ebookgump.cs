using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Zulu.EarthSpells;

/*
 *  Author Oscar Ternström
 */

namespace Server.Gumps.Zulugumps
{
    
    public class ebookgump : Gump
    {

        Mobile test;

        public ebookgump(Mobile owner, bool[] array)
            : base(100, 0)
        {
            try {
                test = owner;
            int xName1 = 80;
            int xName2 = 240;
            int yName1 = 65;
            int yName2 = 65;

            int btnX1 = 60;
            int btnX2 = 220;
            int btnY1 = 70;
            int btnY2 = 70;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddImage(30, 30, 2203);

            this.AddLabel(70, 40, 28, @"Circle 1 Spells");
            this.AddLabel(230, 40, 28, @"Circle 2 Spells");

            if (array[1]) { // starts with 1 instead of 0, 0 is the event for book close, if you start with 0 you cast antidote when you close the damn book
            this.AddLabel(xName1, yName1, 66, @"Antidote");
            this.AddButton(btnX1, btnY1, 2104, 2103, 1, GumpButtonType.Reply, 0);
            }

            if (array[2])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Owl Sight");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 2, GumpButtonType.Reply, 0);
            }

            if (array[3])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Shifting Earth");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 3, GumpButtonType.Reply, 0);
            }

            if (array[4])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Summon Mammals");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 4, GumpButtonType.Reply, 0);
            }

            if (array[5])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Call Lightning");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 5, GumpButtonType.Reply, 0);
            }

            if (array[6])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Earth Blessing");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 6, GumpButtonType.Reply, 0);
            }

            if (array[7])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Earth Portal");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 7, GumpButtonType.Reply, 0);
            }

            if (array[8])
            {
                this.AddLabel(xName1, yName1 += 20, 66, @"Nature´s Touch");
                this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 8, GumpButtonType.Reply, 0);
            }

             if (array[9])
            {
                this.AddLabel(xName2, yName2, 66, @"Gust of Air");
                this.AddButton(btnX2, btnY2, 2104, 2103, 9, GumpButtonType.Reply, 0);
            }

            if (array[10])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Rising Fire");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 10, GumpButtonType.Reply, 0);
            }

            if (array[11])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Shapeshift");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 11, GumpButtonType.Reply, 0);
            }

            if (array[12])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Ice Strike");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 12, GumpButtonType.Reply, 0);
            }

            if (array[13])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Earth Spirit");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 13, GumpButtonType.Reply, 0);
            }

            if (array[14])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Fire Spirit");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 14, GumpButtonType.Reply, 0);
            }

            if (array[15])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Storm Spirit");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 15, GumpButtonType.Reply, 0);
            }

            if (array[16])
            {
                this.AddLabel(xName2, yName2 += 20, 66, @"Water Spirit");
                this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 16, GumpButtonType.Reply, 0);
            }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            Mobile caster = test;
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
                    new EarthBless(caster, null).Cast();
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
                    Console.WriteLine("Casting Ice Strike");
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