using System;
using System.Collections;

using Server.Factions;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Spells.Fifth;

namespace Server.Spells.Seventh
{
    public class PolymorphSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Polymorph", "Vas Ylem Rel",
            221,
            9002,
            Reagent.Bloodmoss,
            Reagent.SpidersSilk,
            Reagent.MandrakeRoot);
        private static readonly Hashtable m_Timers = new Hashtable();
        private readonly int m_NewBody;
        public PolymorphSpell(Mobile caster, Item scroll, int body)
            : base(caster, scroll, m_Info)
        {
            m_NewBody = body;
        }

        public PolymorphSpell(Mobile caster, Item scroll)
            : this(caster, scroll, 0)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
            }
        }
        public static bool StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
            }

            return (t != null);
        }

        public override bool CheckCast()
        {
            if (Caster.Flying)
            {
                Caster.SendLocalizedMessage(1113415); // You cannot use this ability while flying.
                return false;
            }
            if (Sigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
                return false;
            }
            if (TransformationSpellHelper.UnderTransformation(Caster))
            {
                Caster.SendLocalizedMessage(1061633); // You cannot polymorph while in that form.
                return false;
            }
            if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(502167); // You cannot polymorph while disguised.
                return false;
            }
            if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042512); // You cannot polymorph while wearing body paint
                return false;
            }
            if (!Caster.CanBeginAction(typeof(PolymorphSpell)))
            {
                if (Core.ML)
                    EndPolymorph(Caster);
                else
                    Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }
            if (m_NewBody == 0)
            {
                if (Caster.Int <= 60)
                {
                    Spell spell = new PolymorphSpell(Caster, Scroll, 18);
                    spell.Cast();
                }
                else if (Caster.Int <= 90)
                {
                    Spell spell = new PolymorphSpell(Caster, Scroll, 39);
                    spell.Cast();
                }
                else if (Caster.Int >= 110)
                {
                    Spell spell = new PolymorphSpell(Caster, Scroll, 58);
                    spell.Cast();
                }

                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            /*if ( Caster.Mounted )
            {
            Caster.SendLocalizedMessage( 1042561 ); //Please dismount first.
            } 
            else */
            if (Sigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
            }
            else if (!Caster.CanBeginAction(typeof(PolymorphSpell)))
            {
                /*if (Core.ML)
                    EndPolymorph(Caster);
                else */
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            else if (TransformationSpellHelper.UnderTransformation(Caster))
            {
                Caster.SendLocalizedMessage(1061633); // You cannot polymorph while in that form.
            }
            else if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(502167); // You cannot polymorph while disguised.
            }
            else if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042512); // You cannot polymorph while wearing body paint
            }
            else if (!Caster.CanBeginAction(typeof(IncognitoSpell)) || Caster.IsBodyMod)
            {
                DoFizzle();
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(PolymorphSpell)))
                {
                    if (m_NewBody != 0)
                    {
                        /* if (!((Body)m_NewBody).IsHuman)
                         {
                             IMount mt = Caster.Mount;

                             if (mt != null)
                                 mt.Rider = null;
                        } */

                        Caster.BodyMod = m_NewBody;

                        if (m_NewBody == 400 || m_NewBody == 401)
                            Caster.HueMod = Utility.RandomSkinHue();
                        else
                            Caster.HueMod = 0;

                        BaseArmor.ValidateMobile(Caster);
                        BaseClothing.ValidateMobile(Caster);

                        StopTimer(Caster);
                        
                        Timer t = new InternalTimer(Caster);

                        m_Timers[Caster] = t;

                        SpellHelper.AddStatBonus(this.Caster, this.Caster, StatType.Str);
                        SpellHelper.DisableSkillCheck = true;
                        SpellHelper.AddStatBonus(this.Caster, this.Caster, StatType.Dex);
                        SpellHelper.AddStatBonus(this.Caster, this.Caster, StatType.Int);
                        SpellHelper.DisableSkillCheck = false;

                        

                        int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, this.Caster, false) * 120 * this.Caster.SpecBonus(SpecClasse.Mage));
                        TimeSpan length = SpellHelper.GetDuration(this.Caster, this.Caster);

                        string args = String.Format("{0}\t{1}\t{2}", percentage, percentage, percentage);

                        BuffInfo.AddBuff(this.Caster, new BuffInfo(BuffIcon.AnimalForm, 1075847, 1075848, length, this.Caster, args.ToString()));

                        t.Start();

                    }
                }
                else
                {
                    Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                    // Caster.SendMessage("This spell is already in effect."); // test
                }
            }

            FinishSequence();
        }

        private static void EndPolymorph(Mobile m)
        {

            /*mod = m.GetStatMod( "[Magic] Str Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Str Offset" );

				mod = m.GetStatMod( "[Magic] Dex Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Dex Offset" );

				mod = m.GetStatMod( "[Magic] Int Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Int Offset" );
*/
            if (!m.CanBeginAction(typeof(PolymorphSpell)))
            {
                m.BodyMod = 0;
                m.HueMod = -1;
                m.EndAction(typeof(PolymorphSpell));

                BaseArmor.ValidateMobile(m);
                BaseClothing.ValidateMobile(m);
            }
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Owner;
            public InternalTimer(Mobile owner)
                : base(TimeSpan.FromSeconds(0))
            {
                m_Owner = owner;

                int val = (int)owner.Skills[SkillName.Magery].Value;

                if (val > 120)
                    val = 120;

                Delay = TimeSpan.FromSeconds(val);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                EndPolymorph(m_Owner);
            }
        }
    }
}
 