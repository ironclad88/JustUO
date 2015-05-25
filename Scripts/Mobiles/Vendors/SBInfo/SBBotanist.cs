using System;
using System.Collections.Generic;
using Server.Items;
using Server.Items.ZuluIems;

namespace Server.Mobiles
{
    public class SBBotanist : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBBotanist()
        {
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return base.m_SellInfo;
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
                this.Add(new GenericBuyInfo(typeof(YuccaTree), 10000, 10, 0xd38, 0));
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