using Server.Gumps.Zulugumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands
{
    class reags
    {
        public static void Initialize()
        {
            CommandSystem.Register("reag", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
            CommandSystem.Register("reags", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("reag")]
        [Description("Shows a gump of current reags and pagans.")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            if (m != null)
            {
                m.CloseGump(typeof(ReagGump));
                m.SendGump(new ReagGump(m));
            }

        }
    }
}
