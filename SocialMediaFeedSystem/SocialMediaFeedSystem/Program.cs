using SocialMediaFeedSystem.DL;
using SocialMediaFeedSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            usersDL.LoadUsersFromDB();
            Application.Run(new Form1());
        }
    }
}
