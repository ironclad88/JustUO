#region References

using System;
using Server;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Manages the delivery of messages between players involved in an auction
    /// </summary>
    public class AuctionMessaging
    {
        public static void Initialize()
        {
            EventSink.Login += OnPlayerLogin;
        }

        private static void OnPlayerLogin(LoginEventArgs e)
        {
            if (! AuctionSystem.Running)
            {
                return;
            }

            foreach (AuctionItem auction in AuctionSystem.Pending)
            {
                auction.SendMessage(e.Mobile);
            }
        }

        /// <summary>
        ///     Sends a message to a mobile to notify them that they have been outbid during an auction.
        /// </summary>
        /// <param name="auction">The auction generating the message</param>
        /// <param name="amount">The value of the mobile's bid</param>
        /// <param name="to">The mobile sending to. This can be null or offline. If offline, nothing will be sent.</param>
        public static void SendOutbidMessage(AuctionItem auction, int amount, Mobile to)
        {
            if (to == null || to.Account == null || to.NetState == null)
            {
                return;
            }

            AuctionMessageGump gump = new AuctionMessageGump(auction, true, false, false)
            {
                Message = string.Format(AuctionSystem.ST[179], amount),
                OkText = "Close this message",
                ShowExpiration = false
            };

            to.SendGump(new AuctionNoticeGump(gump));
        }

        /// <summary>
        ///     Sends the confirmation request for the reserve not met to the auction owner
        /// </summary>
        /// <param name="item">The auction</param>
        public static void SendReserveMessageToOwner(AuctionItem item)
        {
            if (item.Owner == null || item.Owner.Account == null || item.Owner.NetState == null)
            {
                return;
            }

            string msg = string.Format(AuctionSystem.ST[180], item.HighestBid.Amount, item.Reserve);

            if (! item.IsValid())
            {
                msg += AuctionSystem.ST[181];
            }

            AuctionMessageGump gump = new AuctionMessageGump(item, false, true, true)
            {
                Message = msg,
                OkText = AuctionSystem.ST[182],
                CancelText = AuctionSystem.ST[183]
            };

            item.Owner.SendGump(new AuctionNoticeGump(gump));
        }

        /// <summary>
        ///     Sends the information message about the reserve not met to the buyer
        /// </summary>
        public static void SendReserveMessageToBuyer(AuctionItem item)
        {
            if (item.HighestBid.Mobile == null || item.HighestBid.Mobile.Account == null ||
                item.HighestBid.Mobile.NetState == null)
            {
                return;
            }

            AuctionMessageGump gump = new AuctionMessageGump(item, true, false, true)
            {
                Message =
                    String.Format(AuctionSystem.ST[184], AuctionSystem.DaysForConfirmation, item.HighestBid.Amount,
                        item.Reserve),
                OkText = AuctionSystem.ST[185]
            };

            item.HighestBid.Mobile.SendGump(new AuctionNoticeGump(gump));
        }

        /// <summary>
        ///     Informs the buyer that some of the items auctioned have been deleted.
        /// </summary>
        public static void SendInvalidMessageToBuyer(AuctionItem item)
        {
            Mobile m = item.HighestBid.Mobile;

            if (m == null || m.Account == null || m.NetState == null)
            {
                return;
            }

            string msg = string.Format(AuctionSystem.ST[186], item.HighestBid.Amount);

            if (! item.ReserveMet)
            {
                msg += AuctionSystem.ST[187];
            }

            AuctionMessageGump gump = new AuctionMessageGump(item, false, false, true)
            {
                Message = msg,
                OkText = AuctionSystem.ST[188],
                CancelText = AuctionSystem.ST[189]
            };

            m.SendGump(new AuctionNoticeGump(gump));
        }

        /// <summary>
        ///     Sends the invalid message to the owner.
        /// </summary>
        /// <param name="item"></param>
        public static void SendInvalidMessageToOwner(AuctionItem item)
        {
            Mobile m = item.Owner;

            if (m == null || m.Account == null || m.NetState == null)
            {
                return;
            }

            AuctionMessageGump gump = new AuctionMessageGump(item, true, true, true)
            {
                Message = AuctionSystem.ST[190],
                OkText = AuctionSystem.ST[185]
            };

            m.SendGump(new AuctionNoticeGump(gump));
        }
    }
}