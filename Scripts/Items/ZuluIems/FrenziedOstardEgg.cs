using System;
using Server.Mobiles;
using Server.Spells;
using Server.Custom;

namespace Server.Items
{
    public class FrenziedOstardEgg : Item
    {
        [Constructable]
        public FrenziedOstardEgg()
            : base(0x1726) //0xFF2
        {
            this.Movable = true;
            this.Hue = 0x494;
            this.Stackable = true;
        }

        public FrenziedOstardEgg(Serial serial)
            : base(serial)
        {
        }

        private static readonly Type[] m_Types = new Type[]
        {
            typeof(NecroFrenziedOstard),
            typeof(FrenziedOstard)
        };

        public override string DefaultName
        {
            get
            {
                return "Frenzied Ostard Egg";
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
            BaseCreature creature = (BaseCreature)Activator.CreateInstance(m_Types[Utility.Random(m_Types.Length)]);
            TimeSpan duration;
            duration = TimeSpan.FromDays(1);
            SpellHelper.Summon(creature, from, 0x215, duration, false, false);
            creature.Summoned = false;
            RandomClass rnd = new RandomClass();
            var diceRoll = rnd.D20Roll(1);
            Console.WriteLine("D20 Dice roll: " + diceRoll);
            if (diceRoll <= 4)
            {
                if (from.Skills.AnimalLore.Value >= 90)
                {
                    creature.Controlled = true;
                }
                else
                {
                    creature.Controlled = false;
                }
            }
            else
            {
                creature.Controlled = true;
            }
            this.Consume(1);
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