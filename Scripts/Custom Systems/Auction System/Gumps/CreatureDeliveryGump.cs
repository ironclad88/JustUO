#region References

using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     This gump is used to deliver a creature
    /// </summary>
    public class CreatureDeliveryGump : Gump
    {
        private const int LabelHue = 0x480;
        private readonly AuctionCheck m_Check;
        private List<int> m_Buttons;

        public CreatureDeliveryGump(AuctionCheck check)
            : base(100, 100)
        {
            m_Check = check;
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
            AddImage(0, 0, 2080);
            AddImageTiled(18, 37, 263, 310, 2081);
            AddImage(21, 347, 2083);
            AddLabel(75, 5, 210, AuctionSystem.ST[0]);
            AddLabel(90, 35, 210, AuctionSystem.ST[90]);

            AddImage(45, 60, 2091);
            AddImage(45, 100, 2091);

            AddLabelCropped(45, 75, 210, 20, LabelHue, m_Check.ItemName);

            AddHtml(45, 115, 215, 100, m_Check.HtmlDetails, false, false);

            // Button 1 : Stable
            AddButton(50, 300, 5601, 5605, 1, GumpButtonType.Reply, 0);
            AddLabel(70, 298, LabelHue, AuctionSystem.ST[91]);
            m_Buttons.Add(1);

            // Button 0: Close
            AddButton(50, 325, 5601, 5605, 0, GumpButtonType.Reply, 0);
            AddImage(230, 315, 9004);
            AddLabel(70, 323, LabelHue, AuctionSystem.ST[7]);
            AddLabel(45, 220, LabelHue, AuctionSystem.ST[92]);
            AddLabel(45, 240, LabelHue, AuctionSystem.ST[93]);
            AddLabel(45, 255, LabelHue, AuctionSystem.ST[94]);
            AddLabel(45, 275, LabelHue, AuctionSystem.ST[95]);
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

            if (info.ButtonID != 1)
            {
                return;
            }

            MobileStatuette ms = m_Check.DeliveredItem as MobileStatuette;

            if (ms == null)
            {
                return;
            }

            ms.Stable(sender.Mobile);
            m_Check.Delete();
        }
    }
}