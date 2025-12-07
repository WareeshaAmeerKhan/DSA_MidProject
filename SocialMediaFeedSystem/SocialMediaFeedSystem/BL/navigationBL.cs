using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class NavigationItem
    {
        public Type FormType { get; set; }
        public usersBL CurrentUser { get; set; }
        public Point FormLocation { get; set; }
        public DateTime Timestamp { get; set; }

        public NavigationItem(Type formType, usersBL user, Point location)
        {
            FormType = formType;
            CurrentUser = user;
            FormLocation = location;
            Timestamp = DateTime.Now;
        }
    }
}
