using Server.Spells.Necromancy;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.NecroSpells
{
    public class WyvernStrikeSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "WyvernStrike", "Umbrae Tenebrae Venarent",
            203,
            9051,
            Reagent.DragonBlood,
            Reagent.SerpentsScales,
            Reagent.BlackMoor,
            Reagent.BloodSpawn,
            Reagent.VolcanicAsh);


        public WyvernStrikeSpell(Mobile caster, Item scroll)
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
                return 100;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 35;
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

<<<<<<< HEAD:Scripts/Spells/Zulu/NecroSpells/WyvernStrike.cs
                // SpellHelper.CheckReflect((int)this.Circle, this.Caster, ref m);
=======
                //  SpellHelper.CheckReflect((int)this.Circle, this.Caster, ref m); // had to remove this if inherit from NecromancerSpell
>>>>>>> 9b1b75bde5e69405f791e29ced500105ddcd2620:Scripts/Spells/Zulu/NecroSpells/WyvernStrikeSpell.cs

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

              //  if (this.CheckResisted(m))
              //  {
              //      m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
              //  }
               // else
              //  {
                    double damage = Utility.Random(65, 80);

                    Console.WriteLine("Wyvern Strike DMG: " + damage);
                    Console.WriteLine("Applying PSN: Lethal");
                    m.ApplyPoison(this.Caster, Poison.GetPoison("Lethal"));
                    /*if (this.CheckResisted(m)) // had to remove this if inherit from NecromancerSpell
                    {
                        damage *= 0.75;

                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    }*/
                    damage *= this.GetDamageScalar(m);
                    SpellHelper.Damage(this, m, damage, 0, 20, 0, 0, 0, 0, 80, 0);
                    
             //   }

                m.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
                m.PlaySound(0x205);

                this.HarmfulSpell(m);
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly WyvernStrikeSpell m_Owner;

            public InternalTarget(WyvernStrikeSpell owner)
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
