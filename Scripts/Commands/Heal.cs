using Server.Targeting;

namespace Server.Commands
{
    public class Heal
    {
        public static void Initialize()
        {
            CommandSystem.Register("heal", AccessLevel.Administrator, new CommandEventHandler(Identify_OnCommand));
        }

        [Usage("heal")]
        [Description("heals the target to full hp and cures")]
        private static void Identify_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Select target to heal/cure");
            e.Mobile.Target = new HealTarget();
        }

        public class HealTarget : Target
        {
            public HealTarget()
                : base(50, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                Mobile mob = (Mobile)o;

                mob.Hits = mob.HitsMax;
                mob.Mana = mob.ManaMax;
                mob.Stam = mob.StamMax;

                mob.CurePoison(from);
                
                mob.PlaySound(0x214);
                mob.FixedEffect(0x376A, 10, 16);

            }
        }
    }
}




