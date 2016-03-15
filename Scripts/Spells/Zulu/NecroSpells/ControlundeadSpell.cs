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
    public class ControlundeadSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Darkness", "In Caligne Abditus",
            203,
            9051,
            Reagent.Pumice,
            Reagent.PigIron);



        public ControlundeadSpell(Mobile caster, Item scroll)
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
        
        // quick prototype:
        // just a small quick test. its working but you shouldnt be able to "control" everything, like the hardest liches. 
        // dunno how calc the skill needed to control, must add more checks, the control should be on a timer and the creature should only be able to be controlled once.
        public void Target(BaseCreature m)
        {
            if (m is BaseCreature)
            {
                if (m.OppositionGroup == OppositionGroup.FeyAndUndead) 
                {
                    m.Owners.Add(this.Caster);
                    m.SetControlMaster(this.Caster);
                    m.BardPacified = true;
                    m.IsBonded = false;
                }
                else
                {
                    this.Caster.SendMessage("You can´t control anything but Undead creatures");
                }
            }
            else
            {
                this.Caster.SendMessage("You can´t control that!");
            }
        }

        private class InternalTarget : Target
        {
            private readonly ControlundeadSpell m_Owner;

            public InternalTarget(ControlundeadSpell owner)
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
    }
}
