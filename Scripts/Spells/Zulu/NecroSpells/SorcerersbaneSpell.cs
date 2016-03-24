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
    public class SorceresbaneSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Sorcerer's Bane", "Fluctus Perturbo Magus Navitas",
            203,
            9051,
            Reagent.VolcanicAsh,
            Reagent.WyrmsHeart,
            Reagent.DaemonBone,
            Reagent.DragonBlood,
            Reagent.Deadwood,
            Reagent.Pumice);



        public SorceresbaneSpell(Mobile caster, Item scroll)
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
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckHSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                double damage = Utility.Random(70, 90); // balance this plix

                double resistChance = GetResistSkill(m); // balance this plix
                double finalDmg = damage - resistChance * 0.2; // balance this plix
                Console.WriteLine("Kill spell DMG: " + finalDmg); // balance this plix

                damage *= this.GetDamageScalar(m); // balance this plix
                SpellHelper.Damage(this, m, finalDmg, 0, 0, 0, 0, 0, 0, 100, 0);

                m.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
                m.PlaySound(0x205);

                this.HarmfulSpell(m);
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly SorceresbaneSpell m_Owner;

            public InternalTarget(SorceresbaneSpell owner)
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
