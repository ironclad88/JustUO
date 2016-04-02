using System;
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
                    this.AddLabel(xName1, yName1 += 20, 66, @"Volcanic Eruiption");
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 2, GumpButtonType.Reply, 0);
                }

                if (array[3])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Summon Blood Stone");
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
                    this.AddLabel(xName1, yName1 += 20, 66, @"Wither"); // Animate Dead
                    this.AddButton(btnX1, btnY1 += 20, 2104, 2103, 6, GumpButtonType.Reply, 0);
                }

                if (array[7])
                {
                    this.AddLabel(xName1, yName1 += 20, 66, @"Vengeful Spirit");
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
                    this.AddLabel(xName2, yName2 += 20, 66, @"Animate Dead");
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
                    new ControlundeadSpell(caster, null).Cast();
                    break;
                case 2:
                    new VolcanicEruiptionSpell(caster, null).Cast();
                    break;
                case 3:
                    new BloodStoneSpell(caster, null).Cast();
                    break;
                case 4:
                    new SpectrestouchSpell(caster, null).Cast();
                    break;
                case 5:
                    new AbyssalflameSpell(caster, null).Cast();
                    break;
                case 6:
                    new FrostVeilSpell(caster, null).Cast();
                    break;
                case 7:
                    new VengefulSpiritSpell(caster, null).Cast();
                    break;
                case 8:
                    new WraithsbreathSpell(caster, null).Cast();
                    break;
                case 9:
                    new SorceresbaneSpell(caster, null).Cast();
                    break;
                case 10:
                    new SummonspiritSpell(caster, null).Cast();
                    break;
                case 11:
                    new AnimateDeadSpellZulu(caster, null).Cast();
                    break;
                case 12:
                    new WyvernStrikeSpell(caster, null).Cast();
                    break;
                case 13:
                    new KillSpell(caster, null).Cast();
                    break;
                case 14:
                    new LicheSpell(caster, null).Cast();
                    break;
                case 15:
                    new PlagueSpell(caster, null).Cast();
                    break;
                case 16:
                    new SpellbindSpell(caster, null).Cast();
                    break;
            }
        }
    }
}