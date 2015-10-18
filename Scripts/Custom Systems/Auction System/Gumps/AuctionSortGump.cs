#region References

using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;

#endregion

namespace Arya.Auction
{
    public class AuctionSortGump : Gump
    {
        private const int LabelHue = 0x480;
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private List<int> m_Buttons;

        private readonly bool m_Search;
        private readonly bool m_ReturnToAuction;
        private readonly List<AuctionItem> m_List;

        public AuctionSortGump(Mobile m, IEnumerable<AuctionItem> items, bool returnToAuction, bool search)
            : base(50, 50)
        {
            m.CloseGump(typeof (AuctionSortGump));

            m_List = new List<AuctionItem>(items);
            m_ReturnToAuction = returnToAuction;
            m_Search = search;

            MakeGump();
        }

        private void MakeGump()
        {
            m_Buttons = new List<int>();

            Closable = true;
            m_Buttons.Add(0);

            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage(0);
            AddImageTiled(49, 34, 402, 312, 3004);
            AddImageTiled(50, 35, 400, 310, 2624);
            AddAlphaRegion(50, 35, 400, 310);
            AddImage(165, 65, 10452);
            AddImage(0, 20, 10400);
            AddImage(0, 280, 10402);
            AddImage(35, 20, 10420);
            AddImage(421, 20, 10410);
            AddImage(410, 20, 10430);
            AddImageTiled(90, 32, 323, 16, 10254);
            AddLabel(160, 45, GreenHue, AuctionSystem.ST[49]);

            AddLabel(95, 125, RedHue, AuctionSystem.ST[50]);
            AddImage(75, 125, 2511);

            AddButton(110, 144, 9702, 9703, 1, GumpButtonType.Reply, 0);
            m_Buttons.Add(1);

            AddLabel(135, 141, LabelHue, AuctionSystem.ST[51]);

            AddButton(110, 163, 9702, 9703, 2, GumpButtonType.Reply, 0);
            m_Buttons.Add(2);

            AddLabel(135, 160, LabelHue, AuctionSystem.ST[52]);

            AddImage(420, 280, 10412);
            AddLabel(95, 185, RedHue, AuctionSystem.ST[53]);
            AddImage(75, 185, 2511);

            AddButton(110, 204, 9702, 9703, 3, GumpButtonType.Reply, 0);
            m_Buttons.Add(3);

            AddLabel(135, 201, LabelHue, AuctionSystem.ST[54]);

            AddButton(110, 223, 9702, 9703, 4, GumpButtonType.Reply, 0);
            m_Buttons.Add(4);

            AddLabel(135, 220, LabelHue, AuctionSystem.ST[55]);

            AddLabel(95, 245, RedHue, AuctionSystem.ST[56]);
            AddImage(75, 245, 2511);

            AddButton(110, 264, 9702, 9703, 5, GumpButtonType.Reply, 0);
            m_Buttons.Add(5);

            AddLabel(135, 261, LabelHue, AuctionSystem.ST[57]);

            AddButton(110, 283, 9702, 9703, 6, GumpButtonType.Reply, 0);
            m_Buttons.Add(6);

            AddLabel(135, 280, LabelHue, AuctionSystem.ST[58]);

            AddLabel(290, 125, RedHue, AuctionSystem.ST[59]);
            AddImage(270, 125, 2511);

            AddButton(305, 144, 9702, 9703, 7, GumpButtonType.Reply, 0);
            m_Buttons.Add(7);

            AddLabel(330, 141, LabelHue, AuctionSystem.ST[60]);

            AddButton(305, 163, 9702, 9703, 8, GumpButtonType.Reply, 0);
            m_Buttons.Add(8);

            AddLabel(330, 160, LabelHue, AuctionSystem.ST[61]);

            AddLabel(290, 185, RedHue, AuctionSystem.ST[62]);
            AddImage(270, 185, 2511);

            AddButton(305, 204, 9702, 9703, 9, GumpButtonType.Reply, 0);
            m_Buttons.Add(9);

            AddLabel(330, 201, LabelHue, AuctionSystem.ST[63]);

            AddButton(305, 223, 9702, 9703, 10, GumpButtonType.Reply, 0);
            m_Buttons.Add(10);

            AddLabel(330, 220, LabelHue, AuctionSystem.ST[64]);

            AddLabel(290, 245, RedHue, AuctionSystem.ST[65]);
            AddImage(270, 245, 2511);

            AddButton(305, 264, 9702, 9703, 11, GumpButtonType.Reply, 0);
            m_Buttons.Add(11);

            AddLabel(330, 261, LabelHue, AuctionSystem.ST[63]);

            AddButton(305, 283, 9702, 9703, 12, GumpButtonType.Reply, 0);
            m_Buttons.Add(12);

            AddLabel(330, 280, LabelHue, AuctionSystem.ST[64]);

            AddLabel(120, 315, LabelHue, AuctionSystem.ST[66]);
            AddButton(80, 315, 4017, 4018, 0, GumpButtonType.Reply, 0);
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

            AuctionComparer cmp = null;

            switch (info.ButtonID)
            {
                case 1: // Name
                    cmp = new AuctionComparer(AuctionSorting.Name, true);
                    break;

                case 2:
                    cmp = new AuctionComparer(AuctionSorting.Name, false);
                    break;

                case 3:
                    cmp = new AuctionComparer(AuctionSorting.Date, true);
                    break;

                case 4:
                    cmp = new AuctionComparer(AuctionSorting.Date, false);
                    break;

                case 5:
                    cmp = new AuctionComparer(AuctionSorting.TimeLeft, true);
                    break;

                case 6:
                    cmp = new AuctionComparer(AuctionSorting.TimeLeft, false);
                    break;

                case 7:
                    cmp = new AuctionComparer(AuctionSorting.Bids, true);
                    break;

                case 8:
                    cmp = new AuctionComparer(AuctionSorting.Bids, false);
                    break;

                case 9:
                    cmp = new AuctionComparer(AuctionSorting.MinimumBid, true);
                    break;

                case 10:
                    cmp = new AuctionComparer(AuctionSorting.MinimumBid, false);
                    break;

                case 11:
                    cmp = new AuctionComparer(AuctionSorting.HighestBid, true);
                    break;

                case 12:
                    cmp = new AuctionComparer(AuctionSorting.HighestBid, false);
                    break;
            }

            if (cmp != null)
            {
                m_List.Sort(cmp);
            }

            sender.Mobile.SendGump(new AuctionListing(sender.Mobile, m_List, m_Search, m_ReturnToAuction));
        }
    }
}