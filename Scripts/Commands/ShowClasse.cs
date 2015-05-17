using System;
using Server.Mobiles;

namespace Server.Commands
{
    public class ShowClasse
    {
        public static void Initialize()
        {
            CommandSystem.Register("Showclasse", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
            CommandSystem.Register("Showclass", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("Showclasse")]
        [Description("Returns the players class level.")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            m.GetSpec();
            if (m.SpecLevel == 0 || m.SpecClasse == SpecClasse.None)
            {
                e.Mobile.SendMessage("You do not qualify for any classe.");
            }
            else
            {
                e.Mobile.SendMessage("You're qualified level {0} {1}.", m.SpecLevel, m.SpecClasse.ToString());
            }

        }
    }
}