using Server.Items.ZuluIems.SOS;
using System;

namespace Server.Items
{
    public class MessageInABottle : Item
    {
        private Map m_TargetMap;
        private int m_Level;
        [Constructable]
        public MessageInABottle()
            : this(Map.Trammel)
        {
        }

        public MessageInABottle(Map map)
            : this(map, GetRandomLevel())
        {
            this.Stackable = true;
        }

        [Constructable]
        public MessageInABottle(Map map, int level)
            : base(0x099F)
        {
            
            this.Stackable = true;
            this.Weight = 1.0;
            this.m_TargetMap = map;
            this.m_Level = level;
        }

        public MessageInABottle(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1041080;
            }
        }// a message in a bottle
        [CommandProperty(AccessLevel.GameMaster)]
        public Map TargetMap
        {
            get
            {
                return this.m_TargetMap;
            }
            set
            {
                this.m_TargetMap = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get
            {
                return this.m_Level;
            }
            set
            {
                this.m_Level = Math.Max(1, Math.Min(value, 4));
            }
        }
        public static int GetRandomLevel()
        {
            if (Core.AOS && 1 > Utility.Random(25))
                return 4; // ancient

            // JustZH needs to be fixed at a later date
            return Utility.RandomMinMax(1, 3);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)3); // version

            writer.Write((int)this.m_Level);

            writer.Write(this.m_TargetMap);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch ( version )
            {
                case 3:
                case 2:
                    {
                        this.m_Level = reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        this.m_TargetMap = reader.ReadMap();
                        break;
                    }
                case 0:
                    {
                        this.m_TargetMap = Map.Trammel;
                        break;
                    }
            }

            if (version < 2)
                this.m_Level = GetRandomLevel();

            /*if (version < 3 && this.m_TargetMap == Map.Tokuno)
                this.m_TargetMap = Map.Trammel;*/
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {
                int rnd = Utility.Random(6);
                switch (rnd)
                {
                    case 1:
                        from.AddToBackpack(new SOS1(this.m_TargetMap, this.m_Level));
                        this.Consume();
                       // this.ReplaceWithAndConsume(new SOS1(this.m_TargetMap, this.m_Level));
                       // from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 501891); // You extract the message from the bottle.
                        break;
                    case 2:
                        from.AddToBackpack(new SOS2(this.m_TargetMap, this.m_Level));
                        this.Consume();
                       // this.ReplaceWithAndConsume(new SOS2(this.m_TargetMap, this.m_Level));
                      //  from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 501891); // You extract the message from the bottle.
                        break;
                    case 3:
                        from.AddToBackpack(new SOS3(this.m_TargetMap, this.m_Level));
                        this.Consume();
                       // this.ReplaceWithAndConsume(new SOS3(this.m_TargetMap, this.m_Level));
                       // from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 501891); // You extract the message from the bottle.
                        break;
                    case 4:
                        from.AddToBackpack(new SOS4(this.m_TargetMap, this.m_Level));
                        this.Consume();
                        //this.ReplaceWithAndConsume(new SOS4(this.m_TargetMap, this.m_Level));
                        //from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 501891); // You extract the message from the bottle.
                        break;
                    case 5:
                        from.AddToBackpack(new SOS5(this.m_TargetMap, this.m_Level));
                        this.Consume();
                        //this.ReplaceWithAndConsume(new SOS5(this.m_TargetMap, this.m_Level));
                       // from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 501891); // You extract the message from the bottle.
                        break;
                    case 6:
                        from.AddToBackpack(new SOS6(this.m_TargetMap, this.m_Level));
                        this.Consume();
                       // this.ReplaceWithAndConsume(new SOS6(this.m_TargetMap, this.m_Level));
                       // from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 501891); // You extract the message from the bottle.
                        break;
                }
                
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }
    }
}