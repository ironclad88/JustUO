using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.ACC.CSS.Systems.Cleric
{
	public abstract class ClericSpell : CSpell
	{
        public abstract SpellCircle Circle { get; }
        
		public ClericSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

        public override SkillName CastSkill { get { return SkillName.SpiritSpeak; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(10 * CastDelaySecondsPerTick); } }
        public override bool ClearHandsOnCast { get { return false; } }

        public override int GetMana()
        {
            return RequiredMana;
        }
        
        public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( Caster.Skills[CastSkill].Value < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " Spirit Speak to invoke this prayer" );
				return false;
			}


			return true;
		}

		public override bool CheckFizzle()
		{
			if ( !base.CheckFizzle() )
				return false;

			int tithing = RequiredTithing;
			double min, max;

			GetCastSkills( out min, out max );

			if ( AosAttributes.GetValue( Caster, AosAttribute.LowerRegCost ) > Utility.Random( 100 ) )
				tithing = 0;

			int mana = ScaleMana( GetMana() );



			Caster.TithingPoints -= tithing;

			return true;
		}

		public override void SayMantra()
		{
			Caster.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, Info.Mantra );
			Caster.PlaySound( 0x24A );
		}

		public override void DoFizzle()
		{
			Caster.PlaySound( 0x1D6 );
			Caster.NextSpellTime = 3;
		}

		public override void DoHurtFizzle()
		{
			Caster.PlaySound( 0x1D6 );
		}

		public override void OnDisturb( DisturbType type, bool message )
		{
			base.OnDisturb( type, message );

			if ( message )
				Caster.PlaySound( 0x1D6 );
		}

		public override void OnBeginCast()
		{
			base.OnBeginCast();

			Caster.FixedEffect( 0x37C4, 10, 42, 4, 3 );
		}

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill + 40.0;
		}
	}
}