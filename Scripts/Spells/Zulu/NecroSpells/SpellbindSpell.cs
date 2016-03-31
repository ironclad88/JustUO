using Server.Mobiles;
using Server.Spells.Seventh;
using Server.Targeting;
using System;

namespace Server.Spells.Zulu.NecroSpells
{
    public class SpellbindSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Spellbind", "Nutu Magistri Se Compellere",
            203,
            9051,
            Reagent.EyeofNewt,
            Reagent.VialofBlood,
            Reagent.FertileDirt,
            Reagent.PigIron);



        public SpellbindSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }



        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(BaseCreature m)
        {
            if (m is BaseCreature) // gotta test alot of cases, should not be able to tame players, vendors and so on
            {
                    m.Owners.Add(this.Caster);
                    m.SetControlMaster(this.Caster);
                    m.BardPacified = true;
                    m.IsBonded = false;
            }
            else
            {
                this.Caster.SendMessage("You can´t control that!");
            }
        }

        private class InternalTarget : Target
        {
            private readonly SpellbindSpell m_Owner;

            public InternalTarget(SpellbindSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature)
                {
                    this.m_Owner.Target((BaseCreature)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
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

    }
}
