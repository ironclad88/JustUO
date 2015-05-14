using System;
using Server.Mobiles;
using Server.Gumps.Zulugumps;
namespace Server.Commands
{
    public class Prots
    {
        public static void Initialize()
        {
            CommandSystem.Register("Prots", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("Prots")]
        [Description("Lists the players portections and mods.")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            if (m != null)
            {
                m.CloseGump(typeof(ProtsGump));
                m.SendGump(new ProtsGump(m));
            }
        }
    }
}