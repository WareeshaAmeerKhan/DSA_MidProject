using SocialMediaFeedSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaFeedSystem.DataSturctures;

namespace SocialMediaFeedSystem.DL
{
    public static class FriendSorter
    {
        // Main sorting method - uses Merge Sort (the best algorithm)
        public static void SortFriends(FriendsLinkedList list, bool sortByUsername = true)
        {
            if (list.Head == null || list.Head.Next == null) return;

            if (sortByUsername)
            {
                // Set the head to the sorted list
                list.Head = MergeSortByUsername(list.Head);
            }
            else
            {
                list.Head = MergeSortByFriendshipDate(list.Head);
            }
        }

        // Merge Sort by Username (A-Z)
        private static FriendNode MergeSortByUsername(FriendNode head)
        {
            // Base case: empty list or single node
            if (head == null || head.Next == null) return head;

            // Split the list into two halves
            FriendNode middle = GetMiddle(head);
            FriendNode nextOfMiddle = middle.Next;
            middle.Next = null;

            // Recursively sort both halves
            FriendNode left = MergeSortByUsername(head);
            FriendNode right = MergeSortByUsername(nextOfMiddle);

            // Merge the sorted halves
            return MergeByUsername(left, right);
        }

        // Merge Sort by Friendship Date (Oldest First)
        private static FriendNode MergeSortByFriendshipDate(FriendNode head)
        {
            if (head == null || head.Next == null) return head;

            FriendNode middle = GetMiddle(head);
            FriendNode nextOfMiddle = middle.Next;
            middle.Next = null;

            FriendNode left = MergeSortByFriendshipDate(head);
            FriendNode right = MergeSortByFriendshipDate(nextOfMiddle);

            return MergeByFriendshipDate(left, right);
        }

        // Helper method to find middle of list for splitting
        private static FriendNode GetMiddle(FriendNode head)
        {
            if (head == null) return head;

            FriendNode slow = head;
            FriendNode fast = head.Next;

            // Move fast pointer twice as fast as slow pointer
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }

        // Merge two sorted lists by Username
        private static FriendNode MergeByUsername(FriendNode left, FriendNode right)
        {
            // Base cases
            if (left == null) return right;
            if (right == null) return left;

            FriendNode result;

            // Compare usernames and merge
            if (string.Compare(left.Data.Username, right.Data.Username, StringComparison.OrdinalIgnoreCase) <= 0)
            {
                result = left;
                result.Next = MergeByUsername(left.Next, right);
            }
            else
            {
                result = right;
                result.Next = MergeByUsername(left, right.Next);
            }

            return result;
        }

        // Merge two sorted lists by Friendship Date
        private static FriendNode MergeByFriendshipDate(FriendNode left, FriendNode right)
        {
            if (left == null) return right;
            if (right == null) return left;

            FriendNode result;

            // Compare friendship dates and merge (ascending order - oldest first)
            if (left.Data.FriendshipDate <= right.Data.FriendshipDate)
            {
                result = left;
                result.Next = MergeByFriendshipDate(left.Next, right);
            }
            else
            {
                result = right;
                result.Next = MergeByFriendshipDate(left, right.Next);
            }

            return result;
        }
    }
}