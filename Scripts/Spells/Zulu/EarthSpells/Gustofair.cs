using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    public class Gustofair : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Gust of Air", "Gust Do Ar",
            239,
            9021,
            Reagent.FertileDirt,
            Reagent.BrimStone,
            Reagent.VialofBlood);

        public Gustofair(Mobile caster, Item scroll)
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
                return 105; // dunno about this, gotta check
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

                double damage;

                    damage = Utility.Random(12, 9);

                    if (this.CheckResisted(m))
                    {
                        damage *= 0.75;

                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    }

                    damage *= this.GetDamageScalar(m);
                var test = m.GetRandomPoint3D(5);

                for (int i = 0; i <= 4; i++ ) // check this one out later, havent tried it, its late, its probably ineffective on the server
                {
                    if (Map.Felucca.CanSpawnMobile(test.X, test.Y, test.Z) == true) // if this dont work, dont move the mob
                    {
                        m.MoveToWorld(test, Map.Felucca);
                        break;
                    }
                    test = m.GetRandomPoint3D(5); // get new cord
                }

                m.PlaySound(0x108);
                m.PlaySound(0x109);
                
                m.FixedEffect(0x37CC, 30, 30); // gotta fix
                
                SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0, 0, 0, 0);
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly Gustofair m_Owner;
            public InternalTarget(Gustofair owner)
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