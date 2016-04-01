using Server.Spells.Seventh;
using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.Spells.Zulu.NecroSpells
{
    public class WraithsbreathSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Wraith´s Breath", "Manes Sollicti Mi Compellere",
            203,
            9051,
            Reagent.Obsidian,
            Reagent.Pumice,
            Reagent.Bone,
            Reagent.BlackMoor);



        public WraithsbreathSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }



        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
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

                double damage;


                damage = Utility.Random(35, 22);

                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = targets[i];
                        double duration;

                        duration = 7.0 + (this.Caster.Skills[SkillName.Magery].Value * 0.2); // this needs balancing

                        if(m.NecroResistance < 3) { // if necro resist is lesser than 3, you get paralyzed, else you resist dat paralyze!
                        m.Paralyze(TimeSpan.FromSeconds(duration));

                            m.PlaySound(0x1FA);
                            m.FixedEffect(0x374A, 6, 1);

                            this.HarmfulSpell(m);
                        }
                        else
                        {

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                       
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
            private readonly WraithsbreathSpell m_Owner;

            public InternalTarget(WraithsbreathSpell owner)
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
