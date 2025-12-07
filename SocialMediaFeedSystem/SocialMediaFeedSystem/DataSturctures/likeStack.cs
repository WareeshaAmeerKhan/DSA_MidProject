using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaFeedSystem.BL;

namespace SocialMediaFeedSystem.DataSturctures
{
    public class LikeNode
    {
        public likesBL Data;
        public LikeNode Next;

        public LikeNode(likesBL data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LikeStack
    {
        private LikeNode top;

        public LikeStack()
        {
            top = null;
        }

        public LikeNode Top => top;

        public bool IsEmpty()
        {
            return top == null;
        }

        public void Push(likesBL like)
        {
            LikeNode newNode = new LikeNode(like);
            newNode.Next = top;
            top = newNode;
        }

        public likesBL Pop()
        {
            if (IsEmpty())
                return null;

            likesBL data = top.Data;
            top = top.Next;
            return data;
        }

        public likesBL Peek()
        {
            if (IsEmpty())
                return null;
            return top.Data;
        }

        public int Count()
        {
            int count = 0;
            LikeNode temp = top;
            while (temp != null)
            {
                count++;
                temp = temp.Next;
            }
            return count;
        }

        public void Clear()
        {
            top = null;
        }

        // Check if user has already liked a post
        public bool HasUserLikedPost(int userID, int postID)
        {
            LikeNode temp = top;
            while (temp != null)
            {
                if (temp.Data.UserID == userID && temp.Data.PostID == postID)
                    return true;
                temp = temp.Next;
            }
            return false;
        }

        // Remove like by user and post
        public likesBL RemoveLike(int userID, int postID)
        {
            if (IsEmpty()) return null;

            // If the top node is the one to remove
            if (top.Data.UserID == userID && top.Data.PostID == postID)
            {
                return Pop();
            }

            LikeNode current = top;
            LikeNode previous = null;

            while (current != null && !(current.Data.UserID == userID && current.Data.PostID == postID))
            {
                previous = current;
                current = current.Next;
            }

            if (current == null) return null; // Like not found

            // Remove the node
            previous.Next = current.Next;
            return current.Data;
        }

        // Get like count for a specific post
        public int GetLikeCountForPost(int postID)
        {
            int count = 0;
            LikeNode temp = top;
            while (temp != null)
            {
                if (temp.Data.PostID == postID)
                    count++;
                temp = temp.Next;
            }
            return count;
        }

        // Get all likes for a specific post
        public List<likesBL> GetLikesForPost(int postID)
        {
            List<likesBL> likes = new List<likesBL>();
            LikeNode temp = top;
            while (temp != null)
            {
                if (temp.Data.PostID == postID)
                    likes.Add(temp.Data);
                temp = temp.Next;
            }
            return likes;
        }

        // Get all likes (for database synchronization)
        public List<likesBL> GetAllLikes()
        {
            List<likesBL> allLikes = new List<likesBL>();
            LikeNode temp = top;
            while (temp != null)
            {
                allLikes.Add(temp.Data);
                temp = temp.Next;
            }
            return allLikes;
        }
    }
}
