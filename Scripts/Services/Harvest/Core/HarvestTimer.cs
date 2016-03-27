using System;

namespace Server.Engines.Harvest
{
    public class HarvestTimer : Timer
    {
        private readonly Mobile m_From;
        private readonly Item m_Tool;
        private readonly HarvestSystem m_System;
        private readonly HarvestDefinition m_Definition;
        private readonly object m_ToHarvest;
        private readonly object m_Locked;
        private readonly int m_Count;
        private readonly int m_X;
        private readonly int m_Y;
        private bool m_NoResources;
        private int m_AutoLoop;
        private int m_Index;
        private int m_PauseCycles;
        public HarvestTimer(Mobile from, Item tool, HarvestSystem system, HarvestDefinition def, object toHarvest, object locked)
            : base(TimeSpan.Zero, def.EffectDelay)
        {
            this.m_From = from;
            this.m_Tool = tool;
            this.m_System = system;
            this.m_Definition = def;
            this.m_ToHarvest = toHarvest;
            this.m_Locked = locked;
            this.m_Count = Utility.RandomList(def.EffectCounts);
            this.m_AutoLoop = (m_From as Mobiles.PlayerMobile).AutoLoop;
            m_X = m_From.X;
            m_Y = m_From.Y;
            m_NoResources = false;
            m_PauseCycles = 0;
            (this.m_From as Mobiles.PlayerMobile).IsBusy = true;
            m_From.SendMessage(194, "[Autoloop] Looping, " + m_AutoLoop + " left.");
        }

        protected override void OnTick()
        {
            if(m_PauseCycles > 0)
            {
                m_PauseCycles--;
            }
            else if (m_AutoLoop > 0 && m_X == m_From.X && m_Y == m_From.Y && false == m_NoResources)
            {
                // We have autoloops left, we have not moved and we haven't run out of resources.
                if (!this.m_System.OnHarvesting(this.m_From, this.m_Tool, this.m_Definition, this.m_ToHarvest, this.m_Locked, ++this.m_Index == this.m_Count, out m_NoResources))
                {
                    // One harvest animation is done, check if we should continue or stop
                    --m_AutoLoop;
                    m_Index = 0;
                    if (false == m_NoResources)
                    {
                        // Only pause if there are still resources.
                        m_PauseCycles = 2;
                    }
                }
            }
            else
            {
                // Also end loop if there are no ore/fish/logs. Maybe look if m_From is locked to this.m_Locked somehow?
                m_From.SendMessage(194, "[Autoloop] You finish looping.");
                m_From.EndAction(this.m_Locked);
                (this.m_From as Mobiles.PlayerMobile).IsBusy = false;
                this.Stop();
            }
        }
    }
}