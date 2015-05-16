using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Mobiles;

namespace Server
{
    class SpecLevels
    {

        int[] SpecLevelSkills = { 600, 720, 840, 960, 1080, 1200 };
        double[] SpecLevelPercent = { 60, 68, 76, 84, 92, 100 };

        public void GetLevel(Mobile player, out SpecClasse curr_classe, out int curr_level)
        {
            double s_warrior, s_bard, s_crafter, s_mage, s_ranger, s_thief;
            double total = 0;
            total += s_warrior = totalWarrior(player);
            total += s_bard = totalBard(player);
            total += s_crafter = totalCrafter(player);
            total += s_mage = totalMage(player);
            total += s_ranger = totalRanger(player);
            total += s_thief = totalThief(player);

            //calculate which spec the player has most skill points in
            double max = 0;
            SpecClasse spec = SpecClasse.None;
            int level = 0;

            if (s_warrior > max)
            {
                max = s_warrior;
                spec = SpecClasse.Warrior;
            }
            if (s_bard > max)
            {
                max = s_bard;
                spec = SpecClasse.Bard;
            }
            if (s_crafter > max)
            {
                max = s_crafter;
                spec = SpecClasse.Crafter;
            }
            if (s_mage > max)
            {
                max = s_mage;
                spec = SpecClasse.Mage;
            }
            if (s_ranger > max)
            {
                max = s_ranger;
                spec = SpecClasse.Ranger;
            }
            if (s_thief > max)
            {
                max = s_thief;
                spec = SpecClasse.Thief;
            }

            // spec == current classe, now calculate level
            if(max < SpecLevelSkills[0])
            {
                spec = SpecClasse.None;
            }
            else
            {
                int i;
                for (i = 0; i < SpecLevelSkills.Length && max >= SpecLevelSkills[i] && (100 * max / total) >= SpecLevelPercent[i]; i++)
                {
                    level++;
                }
            }

            curr_level = level;
            curr_classe = spec;
        }

        private double totalWarrior(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.Anatomy.Value;
            skillsTotal += player.Skills.Fencing.Value;
            skillsTotal += player.Skills.Swords.Value;
            skillsTotal += player.Skills.Tactics.Value;
            skillsTotal += player.Skills.Healing.Value;
            skillsTotal += player.Skills.Macing.Value;
            skillsTotal += player.Skills.Parry.Value;
            skillsTotal += player.Skills.Wrestling.Value;
            return skillsTotal;
        }

        private double totalMage(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.Magery.Value;
            skillsTotal += player.Skills.MagicResist.Value;
            skillsTotal += player.Skills.Inscribe.Value;
            skillsTotal += player.Skills.EvalInt.Value;
            skillsTotal += player.Skills.Alchemy.Value;
            skillsTotal += player.Skills.Meditation.Value;
            skillsTotal += player.Skills.SpiritSpeak.Value;
            skillsTotal += player.Skills.ItemID.Value;
            return skillsTotal;
        }

        private double totalCrafter(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.Fletching.Value;
            skillsTotal += player.Skills.Mining.Value;
            skillsTotal += player.Skills.Lumberjacking.Value;
            skillsTotal += player.Skills.ArmsLore.Value;
            skillsTotal += player.Skills.Carpentry.Value;
            skillsTotal += player.Skills.Blacksmith.Value;
            skillsTotal += player.Skills.Tailoring.Value;
            skillsTotal += player.Skills.Tinkering.Value;
            return skillsTotal;
        }

        private double totalBard(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.Begging.Value;
            skillsTotal += player.Skills.Cartography.Value;
            skillsTotal += player.Skills.Discordance.Value;
            skillsTotal += player.Skills.Peacemaking.Value;
            skillsTotal += player.Skills.Provocation.Value;
            skillsTotal += player.Skills.Herding.Value;
            skillsTotal += player.Skills.Musicianship.Value;
            skillsTotal += player.Skills.TasteID.Value;
            return skillsTotal;
        }

        private double totalRanger(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.AnimalLore.Value;
            skillsTotal += player.Skills.AnimalTaming.Value;
            skillsTotal += player.Skills.Archery.Value;
            skillsTotal += player.Skills.Tracking.Value;
            skillsTotal += player.Skills.Fishing.Value;
            skillsTotal += player.Skills.Cooking.Value;
            skillsTotal += player.Skills.Veterinary.Value;
            skillsTotal += player.Skills.Camping.Value;
            return skillsTotal;
        }

        private double totalThief(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.DetectHidden.Value;
            skillsTotal += player.Skills.Hiding.Value;
            skillsTotal += player.Skills.Stealing.Value;
            skillsTotal += player.Skills.Stealth.Value;
            skillsTotal += player.Skills.Lockpicking.Value;
            skillsTotal += player.Skills.Snooping.Value;
            skillsTotal += player.Skills.Poisoning.Value;
            skillsTotal += player.Skills.RemoveTrap.Value; // not sure about this one
            return skillsTotal;
        }

    }
}
