namespace SocialMediaFeedSystem.UI
{
    partial class Feed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Feed));
            this.HeadingLabel = new System.Windows.Forms.Label();
            this.FeedPanel = new System.Windows.Forms.Panel();
            this.SearchBtn = new System.Windows.Forms.PictureBox();
            this.SearchBar = new System.Windows.Forms.TextBox();
            this.SortPostLabel = new System.Windows.Forms.Label();
            this.SortByComboBox = new System.Windows.Forms.ComboBox();
            this.CommentsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.CommentInputPanel = new System.Windows.Forms.Panel();
            this.PostCommentBtn = new System.Windows.Forms.Button();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.cmntLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PostFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.NoFeedLabel = new System.Windows.Forms.Label();
            this.NoPostLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.LeftNavBar = new System.Windows.Forms.Panel();
            this.FriendsNav = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Back = new System.Windows.Forms.PictureBox();
            this.FeedNav = new System.Windows.Forms.PictureBox();
            this.ProfileNav = new System.Windows.Forms.PictureBox();
            this.HomeNav = new System.Windows.Forms.PictureBox();
            this.LogoutNav = new System.Windows.Forms.PictureBox();
            this.ClearSearchBtn = new System.Windows.Forms.PictureBox();
            this.FeedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchBtn)).BeginInit();
            this.CommentPanel.SuspendLayout();
            this.CommentInputPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.LeftNavBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FriendsNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FeedNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HomeNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoutNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClearSearchBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // HeadingLabel
            // 
            this.HeadingLabel.AutoSize = true;
            this.HeadingLabel.BackColor = System.Drawing.Color.Transparent;
            this.HeadingLabel.Font = new System.Drawing.Font("Monotype Corsiva", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.HeadingLabel.ForeColor = System.Drawing.Color.White;
            this.HeadingLabel.Location = new System.Drawing.Point(131, 61);
            this.HeadingLabel.Name = "HeadingLabel";
            this.HeadingLabel.Size = new System.Drawing.Size(177, 49);
            this.HeadingLabel.TabIndex = 36;
            this.HeadingLabel.Text = "Your Feed";
            // 
            // FeedPanel
            // 
            this.FeedPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FeedPanel.Controls.Add(this.ClearSearchBtn);
            this.FeedPanel.Controls.Add(this.SearchBtn);
            this.FeedPanel.Controls.Add(this.SearchBar);
            this.FeedPanel.Controls.Add(this.SortPostLabel);
            this.FeedPanel.Controls.Add(this.SortByComboBox);
            this.FeedPanel.Controls.Add(this.CommentsFlowPanel);
            this.FeedPanel.Controls.Add(this.CommentPanel);
            this.FeedPanel.Controls.Add(this.panel1);
            this.FeedPanel.Controls.Add(this.NoPostLabel);
            this.FeedPanel.Location = new System.Drawing.Point(129, 120);
            this.FeedPanel.Name = "FeedPanel";
            this.FeedPanel.Size = new System.Drawing.Size(862, 470);
            this.FeedPanel.TabIndex = 37;
            // 
            // SearchBtn
            // 
            this.SearchBtn.BackColor = System.Drawing.Color.White;
            this.SearchBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchBtn.BackgroundImage")));
            this.SearchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SearchBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchBtn.Location = new System.Drawing.Point(160, 9);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(24, 24);
            this.SearchBtn.TabIndex = 50;
            this.SearchBtn.TabStop = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // SearchBar
            // 
            this.SearchBar.Font = new System.Drawing.Font("Book Antiqua", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBar.Location = new System.Drawing.Point(10, 9);
            this.SearchBar.Name = "SearchBar";
            this.SearchBar.Size = new System.Drawing.Size(175, 24);
            this.SearchBar.TabIndex = 49;
            // 
            // SortPostLabel
            // 
            this.SortPostLabel.AutoSize = true;
            this.SortPostLabel.BackColor = System.Drawing.Color.Transparent;
            this.SortPostLabel.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Bold);
            this.SortPostLabel.ForeColor = System.Drawing.Color.White;
            this.SortPostLabel.Location = new System.Drawing.Point(212, 11);
            this.SortPostLabel.Name = "SortPostLabel";
            this.SortPostLabel.Size = new System.Drawing.Size(68, 21);
            this.SortPostLabel.TabIndex = 48;
            this.SortPostLabel.Text = "Sort By:";
            // 
            // SortByComboBox
            // 
            this.SortByComboBox.BackColor = System.Drawing.Color.Plum;
            this.SortByComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SortByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortByComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SortByComboBox.Font = new System.Drawing.Font("Book Antiqua", 10F);
            this.SortByComboBox.ItemHeight = 17;
            this.SortByComboBox.Location = new System.Drawing.Point(280, 9);
            this.SortByComboBox.Name = "SortByComboBox";
            this.SortByComboBox.Size = new System.Drawing.Size(178, 25);
            this.SortByComboBox.TabIndex = 47;
            // 
            // CommentsFlowPanel
            // 
            this.CommentsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentsFlowPanel.AutoScroll = true;
            this.CommentsFlowPanel.Location = new System.Drawing.Point(469, 74);
            this.CommentsFlowPanel.Name = "CommentsFlowPanel";
            this.CommentsFlowPanel.Size = new System.Drawing.Size(380, 275);
            this.CommentsFlowPanel.TabIndex = 0;
            // 
            // CommentPanel
            // 
            this.CommentPanel.Controls.Add(this.CommentInputPanel);
            this.CommentPanel.Controls.Add(this.cmntLabel);
            this.CommentPanel.Location = new System.Drawing.Point(469, 46);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(380, 419);
            this.CommentPanel.TabIndex = 40;
            // 
            // CommentInputPanel
            // 
            this.CommentInputPanel.AutoScroll = true;
            this.CommentInputPanel.Controls.Add(this.PostCommentBtn);
            this.CommentInputPanel.Controls.Add(this.CommentLabel);
            this.CommentInputPanel.Controls.Add(this.CommentTextBox);
            this.CommentInputPanel.Location = new System.Drawing.Point(1, 306);
            this.CommentInputPanel.Name = "CommentInputPanel";
            this.CommentInputPanel.Size = new System.Drawing.Size(377, 110);
            this.CommentInputPanel.TabIndex = 1;
            // 
            // PostCommentBtn
            // 
            this.PostCommentBtn.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostCommentBtn.Location = new System.Drawing.Point(264, 49);
            this.PostCommentBtn.Name = "PostCommentBtn";
            this.PostCommentBtn.Size = new System.Drawing.Size(93, 36);
            this.PostCommentBtn.TabIndex = 3;
            this.PostCommentBtn.Text = "Post ";
            this.PostCommentBtn.UseVisualStyleBackColor = true;
            this.PostCommentBtn.Click += new System.EventHandler(this.PostCommentBtn_Click_1);
            // 
            // CommentLabel
            // 
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentLabel.Location = new System.Drawing.Point(9, 8);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(193, 23);
            this.CommentLabel.TabIndex = 2;
            this.CommentLabel.Text = "Add New Comment:";
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentTextBox.Location = new System.Drawing.Point(12, 35);
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Size = new System.Drawing.Size(237, 69);
            this.CommentTextBox.TabIndex = 0;
            // 
            // cmntLabel
            // 
            this.cmntLabel.AutoSize = true;
            this.cmntLabel.BackColor = System.Drawing.Color.Transparent;
            this.cmntLabel.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmntLabel.ForeColor = System.Drawing.Color.White;
            this.cmntLabel.Location = new System.Drawing.Point(65, 198);
            this.cmntLabel.Name = "cmntLabel";
            this.cmntLabel.Size = new System.Drawing.Size(294, 28);
            this.cmntLabel.TabIndex = 0;
            this.cmntLabel.Text = "Check comments on post!";
            this.cmntLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PostFlowPanel);
            this.panel1.Controls.Add(this.NoFeedLabel);
            this.panel1.Location = new System.Drawing.Point(10, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 420);
            this.panel1.TabIndex = 39;
            // 
            // PostFlowPanel
            // 
            this.PostFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostFlowPanel.AutoScroll = true;
            this.PostFlowPanel.Location = new System.Drawing.Point(2, 4);
            this.PostFlowPanel.Name = "PostFlowPanel";
            this.PostFlowPanel.Size = new System.Drawing.Size(445, 413);
            this.PostFlowPanel.TabIndex = 52;
            // 
            // NoFeedLabel
            // 
            this.NoFeedLabel.AutoSize = true;
            this.NoFeedLabel.BackColor = System.Drawing.Color.Transparent;
            this.NoFeedLabel.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Bold);
            this.NoFeedLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.NoFeedLabel.Location = new System.Drawing.Point(128, 187);
            this.NoFeedLabel.Name = "NoFeedLabel";
            this.NoFeedLabel.Size = new System.Drawing.Size(203, 28);
            this.NoFeedLabel.TabIndex = 51;
            this.NoFeedLabel.Text = "Nothing to show!";
            this.NoFeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NoPostLabel
            // 
            this.NoPostLabel.AutoSize = true;
            this.NoPostLabel.BackColor = System.Drawing.Color.Transparent;
            this.NoPostLabel.Font = new System.Drawing.Font("Book Antiqua", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoPostLabel.ForeColor = System.Drawing.Color.Plum;
            this.NoPostLabel.Location = new System.Drawing.Point(280, 212);
            this.NoPostLabel.Name = "NoPostLabel";
            this.NoPostLabel.Size = new System.Drawing.Size(322, 26);
            this.NoPostLabel.TabIndex = 38;
            this.NoPostLabel.Text = "No posts yet! Share something...";
            this.NoPostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel2.TabIndex = 39;
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
            this.LeftNavBar.TabIndex = 38;
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
            this.label6.Location = new System.Drawing.Point(7, 287);
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
            this.HomeNav.Click += new System.EventHandler(this.HomeNav_Click);
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
            // ClearSearchBtn
            // 
            this.ClearSearchBtn.BackColor = System.Drawing.Color.White;
            this.ClearSearchBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearSearchBtn.BackgroundImage")));
            this.ClearSearchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClearSearchBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearSearchBtn.Location = new System.Drawing.Point(184, 9);
            this.ClearSearchBtn.Name = "ClearSearchBtn";
            this.ClearSearchBtn.Size = new System.Drawing.Size(24, 24);
            this.ClearSearchBtn.TabIndex = 51;
            this.ClearSearchBtn.TabStop = false;
            this.ClearSearchBtn.Click += new System.EventHandler(this.ClearSearchBtn_Click);
            // 
            // Feed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1045, 595);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LeftNavBar);
            this.Controls.Add(this.FeedPanel);
            this.Controls.Add(this.HeadingLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Feed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Feed";
            this.FeedPanel.ResumeLayout(false);
            this.FeedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchBtn)).EndInit();
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            this.CommentInputPanel.ResumeLayout(false);
            this.CommentInputPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.ClearSearchBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label HeadingLabel;
        private System.Windows.Forms.Panel FeedPanel;
        private System.Windows.Forms.Label NoPostLabel;
        private System.Windows.Forms.Panel CommentPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel CommentInputPanel;
        private System.Windows.Forms.FlowLayoutPanel CommentsFlowPanel;
        private System.Windows.Forms.TextBox CommentTextBox;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.Button PostCommentBtn;
        private System.Windows.Forms.Label cmntLabel;
        private System.Windows.Forms.PictureBox SearchBtn;
        private System.Windows.Forms.TextBox SearchBar;
        private System.Windows.Forms.Label SortPostLabel;
        private System.Windows.Forms.ComboBox SortByComboBox;
        private System.Windows.Forms.Label NoFeedLabel;
        private System.Windows.Forms.FlowLayoutPanel PostFlowPanel;
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
        private System.Windows.Forms.PictureBox ClearSearchBtn;
    }
}