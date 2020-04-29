using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubNetTree
{
    public class LinkedList
    {
        LinkedListNode head, tail;
        public LinkedList(LinkedListNode n)
        {
            head = n;
            tail = n;

        }
        public LinkedList()
        {
            head = null;
            tail = new LinkedListNode(null);
        }
        public LinkedListNode getHead()
        {
            return head;
        }
        public void setHead(LinkedListNode n)
        {
            head = n;
        }
        public void setHead(BinaryTreeeNode n)
        {
            head = new LinkedListNode(n);
        }
        public LinkedListNode GetTail()
        {
            return tail;
        }
        public void add(BinaryTreeeNode node)
        {
            if (!contains(node))
            {
                tail.setNext(node);
                if (head == null)
                    head = tail;
                tail = tail.getNext();
            }
        }

        public bool contains(BinaryTreeeNode node)
        {
            return contains(head, node);
        }
        private bool contains(LinkedListNode n, BinaryTreeeNode node)
        {
            if (n == null) return false;
            if (n.getData() == node) return true;
            return contains(n.getNext(), node);
        }
    }
}
