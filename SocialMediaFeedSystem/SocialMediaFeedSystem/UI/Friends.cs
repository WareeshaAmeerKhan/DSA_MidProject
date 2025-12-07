using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.UI
{
    public partial class Friends : Form
    {
        private usersBL currentUser;
        private friendDL friendManager;
        private string currentSortOption = "";
        private string currentSearchTerm = ""; // Track current search term

        public Friends(usersBL user)
        {
            InitializeComponent();
            currentUser = user;
            friendManager = new friendDL();

            InitializeSortComboBox();
            SetupSearchFunctionality();

            // Load data into linked lists from database
            friendManager.LoadDataFromDatabase(currentUser.UserID);

            // Initialize all labels and panels as not visible
            NoRequestLabel.Visible = false;
            NoSuggestionLabel.Visible = false;
            NoFriendLabel.Visible = false;
            FriendsFlowPanel.Visible = false;
            RequestsFlowPanel.Visible = false;
            SuggestionsFlowPanel.Visible = false;
            SortByComboBox.Visible = true;
            SortPostLabel.Visible = true;
            SearchBar.Visible = true;
            SearchBtn.Visible = true;

            friendManager.RefreshData(currentUser.UserID);
            LoadFriendData();
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
        }

        private void InitializeSortComboBox()
        {
            // Add sorting options
            SortByComboBox.Items.AddRange(new string[] {
            "Name (A-Z)",
            "Friendship Date (Oldest First)"
        });

            // Set no option selected initially
            SortByComboBox.SelectedIndex = -1;

            SortByComboBox.SelectedIndexChanged += SortByComboBox_SelectedIndexChanged;
        }

        private void SortByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SortByComboBox.SelectedItem != null)
            {
                currentSortOption = SortByComboBox.SelectedItem.ToString();
                ApplyMergeSort();
            }
            else
            {
                // If nothing selected, keep the original order (no sorting)
                // You can reload the friends without applying any sort if needed
                friendManager.RefreshData(currentUser.UserID);
                LoadFriends();
            }
        }

        private void ApplyMergeSort()
        {
            try
            {
                // Get the friends linked list from friendManager
                var friendsList = friendManager.GetFriendsLinkedList();

                // Apply Merge Sort (the best algorithm) based on selection
                switch (currentSortOption)
                {
                    case "Name (A-Z)":
                        FriendSorter.SortFriends(friendsList, true); // Sort by username
                        break;
                    case "Friendship Date (Oldest First)":
                        FriendSorter.SortFriends(friendsList, false); // Sort by friendship date
                        break;
                    default:
                        // No sorting applied, use original order from database
                        friendManager.RefreshData(currentUser.UserID);
                        break;
                }

                LoadFriends(); // Reload friends with new sorting
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying sorting: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformSearch()
        {
            currentSearchTerm = SearchBar.Text.Trim();

            if (string.IsNullOrEmpty(currentSearchTerm))
            {
                // If search is empty, show all friends
                LoadFriends();
                return;
            }

            // Use linear search on the linked list
            PerformLinearSearch();
        }

        private void PerformLinearSearch()
        {
            FriendsFlowPanel.Controls.Clear();
            try
            {
                // Get the linked list and perform linear search
                var friendsList = friendManager.GetFriendsLinkedList();
                var searchResults = friendsList.LinearSearchByUsername(currentSearchTerm);

                // Set visibility based on search results
                if (searchResults.Count > 0)
                {
                    FriendsFlowPanel.Visible = true;
                    NoFriendLabel.Visible = false;

                    foreach (var friend in searchResults)
                    {
                        var friendCard = CreateFriendCard(friend, false, false);
                        FriendsFlowPanel.Controls.Add(friendCard);
                    }

                    // Update count label
                    if (lblFriendsCount != null)
                        lblFriendsCount.Text = $"{searchResults.Count} found for '{currentSearchTerm}'";
                }
                else
                {
                    FriendsFlowPanel.Visible = false;
                    NoFriendLabel.Visible = true;
                    NoFriendLabel.Text = $"No friends found \nmatching '{currentSearchTerm}'";

                    if (lblFriendsCount != null)
                        lblFriendsCount.Text = "0 found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing search: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFriendData()
        {
            LoadFriends();
            LoadFriendRequests();
            LoadSuggestions();
        }

        private void LoadFriends()
        {
            FriendsFlowPanel.Controls.Clear();
            try
            {
                List<friendsBL> friends;

                if (!string.IsNullOrEmpty(currentSearchTerm))
                {
                    // Use linear search for filtered results
                    var friendsList = friendManager.GetFriendsLinkedList();
                    friends = friendsList.LinearSearchByUsername(currentSearchTerm);
                }
                else
                {
                    // Show all friends
                    friends = friendManager.GetFriends(currentUser.UserID);
                }

                // Set visibility based on count
                if (friends.Count > 0)
                {
                    FriendsFlowPanel.Visible = true;
                    NoFriendLabel.Visible = false;

                    foreach (var friend in friends)
                    {
                        var friendCard = CreateFriendCard(friend, false, false);
                        FriendsFlowPanel.Controls.Add(friendCard);
                    }
                }
                else
                {
                    FriendsFlowPanel.Visible = false;
                    NoFriendLabel.Visible = true;

                    if (!string.IsNullOrEmpty(currentSearchTerm))
                    {
                        NoFriendLabel.Text = $"No friends found matching '{currentSearchTerm}'";
                    }
                    else
                    {
                        NoFriendLabel.Text = "No friends yet";
                    }
                }

                // Update count label
                if (lblFriendsCount != null)
                {
                    if (!string.IsNullOrEmpty(currentSearchTerm))
                    {
                        lblFriendsCount.Text = $"{friends.Count} found for '{currentSearchTerm}'";
                    }
                    else
                    {
                        lblFriendsCount.Text = $"{friends.Count}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friends: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFriendRequests()
        {
            RequestsFlowPanel.Controls.Clear();

            try
            {
                var requests = friendManager.GetFriendRequests(currentUser.UserID);

                // Set visibility based on count
                if (requests.Count > 0)
                {
                    RequestsFlowPanel.Visible = true;
                    NoRequestLabel.Visible = false;

                    foreach (var request in requests)
                    {
                        var requestCard = CreateFriendCard(request, true, false);
                        RequestsFlowPanel.Controls.Add(requestCard);
                    }
                }
                else
                {
                    RequestsFlowPanel.Visible = false;
                    NoRequestLabel.Visible = true;
                }

                // Update requests count label if you have one
                if (lblRequestsCount != null)
                    lblRequestsCount.Text = $"{requests.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friend requests: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuggestions()
        {
            SuggestionsFlowPanel.Controls.Clear();

            try
            {
                var suggestions = friendManager.GetFriendSuggestions(currentUser.UserID);

                // Set visibility based on count
                if (suggestions.Count > 0)
                {
                    SuggestionsFlowPanel.Visible = true;
                    NoSuggestionLabel.Visible = false;

                    foreach (var suggestion in suggestions)
                    {
                        var suggestionCard = CreateFriendCard(suggestion, false, true);
                        SuggestionsFlowPanel.Controls.Add(suggestionCard);
                    }
                }
                else
                {
                    SuggestionsFlowPanel.Visible = false;
                    NoSuggestionLabel.Visible = true;
                }

                // Update suggestions count label if you have one
                if (lblSuggestionsCount != null)
                    lblSuggestionsCount.Text = $"{suggestions.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suggestions: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateFriendCard(friendsBL user, bool isRequest = false, bool isSuggestion = false)
        {
            Panel card = new Panel();
            card.Width = 385;
            card.Height = 140; // Increased height to show friendship date
            card.BackColor = Color.FromArgb(240, 200, 240);
            card.Margin = new Padding(10);
            card.Padding = new Padding(10);
            card.Tag = user.UserID;

            // Profile Picture
            PictureBox picProfile = new PictureBox();
            picProfile.Size = new Size(60, 60);
            picProfile.Location = new Point(10, 10);
            picProfile.SizeMode = PictureBoxSizeMode.Zoom;
            picProfile.BackColor = Color.LightGray;
            picProfile.Image = LoadProfileImage(user.ProfilePicPath, user.Username);

            // Username Label
            Label lblUsername = new Label();
            lblUsername.Text = user.Username;
            lblUsername.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lblUsername.ForeColor = Color.Purple;
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(80, 10);

            // Bio Label
            Label lblBio = new Label();
            lblBio.Text = string.IsNullOrEmpty(user.Bio) ? "No bio available" :
                         (user.Bio.Length > 80 ? user.Bio.Substring(0, 80) + "..." : user.Bio);
            lblBio.Font = new Font("Times New Roman", 10);
            lblBio.ForeColor = Color.DarkBlue;
            lblBio.AutoSize = false;
            lblBio.Size = new Size(280, 40);
            lblBio.Location = new Point(80, 35);

            // Friendship Date Label (only for friends, not requests/suggestions)
            if (!isRequest && !isSuggestion)
            {
                Label lblFriendshipDate = new Label();
                lblFriendshipDate.Text = $"Friends since: {user.FriendshipDate.ToString("MMM dd, yyyy")}";
                lblFriendshipDate.Font = new Font("Times New Roman", 9);
                lblFriendshipDate.ForeColor = Color.DarkGreen;
                lblFriendshipDate.AutoSize = true;
                lblFriendshipDate.Location = new Point(80, 75);
                card.Controls.Add(lblFriendshipDate);
            }

            // Action Buttons
            if (isRequest)
            {
                // Accept Button - Plum color
                Label btnAccept = new Label();
                btnAccept.Text = "Accept";
                btnAccept.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                btnAccept.ForeColor = Color.Black;
                btnAccept.BackColor = Color.Plum;
                btnAccept.Size = new Size(60, 25);
                btnAccept.Location = new Point(80, 100);
                btnAccept.TextAlign = ContentAlignment.MiddleCenter;
                btnAccept.Cursor = Cursors.Hand;
                btnAccept.BorderStyle = BorderStyle.FixedSingle;
                btnAccept.Tag = user.RequestID;
                btnAccept.Click += (s, e) =>
                {
                    if (friendManager.AcceptFriendRequest(user.RequestID, currentUser.UserID))
                    {
                        card.Parent.Controls.Remove(card);
                        LoadFriendData(); // Refresh all panels
                        MessageBox.Show("Friend request accepted!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to accept friend request.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                // Reject Button - Red color
                Label btnReject = new Label();
                btnReject.Text = "Reject";
                btnReject.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                btnReject.ForeColor = Color.White;
                btnReject.BackColor = Color.FromArgb(200, 80, 80);
                btnReject.Size = new Size(60, 25);
                btnReject.Location = new Point(150, 100);
                btnReject.TextAlign = ContentAlignment.MiddleCenter;
                btnReject.Cursor = Cursors.Hand;
                btnReject.BorderStyle = BorderStyle.FixedSingle;
                btnReject.Tag = user.RequestID;
                btnReject.Click += (s, e) =>
                {
                    if (friendManager.RejectFriendRequest(user.RequestID, currentUser.UserID))
                    {
                        card.Parent.Controls.Remove(card);
                        LoadFriendData(); // Refresh all panels
                        MessageBox.Show("Friend request rejected.", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to reject friend request.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                card.Controls.AddRange(new Control[] { picProfile, lblUsername, lblBio, btnAccept, btnReject });
            }
            else if (isSuggestion)
            {
                // Add Friend Button - Plum color
                Label btnAddFriend = new Label();
                btnAddFriend.Text = "Add Friend";
                btnAddFriend.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                btnAddFriend.ForeColor = Color.Black;
                btnAddFriend.BackColor = Color.Plum;
                btnAddFriend.Size = new Size(80, 25);
                btnAddFriend.Location = new Point(80, 100);
                btnAddFriend.TextAlign = ContentAlignment.MiddleCenter;
                btnAddFriend.Cursor = Cursors.Hand;
                btnAddFriend.BorderStyle = BorderStyle.FixedSingle;
                btnAddFriend.Tag = user.UserID;
                btnAddFriend.Click += (s, e) =>
                {
                    if (friendManager.SendFriendRequest(currentUser.UserID, user.UserID))
                    {
                        card.Parent.Controls.Remove(card);
                        LoadFriendData(); // Refresh all panels
                        MessageBox.Show("Friend request sent!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to send friend request. Request might already exist.",
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                card.Controls.AddRange(new Control[] { picProfile, lblUsername, lblBio, btnAddFriend });
            }
            else
            {
                // Remove Friend Button - Red color
                Label btnRemove = new Label();
                btnRemove.Text = "Remove";
                btnRemove.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                btnRemove.ForeColor = Color.White;
                btnRemove.BackColor = Color.FromArgb(200, 80, 80);
                btnRemove.Size = new Size(70, 25);
                btnRemove.Location = new Point(80, 100);
                btnRemove.TextAlign = ContentAlignment.MiddleCenter;
                btnRemove.Cursor = Cursors.Hand;
                btnRemove.BorderStyle = BorderStyle.FixedSingle;
                btnRemove.Tag = user.UserID;
                btnRemove.Click += (s, e) =>
                {
                    if (friendManager.RemoveFriend(currentUser.UserID, user.UserID))
                    {
                        card.Parent.Controls.Remove(card);
                        LoadFriendData(); // Refresh all panels
                        MessageBox.Show("Friend removed successfully.", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove friend.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                card.Controls.AddRange(new Control[] { picProfile, lblUsername, lblBio, btnRemove });
            }

            return card;
        }

        private Image LoadProfileImage(string profilePicPath, string username)
        {
            if (!string.IsNullOrEmpty(profilePicPath) && File.Exists(profilePicPath))
            {
                try
                {
                    return Image.FromFile(profilePicPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading profile image: {ex.Message}");
                }
            }

            return Properties.Resources.default_pfp;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Set no option selected initially
            SortByComboBox.SelectedIndex = -1;
            // Refresh data from database into linked lists
            friendManager.RefreshData(currentUser.UserID);
            LoadFriendData();
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

        private void ProfileNav_Click(object sender, EventArgs e)
        {
            // Push current form before navigating
            NavigationManager.Instance.PushForm(this, currentUser);
            Profile view = new Profile(currentUser);
            view.StartPosition = FormStartPosition.Manual;
            view.Location = this.Location;

            this.Hide();
            view.Show();
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

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.Instance.GoBack(this);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
    }
}
