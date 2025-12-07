using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class PostWithAssociatedData
    {
        public postsBL Post { get; set; }
        public List<likesBL> Likes { get; set; }
        public List<commentsBL> Comments { get; set; }

        public PostWithAssociatedData()
        {
            Likes = new List<likesBL>();
            Comments = new List<commentsBL>();
        }
    }
}
