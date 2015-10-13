#region References

using VitaNex.Text;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Provides access to localized text used by the system
    /// </summary>
    public class StringTable : IndependentLanguagePack
    {
        public StringTable()
        {
            Define(0, "Auction Delivery Service");
            Define(1, "Delivering...");
            Define(2, "Place the gold in your bank");
            Define(3, "Place the item in your bank");
            Define(4, "Item");
            Define(5, "Gold");
            Define(6, "View the auction screen");
            Define(7, "Close");
            Define(8, "Welcome to the Auction House");
            Define(9, "Auction An Item");
            Define(10, "View All Auctions");
            Define(11, "View Your Auctions");
            Define(12, "View Your Bids");
            Define(13, "View Your Pendencies");
            Define(14, "Exit");
            Define(15, "The auction system has been stopped. Please try again later.");
            Define(16, "Search");
            Define(17, "Sort");
            Define(18, "Page {0}/{1}"); // Page 1/3 - used when displaying more than one page
            Define(19, "Displaying {0} items"); // {0} is the number of items displayed in an auction listing
            Define(20, "No items to display");
            Define(21, "Previous Page");
            Define(22, "Next Page");
            Define(23, "An unexpected error occurred. Please try again.");
            Define(24, "The selected item has expired. Please refresh the auction listing.");
            Define(25, "Auction Messaging System");
            Define(26, "Auction:");
            Define(27, "View details");
            Define(28, "Not available");
            Define(29, "Message Details:");
            Define(30, "Time left for all part to take their decisions: {0} days and {1} hours.");
            Define(31, "The auction no longer exists, therefore this message is no longer valid.");
            Define(32, "Auction House Search");
            Define(33, "Enter the terms to search for (leave blank for all items)");
            Define(34, "Limit search to those types:");
            Define(35, "Maps");
            Define(36, "Artifacts");
            Define(37, "Power and Stat Cap Scrolls");
            Define(38, "Resources");
            Define(39, "Jewels");
            Define(40, "Weapons");
            Define(41, "Armor");
            Define(42, "Shields");
            Define(43, "Reagents");
            Define(44, "Potions");
            Define(45, "BOD (Large)");
            Define(46, "BOD (Small)");
            Define(47, "Cancel");
            Define(48, "Search only within your current results");
            Define(49, "Auction House Sorting System");
            Define(50, "Name");
            Define(51, "Ascending");
            Define(52, "Descending");
            Define(53, "Date");
            Define(54, "Oldest first");
            Define(55, "Newest first");
            Define(56, "Time Left");
            Define(57, "Shortest first");
            Define(58, "Longest first");
            Define(59, "Number of bids");
            Define(60, "Few first");
            Define(61, "Most first");
            Define(62, "Minimum bid value");
            Define(63, "Lowest first");
            Define(64, "Highest first");
            Define(65, "Highest bid value");
            Define(66, "Cancel Sorting");
            Define(67, "{0} Items total"); // Number of items inside a container - auction view gump
            Define(68, "Starting Bid");
            Define(69, "Reserve");
            Define(70, "Highest Bid");
            Define(71, "No bids yet");
            Define(72, "Web Link");
            Define(73, "{0} Days {1} Hours"); // 5 Days 2 Hours
            Define(74, "{0} Hours"); // 18 Hours
            Define(75, "{0} Minutes"); // 50 Minutes
            Define(76, "{0} Seconds"); // 10 Seconds
            Define(77, "Pending");
            Define(78, "N/A");
            Define(79, "Bid on this item:");
            Define(80, "View Bids");
            Define(81, "Owner's Description");
            Define(82, "Item Hue");
            Define(83, "[3D Clients don't display item hues]");
            Define(84, "This auction is closed and is no longer accepting bids");
            Define(85, "Invalid bid. Bid not accepted.");
            Define(86, "Bidding History");
            Define(87, "Who");
            Define(88, "Amount");
            Define(89, "Return to the Auction");
            Define(90, "Creatures Division");
            Define(91, "Stable the pet");
            Define(92, "Use this ticket to stable your pet.");
            Define(93, "Stabled pets must be claimed"); // This and the following form one sentence
            Define(94, "within a week time from the stable.");
            Define(95, "You will not pay for this service.");
            Define(96, "AUCTION SYSTEM TERMINATION");
            Define(
                97,
                "<basefont color=#FFFFFF>You are about to terminate the auction system running on this server. This will cause all current auctions to end right now. All items will be returned to the original owners and the highest bidders will receive their money back.<br><br>Are you sure you wish to do this?");
            Define(98, "Yes I want to terminate the system");
            Define(99, "Do nothing and let the system running");
            Define(100, "New Auction Configuration");
            Define(101, "Duration");
            Define(102, "Days");
            Define(103, "Description (Optional)");
            Define(104, "Web Link (Optional)");
            Define(106, "I have read the auction agreement and wish"); // This and the following form one sentence
            Define(107, "to continue and commit this auction.");
            Define(108, "Cancel and exit");
            Define(109, "The starting bid must be at least 1 gold coin.");
            Define(110, "The reserve must be greater or equal than the starting bid");
            Define(111, "An auction must last at least {0} days.");
            Define(112, "An auction can last at most {0} days.");
            Define(113, "Please speicfy a name for your auction");
            Define(114, "The reserve you specified is too high. Either lower it or raise the starting bid.");
            Define(115, "The system has been closed");
            Define(116, "The item carried by this check no longer exists due to reasons outside the auction system");
            Define(117, "The content of the check has been delivered to your bank box.");
            Define(118, "Couldn't add the item to your bank box. Please make sure it has enough space to hold it.");
            Define(119, "Your godly powers allow you to access this check.");
            Define(120, "This check can only be used by its owner");
            Define(121, "You are not supposed to remove this item manually. Ever.");
            Define(122, "Gold Check from the Auction System");
            Define(123, "You have been outbid for the auction of {0}. Your bid was {1}.");
            Define(124, "Auction system stopped. Returning your bid of {1} gold for {0}");
            Define(125, "Auction for {0} has been canceled by either you or the owner. Returning your bid.");
            Define(126, "Your bid of {0} for {1} didn't meet the reserve and the owner decided not to accept your offer");
            Define(
                127,
                "The auction was in pending state due to either reserve not being met or because one or more items have been deleted. No decision has been taken by the involved parts to resolve the auction therefore it has been ended unsuccesfully.");
            Define(128, "The auction has been cancelled because the auctioned item has been removed from the world.");
            Define(129, "You have sold {0} through the auction system. The highest bid was {1}.");
            Define(130, "{0} is not a valid reason for an auction gold check");
            Define(131, "Creature Check from the Auction System");
            Define(132, "Item Check from the Auction System");
            Define(133, "Your auction for {0} has terminated without bids.");
            Define(134, "Your auction for {0} has been canceled");
            Define(135, "The auction system has been stopped and your auctioned item is being returned to you. ({0})");
            Define(136, "The auction has been cancelled because the auctioned item has been removed from the world.");
            Define(137, "You have succesfully purchased {0} through the auction system. Your bid was {1}.");
            Define(138, "{0} is not a valid reason for an auction item check");
            Define(139, "You can't auction creatures that don't belong to you.");
            Define(140, "You can't auction dead creatures");
            Define(141, "You can't auction summoned creatures");
            Define(142, "You can't auction familiars");
            Define(143, "Please unload your pet's backpack first");
            Define(144, "The pet represented by this check no longer exists");
            Define(145, "Sorry we're closed at this time. Please try again later.");
            Define(146, "This item no longer exists");
            Define(147, "Control Slots : {0}<br>"); // For a pet
            Define(148, "Bondable : {0}<br>");
            Define(149, "Str : {0}<br>");
            Define(150, "Dex : {0}<br>");
            Define(151, "Int : {0}<br>");
            Define(152, "Amount: {0}<br>");
            Define(153, "Uses remaining : {0}<br>");
            Define(154, "Spell : {0}<br>");
            Define(155, "Charges : {0}<br>");
            Define(156, "Crafted by {0}<br>");
            Define(157, "Resource : {0}<br>");
            Define(158, "Quality : {0}<br>");
            Define(159, "Hit Points : {0}/{1}<br>");
            Define(160, "Durability : {0}<br>");
            Define(161, "Protection: {0}<br>");
            Define(162, "Poison Charges : {0} [{1}]<br>");
            Define(163, "Range : {0}<br>");
            Define(164, "Damage : {0}<br>");
            Define(165, "Accurate<br>"); // Accuracy level, might want to leave as is
            Define(166, "{0} Accurate<br>"); // Will become: Supremely Accurate/Extremely Accurate
            Define(167, "Slayer : {0}<br>");
            Define(168, "Map : {0}<br>");
            Define(169, "Spell Count : {0}");
            Define(170, "Invalid Localization");
            Define(171, "Invalid");
            Define(172, "The item you selected has been removed and will be held under strict custody");
            Define(173, "You cancel the auction and your item is returned to you");
            Define(174, "You cancel the auction and your pet is returned to you");
            Define(175, "You don't have enough control slots to bid on that creature");
            Define(176, "Your bid isn't high enough");
            Define(177, "Your bid doesn't reach the minimum bid");
            Define(178, "Your stable is full. Please free some space before claiming this creature.");
            Define(
                179,
                "You have been outbid. An auction check of {0} gold coins has been deposited in your backpack or bankbox. View the auction details if you wish to place another bid.");
            Define(
                180,
                "Your auction has ended, but the highest bid didn't reach the reserve you specified. You now have option to decide whether to sell your item or not.<br><br>The highest bid is {0}. Your reserve was {1}.");
            Define(
                181,
                "<br><br>Some of the items auctioned have been deleted during the duration of the auction. The buyer will have to accept the new auction before it can be completed.");
            Define(182, "Yes I want to sell my item even if the reserve hasn't been met");
            Define(183, "No I don't want to sell and I want my item returned to me");
            Define(
                184,
                "Your bid didn't meet the reserve specified by the auction owner. The item owner will now have to decide whether to sell or not.<br><br>Your bid was {1}. The reserve is {2}.");
            Define(185, "Close this message");
            Define(
                186,
                "You have participated and won an auction. However due to external events one or more of the items auctioned no longer exist. Please review the auction by using the view details button and decide whether you wish to purchase the items anyway or not.<br><br>Your bid was {0}");
            Define(
                187,
                "<br><br>Your bid didn't meet the reserve specified by the owner. The owner will not have to deicde whether they wish to sell or not");
            Define(188, "Yes I want to purchase anyway");
            Define(189, "No I don't want to purchase and wish to have my money back");
            Define(
                190,
                "Some of the items you acutioned no longer exists because of external reasons. The buyer will now decide whether to purchase or not.");
            Define(191, "Please target the item you wish to put on auction...");
            Define(192, "You cannot have more than {0} auctions active on your account");
            Define(193, "You can only auction items");
            Define(194, "You cannot put that on auction");
            Define(195, "One of the items you're auctioning isn't identified");
            Define(196, "One of the items inside the container isn't allowed at the auction house");
            Define(197, "You cannot sell containers with items nested in other containers");
            Define(198, "You can only auction items that you have in your backpack or in your bank box");
            Define(199, "You don't have enough money in your bank to place this bid");
            Define(200, "The auction system is currently stopped");
            Define(201, "Delete");
            Define(
                202,
                "You have bid on an auction that has been removed by the shard staff. Your bid is now being returned to you.");
            Define(203, "Your auction has been closed by the shard staff and your item is now returned to you.");
            Define(204, "Your bid must be at least {0} higher than the current bid");
            Define(205, "You cannot auction items that are not movable");
            Define(206, "Props");
            Define(207, "The selected auction is no longer active. Please refresh the auctions list.");

            // VERSION 1.7 Begins here

            Define(208, "Allow Buy Now For:");
            Define(209, "If you chose to use the Buy Now feature, please specify a value higher than the reserve");
            Define(210, "Buy this item now for {0} gold");

            // Entry 105 has been changed from the previous version. Buy Now section has been added.
            Define(
                105, @"<basefont color=#FF0000>Auction Agreement<br>
<basefont color=#FFFFFF>By completing and submitting this form you agree to take part in the auction system. The item you are auctioning will be removed from your inventory and will be returned to you only if you cancel this auction, if the auction is unsuccesfull and the item isn't sold, or if staff forces the auction system to stop.
<basefont color=#FF0000>Starting Bid:<basefont color=#FFFFFF> This is the minimum bid accepted for this item. Set this value to something reasonable, and possibly lower than what you expect to collect for the item in the end.
<basefont color=#FF0000>Reserve:<basefont color=#FFFFFF> This value will not be know to the bidders, and you should consider it as a safe price for your item. If the final bid reaches this value, the sale will be automatically finalized by the system. If on the other hand the highest bid is somewhere between the starting bid and the reserve, you will be given the option of choosing whether to sell the item or not. You will have 7 days after the end of the auction to take this decision. If you don't, the auction system will assume you decided not to sell and will return the item to you and the money to the highest bidder. Bidders will not see the value of the reserve, but only a statement saying whether it has been reached or not.
<basefont color=#FF0000>Duration:<basefont color=#FFFFFF> This value specifies how many days the auction will last from its creation time. At the end of this period, the system will proceed to either finalize the sale, return the item and the highest bid, or wait for a decision in case of a reserve not reached issue.
<basefont color=#FF0000>Buy Now:<basefont color=#FFFFFF> This options allows you to specify a safe price at which you are willing to sell the item before the end of the auction. If the buyer is willing to pay this price, they will be able to purchase the item right away without any further bids.
<basefont color=#FF0000>Name:<basefont color=#FFFFFF> This should be a short name defining your auction. The system will suggest a name based on the item you're selling, but you might wish to change it in some circumstances.
<basefont color=#FF0000>Description:<basefont color=#FFFFFF> You can write pretty much anything you wish here about your item. Keep in mind that the item properties you see when moving your mouse over the item will be available to bidders automatically, so there's no need for you to describe those.
<basefont color=#FF0000>Web Link:<basefont color=#FFFFFF> You can add a web link to this auction, in case you have a web page with further information or discussion about this item
<br>
Once you commit this auction you will not be able to retrieve your item until the auction ends. Make sure you understand what this means before committing.");

            Define(211, "You don't have enough money in your bank to buy this item");
            Define(212,
                "You don't have enough space in your bank to make this deposit. Please free some space and try again.");
            Define(213, "Auction Control");
            Define(214, "Properties");
            Define(215, "Account : {0}");
            Define(216, "Auction Owner : {0}");
            Define(217, "Online");
            Define(218, "Offline");
            Define(219, "Auctioned Item");
            Define(220, "Place it in your backpack");
            Define(221, "Put the item back into the system");
            Define(222, "Auction");
            Define(223, "End auction now");
            Define(224, "Close and return item to the owner");
            Define(225, "Close and put the item in your pack");
            Define(226, "Close and delete the item");
            Define(227, "Auction Staff Panel");

            //
            // VERSION 1.8
            //

            Define(228, "{0} gold coins have been withdrawn from your bank account as payment for this service.");
            Define(229,
                "You don't have enough gold in your bank to pay for this serice. The cost of this auction is: {0}.");
            Define(
                230,
                "Your bid has been placed too close to the auction deadline so the auction duration has been extended to accept further bids.");
        }
    }
}