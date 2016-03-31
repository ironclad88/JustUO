using Server.Spells.Necromancy;
using System;

namespace Server.Spells.Zulu.NecroSpells
{
    public class LicheSpell : TransformationSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Lich", "Umbrae Tenebrae Miserere Animi Non Digna Ferentis",
            203,
            9031,
            Reagent.DaemonBone,
            Reagent.BrimStone,
            Reagent.DragonBlood,
            Reagent.BloodSpawn,
            Reagent.ExecutionersCap,
            Reagent.BlackMoor,
            Reagent.VialofBlood,
            Reagent.VolcanicAsh);

        public LicheSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(2.0);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 70.0;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 60;
            }
        }
        public override int Body
        {
            get
            {
                return 749;
            }
        }
        public override int FireResistOffset
        {
            get
            {
                return -50;
            }
        }
        public override int ColdResistOffset
        {
            get
            {
                return +50;
            }
        }
        public override int PoisResistOffset
        {
            get
            {
                return +50;
            }
        }
        public override double TickRate
        {
            get
            {
                return 2.5;
            }
        }
        public override void DoEffect(Mobile m)
        {
            m.PlaySound(0x19C);
            m.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.RightFoot);
        }

        public override void OnTick(Mobile m)
        {
            --m.Hits;
        }
    }
}