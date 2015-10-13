#region References

using System;
using System.Collections.Generic;
using System.Globalization;
using Server;
using Server.Gumps;
using Server.Network;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Summary description for BidViewGump.
    /// </summary>
    public class BidViewGump : Gump
    {
        private const int LabelHue = 0x480;
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;

        private List<int> m_Buttons;

        private readonly AuctionGumpCallback m_Callback;
        private readonly int m_Page;
        private readonly List<Bid> m_Bids;

        public BidViewGump(Mobile m, IEnumerable<Bid> bids, AuctionGumpCallback callback)
            : this(m, bids, callback, 0)
        {
        }

        public BidViewGump(Mobile m, IEnumerable<Bid> bids, AuctionGumpCallback callback, int page)
            : base(100, 100)
        {
            m.CloseGump(typeof (BidViewGump));
            m_Callback = callback;
            m_Page = page;
            m_Bids = new List<Bid>(bids);

            MakeGump();
        }

        private void MakeGump()
        {
            m_Buttons = new List<int>();

            int numOfPages = (m_Bids.Count - 1)/10 + 1;

            if (m_Bids.Count == 0)
            {
                numOfPages = 0;
            }

            Closable = true;
            m_Buttons.Add(0);

            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage(0);
            AddImageTiled(0, 0, 297, 282, 5174);
            AddImageTiled(1, 1, 295, 280, 2702);
            AddAlphaRegion(1, 1, 295, 280);
            AddLabel(12, 5, RedHue, AuctionSystem.ST[86]);

            AddLabel(160, 5, GreenHue, string.Format(AuctionSystem.ST[18], m_Page + 1, numOfPages));
            AddImageTiled(10, 30, 277, 221, 5174);
            AddImageTiled(11, 31, 39, 19, 9274);
            AddAlphaRegion(11, 31, 39, 19);
            AddImageTiled(51, 31, 104, 19, 9274);
            AddAlphaRegion(51, 31, 104, 19);
            AddLabel(55, 30, GreenHue, AuctionSystem.ST[87]);
            AddImageTiled(156, 31, 129, 19, 9274);
            AddAlphaRegion(156, 31, 129, 19);
            AddLabel(160, 30, GreenHue, AuctionSystem.ST[88]);

            for (int i = 0; i < 10; i++)
            {
                AddImageTiled(11, 51 + i*20, 39, 19, 9264);
                AddAlphaRegion(11, 51 + i*20, 39, 19);
                AddImageTiled(51, 51 + i*20, 104, 19, 9264);
                AddAlphaRegion(51, 51 + i*20, 104, 19);
                AddImageTiled(156, 51 + i*20, 129, 19, 9264);
                AddAlphaRegion(156, 51 + i*20, 129, 19);

                if (m_Page*10 + i < m_Bids.Count)
                {
                    Bid bid = m_Bids[m_Page*10 + i];
                    AddLabel(15, 50 + i*20, LabelHue, (m_Page*10 + i + 1).ToString(CultureInfo.InvariantCulture));
                    AddLabelCropped(55, 50 + i*20, 100, 19, LabelHue,
                        bid.Mobile != null ? bid.Mobile.Name : AuctionSystem.ST[78]);
                    AddLabel(160, 50 + i*20, LabelHue, bid.Amount.ToString(CultureInfo.InvariantCulture));
                }
            }

            AddButton(10, 255, 4011, 4012, 0, GumpButtonType.Reply, 0);
            AddLabel(48, 257, LabelHue, AuctionSystem.ST[89]);

            // PREV PAGE: 1
            if (m_Page > 0)
            {
                AddButton(250, 8, 9706, 9707, 1, GumpButtonType.Reply, 0);
                m_Buttons.Add(1);
            }

            // NEXT PAGE: 2
            if (m_Page < numOfPages - 1)
            {
                AddButton(270, 8, 9702, 9703, 2, GumpButtonType.Reply, 0);
                m_Buttons.Add(2);
            }
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

            switch (info.ButtonID)
            {
                case 0:
                    if (m_Callback != null)
                    {
                        try
                        {
                            m_Callback.DynamicInvoke(new object[] {sender.Mobile});
                        }
                        catch
                        {
                        }
                    }
                    break;

                case 1:
                    sender.Mobile.SendGump(new BidViewGump(sender.Mobile, m_Bids, m_Callback, m_Page - 1));
                    break;

                case 2:
                    sender.Mobile.SendGump(new BidViewGump(sender.Mobile, m_Bids, m_Callback, m_Page + 1));
                    break;
            }
        }
    }
}