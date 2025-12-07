namespace SocialMediaFeedSystem.UI
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.PfPBox = new System.Windows.Forms.PictureBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.bioLabel = new System.Windows.Forms.Label();
            this.postsCount = new System.Windows.Forms.Label();
            this.friendsCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UndoBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.PostsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.NoPostLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contentTB = new System.Windows.Forms.RichTextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LeftNavBar = new System.Windows.Forms.Panel();
            this.FriendsNav = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Back = new System.Windows.Forms.PictureBox();
            this.FeedNav = new System.Windows.Forms.PictureBox();
            this.ProfileNav = new System.Windows.Forms.PictureBox();
            this.HomeNav = new System.Windows.Forms.PictureBox();
            this.LogoutNav = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PfPBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.LeftNavBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FriendsNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FeedNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HomeNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoutNav)).BeginInit();
            this.SuspendLayout();
            // 
            // PfPBox
            // 
            this.PfPBox.BackColor = System.Drawing.Color.Transparent;
            this.PfPBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PfPBox.Image = global::SocialMediaFeedSystem.Properties.Resources.default_pfp;
            this.PfPBox.InitialImage = null;
            this.PfPBox.Location = new System.Drawing.Point(123, 62);
            this.PfPBox.Name = "PfPBox";
            this.PfPBox.Size = new System.Drawing.Size(126, 120);
            this.PfPBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PfPBox.TabIndex = 18;
            this.PfPBox.TabStop = false;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Monotype Corsiva", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.White;
            this.usernameLabel.Location = new System.Drawing.Point(284, 62);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(160, 45);
            this.usernameLabel.TabIndex = 19;
            this.usernameLabel.Text = "Username";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bioLabel
            // 
            this.bioLabel.AutoSize = true;
            this.bioLabel.BackColor = System.Drawing.Color.Transparent;
            this.bioLabel.Font = new System.Drawing.Font("Book Antiqua", 14F);
            this.bioLabel.ForeColor = System.Drawing.Color.White;
            this.bioLabel.Location = new System.Drawing.Point(292, 116);
            this.bioLabel.Name = "bioLabel";
            this.bioLabel.Size = new System.Drawing.Size(168, 23);
            this.bioLabel.TabIndex = 20;
            this.bioLabel.Text = "Bio..........................";
            this.bioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // postsCount
            // 
            this.postsCount.AutoSize = true;
            this.postsCount.BackColor = System.Drawing.Color.Transparent;
            this.postsCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.postsCount.ForeColor = System.Drawing.Color.White;
            this.postsCount.Location = new System.Drawing.Point(333, 160);
            this.postsCount.Name = "postsCount";
            this.postsCount.Size = new System.Drawing.Size(17, 19);
            this.postsCount.TabIndex = 22;
            this.postsCount.Text = "0";
            this.postsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // friendsCount
            // 
            this.friendsCount.AutoSize = true;
            this.friendsCount.BackColor = System.Drawing.Color.Transparent;
            this.friendsCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.friendsCount.ForeColor = System.Drawing.Color.White;
            this.friendsCount.Location = new System.Drawing.Point(416, 160);
            this.friendsCount.Name = "friendsCount";
            this.friendsCount.Size = new System.Drawing.Size(17, 19);
            this.friendsCount.TabIndex = 24;
            this.friendsCount.Text = "0";
            this.friendsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(367, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 19);
            this.label5.TabIndex = 25;
            this.label5.Text = "friends";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(288, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = "posts";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.UndoBtn);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.PostsFlowPanel);
            this.panel1.Controls.Add(this.NoPostLabel);
            this.panel1.Location = new System.Drawing.Point(627, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 529);
            this.panel1.TabIndex = 31;
            // 
            // UndoBtn
            // 
            this.UndoBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UndoBtn.BackgroundImage")));
            this.UndoBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UndoBtn.Font = new System.Drawing.Font("Book Antiqua", 11F, System.Drawing.FontStyle.Bold);
            this.UndoBtn.ForeColor = System.Drawing.Color.White;
            this.UndoBtn.Location = new System.Drawing.Point(298, 27);
            this.UndoBtn.Name = "UndoBtn";
            this.UndoBtn.Size = new System.Drawing.Size(116, 41);
            this.UndoBtn.TabIndex = 37;
            this.UndoBtn.Text = "Undo Delete";
            this.UndoBtn.UseVisualStyleBackColor = true;
            this.UndoBtn.Click += new System.EventHandler(this.UndoBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Book Antiqua", 22F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(131, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 35);
            this.label7.TabIndex = 35;
            this.label7.Text = "Your Posts";
            // 
            // PostsFlowPanel
            // 
            this.PostsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostsFlowPanel.AutoScroll = true;
            this.PostsFlowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PostsFlowPanel.Location = new System.Drawing.Point(3, 70);
            this.PostsFlowPanel.Name = "PostsFlowPanel";
            this.PostsFlowPanel.Size = new System.Drawing.Size(410, 456);
            this.PostsFlowPanel.TabIndex = 34;
            // 
            // NoPostLabel
            // 
            this.NoPostLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NoPostLabel.AutoSize = true;
            this.NoPostLabel.BackColor = System.Drawing.Color.Transparent;
            this.NoPostLabel.Font = new System.Drawing.Font("Book Antiqua", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoPostLabel.ForeColor = System.Drawing.Color.Plum;
            this.NoPostLabel.Location = new System.Drawing.Point(55, 218);
            this.NoPostLabel.Name = "NoPostLabel";
            this.NoPostLabel.Size = new System.Drawing.Size(322, 26);
            this.NoPostLabel.TabIndex = 33;
            this.NoPostLabel.Text = "No posts yet! Share something...";
            this.NoPostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(36, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Create New Post:";
            // 
            // contentTB
            // 
            this.contentTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(200)))), ((int)(((byte)(240)))));
            this.contentTB.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentTB.Location = new System.Drawing.Point(41, 84);
            this.contentTB.Name = "contentTB";
            this.contentTB.Size = new System.Drawing.Size(357, 123);
            this.contentTB.TabIndex = 1;
            this.contentTB.Text = "";
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.Transparent;
            this.addBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("addBtn.BackgroundImage")));
            this.addBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addBtn.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(273, 235);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(125, 40);
            this.addBtn.TabIndex = 3;
            this.addBtn.Text = "Post";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.addBtn);
            this.panel3.Controls.Add(this.contentTB);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(90, 252);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(441, 294);
            this.panel3.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 53);
            this.panel2.TabIndex = 35;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // LeftNavBar
            // 
            this.LeftNavBar.BackColor = System.Drawing.Color.Black;
            this.LeftNavBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LeftNavBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LeftNavBar.Controls.Add(this.FriendsNav);
            this.LeftNavBar.Controls.Add(this.label6);
            this.LeftNavBar.Controls.Add(this.Back);
            this.LeftNavBar.Controls.Add(this.FeedNav);
            this.LeftNavBar.Controls.Add(this.ProfileNav);
            this.LeftNavBar.Controls.Add(this.HomeNav);
            this.LeftNavBar.Controls.Add(this.LogoutNav);
            this.LeftNavBar.ForeColor = System.Drawing.Color.White;
            this.LeftNavBar.Location = new System.Drawing.Point(-1, -3);
            this.LeftNavBar.Name = "LeftNavBar";
            this.LeftNavBar.Size = new System.Drawing.Size(75, 600);
            this.LeftNavBar.TabIndex = 34;
            // 
            // FriendsNav
            // 
            this.FriendsNav.BackColor = System.Drawing.Color.Transparent;
            this.FriendsNav.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FriendsNav.BackgroundImage")));
            this.FriendsNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FriendsNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FriendsNav.Location = new System.Drawing.Point(10, 348);
            this.FriendsNav.Name = "FriendsNav";
            this.FriendsNav.Size = new System.Drawing.Size(50, 50);
            this.FriendsNav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FriendsNav.TabIndex = 46;
            this.FriendsNav.TabStop = false;
            this.FriendsNav.Click += new System.EventHandler(this.FriendsNav_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "_______";
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Transparent;
            this.Back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Back.BackgroundImage")));
            this.Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back.Location = new System.Drawing.Point(15, 62);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(40, 40);
            this.Back.TabIndex = 40;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // FeedNav
            // 
            this.FeedNav.BackColor = System.Drawing.Color.Transparent;
            this.FeedNav.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FeedNav.BackgroundImage")));
            this.FeedNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FeedNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FeedNav.Location = new System.Drawing.Point(15, 252);
            this.FeedNav.Name = "FeedNav";
            this.FeedNav.Size = new System.Drawing.Size(40, 40);
            this.FeedNav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FeedNav.TabIndex = 44;
            this.FeedNav.TabStop = false;
            this.FeedNav.Click += new System.EventHandler(this.FeedNav_Click);
            // 
            // ProfileNav
            // 
            this.ProfileNav.BackColor = System.Drawing.Color.Transparent;
            this.ProfileNav.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProfileNav.BackgroundImage")));
            this.ProfileNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ProfileNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProfileNav.Location = new System.Drawing.Point(-3, 432);
            this.ProfileNav.Name = "ProfileNav";
            this.ProfileNav.Size = new System.Drawing.Size(76, 79);
            this.ProfileNav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProfileNav.TabIndex = 43;
            this.ProfileNav.TabStop = false;
            this.ProfileNav.Click += new System.EventHandler(this.ProfileNav_Click);
            // 
            // HomeNav
            // 
            this.HomeNav.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HomeNav.BackgroundImage")));
            this.HomeNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HomeNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HomeNav.Location = new System.Drawing.Point(16, 154);
            this.HomeNav.Name = "HomeNav";
            this.HomeNav.Size = new System.Drawing.Size(40, 40);
            this.HomeNav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.HomeNav.TabIndex = 42;
            this.HomeNav.TabStop = false;
            // 
            // LogoutNav
            // 
            this.LogoutNav.BackColor = System.Drawing.Color.Transparent;
            this.LogoutNav.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LogoutNav.BackgroundImage")));
            this.LogoutNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LogoutNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoutNav.Location = new System.Drawing.Point(16, 547);
            this.LogoutNav.Name = "LogoutNav";
            this.LogoutNav.Size = new System.Drawing.Size(40, 40);
            this.LogoutNav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoutNav.TabIndex = 38;
            this.LogoutNav.TabStop = false;
            this.LogoutNav.Click += new System.EventHandler(this.LogoutNav_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label13.Font = new System.Drawing.Font("Britannic Bold", 20F);
            this.label13.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label13.Location = new System.Drawing.Point(1005, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 30);
            this.label13.TabIndex = 36;
            this.label13.Text = "X";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1045, 595);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LeftNavBar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.friendsCount);
            this.Controls.Add(this.postsCount);
            this.Controls.Add(this.bioLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.PfPBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PfPBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.LeftNavBar.ResumeLayout(false);
            this.LeftNavBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FriendsNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FeedNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HomeNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoutNav)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PfPBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label bioLabel;
        private System.Windows.Forms.Label postsCount;
        private System.Windows.Forms.Label friendsCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label NoPostLabel;
        private System.Windows.Forms.FlowLayoutPanel PostsFlowPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button UndoBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox contentTB;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel LeftNavBar;
        private System.Windows.Forms.PictureBox FriendsNav;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.PictureBox FeedNav;
        private System.Windows.Forms.PictureBox ProfileNav;
        private System.Windows.Forms.PictureBox HomeNav;
        private System.Windows.Forms.PictureBox LogoutNav;
    }
}