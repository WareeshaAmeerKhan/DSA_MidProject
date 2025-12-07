using MySql.Data.MySqlClient;
using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DataSturctures;
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

namespace SocialMediaFeedSystem.UI
{
    public partial class Dashboard : Form
    {
        private usersBL currentUser;
        private PostQueue postQueue = new PostQueue();
        private postsDL postDl = new postsDL();
        private commentsDL commentDl = new commentsDL();
        private likesDL likeDl = new likesDL();

        // Comments panel controls
        private Panel commentsPanel;
        private FlowLayoutPanel commentsFlowPanel;
        private TextBox commentTextBox;
        private Button postCommentBtn;
        private Label commentsTitle;
        private Button closeCommentsBtn;
        private int currentPostID = -1;

        public Dashboard(usersBL user)
        {
            InitializeComponent();
            currentUser = user;
            NoPostLabel.Visible = false;
            PostsFlowPanel.Visible = false;
            InitializeCommentsPanel();
        }

        private void ClearDeleteStackOnOperations()
        {
            // This method will be called whenever new operations are performed
            deleteStack.Clear();

            // Also exit any active edit modes
            ExitAllEditModes();
        }

        private void ExitAllEditModes()
        {
            foreach (Control control in PostsFlowPanel.Controls)
            {
                if (control is Panel card)
                {
                    // Check if this card is in edit mode
                    TextBox txtContent = card.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtContent");
                    if (txtContent != null)
                    {
                        // Find the post associated with this card
                        int postID = (int)card.Tag;
                        postsBL post = GetPostFromQueue(postID);
                        if (post != null)
                        {
                            // Switch back to view mode without saving
                            SwitchToViewMode(card, post);
                        }
                    }
                }
            }
        }

        private postsBL GetPostFromQueue(int postID)
        {
            PostNode current = postQueue.Front;
            while (current != null)
            {
                if (current.Data.PostID == postID)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return null;
        }

        private void InitializeCommentsPanel()
        {
            // Main comments panel
            commentsPanel = new Panel();
            commentsPanel.Size = new Size(350, 300);
            commentsPanel.BackColor = Color.Plum;
            commentsPanel.BorderStyle = BorderStyle.FixedSingle;
            commentsPanel.Visible = false;
            commentsPanel.Padding = new Padding(10);

            // Title
            commentsTitle = new Label();
            commentsTitle.Text = "Comments";
            commentsTitle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            commentsTitle.Location = new Point(10, 10);
            commentsTitle.Size = new Size(200, 20);

            // Close button
            closeCommentsBtn = new Button();
            closeCommentsBtn.Text = "✕";
            closeCommentsBtn.Size = new Size(25, 25);
            closeCommentsBtn.Location = new Point(315, 5);
            closeCommentsBtn.Click += (s, e) => commentsPanel.Visible = false;

            // Comments flow panel
            commentsFlowPanel = new FlowLayoutPanel();
            commentsFlowPanel.Location = new Point(10, 40);
            commentsFlowPanel.Size = new Size(330, 180);
            commentsFlowPanel.AutoScroll = true;
            commentsFlowPanel.BackColor = Color.FromArgb(240, 200, 240);
            commentsFlowPanel.BorderStyle = BorderStyle.FixedSingle;

            // Add comment section
            Panel addCommentPanel = new Panel();
            addCommentPanel.Location = new Point(10, 230);
            addCommentPanel.Size = new Size(330, 60);

            commentTextBox = new TextBox();
            commentTextBox.Location = new Point(0, 5);
            commentTextBox.Size = new Size(240, 40);
            commentTextBox.BackColor = Color.FromArgb(240, 200, 240);
            commentTextBox.Multiline = true;
            commentTextBox.ScrollBars = ScrollBars.Vertical;

            postCommentBtn = new Button();
            postCommentBtn.Text = "Post";
            postCommentBtn.Location = new Point(245, 5);
            postCommentBtn.BackColor = Color.Purple;
            postCommentBtn.ForeColor = Color.White;
            postCommentBtn.Size = new Size(80, 40);
            postCommentBtn.Click += PostCommentBtn_Click;

            addCommentPanel.Controls.Add(commentTextBox);
            addCommentPanel.Controls.Add(postCommentBtn);

            // Add controls to comments panel
            commentsPanel.Controls.Add(commentsTitle);
            commentsPanel.Controls.Add(closeCommentsBtn);
            commentsPanel.Controls.Add(commentsFlowPanel);
            commentsPanel.Controls.Add(addCommentPanel);

            // Add comments panel to form
            this.Controls.Add(commentsPanel);
            commentsPanel.BringToFront();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadUserData();
            LoadPosts();
            LoadFriendsCount(); 
        }

        private void LoadUserData()
        {
            usernameLabel.Text = currentUser.Username;
            bioLabel.Text = currentUser.Bio;

            if (!string.IsNullOrEmpty(currentUser.ProfilePicPath) && System.IO.File.Exists(currentUser.ProfilePicPath))
            {
                PfPBox.Image = Image.FromFile(currentUser.ProfilePicPath);
            }
            else
            {
                PfPBox.Image = Properties.Resources.default_pfp;
            }
        }

        private void LoadPosts()
        {
            postQueue = new PostQueue();
            DataTable dt = postDl.GetPostsByUser(currentUser.UserID);

            foreach (DataRow row in dt.Rows)
            {
                int postID = Convert.ToInt32(row["PostID"]);
                postsBL post = new postsBL
                {
                    PostID = postID,
                    UserID = Convert.ToInt32(row["UserID"]),
                    Content = row["Content"].ToString(),
                    PostDate = Convert.ToDateTime(row["PostDate"]),
                    Likes = likesDL.GetLikeCountForPost(postID), // Get from stack
                    Comments = Convert.ToInt32(row["Comments"])
                };
                postQueue.InsertInOrder(post);
            }
            DisplayPosts();
            UpdatePostCount();
        }

        private void DisplayPosts()
        {
            PostsFlowPanel.Controls.Clear();
            PostNode temp = postQueue.Front;

            while (temp != null)
            {
                PostsFlowPanel.Controls.Add(CreatePostCard(temp.Data));
                temp = temp.Next;
            }
            UpdatePostVisibility();
        }

        private void UpdatePostCount()
        {
            int postCount = 0;
            PostNode temp = postQueue.Front;
            while (temp != null)
            {
                postCount++;
                temp = temp.Next;
            }
            postsCount.Text = postCount.ToString();
            UpdatePostVisibility();
        }

        private void LoadFriendsCount()
        {
            try
            {
                int friendCount = friendDL.GetFriendCount(currentUser.UserID);
                friendsCount.Text = friendCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friends count: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                friendsCount.Text = "0";
            }
        }

        private void UpdatePostVisibility()
        {
            bool hasPosts = PostsFlowPanel.Controls.Count > 0;
            NoPostLabel.Visible = !hasPosts;
            PostsFlowPanel.Visible = hasPosts;
        }

        private Panel CreatePostCard(postsBL post)
        {
            Panel card = new Panel();
            card.Width = 385;
            card.Height = 120;
            card.BackColor = Color.FromArgb(240, 200, 240);
            card.Margin = new Padding(10);
            card.Padding = new Padding(10);
            card.Tag = post.PostID;

            // Content label (will be replaced with textbox in edit mode)
            Label lblContent = new Label();
            lblContent.Text = post.Content;
            lblContent.Font = new Font("Times New Roman", 12);
            lblContent.AutoSize = false;
            lblContent.Width = 320;
            lblContent.Height = 60;
            lblContent.Top = 5;
            lblContent.Left = 5;
            lblContent.Name = "lblContent"; // Name for easy access

            // Date label
            Label lblDate = new Label();
            lblDate.Text = post.PostDate.ToString("dd MMM yyyy • hh:mm tt");
            lblDate.Font = new Font("Times New Roman", 10, FontStyle.Italic);
            lblDate.ForeColor = Color.Purple;
            lblDate.Top = 70;
            lblDate.Left = 5;
            lblDate.AutoSize = true;

            // Like functionality
            PictureBox pbLike = new PictureBox();
            pbLike.Image = likesDL.HasUserLikedPost(currentUser.UserID, post.PostID) ?
                Properties.Resources.Liked : Properties.Resources.Like; // Check from stack
            pbLike.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLike.Size = new Size(25, 25);
            pbLike.Top = 90;
            pbLike.Left = 5;
            pbLike.Cursor = Cursors.Hand;
            pbLike.Tag = post.PostID;
            pbLike.Click += PbLike_Click;

            Label lblLikes = new Label();
            lblLikes.Text = likesDL.GetLikeCountForPost(post.PostID).ToString(); // Get from stack
            lblLikes.Font = new Font("Times New Roman", 9, FontStyle.Bold);
            lblLikes.ForeColor = Color.DarkBlue;
            lblLikes.Top = 93;
            lblLikes.Left = 35;
            lblLikes.AutoSize = true;
            lblLikes.Tag = post.PostID;

            // Comment functionality
            PictureBox pbComment = new PictureBox();
            pbComment.Image = Properties.Resources.Comment;
            pbComment.SizeMode = PictureBoxSizeMode.StretchImage;
            pbComment.Size = new Size(25, 25);
            pbComment.Top = 90;
            pbComment.Left = 70;
            pbComment.Cursor = Cursors.Hand;
            pbComment.Tag = post.PostID;
            pbComment.Click += PbComment_Click;

            Label lblComments = new Label();
            lblComments.Text = post.Comments.ToString();
            lblComments.Font = new Font("Times New Roman", 9, FontStyle.Bold);
            lblComments.ForeColor = Color.DarkBlue;
            lblComments.Top = 93;
            lblComments.Left = 100;
            lblComments.AutoSize = true;
            lblComments.Cursor = Cursors.Hand;
            lblComments.Tag = post.PostID;
            lblComments.Click += LblComments_Click;

            // Edit button (Pencil icon)
            PictureBox pbEdit = new PictureBox();
            pbEdit.Image = Properties.Resources.pen; // Make sure to add this image to your resources
            pbEdit.SizeMode = PictureBoxSizeMode.StretchImage;
            pbEdit.Size = new Size(20, 20);
            pbEdit.Top = 5;
            pbEdit.Left = card.Width - 55; // Position left of delete button
            pbEdit.Cursor = Cursors.Hand;
            pbEdit.Tag = post; // Store the post object
            pbEdit.Click += PbEdit_Click;

            // Delete button
            PictureBox pbDelete = new PictureBox();
            pbDelete.Image = Properties.Resources.Delete;
            pbDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDelete.Size = new Size(20, 20);
            pbDelete.Top = 5;
            pbDelete.Left = card.Width - 30;
            pbDelete.Cursor = Cursors.Hand;
            pbDelete.Tag = post;
            pbDelete.Click += PbDelete_Click;

            // Save label (initially hidden, for saving edits)
            Label lblSave = new Label();
            lblSave.Text = "Save";
            lblSave.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            lblSave.ForeColor = Color.Navy;
            lblSave.BackColor = Color.Plum;
            lblSave.Size = new Size(40, 20);
            lblSave.Top = 90;
            lblSave.Left = card.Width - 45; // Bottom right corner
            lblSave.TextAlign = ContentAlignment.MiddleCenter;
            lblSave.Cursor = Cursors.Hand;
            lblSave.Tag = post; // Store the post object
            lblSave.Visible = false; // Initially hidden
            lblSave.Click += LblSave_Click;

            // Add border to make it look like a button
            lblSave.BorderStyle = BorderStyle.FixedSingle;

            card.Controls.AddRange(new Control[] {
    lblContent, lblDate, pbLike, lblLikes, pbComment, lblComments,
    pbEdit, pbDelete, lblSave  // Changed from pbTick to lblSave
});
            return card;
        }

        private void PbEdit_Click(object sender, EventArgs e)
        {
            // Clear delete stack on edit operation
            ClearDeleteStackOnOperations();

            PictureBox pbEdit = (PictureBox)sender;
            postsBL postToEdit = (postsBL)pbEdit.Tag;
            Panel card = (Panel)pbEdit.Parent;

            // Switch to edit mode
            SwitchToEditMode(card, postToEdit);
        }

        private void LblSave_Click(object sender, EventArgs e)
        {
            Label lblSave = (Label)sender;
            postsBL postToUpdate = (postsBL)lblSave.Tag;
            Panel card = (Panel)lblSave.Parent;

            // Save the edited content
            SaveEditedPost(card, postToUpdate);
        }

        private void SwitchToEditMode(Panel card, postsBL post)
        {
            // Find the content label
            Label lblContent = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblContent");
            if (lblContent == null) return;

            // Create a textbox for editing
            TextBox txtContent = new TextBox();
            txtContent.Text = post.Content;
            txtContent.Font = new Font("Times New Roman", 12);
            txtContent.Multiline = true;
            txtContent.Size = new Size(320, 60);
            txtContent.Location = lblContent.Location;
            txtContent.BackColor = Color.FromArgb(240, 200, 240);
            txtContent.BorderStyle = BorderStyle.FixedSingle;
            txtContent.Name = "txtContent";
            txtContent.Tag = lblContent; // Store reference to original label

            // Hide the label and show the textbox
            lblContent.Visible = false;
            card.Controls.Add(txtContent);
            txtContent.Focus();
            txtContent.SelectAll();

            // Hide edit and delete buttons, show save label
            PictureBox editPb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.pen);
            if (editPb != null)
                editPb.Hide();

            PictureBox deletePb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.Delete);
            if (deletePb != null)
                deletePb.Hide();

            Label saveLabel = card.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text == "Save");
            if (saveLabel != null)
                saveLabel.Show();

            // Disable like and comment functionality during edit
            PictureBox likePb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.Like || pb.Image == Properties.Resources.Liked);
            if (likePb != null)
                likePb.Enabled = false;

            PictureBox commentPb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.Comment);
            if (commentPb != null)
                commentPb.Enabled = false;
        }

        private void SaveEditedPost(Panel card, postsBL post)
        {
            // Find the textbox
            TextBox txtContent = card.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtContent");
            if (txtContent == null) return;

            string newContent = txtContent.Text.Trim();

            if (string.IsNullOrEmpty(newContent))
            {
                MessageBox.Show("Post content cannot be empty!");
                return;
            }

            // Update post content
            post.Content = newContent;

            // Update in database
            if (postDl.UpdatePost(post))
            {
                // Update in queue
                UpdatePostInQueue(post);

                // Switch back to view mode
                SwitchToViewMode(card, post);

                MessageBox.Show("Post updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update post!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SwitchToViewMode(Panel card, postsBL post)
        {
            // Find the textbox and original label
            TextBox txtContent = card.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtContent");
            Label lblContent = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblContent");

            if (txtContent != null && lblContent != null)
            {
                // Update the label with new content
                lblContent.Text = post.Content;
                lblContent.Visible = true;

                // Remove the textbox
                card.Controls.Remove(txtContent);
                txtContent.Dispose();
            }

            // Show edit and delete buttons, hide save label
            PictureBox editPb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.pen);
            if (editPb != null)
                editPb.Show();

            PictureBox deletePb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.Delete);
            if (deletePb != null)
                deletePb.Show();

            Label saveLabel = card.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text == "Save");
            if (saveLabel != null)
                saveLabel.Hide();

            // Re-enable like and comment functionality
            PictureBox likePb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.Like || pb.Image == Properties.Resources.Liked);
            if (likePb != null)
                likePb.Enabled = true;

            PictureBox commentPb = card.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Image == Properties.Resources.Comment);
            if (commentPb != null)
                commentPb.Enabled = true;
        }

        private void UpdatePostInQueue(postsBL updatedPost)
        {
            PostNode current = postQueue.Front;
            while (current != null)
            {
                if (current.Data.PostID == updatedPost.PostID)
                {
                    current.Data.Content = updatedPost.Content;
                    break;
                }
                current = current.Next;
            }
        }

        private void PbDelete_Click(object sender, EventArgs e)
        {
            PictureBox pbDelete = (PictureBox)sender;
            postsBL postToDelete = (postsBL)pbDelete.Tag;

            // Show confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to delete this post?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Store complete post data for undo BEFORE deleting
                PostWithAssociatedData postData = new PostWithAssociatedData
                {
                    Post = postToDelete,
                    Likes = likesDL.GetLikesForPost(postToDelete.PostID),
                    Comments = commentDl.GetCommentsForPost(postToDelete.PostID)
                };

                // Delete post from database (with cascading deletes)
                if (postDl.DeletePost(postToDelete.PostID))
                {
                    // Push to static delete stack
                    deleteStack.Push(postData);

                    // Remove from queue
                    postQueue.RemovePost(postToDelete.PostID);

                    // Update UI
                    DisplayPosts();
                    UpdatePostCount();

                    // Show undo message
                    MessageBox.Show("Post deleted successfully! Click Undo to restore.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete post!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            // Clear delete stack on add post operation
            ClearDeleteStackOnOperations();

            string content = contentTB.Text.Trim();

            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Write something first!");
                return;
            }

            postsBL newPost = new postsBL
            {
                UserID = currentUser.UserID,
                Content = content,
                PostDate = DateTime.Now,
                Likes = 0,
                Comments = 0
            };

            if (postDl.InsertPost(newPost))
            {
                LoadPosts();
                contentTB.Text = "";
            }
            else
            {
                MessageBox.Show("Failed to save post in database!");
            }
        }

        private void PbLike_Click(object sender, EventArgs e)
        {
            // Clear delete stack on like operation
            ClearDeleteStackOnOperations();

            PictureBox pbLike = (PictureBox)sender;
            int postID = (int)pbLike.Tag;

            if (likesDL.HasUserLikedPost(currentUser.UserID, postID))
            {
                // Unlike the post - operation happens on stack first
                if (likesDL.RemoveLike(currentUser.UserID, postID))
                {
                    pbLike.Image = Properties.Resources.Like;
                    UpdateLikeCount(postID);
                    LoadPosts(); // Refresh to update all counts
                }
            }
            else
            {
                // Like the post - operation happens on stack first
                likesBL newLike = new likesBL
                {
                    PostID = postID,
                    UserID = currentUser.UserID
                };

                if (likesDL.AddLike(newLike))
                {
                    pbLike.Image = Properties.Resources.Liked;
                    UpdateLikeCount(postID);
                    LoadPosts(); // Refresh to update all counts
                }
            }
        }

        private void PbComment_Click(object sender, EventArgs e)
        {
            PictureBox pbComment = (PictureBox)sender;
            currentPostID = (int)pbComment.Tag;

            // Position comments panel
            Point screenPos = pbComment.PointToScreen(Point.Empty);
            Point formPos = this.PointToClient(screenPos);
            commentsPanel.Location = new Point(
                Math.Max(10, Math.Min(formPos.X, this.Width - commentsPanel.Width - 10)),
                Math.Max(10, Math.Min(formPos.Y - commentsPanel.Height, this.Height - commentsPanel.Height - 10))
            );

            LoadCommentsForPost(currentPostID);
            commentsPanel.Visible = true;
            commentTextBox.Focus();
        }

        private void UpdateLikeCount(int postID)
        {
            foreach (Control control in PostsFlowPanel.Controls)
            {
                if (control is Panel card && (int)card.Tag == postID)
                {
                    foreach (Control cardControl in card.Controls)
                    {
                        if (cardControl is Label label && label.Tag != null && (int)label.Tag == postID)
                        {
                            // Update the like count label from stack
                            label.Text = likesDL.GetLikeCountForPost(postID).ToString();
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void LblComments_Click(object sender, EventArgs e)
        {
            Label lblComments = (Label)sender;
            currentPostID = (int)lblComments.Tag;

            // Position comments panel
            Point screenPos = lblComments.PointToScreen(Point.Empty);
            Point formPos = this.PointToClient(screenPos);
            commentsPanel.Location = new Point(
                Math.Max(10, Math.Min(formPos.X, this.Width - commentsPanel.Width - 10)),
                Math.Max(10, Math.Min(formPos.Y - commentsPanel.Height, this.Height - commentsPanel.Height - 10))
            );

            LoadCommentsForPost(currentPostID);
            commentsPanel.Visible = true;
            commentTextBox.Focus();
        }

        private void LoadCommentsForPost(int postID)
        {
            commentsFlowPanel.Controls.Clear();

            CommentQueue commentQueue = commentDl.GetCommentsByPostAsQueue(postID);

            if (commentQueue.IsEmpty())
            {
                Label noCommentsLabel = new Label();
                noCommentsLabel.Text = "No comments. Be the first!";
                noCommentsLabel.Font = new Font("Times New Roman", 12, FontStyle.Italic);
                noCommentsLabel.ForeColor = Color.Gray;
                noCommentsLabel.AutoSize = true;
                noCommentsLabel.Margin = new Padding(5);
                commentsFlowPanel.Controls.Add(noCommentsLabel);
            }
            else
            {
                CommentNode temp = commentQueue.Front;
                while (temp != null)
                {
                    commentsFlowPanel.Controls.Add(CreateCommentPanel(temp.Data));
                    temp = temp.Next;
                }
            }

            commentsTitle.Text = $"Comments ({commentQueue.Count()})";
        }

        private Panel CreateCommentPanel(commentsBL comment)
        {
            Panel commentPanel = new Panel();
            commentPanel.Width = 305;
            commentPanel.Height = 60;
            commentPanel.Margin = new Padding(5);
            commentPanel.BackColor = Color.FromArgb(240, 200, 240);
            commentPanel.BorderStyle = BorderStyle.Fixed3D;
            commentPanel.Padding = new Padding(5);

            string username = GetUsernameByUserID(comment.UserID);

            Label userLabel = new Label();
            userLabel.Text = username;
            userLabel.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            userLabel.Location = new Point(5, 5);
            userLabel.AutoSize = true;

            Label contentLabel = new Label();
            contentLabel.Text = comment.Content;
            contentLabel.Font = new Font("Times New Roman", 9);
            contentLabel.Location = new Point(5, 25);
            contentLabel.Size = new Size(280, 30);
            contentLabel.AutoSize = false;

            Label dateLabel = new Label();
            dateLabel.Text = comment.CommentDate.ToString("dd MMM • hh:mm tt");
            dateLabel.Font = new Font("Times New Roman", 8);
            dateLabel.ForeColor = Color.Gray;
            dateLabel.Location = new Point(200, 5);
            dateLabel.AutoSize = true;

            commentPanel.Controls.AddRange(new Control[] { userLabel, contentLabel, dateLabel });
            return commentPanel;
        }

        private string GetUsernameByUserID(int userID)
        {
            usersBL user = usersDL.UsersList.FindUserByID(userID);
            return user != null ? user.Username : "Unknown User";
        }

        private void PostCommentBtn_Click(object sender, EventArgs e)
        {
            // Clear delete stack on comment operation
            ClearDeleteStackOnOperations();

            if (currentPostID == -1) return;

            string commentText = commentTextBox.Text.Trim();

            if (string.IsNullOrEmpty(commentText))
            {
                MessageBox.Show("Please write a comment first!");
                return;
            }

            commentsBL newComment = new commentsBL
            {
                PostID = currentPostID,
                UserID = currentUser.UserID,
                Content = commentText,
                CommentDate = DateTime.Now
            };

            if (commentDl.InsertComment(newComment))
            {
                UpdatePostCommentsCount(currentPostID);
                LoadCommentsForPost(currentPostID);
                commentTextBox.Text = "";
                LoadPosts(); // Refresh to update comments count
                MessageBox.Show("Comment added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add comment!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UpdatePostCommentsCount(int postID)
        {
            try
            {
                CommentQueue commentQueue = commentDl.GetCommentsByPostAsQueue(postID);
                int commentsCount = commentQueue.Count();

                using (MySql.Data.MySqlClient.MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "UPDATE posts SET Comments = @comments WHERE PostID = @postID";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@comments", commentsCount);
                    cmd.Parameters.AddWithValue("@postID", postID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating comments count: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UndoBtn_Click(object sender, EventArgs e)
        {
            if (deleteStack.IsEmpty())
            {
                MessageBox.Show("No posts to undo!", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PostWithAssociatedData postData = deleteStack.Pop();

            try
            {
                // Update the post's likes count BEFORE restoring
                postData.Post.Likes = postData.Likes.Count;

                // Restore the post first with correct likes count
                if (postDl.RestorePost(postData.Post))
                {
                    // Restore likes to the database
                    foreach (var like in postData.Likes)
                    {
                        // Use direct database insertion to ensure likes are restored
                        RestoreLikeToDatabase(like);
                    }

                    // Restore comments
                    foreach (var comment in postData.Comments)
                    {
                        commentDl.InsertComment(comment);
                    }

                    // Force update the post's likes count in database (in case of any sync issues)
                    UpdatePostLikesCountInDB(postData.Post.PostID, postData.Likes.Count);

                    // Also update the likes stack
                    UpdateLikesStack(postData.Likes);

                    // Insert back into queue in correct chronological order
                    postQueue.InsertInOrder(postData.Post);

                    // Refresh display
                    DisplayPosts();
                    UpdatePostCount();

                    MessageBox.Show("Post restored successfully with all likes and comments!", "Undo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to restore post!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restoring post: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this method to restore likes directly to database
        private void RestoreLikeToDatabase(likesBL like)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "INSERT INTO likes (LikeID, PostID, UserID) VALUES (@likeID, @postID, @userID)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@likeID", like.LikeID);
                    cmd.Parameters.AddWithValue("@postID", like.PostID);
                    cmd.Parameters.AddWithValue("@userID", like.UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // If duplicate key, it's okay - just continue
                if (!ex.Message.Contains("Duplicate"))
                {
                    MessageBox.Show($"Error restoring like: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Add this method to update likes count in database
        private void UpdatePostLikesCountInDB(int postID, int likesCount)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "UPDATE posts SET Likes = @likes WHERE PostID = @postID";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@likes", likesCount);
                    cmd.Parameters.AddWithValue("@postID", postID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating likes count: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this method to update the likes stack
        private void UpdateLikesStack(List<likesBL> likes)
        {
            foreach (var like in likes)
            {
                // Check if like already exists in stack
                if (!likesDL.HasUserLikedPost(like.UserID, like.PostID))
                {
                    // Create a new like object for the stack
                    likesBL newLike = new likesBL
                    {
                        LikeID = like.LikeID,
                        PostID = like.PostID,
                        UserID = like.UserID
                    };

                    //Reload likes
                    likesDL.LoadLikesFromDB();
                }
            }
        }

        private void FeedNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);

            ClearDeleteStackOnOperations();
            Feed view = new Feed(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void FriendsNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);

            ClearDeleteStackOnOperations();
            Friends view = new Friends(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void ProfileNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);

            ClearDeleteStackOnOperations();
            Profile view = new Profile(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void LogoutNav_Click(object sender, EventArgs e)
        {
            ClearDeleteStackOnOperations();
            LoginForm view = new LoginForm();
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.Instance.GoBack(this);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
