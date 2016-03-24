using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    public class ShiftingEarth : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Shifting Earth ", "Esmagamento Con Pedra",
            239,
            9021,
            Reagent.PigIron,
            Reagent.Bone,
            Reagent.WyrmsHeart);

        public ShiftingEarth(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
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
                return 90; // dunno about this, gotta check
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 10;
            }
        }

        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckHSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                SpellHelper.CheckReflect(10, this.Caster, ref m);

                double damage;

                //if (Core.AOS)
                //{
                //    damage = this.GetNewAosDamage(23, 1, 4, m);
                //}
                //else
                //{
                    damage = Utility.Random(12, 9);

                    if (this.CheckResisted(m))
                    {
                        damage *= 0.75;

                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    }

                    damage *= this.GetDamageScalar(m);
                //}

                m.FixedEffect(0x020e, 10, 40);

                SpellHelper.Damage(this, m, damage, 0, 0, 0, 0, 0, 100, 0, 0);
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly ShiftingEarth m_Owner;
            public InternalTarget(ShiftingEarth owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    this.m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}