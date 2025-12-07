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
    public class postsDL
    {
        public bool InsertPost(postsBL p)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "INSERT INTO posts(UserID, Content, PostDate, Likes, Comments) VALUES(@u, @c, @d, @l, @cm)";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@u", p.UserID);
                    cmd.Parameters.AddWithValue("@c", p.Content);
                    cmd.Parameters.AddWithValue("@d", p.PostDate);
                    cmd.Parameters.AddWithValue("@l", p.Likes);
                    cmd.Parameters.AddWithValue("@cm", p.Comments);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message);
                return false;
            }
        }

        public DataTable GetPostsByUser(int userId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "SELECT PostID, UserID, Content, PostDate, Likes, Comments FROM posts WHERE UserID = @u ORDER BY PostDate ASC";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@u", userId);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Posts Error: " + ex.Message);
            }
            return dt;
        }

        public DataTable GetAllPostsExceptCurrentUser(int currentUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"SELECT p.*, u.Username 
                           FROM posts p 
                           INNER JOIN users u ON p.UserID = u.UserID 
                           WHERE p.UserID != @currentUserID 
                           ORDER BY p.PostDate ASC";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@currentUserID", currentUserID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load All Posts Error: " + ex.Message);
            }
            return dt;
        }

        // UPDATED DELETE POST METHOD - Handle cascading deletes
        public bool DeletePost(int postID)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    // First delete associated comments
                    string deleteCommentsQuery = "DELETE FROM comments WHERE PostID = @postID";
                    MySqlCommand deleteCommentsCmd = new MySqlCommand(deleteCommentsQuery, con);
                    deleteCommentsCmd.Parameters.AddWithValue("@postID", postID);
                    deleteCommentsCmd.ExecuteNonQuery();

                    // Then delete associated likes
                    string deleteLikesQuery = "DELETE FROM likes WHERE PostID = @postID";
                    MySqlCommand deleteLikesCmd = new MySqlCommand(deleteLikesQuery, con);
                    deleteLikesCmd.Parameters.AddWithValue("@postID", postID);
                    deleteLikesCmd.ExecuteNonQuery();

                    // Finally delete the post
                    string deletePostQuery = "DELETE FROM posts WHERE PostID = @postID";
                    MySqlCommand deletePostCmd = new MySqlCommand(deletePostQuery, con);
                    deletePostCmd.Parameters.AddWithValue("@postID", postID);

                    return deletePostCmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Post Error: " + ex.Message);
                return false;
            }
        }

        public bool RestorePost(postsBL post)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = @"INSERT INTO posts (PostID, UserID, Content, PostDate, Likes, Comments) 
                           VALUES (@postID, @userID, @content, @postDate, @likes, @comments)";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@postID", post.PostID);
                    cmd.Parameters.AddWithValue("@userID", post.UserID);
                    cmd.Parameters.AddWithValue("@content", post.Content);
                    cmd.Parameters.AddWithValue("@postDate", post.PostDate);
                    cmd.Parameters.AddWithValue("@likes", post.Likes); // Make sure likes count is restored
                    cmd.Parameters.AddWithValue("@comments", post.Comments);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Restore Post Error: " + ex.Message);
                return false;
            }
        }

        public bool UpdatePost(postsBL post)
        {
            try
            {
                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    string query = "UPDATE posts SET Content = @content WHERE PostID = @postID";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@content", post.Content);
                    cmd.Parameters.AddWithValue("@postID", post.PostID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Post Error: " + ex.Message);
                return false;
            }
        }
    }
}