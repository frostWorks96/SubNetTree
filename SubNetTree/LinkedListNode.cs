using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubNetTree
{
    public class LinkedListNode
    {
        BinaryTreeeNode node;
        LinkedListNode next;
        public LinkedListNode(BinaryTreeeNode node)
        {
            this.node = node;
            next = null;
        }
        public void setNext(LinkedListNode node)
        {
            next = node;
        }
        public void setNext(BinaryTreeeNode node)
        {
            if (this.node == null) this.node = node; 
            next = new LinkedListNode(node);
        }
        public LinkedListNode getNext()
        {
            return next;
        }
        public BinaryTreeeNode getData()
        {
            return node;
        }
    }
}
