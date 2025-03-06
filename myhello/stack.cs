using System;
using System.Collections.Generic;

namespace myhello
{
    public class StackNode<T>
    {
        public T value;
        public StackNode<T> next;
        public StackNode(T value)
        {
            this.value = value;
            this.next = null;
        }
    }
    public class CustomStack<T>
    {
        private StackNode<T> top;
        public CustomStack()
        {
            this.top = null;
        }
        public void Push(T value)
        {
            StackNode<T> newNode = new StackNode<T>(value);
            newNode.next = top;
            top = newNode;
        }
        public T Pop()
        {
            if (top == null)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            T value = top.value;
            top = top.next;
            return value;
        }
        public void Display()
        {
            StackNode<T> temp = top;
            while (temp != null)
            {
                Console.WriteLine(temp.value);
                temp = temp.next;
            }
        }
    }
}