using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubNetTree
{
    public class LinkedList
    {
        Node head, tail;
        public LinkedList(Node n)
        {
            head = n;
            tail = n;

        }
        public void Add(Node n)
        {
            tail.next = n;
            tail = tail.next;
        }
        public void AddFront(Node n)
        {
            Node temp = head;
            head = n;
            head.next = temp;
        }
        public Node GetHead()
        {
            return head;
        }
        public Node GetTail()
        {
            return tail;
        }
    }
}
