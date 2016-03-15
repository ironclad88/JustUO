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
    public class PlagueSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Plague", "Fluctus Puter Se Aresceret",
            203,
            9051,
            Reagent.VolcanicAsh,
            Reagent.BatWing,
            Reagent.DaemonBone,
            Reagent.DragonBlood,
            Reagent.BloodSpawn,
            Reagent.Pumice,
            Reagent.SerpentsScales);



        public PlagueSpell(Mobile caster, Item scroll)
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
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 6); // range up from 2

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
                
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; ++i)
                    {
                       
                        Mobile m = targets[i];
                        int level = 4;
                        if (this.CheckResisted(m))
                        {
                            level = 3;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        m.ApplyPoison(this.Caster, Poison.GetPoison(level)); // lvl 4 psn, lvl 3 if resisted
                        m.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
                        m.PlaySound(0x205);

                        this.HarmfulSpell(m);

                    }
                }
                else
                {
                    this.Caster.PlaySound(0x29);
                }
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly PlagueSpell m_Owner;

            public InternalTarget(PlagueSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
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
