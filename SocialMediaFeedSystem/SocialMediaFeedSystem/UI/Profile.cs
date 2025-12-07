using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.UI
{
    public partial class Profile : Form
    {
        private usersBL currentUser;
        private string originalUsername;
        private string originalPassword;  
        private string originalEmail;
        private DateTime originalDOB;
        private string originalBio;
        private string originalProfilePicPath;
        private bool passwordVisible = false;
        public Profile(usersBL user)
        {
            InitializeComponent();
            currentUser = user;
            passwordTB.PasswordChar = '•';
            InitializeUserData();
            DisableEditing();
        }

        private void InitializeUserData()
        {
            originalUsername = currentUser.Username;
            originalPassword = currentUser.PasswordHash;
            originalEmail = currentUser.Email;
            originalDOB = currentUser.DOB;
            originalBio = currentUser.Bio;
            originalProfilePicPath = currentUser.ProfilePicPath;

            usernameTB.Text = currentUser.Username;
            emailTB.Text = currentUser.Email;
            dobPicker.Value = currentUser.DOB;
            bioTB.Text = currentUser.Bio;

            // Load profile picture
            if (!string.IsNullOrEmpty(currentUser.ProfilePicPath) && System.IO.File.Exists(currentUser.ProfilePicPath))
            {
                PfPBox.Image = Image.FromFile(currentUser.ProfilePicPath);
            }
            else
            {
                PfPBox.Image = Properties.Resources.default_pfp;
            }
        }

        private void DisableEditing()
        {
            usernameTB.Enabled = false;
            passwordTB.Enabled = false;
            emailTB.Enabled = false;
            dobPicker.Enabled = false;
            bioTB.Enabled = false;
            saveBtn.Visible = false;
            discardBtn.Visible = false;
            saveBtn.Enabled = false;
            discardBtn.Enabled = false;
        }

        private void EnableEditing()
        {
            saveBtn.Visible = true;
            discardBtn.Visible = true;
            saveBtn.Enabled = true;
            discardBtn.Enabled = true;
        }

        private void discardBtn_Click(object sender, EventArgs e)
        {
            currentUser.Username = originalUsername;
            currentUser.Email = originalEmail;
            currentUser.DOB = originalDOB;
            currentUser.Bio = originalBio;
            currentUser.ProfilePicPath = originalProfilePicPath;

            InitializeUserData();
            DisableEditing();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usernameBtn_Click(object sender, EventArgs e)
        {
            usernameTB.Enabled = !usernameTB.Enabled;
            if (usernameTB.Enabled) EnableEditing();
        }

        private void emailBtn_Click(object sender, EventArgs e)
        {
            emailTB.Enabled = !emailTB.Enabled;
            if (emailTB.Enabled) EnableEditing();
        }

        private void dobBtn_Click(object sender, EventArgs e)
        {
            dobPicker.Enabled = !dobPicker.Enabled;
            if (dobPicker.Enabled) EnableEditing();
        }

        private void bioBtn_Click(object sender, EventArgs e)
        {
            bioTB.Enabled = !bioTB.Enabled;
            if (bioTB.Enabled) EnableEditing();
        }

        private void passwordBtn_Click(object sender, EventArgs e)
        {
            ResetPassword reset = new ResetPassword();
            reset.StartPosition = FormStartPosition.Manual;
            reset.Location = this.Location;
            reset.Show();
        }

        private void PfpBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Select Profile Picture";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PfPBox.Image = Image.FromFile(openFileDialog.FileName);
                        currentUser.ProfilePicPath = openFileDialog.FileName;
                        EnableEditing();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(usernameTB.Text))
                {
                    MessageBox.Show("Username cannot be empty!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(emailTB.Text) || !IsValidEmail(emailTB.Text))
                {
                    MessageBox.Show("Please enter a valid email address!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update user object
                currentUser.Username = usernameTB.Text;
                currentUser.Email = emailTB.Text;
                currentUser.DOB = dobPicker.Value;
                currentUser.Bio = bioTB.Text;

                // Update in memory list
                usersDL.UpdateUserInList(currentUser);

                // Update in database
                if (usersDL.UpdateUserInDatabase(currentUser))
                {
                    MessageBox.Show("Profile updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisableEditing();
                }
                else
                {
                    MessageBox.Show("Failed to update profile in database!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ViewProfile_Load(object sender, EventArgs e)
        {
            //clicked by mistake...
        }


        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.Instance.GoBack(this);
        }

        private void LogoutNav_Click(object sender, EventArgs e)
        {
            NavigationManager.ClearHistory();
            LoginForm view = new LoginForm();
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void FriendsNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Friends view = new Friends(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void FeedNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Feed view = new Feed(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void HomeNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Dashboard view = new Dashboard(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }
    }
}
