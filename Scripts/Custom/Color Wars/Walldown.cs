//Created by ashftw - ashley_bamforth@hotmail.com
//Downloaded from www.runuo.com

using Server.Commands;
using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
namespace Server.Commands
{
public class walldownCommand
	{
		public static void Initialize()
		{
          CommandSystem.Register("walldown", AccessLevel.GameMaster, new CommandEventHandler(walldown_OnCommand));
		
		}
		

			
		[Usage( "walldown" )]
		[Description( "Lowers the CWw arena walls." )]
		public static void walldown_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			
			ArrayList list = new ArrayList();
			
			foreach ( Item item in World.Items.Values )
			{
				if ( item is CWwWallSouth )
					list.Add( item );
				if ( item is CWwWallEast )
					list.Add( item );
			}
				foreach ( Item item in list )
				item.Delete();
				
			from.SendMessage("The CWw walls have been lowered." );				
		}
	}
}
