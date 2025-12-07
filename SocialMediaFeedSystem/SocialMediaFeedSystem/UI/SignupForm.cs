using SocialMediaFeedSystem;
using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DL;
using SocialMediaFeedSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SocialMediaFeedSystem.UI
{
    public partial class SignupForm : Form
    {
        private bool passwordVisible = false;
        string selectedImagePath = "";
        public SignupForm()
        {
            InitializeComponent();
            passwordTB.PasswordChar = '•';
            PfP.Visible = false;
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {
            // clicked by mistake
        }

        private void pfpBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Profile Picture";
            dlg.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg;*.jpeg;*.png;*.gif";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = dlg.FileName;
                PfP.Image = Image.FromFile(selectedImagePath);
                PfP.Visible = true;

                MessageBox.Show("Image uploaded successfully!");
            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernameTB.Text.Trim();
                string email = emailTB.Text.Trim();
                string password = passwordTB.Text.Trim();
                DateTime dob = dobPicker.Value;
                string bio = bioTB.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username, Email, and Password are required!", "Error");
                    return;
                }

                if (!usersDL.IsValidPassword(password))
                {
                    MessageBox.Show("Password must be at least 8 characters long and include at least one letter and one number.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Invalid Email!", "Error");
                    return;
                }

                // Check duplicate user in Linked List
                bool found = usersDL.CheckUserExists(username, email);

                if (found)
                {
                    MessageBox.Show("User already exists!", "Error");
                    return;
                }

                string imagePathForDB = string.IsNullOrEmpty(selectedImagePath) ? null : selectedImagePath;

                usersBL newUser = new usersBL(username, email, password, dob, bio, imagePathForDB);

                // Add to linked list
                if (usersDL.AddUserToDBAndList(newUser))
                {
                    MessageBox.Show("Account Created Successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to save user in database!");
                }
                LoginForm login = new LoginForm();
                login.StartPosition = FormStartPosition.Manual;
                login.Location = this.Location;

                this.Hide();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (passwordVisible)
            {
                passwordTB.PasswordChar = '•';   // Hide password
                pictureBox1.Image = Properties.Resources.hide;   // Hide icon
            }
            else
            {
                passwordTB.PasswordChar = '\0';  // Show password
                pictureBox1.Image = Properties.Resources.show;   // Show icon
            }

            passwordVisible = !passwordVisible;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void loginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;

            this.Hide();
            login.Show();
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
