using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class friendsBL
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string ProfilePicPath { get; set; } // Changed to string for file paths
        public int RequestID { get; set; } // For friend requests
        public DateTime FriendshipDate { get; set; } // Add this property

    }
}
