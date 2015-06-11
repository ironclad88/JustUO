using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles.ZuluMonsters
{
    [CorpseName("an orcish corpse")]
    public class OrcMage : BaseCreature
    {
        [Constructable]
        public OrcMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = NameList.RandomName("orc") + " the Orcmage";
            this.Body = 17;
            this.BaseSoundID = 0x45A;
            this.Hue = 201;

            this.SetStr(195);
            this.SetDex(90);
            this.SetInt(300);

            this.SetHits(95, 110);

            this.SetDiceDmg(2, 4);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetSkill(SkillName.MagicResist, 70);
            this.SetSkill(SkillName.Tactics, 50);
            this.SetSkill(SkillName.Magery, 100);

            this.Fame = 1500;
            this.Karma = -1500;

            this.VirtualArmor = 28;

        }

        public OrcMage(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType
        {
            get
            {
                return InhumanSpeech.Orc;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return false; // this should not matter atm
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.SavagesAndOrcs;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager);
        }
        /* // Not needed
        public override bool IsEnemy(Mobile m)
        {
            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }
        */
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}