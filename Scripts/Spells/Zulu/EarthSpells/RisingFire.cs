using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    public class RisingFire : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Rising Fire", "Batida Do Fogo",
            239,
            9021,
            Reagent.BatWing,
            Reagent.BrimStone,
            Reagent.VialofBlood);

        public RisingFire(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.0);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 100; // dunno about this, gotta check
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 15;
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

        public void Target(IPoint3D p)
        {
            if (!this.Caster.CanSee(p))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (SpellHelper.CheckTown(p, this.Caster) && this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, p);

                if (p is Item)
                    p = ((Item)p).GetWorldLocation();

                List<Mobile> targets = new List<Mobile>();

                Map map = this.Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 5); // range up from 2

                    foreach (Mobile m in eable)
                    {
                        if (m == this.Caster)
                            continue;

                        if (SpellHelper.ValidIndirectTarget(this.Caster, m) && this.Caster.CanBeHarmful(m, false))
                        {
                            if (!this.Caster.InLOS(m))
                                continue;

                            targets.Add(m);

                            
                        }
                    }

                    eable.Free();
                }

                double damage;

               
                    damage = Utility.Random(27, 22);

                if (targets.Count > 0)
                {
                    if (Core.AOS && targets.Count > 2)
                        damage = (damage * 2) / targets.Count;
                    else if (!Core.AOS)
                        damage /= targets.Count;

                    double toDeal;
                    for (int i = 0; i < targets.Count; ++i)
                    {
                        toDeal = damage;
                        Mobile m = targets[i];

                        if (this.CheckResisted(m))
                        {
                            toDeal *= 0.6;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }
                        toDeal *= this.GetDamageScalar(m);
                        this.Caster.DoHarmful(m);
                        SpellHelper.Damage(this, m, toDeal, 0, 0, 0, 0, 100);


                        m.FixedParticles(0x3709, 10, 20, 5032, EffectLayer.Waist); 
                       
                    }
                }
                else
                {
                    this.Caster.PlaySound(0x29);
                }
            }

            this.FinishSequence();
        }

        public static void playEffect(Mobile m)
        {

        }

        private class InternalTarget : Target
        {
            private readonly RisingFire m_Owner;
            public InternalTarget(RisingFire owner)
                : base(Core.ML ? 10 : 12, true, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D p = o as IPoint3D;

                if (p != null)
                    this.m_Owner.Target(p);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}