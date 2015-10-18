using System;

//
// This is a first simple AI
//
//
namespace Server.Mobiles
{
    public class ElementalAI : BaseAI
    {
        public ElementalAI(BaseCreature m)
            : base(m)
        {
        }

        public override bool DoActionWander()
        {
            this.m_Mobile.DebugSay("I'm fine");

            if (this.m_Mobile.Combatant != null)
            {
                if (this.m_Mobile.Debug)
                    this.m_Mobile.DebugSay("{0} is attacking me", this.m_Mobile.Combatant.Name);

                this.m_Mobile.Say(Utility.RandomList(1005305, 501603));

                this.Action = ActionType.Flee;
            }
            else
            {
                if (this.m_Mobile.FocusMob != null)
                {
                    if (this.m_Mobile.Debug)
                        this.m_Mobile.DebugSay("{0} has talked to me", this.m_Mobile.FocusMob.Name);

                    this.Action = ActionType.Interact;
                }
                else
                {
                    this.m_Mobile.Warmode = false;
                    //make vendors wander less
                    
                }
            }

            return true;
        }

        public override bool DoActionInteract()
        {
            Mobile customer = this.m_Mobile.FocusMob;

            if (this.m_Mobile.Combatant != null)
            {
                if (this.m_Mobile.Debug)
                    this.m_Mobile.DebugSay("{0} is attacking me", this.m_Mobile.Combatant.Name);

                this.m_Mobile.Say(Utility.RandomList(1005305, 501603));

                this.Action = ActionType.Flee;

                return true;
            }

            if (customer == null || customer.Deleted || customer.Map != this.m_Mobile.Map)
            {
                this.m_Mobile.DebugSay("My customer have disapeared");
                this.m_Mobile.FocusMob = null;

                this.Action = ActionType.Wander;
            }
            else
            {
                if (customer.InRange(this.m_Mobile, this.m_Mobile.RangeFight))
                {
                    if (this.m_Mobile.Debug)
                        this.m_Mobile.DebugSay("I am with {0}", customer.Name);

                    this.m_Mobile.Direction = this.m_Mobile.GetDirectionTo(customer);
                }
                else
                {
                    if (this.m_Mobile.Debug)
                        this.m_Mobile.DebugSay("{0} is gone", customer.Name);

                    this.m_Mobile.FocusMob = null;

                    this.Action = ActionType.Wander;
                }
            }

            return true;
        }

        public override bool DoActionGuard()
        {
            this.m_Mobile.FocusMob = this.m_Mobile.Combatant;
            return base.DoActionGuard();
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(this.m_Mobile, 4))
                return true;

            return base.HandlesOnSpeech(from);
        }
    }
}