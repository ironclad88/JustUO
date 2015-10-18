using Server.Items;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands
{
    public class Identify
    {
        public static void Initialize()
        {
            CommandSystem.Register("Identify", AccessLevel.Administrator, new CommandEventHandler(Identify_OnCommand));
            CommandSystem.Register("id", AccessLevel.Administrator, new CommandEventHandler(Identify_OnCommand));
        }

        [Usage("Identify")]
        [Description("Identifies the item")]
        private static void Identify_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Select a bag to identify items in, or select unidentified item");
            e.Mobile.Target = new IDTarget();

        }

        public class IDTarget : Target
        {
            public IDTarget()
                : base(20, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Item)
                {

                    if (o is BaseContainer)
                    {
                        BaseContainer c = (BaseContainer)o;
                        foreach (Item i in c.Items)
                        {
                            if (i is Item)
                                ((Item)i).Unidentified = false;
                        }
                        from.SendMessage("Identified bag done!");
                    }
                    else if (o is Item)
                        ((Item)o).Unidentified = false;
                }
                from.Target = new IDTarget();
            }
        }
    }
}




           