using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class postsBL
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }

        public postsBL() { }

        public postsBL(int postId, int userId, string content, DateTime postDate, int likes, int comments)
        {
            PostID = postId;
            UserID = userId;
            Content = content;
            PostDate = postDate;
            Likes = likes;
            Comments = comments;
        }
    }
}
