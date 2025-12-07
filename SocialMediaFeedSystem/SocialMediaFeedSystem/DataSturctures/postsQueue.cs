using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFeedSystem.BL
{
    internal class PostNode
    {
        public postsBL Data;
        public PostNode Next;

        public PostNode(postsBL data)
        {
            Data = data;
            Next = null;
        }
    }

    internal class PostQueue
    {
        private PostNode front;
        private PostNode rear;

        public PostQueue() { front = rear = null; }
        public PostNode Front => front;
        public PostNode Rear => rear;

        public bool IsEmpty() => front == null;

        public void Enqueue(postsBL post)
        {
            PostNode newNode = new PostNode(post);

            if (rear == null)
            {
                front = rear = newNode;
                return;
            }

            rear.Next = newNode;
            rear = newNode;
        }

        public postsBL Dequeue()
        {
            if (front == null) return null;

            postsBL data = front.Data;
            front = front.Next;

            if (front == null) rear = null;
            return data;
        }

        public bool RemovePost(int postID)
        {
            if (front == null) return false;

            if (front.Data.PostID == postID)
            {
                front = front.Next;
                if (front == null) rear = null;
                return true;
            }

            PostNode current = front;
            while (current.Next != null && current.Next.Data.PostID != postID)
                current = current.Next;

            if (current.Next == null) return false;

            current.Next = current.Next.Next;
            if (current.Next == null) rear = current;
            return true;
        }

        public void InsertInOrder(postsBL post)
        {
            PostNode newNode = new PostNode(post);

            if (front == null || post.PostDate <= front.Data.PostDate)
            {
                newNode.Next = front;
                front = newNode;
                if (rear == null) rear = newNode;
                return;
            }

            if (post.PostDate >= rear.Data.PostDate)
            {
                rear.Next = newNode;
                rear = newNode;
                return;
            }

            PostNode current = front;
            while (current.Next != null && current.Next.Data.PostDate < post.PostDate)
                current = current.Next;

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        // Linear Search for posts by content (keyword search)
        public List<postsBL> LinearSearchByContent(string searchTerm)
        {
            var results = new List<postsBL>();
            int comparisons = 0;

            PostNode current = front;
            while (current != null)
            {
                comparisons++;

                // Case-insensitive partial match in post content
                if (current.Data.Content.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(current.Data);
                }

                current = current.Next;
            }

            // Debug output
            Console.WriteLine($"Post Search: Found {results.Count} matches in {comparisons} comparisons");

            return results;
        }

        // Add this method to your PostQueue class
        // Linear Search for posts by username (post owner)
        public List<postsBL> LinearSearchByUsername(string searchTerm, Func<int, string> getUserName)
        {
            var results = new List<postsBL>();
            int comparisons = 0;

            PostNode current = front;
            while (current != null)
            {
                comparisons++;

                // Get username using the provided function and check for match
                string username = getUserName(current.Data.UserID);
                if (username.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(current.Data);
                }

                current = current.Next;
            }

            return results;
        }

        // Sort by Latest (Newest first) - using Merge Sort (O(n log n))
        // Sort by Latest (Newest first) - using Merge Sort (O(n log n))
        public void SortByLatest()
        {
            if (front == null || front.Next == null) return;

            front = MergeSortByLatest(front);

            // Update rear pointer after sorting
            rear = front;
            while (rear != null && rear.Next != null)
                rear = rear.Next;
        }

        // Merge Sort by Latest (PostDate descending - newest first)
        private PostNode MergeSortByLatest(PostNode head)
        {
            if (head == null || head.Next == null) return head;

            // Split the list into two halves
            PostNode middle = GetMiddle(head);
            PostNode nextOfMiddle = middle.Next;
            middle.Next = null;

            // Recursively sort both halves
            PostNode left = MergeSortByLatest(head);
            PostNode right = MergeSortByLatest(nextOfMiddle);

            // Merge the sorted halves (descending order - newest first)
            return MergeByLatest(left, right);
        }

        private PostNode MergeByLatest(PostNode left, PostNode right)
        {
            if (left == null) return right;
            if (right == null) return left;

            PostNode result;

            // Compare by PostDate (descending - newest first)
            if (left.Data.PostDate >= right.Data.PostDate)
            {
                result = left;
                result.Next = MergeByLatest(left.Next, right);
            }
            else
            {
                result = right;
                result.Next = MergeByLatest(left, right.Next);
            }

            return result;
        }

        // Sort by Trending (Likes + Comments descending) - using Merge Sort (O(n log n))
        public void SortByTrending()
        {
            if (front == null || front.Next == null) return;

            front = MergeSortByTrending(front);

            // Update rear pointer after sorting
            rear = front;
            while (rear != null && rear.Next != null)
                rear = rear.Next;
        }

        // Merge Sort by Trending (Engagement descending)
        private PostNode MergeSortByTrending(PostNode head)
        {
            if (head == null || head.Next == null) return head;

            // Split the list into two halves
            PostNode middle = GetMiddle(head);
            PostNode nextOfMiddle = middle.Next;
            middle.Next = null;

            // Recursively sort both halves
            PostNode left = MergeSortByTrending(head);
            PostNode right = MergeSortByTrending(nextOfMiddle);

            // Merge the sorted halves by engagement (descending)
            return MergeByTrending(left, right);
        }

        private PostNode MergeByTrending(PostNode left, PostNode right)
        {
            if (left == null) return right;
            if (right == null) return left;

            PostNode result;

            // Calculate engagement scores
            int leftEngagement = left.Data.Likes + left.Data.Comments;
            int rightEngagement = right.Data.Likes + right.Data.Comments;

            // Compare by engagement (descending - highest first)
            if (leftEngagement >= rightEngagement)
            {
                result = left;
                result.Next = MergeByTrending(left.Next, right);
            }
            else
            {
                result = right;
                result.Next = MergeByTrending(left, right.Next);
            }

            return result;
        }

        // Helper method to find middle of list for splitting (used in merge sort)
        private PostNode GetMiddle(PostNode head)
        {
            if (head == null) return head;

            PostNode slow = head;
            PostNode fast = head.Next;

            // Move fast pointer twice as fast as slow pointer
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }

        // Convert queue to list for easier manipulation
        public List<postsBL> ToList()
        {
            var list = new List<postsBL>();
            PostNode current = front;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }
    }
}
