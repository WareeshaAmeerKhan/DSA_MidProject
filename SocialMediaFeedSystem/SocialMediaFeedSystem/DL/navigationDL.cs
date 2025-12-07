using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DL;
using SocialMediaFeedSystem.DataSturctures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocialMediaFeedSystem.UI;

namespace SocialMediaFeedSystem.DL
{
    public class NavigationManager
    {
        private static NavigationManager instance;
        private static NavigationStack navigationStack;

        public static NavigationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NavigationManager();
                }
                return instance;
            }
        }

        private NavigationManager()
        {
            navigationStack = new NavigationStack();
        }

        // Push current form info when navigating to new form
        public void PushForm(Form currentForm, usersBL currentUser)
        {
            NavigationItem item = new NavigationItem(
                currentForm.GetType(),
                currentUser,
                currentForm.Location
            );

            navigationStack.Push(item);

            // Debug output
            Console.WriteLine($"Pushed: {currentForm.GetType().Name}");
            navigationStack.DisplayStack();
        }

        // Pop and navigate back
        public void GoBack(Form currentForm)
        {
            if (navigationStack.IsEmpty())
            {
                // If stack is empty, exit application or show message
                if (MessageBox.Show("No previous page. Exit Application?", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                return;
            }

            // Pop the last navigation item
            NavigationItem previousItem = navigationStack.Pop();

            // Debug output
            Console.WriteLine($"Popped: {previousItem.FormType.Name}");
            navigationStack.DisplayStack();

            // Create and show the previous form
            Form previousForm = (Form)Activator.CreateInstance(previousItem.FormType, previousItem.CurrentUser);
            previousForm.StartPosition = FormStartPosition.Manual;
            previousForm.Location = previousItem.FormLocation;

            // Close current form and show previous one
            currentForm.Hide();
            previousForm.Show();
        }

        // Clear navigation history (e.g., on logout)
        public static void ClearHistory()
        {
            navigationStack.Clear();
        }
    }
}
