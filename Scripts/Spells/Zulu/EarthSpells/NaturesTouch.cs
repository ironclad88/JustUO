using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    class NaturesTouch : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Natures Touch", "Guerissez Par Terre ",
            236,
            9031,
            Reagent.Pumice,
            Reagent.VialofBlood,
            Reagent.Obsidian);

        public NaturesTouch(Mobile caster, Item scroll)
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

        public override bool CheckCast()
        {
            if (Engines.ConPVP.DuelContext.CheckSuddenDeath(this.Caster))
            {
                this.Caster.SendMessage(0x22, "You cannot cast this spell when in sudden death.");
                return false;
            }

            return base.CheckCast();
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
            else if (m is BaseCreature && ((BaseCreature)m).IsAnimatedDead)
            {
                this.Caster.SendLocalizedMessage(1061654); // You cannot heal that which is not alive.
            }
            else if (m.IsDeadBondedPet)
            {
                this.Caster.SendLocalizedMessage(1060177); // You cannot heal a creature that is already dead!
            }
            else if (this.CheckBSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);
                
                int toHeal = (int)(this.Caster.Skills[SkillName.Magery].Value * 0.4 * this.Caster.SpecBonus(SpecClasse.Mage));
                toHeal += Utility.Random(20, 40);
                
                SpellHelper.Heal(toHeal, m, this.Caster);

                m.FixedParticles(0x375A, 9, 32, 5030, EffectLayer.Waist);
                m.PlaySound(0x203);
            }

            this.FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly NaturesTouch m_Owner;
            public InternalTarget(NaturesTouch owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Beneficial)
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
