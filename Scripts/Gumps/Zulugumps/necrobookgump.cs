using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells.Zulu.NecroSpells;

/*
 *  Author Oscar Ternström
 */

namespace Server.Gumps.Zulugumps
{

    public class necrobookgump : Gump
    {
        Mobile test;
        public necrobookgump(Mobile owner, bool[] array)
            : base(100, 0)
        {
            try
            {
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
                this.AddImage(30, 30, 2200);

                this.AddLabel(80, 40, 28, @"Lesser Chants");
                this.AddLabel(240, 40, 28, @"Greater Chants");

                if (array[1])
                { // starts with 1 instead of 0, 0 is the event for book close, if you start with 0 you cast antidote when you close the damn book
                    this.AddLabel(xName1, yName1, 66, @"Control Undead");
                    this.AddButton(btnX1, btnY1, 2104, 2103, 1, GumpButtonType.Reply, 0);
                }

                if (array[2])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Darkness");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 2, GumpButtonType.Reply, 0);
                }

                if (array[3])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Decaying Ray");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 3, GumpButtonType.Reply, 0);
                }

                if (array[4])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Spectre´s Touch");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 4, GumpButtonType.Reply, 0);
                }

                if (array[5])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Abyssal Flame");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 5, GumpButtonType.Reply, 0);
                }

                if (array[6])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Animate Dead");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 6, GumpButtonType.Reply, 0);
                }

                if (array[7])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Sacrifice");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 7, GumpButtonType.Reply, 0);
                }

                if (array[8])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Wraith Breath");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 8, GumpButtonType.Reply, 0);
                }

                if (array[9])
                {
                    this.AddLabel(xName2, yName2, 66, @"Sorcerer´s Bane");
                    this.AddButton(btnX2, btnY2, 2104, 2103, 9, GumpButtonType.Reply, 0);
                }

                if (array[10])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Summon Spirit");
                    this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 10, GumpButtonType.Reply, 0);
                }

                if (array[11])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Wraithform");
                    this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 11, GumpButtonType.Reply, 0);
                }

                if (array[12])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Wyvern Strike");
                    this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 12, GumpButtonType.Reply, 0);
                }

                if (array[13])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Kill");
                    this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 13, GumpButtonType.Reply, 0);
                }

                if (array[14])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Liche");
                    this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 14, GumpButtonType.Reply, 0);
                }

                if (array[15])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Plague");
                    this.AddButton(btnX2, btnY2 += 20, 2104, 2103, 15, GumpButtonType.Reply, 0);
                }

                if (array[16])
                {
                    this.AddLabel(xName2, yName2 += 20, 66, @"Spellbind");
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
                    Console.WriteLine("Casting Control Undead");
                    break;
                case 2:
                    Console.WriteLine("Casting Darkness");
                    break;
                case 3:
                    Console.WriteLine("Casting Decaying Ray");
                    break;
                case 4:
                    Console.WriteLine("Casting Spectre´s Touch");
                    break;
                case 5:
                    Console.WriteLine("Casting Abyssal Flame");
                    break;
                case 6:
                    Console.WriteLine("Casting Animate Dead");
                    break;
                case 7:
                    Console.WriteLine("Casting Sacrifice");
                    break;
                case 8:
                    Console.WriteLine("Casting Wraith Breath");
                    break;
                case 9:
                    Console.WriteLine("Casting Sorcerer´s Bane");
                    break;
                case 10:
                    Console.WriteLine("Casting Summon Spirit");
                    break;
                case 11:
                    Console.WriteLine("Casting Wraithform");
                    break;
                case 12:
                   // Console.WriteLine("Casting Wyvern Strike");
                    new WyvernStrikeSpell(caster, null).Cast();
                    break;
                case 13:
                    Console.WriteLine("Casting Kill");
                    new KillSpell(caster, null).Cast();
                    break;
                case 14:
                    new LicheSpell(caster, null).Cast();
                    //Console.WriteLine("Casting Liche");
                    break;
                case 15:
                    Console.WriteLine("Casting Plague");
                    break;
                case 16:
                    Console.WriteLine("Casting Spellbind");
                    break;

            }
        }
    }
}