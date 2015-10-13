#region References

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Server;
using Server.Accounting;
using Server.Items;
using Server.Mobiles;
using VitaNex;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Defines situations for pending situations
    /// </summary>
    public enum AuctionPendency : byte
    {
        /// <summary>
        ///     The user still has to make a decision
        /// </summary>
        Pending = 0,

        /// <summary>
        ///     The user OKs the auction
        /// </summary>
        Accepted = 1,

        /// <summary>
        ///     The user didn't accept the auction
        /// </summary>
        NotAccepted = 2
    }

    /// <summary>
    ///     Defines what happens with the auction item if the auction is ended by the staff
    /// </summary>
    public enum ItemFate
    {
        /// <summary>
        ///     The item is returned to the owner
        /// </summary>
        ReturnToOwner,

        /// <summary>
        ///     The item is taken by the staff
        /// </summary>
        ReturnToStaff,

        /// <summary>
        ///     The item is deleted
        /// </summary>
        Delete
    }

    /// <summary>
    ///     Specifies the type of message that should be dispatched for the buyer or the owner
    /// </summary>
    public enum AuctionMessage : byte
    {
        /// <summary>
        ///     No message should be dispatched
        /// </summary>
        None = 0,

        /// <summary>
        ///     An information message should be dispatched
        /// </summary>
        Information = 1,

        /// <summary>
        ///     A feedback message should be dispatched
        /// </summary>
        Response = 2
    }

    /// <summary>
    ///     An auction entry, holds all the information about a single auction process
    /// </summary>
    public class AuctionItem
    {
        #region ItemInfo class

        public class ItemInfo
        {
            private string m_Name;
            private Item m_Item;
            private string m_Props;

            public string Name
            {
                get { return m_Item != null ? m_Name : "N/A"; }
            }

            public Item Item
            {
                get { return m_Item; }
            }

            public string Properties
            {
                get { return m_Item != null ? m_Props : AuctionSystem.ST[146]; }
            }

            public ItemInfo(Item item)
            {
                m_Item = item;

                /*
				if (item.Name != null)
					m_Name = item.Name;
				else
					m_Name = m_StringList[ item.LabelNumber ] as string;
				*/

                m_Name = item.ResolveName(); //VNc Clilocs Extension

                if (item.Amount > 1)
                {
                    m_Name = string.Format("{0} {1}", item.Amount, m_Name);
                }

                if (item is MobileStatuette)
                {
                    m_Props = GetCreatureProperties((item as MobileStatuette).Creature);
                }
                else
                {
                    m_Props = GetItemProperties(item);
                }
            }

            public ItemInfo(GenericReader reader, int version)
            {
                Load(reader, version);
            }

            private static string GetCreatureProperties(BaseCreature creature)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<basefont color=#FFFFFF>");

                if (creature.Name != null)
                {
                    sb.AppendFormat("Name : {0}<br", creature.Name);
                }

                sb.AppendFormat(AuctionSystem.ST[147], creature.ControlSlots);
                sb.AppendFormat(AuctionSystem.ST[148], creature.IsBondable ? "Yes" : "No");
                sb.AppendFormat(AuctionSystem.ST[149], creature.Str);
                sb.AppendFormat(AuctionSystem.ST[150], creature.Dex);
                sb.AppendFormat(AuctionSystem.ST[151], creature.Int);

                int index = 0;
                Skill skill = null;

                while ((skill = creature.Skills[index++]) != null)
                {
                    if (skill.Value > 0)
                    {
                        sb.AppendFormat("{0} : {1}<br>", skill.Name, skill.Value);
                    }
                }

                return sb.ToString();
            }

            private ItemInfo()
            {
            }

            public void Save(GenericWriter writer)
            {
                // Version 1
                // Version 0
                writer.Write(m_Name);
                writer.Write(m_Item);
                writer.Write(m_Props);
            }

            public void Load(GenericReader reader, int version)
            {
                switch (version)
                {
                    case 1:
                    case 0:
                    {
                        m_Name = reader.ReadString();
                        m_Item = reader.ReadItem();
                        m_Props = reader.ReadString();
                    }
                        break;
                }
            }

            /// <summary>
            ///     Verifies if the mobile referenced by this item is still valid
            /// </summary>
            public void VeirfyIntegrity()
            {
                // If the creature is gone, delete the statuette
                if (m_Item is MobileStatuette && (m_Item as MobileStatuette).Creature == null)
                {
                    m_Item.Delete();
                    m_Item = null; // This will make this item invalid
                }
            }
        }

        #endregion

        #region Item Properties

        //private static LanguagePack m_StringList;

        /// <summary>
        ///     Gets an html formatted string with all the properies for the item
        /// </summary>
        /// <returns>A string object containing the html structure corresponding to the item properties</returns>
        private static string GetItemProperties(Item item)
        {
            if (item == null || item.PropertyList == null)
            {
                return AuctionSystem.ST[78];
            }

            if (Core.AOS)
            {
                #region AoS

                ObjectPropertyList plist = new ObjectPropertyList(item);
                item.GetProperties(plist);

                var data = plist.UnderlyingStream.UnderlyingStream.ToArray();
                ArrayList list = new ArrayList();

                int index = 15; // First localization number index

                while (true)
                {
                    uint number = 0;

                    if (index + 4 >= data.Length)
                    {
                        break;
                    }

                    number = (uint) (data[index++] << 24 | data[index++] << 16 | data[index++] << 8 | data[index++]);
                    ushort length = 0;

                    if (index + 2 > data.Length)
                    {
                        break;
                    }

                    length = (ushort) (data[index++] << 8 | data[index++]);

                    // Read the string
                    int end = index + length;

                    if (end >= data.Length)
                    {
                        end = data.Length - 1;
                    }

                    StringBuilder s = new StringBuilder();
                    while (index + 2 <= end + 1)
                    {
                        short next = (short) (data[index++] | data[index++] << 8);

                        if (next == 0)
                        {
                            break;
                        }

                        s.Append(Encoding.Unicode.GetString(BitConverter.GetBytes(next)));
                    }

                    list.Add(ComputeProperty((int) number, s.ToString()));
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<basefont color=#FFFFFF><p>");

                foreach (string prop in list)
                {
                    sb.AppendFormat("{0}<br>", prop);
                }

                return sb.ToString();

                #endregion
            }
            else
            {
                #region Non AoS

                StringBuilder sb = new StringBuilder();
                sb.Append("<basefont color=#FFFFFF><p>");

                // Get the item name
                sb.AppendFormat(
                    "{0}<br>",
                    !String.IsNullOrEmpty(item.Name)
                        ? item.Name
                        : Capitalize(item.ResolveName() /*m_StringList.Table[ item.LabelNumber ] as string*/));

                // Amount
                if (item.Amount > 1)
                {
                    sb.AppendFormat(AuctionSystem.ST[152], item.Amount);
                }

                // Loot type
                if (item.LootType != LootType.Regular)
                {
                    sb.AppendFormat("{0}<br>", item.LootType);
                }

                if (item is IUsesRemaining)
                {
                    sb.AppendFormat(AuctionSystem.ST[153], (item as IUsesRemaining).UsesRemaining);
                }

                // Manage item types

                if (item is BaseWand)
                {
                    #region Wands

                    BaseWand bw = item as BaseWand;
                    sb.AppendFormat(AuctionSystem.ST[154], bw.Effect);
                    sb.AppendFormat(AuctionSystem.ST[155], bw.Charges);

                    #endregion
                }
                else if (item is BaseArmor)
                {
                    #region Armor

                    BaseArmor ba = item as BaseArmor;

                    if (ba.PlayerConstructed)
                    {
                        if (ba.Crafter != null)
                        {
                            sb.AppendFormat(AuctionSystem.ST[156], ba.Crafter.Name);
                        }
                        sb.AppendFormat(AuctionSystem.ST[157], ba.Resource);
                    }

                    sb.AppendFormat(AuctionSystem.ST[158], ba.Quality);
                    sb.AppendFormat(AuctionSystem.ST[159], ba.HitPoints, ba.MaxHitPoints);

                    if (ba.Durability != ArmorDurabilityLevel.Regular)
                    {
                        sb.AppendFormat(AuctionSystem.ST[160], ba.Durability);
                    }

                    if (ba.ProtectionLevel != ArmorProtectionLevel.Regular)
                    {
                        sb.AppendFormat(AuctionSystem.ST[161], ba.ProtectionLevel);
                    }

                    #endregion
                }
                else if (item is BaseWeapon)
                {
                    #region Weapons

                    BaseWeapon bw = item as BaseWeapon;

                    if (bw.PlayerConstructed)
                    {
                        if (bw.Crafter != null)
                        {
                            sb.AppendFormat(AuctionSystem.ST[156], bw.Crafter.Name);
                        }
                        sb.AppendFormat(AuctionSystem.ST[157], bw.Resource);
                    }

                    sb.AppendFormat(AuctionSystem.ST[158], bw.Quality);
                    sb.AppendFormat(AuctionSystem.ST[159], bw.HitPoints, bw.MaxHitPoints);

                    if (bw.PoisonCharges > 0)
                    {
                        sb.AppendFormat(AuctionSystem.ST[162], bw.PoisonCharges, bw.Poison);
                    }

                    if (item is BaseRanged)
                    {
                        sb.AppendFormat(AuctionSystem.ST[163], bw.MaxRange);
                    }

                    if (bw.DamageLevel != WeaponDamageLevel.Regular)
                    {
                        sb.AppendFormat(AuctionSystem.ST[164], bw.DamageLevel);
                    }

                    if (bw.DurabilityLevel != WeaponDurabilityLevel.Regular)
                    {
                        sb.AppendFormat(AuctionSystem.ST[160], bw.DurabilityLevel);
                    }

                    if (bw.AccuracyLevel != WeaponAccuracyLevel.Regular)
                    {
                        if (bw.AccuracyLevel == WeaponAccuracyLevel.Accurate)
                        {
                            sb.AppendFormat(AuctionSystem.ST[165]);
                        }
                        else
                        {
                            sb.AppendFormat(AuctionSystem.ST[166], bw.AccuracyLevel);
                        }
                    }

                    if (bw.Slayer != SlayerName.None)
                    {
                        sb.AppendFormat(AuctionSystem.ST[167], bw.Slayer);
                    }

                    #endregion
                }
                else if (item is TreasureMap)
                {
                    #region Treasure Map

                    TreasureMap tm = item as TreasureMap;
                    sb.AppendFormat(AuctionSystem.ST[168], tm.ChestMap);

                    #endregion
                }
                else if (item is Spellbook)
                {
                    #region Spellbook

                    Spellbook sp = item as Spellbook;
                    sb.AppendFormat(AuctionSystem.ST[169], sp.SpellCount);

                    #endregion
                }
                else if (item is PotionKeg)
                {
                    #region Potion Keg

                    PotionKeg pk = item as PotionKeg;

                    int number;

                    if (pk.Held <= 0)
                    {
                        number = 502246; // The keg is empty.
                    }
                    else if (pk.Held < 5)
                    {
                        number = 502248; // The keg is nearly empty.
                    }
                    else if (pk.Held < 20)
                    {
                        number = 502249; // The keg is not very full.
                    }
                    else if (pk.Held < 30)
                    {
                        number = 502250; // The keg is about one quarter full.
                    }
                    else if (pk.Held < 40)
                    {
                        number = 502251; // The keg is about one third full.
                    }
                    else if (pk.Held < 47)
                    {
                        number = 502252; // The keg is almost half full.
                    }
                    else if (pk.Held < 54)
                    {
                        number = 502254; // The keg is approximately half full.
                    }
                    else if (pk.Held < 70)
                    {
                        number = 502253; // The keg is more than half full.
                    }
                    else if (pk.Held < 80)
                    {
                        number = 502255; // The keg is about three quarters full.
                    }
                    else if (pk.Held < 96)
                    {
                        number = 502256; // The keg is very full.
                    }
                    else if (pk.Held < 100)
                    {
                        number = 502257; // The liquid is almost to the top of the keg.
                    }
                    else
                    {
                        number = 502258; // The keg is completely full.
                    }

                    sb.AppendFormat(Capitalize(ClilocLNG.ENU.GetString(number) /*m_StringList.Table[number] as string*/));

                    #endregion
                }

                return sb.ToString();

                #endregion
            }
        }

        /// <summary>
        ///     Capitalizes each word in a string
        /// </summary>
        /// <param name="property">The input string</param>
        /// <returns>The output string </returns>
        private static string Capitalize(string property)
        {
            var parts = property.Split(' ');
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (part.Length == 0)
                {
                    continue;
                }

                char c = char.ToUpper(part[0]);

                part = part.Substring(1);
                sb.AppendFormat("{0}{1}", string.Concat(c, part), i < parts.Length - 1 ? " " : "");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Converts a localization number and its arguments to a valid string
        /// </summary>
        /// <param name="number">The localization number of the label</param>
        /// <param name="arguments">The arguments for the label</param>
        /// <returns>The translated string</returns>
        private static string ComputeProperty(int number, string arguments)
        {
            string prop = ClilocLNG.ENU.GetRawString(number); //m_StringList.Table[number] as string;

            if (prop == null)
            {
                return AuctionSystem.ST[170];
            }

            if (String.IsNullOrEmpty(arguments))
            {
                return Capitalize(prop);
            }

            var args = arguments.Split('\t');
            Regex reg = new Regex(@"~\d+\w*~", RegexOptions.None);
            MatchCollection matches = reg.Matches(prop, 0);

            if (matches.Count != args.Length)
            {
                return AuctionSystem.ST[171];
            }

            // Valid
            for (int i = 0; i < matches.Count; i++)
            {
                if (args[i].StartsWith("#"))
                {
                    int loc;

                    if (Int32.TryParse(args[i].Substring(1), out loc) && loc > 0)
                    {
                        args[i] = ClilocLNG.ENU.GetString(loc); //m_StringList.Table[loc] as string;
                    }
                }

                prop = prop.Replace(matches[i].Value, args[i]);
            }

            return Capitalize(prop);
        }

        #endregion

        #region Variables

        private Item m_Item;
        private Mobile m_Owner;
        private DateTime m_StartTime;
        private DateTime m_EndTime;
        private TimeSpan m_Duration = TimeSpan.FromDays(7);
        private int m_MinBid = 1000;
        private int m_Reserve = 2000;
        private string m_Description = "";
        private List<Bid> m_Bids;
        private string m_WebLink = "";
        private string m_ItemName;
        private bool m_Pending;
        private ItemInfo[] m_Items;
        private Guid m_ID;
        private AuctionPendency m_OwnerPendency = AuctionPendency.Pending;
        private AuctionPendency m_BuyerPendency = AuctionPendency.Pending;
        private AuctionMessage m_OwnerMessage = AuctionMessage.None;
        private AuctionMessage m_BuyerMessage = AuctionMessage.None;
        private DateTime m_PendingEnd;
        private int m_BuyNow;

        #region Props

        /// <summary>
        ///     States whether this auction allows the buy now feature
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool AllowBuyNow
        {
            get { return m_BuyNow > 0; }
        }

        /// <summary>
        ///     Gets the buy now value
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public int BuyNow
        {
            get { return m_BuyNow; }
            set { m_BuyNow = value; }
        }

        /// <summary>
        ///     Gets the date and time corrsponding to the moment when the pending situation will automatically end
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public DateTime PendingEnd
        {
            get { return m_PendingEnd; }
        }

        /// <summary>
        ///     Gets the item being sold at the auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public Item Item
        {
            get { return m_Item; }
        }

        /// <summary>
        ///     Gets the owner of the item
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public Mobile Owner
        {
            get { return m_Owner; }
        }

        /// <summary>
        ///     Gets the starting time for this auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public DateTime StartTime
        {
            get { return m_StartTime; }
        }

        /// <summary>
        ///     Gets the end time for this auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public DateTime EndTime
        {
            get { return m_EndTime; }
        }

        /// <summary>
        ///     Gets the running length of the auction for this item
        /// </summary>
        public TimeSpan Duration
        {
            get { return m_Duration; }
            set
            {
                try
                {
                    m_Duration = value;
                }
                catch
                {
                    m_Duration = TimeSpan.Zero;
                }
            }
        }

        /// <summary>
        ///     Gets the time to live left for this auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public TimeSpan TimeLeft
        {
            get { return m_EndTime - DateTime.UtcNow; }
        }

        /// <summary>
        ///     Gets or sets the minimum bid allowed for this item
        /// </summary>
        public int MinBid
        {
            get { return m_MinBid; }
            set { m_MinBid = value; }
        }

        /// <summary>
        ///     Gets or sets the reserve price for the item
        /// </summary>
        public int Reserve
        {
            get { return m_Reserve; }
            set { m_Reserve = value; }
        }

        /// <summary>
        ///     Gets or sets the description for this item
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        /// <summary>
        ///     A web link associated with this auction item
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public string WebLink
        {
            get { return m_WebLink; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (value.ToLower().StartsWith("http://") && value.Length > 7)
                    {
                        value = value.Substring(7);
                    }
                }

                m_WebLink = value;
            }
        }

        /// <summary>
        ///     Gets or sets the list of existing bids
        /// </summary>
        public List<Bid> Bids
        {
            get { return m_Bids; }
            set { m_Bids = value; }
        }

        /// <summary>
        ///     Gets the account that's selling this item
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public Account Account
        {
            get { return m_Owner != null && m_Owner.Account != null ? m_Owner.Account as Account : null; }
        }

        /// <summary>
        ///     Gets or sets the name of the item being sold
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public string ItemName
        {
            get { return m_ItemName; }
            set { m_ItemName = value; }
        }

        /// <summary>
        ///     True if the auction is over but the reserve hasn't been met and the owner still haven't decided
        ///     if to sell the item or not. This value makes no sense before the auction is over.
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool Pending
        {
            get { return m_Pending; }
        }

        /// <summary>
        ///     Gets the definitions of the items sold
        /// </summary>
        public ItemInfo[] Items
        {
            get { return m_Items; }
        }

        /// <summary>
        ///     Gets the number of items sold
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public int ItemCount
        {
            get { return m_Items.Length; }
        }

        /// <summary>
        ///     Gets the unique ID of this auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public Guid ID
        {
            get { return m_ID; }
        }

        #endregion

        #endregion

        #region Serialization

        /// <summary>
        ///     Saves the auction item into the world file
        /// </summary>
        /// <param name="writer"></param>
        public void Serialize(GenericWriter writer)
        {
            // Version 1
            writer.Write(m_BuyNow);

            // Version 0
            writer.Write(m_Owner);
            writer.Write(m_StartTime);
            writer.Write(m_Duration);
            writer.Write(m_MinBid);
            writer.Write(m_Reserve);
            writer.Write(m_Duration);
            writer.Write(m_Description);
            writer.Write(m_WebLink);
            writer.Write(m_Pending);
            writer.Write(m_ItemName);
            writer.Write(m_Item);
            writer.Write(m_ID.ToString());
            writer.WriteDeltaTime(m_EndTime);
            writer.Write((byte) m_OwnerPendency);
            writer.Write((byte) m_BuyerPendency);
            writer.Write((byte) m_OwnerMessage);
            writer.Write((byte) m_BuyerMessage);
            writer.WriteDeltaTime(m_PendingEnd);

            // Items
            writer.Write(m_Items.Length);

            foreach (ItemInfo info in m_Items)
            {
                info.Save(writer);
            }

            // Bids
            writer.Write(m_Bids.Count);

            foreach (Bid bid in m_Bids)
            {
                bid.Serialize(writer);
            }
        }

        /// <summary>
        ///     Loads an AuctionItem
        /// </summary>
        /// <returns>An AuctionItem</returns>
        public static AuctionItem Deserialize(GenericReader reader, int version)
        {
            AuctionItem auction = new AuctionItem();

            switch (version)
            {
                case 1:
                    auction.m_BuyNow = reader.ReadInt();
                    goto case 0;
                case 0:
                {
                    auction.m_Owner = reader.ReadMobile();
                    auction.m_StartTime = reader.ReadDateTime();
                    auction.m_Duration = reader.ReadTimeSpan();
                    auction.m_MinBid = reader.ReadInt();
                    auction.m_Reserve = reader.ReadInt();
                    auction.m_Duration = reader.ReadTimeSpan();
                    auction.m_Description = reader.ReadString();
                    auction.m_WebLink = reader.ReadString();
                    auction.m_Pending = reader.ReadBool();
                    auction.m_ItemName = reader.ReadString();
                    auction.m_Item = reader.ReadItem();
                    auction.m_ID = new Guid(reader.ReadString());
                    auction.m_EndTime = reader.ReadDeltaTime();
                    auction.m_OwnerPendency = (AuctionPendency) reader.ReadByte();
                    auction.m_BuyerPendency = (AuctionPendency) reader.ReadByte();
                    auction.m_OwnerMessage = (AuctionMessage) reader.ReadByte();
                    auction.m_BuyerMessage = (AuctionMessage) reader.ReadByte();
                    auction.m_PendingEnd = reader.ReadDeltaTime();

                    int count = reader.ReadInt();
                    auction.m_Items = new ItemInfo[count];

                    for (int i = 0; i < count; i++)
                    {
                        auction.m_Items[i] = new ItemInfo(reader, version);
                    }

                    count = reader.ReadInt();

                    for (int i = 0; i < count; i++)
                    {
                        auction.Bids.Add(Bid.Deserialize(reader, version));
                    }
                }
                    break;
            }

            return auction;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the minimum increment required for the auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public int BidIncrement
        {
            get
            {
                if (m_MinBid <= 100)
                {
                    return 10;
                }

                if (m_MinBid <= 500)
                {
                    return 20;
                }

                if (m_MinBid <= 1000)
                {
                    return 50;
                }

                if (m_MinBid <= 5000)
                {
                    return 100;
                }

                if (m_MinBid <= 10000)
                {
                    return 200;
                }

                if (m_MinBid <= 20000)
                {
                    return 250;
                }

                if (m_MinBid <= 50000)
                {
                    return 500;
                }

                return 1000;
            }
        }

        /// <summary>
        ///     States whether an item has at least one bid
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool HasBids
        {
            get { return m_Bids.Count > 0; }
        }

        /// <summary>
        ///     Gets the highest bid for this item
        /// </summary>
        public Bid HighestBid
        {
            get { return m_Bids.Count > 0 ? m_Bids[0] : null; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public Mobile HighestBidder
        {
            get { return m_Bids.Count > 0 ? (m_Bids[0]).Mobile : null; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int HighestBidValue
        {
            get { return m_Bids.Count > 0 ? (m_Bids[0]).Amount : 0; }
        }

        /// <summary>
        ///     States whether the reserve has been met for this item
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool ReserveMet
        {
            get { return HighestBid != null && HighestBid.Amount >= m_Reserve; }
        }

        /// <summary>
        ///     States whether this auction has expired
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool Expired
        {
            get { return DateTime.UtcNow > m_EndTime; }
        }

        /// <summary>
        ///     Gets the minimum bid that a player can place
        /// </summary>
        public int MinNewBid
        {
            get { return HighestBid != null ? HighestBid.Amount : m_MinBid; }
        }

        /// <summary>
        ///     Gets the next deadline required by this auction
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public DateTime Deadline
        {
            get { return !Expired ? m_EndTime : (m_Pending ? m_PendingEnd : DateTime.MaxValue); }
        }

        /// <summary>
        ///     Specifies if the pending period has timed out
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool PendingExpired
        {
            get { return DateTime.UtcNow >= m_PendingEnd; }
        }

        /// <summary>
        ///     Gets the time left before the pending period expired
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public TimeSpan PendingTimeLeft
        {
            get { return m_PendingEnd - DateTime.UtcNow; }
        }

        /// <summary>
        ///     States whether this auction is selling a pet
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public bool Creature
        {
            get { return m_Item is MobileStatuette; }
        }

        /// <summary>
        ///     Gets the BaseCreature sold through this auction. This will be null when selling an item.
        /// </summary>
        [CommandProperty(AccessLevel.Administrator)]
        public BaseCreature Pet
        {
            get
            {
                if (Creature)
                {
                    MobileStatuette ms = m_Item as MobileStatuette;

                    if (ms != null && ms.Creature != null)
                    {
                        return ms.Creature;
                    }
                }

                return null;
            }
        }

        #endregion

        /// <summary>
        ///     Creates a new AuctionItem
        /// </summary>
        /// <param name="item">The item being sold</param>
        /// <param name="owner">The owner of the item</param>
        public AuctionItem(Item item, Mobile owner)
        {
            m_ID = Guid.NewGuid();
            m_Item = item;
            m_Owner = owner;
            m_Bids = new List<Bid>();

            if (!Creature)
            {
                m_Item.Visible = false;
                m_Owner.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[172]);
            }

            // Item name
            /*
			if (m_Item.Name != null && m_Item.Name.Length > 0)
			{
				m_ItemName = m_Item.Name;
			}
			else
			{
				m_ItemName = m_StringList.Table[m_Item.LabelNumber] as string;
			}*/
            m_ItemName = m_Item.ResolveName(m_Owner);

            if (m_Item.Amount > 1)
            {
                m_ItemName = string.Format("{0} {1}", m_Item.Amount, m_ItemName);
            }
        }

        /// <summary>
        ///     Creates an AuctionItem - for use in deserialization
        /// </summary>
        private AuctionItem()
        {
            m_Bids = new List<Bid>();
        }

        /// <summary>
        ///     Gets the item info corresponding to the index value
        /// </summary>
        [IndexerName("SoldItem")]
        public ItemInfo this[int index]
        {
            get { return index > -1 && index < m_Items.Length ? m_Items[index] : null; }
        }

        /// <summary>
        ///     Confirms the auction item and adds it into the system
        /// </summary>
        public void Confirm()
        {
            m_StartTime = DateTime.UtcNow;
            m_EndTime = m_StartTime + m_Duration;

            if (Creature && Pet != null)
            {
                Pet.ControlTarget = null;
                Pet.ControlOrder = OrderType.Stay;
                Pet.Internalize();

                Pet.SetControlMaster(null);
                Pet.SummonMaster = null;
            }

            // Calculate all the ItemInfo
            if (m_Item is Container && m_Item.Items.Count > 0)
            {
                // Container with items
                m_Items = new ItemInfo[m_Item.Items.Count];

                for (int i = 0; i < m_Items.Length; i++)
                {
                    m_Items[i] = new ItemInfo(m_Item.Items[i]);
                }
            }
            else
            {
                m_Items = new ItemInfo[1];

                m_Items[0] = new ItemInfo(m_Item);
            }

            AuctionSystem.Add(this);
            AuctionScheduler.UpdateDeadline(m_EndTime);
            AuctionLog.WriteNewAuction(this);
        }

        /// <summary>
        ///     Cancels the new auction and returns the item to the owner
        /// </summary>
        public void Cancel()
        {
            if (!Creature)
            {
                m_Item.Visible = true;
                m_Owner.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[173]);
            }
            else
            {
                if (m_Item is MobileStatuette)
                {
                    ((MobileStatuette) m_Item).GiveCreatureTo(m_Owner);
                }

                m_Owner.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[174]);
            }
        }

        /// <summary>
        ///     Sends the associated web link to a mobile
        /// </summary>
        /// <param name="m">The mobile that should receive the web link</param>
        public void SendLinkTo(Mobile m)
        {
            if (m != null && m.NetState != null && !String.IsNullOrEmpty(m_WebLink))
            {
                m.LaunchBrowser(string.Format("http://{0}", m_WebLink));
            }
        }

        /// <summary>
        ///     Verifies if a mobile can place a bid on this item
        /// </summary>
        /// <param name="m">The Mobile trying to bid</param>
        /// <returns>True if the mobile is allowed to bid</returns>
        public bool CanBid(Mobile m)
        {
            if (m.AccessLevel > AccessLevel.Player)
            {
                return false; // Staff shoudln't bid. This will also give the bids view to staff members.
            }

            if (Account == (m.Account as Account)) // Same account as auctioneer
            {
                return false;
            }

            return !Creature || Pet != null && Pet.CanBeControlledBy(m);
        }

        /// <summary>
        ///     Verifies if a mobile is the owner of this auction (checks accounts)
        /// </summary>
        /// <param name="m">The mobile being checked</param>
        /// <returns>True if the mobile is the owner of the auction</returns>
        public bool IsOwner(Mobile m)
        {
            return (Account == (m.Account as Account));
        }

        /// <summary>
        ///     Places a new bid
        /// </summary>
        /// <param name="from">The Mobile bidding</param>
        /// <param name="amount">The bid amount</param>
        /// <returns>True if the bid has been added and accepted</returns>
        public bool PlaceBid(Mobile from, int amount)
        {
            if (!CanBid(from))
            {
                return false;
            }

            if (HighestBid != null)
            {
                if (amount <= HighestBid.Amount)
                {
                    from.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[176]);
                    return false;
                }
            }
            else if (amount <= m_MinBid)
            {
                from.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[177]);
                return false;
            }

            int delta = 0;

            if (HighestBid != null)
            {
                delta = amount - HighestBid.Amount;
            }
            else
            {
                delta = amount - m_MinBid;
            }

            if (BidIncrement > delta)
            {
                from.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[204], BidIncrement);
                return false;
            }

            // Ok, do bid
            Bid bid = Bid.CreateBid(from, amount);

            if (bid != null)
            {
                if (HighestBid != null)
                {
                    HighestBid.Outbid(this); // Return money to previous highest bidder
                }

                m_Bids.Insert(0, bid);
                AuctionLog.WriteBid(this);

                // Check for auction extension
                if (AuctionSystem.LateBidExtention > TimeSpan.Zero)
                {
                    TimeSpan timeLeft = m_EndTime - DateTime.UtcNow;

                    if (timeLeft < TimeSpan.FromMinutes(5.0))
                    {
                        m_EndTime += AuctionSystem.LateBidExtention;
                        bid.Mobile.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[230]);
                    }
                }
            }

            return bid != null;
        }

        /// <summary>
        ///     Forces the end of the auction when the item or creature has been deleted
        /// </summary>
        public void EndInvalid()
        {
            AuctionSystem.Auctions.Remove(this);

            if (HighestBid != null)
            {
                AuctionGoldCheck gold = new AuctionGoldCheck(this, AuctionResult.ItemDeleted);
                GiveItemTo(HighestBid.Mobile, gold);
            }

            // The item has been deleted, no need to return it to the owner.
            // If it's a statuette, delete it
            if (Creature && m_Item != null)
            {
                m_Item.Delete();
            }

            AuctionLog.WriteEnd(this, AuctionResult.ItemDeleted, null, null);

            // Over.
        }

        /// <summary>
        ///     Forces the end of the auction and removes it from the system
        /// </summary>
        /// <param name="m">The staff member deleting the auction</param>
        /// <param name="itemfate">Specifies what should occur with the item</param>
        public void StaffDelete(Mobile m, ItemFate itemfate)
        {
            if (AuctionSystem.Auctions.Contains(this))
            {
                AuctionSystem.Auctions.Remove(this);
            }
            else if (AuctionSystem.Pending.Contains(this))
            {
                AuctionSystem.Pending.Remove(this);
            }

            if (HighestBid != null)
            {
                AuctionGoldCheck gold = new AuctionGoldCheck(this, AuctionResult.StaffRemoved);
                GiveItemTo(HighestBid.Mobile, gold);
            }

            AuctionItemCheck item = new AuctionItemCheck(this, AuctionResult.StaffRemoved);
            string comments = null;

            switch (itemfate)
            {
                case ItemFate.Delete:

                    item.ForceDelete();
                    comments = "The item has been deleted";
                    break;

                case ItemFate.ReturnToOwner:

                    GiveItemTo(m_Owner, item);
                    comments = "The item has been returned to the owner";
                    break;

                case ItemFate.ReturnToStaff:

                    GiveItemTo(m, item);
                    comments = "The item has been claimed by the staff";
                    break;
            }

            AuctionLog.WriteEnd(this, AuctionResult.StaffRemoved, m, comments);

            // OVer.
        }

        /// <summary>
        ///     Ends the auction.
        ///     This function is called by the auction system during its natural flow
        /// </summary>
        /// <param name="m">The Mobile eventually forcing the ending</param>
        public void End(Mobile m)
        {
            AuctionSystem.Auctions.Remove(this);

            if (HighestBid == null)
            {
                // No bids, simply return the item
                AuctionCheck item = new AuctionItemCheck(this, AuctionResult.NoBids);
                GiveItemTo(m_Owner, item);

                // Over, this auction no longer exists
                AuctionLog.WriteEnd(this, AuctionResult.NoBids, m, null);
            }
            else
            {
                // Verify that all items still exist too, otherwise make it pending
                if (IsValid() && ReserveMet)
                {
                    // Auction has been succesful
                    AuctionCheck item = new AuctionItemCheck(this, AuctionResult.Succesful);
                    GiveItemTo(HighestBid.Mobile, item);

                    AuctionCheck gold = new AuctionGoldCheck(this, AuctionResult.Succesful);
                    GiveItemTo(m_Owner, gold);

                    // Over, this auction no longer exists
                    AuctionLog.WriteEnd(this, AuctionResult.Succesful, m, null);
                }
                else
                {
                    // Reserve hasn't been met or auction isn't valid, this auction is pending
                    m_Pending = true;
                    m_PendingEnd = DateTime.UtcNow + TimeSpan.FromDays(AuctionSystem.DaysForConfirmation);
                    AuctionSystem.Pending.Add(this);

                    DoOwnerMessage();
                    DoBuyerMessage();

                    Mobile owner = GetOnlineMobile(m_Owner);
                    Mobile buyer = GetOnlineMobile(HighestBid.Mobile);

                    SendMessage(owner);
                    SendMessage(buyer);

                    AuctionScheduler.UpdateDeadline(m_PendingEnd);

                    AuctionLog.WritePending(this, ReserveMet ? "Item deleted" : "Reserve not met");
                }
            }
        }

        /// <summary>
        ///     Gets the online mobile belonging to a mobile's account
        /// </summary>
        private Mobile GetOnlineMobile(Mobile m)
        {
            if (m == null || m.Account == null)
            {
                return null;
            }

            if (m.NetState != null)
            {
                return m;
            }

            Account acc = m.Account as Account;

            if (acc != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    Mobile mob = acc[i];

                    if (mob != null && mob.NetState != null)
                    {
                        return mob;
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Ends the auction.
        ///     This function is called when the system is being disbanded and all auctions must be forced close
        ///     The item will be returned to the original owner, and the highest bidder will receive the money back
        /// </summary>
        public void ForceEnd()
        {
            AuctionSystem.Auctions.Remove(this);

            // Turn the item into a deed and give it to the auction owner
            AuctionCheck item = new AuctionItemCheck(this, AuctionResult.SystemStopped);

            GiveItemTo(m_Owner, item); // This in case the item has been wiped or whatever

            if (HighestBid != null)
            {
                HighestBid.AuctionCanceled(this);
            }

            AuctionLog.WriteEnd(this, AuctionResult.SystemStopped, null, null);
        }

        /// <summary>
        ///     This function will put an item in a player's backpack, and if full put it inside their bank.
        ///     If the mobile is null, this will delete the item.
        /// </summary>
        /// <param name="m">The mobile receiving the item</param>
        /// <param name="item">The item being given</param>
        private static void GiveItemTo(Mobile m, Item item)
        {
            if (m == null || item == null)
            {
                if (item != null)
                {
                    item.Delete();
                }

                return;
            }

            if (m.Backpack != null && m.Backpack.TryDropItem(m, item, false))
            {
                return;
            }

            if (m.BankBox != null)
            {
                m.BankBox.AddItem(item);
            }
            else
            {
                item.Delete(); // Sucks to be you
            }
        }

        /// <summary>
        ///     Verifies if all the items being sold through this auction still exist
        /// </summary>
        /// <returns>True if all the items still exist</returns>
        public bool IsValid()
        {
            return m_Items.All(info => info.Item != null);
        }

        /// <summary>
        ///     Defines what kind of message the auction owner should receive. Doesn't send any messages.
        /// </summary>
        public void DoOwnerMessage()
        {
            if (m_Owner == null || m_Owner.Account == null)
            {
                // If owner deleted the character, accept the auction by default
                m_OwnerPendency = AuctionPendency.Accepted;
            }
            else if (!IsValid() && ReserveMet)
            {
                // Assume the owner will sell even if invalid when reserve is met
                m_OwnerPendency = AuctionPendency.Accepted;
            }
            else if (!ReserveMet)
            {
                m_OwnerPendency = AuctionPendency.Pending;
                m_OwnerMessage = AuctionMessage.Response; // This is always reserve not met for the owner
            }
            else if (!IsValid())
            {
                m_OwnerPendency = AuctionPendency.Accepted;
                m_OwnerMessage = AuctionMessage.Information; // This is always about validty for the owner
            }
        }

        /// <summary>
        ///     Defines what kind of message the buyer should receive. Doesn't send any messages.
        /// </summary>
        public void DoBuyerMessage()
        {
            if (HighestBid.Mobile == null || HighestBid.Mobile.Account == null)
            {
                // Buyer deleted the character, accept the auction by default
                m_BuyerPendency = AuctionPendency.Accepted;
            }
            else if (!IsValid())
            {
                // Send the buyer a message about missing items in the auction
                m_BuyerMessage = AuctionMessage.Response;
                m_BuyerPendency = AuctionPendency.Pending;
            }
            else if (!ReserveMet)
            {
                // Assume the buyer will buy even if the reserve hasn't been met
                m_BuyerPendency = AuctionPendency.Accepted;
                // Send the buyer a message to inform them of the reserve issue
                m_BuyerMessage = AuctionMessage.Information;
            }
        }

        /// <summary>
        ///     Validates the pending status of the auction. This method should be called whenever a pendency
        ///     value is changed. If the auction has been validated, it will finalize items and remove the auction from the system.
        ///     This is the only method that should be used to finalize a pending auction.
        /// </summary>
        public void Validate()
        {
            if (!AuctionSystem.Pending.Contains(this))
            {
                return;
            }

            if (m_OwnerPendency == AuctionPendency.Accepted && m_BuyerPendency == AuctionPendency.Accepted)
            {
                // Both parts confirmed the auction
                m_Pending = false;
                AuctionSystem.Pending.Remove(this);

                AuctionCheck item = new AuctionItemCheck(this, AuctionResult.PendingAccepted);
                AuctionCheck gold = new AuctionGoldCheck(this, AuctionResult.PendingAccepted);

                GiveItemTo(HighestBid.Mobile, item); // Item to buyer

                GiveItemTo(m_Owner, gold); // Gold to owner

                // Over, this auction no longer exists
                AuctionLog.WriteEnd(this, AuctionResult.PendingAccepted, null, null);
            }
            else if (m_OwnerPendency == AuctionPendency.NotAccepted || m_BuyerPendency == AuctionPendency.NotAccepted)
            {
                // At least one part refused
                m_Pending = false;
                AuctionSystem.Pending.Remove(this);

                AuctionCheck item = new AuctionItemCheck(this, AuctionResult.PendingRefused);
                AuctionCheck gold = new AuctionGoldCheck(this, AuctionResult.PendingRefused);

                GiveItemTo(m_Owner, item); // Give item back to owner

                GiveItemTo(HighestBid.Mobile, gold); // Give gold to highest bidder

                // Over, this auction no longer exists
                AuctionLog.WriteEnd(this, AuctionResult.PendingRefused, null, null);
            }
        }

        /// <summary>
        ///     Sends any message this auction might have in store for a given mobile
        /// </summary>
        /// <param name="to">The Mobile logging into the server</param>
        public void SendMessage(Mobile to)
        {
            if (!m_Pending || to == null)
            {
                return;
            }

            if (to == m_Owner || (m_Owner != null && to.Account.Equals(m_Owner.Account)))
            {
                // This is the owner loggin in
                if (m_OwnerMessage != AuctionMessage.None)
                {
                    // Owner needs a message
                    switch (m_OwnerMessage)
                    {
                        case AuctionMessage.Information:
                            // Send information message about validity condition
                            AuctionMessaging.SendInvalidMessageToOwner(this);
                            break;
                        case AuctionMessage.Response:
                            // Send reserve not met confirmation request
                            AuctionMessaging.SendReserveMessageToOwner(this);
                            break;
                    }
                }
            }
            else if (to == HighestBid.Mobile ||
                     (HighestBid.Mobile != null && to.Account.Equals(HighestBid.Mobile.Account)))
            {
                // This is the buyer logging in
                if (m_BuyerMessage != AuctionMessage.None)
                {
                    // Buyer should receive a message
                    switch (m_BuyerMessage)
                    {
                        case AuctionMessage.Information:
                            // Send message about reserve not met condition
                            AuctionMessaging.SendReserveMessageToBuyer(this);
                            break;
                        case AuctionMessage.Response:
                            // Send request to confirm invalid items auction
                            AuctionMessaging.SendInvalidMessageToBuyer(this);
                            break;
                    }
                }
            }
        }

        /// <summary>
        ///     Confirms an information message
        /// </summary>
        /// <param name="owner">True if the message was sent to the owner, false if to the buyer</param>
        public void ConfirmInformationMessage(bool owner)
        {
            if (owner)
            {
                // Owner
                m_OwnerMessage = AuctionMessage.None; // Don't resent
            }
            else
            {
                // Buyer
                m_BuyerMessage = AuctionMessage.None;
            }
        }

        /// <summary>
        ///     Gives a response to a message
        /// </summary>
        /// <param name="owner">True if the message was sent to the owner, false if to the buyer</param>
        /// <param name="ok">The response to the message</param>
        public void ConfirmResponseMessage(bool owner, bool ok)
        {
            if (owner)
            {
                m_OwnerPendency = ok ? AuctionPendency.Accepted : AuctionPendency.NotAccepted;
            }
            else
            {
                m_BuyerPendency = ok ? AuctionPendency.Accepted : AuctionPendency.NotAccepted;
            }

            Validate();
        }

        /// <summary>
        ///     The pending period has timed out and the auction must end unsuccesfully
        /// </summary>
        public void PendingTimeOut()
        {
            AuctionSystem.Pending.Remove(this);

            m_OwnerPendency = AuctionPendency.NotAccepted;
            m_BuyerPendency = AuctionPendency.NotAccepted;
            m_OwnerMessage = AuctionMessage.None;
            m_BuyerMessage = AuctionMessage.None;

            AuctionCheck item = new AuctionItemCheck(this, AuctionResult.PendingTimedOut);
            AuctionCheck gold = new AuctionGoldCheck(this, AuctionResult.PendingTimedOut);

            GiveItemTo(m_Owner, item);

            GiveItemTo(HighestBid.Mobile, gold);

            // Over, this auction no longer exists
            AuctionLog.WriteEnd(this, AuctionResult.PendingTimedOut, null, null);
        }

        /// <summary>
        ///     Verifies is a mobile has bid on this auction
        /// </summary>
        public bool MobileHasBids(Mobile m)
        {
            return m_Bids.Any(bid => bid.Mobile == m);
        }

        /// <summary>
        ///     Outputs relevant information about this auction
        /// </summary>
        public void Profile(StringBuilder content)
        {
            content.AppendLine("ID : {0}", m_ID);
            content.AppendLine("Name : {0}", m_ItemName);

            if (m_Owner != null && m_Owner.Account is Account)
            {
                content.AppendLine(
                    "Owner : {0} [ Account {1} - Serial {2} ]", m_Owner.Name, ((Account) m_Owner.Account).Username,
                    m_Owner.Serial);
            }
            else
            {
                content.AppendLine("Owner : no longer existing");
            }

            content.AppendLine("Starting bid: {0}", m_MinBid);
            content.AppendLine("Reserve : {0}", m_Reserve);

            content.AppendLine("Created on {0} at {1}", m_StartTime.ToShortDateString(), m_StartTime.ToShortTimeString());
            content.AppendLine("Duration: {0}", m_Duration.ToString());
            content.AppendLine("End Time: {0} at {1}", m_EndTime.ToShortDateString(), m_EndTime.ToShortTimeString());

            content.AppendLine("Expired : {0}", Expired.ToString());
            content.AppendLine("Pending : {0}", Pending.ToString());
            content.AppendLine("Next Deadline : {0} at {1}", Deadline.ToShortDateString(), Deadline.ToShortTimeString());

            content.AppendLine();

            if (Creature)
            {
                content.AppendLine("** This auction is selling a pet");

                // Pet
                if (m_Item != null && Pet != null)
                {
                    content.AppendLine("Creature: {0}", Pet.Serial);
                    content.AppendLine("Statuette : {0}", m_Item.Serial);
                    content.AppendLine("Type : {0}", m_Item.Name);
                }
                else
                {
                    content.AppendLine("Pet deleted, this auction is invalid");
                }
            }
            else
            {
                // Items
                content.AppendLine("{0} Items", m_Items.Length);

                foreach (ItemInfo item in m_Items)
                {
                    content.AppendFormat("- {0}", item.Name);

                    if (item.Item != null)
                    {
                        content.AppendLine(" [{0}]", item.Item.Serial);
                    }
                    else
                    {
                        content.AppendLine(" [Deleted]");
                    }
                }
            }

            content.AppendLine();
            content.AppendLine("{0} Bids", m_Bids.Count);

            foreach (Bid bid in m_Bids)
            {
                bid.Profile(content);
            }

            content.AppendLine();
        }

        /// <summary>
        ///     Attempts to buy now
        /// </summary>
        /// <param name="m">The user trying to purchase</param>
        /// <returns>True if the item has been sold</returns>
        public bool DoBuyNow(Mobile m)
        {
            if (!Banker.Withdraw(m, m_BuyNow))
            {
                m.SendMessage(AuctionSystem.MessageHue, AuctionSystem.ST[211]);
                return false;
            }

            AuctionSystem.Auctions.Remove(this);

            if (HighestBid != null)
            {
                HighestBid.Outbid(this);
            }

            // Simulate bid
            Bid bid = new Bid(m, BuyNow);
            m_Bids.Insert(0, bid);

            AuctionGoldCheck gold = new AuctionGoldCheck(this, AuctionResult.BuyNow);
            AuctionItemCheck item = new AuctionItemCheck(this, AuctionResult.BuyNow);

            GiveItemTo(m, item);
            GiveItemTo(m_Owner, gold);

            // Over.
            AuctionLog.WriteEnd(this, AuctionResult.BuyNow, m, null);

            return true;
        }

        /// <summary>
        ///     Verifies if the eventual pets in this auction are gone
        /// </summary>
        public void VeirfyIntergrity()
        {
            foreach (ItemInfo ii in m_Items)
            {
                ii.VeirfyIntegrity();
            }
        }
    }
}