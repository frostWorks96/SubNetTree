
using System.Windows.Forms;
using Subnet;
namespace SubNetTree
{
    public class IP_SubNetButton
    {

        private Button button;
        private Subnet.SubNet subNet;
        private MainWindow mainVars;

        public IP_SubNetButton(SubNet subNet)
        {
            this.button = new System.Windows.Forms.Button();
            this.subNet = subNet;
            button.Text = subNet.GetIP() + "/" + subNet.GetSubnetMask();
            button.AutoSize = true;
        }
        public IP_SubNetButton(SubNet subNet, MainWindow mw)
        {
            mainVars = mw;
            this.button = new System.Windows.Forms.Button();
            this.subNet = subNet;
            button.Text = subNet.GetIP() + "/" + subNet.GetSubnetMask();
            button.AutoSize = true;
        }
        public IP_SubNetButton(string ip, int sbnm)
        {
            this.button = new System.Windows.Forms.Button();
            this.subNet = subNet;
            button.Text = subNet.GetIP() + "/" + subNet.GetSubnetMask();
            button.AutoSize = true;
        }
        public Button GetButton()
        {
            return button;
        }

        public Subnet.SubNet GetSubNet()
        {
            return subNet;
        }
        public void SetButton(Button b)
        {
            button = b;
        }
        public void SetSubNet(Subnet.SubNet s)
        {
            subNet = s;
        }
        /*   public static string splitSubNetLeft(string ip, string sub)
        *   desc: from an ip adress and the subnet mask you split the ipaddress for 
        *   the subnet tree (subnetting)
        *   
        *   param: a ip address and the subnet mask
        *   
        *   return the subneted ip address, the lower aspect
        */
        public static string splitSubNetLeft(string ip, string sub)
        {

            int index = FindIndexOfNybbles(sub);
            int subN = FindSubN(sub);
            string[] nybbles = ip.Split('.');


            int minOf1 = FindMinDec(nybbles[index], subN);
            int minOF2 = FindMaxDec(nybbles[index], subN);
            nybbles[index] = minOf1 + "";

            int num = int.Parse(sub);
            num++;
            sub = "/" + num;
            string iptemp = nybbles[0] + "." + nybbles[1] + "." + nybbles[2] + "." + nybbles[3];

            return iptemp;

        }
        
        /*   public static string splitSubNetRight(string ip, string sub)
         *   desc: from an ip adress and the subnet mask you split the ipaddress for 
         *   the subnet tree (subnetting)
         *   
         *   param: a ip address and the subnet mask
         *   
         *   return the subneted ip address, the higher aspect
         */
        public static string splitSubNetRight(string ip, string sub)
        {

            int index = FindIndexOfNybbles(sub);
            int subN = FindSubN(sub);
            string[] nybbles = ip.Split('.');


            int minOf1 = FindMinDec(nybbles[index], subN);
            int minOF2 = FindMaxDec(nybbles[index], subN);
            nybbles[index] = minOf1 + "";

            int num = int.Parse(sub);
            num++;
            sub = "/" + num;
            string iptemp = nybbles[0] + "." + nybbles[1] + "." + nybbles[2] + "." + nybbles[3];


            nybbles[index] = minOF2 + "";
            iptemp = nybbles[0] + "." + nybbles[1] + "." + nybbles[2] + "." + nybbles[3];
            return iptemp;

        }
        /* public static int FindIndexOfNybbles(string sub)
         *  desc: finds the indexd of the Nibble 
         * 
         *  para: the subnet mask
         *  
         *  returns: the index that can be changed or modifed 
         */
        public static int FindIndexOfNybbles(string sub)
        {
            string t = sub;
            int num = int.Parse(sub);
            int index = 0;
            for (; index < 4; index++)
            {
                if (num >= 8)
                {
                    num -= 8;
                }
                else
                {
                    break;
                }
            }
            return index;
        }
        /* public static int FindSubN(string sub)
         *  desc: calculate the value of the subnet mask 
         *  to see how much can be modified in the ip address
         *  
         *  pram: the subnet mask
         *  
         *  return the leftover subnet mask
         */
        public static int FindSubN(string sub)
        {
            int num = int.Parse(sub);
            int index = 0;
            for (; index < 4; index++)
            {
                if (num >= 8)
                {
                    num -= 8;
                }
                else
                {
                    break;
                }
            }
            return num;
        }
        /*public static int FindMinDec(string str, int index)
         *  desc: find the samllest number the nibble can become
         *  
         *  param: the nyble that the subnet mask points to and the leftover 
         *  subnet number from FindSubnetN
         *  
         *  returns the smallest the subnet can be
         */
        public static int FindMinDec(string str, int index)
        {
            int nyble = int.Parse(str);
            int sum = 0;
            int byte_ = 128;
            for (int i = 0; i < index; i++)
            {
                if (nyble >= byte_)
                {
                    sum += byte_;
                    nyble -= byte_;
                }
                else if (nyble == 0) break;
                byte_ /= 2;
            }
            return sum;
        }
        /* public static int FindMaxDec(string str, int index)
         *  desc: find the largest number the nibble can become
         *  
         *  param: the nyble that the subnet mask points to and the leftover 
         *  subnet number from FindSubnetN
         *  
         *  returns the largest the subnet can be we than add one 
         *  and it is now the other subnets miniumum
         */
        public static int FindMaxDec(string str, int index)
        {

            int nyble = int.Parse(str);
            int sum = 0;
            int byte_ = 128;
            for (int i = 0; i < 8; i++)
            {
                if (i >= index)
                {
                    sum += byte_;
                    break;
                }
                else if (nyble >= byte_)
                {
                    sum += byte_;
                    nyble -= byte_;
                }

                byte_ /= 2;
            }

            return sum;
        }
        /*
         *
         *
         *
         */
        public void centerButtonBetweenChldren(BinaryTreeeNode parent)
        {
            if (parent == null) return;
            if (parent.left != null && parent.right != null)
            {
                parent.getButton().Location = new System.Drawing.Point(
                    (parent.left.getButton().Location.X +
                    parent.right.getButton().Location.X) / 2,
                     parent.getButton().Location.Y);
                mainVars.getForm().Refresh();
            }
            parent = mainVars.binaryTree.findChilds_parent(parent, mainVars.binaryTree.getRoot());
            if (parent == mainVars.binaryTree.getRoot()) return;
            centerButtonBetweenChldren(parent);
        }


        /*
         * 
         * 
         * souse https://www.geeksforgeeks.org/find-two-rectangles-overlap/ 
         */

        static bool doOverlap(BinaryTreeeNode n1, BinaryTreeeNode n2)

        {
            System.Drawing.Point lt1 = new System.Drawing.Point(
                                           n1.getButton().Location.X,
                                           n1.getButton().Location.Y),
                                 rb1 = new System.Drawing.Point(
                                           n1.getButton().Location.X +
                                           n1.getButton().Width,
                                           n1.getButton().Location.Y -
                                           n1.getButton().Height),
                                 lt2 = new System.Drawing.Point(
                                           n2.getButton().Location.X,
                                           n2.getButton().Location.Y),
                                 rb2 = new System.Drawing.Point(
                                           n2.getButton().Location.X +
                                           n2.getButton().Width,
                                           n2.getButton().Location.Y -
                                           n2.getButton().Height);
            // If one rectangle is on left side of other  
            if (lt1.X > rb2.X || lt2.X > rb1.X)
            {
                return false;
            }

            // If one rectangle is above other  
            if (lt1.Y < rb2.Y || lt2.Y < rb1.Y)
            {
                return false;
            }
            return true;
        }

        public void shiftButtonsL(LinkedList[] l)
        {
            System.Drawing.Point last_XY = new System.Drawing.Point();
            LinkedListNode parents = l[l.Length - 2].getHead().getNext(),
                children =  null;
            if (l[l.Length - 1].getHead() != null)
                 children = l[l.Length - 1].getHead().getNext();
            while (parents != null)
            {
                if (last_XY.IsEmpty)
                {
                    //points to the secound most right child
                    BinaryTreeeNode ch = children.getNext().getData();
                    last_XY = ch.getButton().Location;
                }
                else if (parents.getData().right != null)
                {
                    if (last_XY != parents.getData().left.getButton().Location)
                    {

                        parents.getData().right.getButton().Location = new System.Drawing.Point(
                                last_XY.X - parents.getData().right.getButton().Width - parents.getData().right.getButton().Width, last_XY.Y
                            );
                        parents.getData().left.getButton().Location = new System.Drawing.Point(
                               parents.getData().right.getButton().Location.X - parents.getData().getButton().Width - parents.getData().left.getButton().Width - 10, last_XY.Y
                            );
                        last_XY = parents.getData().left.getButton().Location;
                    }
                }
                parents = parents.getNext();
            }
        }
        public void shiftButtonsR(LinkedList[] l)
        {
            System.Drawing.Point last_XY = new System.Drawing.Point();
            LinkedListNode parents = l[l.Length - 2].getHead().getNext();
            LinkedListNode children = l[l.Length - 1].getHead().getNext();
            while (parents != null)
            {
                if (last_XY.IsEmpty)
                {
                    //points to the secound most right child
                    BinaryTreeeNode ch = children.getNext().getData();
                    last_XY = ch.getButton().Location;
                }
                else if (parents.getData().left != null)
                {
                    if (last_XY != parents.getData().right.getButton().Location)
                    {

                        parents.getData().left.getButton().Location = new System.Drawing.Point(
                                last_XY.X + parents.getData().left.getButton().Width +
                                parents.getData().left.getButton().Width, last_XY.Y
                            );
                        parents.getData().right.getButton().Location = new System.Drawing.Point(
                               parents.getData().left.getButton().Location.X +
                               parents.getData().getButton().Width + 
                               parents.getData().right.getButton().Width + 10, last_XY.Y
                            );
                        last_XY = parents.getData().right.getButton().Location;
                    }
                }
                parents = parents.getNext();
            }
        }
        public void fixProblemsLeft(LinkedList[] l)
        {
            for (int i = l.Length - 1; i >= 0; i--)
            {
                LinkedListNode node1 = l[i].getHead().getNext(),
                               node2 = l[i].getHead().getNext();
                int node1_index = 0,
                node2_index = 0;
                while (node1 != null)
                {

                    node2_index = 0;
                    node2 = l[i].getHead().getNext();
                    while (node2 != null)
                    {
                        BinaryTreeeNode binaryNode1 = node1.getData(),
                                        binaryNode2 = node2.getData();

                        //the smaller the number the larger the x corrodent should be

                        //node 1 nneds to be shifted left or node 2 needs to shift right
                        //if index 2.x is less than index 3.x shift 3 left
                        if (node1 != node2)
                        {
                            if (node2_index < node1_index &&
                                binaryNode1.getButton().Location.X >
                                binaryNode2.getButton().Location.X)
                            {
                                binaryNode1.getButton().Location = new System.Drawing.Point(
                                   binaryNode2.getButton().Location.X -
                                   binaryNode1.getButton().Width -
                                   (binaryNode1.getButton().Width) - 10,
                                   binaryNode1.getButton().Location.Y);
                            }
                            if (doOverlap(binaryNode1, binaryNode2))
                            {
                                binaryNode1.getButton().Location = new System.Drawing.Point(
                                   binaryNode2.getButton().Location.X -
                                   binaryNode1.getButton().Width -
                                   (binaryNode1.getButton().Width) - 10,
                                   binaryNode1.getButton().Location.Y);
                            }
                        }

                        node2_index++;
                        node2 = node2.getNext();
                    }
                    node1_index++;
                    node1 = node1.getNext();
                }

            }
        }

        public void fixProblemsRight(LinkedList[] l)
        {
            for (int i = l.Length - 1; i >= 0; i--)
            {
                LinkedListNode node1 = l[i].getHead().getNext(),
                               node2 = l[i].getHead().getNext();
                int node1_index = 0,
                node2_index = 0;
                while (node1 != null)
                {

                    node2_index = 0;
                    node2 = l[i].getHead().getNext();
                    while (node2 != null)
                    {
                        BinaryTreeeNode binaryNode1 = node1.getData(),
                                        binaryNode2 = node2.getData();

                        //the smaller the number the larger the x corrodent should be

                        //node 1 nneds to be shifted left or node 2 needs to shift right
                        //if index 2.x is less than index 3.x shift 3 left
                        if (node1 != node2)
                        {
                            if (node2_index < node1_index &&
                                binaryNode1.getButton().Location.X <
                                binaryNode2.getButton().Location.X)
                            {
                                binaryNode1.getButton().Location = new System.Drawing.Point(
                                   binaryNode2.getButton().Location.X +
                                   binaryNode1.getButton().Width +
                                   (binaryNode1.getButton().Width) + 10,
                                   binaryNode1.getButton().Location.Y);
                            }
                            if (doOverlap(binaryNode1, binaryNode2))
                            {
                                binaryNode1.getButton().Location = new System.Drawing.Point(
                                   binaryNode2.getButton().Location.X +
                                   binaryNode1.getButton().Width +
                                   (binaryNode1.getButton().Width) + 10,
                                   binaryNode1.getButton().Location.Y);
                            }
                        }

                        node2_index++;
                        node2 = node2.getNext();
                    }
                    node1_index++;
                    node1 = node1.getNext();
                }

            }
        }


        public void centerAllButtonS(LinkedList[] l)
        {
            for (int i = l.Length - 2; i >= 0; i--)
            {
                LinkedListNode node = l[i].getHead().getNext();
                while (node != null)
                {
                    if (node.getData().right != null)
                    {
                        BinaryTreeeNode parent = mainVars.binaryTree.findChilds_parent(node.getData(), mainVars.binaryTree.getRoot());
                        centerButtonBetweenChldren(node.getData());
                        while (parent != null && parent != mainVars.binaryTree.getRoot())
                        {
                            centerButtonBetweenChldren(parent);
                            parent = mainVars.binaryTree.findChilds_parent(parent, mainVars.binaryTree.getRoot());
                        }

                    }
                    node = node.getNext();
                }


            }
        }
        public void drawALine(LinkedList [] l, System.Drawing.Color c)
        { 
            System.Drawing.Graphics g = mainVars.getForm().CreateGraphics();
            System.Drawing.Pen pen = new System.Drawing.Pen(c, 3);
            for (int i = l.Length - 1; i >= 0; i--)
            {
                LinkedListNode node = l[i].getHead().getNext();
                while (node != null)
                {
                    if (node.getData().right != null)
                    {
                        BinaryTreeeNode parent = node.getData();
                        int parent_midleX = parent.getButton().Location.X + (parent.getButton().Width / 2),
                            parent_midleY = parent.getButton().Location.Y + parent.right.getButton().Height;
                        int child_midlleX = parent.right.getButton().Location.X + (parent.right.getButton().Width / 2),
                            child_midlleY = parent.right.getButton().Location.Y ;
                        g.DrawLine(pen, parent_midleX, parent_midleY,
                            child_midlleX, child_midlleY);
                        child_midlleX = parent.left.getButton().Location.X + (parent.left.getButton().Width / 2);
                        child_midlleY = parent.left.getButton().Location.Y;
                        g.DrawLine(pen, parent_midleX, parent_midleY,
                            child_midlleX, child_midlleY);
                        
                    }
                    node = node.getNext();
                }
            }
               
            pen.Dispose(); 
        }
        public void ButtonControls(object sender)
        {
            
            BinaryTreeeNode lc = mainVars.lastClicked;

            lc.getButton().BackColor = System.Drawing.Color.LightBlue;
            lc.getButton().Enabled = false;
            BinaryTreeeNode bt = mainVars.binaryTree.getRoot();
            lc.setSubnet(lc.getSubNet().GetIP(), lc.getSubNet().GetSubnetMask() + "");
            Button left = lc.left.getButton(),
                   right = lc.right.getButton(),
                   root1 = lc.getButton();

            left.Location = new System.Drawing.Point(root1.Location.X - root1.Width - 5, root1.Height + root1.Height / 2 + root1.Location.Y);
            right.Location = new System.Drawing.Point(root1.Location.X + root1.Width + 5, left.Location.Y);
            mainVars.addButton(left);
            mainVars.addButton(right);
            int arrLength = mainVars.binaryTree.maxDepth(mainVars.binaryTree.getRoot());
            LinkedList[] l = new LinkedList[mainVars.binaryTree.maxDepth(mainVars.binaryTree.getRoot())];
            // inistalize hole array of link lists
            for (int i = l.Length - 1; i >= 0; i--)
            {
                l[i] = new LinkedList();

            }
            if (bt.getButton().Location.X > lc.getButton().Location.X)
            { 
                //store each layer of nodes in the linked list
                l = mainVars.binaryTree.printTreeLeft(bt.left, l);
               // drawALine(l, mainVars.getForm().BackColor);
                shiftButtonsL(l);//move bottom row
                centerAllButtonS(l);//shift the parents and there parents parents and so on
                //needs to be in a method
                fixProblemsLeft(l);
                centerAllButtonS(l);//shift the parents and there parents parents and so on
               
                drawALine(l, System.Drawing.Color.Black);



            }
            else
            { 
                l = mainVars.binaryTree.printTreeRight(bt.right, l);
                drawALine(l, mainVars.getForm().BackColor);
                shiftButtonsR(l);
                centerAllButtonS(l);
                fixProblemsRight(l);
                centerAllButtonS(l);
                drawALine(l, System.Drawing.Color.Black);
            }
            
        }
 
       
        
    }
}

