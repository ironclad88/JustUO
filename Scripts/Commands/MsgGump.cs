using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;
using Server.Gumps;
using Server.Network; 
using Server.Mobiles; 

namespace Server.Gumps 
{ 
	public class MsgGump : Gump 
	{ 
		public static void Initialize() 
		{ 
            // Players use the [Chat command to use the in game messenger system
			CommandSystem.Register( "msg", AccessLevel.Player, new CommandEventHandler( Msg_OnCommand ) ); 
		} 

		[Usage( "msg" )] 
		[Description( "Player messaging system." )] 
		private static void Msg_OnCommand( CommandEventArgs e ) 
		{
			e.Mobile.CloseGump( typeof(MsgGump) ); 
			e.Mobile.CloseGump( typeof(MsgClientGump) ); 
			e.Mobile.SendGump( new MsgGump( e.Mobile ) ); 
		} 

	    /*
         * Use the method below if you would rather have a normal word used, instead of a command
         * Using this will pull the messenger gump by using the following words
         * You can add to them if you like. 
         * If you use this, you should comment out the above command entry and Initialization line.
         * -Dian
		public static void Initialize()
		{
			// Register our speech handler
			EventSink.Speech += new SpeechEventHandler( EventSink_Speech );
		}
	
		private static void EventSink_Speech( SpeechEventArgs args )
		{
		//	Mobile m = args.Mobile;
			
			if ( !args.Handled
            && Insensitive.Equals( args.Speech, "online" )
            || Insensitive.Equals(args.Speech, "players") 
            || Insensitive.Equals(args.Speech, "chat"))
			{
				args.Mobile.CloseGump( typeof(MsgGump) ); 
				args.Mobile.CloseGump( typeof(MsgClientGump) ); 
				args.Mobile.SendGump( new MsgGump( args.Mobile ) ); 

				args.Handled = true;
			}
		}*/
        
		public static bool OldStyle = PropsConfig.OldStyle; 

		public static int GumpOffsetX = 50;//PropsConfig.GumpOffsetX;
        public static int GumpOffsetY = 50;//PropsConfig.GumpOffsetY;

        public static int TextHue = PropsConfig.TextHue;
        public static int TextOffsetX = PropsConfig.TextOffsetX;

        public static int OffsetGumpID = PropsConfig.OffsetGumpID;
        public static int HeaderGumpID = PropsConfig.HeaderGumpID;
        public static int EntryGumpID = PropsConfig.EntryGumpID;
        public static int BackGumpID = PropsConfig.BackGumpID;
        public static int SetGumpID = PropsConfig.SetGumpID;

        public static int SetWidth = PropsConfig.SetWidth;
        public static int SetOffsetX = PropsConfig.SetOffsetX, SetOffsetY = PropsConfig.SetOffsetY;
        public static int SetButtonID1 = PropsConfig.SetButtonID1;
        public static int SetButtonID2 = PropsConfig.SetButtonID2;

        public static int PrevWidth = PropsConfig.PrevWidth;
        public static int PrevOffsetX = PropsConfig.PrevOffsetX, PrevOffsetY = PropsConfig.PrevOffsetY;
        public static int PrevButtonID1 = PropsConfig.PrevButtonID1;
        public static int PrevButtonID2 = PropsConfig.PrevButtonID2;

        public static int NextWidth = PropsConfig.NextWidth;
        public static int NextOffsetX = PropsConfig.NextOffsetX, NextOffsetY = PropsConfig.NextOffsetY;
        public static int NextButtonID1 = PropsConfig.NextButtonID1;
        public static int NextButtonID2 = PropsConfig.NextButtonID2;

        public static int OffsetSize = PropsConfig.OffsetSize;

        public static int EntryHeight = PropsConfig.EntryHeight;
        public static int BorderSize = PropsConfig.BorderSize; 

		private static bool PrevLabel = false, NextLabel = false;

        private static int PrevLabelOffsetX = PrevWidth + 1;
        private static int PrevLabelOffsetY = 0;

        private static int NextLabelOffsetX = -29;
        private static int NextLabelOffsetY = 0;

        private static int EntryWidth = 350;
        private static int EntryCount = 15;

        private static int TotalWidth = OffsetSize + EntryWidth + OffsetSize + SetWidth + OffsetSize;
        private static int TotalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (EntryCount + 1));

        private static int BackWidth = BorderSize + TotalWidth + BorderSize;
        private static int BackHeight = BorderSize + TotalHeight + BorderSize; 

		private Mobile m_Owner; 
		private PlayerMobile m; 
		private ArrayList m_Mobiles; 
		private int m_Page; 

		private class InternalComparer : IComparer 
		{ 
			public static readonly IComparer Instance = new InternalComparer(); 

			public InternalComparer() 
			{ 
			} 

			public int Compare( object x, object y ) 
			{ 
				if ( x == null && y == null ) 
					return 0; 
				else if ( x == null ) 
					return -1; 
				else if ( y == null ) 
					return 1; 

				Mobile a = x as Mobile; 
				Mobile b = y as Mobile; 

				if ( a == null || b == null ) 
					throw new ArgumentException(); 

				if ( a.AccessLevel > b.AccessLevel ) 
					return -1; 
				else if ( a.AccessLevel < b.AccessLevel ) 
					return 1; 
				else 
					return CaseInsensitiveComparer.Default.Compare( a.Name, b.Name ); 
			} 
		} 

		public MsgGump( Mobile owner ) : this( owner, BuildList( owner ), 0 ) 
		{ 
		} 

		public MsgGump( Mobile owner, ArrayList list, int page ) : base( GumpOffsetX, GumpOffsetY ) 
		{ 
			owner.CloseGump( typeof( MsgGump ) ); 

			m_Owner = owner; 
			m_Mobiles = list; 
			m = m_Owner as PlayerMobile; 

			Initialize( page, m ); 
		} 

		public static ArrayList BuildList( Mobile owner ) 
		{ 
			ArrayList list = new ArrayList();
            List<NetState> states = NetState.Instances;

			for ( int i = 0; i < states.Count; ++i ) 
			{ 
				Mobile m = ((NetState)states[i]).Mobile; 

				if ( m != null && (m == owner || ((PlayerMobile)m).Onshow || m.AccessLevel < owner.AccessLevel )) 
					list.Add( m ); 
			} 

			list.Sort( InternalComparer.Instance ); 

			return list; 
		} 

		public void Initialize( int page, PlayerMobile whopm ) 
		{ 
			m_Page = page; 
			PlayerMobile who = whopm; 
    
			int count = m_Mobiles.Count - (page * EntryCount); 

			if ( count < 0 ) 
				count = 0; 
			else if ( count > EntryCount ) 
				count = EntryCount; 

			int totalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (count + 1)); 

			AddPage( 0 ); 

			AddBackground( 0, 0, BackWidth, BorderSize + totalHeight + BorderSize, BackGumpID ); 
			AddImageTiled( BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID ); 
        
			int x = BorderSize + OffsetSize; 
			int y = BorderSize + OffsetSize; 

			int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0); 

			if ( !OldStyle )  
				AddAlphaRegion( x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight );

	//		AddLabel( x + TextOffsetX, y, 1153, String.Format( "Page {0} of {1} ({2})", page+1, (m_Mobiles.Count + EntryCount - 1) / EntryCount, m_Mobiles.Count ) ); 
			
			if ( who.LocShow == false )
			{
				AddLabel( 12, 12, 36, "location" );
				AddButton( 60, 12, 0xD3, 0xD2, 5, GumpButtonType.Reply, 0 );
			} 
			else if(who.LocShow == true)
			{ 
				AddLabel( 12, 12, 61, "location" ); 
				AddButton( 60, 12, 0xD2, 0xD3, 5, GumpButtonType.Reply, 0 ); 
			}
			
			x += emptyWidth + OffsetSize; 

			if ( OldStyle ) 
				AddImageTiled( x, y, TotalWidth - (OffsetSize * 3) - SetWidth, EntryHeight, HeaderGumpID ); 
			else 
				AddImageTiled( x, y, PrevWidth, EntryHeight, HeaderGumpID ); 

			if ( page > 0 ) 
			{ 
				AddButton( x + PrevOffsetX, y + PrevOffsetY, PrevButtonID1, PrevButtonID2, 1, GumpButtonType.Reply, 0 ); 

				if ( PrevLabel ) 
					AddLabel( x + PrevLabelOffsetX, y + PrevLabelOffsetY, 1153, "Previous" ); 
			} 

			x += PrevWidth + OffsetSize; 

			if ( !OldStyle ) 
				AddImageTiled( x, y, NextWidth, EntryHeight, HeaderGumpID ); 

			if ( (page + 1) * EntryCount < m_Mobiles.Count ) 
			{ 
				AddButton( x + NextOffsetX, y + NextOffsetY, NextButtonID1, NextButtonID2, 2, GumpButtonType.Reply, 1 ); 

				if ( NextLabel ) 
					AddLabel( x + NextLabelOffsetX, y + NextLabelOffsetY, 1153, "Next" ); 
			} 

			for ( int i = 0, index = page * EntryCount; i < EntryCount && index < m_Mobiles.Count; ++i, ++index ) 
			{ 
				x = BorderSize + OffsetSize; 
				y += EntryHeight + OffsetSize; 

				Mobile m = (Mobile)m_Mobiles[index]; 
				string map = m.Map.ToString();
				string region = m.Region.ToString();  

				AddAlphaRegion( x, y, EntryWidth, EntryHeight );
				AddLabelCropped( x + TextOffsetX, y, EntryWidth - TextOffsetX, EntryHeight, GetHueFor( m ), m.Deleted ? "(deleted)" : m.Name );
				
				if(region == "")
				{
					AddLabelCropped( (int)((x + TextOffsetX) + (m.Name.Length * 7.5)), y, EntryWidth - TextOffsetX, EntryHeight, 1153, ((PlayerMobile)m).LocShow ?  String.Format( "[Unknown]") : "" );
					AddLabelCropped( (int)((x + TextOffsetX) + (m.Name.Length * 7.5) + 60 ), y, EntryWidth - TextOffsetX, EntryHeight, 1153, ((PlayerMobile)m).LocShow ?  String.Format( "[{0}]",map) : "" );
				}
				else
				{ 
					AddLabelCropped( (int)((x + TextOffsetX) + (m.Name.Length * 7.5)), y, EntryWidth - TextOffsetX, EntryHeight, 1153, ((PlayerMobile)m).LocShow ?  String.Format( "[{0}]",region) : "" );
					AddLabelCropped( (int)((x + TextOffsetX) + (m.Name.Length * 7.5) + (region.Length * 7.5)), y, EntryWidth - TextOffsetX, EntryHeight, 1153, ((PlayerMobile)m).LocShow ?  String.Format( "[{0}]",map) : "" );
				}

				x += EntryWidth + OffsetSize; 

				if ( SetGumpID != 0 ) 
					AddImageTiled( x, y, SetWidth, EntryHeight, SetGumpID ); 

				if (( m.NetState != null && !m.Deleted && m != who && !((PlayerMobile)m).IgnoreList.Contains ( who ) ) || ( who.AccessLevel > m.AccessLevel ))
					AddButton( x + SetOffsetX, y + SetOffsetY, SetButtonID1, SetButtonID2, i + 6, GumpButtonType.Reply, 0 );

				if ( who.IgnoreList.Contains ( m ) )
				{
					if( who == m )
						AddLabelCropped( 290, y, EntryWidth - TextOffsetX, EntryHeight, 32, m.Deleted ? "(deleted)" : String.Format("Guild Msg Off") );
					else
						AddLabelCropped( 290, y, EntryWidth - TextOffsetX, EntryHeight, 32, m.Deleted ? "(deleted)" : String.Format("- Ignored -") );	
				}
			}

			AddLabel( 150, 12, 43, "Msg Guild" );
			AddButton( 210, 13, 0x15e1, 0x15e5, 4, GumpButtonType.Reply, 0 );
 
			if ( who.Onshow == false )
			{
				AddLabel( 270, 12, 36, "Invis" );  
				AddButton( 310, 12, 0xD3, 0xD2, 3, GumpButtonType.Reply, 0 );
			} 
			else if(who.Onshow == true)
			{ 
				AddLabel( 270, 12, 61, "Online" );
				AddButton( 310, 12, 0xD2, 0xD3, 3, GumpButtonType.Reply, 0 ); 
			}
		} 

		private static int GetHueFor( Mobile m ) 
		{ 
			switch ( m.AccessLevel ) 
			{ 
				case AccessLevel.Administrator: return 0x516; 
				case AccessLevel.Seer: return 0x144; 
				case AccessLevel.GameMaster: return 0x21; 
				case AccessLevel.Counselor: return 0x2; 
				case AccessLevel.Player: default: 
				{ 
					if (m.NameHue != -1) 
						return m.NameHue; 
					else if ( m.Kills >= 5 ) 
						return 0x21; 
					else if ( m.Criminal ) 
						return 0x3B1; 

					return 0x58; 
				} 
			} 
		} 

		public override void OnResponse( NetState state, RelayInfo info ) 
		{ 
			Mobile from = state.Mobile; 
    
			switch ( info.ButtonID ) 
			{ 
				case 0: // Closed 
				{ 
					return; 
				} 
				case 1: // Previous 
				{ 
					if ( m_Page > 0 ) 
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page - 1 ) ); 

					break; 
				} 
				case 2: // Next 
				{ 
					if ( (m_Page + 1) * EntryCount < m_Mobiles.Count ) 
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page + 1 ) ); 

					break; 
				} 
				case 3: // Msg on/off 
				{ 
					if ( m.Onshow == true ) 
					{ 
						m.Onshow = false; 
						from.SendMessage( "You are now invisible to other players" );
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
					} 
					else if ( m.Onshow == false ) 
					{ 
						m.Onshow = true;
						from.SendMessage( "You are now visible to other players" ); 
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
					} 
					break; 
				}

				case 4: // GuildChat 
				{
					if ( from.Guild == null ) 
					{ 
						from.SendMessage( "You are not a member of any guild!" );
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
					}
					else
					{
						Mobile m = from;
						if( m.NetState == null || from == null )
						{
							from.CloseGump( typeof(MsgClientGump) ); 
							from.CloseGump( typeof(MsgGump) ); 	
						}
						else
						{
							from.CloseGump( typeof(MsgClientGump) ); 
							from.CloseGump( typeof(MsgGump) ); 
							from.SendGump( new MsgClientGump( from, m.NetState ) );
						}
					}
					break;
				}
				
				case 5: // Location show on/off 
				{ 
					if ( m.LocShow == true ) 
					{ 
						m.LocShow = false; 
						from.SendMessage( "Your location is hidden to other players" );
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
					} 
					else if ( m.LocShow == false ) 
					{ 
						m.LocShow = true;
						from.SendMessage( "Your location is now visible to other players" ); 
						from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
					} 
					break; 
				}
				
				default: 
				{ 
					int index = (m_Page * EntryCount) + (info.ButtonID - 6); 

					if ( index >= 0 && index < m_Mobiles.Count ) 
					{ 
						Mobile m = (Mobile)m_Mobiles[index]; 

						if ( m.Deleted ) 
						{ 
							from.SendMessage( "That player has deleted their character." ); 
							from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
						} 
						else if ( m.NetState == null || m == null ) 
						{ 
							from.SendMessage( "That player is no longer online." ); 
							from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
						} 
						else if ( m == m_Owner || ((PlayerMobile)m).Onshow || m_Owner.AccessLevel > m.AccessLevel ) 
						{
							from.CloseGump( typeof(MsgClientGump) ); 
							from.CloseGump( typeof(MsgGump) ); 
							from.SendGump( new MsgClientGump( from, m.NetState ) ); 
						} 
						else 
						{ 
							from.SendMessage( "You cannot see them." ); 
							from.SendGump( new MsgGump( from, m_Mobiles, m_Page ) ); 
						} 
					} 

					break; 
				} 
			} 
		} 
	} 
}