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

namespace SocialMediaFeedSystem.UI
{
    public partial class Feed : Form
    {
        private usersBL currentUser;
        private PostQueue postQueue = new PostQueue();
        private postsDL postDl = new postsDL();
        private commentsDL commentDl = new commentsDL();
        private int currentPostID = -1;
        private Panel currentActivePostCard = null;
        private string currentSortOption = "Oldest First"; // Default sort
        private string currentSearchTerm = "";

        public Feed(usersBL user)
        {
            InitializeComponent();
            currentUser = user;
            InitializeUI();
            InitializeSortComboBox();
            SetupSearchFunctionality();
            LoadPosts();
        }

        private void InitializeUI()
        {
            NoPostLabel.Visible = false;
            NoFeedLabel.Visible = false;
            cmntLabel.Visible = true;
            PostFlowPanel.Visible = false;
            CommentsFlowPanel.Visible = false;
            CommentLabel.Visible = false;
            CommentInputPanel.Visible = false;
            SortByComboBox.Visible = true;
            SortPostLabel.Visible = true;
            SearchBar.Visible = true;
            SearchBtn.Visible = true;
            ClearSearchBtn.Visible = false;

            // Set colors to match Dashboard theme
            PostFlowPanel.BackColor = Color.FromArgb(240, 200, 240);
            CommentsFlowPanel.BackColor = Color.FromArgb(240, 200, 240);
            CommentInputPanel.BackColor = Color.Plum;
            CommentTextBox.BackColor = Color.FromArgb(240, 200, 240);
            CommentLabel.BackColor = Color.Transparent;
            PostCommentBtn.BackColor = Color.Purple;
            PostCommentBtn.ForeColor = Color.White;
        }

        private void InitializeSortComboBox()
        {
            // Add sorting options with "Oldest First" as default (FIFO)
            SortByComboBox.Items.AddRange(new string[] {
                "Oldest First", // FIFO - Default
                "Latest First",
                "Trending"
            });

            // Set default selection to "Oldest First"
            SortByComboBox.SelectedIndex = 0;
            currentSortOption = "Oldest First";

            SortByComboBox.SelectedIndexChanged += SortByComboBox_SelectedIndexChanged;
        }

        private void SetupSearchFunctionality()
        {
            // Add click event for search button
            SearchBtn.Click += SearchBtn_Click;

            // Optional: Add Enter key support for search
            SearchBar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    PerformSearch();
                }
            };

            // Optional: Add real-time search as user types
            SearchBar.TextChanged += SearchBar_TextChanged;
        }

        private void SortByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SortByComboBox.SelectedItem != null)
            {
                currentSortOption = SortByComboBox.SelectedItem.ToString();
                ApplySortingAndSearch();
            }
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            // Optional: Real-time search with debounce
            // You can add a Timer here for better performance
            if (SearchBar.Text.Length == 0)
            {
                PerformSearch(); // Clear search when text is empty
            }
        }

        private void PerformSearch()
        {
            currentSearchTerm = SearchBar.Text.Trim();
            ApplySortingAndSearch();
        }

        private void ApplySortingAndSearch()
        {
            try
            {
                // Apply search filter first
                List<postsBL> postsToDisplay;

                if (!string.IsNullOrEmpty(currentSearchTerm))
                {
                    // Use linear search for both content and username
                    var contentResults = postQueue.LinearSearchByContent(currentSearchTerm);
                    var usernameResults = postQueue.LinearSearchByUsername(currentSearchTerm, GetUsernameByUserID);

                    // Combine results and remove duplicates
                    var allResults = contentResults.Union(usernameResults).Distinct().ToList();
                    postsToDisplay = allResults;
                }
                else
                {
                    // Use all posts
                    postsToDisplay = postQueue.ToList();
                }

                // Create a temporary queue for sorting
                PostQueue tempQueue = new PostQueue();
                foreach (var post in postsToDisplay)
                {
                    tempQueue.Enqueue(post);
                }

                // Apply sorting based on current option
                switch (currentSortOption)
                {
                    case "Latest First":
                        HeadingLabel.Text = "Your Feed";
                        tempQueue.SortByLatest();
                        break;
                    case "Trending":
                        HeadingLabel.Text = "Trending";
                        tempQueue.SortByTrending();
                        break;
                    case "Oldest First":
                    default:
                        HeadingLabel.Text = "Your Feed";
                        // No sorting needed - already in FIFO order (oldest first)
                        break;
                }

                // Display the sorted and filtered posts
                DisplayPosts(tempQueue);
                UpdateSearchStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying sorting/search: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSearchStatus()
        {
            if (!string.IsNullOrEmpty(currentSearchTerm))
            {
                int displayedCount = PostFlowPanel.Controls.Count;
                NoFeedLabel.Visible = true;
                NoFeedLabel.Text = $"{displayedCount} posts for '{currentSearchTerm}'";

                // Show clear search button when there's a search term
                ClearSearchBtn.Visible = true;
            }
            else
            {
                NoFeedLabel.Visible = false;
                // Hide clear search button when no search term
                ClearSearchBtn.Visible = false;
            }
        }

        private void LoadPosts()
        {
            postQueue = new PostQueue();
            DataTable allPostsDt = postDl.GetAllPostsExceptCurrentUser(currentUser.UserID);

            // Create a list to reverse the order for FIFO
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in allPostsDt.Rows)
            {
                rows.Add(row);
            }

            // Enqueue posts in FIFO order (oldest first)
            foreach (DataRow row in rows)
            {
                int postID = Convert.ToInt32(row["PostID"]);
                postsBL post = new postsBL
                {
                    PostID = postID,
                    UserID = Convert.ToInt32(row["UserID"]),
                    Content = row["Content"].ToString(),
                    PostDate = Convert.ToDateTime(row["PostDate"]),
                    Likes = likesDL.GetLikeCountForPost(postID), // Use static likesDL
                    Comments = Convert.ToInt32(row["Comments"])
                };
                postQueue.Enqueue(post); // This maintains FIFO order
            }

            // Apply default sorting (FIFO) and display
            ApplySortingAndSearch();
            UpdatePostVisibility();
        }

        private void DisplayPosts(PostQueue queue)
        {
            PostFlowPanel.Controls.Clear();
            currentActivePostCard = null;

            PostNode temp = queue.Front;
            while (temp != null)
            {
                PostFlowPanel.Controls.Add(CreatePostCard(temp.Data));
                temp = temp.Next;
            }

            UpdatePostVisibility();
        }

        private Panel CreatePostCard(postsBL post)
        {
            Panel card = new Panel();
            card.Width = 385;
            card.Height = 150;
            card.BackColor = Color.Plum;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);
            card.Padding = new Padding(10);
            card.Tag = post.PostID;

            string username = GetUsernameByUserID(post.UserID);

            // Username label
            Label lblUsername = new Label();
            lblUsername.Text = username;
            lblUsername.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lblUsername.ForeColor = Color.DarkBlue;
            lblUsername.AutoSize = true;
            lblUsername.Top = 10;
            lblUsername.Left = 10;

            // Content label
            Label lblContent = new Label();
            lblContent.Text = post.Content;
            lblContent.Font = new Font("Times New Roman", 11);
            lblContent.AutoSize = false;
            lblContent.Width = 360;
            lblContent.Height = 60;
            lblContent.Top = 35;
            lblContent.Left = 10;

            // Date label
            Label lblDate = new Label();
            lblDate.Text = post.PostDate.ToString("dd MMM yyyy • hh:mm tt");
            lblDate.Font = new Font("Times New Roman", 10, FontStyle.Italic);
            lblDate.ForeColor = Color.Purple;
            lblDate.Top = 100;
            lblDate.Left = 10;
            lblDate.AutoSize = true;

            // Like functionality - UPDATED TO USE STATIC likesDL
            PictureBox pbLike = new PictureBox();
            pbLike.Image = likesDL.HasUserLikedPost(currentUser.UserID, post.PostID) ?
                Properties.Resources.Liked : Properties.Resources.Like;
            pbLike.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLike.Size = new Size(30, 30);
            pbLike.Top = 115;
            pbLike.Left = 300;
            pbLike.Cursor = Cursors.Hand;
            pbLike.Tag = post.PostID;
            pbLike.Click += PbLike_Click;

            // Comment functionality
            PictureBox pbComment = new PictureBox();
            pbComment.Image = Properties.Resources.Comment;
            pbComment.SizeMode = PictureBoxSizeMode.StretchImage;
            pbComment.Size = new Size(30, 30);
            pbComment.Top = 115;
            pbComment.Left = 340;
            pbComment.Cursor = Cursors.Hand;
            pbComment.Tag = post.PostID;
            pbComment.Click += PbComment_Click;

            // Like count - UPDATED TO USE STATIC likesDL
            Label lblLikesCount = new Label();
            lblLikesCount.Text = likesDL.GetLikeCountForPost(post.PostID).ToString();
            lblLikesCount.Font = new Font("Times New Roman", 9, FontStyle.Bold);
            lblLikesCount.ForeColor = Color.DarkBlue;
            lblLikesCount.Top = 100;
            lblLikesCount.Left = 307;
            lblLikesCount.AutoSize = true;
            lblLikesCount.Tag = post.PostID;

            // Comment count
            Label lblCommentsCount = new Label();
            lblCommentsCount.Text = post.Comments.ToString();
            lblCommentsCount.Font = new Font("Times New Roman", 9, FontStyle.Bold);
            lblCommentsCount.ForeColor = Color.DarkBlue;
            lblCommentsCount.Top = 100;
            lblCommentsCount.Left = 347;
            lblCommentsCount.AutoSize = true;
            lblCommentsCount.Cursor = Cursors.Hand;
            lblCommentsCount.Tag = post.PostID;
            lblCommentsCount.Click += LblComments_Click;

            card.Controls.AddRange(new Control[] {
                lblUsername, lblContent, lblDate, pbLike, pbComment, lblLikesCount, lblCommentsCount
            });
            return card;
        }

        private void PbLike_Click(object sender, EventArgs e)
        {
            PictureBox pbLike = (PictureBox)sender;
            int postID = (int)pbLike.Tag;

            // UPDATED TO USE STATIC likesDL
            if (likesDL.HasUserLikedPost(currentUser.UserID, postID))
            {
                // Unlike the post - operation happens on stack first
                if (likesDL.RemoveLike(currentUser.UserID, postID))
                {
                    pbLike.Image = Properties.Resources.Like;
                    UpdateLikeCount(postID);
                    // Refresh posts to update all counts
                    LoadPosts();
                }
                else
                {
                    MessageBox.Show("Failed to unlike post!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Like the post - operation happens on stack first
                likesBL newLike = new likesBL { PostID = postID, UserID = currentUser.UserID };
                if (likesDL.AddLike(newLike))
                {
                    pbLike.Image = Properties.Resources.Liked;
                    UpdateLikeCount(postID);
                    // Refresh posts to update all counts
                    LoadPosts();
                }
                else
                {
                    MessageBox.Show("Failed to like post!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PbComment_Click(object sender, EventArgs e)
        {
            PictureBox pbComment = (PictureBox)sender;
            int clickedPostID = (int)pbComment.Tag;
            HighlightPostCard(clickedPostID);
            ShowCommentsSection(clickedPostID);
        }

        private void LblComments_Click(object sender, EventArgs e)
        {
            Label lblComments = (Label)sender;
            int clickedPostID = (int)lblComments.Tag;
            HighlightPostCard(clickedPostID);
            ShowCommentsSection(clickedPostID);
        }

        private void HighlightPostCard(int postID)
        {
            if (currentActivePostCard != null)
            {
                currentActivePostCard.BackColor = Color.Plum;
            }

            foreach (Control control in PostFlowPanel.Controls)
            {
                if (control is Panel postCard && (int)postCard.Tag == postID)
                {
                    postCard.BackColor = Color.FromArgb(200, 160, 220);
                    currentActivePostCard = postCard;
                    break;
                }
            }
        }

        private void ShowCommentsSection(int postID)
        {
            currentPostID = postID;
            LoadCommentsForPost(currentPostID);
            cmntLabel.Visible = false;
            CommentsFlowPanel.Visible = true;
            CommentInputPanel.Visible = true;
            CommentTextBox.Focus();
        }

        private void LoadCommentsForPost(int postID)
        {
            CommentsFlowPanel.Controls.Clear();
            CommentQueue commentQueue = commentDl.GetCommentsByPostAsQueue(postID);

            if (commentQueue.IsEmpty())
            {
                Label noCommentsLabel = new Label();
                noCommentsLabel.Text = "No comments. Be the first to comment!";
                noCommentsLabel.Font = new Font("Times New Roman", 11, FontStyle.Italic);
                noCommentsLabel.ForeColor = Color.Gray;
                noCommentsLabel.AutoSize = true;
                noCommentsLabel.Margin = new Padding(10);
                noCommentsLabel.TextAlign = ContentAlignment.MiddleCenter;
                noCommentsLabel.Width = 280;
                noCommentsLabel.Height = 30;
                CommentsFlowPanel.Controls.Add(noCommentsLabel);
            }
            else
            {
                CommentNode temp = commentQueue.Front;
                while (temp != null)
                {
                    CommentsFlowPanel.Controls.Add(CreateCommentPanel(temp.Data));
                    temp = temp.Next;
                }
            }
        }

        private Panel CreateCommentPanel(commentsBL comment)
        {
            Panel commentPanel = new Panel();
            commentPanel.Width = 280;
            commentPanel.Height = 80;
            commentPanel.Margin = new Padding(5);
            commentPanel.BackColor = Color.FromArgb(240, 200, 240);
            commentPanel.BorderStyle = BorderStyle.FixedSingle;
            commentPanel.Padding = new Padding(8);

            string username = GetUsernameByUserID(comment.UserID);

            Label userLabel = new Label();
            userLabel.Text = username;
            userLabel.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            userLabel.ForeColor = Color.DarkBlue;
            userLabel.Location = new Point(8, 8);
            userLabel.AutoSize = true;

            Label contentLabel = new Label();
            contentLabel.Text = comment.Content;
            contentLabel.Font = new Font("Times New Roman", 9);
            contentLabel.Location = new Point(8, 30);
            contentLabel.Size = new Size(260, 30);
            contentLabel.AutoSize = false;

            Label dateLabel = new Label();
            dateLabel.Text = comment.CommentDate.ToString("dd MMM • hh:mm tt");
            dateLabel.Font = new Font("Times New Roman", 8);
            dateLabel.ForeColor = Color.Gray;
            dateLabel.Location = new Point(150, 8);
            dateLabel.AutoSize = true;

            commentPanel.Controls.AddRange(new Control[] { userLabel, contentLabel, dateLabel });
            return commentPanel;
        }

        private void PostCommentBtn_Click_1(object sender, EventArgs e)
        {
            if (currentPostID == -1)
            {
                MessageBox.Show("Please select a post first!");
                return;
            }

            string commentText = CommentTextBox.Text.Trim();

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
                CommentTextBox.Text = "";
                LoadPosts(); // Refresh posts to update counts

                // Re-highlight the active post after refresh
                if (currentPostID != -1)
                {
                    foreach (Control control in PostFlowPanel.Controls)
                    {
                        if (control is Panel postCard && (int)postCard.Tag == currentPostID)
                        {
                            postCard.BackColor = Color.FromArgb(200, 160, 220);
                            currentActivePostCard = postCard;
                            break;
                        }
                    }
                }

                MessageBox.Show("Comment added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                HideCommentsSection();
            }
            else
            {
                MessageBox.Show("Failed to add comment!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideCommentsSection()
        {
            cmntLabel.Visible = true;
            CommentsFlowPanel.Visible = false;
            CommentLabel.Visible = false;
            CommentInputPanel.Visible = false;
            if (currentActivePostCard != null)
            {
                currentActivePostCard.BackColor = Color.Plum;
                currentActivePostCard = null;
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

        private void UpdateLikeCount(int postID)
        {
            foreach (Control control in PostFlowPanel.Controls)
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

        private void UpdatePostVisibility()
        {
            bool hasPosts = PostFlowPanel.Controls.Count > 0;
            NoPostLabel.Visible = !hasPosts;
            PostFlowPanel.Visible = hasPosts;
        }

        private string GetUsernameByUserID(int userID)
        {
            // Use the linked list from usersDL
            usersBL user = usersDL.UsersList.FindUserByID(userID);
            return user != null ? user.Username : "Unknown User";
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.Instance.GoBack(this);
        }

        private void LogoutNav_Click(object sender, EventArgs e)
        {
            NavigationManager.ClearHistory();
            LoginForm login = new LoginForm();
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;

            this.Hide();
            login.Show();
        }

        private void ProfileNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Profile login = new Profile(currentUser);
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;

            this.Hide();
            login.Show();
        }

        private void FriendsNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Friends login = new Friends(currentUser);
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;

            this.Hide();
            login.Show();
        }

        private void HomeNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Dashboard login = new Dashboard(currentUser);
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;

            this.Hide();
            login.Show();
        }

        private void ClearSearchBtn_Click(object sender, EventArgs e)
        {
            currentSearchTerm = "";
            SearchBar.Text = "";
            ApplySortingAndSearch();

            // Hide the clear button
            ClearSearchBtn.Visible = false;

            // Reset the heading
            HeadingLabel.Text = "Your Feed";
        }
    }
}
