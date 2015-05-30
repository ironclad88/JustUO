using System;
using System.Collections.Generic;
using Server.Multis;

namespace Server.Mobiles
{
    public class SBShipwright : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBShipwright()
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
                this.Add(new GenericBuyInfo("1041205", typeof(SmallBoatDeed), 25000, 20, 0x14F2, 0));
                this.Add(new GenericBuyInfo("1041206", typeof(SmallDragonBoatDeed), 35000, 20, 0x14F2, 0));
                this.Add(new GenericBuyInfo("1041207", typeof(MediumBoatDeed), 50000, 20, 0x14F2, 0));
                this.Add(new GenericBuyInfo("1041208", typeof(MediumDragonBoatDeed), 60000, 20, 0x14F2, 0));
                this.Add(new GenericBuyInfo("1041209", typeof(LargeBoatDeed), 80000, 20, 0x14F2, 0));
                this.Add(new GenericBuyInfo("1041210", typeof(LargeDragonBoatDeed), 90000, 20, 0x14F2, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                //You technically CAN sell them back, *BUT* the vendors do not carry enough money to buy with
            }
        }
    }
}