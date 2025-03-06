using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class MyLinkList<T> where T : struct
    {
        Node<T> Head = null;
        public void DisplayList()
        {
            Node<T> temp = Head;
            while (temp != null)
            {
                Console.WriteLine(temp.val);
                temp = temp.next;
            }
        }

        public void AddElement(T input)
        {
            if (Head == null)
            {
                Head = new Node<T>
                {
                    val = input
                };
            }

            else
            { 
                Node<T> temp = Head;
                while (temp.next != null)
                {
                    temp = temp.next;
                }
                temp.next = new Node<T>()
                {
                    val = input
                };
            }
        }
    }

    public class Node<T>
    {
        public T val;
        public Node<T> next;

    }
}
