#region References

using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Provides the message notice for messages from the auction system
    /// </summary>
    public class AuctionNoticeGump : Gump
    {
        private readonly AuctionMessageGump m_Message;
        private List<int> m_Buttons;

        public AuctionNoticeGump(AuctionMessageGump msg)
            : base(25, 25)
        {
            m_Message = msg;
            MakeGump();
        }

        private void MakeGump()
        {
            m_Buttons = new List<int>();

            Closable = false;
            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage(0);
            AddImageTiled(0, 0, 75, 75, 3004);
            AddImageTiled(1, 1, 73, 73, 2624);
            AddAlphaRegion(1, 1, 73, 73);
            AddButton(7, 7, 5573, 5574, 1, GumpButtonType.Reply, 0);
            m_Buttons.Add(1);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (!m_Buttons.Contains(info.ButtonID))
            {
                string player = sender.Mobile != null ? sender.Mobile.ToString() : "Unkown";
                string acc = sender.Mobile != null && sender.Mobile.Account != null
                    ? sender.Mobile.Account.Username
                    : "Unkown";

                Console.WriteLine(@"The auction system located a potential exploit. 
					Player {0} (Acc. {1}) tried to press an unregistered button in a gump of type: {2}", player, acc, GetType().Name);

                return;
            }

            if (! AuctionSystem.Running)
            {
                sender.Mobile.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[15]);
                return;
            }

            if (info.ButtonID == 1)
            {
                if (m_Message != null)
                {
                    m_Message.SendTo(sender.Mobile);
                }
            }
        }
    }
}