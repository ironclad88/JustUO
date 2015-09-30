using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.ClericSpells
{
    class TrialByFire : HolySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Trial by Fire", "Temptatio Exsuscito",
            239,
            9021,
            Reagent.BlackPearl,
            Reagent.Bloodmoss,
            Reagent.Garlic);

        public TrialByFire(Mobile caster, Item scroll)
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
        

        public static void Initialize()
		{
			PlayerEvent.HitByWeapon += new PlayerEvent.OnWeaponHit( InternalCallback );
		}


		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

            if (!Caster.CanBeginAction(typeof(TrialByFire)))
			{
				Caster.SendLocalizedMessage( 501775 ); // This spell is already in effect
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Caster.SendMessage( "Your body is covered by holy flames." );
                Caster.BeginAction(typeof(TrialByFire));

				Caster.FixedParticles( 0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot );
				Caster.PlaySound( 0x208 );

				DateTime Expire = DateTime.Now + TimeSpan.FromMinutes( Caster.Skills[SkillName.Magery].Value / 10.0 );
				new InternalTimer( Caster, Expire ).Start();

			}
			FinishSequence();
		}

		private static void InternalCallback( Mobile attacker, Mobile defender, int damage, WeaponAbility a )
		{
            if (!defender.CanBeginAction(typeof(TrialByFire)) && Utility.RandomBool())
			{
				defender.DoHarmful( attacker );

				double scale = 1.0;

				scale += defender.Skills[SkillName.Inscribe].Value * 0.001;

				if ( defender.Player )
				{
					scale += defender.Int * 0.001;
					scale += AosAttributes.GetValue( defender, AosAttribute.SpellDamage ) * 0.01;
				}

                int baseDamage = 9999;// + (int)(defender.Skills[SkillName.EvalInt].Value / 5.0);

				double firedmg = Utility.RandomMinMax( baseDamage, baseDamage + 3 );

				firedmg *= scale;

                SpellHelper.Damage(TimeSpan.Zero, attacker, defender, baseDamage, 0, 100, 0, 0, 0);

				attacker.FixedParticles( 0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot );
				attacker.PlaySound( 0x208 );
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile Source;
			private DateTime Expire;

			public InternalTimer( Mobile from, DateTime end ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				Source = from;
				Expire = end;
			}

			protected override void OnTick()
			{
				if ( DateTime.Now >= Expire || !Source.CheckAlive() )
				{
                    Source.EndAction(typeof(TrialByFire));
					Stop();
					Source.SendMessage( "The holy fire around you fades." );
				}
			}
		}
    }

    public class PlayerEvent
    {
        public delegate void OnWeaponHit(Mobile attacker, Mobile defender, int damage, WeaponAbility a);
        public static event OnWeaponHit HitByWeapon;

        public static void InvokeHitByWeapon(Mobile attacker, Mobile defender, int damage, WeaponAbility a)
        {
            if (HitByWeapon != null)
                HitByWeapon(attacker, defender, damage, a);
        }
    }
}
