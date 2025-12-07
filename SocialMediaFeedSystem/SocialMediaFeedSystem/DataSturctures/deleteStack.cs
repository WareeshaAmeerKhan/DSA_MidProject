using SocialMediaFeedSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.DataSturctures
{
    internal class DeleteNode
    {
        public PostWithAssociatedData Data;
        public DeleteNode Next;

        public DeleteNode(PostWithAssociatedData data)
        {
            Data = data;
            Next = null;
        }
    }

    internal static class deleteStack
    {
        private static DeleteNode top;

        public static bool IsEmpty()
        {
            return top == null;
        }

        public static void Push(PostWithAssociatedData postData)
        {
            DeleteNode newNode = new DeleteNode(postData);
            newNode.Next = top;
            top = newNode;
        }

        public static PostWithAssociatedData Pop()
        {
            if (IsEmpty())
                return null;

            PostWithAssociatedData data = top.Data;
            top = top.Next;
            return data;
        }

        public static void Clear()
        {
            top = null;
        }
    }

}
