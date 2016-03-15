using Server.Spells.Seventh;
using System;

namespace Server.Spells.Zulu.NecroSpells
{
    public class AnimatedeadSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Animate dead", "Corpus Sine Nomine Expergefaceret",
            203,
            9051,
            Reagent.Bone,
            Reagent.FertileDirt,
            Reagent.VialofBlood,
            Reagent.Obsidian);



        public AnimatedeadSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }



        public override void OnCast()
        {
            Caster.SendMessage("Not yet implemented");
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.5);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 130;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 40;
            }
        }
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }

    }
}
