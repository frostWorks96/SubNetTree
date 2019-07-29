using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubNetTree
{
    public class Node
    {
        IP_SubNetButton ip_SubNetButton;
        public Node next;
        public Node(IP_SubNetButton ip, Node n)
        {
            ip_SubNetButton = ip;
            next = n;
        }
        public Node(IP_SubNetButton ip)
        {
            ip_SubNetButton = ip; 
        }
        public IP_SubNetButton GetIP()
        {
            return ip_SubNetButton;
        }
    }
}
