using SocialMediaFeedSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            LoginForm view = new LoginForm();
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void Signup_Click(object sender, EventArgs e)
        {
            SignupForm view = new SignupForm();
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
