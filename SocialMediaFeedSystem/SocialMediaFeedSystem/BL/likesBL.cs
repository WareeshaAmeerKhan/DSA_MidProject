using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class likesBL
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }

        public likesBL() { }

        public likesBL(int id, int post, int user)
        {
            LikeID = id;
            PostID = post;
            UserID = user;
        }
    }
}
