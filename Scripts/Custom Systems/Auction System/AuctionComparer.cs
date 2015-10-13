#region References

using System;
using System.Collections.Generic;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Defines auction sorting methods
    /// </summary>
    public enum AuctionSorting
    {
        /// <summary>
        ///     Sorting by item name
        /// </summary>
        Name,

        /// <summary>
        ///     Sorting by date of creation
        /// </summary>
        Date,

        /// <summary>
        ///     Sorting by time left for the auction
        /// </summary>
        TimeLeft,

        /// <summary>
        ///     Sorting by the number of bids
        /// </summary>
        Bids,

        /// <summary>
        ///     Sorting by value of minimum bid
        /// </summary>
        MinimumBid,

        /// <summary>
        ///     Sorting by value of the higherst bid
        /// </summary>
        HighestBid
    }

    /// <summary>
    ///     Provides sorting for auction listings
    /// </summary>
    public class AuctionComparer : IComparer<AuctionItem>
    {
        private readonly bool m_Ascending;
        private readonly AuctionSorting m_Sorting;

        public AuctionComparer(AuctionSorting sorting, bool ascending)
        {
            m_Ascending = ascending;
            m_Sorting = sorting;
        }

        #region IComparer<T>

        public int Compare(AuctionItem x, AuctionItem y)
        {
            if (!m_Ascending)
            {
                var temp = x;
                x = y;
                y = temp;
            }

            if (x == y)
            {
                return 0;
            }

            if (x == null)
            {
                return 1;
            }

            if (y == null)
            {
                return -1;
            }

            switch (m_Sorting)
            {
                case AuctionSorting.Bids:
                    return x.Bids.Count.CompareTo(y.Bids.Count);

                case AuctionSorting.Date:
                    return x.StartTime.CompareTo(y.StartTime);

                case AuctionSorting.HighestBid:
                    return x.MinNewBid.CompareTo(y.MinNewBid);

                case AuctionSorting.MinimumBid:
                    return x.MinBid.CompareTo(y.MinBid);

                case AuctionSorting.TimeLeft:
                    return x.TimeLeft.CompareTo(y.TimeLeft);

                case AuctionSorting.Name:
                    return String.Compare(x.ItemName, y.ItemName, StringComparison.Ordinal);
            }

            return 0;
        }

        #endregion
    }
}