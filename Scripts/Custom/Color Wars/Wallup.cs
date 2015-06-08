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
public class wallupCommand
	{
		public static void Initialize()
		{
		
          CommandSystem.Register("wallup", AccessLevel.GameMaster, new CommandEventHandler(wallup_OnCommand));
		
		}
		
			
		[Usage( "wallup" )]
		[Description( "Raises the CWw arena walls." )]
		public static void wallup_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			
			Item cws = new CWwWallSouth();
			cws.MoveToWorld( new Point3D( 5915, 479, -22 ), Map.Felucca );
			
			Item cws2 = new CWwWallSouth();
            cws2.MoveToWorld(new Point3D(5916, 479, -22), Map.Felucca);
			
			Item cwn = new CWwWallSouth();
            cwn.MoveToWorld(new Point3D(5987, 415, -22), Map.Felucca);
			
			Item cwn2 = new CWwWallSouth();
            cwn2.MoveToWorld(new Point3D(5988, 415, -22), Map.Felucca);
			
			Item cwe = new CWwWallEast();
			cwe.MoveToWorld( new Point3D( 5919, 411, -22 ), Map.Felucca );
			
			Item cwe2 = new CWwWallEast();
            cwe2.MoveToWorld(new Point3D(5919, 412, -22), Map.Felucca);
			
			Item cww = new CWwWallEast();
			cww.MoveToWorld( new Point3D( 5983, 483, -22 ), Map.Felucca );
			
			Item cww2 = new CWwWallEast();
            cww2.MoveToWorld(new Point3D(5983, 484, -22), Map.Felucca);

			from.SendMessage("The CWw walls have been raised." );
		}	
		
		
	}

}
