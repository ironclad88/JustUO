using System;
using System.Text;
using Server.Mobiles;

namespace Server.Commands
{
    public class Autoloop
    {
        public static void Initialize()
        {
            CommandSystem.Register("Autoloop", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("Autoloop")]
        [Description("Set number of automatic loops.")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            try
            {
                Mobile m = e.Mobile;
                if (e.Length != 1)
                {
                    e.Mobile.SendMessage(194, "Autoloop <number of loops>");
                }
                int loops = Convert.ToInt32(e.ArgString);
                if (loops > 10000) loops = 10000;
                if (loops < 1) loops = 1;
                (e.Mobile as Server.Mobiles.PlayerMobile).AutoLoop = loops;
                e.Mobile.SendMessage(194, "[Autoloop] Autoloops set to " + loops + ".");
            }
            catch (Exception z)
            {
                e.Mobile.SendMessage(194, "Autoloop <number of loops>");
            }
        }

    }
}
