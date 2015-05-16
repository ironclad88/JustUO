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
    public class LicheSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "WyvernStrike", "Umbrae Tenebrae Miserere Animi Non Digna Ferentis",
            203,
            9051,
            Reagent.DaemonBone,
            Reagent.BrimStone,
            Reagent.DragonBlood,
            Reagent.BloodSpawn,
            Reagent.ExecutionersCap,
            Reagent.BlackMoor,
            Reagent.VialofBlood,
            Reagent.VolcanicAsh);

        

        public LicheSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
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
