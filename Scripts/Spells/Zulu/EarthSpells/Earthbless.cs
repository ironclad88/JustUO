using Server.Custom;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    class EarthBless : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Earth bless", "Foria Da Terra",
            236,
            9031,
            Reagent.PigIron,
            Reagent.Obsidian,
            Reagent.VolcanicAsh);

        public EarthBless(Mobile caster, Item scroll)
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
                return 85; // dunno about this, gotta check
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 5;
            }
        }

        public override void OnCast()
        {
            setCords(Caster.Y, Caster.X);
            Mobile m = this.Caster;
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckBSequence(m))
            {
                //   if(this.Caster) // check if caster is already buffed
                SpellHelper.Turn(this.Caster, m);

                SpellHelper.DisableSkillCheck = true;
                SpellHelper.AddStatBonus(this.Caster, m, StatType.Str);
                SpellHelper.AddStatBonus(this.Caster, m, StatType.Dex);
                SpellHelper.AddStatBonus(this.Caster, m, StatType.Int);
                SpellHelper.DisableSkillCheck = false;

                m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                m.PlaySound(0x1EA);
                
                Random rnd = new Random();
                int percentageRandomness = rnd.Next(15); 

                int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, m, false) * percentageRandomness * 200 * this.Caster.SpecBonus(SpecClasse.Mage));
                TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                TimeSpan durSpecBonus = TimeSpan.FromMinutes(5 * this.Caster.SpecBonus(SpecClasse.Mage));
                length.Add(durSpecBonus);

                string args = String.Format("{0}\t{1}\t{2}", percentage, percentage, percentage);

                BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Bless, 1075847, 1075848, length, m, args.ToString()));
            }

            this.FinishSequence();
        }

    }
}
