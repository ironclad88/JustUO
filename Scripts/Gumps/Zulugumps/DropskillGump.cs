using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;

/*
 *  Author Oscar Ternström
 */

namespace Server.Gumps.Zulugumps
{

    public class DropskillGump : Gump
    {

        // skill increase
        const double SKILL_INC = 100;
        // number of skill boosts
        const int NUMBER_OF_BOOSTS = 3;

        public DropskillGump(Mobile owner)
            : this(owner, ResurrectMessage.Generic, false)
        {
        }


        public DropskillGump(Mobile owner, ResurrectMessage msg, bool fromSacrifice)
            : this(owner, msg)
        {
        }

        public DropskillGump(Mobile owner, ResurrectMessage msg)
            : base(100, 0)
        {

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(90, 30, 450, 538, 9200);
            AddLabel(299, 50, 0, @"Drop skill");
            //AddItem(255, 50, 4029);

            AddLabel(101, 100, 0, @"Archery");
            AddLabel(101, 130, 0, @"Fencing");
            AddLabel(101, 160, 0, @"Mace Fighting");
            AddLabel(101, 190, 0, @"Parrying");
            AddLabel(101, 220, 0, @"Swordsmanship");
            AddLabel(101, 250, 0, @"Tactics");
            AddLabel(101, 280, 0, @"Wrestling");
            AddLabel(101, 310, 0, @"Healing");
            AddLabel(101, 340, 0, @"Vetrinary");
            AddLabel(100, 370, 0, @"Alchemy");
            AddLabel(101, 400, 0, @"Inscription");
            AddLabel(101, 430, 0, @"Magery");
            AddLabel(101, 460, 0, @"Meditation");
            AddLabel(101, 490, 0, @"Resist");
            AddLabel(101, 520, 0, @"Spirit Speak");


            AddLabel(247, 100, 0, @"Begging");
            AddLabel(247, 130, 0, @"Detect Hidden");
            AddLabel(247, 160, 0, @"Hiding");
            AddLabel(247, 190, 0, @"Lockpick");
            AddLabel(247, 220, 0, @"Poisoning");
            AddLabel(247, 250, 0, @"Remove Trap");
            AddLabel(247, 280, 0, @"Snooping");
            AddLabel(247, 310, 0, @"Stealing");
            AddLabel(247, 340, 0, @"Stealth");
            AddLabel(247, 370, 0, @"Anatomy");
            AddLabel(247, 400, 0, @"Animal Lore");
            AddLabel(247, 430, 0, @"Animal Taming");
            AddLabel(247, 460, 0, @"Camping");
            AddLabel(247, 490, 0, @"Herding");
            AddLabel(247, 520, 0, @"Taste ID");


            AddLabel(400, 100, 0, @"Tracking");
            AddLabel(400, 130, 0, @"Arms Lore");
            AddLabel(400, 160, 0, @"Blacksmithy");
            AddLabel(400, 190, 0, @"Bowcraft");
            AddLabel(400, 220, 0, @"Carpentry");
            AddLabel(400, 250, 0, @"Cooking");
            AddLabel(400, 280, 0, @"Item ID");
            AddLabel(400, 310, 0, @"Cartography");
            AddLabel(400, 340, 0, @"Tailoring");
            AddLabel(400, 370, 0, @"Tinkering");
            AddLabel(400, 400, 0, @"Fishing");
            AddLabel(400, 430, 0, @"Mining");
            AddLabel(400, 460, 0, @"Lumberjacking");

            AddButton(195, 100, 210, 211, 1, GumpButtonType.Reply, 0, "Archery"); // done
            AddButton(195, 130, 210, 211, 2, GumpButtonType.Reply, 0, "Fencing");
            AddButton(195, 160, 210, 211, 3, GumpButtonType.Reply, 0, "Mace Fighting");
            AddButton(195, 190, 210, 211, 4, GumpButtonType.Reply, 0, "Parrying");
            AddButton(195, 220, 210, 211, 5, GumpButtonType.Reply, 0, "Swordsmanship");
            AddButton(195, 250, 210, 211, 6, GumpButtonType.Reply, 0, "Tactics");
            AddButton(195, 280, 210, 211, 7, GumpButtonType.Reply, 0, "Wrestling");
            AddButton(195, 310, 210, 211, 8, GumpButtonType.Reply, 0, "Healing");
            AddButton(195, 340, 210, 211, 9, GumpButtonType.Reply, 0, "Vetrinary");
            AddButton(195, 370, 210, 211, 10, GumpButtonType.Reply, 0, "Alchemy");
            AddButton(195, 400, 210, 211, 11, GumpButtonType.Reply, 0, "Inscription");
            AddButton(195, 430, 210, 211, 12, GumpButtonType.Reply, 0, "Magery");
            AddButton(195, 460, 210, 211, 13, GumpButtonType.Reply, 0, "Meditation");
            AddButton(195, 490, 210, 211, 14, GumpButtonType.Reply, 0, "Resist");
            AddButton(195, 520, 210, 211, 15, GumpButtonType.Reply, 0, "Spirit Speak");

            AddButton(345, 100, 210, 211, 16, GumpButtonType.Reply, 0, "Begging");
            AddButton(345, 130, 210, 211, 17, GumpButtonType.Reply, 0, "Detect Hidden");
            AddButton(345, 160, 210, 211, 18, GumpButtonType.Reply, 0, "Hiding");
            AddButton(345, 190, 210, 211, 19, GumpButtonType.Reply, 0, "Lockpick");
            AddButton(345, 220, 210, 211, 20, GumpButtonType.Reply, 0, "Poisoning");
            AddButton(345, 250, 210, 211, 21, GumpButtonType.Reply, 0, "Remove Trap");
            AddButton(345, 280, 210, 211, 22, GumpButtonType.Reply, 0, "Snooping");
            AddButton(345, 310, 210, 211, 23, GumpButtonType.Reply, 0, "Stealing");
            AddButton(345, 340, 210, 211, 24, GumpButtonType.Reply, 0, "Stealth");
            AddButton(345, 370, 210, 211, 25, GumpButtonType.Reply, 0, "Anatomy");
            AddButton(345, 400, 210, 211, 26, GumpButtonType.Reply, 0, "Animal Lore");
            AddButton(345, 430, 210, 211, 27, GumpButtonType.Reply, 0, "Animal Taming");
            AddButton(345, 460, 210, 211, 28, GumpButtonType.Reply, 0, "Camping");
            AddButton(345, 490, 210, 211, 29, GumpButtonType.Reply, 0, "Herding");
            AddButton(345, 520, 210, 211, 30, GumpButtonType.Reply, 0, "Taste ID");

            AddButton(490, 100, 210, 211, 31, GumpButtonType.Reply, 0, "Tracking");
            AddButton(490, 130, 210, 211, 32, GumpButtonType.Reply, 0, "Arms Lore");
            AddButton(490, 160, 210, 211, 33, GumpButtonType.Reply, 0, "Blacksmithy");
            AddButton(490, 190, 210, 211, 34, GumpButtonType.Reply, 0, "Bowcraft");
            AddButton(490, 220, 210, 211, 35, GumpButtonType.Reply, 0, "Carpentry");
            AddButton(490, 250, 210, 211, 36, GumpButtonType.Reply, 0, "Cooking");
            AddButton(490, 280, 210, 211, 37, GumpButtonType.Reply, 0, "Item ID");
            AddButton(490, 310, 210, 211, 38, GumpButtonType.Reply, 0, "Cartography");
            AddButton(490, 340, 210, 211, 39, GumpButtonType.Reply, 0, "Tailoring");
            AddButton(490, 370, 210, 211, 40, GumpButtonType.Reply, 0, "Tinkering");
            AddButton(490, 400, 210, 211, 41, GumpButtonType.Reply, 0, "Fishing");
            AddButton(490, 430, 210, 211, 42, GumpButtonType.Reply, 0, "Mining");
            AddButton(490, 460, 210, 211, 43, GumpButtonType.Reply, 0, "Lumberjacking");
            
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 1:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Archery, "Archery"));
                    break;
                case 2:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Fencing, "Fencing"));
                    break;
                case 3:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Macing, "Mace fighting"));
                    break;
                case 4:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Parry, "Parrying"));
                    break;
                case 5:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Swords, "Swordsmanship"));
                    break;
                case 6:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Tactics, "Tactics"));
                    break;
                case 7:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Wrestling, "Wrestling"));
                    break;
                case 8:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Healing, "Healing"));
                    break;
                case 9:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Veterinary, "Vetrinary"));
                    break;
                case 10:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Archery, "Archery"));
                    break;
                case 11:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Inscribe, "Inscription"));
                    break;
                case 12:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Magery, from.Skills.Magery.Name));
                    break;
                case 13:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Meditation, from.Skills.Meditation.Name));
                    break;
                case 14:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.MagicResist, "Magic Resistance"));
                    break;
                case 15:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.SpiritSpeak, "Spirit speak"));
                    break;
                case 16:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Begging, from.Skills.Begging.Name));
                    break;
                case 17:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.DetectHidden, "Detect hidden"));
                    break;
                case 18:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Hiding, from.Skills.Hiding.Name));
                    break;
                case 19:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Lockpicking, from.Skills.Lockpicking.Name));
                    break;
                case 20:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Poisoning, from.Skills.Poisoning.Name));
                    break;
                case 21:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.RemoveTrap, "Remove trap"));
                    break;
                case 22:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Snooping, from.Skills.Snooping.Name));
                    break;
                case 23:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Stealing, from.Skills.Stealing.Name));
                    break;
                case 24:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Stealth, from.Skills.Stealth.Name));
                    break;
                case 25:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Anatomy, from.Skills.Anatomy.Name));
                    break;
                case 26:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.AnimalLore, "Animal lore"));
                    break;
                case 27:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.AnimalTaming, "Animal taming"));
                    break;
                case 28:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Camping, from.Skills.Camping.Name));
                    break;
                case 29:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Herding, from.Skills.Herding.Name));
                    break;
                case 30:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.TasteID, "Taste identification"));
                    break;
                case 31:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Tracking, from.Skills.Tracking.Name));
                    break;
                case 32:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.ArmsLore, "Armslore"));
                    break;
                case 33:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Blacksmith, "Blacksmithing"));
                    break;
                case 34:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Fletching, "Fletching"));
                    break;
                case 35:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Carpentry, from.Skills.Carpentry.Name));
                    break;
                case 36:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Cooking, from.Skills.Cooking.Name));
                    break;
                case 37:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.ItemID, "Item ID"));
                    break;
                case 38:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Cartography, from.Skills.Cartography.Name));
                    break;
                case 39:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Tailoring, from.Skills.Tailoring.Name));
                    break;
                case 40:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Tinkering, from.Skills.Tinkering.Name));
                    break;
                case 41:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Fishing, from.Skills.Fishing.Name));
                    break;
                case 42:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Mining, from.Skills.Mining.Name));
                    break;
                case 43:
                    from.CloseGump(typeof(DropskillGump));
                    from.SendGump(new SureCheckGump(from, from.Skills.Lumberjacking, from.Skills.Lumberjacking.Name));
                    break;
            }


        }

    }
}