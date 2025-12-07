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
    public class likesDL
    {
        private static LikeStack likeStack = new LikeStack();
        private static bool isInitialized = false;

        // Static constructor to load likes on first access
        static likesDL()
        {
            LoadLikesFromDB();
        }

        // Load all likes from database into stack
        public static void LoadLikesFromDB()
        {
            likeStack.Clear();

            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "SELECT * FROM likes ORDER BY LikeID DESC";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            likesBL like = new likesBL(
                                reader.GetInt32("LikeID"),
                                reader.GetInt32("PostID"),
                                reader.GetInt32("UserID")
                            );

                            likeStack.Push(like);
                        }
                    }
                }
                isInitialized = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Likes Error: " + ex.Message);
            }
        }

        // ADD LIKE - First to stack, then to database
        public static bool AddLike(likesBL like)
        {
            try
            {
                // First check if like already exists in stack
                if (likeStack.HasUserLikedPost(like.UserID, like.PostID))
                    return true; // Already liked

                // First add to database to get LikeID
                int likeID = InsertLikeToDB(like);
                if (likeID > 0)
                {
                    like.LikeID = likeID;

                    // Then push to stack
                    likeStack.Push(like);

                    // Update post likes count in database
                    UpdatePostLikesCountInDB(like.PostID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Like Error: " + ex.Message);
            }

            return false;
        }

        // REMOVE LIKE - First from stack, then from database
        public static bool RemoveLike(int userID, int postID)
        {
            try
            {
                // First remove from stack
                likesBL removedLike = likeStack.RemoveLike(userID, postID);

                if (removedLike != null)
                {
                    // Then remove from database
                    bool dbSuccess = DeleteLikeFromDB(removedLike.LikeID);

                    if (dbSuccess)
                    {
                        // Update post likes count in database
                        UpdatePostLikesCountInDB(postID);
                        return true;
                    }
                    else
                    {
                        // If database deletion failed, add back to stack
                        likeStack.Push(removedLike);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Remove Like Error: " + ex.Message);
            }

            return false;
        }

        // CHECK IF USER LIKED POST - From stack only
        public static bool HasUserLikedPost(int userID, int postID)
        {
            return likeStack.HasUserLikedPost(userID, postID);
        }

        // GET LIKE COUNT - From stack only
        public static int GetLikeCountForPost(int postID)
        {
            return likeStack.GetLikeCountForPost(postID);
        }

        // GET LIKES FOR POST - From stack only
        public static List<likesBL> GetLikesForPost(int postID)
        {
            return likeStack.GetLikesForPost(postID);
        }

        // DATABASE OPERATIONS - These are private and only called by stack operations

        private static int InsertLikeToDB(likesBL like)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "INSERT INTO likes (PostID, UserID) VALUES (@postID, @userID); SELECT LAST_INSERT_ID();";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postID", like.PostID);
                    cmd.Parameters.AddWithValue("@userID", like.UserID);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Like to DB Error: " + ex.Message);
                return -1;
            }
        }

        private static bool DeleteLikeFromDB(int likeID)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "DELETE FROM likes WHERE LikeID = @likeID";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@likeID", likeID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Like from DB Error: " + ex.Message);
                return false;
            }
        }

        private static void UpdatePostLikesCountInDB(int postID)
        {
            try
            {
                // Get count from stack
                int likeCount = likeStack.GetLikeCountForPost(postID);

                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "UPDATE posts SET Likes = @likes WHERE PostID = @postID";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@likes", likeCount);
                    cmd.Parameters.AddWithValue("@postID", postID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Post Likes Count Error: " + ex.Message);
            }
        }
    }
}
