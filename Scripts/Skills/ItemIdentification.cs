using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class ItemIdentification
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.ItemID].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile from)
        {
            from.SendLocalizedMessage(500343); // What do you wish to appraise and identify?
            from.Target = new InternalTarget();

            if(from.SpecClasse == SpecClasse.Mage)
            {
                switch (from.SpecLevel)
                {
                    case 1:
                        return TimeSpan.FromSeconds(4.5);
                    case 2:
                        return TimeSpan.FromSeconds(3.0);
                    case 3:
                        return TimeSpan.FromSeconds(2.5);
                    case 4:
                        return TimeSpan.FromSeconds(2.0);
                    case 5:
                        return TimeSpan.FromSeconds(1.0);
                    case 6:
                        return TimeSpan.FromSeconds(1.0);
                }
            }
            return TimeSpan.FromSeconds(5.0);
        }

        [PlayerVendorTarget]
        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(8, false, TargetFlags.None)
            {
                this.AllowNonlocal = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Item)
                {
                    if (from.CheckTargetSkill(SkillName.ItemID, o, 0, 130)) // changed to 130 from 100, makes more sense in my mind
                    {
                        if (o is Item)
                            ((Item)o).Unidentified = false;

                        if (!Core.AOS)
                            ((Item)o).OnSingleClick(from);
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain...
                    }
                }
                else if (o is Mobile)
                {
                    ((Mobile)o).OnSingleClick(from);
                }
                else
                {
                    from.SendLocalizedMessage(500353); // You are not certain...
                }
                Server.Engines.XmlSpawner2.XmlAttach.RevealAttachments(from, o);
            }
        }
    }
}