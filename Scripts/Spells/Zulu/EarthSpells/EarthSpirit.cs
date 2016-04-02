using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    public class EarthSpirit : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Earth Spirit", "Chame O Terra Elemental",
            16,
            false,
            Reagent.DragonBlood,
            Reagent.FertileDirt,
            Reagent.VolcanicAsh);

        public EarthSpirit(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if ((this.Caster.Followers + 3) > this.Caster.FollowersMax)
            {
                this.Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(2);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 120; // dunno about this, gotta check
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 20;
            }
        }

        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                BaseCreature creature = (BaseCreature)Activator.CreateInstance(typeof(EarthlordElemental)); // make earth elemental lord

                TimeSpan duration;

                duration = TimeSpan.FromSeconds(5.0 * this.Caster.Skills[SkillName.Magery].Value * this.Caster.SpecBonus(SpecClasse.Mage));

                SpellHelper.Summon(creature, this.Caster, 0x215, duration, false, false);
            }
            this.FinishSequence();
        }

        public override TimeSpan GetCastDelay()
        {

            return base.GetCastDelay() + TimeSpan.FromSeconds(4.0);
        }
    }
}