#region References

using System;
using System.Collections.Generic;
using Server;
using Server.Engines.BulkOrders;
using Server.Gumps;
using Server.Items;
using Server.Network;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Summary description for AuctionSearchGump.
    /// </summary>
    public class AuctionSearchGump : Gump
    {
        private const int LabelHue = 0x480;
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;

        private List<int> m_Buttons;

        private readonly List<AuctionItem> m_List;
        private readonly bool m_ReturnToAuction;

        public AuctionSearchGump(Mobile m, IEnumerable<AuctionItem> items, bool returnToAuction)
            : base(50, 50)
        {
            m.CloseGump(typeof (AuctionSearchGump));

            m_List = new List<AuctionItem>(items);
            m_ReturnToAuction = returnToAuction;

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
            AddImageTiled(49, 34, 402, 347, 3004);
            AddImageTiled(50, 35, 400, 345, 2624);
            AddAlphaRegion(50, 35, 400, 345);
            AddImage(165, 65, 10452);
            AddImage(0, 20, 10400);
            AddImage(0, 320, 10402);
            AddImage(35, 20, 10420);
            AddImage(421, 20, 10410);
            AddImage(410, 20, 10430);
            AddImageTiled(90, 32, 323, 16, 10254);
            AddLabel(185, 45, GreenHue, AuctionSystem.ST[32]);
            AddImage(420, 320, 10412);
            AddImage(0, 170, 10401);
            AddImage(420, 170, 10411);

            // TEXT 0 : Search text
            AddLabel(70, 115, LabelHue, AuctionSystem.ST[33]);
            AddImageTiled(145, 135, 200, 20, 3004);
            AddImageTiled(146, 136, 198, 18, 2624);
            AddAlphaRegion(146, 136, 198, 18);
            AddTextEntry(146, 135, 198, 20, RedHue, 0, @"");

            AddLabel(70, 160, LabelHue, AuctionSystem.ST[35]);

            AddCheck(260, 221, 2510, 2511, false, 1);
            AddLabel(280, 220, LabelHue, AuctionSystem.ST[35]);

            if (Core.AOS)
            {
                AddCheck(260, 261, 2510, 2511, false, 9);
                AddLabel(280, 260, LabelHue, AuctionSystem.ST[36]);

                AddCheck(260, 241, 2510, 2511, false, 4);
                AddLabel(280, 240, LabelHue, AuctionSystem.ST[37]);
            }

            AddCheck(260, 201, 2510, 2511, false, 4);
            AddLabel(280, 200, LabelHue, AuctionSystem.ST[38]);

            AddCheck(260, 181, 2510, 2511, false, 5);
            AddLabel(280, 180, LabelHue, AuctionSystem.ST[39]);

            AddCheck(90, 181, 2510, 2511, false, 6);
            AddLabel(110, 180, LabelHue, AuctionSystem.ST[40]);

            AddCheck(90, 201, 2510, 2511, false, 7);
            AddLabel(110, 200, LabelHue, AuctionSystem.ST[41]);

            AddCheck(90, 221, 2510, 2511, false, 8);
            AddLabel(110, 220, LabelHue, AuctionSystem.ST[42]);

            AddCheck(90, 241, 2510, 2511, false, 2);
            AddLabel(110, 240, LabelHue, AuctionSystem.ST[43]);

            AddCheck(90, 261, 2510, 2511, false, 12);
            AddLabel(110, 260, LabelHue, AuctionSystem.ST[44]);

            if (Core.AOS)
            {
                AddCheck(90, 280, 2510, 2511, false, 11);
                AddLabel(110, 279, LabelHue, AuctionSystem.ST[45]);

                AddCheck(260, 280, 2510, 2511, false, 10);
                AddLabel(280, 279, LabelHue, AuctionSystem.ST[46]);
            }

            // BUTTON 1 : Search
            AddButton(255, 350, 4005, 4006, 1, GumpButtonType.Reply, 0);
            AddLabel(295, 350, LabelHue, AuctionSystem.ST[16]);
            m_Buttons.Add(1);

            // BUTTON 0 : Cancel
            AddButton(85, 350, 4017, 4018, 0, GumpButtonType.Reply, 0);
            AddLabel(125, 350, LabelHue, AuctionSystem.ST[47]);
            m_Buttons.Add(0);

            // CHECK 0: Search withing existing results
            AddCheck(80, 310, 9721, 9724, false, 0);
            AddLabel(115, 312, LabelHue, AuctionSystem.ST[48]);
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

            if (info.ButtonID == 0)
            {
                // Cancel
                sender.Mobile.SendGump(new AuctionListing(sender.Mobile, m_List, true, m_ReturnToAuction));
                return;
            }

            bool searchExisting = false;
            bool artifacts = false;
            bool commodity = false;
            string text = null;
            var types = new List<Type>();

            if (!String.IsNullOrEmpty(info.TextEntries[0].Text))
            {
                text = info.TextEntries[0].Text;
            }

            foreach (int check in info.Switches)
            {
                switch (check)
                {
                    case 0:
                        searchExisting = true;
                        break;

                    case 1:
                        types.Add(typeof (MapItem));
                        break;

                    case 2:
                        types.Add(typeof (BaseReagent));
                        break;

                    case 3:
                        commodity = true;
                        break;

                    case 4:
                    {
                        types.Add(typeof (StatCapScroll));
                        types.Add(typeof (PowerScroll));
                    }
                        break;

                    case 5:
                        types.Add(typeof (BaseJewel));
                        break;

                    case 6:
                        types.Add(typeof (BaseWeapon));
                        break;

                    case 7:
                        types.Add(typeof (BaseArmor));
                        break;

                    case 8:
                        types.Add(typeof (BaseShield));
                        break;

                    case 9:
                        artifacts = true;
                        break;

                    case 10:
                        types.Add(typeof (SmallBOD));
                        break;

                    case 11:
                        types.Add(typeof (LargeBOD));
                        break;

                    case 12:
                    {
                        types.Add(typeof (BasePotion));
                        types.Add(typeof (PotionKeg));
                    }
                        break;
                }
            }

            var source = searchExisting ? new List<AuctionItem>(m_List) : new List<AuctionItem>(AuctionSystem.Auctions);

            List<AuctionItem> typeSearch = null;
            List<AuctionItem> commoditySearch = null;
            List<AuctionItem> artifactsSearch = null;

            if (types.Count > 0)
            {
                typeSearch = AuctionSearch.ForTypes(source, types);
            }

            if (commodity)
            {
                commoditySearch = AuctionSearch.ForCommodities(source);
            }

            if (artifacts)
            {
                artifactsSearch = AuctionSearch.ForArtifacts(source);
            }

            var results = new List<AuctionItem>();

            if (typeSearch == null && artifactsSearch == null && commoditySearch == null)
            {
                results.AddRange(source);
            }
            else
            {
                if (typeSearch != null)
                {
                    results.AddRange(typeSearch);
                }

                if (commoditySearch != null)
                {
                    results = AuctionSearch.Merge(results, commoditySearch);
                }

                if (artifactsSearch != null)
                {
                    results = AuctionSearch.Merge(results, artifactsSearch);
                }
            }

            // Perform search
            if (text != null)
            {
                results = AuctionSearch.SearchForText(results, text);
            }

            sender.Mobile.SendGump(new AuctionListing(sender.Mobile, results, true, m_ReturnToAuction));
        }
    }
}