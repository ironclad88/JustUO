using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps.Zulugumps
{

    public class ProtsGump : Gump
    {

        public ProtsGump(Mobile owner)
            : this(owner, ResurrectMessage.Generic, false)
        {
        }


        public ProtsGump(Mobile owner, ResurrectMessage msg, bool fromSacrifice)
            : this(owner, msg)
        {
        }

        public ProtsGump(Mobile owner, ResurrectMessage msg)
            : base(100, 0)
        {

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            PlayerMobile player = owner as PlayerMobile;

            int armor = (int)player.ArmorRating;
            int phys = owner.PhysicalResistance;
            int fire = owner.FireResistance;
            int water = owner.ColdResistance;
            int air = owner.EnergyResistance;
            int earth = owner.EarthResistance;
            int necro = owner.NecroResistance;
            int holy = owner.HolyResistance;

            AddPage(0);
            AddBackground(90, 30, 400, 300, 9200);
            AddLabel(150, 50, 0, @"Protections & Mods");
            AddItem(100, 50, 7107);
            int startX = 100;
            int startY = 100;
            AddLabel(startX, startY, 0, @"Armor Rating");
            AddLabel(startX, startY += 25, 0, @"Physical Protection");
            AddLabel(startX, startY += 25, 0, @"Fire Protection");
            AddLabel(startX, startY += 25, 0, @"Water Protection");
            AddLabel(startX, startY += 25, 0, @"Air Protection");
            AddLabel(startX, startY += 25, 0, @"Earth Protection");
            AddLabel(startX, startY += 25, 0, @"Necro Protection");
            AddLabel(startX, startY += 25, 0, @"Holy Protection");

            startX += 250;
            startY = 100;
            AddLabel(startX, startY, 0, armor.ToString());
            AddLabel(startX, startY += 25, 0, phys.ToString() + @"%");
            AddLabel(startX, startY += 25, 0, fire.ToString() + @"%");
            AddLabel(startX, startY += 25, 0, water.ToString() + @"%");
            AddLabel(startX, startY += 25, 0, air.ToString() + @"%");
            AddLabel(startX, startY += 25, 0, earth.ToString() + @"%");
            AddLabel(startX, startY += 25, 0, necro.ToString() + @"%");
            AddLabel(startX, startY += 25, 0, holy.ToString() + @"%");

        }
    }
}