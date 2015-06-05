using System;
using Server.Mobiles;
using Server.Spells;
using Server.Custom;

namespace Server.Items
{
    public class OstardEgg : Item
    {
        [Constructable]
        public OstardEgg()
            : base(0x1726) //0xFF2
        {
            this.Movable = true;
            this.Hue = 1102;
            this.Stackable = true;
        }

        [Constructable]
        public OstardEgg(Serial serial)
            : base(serial)
        {
        }

        private static readonly Type[] m_Types = new Type[] // should not include awesome ostards like necro or holy
        {
            typeof(DesertOstard),
            typeof(ForestOstard)
        };

        public override string DefaultName
        {
            get
            {
                return "Ostard Egg";
            }
        }
        public override void OnDoubleClick(Mobile from) // regular ostards are always tame
        {
            BaseCreature creature = (BaseCreature)Activator.CreateInstance(m_Types[Utility.Random(m_Types.Length)]);
            TimeSpan duration;
            duration = TimeSpan.FromDays(1000);
            from.SendMessage("The egg begins to move and");
            SpellHelper.Summon(creature, from, 0x215, duration, false, false);
            creature.Summoned = false;

            from.SendMessage("A baby ostard appears and accepts you as his master!");
            creature.Controlled = true;

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