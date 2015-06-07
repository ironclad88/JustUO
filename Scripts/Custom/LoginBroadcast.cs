using System;
using System.Text;
using Server;
using Server.Commands;
/*
# Login/Logout/New Player Broadcast
# * Author: mordero
# * Email: mordero@gmail.com
# * Description: Will broadcast to current online players when someone has logged in/out. If the person who has logged in/out is above the player access level, it only broadcasts to the staff.
# * Description of Edit: When a new character is created, a message with their name, will be broadcasted to current online players. 
# * Additional Info: You may edit the New Player Message to your liking. Remember that the {0} denotes the player's name.
# * Installation: Just drag into your custom scripts folder.
# * Additional edits made by Orbit Storm to include a New Player Login Broadcast (and cleanup of wording for easier understanding).
# * All credit goes to mordero for developing this script and releasing it on RunUO. Please leave this header intact if you redistribute!
*/

namespace Server.Custom
{
    class LoginBroadcast
    {
        //{0} is the name of the player
        private readonly static string m_LoginMessage = "{0} has logged in.";//Login Message
        private readonly static string m_LogoutMessage = "{0} has logged out.";//Logout Message
        private readonly static int m_LoginHue = 0x482;//Login Message Hue
        private readonly static int m_LogoutHue = 0x482;//Logout Message Hue
        //maximum access level to announce
        private static AccessLevel m_AnnounceLevel = AccessLevel.Player;
        /// <summary>
        /// Subscribes to the login and out event
        /// </summary>
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(EventSink_Login);
            EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
        }
        /// <summary>
        /// On player logout, broadcast a message.
        /// </summary>
        public static void EventSink_Logout(LogoutEventArgs e)
        {
            if (e.Mobile.Player)
            {
                if (e.Mobile.AccessLevel <= m_AnnounceLevel)
                    CommandHandlers.BroadcastMessage(AccessLevel.Player, m_LogoutHue, String.Format(m_LogoutMessage, e.Mobile.Name));
                else //broadcast any other level to the staff
                    CommandHandlers.BroadcastMessage(AccessLevel.Counselor, m_LogoutHue, String.Format(m_LogoutMessage, e.Mobile.Name));
            }
        }
        /// <summary>
        /// On player login, broadcast a message.
        /// </summary>
        public static void EventSink_Login(LoginEventArgs e)
        {
            if (e.Mobile.Player)
            {
                if (e.Mobile.AccessLevel <= m_AnnounceLevel)
                    CommandHandlers.BroadcastMessage(AccessLevel.Player, m_LoginHue, String.Format(m_LoginMessage, e.Mobile.Name));
                else //broadcast any other level to the staff
                    CommandHandlers.BroadcastMessage(AccessLevel.Counselor, m_LoginHue, String.Format(m_LoginMessage, e.Mobile.Name));
            }
        }
	}
}