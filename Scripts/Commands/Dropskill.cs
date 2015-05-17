using System;
using Server.Mobiles;
using Server.Gumps.Zulugumps;

namespace Server.Commands
{
    public class Dropskill
    {
        public static void Initialize()
        {
            CommandSystem.Register("Dropskill", AccessLevel.Player, new CommandEventHandler(dropSkill_OnCommand));
            CommandSystem.Register("Dropskills", AccessLevel.Player, new CommandEventHandler(dropSkill_OnCommand));
        }

        [Usage("Dropskill")]
        [Description("Opens a dropskill gump.")]
        private static void dropSkill_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            //Console.WriteLine(m.GetEquipment());
            if (m.GetEquipment().Length <= 0) { 
            m.SendGump(new DropskillGump(m));
            }
            else
            {
                m.SendMessage("You can´t have any items equipped to use this command.");
            }
        }
    }
}