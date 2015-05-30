using System;
using System.Collections.Generic;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles 
{ 
    public class SBVarietyDealer : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBVarietyDealer()
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
                //this.Add(new GenericBuyInfo(typeof(Bandage), 5, 20, 0xE21, 0));

                this.Add(new GenericBuyInfo(typeof(BlankScroll), 5, GlobalSettings.BlankAmount, 0x0E34, 0));

                this.Add(new GenericBuyInfo(typeof(NightSightPotion), 15, 10, 0xF06, 0));
                this.Add(new GenericBuyInfo(typeof(AgilityPotion), 15, 10, 0xF08, 0));
                this.Add(new GenericBuyInfo(typeof(StrengthPotion), 15, 10, 0xF09, 0));
                this.Add(new GenericBuyInfo(typeof(RefreshPotion), 15, 10, 0xF0B, 0));
                this.Add(new GenericBuyInfo(typeof(LesserCurePotion), 15, 10, 0xF07, 0));
                this.Add(new GenericBuyInfo(typeof(LesserHealPotion), 15, 10, 0xF0C, 0));
                this.Add(new GenericBuyInfo(typeof(LesserPoisonPotion), 15, 10, 0xF0A, 0));
                this.Add(new GenericBuyInfo(typeof(LesserExplosionPotion), 21, 10, 0xF0D, 0));


                this.Add(new GenericBuyInfo(typeof(BlackPearl), 5, GlobalSettings.ReagAmount, 0xF7A, 0));
                this.Add(new GenericBuyInfo(typeof(Bloodmoss), 5, GlobalSettings.ReagAmount, 0xF7B, 0));
                this.Add(new GenericBuyInfo(typeof(MandrakeRoot), 3, GlobalSettings.ReagAmount, 0xF86, 0));
                this.Add(new GenericBuyInfo(typeof(Garlic), 3, GlobalSettings.ReagAmount, 0xF84, 0));
                this.Add(new GenericBuyInfo(typeof(Ginseng), 3, GlobalSettings.ReagAmount, 0xF85, 0));
                this.Add(new GenericBuyInfo(typeof(Nightshade), 3, GlobalSettings.ReagAmount, 0xF88, 0));
                this.Add(new GenericBuyInfo(typeof(SpidersSilk), 3, GlobalSettings.ReagAmount, 0xF8D, 0));
                this.Add(new GenericBuyInfo(typeof(SulfurousAsh), 3, GlobalSettings.ReagAmount, 0xF8C, 0)); 

               // this.Add(new GenericBuyInfo(typeof(BreadLoaf), 7, 10, 0x103B, 0));
                this.Add(new GenericBuyInfo(typeof(Backpack), 15, 20, 0x9B2, 0));



                this.Add(new GenericBuyInfo(typeof(RecallRune), 15, 10, 0x1f14, 0));
                this.Add(new GenericBuyInfo(typeof(Spellbook), 18, 10, 0xEFA, 0));

            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                this.Add(typeof(Bandage), 1);

                this.Add(typeof(BlankScroll), 3);

                this.Add(typeof(NightSightPotion), 7);
                this.Add(typeof(AgilityPotion), 7);
                this.Add(typeof(StrengthPotion), 7);
                this.Add(typeof(RefreshPotion), 7);
                this.Add(typeof(LesserCurePotion), 7);
                this.Add(typeof(LesserHealPotion), 7);
                this.Add(typeof(LesserPoisonPotion), 7);
                this.Add(typeof(LesserExplosionPotion), 10);

                this.Add(typeof(Bolt), 3);
                this.Add(typeof(Arrow), 2);

                this.Add(typeof(BlackPearl), 3);
                this.Add(typeof(Bloodmoss), 3);
                this.Add(typeof(MandrakeRoot), 2);
                this.Add(typeof(Garlic), 2);
                this.Add(typeof(Ginseng), 2);
                this.Add(typeof(Nightshade), 2);
                this.Add(typeof(SpidersSilk), 2);
                this.Add(typeof(SulfurousAsh), 2);

                this.Add(typeof(BreadLoaf), 3);
                this.Add(typeof(Backpack), 7);
                this.Add(typeof(RecallRune), 8);
                this.Add(typeof(Spellbook), 9);
                this.Add(typeof(BlankScroll), 3);

                if (Core.AOS)
                {
                    this.Add(typeof(BatWing), 2);
                    this.Add(typeof(GraveDust), 2);
                    this.Add(typeof(DaemonBlood), 3);
                    this.Add(typeof(NoxCrystal), 3);
                    this.Add(typeof(PigIron), 3);
                }

                Type[] types = Loot.RegularScrollTypes;

                for (int i = 0; i < types.Length; ++i)
                    this.Add(types[i], ((i / 8) + 2) * 5);
            }
        }
    }
}