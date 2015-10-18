#region References

using Server;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Delegate for targeting callbacks
    /// </summary>
    public delegate void AuctionTargetCallback(Mobile from, object targeted);

    /// <summary>
    ///     Delegate for gumps navigation
    /// </summary>
    public delegate void AuctionGumpCallback(Mobile user);
}