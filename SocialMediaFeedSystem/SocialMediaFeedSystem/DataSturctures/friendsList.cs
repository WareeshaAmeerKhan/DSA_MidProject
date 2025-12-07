using SocialMediaFeedSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.DataSturctures
{
    // Node structure for linked list
    public class FriendNode
    {
        public friendsBL Data { get; set; }
        public FriendNode Next { get; set; }

        public FriendNode(friendsBL data)
        {
            Data = data;
            Next = null;
        }
    }

    // Linked List class for friends management
    public class FriendsLinkedList
    {
        private FriendNode head;

        // Make Head property settable
        public FriendNode Head
        {
            get => head;
            set => head = value;
        }

        public int Count { get; private set; }

        public FriendsLinkedList()
        {
            head = null;
            Count = 0;
        }

        // Insert at end
        public void InsertAtEnd(friendsBL data)
        {
            FriendNode newNode = new FriendNode(data);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                FriendNode temp = head;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = newNode;
            }
            Count++;
        }

        // Insert at head
        public void InsertAtHead(friendsBL data)
        {
            FriendNode newNode = new FriendNode(data);
            newNode.Next = head;
            head = newNode;
            Count++;
        }

        // Delete node by UserID
        public bool DeleteNode(int userId)
        {
            if (head == null) return false;

            // If head needs to be deleted
            if (head.Data.UserID == userId)
            {
                head = head.Next;
                Count--;
                return true;
            }

            FriendNode temp = head;
            while (temp.Next != null)
            {
                if (temp.Next.Data.UserID == userId)
                {
                    temp.Next = temp.Next.Next;
                    Count--;
                    return true;
                }
                temp = temp.Next;
            }
            return false;
        }

        // Delete node by RequestID (for friend requests)
        public bool DeleteNodeByRequestId(int requestId)
        {
            if (head == null) return false;

            if (head.Data.RequestID == requestId)
            {
                head = head.Next;
                Count--;
                return true;
            }

            FriendNode temp = head;
            while (temp.Next != null)
            {
                if (temp.Next.Data.RequestID == requestId)
                {
                    temp.Next = temp.Next.Next;
                    Count--;
                    return true;
                }
                temp = temp.Next;
            }
            return false;
        }

        // Find node by UserID
        public friendsBL FindNode(int userId)
        {
            FriendNode temp = head;
            while (temp != null)
            {
                if (temp.Data.UserID == userId)
                    return temp.Data;
                temp = temp.Next;
            }
            return null;
        }

        // Find node by RequestID
        public friendsBL FindNodeByRequestId(int requestId)
        {
            FriendNode temp = head;
            while (temp != null)
            {
                if (temp.Data.RequestID == requestId)
                    return temp.Data;
                temp = temp.Next;
            }
            return null;
        }

        // Check if user exists in list
        public bool Contains(int userId)
        {
            return FindNode(userId) != null;
        }

        // Convert linked list to regular list (for UI display)
        public List<friendsBL> ToList()
        {
            var list = new List<friendsBL>();
            FriendNode temp = head;
            while (temp != null)
            {
                list.Add(temp.Data);
                temp = temp.Next;
            }
            return list;
        }

        // Clear the linked list
        public void Clear()
        {
            head = null;
            Count = 0;
        }

        // Add this method to your FriendsLinkedList class
        public List<friendsBL> LinearSearchByUsername(string searchTerm)
        {
            var results = new List<friendsBL>();
            int comparisons = 0;

            FriendNode current = head;
            while (current != null)
            {
                comparisons++;

                // Case-insensitive partial match
                if (current.Data.Username.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(current.Data);
                }

                current = current.Next;
            }

            // Optional: Debug output to see search performance
            Console.WriteLine($"Linear Search: Found {results.Count} matches in {comparisons} comparisons");

            return results;
        }
    }
}