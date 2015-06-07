using System; 
using System.Net; 
using Server; 
using Server.Accounting; 
using Server.Network; 
using Server.Targets; 
using Server.Gumps; 
using Server.Mobiles;
using Server.Guilds;

namespace Server.Gumps
{
	public class MsgClientGump : Gump
	{
		private NetState m_State;

		private void Resend( Mobile to, RelayInfo info )
		{
			TextRelay te = info.GetTextEntry( 0 );

			to.SendGump( new MsgClientGump( to, m_State, te == null ? "" : te.Text ) );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_State == null )
				return;

			Mobile focus = m_State.Mobile;
			Mobile from = state.Mobile;

			if ( focus == null )
			{
				from.SendMessage( "That character is no longer online." );
				return;
			}
			else if ( focus.Deleted )
			{
				from.SendMessage( "That character no longer exists." );
				return;
			}

			switch ( info.ButtonID )
			{
				case 1: // Tell
				{
					TextRelay text = info.GetTextEntry( 0 );

					if ( text != null )
					{
						if ((((PlayerMobile)focus).IgnoreList.Contains ( from ) ) && ( from.AccessLevel == AccessLevel.Player ))
						{ 
							from.SendMessage( "That user has blocked you." ); 
						} 
						else
						{
							from.SendMessage( 0x482, "You told {0}:", focus.Name );
							from.SendMessage( 0x482, text.Text );
							focus.SendMessage( 0x482, "{0} tells you:", from.Name );
							focus.SendMessage( 0x482, text.Text );
							focus.CloseGump( typeof(MsgReplyGump) );
							focus.SendGump( new MsgReplyGump( from.NetState ) );
							
						}
					}

					from.SendGump( new MsgClientGump( from, m_State ) );

					break;
				}
				case 4: // Remove Ignore 
				{
					((PlayerMobile)from).IgnoreList.Remove ( focus );
					from.SendMessage ( "{0} will no longer be ignored", focus.Name );
					from.SendGump( new MsgClientGump( from, m_State ) );
					break;
				}
				case 5: // Add Ignore 
				{
					((PlayerMobile)from).IgnoreList.Add ( focus );
					from.SendMessage ( "{0} added to ignore list", focus.Name );
					from.SendGump( new MsgClientGump( from, m_State ) );
					break;
				}
				case 6: // GuildChat 
				{ 
					TextRelay text = info.GetTextEntry( 0 );
					Guild GuildC = from.Guild as Guild;

					if ( GuildC == null ) 
					{ 
						from.SendMessage( "You are not a member of any guild!" ); 
					} 

					else if ( text != null ) 
					{
						foreach ( NetState guild in NetState.Instances ) 
						{
							Mobile m = guild.Mobile;
							PlayerMobile pm = m as PlayerMobile; 
							if ( pm != null && GuildC.IsMember( pm ) && !(pm.IgnoreList.Contains ( from )) && !(pm.IgnoreList.Contains ( pm )) ) 
							{
								m.SendMessage( 75, "{0} tells the guild:", from.Name ); 
								m.SendMessage( 75, text.Text );  
							}
						} 
					}
					from.SendGump( new MsgClientGump( from, m_State ) );

					break;
				}
				case 7: // Enable Guild 
				{
					((PlayerMobile)from).IgnoreList.Remove ( focus );
					from.SendMessage ( "You will now recieve guild messages." );
					from.SendGump( new MsgClientGump( from, m_State ) );
					break;
				}
				case 8: // Block Guild 
				{
					((PlayerMobile)from).IgnoreList.Add ( focus );
					from.SendMessage ( "You will no longer recieve guild messages." );
					from.SendGump( new MsgClientGump( from, m_State ) );
					break;
				}
			} 
		}

		public MsgClientGump( Mobile from, NetState state ) : this( from, state, "" )
		{
		}

		private const int LabelColor32 = 0xFFFFFF;

		public string Center( string text )
		{
			return String.Format( "<CENTER>{0}</CENTER>", text );
		}

		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		public MsgClientGump( Mobile from, NetState state, string initialText ) : base( 50, 50 )
		{
			if ( from == null )
				return;
			if ( state == null )
				return;

			m_State = state;

			AddPage( 0 );

			AddBackground( 0, 0, 400, 204, 5054 );

			AddImageTiled( 10, 10, 380, 19, 0xA40 );
			AddAlphaRegion( 10, 10, 380, 19 );

			AddImageTiled( 10, 32, 380, 162, 0xA40 );
			AddAlphaRegion( 10, 32, 380, 162 );

			AddHtml( 10, 10, 380, 20, Color( Center( "Zulu Player Messenger" ), LabelColor32 ), false, false );

			int line = 0;

			Account a = state.Account as Account;
			Mobile m = state.Mobile;

			if ( m == from  && from.Guild != null )
			{
				Guild guild = from.Guild as Guild;

				AddHtml( 14, 36 + (line * 20), 160, 20, Color( "Guild:", LabelColor32 ), false, false );
				AddHtml( 50, 36 + (line++ * 20), 160, 20, Color( String.Format( "{0}", guild.Name ), LabelColor32 ), false, false );
				
				AddHtml( 14, 36 + (line * 20), 160, 20, Color( "Type:", LabelColor32 ), false, false );
				AddHtml( 50, 36 + (line++ * 20), 160, 20, Color( String.Format( "{0}", guild.Type ), LabelColor32 ), false, false );

				AddButton( 13, 165, 0xFAB, 0xFAD, 6, GumpButtonType.Reply, 0 );
				AddHtml( 48, 166, 200, 20, Color( "Send Guild Message", LabelColor32 ), false, false );

				AddImageTiled( 12, 82, 376, 80, 0xA40 );
				AddImageTiled( 13, 83, 374, 78, 0xBBC );
				AddTextEntry( 15, 83, 372, 78, 0x480, 0, "" );

				AddImageTiled( 205, 45, 182, 24, 5058 );

				AddImageTiled( 206, 46, 180, 22, 0xA40 );
				AddAlphaRegion( 206, 46, 180, 22 );

				line = 0;

				if ( ((PlayerMobile)from).IgnoreList.Contains ( m ) )
				{ 
					AddButton( 206, 46 + (line * 20), 0xFA5, 0xFA7, 7, GumpButtonType.Reply, 0 );
					AddHtml( 240, 48 + (line++ * 20), 140, 20, Color( "Enable Guild Messages", LabelColor32 ), false, false );
				}
				else
				{
					AddButton( 206, 46 + (line * 20), 0xFA5, 0xFA7, 8, GumpButtonType.Reply, 0 );
					AddHtml( 240, 48 + (line++ * 20), 140, 20, Color( "Disable Guild Messages", LabelColor32 ), false, false );
				}

			}

			if ( m != null && m != from )
			{
				Guild guild = m.Guild as Guild;

				AddHtml( 14, 36 + (line * 20), 160, 20, Color( "Name:", LabelColor32 ), false, false );
				AddHtml( 70, 36 + (line++ * 20), 160, 20, Color( String.Format( "{0}", m.Name ), LabelColor32 ), false, false );

				AddHtml( 14, 36 + (line * 20), 160, 20, Color( "Guild:", LabelColor32 ), false, false );
				
				if( guild == null )
					AddHtml( 70, 36 + (line++ * 20), 160, 20, Color( "None", LabelColor32 ), false, false );
				else
					AddHtml( 70, 36 + (line++ * 20), 160, 20, Color( String.Format( "{0}", guild.Name ), LabelColor32 ), false, false );
				
				AddButton( 13, 165, 0xFAB, 0xFAD, 1, GumpButtonType.Reply, 0 );
				AddHtml( 48, 166, 200, 20, Color( "Send Message", LabelColor32 ), false, false );

				AddImageTiled( 12, 82, 376, 80, 0xA40 );
				AddImageTiled( 13, 83, 374, 78, 0xBBC );
				AddTextEntry( 15, 83, 372, 78, 0x480, 0, "" );

				AddImageTiled( 205, 45, 182, 24, 5058 );

				AddImageTiled( 206, 46, 180, 22, 0xA40 );
				AddAlphaRegion( 206, 46, 180, 22 );

				line = 0;
				
				if ( ((PlayerMobile)from).IgnoreList.Contains ( m ) )
				{ 
					AddButton( 206, 46 + (line * 20), 0xFA5, 0xFA7, 4, GumpButtonType.Reply, 0 );
					AddHtml( 240, 48 + (line++ * 20), 140, 20, Color( "Remove From Ignore List", LabelColor32 ), false, false );
				}
				else
				{
					AddButton( 206, 46 + (line * 20), 0xFA5, 0xFA7, 5, GumpButtonType.Reply, 0 );
					AddHtml( 240, 48 + (line++ * 20), 140, 20, Color( "Add To Ignore List", LabelColor32 ), false, false );
				}
			}
		}
	}
}