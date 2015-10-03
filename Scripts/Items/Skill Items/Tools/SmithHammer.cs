using System;
using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute(0x13E3, 0x13E4)]
    public class SmithHammer : BaseTool
    {
        [Constructable]
        public SmithHammer()
            : base(0x13E3)
        {
            this.Weight = 8.0;
            this.Layer = Layer.OneHanded;
        }

        [Constructable]
        public SmithHammer(int uses)
            : base("lol", 0x13E3)
        {
            this.Weight = 8.0;
            this.Layer = Layer.OneHanded;
        }

        public SmithHammer(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAnimation DefAnimation
        {
            get
            {
                return WeaponAnimation.Bash1H;
            }
        }
        public override SkillName DefSkill
        {
            get
            {
                return SkillName.Macing;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 50;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return 13;
            }
        }
        public override int AosMaxDamage
        {
            get
            {
                return 16;
            }
        }
        public override int AosSpeed
        {
            get
            {
                return 42;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 2.75f;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 30;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 2;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 36;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 46;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 31;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 80;
            }
        }

        public override CraftSystem CraftSystem
        {
            get
            {
                return DefBlacksmithy.CraftSystem;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}