using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SocialMediaFeedSystem.UI
{
    public partial class ResetPassword : Form
    {
        private bool passwordVisible = false;

        public ResetPassword()
        {
            InitializeComponent();
            passwordTB.PasswordChar = '•';
            emailTB.Text = "";
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string email = emailTB.Text;
                string password = passwordTB.Text;

                // Validate inputs
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both email and password!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!usersDL.IsValidPassword(password))
                {
                    MessageBox.Show("Password must be at least 8 characters long and include at least one letter and one number.",
                                  "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update password using the DL method
                if (usersDL.UpdatePasswordByEmail(email, password))
                {
                    MessageBox.Show("Password updated successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Failed to update password. Please try again.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ShowHide_Click(object sender, EventArgs e)
        {
            if (passwordVisible)
            {
                passwordTB.PasswordChar = '•';   // Hide password
                ShowHide.Image = Properties.Resources.hide;   // Hide icon
            }
            else
            {
                passwordTB.PasswordChar = '\0';  // Show password
                ShowHide.Image = Properties.Resources.show;   // Show icon
            }

            passwordVisible = !passwordVisible;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
