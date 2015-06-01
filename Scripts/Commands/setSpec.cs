using System;
using System.Text;
using Server.Mobiles;

namespace Server.Commands
{
    public class SetSpec
    {
        public static void Initialize()
        {
            CommandSystem.Register("setSpec", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));
        }

        [Usage("SetSpec")]
        [Description("Set level and spec.")]
        private static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            string arguments = e.ArgString;
            Console.WriteLine(arguments);
            string[] argsSplitted = arguments.Split(' ');
            if (e.Length != 1)
            {
                e.Mobile.SendMessage("setSpec <class name> <spec level>");
            }
            if (Convert.ToInt32(argsSplitted[1]) >= 7)
            {
                e.Mobile.SendMessage("to high spec entered. Max 6");
            }
            //Console.WriteLine(argsSplitted[0], argsSplitted[1]);
            getClass(argsSplitted[0], Convert.ToInt32(argsSplitted[1]), m);
            m.GetSpec();
        }

        private static string getClass(string className, int level, Mobile player){
            /*
            int spec1 = 75;
            int spec2 = 90;
            int spec3 = 105;
            int spec4 = 120;
            int spec5 = 135;
            int spec6 = 150;
            */
        
            foreach (Skill skill in player.Skills) // set all skills to 0
            {
                if (skill.Cap < 0)
                {
                    skill.SetCap(0);
                }

                skill.SetBase(0, true, false);
            }

            int skillInc = 0;
            switch (level)
            {
                case 1:
                    skillInc = 75;
                    break;
                case 2:
                    skillInc = 90;
                    break;
                case 3:
                    skillInc = 105;
                    break;
                case 4:
                    skillInc = 120;
                    break;
                case 5:
                    skillInc = 135;
                    break;
                case 6:
                    skillInc = 150;
                    break;
            }

            if (className == "Mage" || className == "mage")
            {
                player.Skills.Magery.Base = skillInc;
                player.Skills.MagicResist.Base = skillInc;
                player.Skills.Inscribe.Base = skillInc;
                player.Skills.EvalInt.Base = skillInc;
                player.Skills.Alchemy.Base = skillInc;
                player.Skills.Meditation.Base = skillInc;
                player.Skills.SpiritSpeak.Base = skillInc;
                player.Skills.ItemID.Base = skillInc;
            }
            else if (className == "Warrior" || className == "warrior")
            {
                player.Skills.Anatomy.Base = skillInc;
                player.Skills.Fencing.Base = skillInc;
                player.Skills.Swords.Base = skillInc;
                player.Skills.Tactics.Base = skillInc;
                player.Skills.Healing.Base = skillInc;
                player.Skills.Macing.Base = skillInc;
                player.Skills.Parry.Base = skillInc;
                player.Skills.Wrestling.Base = skillInc;
            }
            else if (className == "Theif" || className == "theif")
            {
                player.Skills.DetectHidden.Base = skillInc;
                player.Skills.Hiding.Base = skillInc;
                player.Skills.Stealing.Base = skillInc;
                player.Skills.Stealth.Base = skillInc;
                player.Skills.Lockpicking.Base = skillInc;
                player.Skills.Snooping.Base = skillInc;
                player.Skills.Poisoning.Base = skillInc;
                player.Skills.RemoveTrap.Base = skillInc;
            }
            else if (className == "Bard" || className == "bard")
            {
                player.Skills.Begging.Base = skillInc;
                player.Skills.Cartography.Base = skillInc;
                player.Skills.Discordance.Base = skillInc;
                player.Skills.Peacemaking.Base = skillInc;
                player.Skills.Provocation.Base = skillInc;
                player.Skills.Herding.Base = skillInc;
                player.Skills.Musicianship.Base = skillInc;
                player.Skills.TasteID.Base = skillInc;
            }
            else if (className == "Crafter" || className == "crafter")
            {
                player.Skills.Fletching.Base = skillInc;
                player.Skills.Mining.Base = skillInc;
                player.Skills.Lumberjacking.Base = skillInc;
                player.Skills.ArmsLore.Base = skillInc;
                player.Skills.Carpentry.Base = skillInc;
                player.Skills.Blacksmith.Base = skillInc;
                player.Skills.Tailoring.Base = skillInc;
                player.Skills.Tinkering.Base = skillInc;
            }
            else if (className == "Ranger" || className == "ranger")
            {
                player.Skills.AnimalLore.Base = skillInc;
                player.Skills.AnimalTaming.Base = skillInc;
                player.Skills.Archery.Base = skillInc;
                player.Skills.Tracking.Base = skillInc;
                player.Skills.Fishing.Base = skillInc;
                player.Skills.Cooking.Base = skillInc;
                player.Skills.Veterinary.Base = skillInc;
                player.Skills.Camping.Base = skillInc;
            }
            return "";
        }

        }
    }
