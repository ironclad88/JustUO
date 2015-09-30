﻿using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Spells.Zulu.ClericSpells
{
    class HolyGate : HolySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Holy Gate", "Angelus De Porta",
            239,
            9021,
            Reagent.BlackPearl,
            Reagent.Bloodmoss,
            Reagent.Garlic);

        public HolyGate(Mobile caster, Item scroll)
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

        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            setCords(Caster.Y, Caster.X);
            Caster.Target = new InternalTarget(this);
        }


        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                Effects.PlaySound(p, Caster.Map, 0x382);

                Point3D loc = new Point3D(p.X, p.Y, p.Z);
                Item item = new InternalItem(loc, Caster.Map, Caster);
            }

            FinishSequence();
        }

        public static void playEffect(Mobile m)
        {

        }

        private class InternalTarget : Target
        {
            private readonly HolyGate m_Owner;
            public InternalTarget(HolyGate owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.None)
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


        [DispellableField]
        private class InternalItem : Item
        {
            private Timer m_Timer;
            private DateTime m_End;
            private Mobile m_Owner;

            public override bool BlocksFit { get { return true; } }

            public InternalItem(Point3D loc, Map map, Mobile caster)
                : base(0xF6C)
            {
                m_Owner = caster;
                Visible = false;
                Movable = false;
                this.Light = LightType.Circle300;
                this.Hue = 0x49E;
                Name = "Holy Gate";
                MoveToWorld(loc, map);

                if (caster.InLOS(this))
                    Visible = true;
                else
                    Delete();

                if (Deleted)
                    return;

                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(30.0));
                m_Timer.Start();

                m_End = DateTime.Now + TimeSpan.FromSeconds(30.0);
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override bool HandlesOnMovement { get { return true; } }
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)1); // version
                writer.Write(m_End - DateTime.Now);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
                TimeSpan duration = reader.ReadTimeSpan();

                m_Timer = new InternalTimer(this, duration);
                m_Timer.Start();

                m_End = DateTime.Now + duration;
            }

            public override bool OnMoveOver(Mobile m)
            {
                if (m is PlayerMobile && !m.Alive)
                {
                    m.SendGump(new ResurrectGump(m));

                    m.SendMessage("The power of the gods surges through you!");
                }
                else
                    m.PlaySound(0x339);
                return true;
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
            }

            private class InternalTimer : Timer
            {
                private InternalItem m_Item;

                public InternalTimer(InternalItem item, TimeSpan duration)
                    : base(duration)
                {
                    m_Item = item;
                }

                protected override void OnTick()
                {
                    m_Item.Delete();
                }
            }
        }

    }
}