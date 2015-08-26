using System;
using System.Collections.Generic;
using Server.Items;
using Server.Items.ZuluIems.Weapons.Ranged;
using Server.Custom;

namespace Server.Mobiles
{
    public class SBBowyer : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBBowyer()
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
                this.Add(new GenericBuyInfo(typeof(Icebow), 500, 20, 0x13B2, 0x0492));
                this.Add(new GenericBuyInfo(typeof(IceArrow), 50, GlobalSettings.ArrowAmount, 0xF3F, 0x0492));

                this.Add(new GenericBuyInfo(typeof(Firebow), 500, 20, 0x13B2, 0x0494));
                this.Add(new GenericBuyInfo(typeof(FireArrow), 50, GlobalSettings.ArrowAmount, 0xF3F, 0x0494));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                this.Add(typeof(FletcherTools), 1);
            }
        }
    }
}