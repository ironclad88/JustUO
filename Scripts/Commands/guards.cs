using System;
using System.Text;
using Server.Mobiles;
using Server.Regions;

namespace Server.Commands
{
    public class guards
    {
        public static void Initialize()
        {
            CommandSystem.Register("guards", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("Guards")]
        [Description("Call guards")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            
           // if (e.Length != 1)
           // {
                e.Mobile.SendMessage("this is not yet implemented");
           // }
            
        }


    }
}
