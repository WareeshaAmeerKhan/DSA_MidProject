using MySql.Data.MySqlClient;
using SocialMediaFeedSystem.BL;
using SocialMediaFeedSystem.DataSturctures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.DL
{
    internal class friendDL
    {
        private FriendsLinkedList friendsList;
        private FriendsLinkedList friendRequestsList;
        private FriendsLinkedList suggestionsList;

        public friendDL()
        {
            friendsList = new FriendsLinkedList();
            friendRequestsList = new FriendsLinkedList();
            suggestionsList = new FriendsLinkedList();
        }

        // Add this method to expose the friends linked list
        public FriendsLinkedList GetFriendsLinkedList()
        {
            return friendsList;
        }

        // Load all data from database into linked lists
        public void LoadDataFromDatabase(int userId)
        {
            LoadFriendsFromDB(userId);
            LoadFriendRequestsFromDB(userId);
            LoadSuggestionsFromDB(userId);
        }

        // Add this method to friendDL class
        public static int GetFriendCount(int userId)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"
                SELECT COUNT(*) 
                FROM Friends 
                WHERE (User1ID = @UserID OR User2ID = @UserID)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting friend count: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void LoadFriendsFromDB(int userId)
        {
            friendsList.Clear();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"
                        SELECT u.UserID, u.Username, u.Bio, u.ProfilePic, f.CreatedDate as FriendshipDate
                        FROM Users u
                        INNER JOIN Friends f ON (u.UserID = f.User1ID OR u.UserID = f.User2ID)
                        WHERE (f.User1ID = @UserID OR f.User2ID = @UserID) 
                        AND u.UserID != @UserID";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            friendsBL friend = new friendsBL
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Bio = reader["Bio"] == DBNull.Value ? "" : reader["Bio"].ToString(),
                                ProfilePicPath = reader["ProfilePic"] == DBNull.Value ? null : reader["ProfilePic"].ToString(),
                                FriendshipDate = Convert.ToDateTime(reader["FriendshipDate"])
                            };
                            friendsList.InsertAtEnd(friend);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading friends: " + ex.Message);
            }
        }

        private void LoadFriendRequestsFromDB(int userId)
        {
            friendRequestsList.Clear();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"
                        SELECT u.UserID, u.Username, u.Bio, u.ProfilePic, fr.RequestID
                        FROM Users u
                        INNER JOIN FriendRequests fr ON u.UserID = fr.SenderID
                        WHERE fr.ReceiverID = @UserID AND fr.Status = 'Pending'";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            friendsBL request = new friendsBL
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Bio = reader["Bio"] == DBNull.Value ? "" : reader["Bio"].ToString(),
                                ProfilePicPath = reader["ProfilePic"] == DBNull.Value ? null : reader["ProfilePic"].ToString(),
                                RequestID = Convert.ToInt32(reader["RequestID"])
                            };
                            friendRequestsList.InsertAtEnd(request);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading friend requests: " + ex.Message);
            }
        }

        private void LoadSuggestionsFromDB(int userId)
        {
            suggestionsList.Clear();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"
                        SELECT u.UserID, u.Username, u.Bio, u.ProfilePic
                        FROM Users u
                        WHERE u.UserID != @UserID
                        AND u.UserID NOT IN (
                            SELECT CASE 
                                WHEN User1ID = @UserID THEN User2ID 
                                ELSE User1ID 
                            END
                            FROM Friends
                            WHERE User1ID = @UserID OR User2ID = @UserID
                        )
                        AND u.UserID NOT IN (
                            SELECT SenderID FROM FriendRequests 
                            WHERE ReceiverID = @UserID AND Status = 'Pending'
                        )
                        AND u.UserID NOT IN (
                            SELECT ReceiverID FROM FriendRequests 
                            WHERE SenderID = @UserID AND Status = 'Pending'
                        )
                        LIMIT 20";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            friendsBL suggestion = new friendsBL
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Bio = reader["Bio"] == DBNull.Value ? "" : reader["Bio"].ToString(),
                                ProfilePicPath = reader["ProfilePic"] == DBNull.Value ? null : reader["ProfilePic"].ToString()
                            };
                            suggestionsList.InsertAtEnd(suggestion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suggestions: " + ex.Message);
            }
        }

        // Get methods now return data from linked lists instead of database
        public System.Collections.Generic.List<friendsBL> GetFriends(int userId)
        {
            return friendsList.ToList();
        }

        public System.Collections.Generic.List<friendsBL> GetFriendRequests(int userId)
        {
            return friendRequestsList.ToList();
        }

        public System.Collections.Generic.List<friendsBL> GetFriendSuggestions(int userId)
        {
            return suggestionsList.ToList();
        }

        // Send Friend Request - add to linked list AND database
        public bool SendFriendRequest(int senderId, int receiverId)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    // Check if request already exists
                    string checkQuery = @"
                        SELECT COUNT(*) FROM FriendRequests 
                        WHERE SenderID = @SenderID AND ReceiverID = @ReceiverID 
                        AND Status = 'Pending'";

                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@SenderID", senderId);
                    checkCmd.Parameters.AddWithValue("@ReceiverID", receiverId);

                    int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (existingCount > 0)
                    {
                        return true;
                    }

                    // Insert new friend request into database
                    string insertQuery = @"
                        INSERT INTO FriendRequests (SenderID, ReceiverID, Status)
                        VALUES (@SenderID, @ReceiverID, 'Pending');
                        SELECT LAST_INSERT_ID();";

                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@SenderID", senderId);
                    insertCmd.Parameters.AddWithValue("@ReceiverID", receiverId);

                    int requestId = Convert.ToInt32(insertCmd.ExecuteScalar());

                    // Get sender info and add to suggestions list (remove from suggestions)
                    var senderInfo = GetUserInfo(senderId);
                    if (senderInfo != null)
                    {
                        // Remove from suggestions list
                        suggestionsList.DeleteNode(receiverId);
                    }

                    return requestId > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Friend Request Error: " + ex.Message);
                return false;
            }
        }

        // Accept Friend Request - update linked lists AND database
        public bool AcceptFriendRequest(int requestId, int receiverId)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    using (MySqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            // Get the request info before updating
                            var request = friendRequestsList.FindNodeByRequestId(requestId);
                            if (request == null) return false;

                            // Update friend request status in database
                            string updateQuery = @"
                                UPDATE FriendRequests 
                                SET Status = 'Accepted' 
                                WHERE RequestID = @RequestID AND ReceiverID = @ReceiverID";

                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, con, transaction);
                            updateCmd.Parameters.AddWithValue("@RequestID", requestId);
                            updateCmd.Parameters.AddWithValue("@ReceiverID", receiverId);
                            updateCmd.ExecuteNonQuery();

                            // Get sender ID from the request
                            string getSenderQuery = @"
                                SELECT SenderID FROM FriendRequests 
                                WHERE RequestID = @RequestID";

                            MySqlCommand getSenderCmd = new MySqlCommand(getSenderQuery, con, transaction);
                            getSenderCmd.Parameters.AddWithValue("@RequestID", requestId);
                            int senderId = Convert.ToInt32(getSenderCmd.ExecuteScalar());

                            // Create friendship in database
                            int user1Id = Math.Min(senderId, receiverId);
                            int user2Id = Math.Max(senderId, receiverId);

                            string insertQuery = @"
                                INSERT INTO Friends (User1ID, User2ID)
                                VALUES (@User1ID, @User2ID)";

                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, con, transaction);
                            insertCmd.Parameters.AddWithValue("@User1ID", user1Id);
                            insertCmd.Parameters.AddWithValue("@User2ID", user2Id);
                            insertCmd.ExecuteNonQuery();

                            // Update linked lists
                            // Remove from requests list
                            friendRequestsList.DeleteNodeByRequestId(requestId);

                            // Remove from suggestions list if exists
                            suggestionsList.DeleteNode(senderId);

                            // Add to friends list
                            var friendInfo = GetUserInfo(senderId);
                            if (friendInfo != null)
                            {
                                friendsList.InsertAtEnd(friendInfo);
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Accept Friend Request Transaction Error: " + ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Accept Friend Request Error: " + ex.Message);
                return false;
            }
        }

        // Reject Friend Request - update linked lists AND database
        public bool RejectFriendRequest(int requestId, int receiverId)
        {
            try
            {
                // Update database
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"
                        UPDATE FriendRequests 
                        SET Status = 'Rejected' 
                        WHERE RequestID = @RequestID AND ReceiverID = @ReceiverID";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    cmd.Parameters.AddWithValue("@ReceiverID", receiverId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update linked list - remove from requests
                        friendRequestsList.DeleteNodeByRequestId(requestId);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reject Friend Request Error: " + ex.Message);
                return false;
            }
        }

        // Remove Friend - update linked lists AND database
        public bool RemoveFriend(int userId, int friendId)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    int user1Id = Math.Min(userId, friendId);
                    int user2Id = Math.Max(userId, friendId);

                    string query = @"
                        DELETE FROM Friends 
                        WHERE User1ID = @User1ID AND User2ID = @User2ID";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@User1ID", user1Id);
                    cmd.Parameters.AddWithValue("@User2ID", user2Id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update linked list - remove from friends
                        friendsList.DeleteNode(friendId);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Remove Friend Error: " + ex.Message);
                return false;
            }
        }

        // Helper method to get user info
        private friendsBL GetUserInfo(int userId)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "SELECT UserID, Username, Bio, ProfilePic FROM Users WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new friendsBL
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Bio = reader["Bio"] == DBNull.Value ? "" : reader["Bio"].ToString(),
                                ProfilePicPath = reader["ProfilePic"] == DBNull.Value ? null : reader["ProfilePic"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting user info: " + ex.Message);
            }
            return null;
        }

        // Refresh all data from database
        public void RefreshData(int userId)
        {
            LoadDataFromDatabase(userId);
        }
    }
}
