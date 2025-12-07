using SocialMediaFeedSystem.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaFeedSystem.DataSturctures
{
    // Node for the stack
    public class StackNode
    {
        public NavigationItem Data { get; set; }
        public StackNode Next { get; set; }

        public StackNode(NavigationItem data)
        {
            Data = data;
            Next = null;
        }
    }

    // Manual Stack implementation using linked list
    public class NavigationStack
    {
        private StackNode top;
        public int Count { get; private set; }

        public NavigationStack()
        {
            top = null;
            Count = 0;
        }

        // Push operation
        public void Push(NavigationItem data)
        {
            StackNode newNode = new StackNode(data);
            newNode.Next = top;
            top = newNode;
            Count++;
        }

        // Pop operation
        public NavigationItem Pop()
        {
            if (IsEmpty())
            {
                return null;
            }

            StackNode temp = top;
            NavigationItem item = temp.Data;
            top = top.Next;
            Count--;
            return item;
        }

        // Peek operation
        public NavigationItem Peek()
        {
            if (IsEmpty())
            {
                return null;
            }
            return top.Data;
        }

        // Check if stack is empty
        public bool IsEmpty()
        {
            return top == null;
        }

        // Get stack size
        public int Size()
        {
            return Count;
        }

        // Clear the stack
        public void Clear()
        {
            while (!IsEmpty())
            {
                Pop();
            }
        }

        // Display stack for debugging (optional)
        public void DisplayStack()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty!");
                return;
            }

            StackNode current = top;
            Console.Write("Stack (top to bottom): ");
            while (current != null)
            {
                Console.Write($"{current.Data.FormType.Name} -> ");
                current = current.Next;
            }
            Console.WriteLine("END");
        }
    }
}
