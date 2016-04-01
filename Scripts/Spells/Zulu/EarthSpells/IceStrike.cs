using Server.Custom;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.EarthSpells
{
    public class IceStrike : EarthSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Ice Strike", "Geada Com Inverno",
            239,
            9021,
            Reagent.BatWing,
            Reagent.Bone,
            Reagent.BrimStone);

        public IceStrike(Mobile caster, Item scroll)
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
                return 20;
            }
        }

        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
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
            else if (this.CheckHSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);
                
                double damage;
                
                damage = getSpellDmg(this.Caster);

                if (this.CheckResisted(m)) // resist sure
                {
                    damage *= 0.75;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }
                
                SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0);
                m.FixedParticles(0x3789, 30, 30, 5032, EffectLayer.Waist);
                m.PlaySound(0x117);
                m.PlaySound(0x118);
                Console.WriteLine("Ice Strike DMG: " + damage);
            }
            
            this.FinishSequence();
        }

        public static void playEffect(Mobile m)
        {

        }

        private class InternalTarget : Target
        {
            private readonly IceStrike m_Owner;
            public InternalTarget(IceStrike owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    this.m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }

        public static double getSpellDmg(Mobile caster)
        {
            var modifier = caster.Skills.Magery.Value / 5;
            
            var amount = Utility.Random(12 * 3) + Utility.Random(1, 5) + modifier;
            Console.WriteLine("Ice strike amount1: " + amount);
          
            if(amount > 12 * (13 + 12)){
                amount = 12 * (13 + 12);
            }
            Console.WriteLine("Ice strike amount2: " + amount);
            amount += getEfficiency(caster, amount) / 4; // down from 5

            return amount;
        }
        /*function SPELL_MathDamage( mobile, circle, targRadius := 0 )

	if( targRadius )
                circle -= 3;
        endif
        
        if( circle < 1 )
                circle := 1;
        endif
        
        var modifier := CInt( AP_GetSkill( mobile, MAGERY ) / 5 ),
            amount := RandomDiceRoll(( circle * 3 )+"d5+"+modifier );
            
        if( amount > CInt( circle * ( 13 + circle )))
                amount := CInt( circle * ( 13 + circle ));
        endif

	amount := CInt( SPELL_GetEfficiency( mobile, amount ) / 5 );

	return amount;
endfunction*/

        public static double getEfficiency(Mobile caster, double value)
        {
            if (caster.SpecClasse == SpecClasse.Mage)
            {
                Console.WriteLine("DMG before spec: " + value);
                value *= caster.SpecBonus(SpecClasse.Mage);
                Console.WriteLine("DMG after spec: " + value);
            }
            else if (caster.SpecClasse == SpecClasse.Warrior)
            {
                value /= caster.SpecBonus(SpecClasse.Warrior);
            }

          //  return getSpellPenalty() / 5; // not yet implemented
            return value;
        }

        /*function SPELL_GetEfficiency( mobile, value )

	if( GetObjProperty( mobile, MAGE ))
		value *= ClasseBonus( mobile, MAGE );
	elseif( GetObjProperty( mobile, WARRIOR ))
		value /= ClasseBonus( mobile, WARRIOR );
	endif

	var penalty := CInt( SPELL_GetPenalty( mobile ));
	if( penalty >= 100 )
		return 0;
	endif

	return CInt( value * ( 100 - penalty ) / 100 );
endfunction*/

        public static double getSpellPenalty() // we dont have magic penalty yet
        {
            return 1; // not yet implemented
        }
        /*function SPELL_GetPenalty( mobile )

	var penalty := 0;
	if( mobile.IsA( POLCLASS_NPC ))
                return 0;
        endif

        var cfg_desc := ReadConfigFile( ":*:itemdesc" );
        
	foreach item in ListEquippedItems( mobile )
                SleepMS(5);
                
                var cfg_penalty := cfg_desc[item.objtype].MagicPenalty;
		if( cfg_penalty )
			penalty += cfg_penalty;
			continue;
		endif
                
                var prop_penalty := GetObjProperty( item, "MagicPenalty" );
		if( prop_penalty )
			penalty += prop_penalty;
			continue;
		endif
	endforeach

	if( penalty > 100 )
		penalty := 100;
	endif

	return CInt( penalty );
endfunction
*/
    }
}