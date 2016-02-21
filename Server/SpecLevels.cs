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
           // double s_warrior, s_bard, s_crafter, s_mage, s_ranger, s_thief, s_cleric;
            double total = player.SkillsTotal, max = 0;
            int level = 0;
            var classDict = new Dictionary<SpecClasse, double>(6);

            try { 
            classDict.Add(SpecClasse.Warrior, totalWarrior(player));
            classDict.Add(SpecClasse.Bard, totalBard(player));
            classDict.Add(SpecClasse.Crafter, totalCrafter(player));
            classDict.Add(SpecClasse.Mage, totalMage(player));
            classDict.Add(SpecClasse.Ranger, totalRanger(player));
            classDict.Add(SpecClasse.Thief, totalThief(player));
            classDict.Add(SpecClasse.Cleric, totalCleric(player));
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // this wont happen, sissy catch
            }
         
            SpecClasse spec = SpecClasse.None;
          
            foreach (var val in classDict)
            {
                if (val.Value > max)
                {
                    spec = val.Key;
                    max = val.Value;
                }
            }

            // spec == current classe, now calculate level
            if(max < SpecLevelSkills[0])
            {
                spec = SpecClasse.None;
            }
            else
            {
                int i;
                for (i = 0; i < SpecLevelSkills.Length && max >= SpecLevelSkills[i] && (1000 * max / total) >= SpecLevelPercent[i]; i++)
                {
                    level++;
                }
            }
            
            curr_level = level;
            if (level > 0)
            {
                curr_classe = spec;
            }
            else
            {
                curr_classe = SpecClasse.None;
            }
            Console.WriteLine("level " + curr_level + " classe " + curr_classe);
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

        private double totalCleric(Mobile player)
        {
            double skillsTotal = 0;
            skillsTotal += player.Skills.Macing.Value;
            skillsTotal += player.Skills.Anatomy.Value;
            skillsTotal += player.Skills.EvalInt.Value;
            skillsTotal += player.Skills.Magery.Value;
            skillsTotal += player.Skills.SpiritSpeak.Value;
            skillsTotal += player.Skills.Parry.Value;
            skillsTotal += player.Skills.Meditation.Value;
            skillsTotal += player.Skills.Tactics.Value; 
            return skillsTotal;
        }
        
    }
}
