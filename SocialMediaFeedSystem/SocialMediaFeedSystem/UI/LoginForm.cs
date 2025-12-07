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
using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DL;

namespace SocialMediaFeedSystem.UI
{
    public partial class LoginForm : Form
    {
        private bool passwordVisible = false;
        public LoginForm()
        {
            InitializeComponent();
            passwordTB.PasswordChar = '•';
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPassword forget = new ResetPassword();
            forget.StartPosition = FormStartPosition.Manual;
            forget.Location = this.Location;
            forget.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignupForm signup = new SignupForm();
            signup.StartPosition = FormStartPosition.Manual;
            signup.Location = this.Location;

            this.Hide();
            signup.Show();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordTB.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            // LOGIN USING LIST
            usersBL user = usersDL.LoginUserFromList(username);

            if (user == null)
            {
                MessageBox.Show("User not found!");
                return;
            }

            // Password verification
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                MessageBox.Show("Incorrect Password!");
                return;
            }

            // Successful
            MessageBox.Show("Login Successful!");

            Dashboard dashboard = new Dashboard(user);
            dashboard.StartPosition = FormStartPosition.Manual;
            dashboard.Location = this.Location;

            this.Hide();
            dashboard.Show();
        }

        private void ShowHide_Click(object sender, EventArgs e)
        {
            if (passwordVisible)
            {
                passwordTB.PasswordChar = '•';
                ShowHide.Image = Properties.Resources.hide;
            }
            else
            {
                passwordTB.PasswordChar = '\0';
                ShowHide.Image = Properties.Resources.show;
            }
            passwordVisible = !passwordVisible;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainPage_Click(object sender, EventArgs e)
        {
            Form1 dashboard = new Form1();
            dashboard.StartPosition = FormStartPosition.Manual;
            dashboard.Location = this.Location;

            this.Hide();
            dashboard.Show();
        }
    }
}