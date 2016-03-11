using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands
{
    class Fame
    {
        public static void Initialize()
        {
            CommandSystem.Register("Fame", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("Fame")]
        [Description("Returns the players fame/karma.")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            e.Mobile.SendMessage($"Fame: {e.Mobile.Fame}");
            e.Mobile.SendMessage($"Karma: {e.Mobile.Karma}");
            
        }
    }
}
