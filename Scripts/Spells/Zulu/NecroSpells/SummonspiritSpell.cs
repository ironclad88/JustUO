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
    public class SummonspiritSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Plague", "Fluctus Puter Se Aresceret",
            203,
            9051,
            Reagent.VolcanicAsh,
            Reagent.BatWing,
            Reagent.DaemonBone,
            Reagent.DragonBlood,
            Reagent.BloodSpawn,
            Reagent.Pumice,
            Reagent.SerpentsScales);

        /* POL SCRIPT MOBS
        var creature := RandomDiceStr("1d8") + bonus;
		case( creature )
			1:
			2:
			3:
			4:	npctemplate := "shade";		break;
			5:
			6:
			7:	npctemplate := "liche";		break;
			8:
			9:	npctemplate := "lichelord";	break;
			10:	npctemplate := "bloodliche";	break;
		endcase
        */


        private static readonly Type[] m_Types = new Type[] // fix this list
        {
            typeof(Skeleton),
            typeof(Lich),
            typeof(Shade),
            typeof(LichLord)
         //   typeof(Bloodlich)
        };

        public SummonspiritSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                int count = Utility.Random(2, 2); // from pol

                if(this.Caster.SpecClasse == SpecClasse.Mage)
                {
                    count += 2;
                }

                Timer timer = Timer.DelayCall(TimeSpan.FromMilliseconds(50), new TimerCallback(delegate () // this is awesome!
                {
                    do
                    {
                        BaseCreature creature = (BaseCreature)Activator.CreateInstance(m_Types[Utility.Random(m_Types.Length)]);
                        TimeSpan duration;

                        duration = TimeSpan.FromSeconds(4.0 * this.Caster.Skills[SkillName.Magery].Value * this.Caster.SpecBonus(SpecClasse.Mage));

                        SpellHelper.Summon(creature, this.Caster, 0x215, duration, false, false);
                        count++;

                    } while (count < 3);
                }));
                Caster.PlaySound(0x22B);
            }

            this.FinishSequence();
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
    }
}
