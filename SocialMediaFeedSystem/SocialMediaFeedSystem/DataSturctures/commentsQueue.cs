using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.BL
{
    public class CommentNode
    {
        public commentsBL Data;
        public CommentNode Next;

        public CommentNode(commentsBL data)
        {
            Data = data;
            Next = null;
        }
    }

    public class CommentQueue
    {
        private CommentNode front;
        private CommentNode rear;

        public CommentQueue() { front = rear = null; }
        public CommentNode Front => front;
        public bool IsEmpty() => front == null;

        public void Enqueue(commentsBL comment)
        {
            CommentNode newNode = new CommentNode(comment);

            if (rear == null)
            {
                front = rear = newNode;
                return;
            }

            rear.Next = newNode;
            rear = newNode;
        }

        public commentsBL Dequeue()
        {
            if (front == null) return null;

            commentsBL data = front.Data;
            front = front.Next;

            if (front == null) rear = null;
            return data;
        }

        public int Count()
        {
            int count = 0;
            CommentNode temp = front;
            while (temp != null)
            {
                count++;
                temp = temp.Next;
            }
            return count;
        }
    }
}
