using MySql.Data.MySqlClient;
using SocialMediaFeedSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.DL
{
    public class commentsDL
    {
        public List<commentsBL> GetCommentsForPost(int postID)
        {
            List<commentsBL> comments = new List<commentsBL>();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "SELECT * FROM comments WHERE PostID = @postID";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postID", postID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new commentsBL(
                                Convert.ToInt32(reader["CommentID"]),
                                Convert.ToInt32(reader["PostID"]),
                                Convert.ToInt32(reader["UserID"]),
                                reader["Content"].ToString(),
                                Convert.ToDateTime(reader["CommentDate"])
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting comments: " + ex.Message);
            }
            return comments;
        }

        public bool InsertComment(commentsBL c)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "INSERT INTO comments(PostID, UserID, Content, CommentDate) VALUES (@p, @u, @c, @d)";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@p", c.PostID);
                    cmd.Parameters.AddWithValue("@u", c.UserID);
                    cmd.Parameters.AddWithValue("@c", c.Content);
                    cmd.Parameters.AddWithValue("@d", c.CommentDate);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Comment Error: " + ex.Message);
                return false;
            }
        }

        public CommentQueue GetCommentsByPostAsQueue(int postId)
        {
            CommentQueue commentQueue = new CommentQueue();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "SELECT * FROM comments WHERE PostID=@p ORDER BY CommentDate ASC";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@p", postId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            commentQueue.Enqueue(new commentsBL(
                                reader.GetInt32("CommentID"),
                                reader.GetInt32("PostID"),
                                reader.GetInt32("UserID"),
                                reader.GetString("Content"),
                                reader.GetDateTime("CommentDate")
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Comments Error: " + ex.Message);
            }
            return commentQueue;
        }
    }
}
