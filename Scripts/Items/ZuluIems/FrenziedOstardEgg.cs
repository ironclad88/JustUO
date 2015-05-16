using System;
using Server.Mobiles;
using Server.Spells;
using Server.Custom;
using Server.Mobiles.Animals.Mounts.ZuluOstards;

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

        [Constructable]
        public FrenziedOstardEgg(Serial serial)
            : base(serial)
        {
        }

        private static readonly Type[] m_Types = new Type[]
        {
            typeof(NecroFrenziedOstard),
            typeof(FrenziedOstard),
            typeof(EarthFrenziedOstard),
            typeof(EmeraldFrenziedOstard),
            typeof(FireFrenziedOstard),
            typeof(GoldenFrenziedOstard),
            typeof(HeavenlyFrenziedOstard),
            typeof(HighlandFrenziedOstard),
            typeof(IceFrenziedOstard),
            typeof(MistFrenziedOstard),
            typeof(MountainFrenziedOstard),
            typeof(PlainsFrenziedOstard),
            typeof(RubyFrenziedOstard),
            typeof(SapphireFrenziedOstard),
            typeof(ShadowFrenziedOstard),
            typeof(SnowFrenziedOstard),
            typeof(StoneFrenziedOstard),
            typeof(SwampFrenziedOstard),
            typeof(TropicalFrenziedOstard),
            typeof(ValleyFrenziedOstard)
        };

        private static readonly Type[] ghostLlama = new Type[]
        {
            typeof(GhostLlama)

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
            TimeSpan duration;
            duration = TimeSpan.FromDays(1000);
            RandomClass rnd = new RandomClass();
            var eastereggroll = rnd.D100Roll(1);
            if (eastereggroll == 66)
            {
                var secondRoll = rnd.D20Roll(1);
                if (secondRoll >= 10) {  // hard as crap to get
                Console.WriteLine("GHOST LLAMA!");
                BaseCreature creature2 = (BaseCreature)Activator.CreateInstance(typeof(GhostLlama));
                SpellHelper.Summon(creature2, from, 0x215, duration, false, false);
                creature2.Summoned = false;
                creature2.Controlled = true;
                from.SendMessage("What is the meaning of this?!");
                this.Consume(1);
                }
            }
            else { 
            BaseCreature creature = (BaseCreature)Activator.CreateInstance(m_Types[Utility.Random(m_Types.Length)]);
            
            SpellHelper.Summon(creature, from, 0x215, duration, false, false);
            creature.Summoned = false;
            
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