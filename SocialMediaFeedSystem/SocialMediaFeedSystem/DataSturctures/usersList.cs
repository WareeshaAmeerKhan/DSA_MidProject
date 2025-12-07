using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    // Node class for singly linked list
    public class UserNode
    {
        public usersBL Data { get; set; }
        public UserNode Next { get; set; }

        public UserNode(usersBL data)
        {
            Data = data;
            Next = null;
        }
    }

    // Linked List class for users
    public class UserLinkedList
    {
        private UserNode head;

        public UserNode Head => head;

        public UserLinkedList()
        {
            head = null;
        }

        // Check if list is empty
        public bool IsEmpty()
        {
            return head == null;
        }

        // Insert at end
        public void InsertAtEnd(usersBL user)
        {
            UserNode newNode = new UserNode(user);

            if (head == null)
            {
                head = newNode;
                return;
            }

            UserNode temp = head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = newNode;
        }

        // Find user by username or email (for login)
        public usersBL FindUser(string input)
        {
            UserNode temp = head;
            while (temp != null)
            {
                if (temp.Data.Username == input || temp.Data.Email == input)
                    return temp.Data;
                temp = temp.Next;
            }
            return null;
        }

        // Add this method to your UserLinkedList class if it doesn't exist
        public usersBL FindUserByEmail(string email)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.Data.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    return current.Data;
                current = current.Next;
            }
            return null;
        }

        // Check if username or email already exists (for signup)
        public bool UserExists(string username, string email)
        {
            UserNode temp = head;
            while (temp != null)
            {
                if (temp.Data.Username == username || temp.Data.Email == email)
                    return true;
                temp = temp.Next;
            }
            return false;
        }

        // Display all users (for debugging)
        public void DisplayList()
        {
            UserNode temp = head;
            while (temp != null)
            {
                Console.WriteLine($"User: {temp.Data.Username}, Email: {temp.Data.Email}");
                temp = temp.Next;
            }
        }

        // Find user by ID
        public usersBL FindUserByID(int userID)
        {
            UserNode temp = head;
            while (temp != null)
            {
                if (temp.Data.UserID == userID)
                    return temp.Data;
                temp = temp.Next;
            }
            return null;
        }

        // Update user in list
        public bool UpdateUser(usersBL updatedUser)
        {
            UserNode temp = head;
            while (temp != null)
            {
                if (temp.Data.UserID == updatedUser.UserID)
                {
                    temp.Data = updatedUser;
                    return true;
                }
                temp = temp.Next;
            }
            return false;
        }

        // Clear the list
        public void Clear()
        {
            head = null;
        }
    }
}
