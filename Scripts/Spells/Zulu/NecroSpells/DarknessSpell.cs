using Server.Items;
using Server.Mobiles;
using Server.Spells.Necromancy;
using Server.Targeting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.NecroSpells
{
    public class DarknessSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Darkness", "In Caligne Abditus",
            203,
            9051,
            Reagent.Pumice,
            Reagent.PigIron);



        public DarknessSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            setCords(Caster.Y, Caster.X);
            this.Caster.Target = new InternalTarget(this);
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(2.4);
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
                return 50;
            }
        }
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }

        public void Target(Mobile m)
        {
            if (m is Mobile)
            {
                Mobile targ = m;
                
                if (targ.BeginAction(typeof(LightCycle)))
                {
                    new LightCycle.NightSightTimer(targ).Start();
                    
                    targ.LightLevel = 12;
                    
                    targ.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                    targ.PlaySound(0x1E3);
                }
            }
        }

        private class InternalTarget : Target
        {
            private readonly DarknessSpell m_Owner;

            public InternalTarget(DarknessSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    this.m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
