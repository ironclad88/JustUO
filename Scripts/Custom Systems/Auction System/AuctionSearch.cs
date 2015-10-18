#region References

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Provides search methods for the auction system
    /// </summary>
    public class AuctionSearch
    {
        /// <summary>
        ///     Merges search results
        /// </summary>
        public static List<AuctionItem> Merge(List<AuctionItem> first, List<AuctionItem> second)
        {
            var result = new List<AuctionItem>(first);

            foreach (AuctionItem item in second)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        ///     Performs a text search
        /// </summary>
        /// <param name="items">The items to search</param>
        /// <param name="text">The text search, names should be whitespace separated</param>
        public static List<AuctionItem> SearchForText(List<AuctionItem> items, string text)
        {
            var split = text.Split(' ');
            var result = new List<AuctionItem>();

            return split.Aggregate(result, (current, s) => Merge(current, TextSearch(items, s)));
        }

        /// <summary>
        ///     Performs a text search
        /// </summary>
        /// <param name="list">The AuctionItem objects to search</param>
        /// <param name="name">The text to search for</param>
        private static List<AuctionItem> TextSearch(List<AuctionItem> list, string name)
        {
            var results = new List<AuctionItem>();

            if (list == null || name == null)
            {
                return results;
            }

            IEnumerator ie = null;

            try
            {
                name = name.ToLower();

                ie = list.GetEnumerator();

                while (ie.MoveNext())
                {
                    AuctionItem item = ie.Current as AuctionItem;

                    if (item == null)
                    {
                        continue;
                    }

                    if (item.ItemName.ToLower().IndexOf(name, StringComparison.Ordinal) > -1)
                    {
                        results.Add(item);
                        break;
                    }

                    if (item.Description.ToLower().IndexOf(name, StringComparison.Ordinal) > -1)
                    {
                        results.Add(item);
                        break;
                    }

                    // Search individual items
                    foreach (AuctionItem.ItemInfo info in item.Items)
                    {
                        if (info.Name.ToLower().IndexOf(name, StringComparison.Ordinal) > -1)
                        {
                            results.Add(item);
                            break;
                        }

                        if (info.Properties.ToLower().IndexOf(name, StringComparison.Ordinal) > .1)
                        {
                            results.Add(item);
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                IDisposable d = ie as IDisposable;

                if (d != null)
                {
                    d.Dispose();
                }
            }

            return results;
        }

        /// <summary>
        ///     Performs a search for types being auctioned
        /// </summary>
        /// <param name="list">The items to search</param>
        /// <param name="types">The list of types to find matches for</param>
        public static List<AuctionItem> ForTypes(List<AuctionItem> list, List<Type> types)
        {
            var results = new List<AuctionItem>();

            if (list == null || types == null)
            {
                return results;
            }

            IEnumerator ie = null;

            try
            {
                ie = list.GetEnumerator();

                while (ie.MoveNext())
                {
                    AuctionItem item = ie.Current as AuctionItem;

                    if (item == null)
                    {
                        continue;
                    }

                    if (types.Any(t => MatchesType(item, t)))
                    {
                        results.Add(item);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                IDisposable d = ie as IDisposable;

                if (d != null)
                {
                    d.Dispose();
                }
            }

            return results;
        }

        /// <summary>
        ///     Verifies if a specified type is a match to the items sold through an auction
        /// </summary>
        /// <param name="item">The AuctionItem being evaluated</param>
        /// <param name="type">The type looking to match</param>
        /// <returns>True if there's a match</returns>
        private static bool MatchesType(AuctionItem item, Type type)
        {
            return item.Items.Where(info => info.Item != null).Any(info => type.IsInstanceOfType(info.Item));
        }

        /// <summary>
        ///     Performs a search for Artifacts by evaluating the ArtifactRarity property
        /// </summary>
        /// <param name="items">The list of items to search</param>
        /// <returns>An ArrayList of results</returns>
        public static List<AuctionItem> ForArtifacts(List<AuctionItem> items)
        {
            var results = new List<AuctionItem>();

            foreach (AuctionItem auction in items)
            {
                foreach (AuctionItem.ItemInfo info in auction.Items)
                {
                    Item item = info.Item;

                    if (item == null)
                    {
                        continue;
                    }

                    Type t = item.GetType();
                    PropertyInfo prop = null;

                    try
                    {
                        prop = t.GetProperty("ArtifactRarity");
                    }
                    catch
                    {
                    }

                    if (prop == null)
                    {
                        continue;
                    }

                    int rarity = (int) prop.GetValue(item, null);

                    if (rarity <= 0)
                    {
                        continue;
                    }

                    results.Add(auction);
                    break;
                }
            }

            return results;
        }

        /// <summary>
        ///     Searches a list of auctions for ICommodities
        /// </summary>
        /// <param name="items">The list to search</param>
        /// <returns>An ArrayList of results</returns>
        public static List<AuctionItem> ForCommodities(List<AuctionItem> items)
        {
            return
                items.Where(
                    auction =>
                        (auction.Items.Where(info => info.Item != null).Select(info => info.Item.GetType())).Any(
                            t => t.GetInterface("ICommodity") != null)).ToList();
        }
    }
}