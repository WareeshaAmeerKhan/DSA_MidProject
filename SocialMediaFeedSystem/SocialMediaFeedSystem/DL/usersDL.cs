using MySql.Data.MySqlClient;
using SocialMediaFeedSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.DL
{
    internal class usersDL
    {
        // The linked list is now here instead of in static users class
        public static UserLinkedList UsersList = new UserLinkedList();

        // Load all users from DB into Linked List
        public static void LoadUsersFromDB()
        {
            UsersList.Clear(); // Reset list

            string query = "SELECT * FROM users";

            using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usersBL user = new usersBL(
                            reader.GetInt32("UserID"),
                            reader.GetString("Username"),
                            reader.GetString("Email"),
                            reader.GetString("Password"),
                            reader.GetDateTime("DOB"),
                            reader.IsDBNull(reader.GetOrdinal("Bio")) ? "" : reader.GetString("Bio"),
                            reader.IsDBNull(reader.GetOrdinal("ProfilePic")) ? "" : reader.GetString("ProfilePic")
                        );

                        UsersList.InsertAtEnd(user);
                    }
                }
            }
        }

        public static bool AddUserToDBAndList(usersBL user)
        {
            string query = "INSERT INTO users (Username, Email, Password, DOB, Bio, ProfilePic) " +
                           "VALUES (@u, @e, @p, @dob, @b, @pic)";

            using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                cmd.Parameters.AddWithValue("@dob", user.DOB);
                cmd.Parameters.AddWithValue("@b", user.Bio);

                if (string.IsNullOrEmpty(user.ProfilePicPath))
                    cmd.Parameters.AddWithValue("@pic", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@pic", user.ProfilePicPath);

                // Execute the insert
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Get the auto-generated UserID
                    cmd.CommandText = "SELECT LAST_INSERT_ID()";
                    int newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                    user.UserID = newUserId;

                    // Add to linked list directly
                    UsersList.InsertAtEnd(user);
                    return true;
                }
                return false;
            }
        }

        public static usersBL LoginUserFromList(string input)
        {
            return UsersList.FindUser(input);
        }

        public static void UpdateUserInList(usersBL updatedUser)
        {
            UsersList.UpdateUser(updatedUser);
        }

        // Check if user exists in linked list
        public static bool CheckUserExists(string username, string email)
        {
            return UsersList.UserExists(username, email);
        }

        public static bool UpdateUserInDatabase(usersBL user)
        {
            string query = @"UPDATE users 
                    SET Username = @username, 
                        Email = @email, 
                        DOB = @dob, 
                        Bio = @bio, 
                        ProfilePic = @profilePic 
                    WHERE UserID = @userID";

            using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@dob", user.DOB);
                    cmd.Parameters.AddWithValue("@bio", user.Bio);
                    cmd.Parameters.AddWithValue("@profilePic",
                        string.IsNullOrEmpty(user.ProfilePicPath) ?
                        DBNull.Value : (object)user.ProfilePicPath);
                    cmd.Parameters.AddWithValue("@userID", user.UserID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool UpdatePasswordByEmail(string email, string newPlainTextPassword)
        {
            try
            {
                // Find user in linked list
                usersBL user = UsersList.FindUserByEmail(email);
                if (user == null)
                {
                    MessageBox.Show("No account found with this email address!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Hash the new password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPlainTextPassword);

                // Update in database
                string query = "UPDATE users SET Password = @password WHERE Email = @email";

                using (MySqlConnection con = DatabaseHelper.Instance.getConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Also update the user in the linked list
                        user.UpdatePasswordHash(hashedPassword);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            // Pattern: At least one letter, one digit, one special character, minimum 8 characters
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
