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
    public class AbyssalflameSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Abyssal Flame", "Orinundus Barathrum Erado Hostes Hostium",
            203,
            9051,
            Reagent.BrimStone,
            Reagent.Obsidian,
            Reagent.VolcanicAsh,
            Reagent.DaemonBone,
            Reagent.DragonBlood);



        public AbyssalflameSpell(Mobile caster, Item scroll)
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

                double damage;


                damage = Utility.Random(35, 22);

                if (targets.Count > 0)
                {
                    damage = (damage * 2) / targets.Count;


                    double toDeal;
                    for (int i = 0; i < targets.Count; ++i)
                    {
                        toDeal = damage;
                        Mobile m = targets[i];

                        if (this.CheckResisted(m))
                        {
                            toDeal *= 0.75;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }
                        toDeal *= this.GetDamageScalar(m);
                        this.Caster.DoHarmful(m);
                        SpellHelper.Damage(this, m, toDeal, 0, 80, 0, 0, 0, 0, 30, 0);
                        
                        m.MovingParticles(m, 0x36D4, 5, 1, false, true, 9502, 4019, 0x160); // effect not working atm (fireball exploding effect)
                        m.PlaySound(Core.AOS ? 0x15E : 0x44B);


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
            private readonly AbyssalflameSpell m_Owner;

            public InternalTarget(AbyssalflameSpell owner)
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
