using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.BL
{
    public class usersBL
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; private set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public string ProfilePicPath { get; set; }

        // Constructor for existing users (from database)
        public usersBL(int userID, string username, string email, string passwordHash, DateTime dOB, string bio, string profilePicPath)
        {
            UserID = userID;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            DOB = dOB;
            Bio = bio;
            ProfilePicPath = profilePicPath;
        }

        // Constructor for new users (during signup)
        public usersBL(string username, string email, string password, DateTime dob, string bio, string profilePic)
        {
            Username = username;
            Email = email;
            SetPassword(password); // Use SetPassword to hash it
            DOB = dob;
            Bio = bio;
            ProfilePicPath = profilePic;
        }

        // Hash password using BCrypt
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Set new password (hashes it automatically)
        public void SetPassword(string plainTextPassword)
        {
            if (string.IsNullOrEmpty(plainTextPassword))
                throw new ArgumentException("Password cannot be null or empty");

            PasswordHash = HashPassword(plainTextPassword);
        }

        // Update password hash directly (for database operations)
        public void UpdatePasswordHash(string newHash)
        {
            if (string.IsNullOrEmpty(newHash))
                throw new ArgumentException("Password hash cannot be null or empty");

            PasswordHash = newHash;
        }
    }
}
