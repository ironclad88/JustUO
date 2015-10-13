using Server.Items;
using Server.Items.ZuluIems.ElementalGear.Water;
using Server.Items.ZuluIems.ElementalGear.Water.Leather;
using Server.Items.ZuluIems.ElementalGear.Water.Plate;
using Server.Items.ZuluIems.ElementalGear.Water.Weapon;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Gumps.Zulugumps.ElementalLords
{
    class AirLordGump : Gump
    {
        Mobile test;

        public AirLordGump(Mobile owner)
            : base(100, 0)
        {
            try
            {
                test = owner;

                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(57, 39, 388, 352, 9200);
                this.AddLabel(167, 53, 0, @"Select Air Elemental gear");
                this.AddItem(82, 107, 5141, 1161);
                this.AddItem(75, 179, 5068, 1161);
                this.AddItem(74, 265, 5042, 1161);
                this.AddTextEntry(142, 117, 200, 20, 0, (int)Buttons.TextEntry1, @"Plate armor");
                this.AddTextEntry(144, 186, 200, 20, 0, (int)Buttons.TextEntry2, @"Leather armor");
                this.AddTextEntry(146, 268, 200, 20, 0, (int)Buttons.TextEntry3, @"Bow");
                this.AddButton(350, 270, 2103, 2104, 1, GumpButtonType.Reply, 0);
                this.AddButton(350, 190, 2103, 2104, 2, GumpButtonType.Reply, 0);
                this.AddButton(350, 120, 2103, 2104, 3, GumpButtonType.Reply, 0);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public enum Buttons
        {
            TextEntry1,
            TextEntry2,
            TextEntry3
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            Mobile caster = test;
            switch (info.ButtonID)
            {
                case 1:
                    Console.WriteLine("Air bow!");
                    addGear(3, from);
                    break;
                case 2:
                    Console.WriteLine("Leather Armor!");
                    addGear(2, from);
                    break;
                case 3:
                    Console.WriteLine("Plate Armor!");
                    addGear(1, from);
                    break;

            }
        }

        private void addGear(int selection, Mobile from)
        {
            if (selection == 1)
            {
                from.AddToBackpack(new AirPlateChest());
                from.AddToBackpack(new AirPlateLegs());
                from.AddToBackpack(new AirPlateArms());
                from.AddToBackpack(new AirPlateGloves());
                from.AddToBackpack(new AirPlateGorget());
                from.AddToBackpack(new AirPlateHelm());
            }
            else if (selection == 2)
            {
                from.AddToBackpack(new AirLeatherChest());
                from.AddToBackpack(new AirLeatherArms());
                from.AddToBackpack(new AirLeatherGloves());
                from.AddToBackpack(new AirLeatherLegs());
                from.AddToBackpack(new AirLeatherHelm());
                from.AddToBackpack(new AirLeatherGorget());
            }
            else
            {
                from.AddToBackpack(new AirBow());
            }

            clearBools(from);
        }

        private void clearBools(Mobile from)
        {
            from.AirPent1 = false;
            from.AirPent2 = false;
            from.AirPent3 = false;
            from.AirPent4 = false;
            from.AirPent5 = false;
            from.AirPent6 = false;
            from.AirPent7 = false;
            from.AirPent8 = false;
            from.AirPent9 = false;
        }
    }
}
