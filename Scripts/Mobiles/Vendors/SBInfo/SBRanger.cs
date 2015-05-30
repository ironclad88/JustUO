using System;
using System.Collections.Generic;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
    public class SBRanger : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBRanger()
        {
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return base.SellInfo;
            }
        }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return this.m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                this.Add(new GenericBuyInfo(typeof(Crossbow), 55, 20, 0xF50, 0));
                this.Add(new GenericBuyInfo(typeof(HeavyCrossbow), 55, 20, 0x13FD, 0));
                this.Add(new GenericBuyInfo(typeof(Bolt), 2, GlobalSettings.ArrowAmount, 0x1BFB, 0));
                this.Add(new GenericBuyInfo(typeof(Bow), 40, 20, 0x13B2, 0));
                this.Add(new GenericBuyInfo(typeof(Arrow), 2, GlobalSettings.ArrowAmount, 0xF3F, 0));
                this.Add(new GenericBuyInfo(typeof(Feather), 2, GlobalSettings.ArrowAmount, 0x1BD1, 0));
                this.Add(new GenericBuyInfo(typeof(Shaft), 3, GlobalSettings.ArrowAmount, 0x1BD4, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
            }
        }
    }
}