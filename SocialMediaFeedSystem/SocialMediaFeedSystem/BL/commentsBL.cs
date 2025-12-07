using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class commentsBL
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }

        public commentsBL() { }

        public commentsBL(int id, int postId, int userId, string content, DateTime date)
        {
            CommentID = id;
            PostID = postId;
            UserID = userId;
            Content = content;
            CommentDate = date;
        }
    }
}
