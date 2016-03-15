using Server.Spells.Seventh;
using System;

namespace Server.Spells.Zulu.NecroSpells
{
    public class DecayingraySpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Decaying Ray", "Umbra Aufero Vita",
            203,
            9051,
            Reagent.VolcanicAsh,
            Reagent.VialofBlood,
            Reagent.DaemonBone);



        public DecayingraySpell(Mobile caster, Item scroll)
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
