using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.NecroSpells
{
    public abstract class NecroSpell : Spell
    {
        public NecroSpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }

        public abstract double RequiredSkill { get; }
        public abstract int RequiredMana { get; }

        public override SkillName CastSkill
        {
            get
            {
                return SkillName.Magery;
            }
        }
        public override SkillName DamageSkill
        {
            get
            {
                return SkillName.Magery;
                // return SkillName.EvalInt; // not sure about this one
            }
        }
        //public override int CastDelayBase{ get{ return base.CastDelayBase; } } // Reference, 3
        public override bool ClearHandsOnCast
        {
            get
            {
                return false;
            }
        }
        public override double CastDelayFastScalar
        {
            get
            {
                return (Core.SE ? base.CastDelayFastScalar : 0);
            }
        }// Necromancer spells are not affected by fast cast items, though they are by fast cast recovery
        public override int ComputeKarmaAward()
        {
            //TODO: Verify this formula being that Necro spells don't HAVE a circle.
            //int karma = -(70 + (10 * (int)Circle));
            int karma = -(40 + (int)(10 * (this.CastDelayBase.TotalSeconds / this.CastDelaySecondsPerTick)));

            if (Core.ML) // Pub 36: "Added a new property called Increased Karma Loss which grants higher karma loss for casting necromancy spells."
                karma += AOS.Scale(karma, AosAttributes.GetValue(this.Caster, AosAttribute.IncreasedKarmaLoss));

            return karma;
        }

        public override void GetCastSkills(out double min, out double max)
        {
            min = this.RequiredSkill;
            max = this.Scroll != null ? min : this.RequiredSkill + 40.0;
        }


        public override int GetMana()
        {
            return this.RequiredMana;
        }
    }
}