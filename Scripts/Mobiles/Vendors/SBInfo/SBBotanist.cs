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
                this.Add(new GenericBuyInfo(typeof(YuccaTree), 10000, 20, 0xd38, 0));
                this.Add(new GenericBuyInfo(typeof(PottedCactus), 100, 10, 0x1E0F, 0));
                this.Add(new GenericBuyInfo(typeof(PottedCactus1), 200, 10, 0x1E10, 0));
                this.Add(new GenericBuyInfo(typeof(PottedCactus2), 250, 10, 0x1E11, 0));
                this.Add(new GenericBuyInfo(typeof(PottedCactus3), 300, 10, 0x1E12, 0));
                this.Add(new GenericBuyInfo(typeof(PottedCactus4), 350, 10, 0x1E13, 0));
                this.Add(new GenericBuyInfo(typeof(PottedPlant), 100, 10, 0x11CA, 0));
                this.Add(new GenericBuyInfo(typeof(PottedPlant1), 120, 10, 0x11CB, 0));
                this.Add(new GenericBuyInfo(typeof(PottedTree), 400, 10, 0x11C8, 0));

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