using System;

namespace Server.Items
{
    /// <summary>
    /// JustZH probably have to add Dice DMG to this one, but it shouldnt be used as a melee weapon, its for mages
    /// </summary>
    [FlipableAttribute(0xDF1, 0xDF0)]
    public class BlackLichStaff : BaseStaff
    {
        [Constructable]
        public BlackLichStaff()
            : base(0xDF0)
        {
            this.Name = "the black lich staff";
            this.Hue = 0x2C3;
            this.Weight = 6.0;
            this.SkillBonuses.SetValues(0, SkillName.Magery, 5.0);
       //     this.PoisonCharges = 100;
            this.PermaPoison = true;
            this.Poison = Poison.Lethal;
        }

        public BlackLichStaff(Serial serial)
            : base(serial)
        {
        }

       
        public override int AosStrengthReq
        {
            get
            {
                return 35;
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
                return 39;
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
                return 35;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 8;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 33;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 35;
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
                return 70;
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