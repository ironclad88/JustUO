using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands
{
    public class playerbroadcast
    {
        public static void Initialize()
        {
            CommandSystem.Register("c", AccessLevel.Player, new CommandEventHandler(c_OnCommand));
        }

        [Usage("c")]
        [Description("Broadcasts message to all online players")]
        private static void c_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            string arguments = e.ArgString;
            if (arguments.Length > 0) { 
            Broadcast(arguments, m);           // add some anti-spam function to this.
            }
            else
            {
                e.Mobile.SendMessage("You can´t post blank message");
            }
        }


        public static void Broadcast(string text, Mobile from)
        {
            Packet p;
            string textToAll = from.Name + ": " + text;
            p = new UnicodeMessage(Serial.MinusOne, -1, MessageType.Regular, 0x35, 3, "ENU", "System", textToAll);

            List<NetState> list = NetState.Instances;

            p.Acquire();

            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Mobile != null)
                {
                    list[i].Send(p);
                }
            }

            p.Release();

            NetState.FlushAll();
        }

    }
}


		