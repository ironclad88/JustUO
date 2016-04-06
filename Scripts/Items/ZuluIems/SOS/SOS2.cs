using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Gumps;

namespace Server.Items.ZuluIems.SOS
{
    [Flipable(0x14ED, 0x14EE)]
    public class SOS2 : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1041081; // a waterstained SOS
            }
        }

        private int m_Level;
        private Map m_TargetMap;
        private Point3D m_TargetLocation;
        private int m_MessageIndex;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsAncient
        {
            get
            {
                return (this.m_Level >= 4);
            }
        }

        private static int randomLevel()
        {
            int chance = Utility.Random(1, 100);
            
            if(chance <= 3)
            {
                return 5;
            }
            else if(chance <= 10)
            {
                return 4;
            }
            else if (chance <= 40)
            {
                return 3;
            }
            else if (chance <= 60)
            {
                return 2;
            }
            return 1;
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
                this.m_Level = randomLevel();
                this.InvalidateProperties();
            }
        }

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
        public Point3D TargetLocation
        {
            get
            {
                return this.m_TargetLocation;
            }
            set
            {
                this.m_TargetLocation = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MessageIndex
        {
            get
            {
                return this.m_MessageIndex;
            }
            set
            {
                this.m_MessageIndex = value;
            }
        }

        public SOS2(Serial serial)
            : base(serial)
        {
        }

        [Constructable]
        public SOS2(Map map)
            : this(map, MessageInABottle.GetRandomLevel())
        {
        }

        [Constructable]
        public SOS2(Map map, int level)
            : base(0x14EE)
        {
            this.Weight = 1.0;
            //this.Stackable = true;
            this.Hue = 1161;
            this.m_Level = level;
            this.m_MessageIndex = Utility.Random(MessageEntry.Entries.Length);
            this.m_TargetMap = Map.Felucca;
            this.m_TargetLocation = FindLocation(this.m_TargetMap);

        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)4); // version

            writer.Write(this.m_Level);

            writer.Write(this.m_TargetMap);
            writer.Write(this.m_TargetLocation);
            writer.Write(this.m_MessageIndex);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 4:
                case 3:
                case 2:
                    {
                        this.m_Level = reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        this.m_TargetMap = reader.ReadMap();
                        this.m_TargetLocation = reader.ReadPoint3D();
                        this.m_MessageIndex = reader.ReadInt();

                        break;
                    }
                case 0:
                    {
                        this.m_TargetMap = this.Map;

                        if (this.m_TargetMap == null || this.m_TargetMap == Map.Internal)
                            this.m_TargetMap = Map.Trammel;

                        this.m_TargetLocation = FindLocation(this.m_TargetMap);
                        this.m_MessageIndex = Utility.Random(MessageEntry.Entries.Length);

                        break;
                    }
            }

            if (version < 2)
                this.m_Level = MessageInABottle.GetRandomLevel();

            if (version < 3)


                if (version < 4 && this.m_TargetMap == Map.Tokuno)
                    this.m_TargetMap = Map.Trammel;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {
                MessageEntry entry;

                if (this.m_MessageIndex >= 0 && this.m_MessageIndex < MessageEntry.Entries.Length)
                    entry = MessageEntry.Entries[this.m_MessageIndex];
                else
                    entry = MessageEntry.Entries[this.m_MessageIndex = Utility.Random(MessageEntry.Entries.Length)];

                //from.CloseGump( typeof( MessageGump ) );
                from.SendGump(new MessageGump(entry, this.m_TargetMap, this.m_TargetLocation));
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        private static readonly int[] m_WaterTiles = new int[]
        {
            0x00A8, 0x00AB,
            0x0136, 0x0137
        };

        private static readonly Rectangle2D[] m_BritRegions = new Rectangle2D[] { new Rectangle2D(0, 0, 5120, 4096) };

        public static Point3D FindLocation(Map map)
        {
            // Custom loc
            int x = 2625;
            int y = 3252;

            return new Point3D(x, y, 0);
        }



#if false
		private class MessageGump : Gump
		{
			public MessageGump( MessageEntry entry, Map map, Point3D loc ) : base( (640 - entry.Width) / 2, (480 - entry.Height) / 2 )
			{
				int xLong = 0, yLat = 0;
				int xMins = 0, yMins = 0;
				bool xEast = false, ySouth = false;
				string fmt;

				if ( Sextant.Format( loc, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
					fmt = String.Format( "{0}°{1}'{2},{3}°{4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
				else
					fmt = "?????";

				AddPage( 0 );
				AddBackground( 0, 0, entry.Width, entry.Height, 2520 );
				AddHtml( 38, 38, entry.Width - 83, entry.Height - 86, String.Format( entry.Message, fmt ), false, false );
			}
		}
#else
        private class MessageGump : Gump
        {
            public MessageGump(MessageEntry entry, Map map, Point3D loc)
                : base(150, 50)
            {
                int xLong = 0, yLat = 0;
                int xMins = 0, yMins = 0;
                bool xEast = false, ySouth = false;
                string fmt;

                if (Sextant.Format(loc, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth))
                    fmt = String.Format("{0}°{1}'{2},{3}°{4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W");
                else
                    fmt = "?????";

                this.AddPage(0);

                this.AddBackground(0, 40, 350, 300, 2520);

                // Added better text
                this.AddHtml(30, 80, 285, 160, "This is a message hastily scribbled by a passenger aboard a sinking ship. While it is probably too late to save the passengers and crew, perhaps some treasure went down with the ship!", true, false);

                // JustZH removed this, maybe add something better later
                /*this.AddHtmlLocalized(30, 80, 285, 160, 1018326, true, true); /* This is a message hastily scribbled by a passenger aboard a sinking ship.
                * While it is probably too late to save the passengers and crew,
                * perhaps some treasure went down with the ship!
                * The message gives the ship's last known sextant co-ordinates.
                */

                this.AddHtml(35, 240, 230, 20, fmt, false, false);

                this.AddButton(35, 265, 4005, 4007, 0, GumpButtonType.Reply, 0);
                this.AddHtmlLocalized(70, 265, 100, 20, 1011036, false, false); // OKAY
            }
        }
#endif

        private class MessageEntry
        {
            private readonly int m_Width;

            private readonly int m_Height;

            private readonly string m_Message;

            public int Width
            {
                get
                {
                    return this.m_Width;
                }
            }
            public int Height
            {
                get
                {
                    return this.m_Height;
                }
            }
            public string Message
            {
                get
                {
                    return this.m_Message;
                }
            }

            public MessageEntry(int width, int height, string message)
            {
                this.m_Width = width;
                this.m_Height = height;
                this.m_Message = message;
            }

            private static readonly MessageEntry[] m_Entries = new MessageEntry[]
            {
                new MessageEntry(280, 180, "")
            };

            public static MessageEntry[] Entries
            {
                get
                {
                    return m_Entries;
                }
            }
        }
    }
}