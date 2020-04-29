using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubNetTree
{
    public class BinTree
    {
        private BinaryTreeeNode root; 
        public BinTree(BinaryTreeeNode n)
        {
            root = n; 
        }  
        public BinTree(string ipAddress, int sbnm, char c, MainWindow mainWindow)
        {
             
            root = new BinaryTreeeNode(new IP_SubNetButton(new Subnet.SubNet(ipAddress, sbnm), mainWindow), c, mainWindow);
        }
        

        public BinaryTreeeNode findChilds_parent(BinaryTreeeNode kid, BinaryTreeeNode node)
        {
            if (node == null) return null;
            if (node.left == kid || node.right == kid) return node;
            
            findChilds_parent(kid, node.right);
            return findChilds_parent(kid, node.left); 

        } 
        public int maxDepth(BinaryTreeeNode node)
        {
            if (node == null)
                return -1;
            else
            {
                /* compute the depth of each subtree */
                int lDepth = maxDepth(node.left);
                int rDepth = maxDepth(node.right);

                /* use the larger one */
                if (lDepth > rDepth)
                    return (lDepth + 1);
                else
                    return (rDepth + 1);
            }
        }
        public LinkedList[] printTreeRight(BinaryTreeeNode n, LinkedList [] list)
        { 
            if (n != null)
            { 
                if(n.left!=null)
                 
                if (n.left != null &&
                n.left.getButton().Location.X   <=
                (root.getButton().Location.X + root.getButton().Width))
                { 
                    while (n.left.getButton().Location.X <=
                            (root.getButton().Location.X + root.getButton().Width))
                    {
                        n.left.getButton().Location = new System.Drawing.Point(
                           n.left.getButton().Location.X +
                           1,
                            n.left.getButton().Location.Y);

                    }
                    n.right.getButton().Location = new System.Drawing.Point(
                        n.left.getButton().Location.X +
                        n.left.getButton().Width + 
                        n.getButton().Width + 10,
                        n.right.getButton().Location.Y);
                    n.get_ip_subnetbutton().centerButtonBetweenChldren(n);
                    BinaryTreeeNode temtBTN = findChilds_parent(n, root.right);
                    if (temtBTN != null)
                    {
                        if (n == temtBTN.left)
                            temtBTN.right.getButton().Location = new System.Drawing.Point(
                                n.getButton().Location.X +
                                temtBTN.getButton().Width +
                                temtBTN.right.getButton().Width,
                                temtBTN.right.getButton().Location.Y);
                        else
                        {
                            temtBTN.left.getButton().Location = new System.Drawing.Point(
                            n.getButton().Location.X +
                            temtBTN.getButton().Width +
                            temtBTN.left.getButton().Width,
                            temtBTN.left.getButton().Location.Y);
                        }
                    }
                }
                printTreeRight(n.left, list);
                printTreeRight(n.right, list); 
                list[n.getSubNetMask() - root.getSubNetMask() - 1].add(n); 

            }
            return list;
        }
        public LinkedList [] printTreeLeft(BinaryTreeeNode n, LinkedList [] list)
        {
            if (n != null)
            {
                if (n.right != null &&
                (n.right.getButton().Location.X +
                n.right.getButton().Width) >
                root.getButton().Location.X)
                {
                    while ((n.right.getButton().Location.X +
                    n.right.getButton().Width) >
                    root.getButton().Location.X)
                    {
                        n.right.getButton().Location = new System.Drawing.Point(
                           n.right.getButton().Location.X -
                           1,
                            n.right.getButton().Location.Y);

                    } 
                    n.left.getButton().Location = new System.Drawing.Point(
                        n.right.getButton().Location.X - 
                        n.left.getButton().Width - 
                        n.getButton().Width - 10,
                        n.left.getButton().Location.Y); 
                    n.get_ip_subnetbutton().centerButtonBetweenChldren(n);
                    BinaryTreeeNode temtBTN = findChilds_parent(n, root.left);
                   if (temtBTN != null)
                    {
                        if (n == temtBTN.right)
                            temtBTN.left.getButton().Location = new System.Drawing.Point(
                                n.getButton().Location.X -
                                temtBTN.left.getButton().Width,
                                temtBTN.left.getButton().Location.Y);
                        else
                        {
                            temtBTN.right.getButton().Location = new System.Drawing.Point(
                            n.getButton().Location.X -
                            temtBTN.getButton().Width -
                            temtBTN.right.getButton().Width,
                            temtBTN.right.getButton().Location.Y);
                        }
                    }
                }
                printTreeLeft(n.right, list);
                printTreeLeft(n.left, list); 
                list[n.getSubNetMask()-root.getSubNetMask()-1].add(n); 

            }
            return list;
        }
        public BinaryTreeeNode getRoot() { 
            return root;
         }
        /*  private void addNode(Node n, string ipAddress, int sbnm)
          *  desc: calls add node sending in thr root
          *  param: the ip and subnet mask 
          */
        public void addNode(string ipAddress, int sbnm)
        {
            addNode(root, ipAddress, sbnm);
        }
        /*  private void addNode(Node n, string ipAddress, int sbnm)
         *  desc: find the button that was clicked and subnet it 
         *  param: the Node to perform recurtion and the ipAddress 
         *         and sbnm of the button clicked
         *  **recursive**
         */
        private void addNode(BinaryTreeeNode n, string ipAddress, int sbnm)
        {
            int resalt = compareToIpAddress(n.getSubNet().GetIP(), ipAddress);
            if (resalt == 0)  n.setSubnet(ipAddress, sbnm + ""); 
            else if (resalt == -1) addNode(n.left, ipAddress, sbnm);
            else if(resalt == 1) addNode(n.right, ipAddress, sbnm);
        }
        /* private int compareToIpAddress(string ip_original, string ip_new)
         *  desc: figure out wich ipaddress is bigger
         *  
         *  param: 2 ip address the ip that exsists and one that is being added
         *  
         *  return: return -1 if the new ip is less than the orinal
         *          return 1 if the new ip is bigger
         *          return 0 if they are equal;
         */
        private int compareToIpAddress(string ip_original, string ip_new)
        {
            string[] nybbles_original = ip_original.Split('.');
            string[] nybbles_new = ip_new.Split('.');
            for(int i = 0; i < 4; i++)
            {
                int newNybble = int.Parse(nybbles_new[i]);
                int originalNybble = int.Parse(nybbles_original[i]);
                if (newNybble < originalNybble) return -1;
                else if (newNybble > originalNybble) return 1; 
            }
            return 0;
        } 
    }
}
