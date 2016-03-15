using Server.Spells.Seventh;
using System;

namespace Server.Spells.Zulu.NecroSpells
{
    public class WraithformSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Wraithform", "Manes Sollicti Mihi Infundite",
            203,
            9051,
            Reagent.DaemonBone,
            Reagent.BrimStone,
            Reagent.BloodSpawn);



        public WraithformSpell(Mobile caster, Item scroll)
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
