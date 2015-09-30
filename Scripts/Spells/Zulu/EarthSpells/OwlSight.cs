using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    class OwlSight : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Owl Sight", "Vista Da Noite",
            236,
            9031,
            Reagent.EyeofNewt);

        public OwlSight(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 85; // dunno about this, gotta check
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 5;
            }
        }

        public override void OnCast()
        {
            setCords(Caster.Y, Caster.X);
            this.Caster.Target = new OwlsightTarget(this);
        }

        private class OwlsightTarget : Target
        {
            private readonly Spell m_Spell;
            public OwlsightTarget(Spell spell)
                : base(20, false, TargetFlags.Beneficial) // changed range to 20 (12)
            {
                this.m_Spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile && this.m_Spell.CheckBSequence((Mobile)targeted))
                {
                    Mobile targ = (Mobile)targeted;

                    SpellHelper.Turn(this.m_Spell.Caster, targ);

                    if (targ.BeginAction(typeof(LightCycle)))
                    {
                        new LightCycle.OwlsightTimer(targ, 40).Start();
                       

                        targ.LightLevel = 0; // i think 0 is good, gotta test

                        targ.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                        targ.PlaySound(0x1E3);

                       // BuffInfo.AddBuff(targ, new BuffInfo(BuffIcon.NightSight, 1075643));	//Night Sight/You ignore lighting effects
                    }
                    else
                    {
                        from.SendMessage("{0} already have owl sight.", from == targ ? "You" : "They");
                    }
                }

                this.m_Spell.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Spell.FinishSequence();
            }
        }
    }
}
