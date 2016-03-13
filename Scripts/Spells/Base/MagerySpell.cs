using System;
using Server.Items;

namespace Server.Spells
{
    public abstract class MagerySpell : Spell
    {
        private static readonly int[] m_ManaTable = new int[] { 4, 6, 9, 11, 14, 20, 40, 50 };
        private const double ChanceOffset = 20.0, ChanceLength = 100.0 / 7.0;
        public MagerySpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }

        public abstract SpellCircle Circle { get; }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds((3 + (int)this.Circle) * this.CastDelaySecondsPerTick);
            }
        }
        public override bool ConsumeReagents()
        {
            if (base.ConsumeReagents())
                return true;

            if (ArcaneGem.ConsumeCharges(this.Caster, (Core.SE ? 1 : 1 + (int)this.Circle)))
                return true;

            return false;
        }

        public override void GetCastSkills(out double min, out double max)
        {
            // int circle = (int)this.Circle;

            //  if (this.Scroll != null)
            //      circle -= 2;

            double avg = ChanceLength * (int)this.Circle;

            min = avg - ChanceOffset;
            max = avg + ChanceOffset;
        }

        public override int GetMana()
        {
            if (this.Scroll is BaseWand)
                return 0;



            return m_ManaTable[(int)this.Circle];
        }

        public override double GetResistSkill(Mobile m)
        {
            int maxSkill = (1 + (int)this.Circle) * 10;
            maxSkill += (1 + ((int)this.Circle / 6)) * 25;

            if (m.Skills[SkillName.MagicResist].Value < maxSkill)
                m.CheckSkill(SkillName.MagicResist, 0.0, m.Skills[SkillName.MagicResist].Cap);

            return m.Skills[SkillName.MagicResist].Value;
        }

        public virtual bool CheckResisted(Mobile target)
        {
            double n = this.GetResistPercent(target);

            n /= 100.0;

            var permMagicReturn = checkPermaMagic(target, this.Circle);
            if (permMagicReturn)
                return true;
            
            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)this.Circle) * 10;
            maxSkill += (1 + ((int)this.Circle / 6)) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill)
                target.CheckSkill(SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap);

            return (n >= Utility.RandomDouble());
        }

        public virtual bool checkPermaMagic(Mobile target, SpellCircle circle) // JustZH fix later
        {
            if (target.PermaMagicResistance != 0)
            {
                switch (target.PermaMagicResistance)
                {
                    case 1:
                        if ((int)circle <= 1) { target.FixedEffect(0x37B9, 10, 5); return true; }
                        break;
                    case 2:
                        if ((int)circle <= 2) { target.FixedEffect(0x37B9, 10, 5);  return true; }
                        break;
                    case 3:
                        if ((int)circle <= 3) { target.FixedEffect(0x37B9, 10, 5);  return true; }
                        break;
                    case 4:
                        if ((int)circle <= 4) { target.FixedEffect(0x37B9, 10, 5);  return true; }
                        break;
                    case 5:
                        if ((int)circle <= 5) { target.FixedEffect(0x37B9, 10, 5);  return true; }
                        break;
                    case 6:
                        if ((int)circle <= 6) { target.FixedEffect(0x37B9, 10, 5);  return true; }
                        break;
                }
            }
            return false;
        }

        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
            double secondPercent = target.Skills[SkillName.MagicResist].Value - (((this.Caster.Skills[this.CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
        }

        public virtual double GetResistPercent(Mobile target)
        {
            return this.GetResistPercentForCircle(target, this.Circle);
        }

        public override TimeSpan GetCastDelay()
        {
            if (!Core.ML && this.Scroll is BaseWand)
                return TimeSpan.Zero;

            if (!Core.AOS)
                return TimeSpan.FromSeconds(0.5 + (0.25 * (int)this.Circle));

            return base.GetCastDelay();
        }
    }
}