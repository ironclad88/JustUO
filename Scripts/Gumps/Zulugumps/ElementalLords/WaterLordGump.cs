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
    class WaterLordGump : Gump
    {
        Mobile test;

        public WaterLordGump(Mobile owner)
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
                this.AddLabel(167, 53, 0, @"Select Water Elemental gear");
                this.AddItem(82, 107, 5141, 1167);
                this.AddItem(75, 179, 5068, 1167);
                this.AddItem(74, 265, 5125, 1167);
                this.AddTextEntry(142, 117, 200, 20, 0, (int)Buttons.TextEntry1, @"Plate armor");
                this.AddTextEntry(144, 186, 200, 20, 0, (int)Buttons.TextEntry2, @"Leather armor");
                this.AddTextEntry(146, 268, 200, 20, 0, (int)Buttons.TextEntry3, @"Warfork");
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
                    Console.WriteLine("War fork!");
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

        private void addGear(int selection, Mobile from){
            if(selection == 1){
                from.AddToBackpack(new WaterPlateChest());
                from.AddToBackpack(new WaterPlateLegs());
                from.AddToBackpack(new WaterPlateArms());
                from.AddToBackpack(new WaterPlateGloves());
                from.AddToBackpack(new WaterPlateGorget());
                from.AddToBackpack(new WaterPlateHelm());
            }
            else if (selection == 2)
            {
                from.AddToBackpack(new WaterLeatherChest());
                from.AddToBackpack(new WaterLeatherArms());
                from.AddToBackpack(new WaterLeatherGloves());
                from.AddToBackpack(new WaterLeatherLegs());
                from.AddToBackpack(new WaterLeatherHelm());
                from.AddToBackpack(new WaterLeatherGorget());
            }
            else
            {
                from.AddToBackpack(new WaterWarFork());
            }

            clearBools(from);
        }

        private void clearBools(Mobile from)
        {
            from.WaterPent1 = false;
            from.WaterPent2 = false;
            from.WaterPent3 = false;
            from.WaterPent4 = false;
            from.WaterPent5 = false;
            from.WaterPent6 = false;
            from.WaterPent7 = false;
            from.WaterPent8 = false;
            from.WaterPent9 = false;
        }
    }
}
