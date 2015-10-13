#region References

using System;
using System.Collections.Generic;
using System.Linq;
using Server;

#endregion

namespace Arya.Auction
{
    /// <summary>
    ///     Summary description for AuctionScheduler.
    /// </summary>
    public class AuctionScheduler
    {
        private static InternalTimer m_Timer;
        private static DateTime m_Deadline = DateTime.MaxValue;

        /// <summary>
        ///     Gets the next deadline
        /// </summary>
        public static DateTime Deadline
        {
            get { return m_Deadline; }
        }

        public static void Initialize()
        {
            m_Timer = new InternalTimer();
            ResetTimer();
        }

        public static void Stop()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
            }
        }

        private static void ResetTimer()
        {
            if (AuctionSystem.Running)
            {
                CalculateDeadline();
            }

            m_Timer.Start();
        }

        /// <summary>
        ///     Calculates the next deadline for the scheduler
        /// </summary>
        private static void CalculateDeadline()
        {
            var list = new List<AuctionItem>(AuctionSystem.Auctions.Count + AuctionSystem.Pending.Count);

            list.AddRange(AuctionSystem.Auctions);
            list.AddRange(AuctionSystem.Pending);

            m_Deadline = DateTime.MaxValue;

            foreach (AuctionItem auction in list.Where(auction => auction.Deadline < m_Deadline))
            {
                m_Deadline = auction.Deadline;
            }
        }

        /// <summary>
        ///     This method accepts a new deadline being added to the system
        /// </summary>
        /// <param name="deadline">The new deadline</param>
        public static void UpdateDeadline(DateTime deadline)
        {
            if (deadline < m_Deadline)
            {
                m_Deadline = deadline;
            }
        }

        /// <summary>
        ///     Fires the DeadlineReached event
        /// </summary>
        private static void OnDeadlineReached()
        {
            AuctionSystem.OnDeadlineReached();
        }

        private static void OnTimer()
        {
            if (m_Deadline >= DateTime.UtcNow)
            {
                return;
            }

            m_Timer.Stop();

            OnDeadlineReached();

            ResetTimer();
        }

        private class InternalTimer : Timer
        {
            public InternalTimer()
                : base(TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(5.0))
            {
            }

            protected override void OnTick()
            {
                OnTimer();
            }
        }
    }
}