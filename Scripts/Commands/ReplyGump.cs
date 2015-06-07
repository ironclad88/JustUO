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
	public class MsgReplyGump : Gump
	{
		private NetState m_State;

		private const int LabelColor32 = 0xFFFFFF;

		public string Center( string text )
		{
			return String.Format( "<CENTER>{0}</CENTER>", text );
		}

		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		public MsgReplyGump( NetState state ) : base( 10, 30 )
		{
			if ( state == null )
				return;
			
			m_State = state;
			Mobile mobile = m_State.Mobile;

			AddPage( 0 );

			AddBackground( 420, 10, ( 98 + (mobile.Name.Length * 8) ), 39, 5054 );
			AddImageTiled( 425, 15, ( 87 + (mobile.Name.Length * 8) ), 29, 0xA40 );
			AddAlphaRegion( 425, 15,( 87 + (mobile.Name.Length * 8) ), 29 );

			AddButton( 426, 19, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0 );
			AddHtml( 460, 20, 200, 20, Color( String.Format( "Reply To {0}", mobile.Name ), LabelColor32 ) , false, false );

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
				case 1: // Reply
				{
					from.CloseGump( typeof(MsgReplyGump) );
					from.CloseGump( typeof(MsgClientGump) ); 
					from.CloseGump( typeof(MsgGump) ); 
					from.SendGump( new MsgClientGump( from, focus.NetState ) );

					break;
				}
			} 
		}
	}
}