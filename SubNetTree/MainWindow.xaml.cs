using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Forms = System.Windows.Forms;
using System.Windows.Controls;
using WMessageBox = System.Windows.MessageBox;
using Panel = System.Windows.Forms.Panel;
using Label = System.Windows.Forms.Label;
namespace SubNetTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public BinTree binaryTree;
        public BinaryTreeeNode lastClicked;
        System.Windows.Forms.Form f;
        public MainWindow()
        {
            InitializeComponent();
            string ip = "192.168.2.1";
            int subnetmask = 7;

            snMask.IsEnabled = false;
            nHosts.IsEnabled = false;
            Submit2.IsEnabled = false;
        }
        public void write(string str)
        {
            
            WMessageBox.Show(str);
        }
        public Forms.Form getForm()
        {
            return f;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ipTextBox.IsEnabled)
            {
                string error = "";
                string ip = ipTextBox.Text;
                List<string> bytess = ip.Split('.').ToList<string>();
                if (bytess.Count != 4)
                {
                    WMessageBox.Show("less than 4 bytes");
                }
                else
                {

                    if (!IsAnInt(bytess[0]))
                    {
                        error += bytess[0];

                    }
                    if (!IsAnInt(bytess[1]))
                    {
                        if (error.Length != 0)
                        {
                            error += "." + bytess[1];
                        }
                        else
                        {
                            error += bytess[1];
                        }

                    }
                    if (!IsAnInt(bytess[2]))
                    {
                        if (error.Length != 0)
                        {
                            error += "." + bytess[2];
                        }
                        else
                        {
                            error += bytess[2];
                        }
                    }
                    if (!IsAnInt(bytess[3]))
                    {

                        if (error.Length != 0)
                        {
                            error += "." + bytess[3];
                        }
                        else
                        {
                            error += bytess[3];
                        }

                    }
                    if (error.Length != 0)
                    {
                        Forms.Form f = new Forms.Form();
                        Forms.Label messge1 = new Forms.Label(),
                            messge2 = new Forms.Label(),
                            l = new Forms.Label();
                        messge1.AutoSize = true;
                        l.AutoSize = true;
                        messge2.AutoSize = true;
                        messge1.Text = "the IP address";
                        messge1.Location = new System.Drawing.Point(0, 0);



                        l.Text = error;

                        l.ForeColor = System.Drawing.Color.Red;

                        l.Location = new System.Drawing.Point(messge1.Width + 1, 0);
                        messge2.Text = "has an error please fix the error";

                        messge2.Location = new System.Drawing.Point(l.Width + messge1.Width, 0);

                        f.SetBounds(1500, 800, l.Width + messge1.Width + messge2.Width + 100, 70);
                        f.Controls.Add(messge1);

                        f.Controls.Add(l);

                        f.Controls.Add(messge2);

                        f.Show();
                    }
                    else
                    {
                        ipTextBox.IsEnabled = false;
                        snMask.IsEnabled = true;
                        nHosts.IsEnabled = true;
                        Submit2.IsEnabled = true;
                        Submit1.Content = "Change IP";
                    }
                }
            }
            else
            {
                ipTextBox.IsEnabled = true;
                snMask.IsEnabled = false;
                nHosts.IsEnabled = false;
                Submit2.IsEnabled = false;
                Submit1.Content = "Submit";
            }

        }
       
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (snMask.SelectedIndex > 0)
            {
                ComboBoxItem typeItem = (ComboBoxItem)snMask.SelectedItem;
                binaryTree = new BinTree(ipTextBox.Text, snMask.SelectedIndex - 1 + 8, 'C', this);
  
                MakeSubNetingForm();
            }
            else if (nHosts != null || !nHosts.Text.Equals(""))
            {
                int result = 0;
                if (int.TryParse(nHosts.Text.Trim(), out result))
                {
                    if (result > 1)
                    {
                        int sum = 2;
                        for (int i = 31; i >= 0; i--)//decrements while sum doubles until the sum is more than the result
                        {

                            if (sum >= result)
                            {
                                
                                ComboBoxItem typeItem = (ComboBoxItem)snMask.SelectedItem;
                                binaryTree = new BinTree(ipTextBox.Text, i, 'C', this);  

                                binaryTree.getRoot().getButton().Text = binaryTree.getRoot().getSubNet().GetIP() + "/" + binaryTree.getRoot().getSubNet().GetSubnetMask();
                                binaryTree.getRoot().getButton().AutoSize = true;
                                break;//end the loop becouse there is no point to stay  
                            }
                            sum += sum;
                        }
                        MakeSubNetingForm();
                    }
                    else
                    {
                        WMessageBox.Show("error");
                    }
                }
                else
                {
                    WMessageBox.Show("error");

                }
            }
        }
        public static bool IsAnInt(string s)
        {
            try
            {
                int t = Int32.Parse(s);
                if (t < 0 || t > 255) return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void MakeSubNetingForm()
        {
            f = new FrostForm();
            f.AutoScroll = true;

            f.Size = new System.Drawing.Size(10000, 5000);
            binaryTree.getRoot().getButton().Location = new System.Drawing.Point(5000, 0);
            f.WindowState = Forms.FormWindowState.Maximized;
            f.SetAutoScrollMargin(10000, 5000);



            binaryTree.getRoot().getButton().Click += (sender, args) =>
            {

                IPButton_Click();
            };
            f.Controls.Add(binaryTree.getRoot().getButton());


            f.Show();
            int x = binaryTree.getRoot().getButton().Location.X - 600;
            f.AutoScrollPosition = new System.Drawing.Point(x, 0);

        }
        private void IPButton_Click()
        {
            f.AutoScroll = true;
            binaryTree.getRoot().getButton().BackColor = System.Drawing.Color.LightBlue;
            binaryTree.getRoot().getButton().Enabled = false;
           
            binaryTree.addNode(binaryTree.getRoot().getIp(), binaryTree.getRoot().getSubNetMask());
            Forms.Button left = binaryTree.getRoot().left.getButton();
            Forms.Button right = binaryTree.getRoot().right.getButton();
            Forms.Button root1 = binaryTree.getRoot().getButton();
            
            left.Location = new System.Drawing.Point(root1.Location.X - root1.Width, root1.Location.Y + root1.Height + root1.Height / 2);




            f.Controls.Add(left);
            right.Location = new System.Drawing.Point(root1.Location.X + root1.Width, left.Location.Y);
            f.Controls.Add(right);
                /* 
                root.GetLeft().GetButton().Click += (sender, args) =>
                { 
                    lastClicked = root.GetLeft();
                    IPClick(sender);
                };

                root.GetRight().GetButton().Click += (sender, args) =>
                {
                    lastClicked = root.GetRight();
                    IPClick(sender);
                };*/
        }
        public void shiftButtons(IP_SubNetButton ip)
        {

            /*if (ip.GetButton().Location.X > root.GetButton().Location.X)
            {
                shiftRight(root.GetRight(), root.GetRight().GetButton().Location.X);
            }
            else if (ip.GetButton().Location.X < root.GetButton().Location.X)
            {
                shiftLeft(root.GetLeft(), root.GetLeft().GetButton().Location.X);
            }*/
        }
        public void shiftLeft(IP_SubNetButton ip, int numShift)
        {
            if (ip == null) return;
            int x = ip.GetButton().Location.X, y = ip.GetButton().Location.Y;
            ip.GetButton().Location = new System.Drawing.Point(x - numShift, y);
            numShift /= 2;
           /* shiftLeft(ip.GetLeft(), numShift);
            shiftLeft(ip.GetRight(), numShift);*/
        }
        public void shiftRight(IP_SubNetButton ip, int numShift)
        {
            if (ip == null) return;
            int x = ip.GetButton().Location.X, y = ip.GetButton().Location.Y;
            ip.GetButton().Location = new System.Drawing.Point(x + numShift, y);
            numShift /= 2;
           // shiftLeft(ip.GetLeft(), numShift);
           // shiftLeft(ip.GetRight(), numShift);
        }

        public void IPClick(object sender)
        {

            /*
              
            lastClicked.Add(left, right);
            SetButtonLocation(lastClicked);

            lastClicked.GetButton().BackColor = System.Drawing.Color.LightBlue;
            lastClicked.GetButton().Enabled = false;
            
            left.GetButton().Click += (objectSender, args) =>
            {
                lastClicked = left;
                IPClick(objectSender);
            };
            right.GetButton().Click += (sender1, args) =>
            {
                lastClicked = right;
                IPClick(sender1);
            };
            //f.Controls.Add(left.GetButton());
            // f.Controls.Add(right.GetButton());
            */
        }
        public void addButton(Forms.Button b)
        {
            f.Controls.Add(b);
        }
        public void shiftRight(IP_SubNetButton root)
        {
            /*root.BottomRowLeftMostAtHead(root);
            LinkedList temp = root.GetList();
            IP_SubNetButton mostLeft = root.GetList().GetHead().get_ip_subnetbutton();
            int shiftNum = root.GetButton().Location.X + root.GetButton().Width + 5 - mostLeft.GetButton().Location.X;
            Node tempNode = root.GetList().GetHead();
            LinkedList parents = null;
            while (tempNode != null)
            {
                tempNode.getButton().Location = new System.Drawing.Point(
                    tempNode.getButton().Location.X + shiftNum, tempNode.getButton().Location.Y
                    );
                if (parents == null) parents = new LinkedList(new Node(tempNode.getParent()));
                else parents.Add(new Node(tempNode.getParent()));
                tempNode = tempNode.next;
            }
        }
        private void SetButtonLocation(IP_SubNetButton originalButton)
        {
            
            Forms.Button root1 = originalButton.GetButton(),
                left = originalButton.GetLeft().GetButton(),
                right = originalButton.GetRight().GetButton();

            left.Location = new System.Drawing.Point(root1.Location.X - (root1.Width), root1.Height + root1.Height / 2 + root1.Location.Y);
            right.Location = new System.Drawing.Point(root1.Location.X + (root1.Width), left.Location.Y);
            f.Controls.Add(left);
            f.Controls.Add(right);
            if (left.Location.X <= root.GetButton().Location.X +root.GetButton().Width )
            {
                f.Refresh();
                shiftRight(originalButton);
            }*/
        }


      
        private void SnMask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public static string splitSubNetLeft(string ip, string sub)
        {

            int index = FindIndexOfOctets(sub);
            int subN = FindSubN(sub);
            string[] octets = ip.Split('.');


            int minOf1 = FindMinDec(octets[index], subN);
            int minOF2 = FindMaxDec(octets[index], subN);
            octets[index] = minOf1 + "";

            int num = int.Parse(sub.Substring(1));
            num++;
            sub = "/" + num;
            string iptemp = octets[0] + "." + octets[1] + "." + octets[2] + "." + octets[3];

            return iptemp;

        }
        public static string splitSubNetRight(string ip, string sub)
        {

            int index = FindIndexOfOctets(sub);
            int subN = FindSubN(sub);
            string[] octets = ip.Split('.');


            int minOf1 = FindMinDec(octets[index], subN);
            int minOF2 = FindMaxDec(octets[index], subN);
            octets[index] = minOf1 + "";

            int num = int.Parse(sub.Substring(1));
            num++;
            sub = "/" + num;
            string iptemp = octets[0] + "." + octets[1] + "." + octets[2] + "." + octets[3];


            octets[index] = minOF2 + "";
            iptemp = octets[0] + "." + octets[1] + "." + octets[2] + "." + octets[3];
            return iptemp;

        }
        public static int FindIndexOfOctets(string sub)
        {
            string t = sub;
            int num = int.Parse(sub.Substring(1));
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
        public static int FindSubN(string sub)
        {
            string t = sub;
            int num = int.Parse(sub.Substring(1));
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

        public static int FindMinDec(string str, int index)
        {
            int octect = int.Parse(str);
            int sum = 0;
            int byte_ = 128;
            for (int i = 0; i < index; i++)
            {
                if (octect >= byte_)
                {
                    sum += byte_;
                    octect -= byte_;
                }
                else if (octect == 0) break;
                byte_ /= 2;
            }
            return sum;
        }

        public static int FindMaxDec(string str, int index)
        {

            int octect = int.Parse(str);
            int sum = 0;
            int byte_ = 128;
            for (int i = 0; i < 8; i++)
            {
                if (i >= index)
                {
                    sum += byte_;
                    break;
                }
                else if (octect >= byte_)
                {
                    sum += byte_;
                    octect -= byte_;
                }

                byte_ /= 2;
            }

            return sum;
        }
    }
}
