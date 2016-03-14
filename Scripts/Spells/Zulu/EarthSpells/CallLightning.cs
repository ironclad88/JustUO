using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    public class CallLightning : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Call Lightning", "Batida Do Deus",
            239,
            9021,
            Reagent.PigIron,
            Reagent.Bone,
            Reagent.WyrmsHeart);

        public CallLightning(Mobile caster, Item scroll)
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
            setCords(Caster.Y, Caster.X);
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

               // SpellHelper.CheckReflect(10, this.Caster, ref m); // can´t reflect earth spells

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
                int count = 0;
                Timer timer = Timer.DelayCall(TimeSpan.FromMilliseconds(50), new TimerCallback(delegate () // this is awesome!
                   {
                       do
                       {
                           m.BoltEffect(0x4f4);
                           count++;

                       } while (count < 5);
                   }));
                SpellHelper.Damage(this, m, damage, 0, 0, 0, 0, 100);
            }

            this.FinishSequence();
        }

        public static void playEffect(Mobile m)
        {

        }

        private class InternalTarget : Target
        {
            private readonly CallLightning m_Owner;
            public InternalTarget(CallLightning owner)
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