using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;
using Server.Network;
using Server.Gumps;

namespace Server.Commands
{
    public class Count
    {
        public static void Initialize()
        {
            CommandSystem.Register("Count", AccessLevel.Player, new CommandEventHandler(count_OnCommand));
        }

        [Usage("Count")]
        [Description("Counts top level items in a container")]
        private static void count_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Select a bag to count items in.");
            e.Mobile.Target = new CountTarget();
        }
    }
    public class CountTarget : Target
    {
        public CountTarget()
            : base(20, false, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile m, object targeted)
        {
            List<Type> name = new List<Type>();
            List<int> count = new List<int>();
            if (targeted is BaseContainer)
            {
                BaseContainer c = (BaseContainer)targeted;
                foreach (Item i in c.Items)
                {
                    if (name.Contains(i.GetType()))
                    {
                        count[name.IndexOf(i.GetType())] = (int)count[name.IndexOf(i.GetType())] + i.Amount;
                    }
                    else
                    {
                        name.Add(i.GetType());
                        count.Add((int)i.Amount);
                    }

                }

                if (m.HasGump(typeof(CountGump)))
                    m.CloseGump(typeof(CountGump));
                m.SendGump(new CountGump(m, name, count));

                //foreach (Type n in name)
                //{
                //    m.SendMessage("{0}: {1}", n.Name, (int)count[name.IndexOf(n)]);
                //}
            }
            else
                m.SendMessage("That is not a container.");
        }
    }
}

namespace Server.Gumps
{
    public class CountGump : Gump
    {
        private List<Type> name;
        private List<int> count;

        private Mobile caller;

        public CountGump(Mobile mobile, List<Type> namew, List<int> countw)
            : base(50, 50)
        {
            caller = mobile;
            name = namew;
            count = countw;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 300, 426, 3500);
            AddPage(1);


            int page = 0, cpage = 1;
            int max = 18;
            if (name != null)// && name.Count == count.Count)
                foreach (Type n in name)
                {
                    if (n == null)
                        continue;
                    AddLabel(15, 23 + (20 * page), 10, string.Format("{0}: {1}", n.Name, (int)count[name.IndexOf(n)]));
                    page++;
                    if (page >= max)
                    {
                        page = 0;
                        cpage++;
                        AddButton(265, 390, 22405, 22405, 600, GumpButtonType.Page, cpage);

                        AddPage(cpage);
                        if (cpage > 1) AddButton(25, 390, 22402, 22402, 600, GumpButtonType.Page, cpage - 1);
                    }

                }

        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {

                        break;
                    }

            }
        }
    }
}