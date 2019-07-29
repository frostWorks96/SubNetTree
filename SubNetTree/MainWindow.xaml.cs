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
        IP_SubNetButton root;
        IP_SubNetButton lastClicked;
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
                root = new IP_SubNetButton(new Forms.Button(), new Subnet.SubNet(ipTextBox.Text, snMask.SelectedIndex - 1 + 8), 'C');
                root.GetButton().Text = root.GetSubNet().GetIP() + "/" + root.GetSubNet().GetSubnetMask();
                root.GetButton().AutoSize = true;
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
                                root = new IP_SubNetButton(new Forms.Button(), new Subnet.SubNet(ipTextBox.Text, i), 'C');
                                root.GetButton().Text = root.GetSubNet().GetIP() + "/" + root.GetSubNet().GetSubnetMask();
                                root.GetButton().AutoSize = true;
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
            root.GetButton().Location = new System.Drawing.Point(5000, 0);
            f.WindowState = Forms.FormWindowState.Maximized;
            f.SetAutoScrollMargin(10000, 5000);



            root.GetButton().Click += (sender, args) =>
            {

                IPButton_Click();
            };
            f.Controls.Add(root.GetButton());


            f.Show();
            int x = root.GetButton().Location.X - 600;
            f.AutoScrollPosition = new System.Drawing.Point(x, 0);

        }
        private void IPButton_Click()
        {
            f.AutoScroll = true;
            root.GetButton().BackColor = System.Drawing.Color.LightBlue;
            root.GetButton().Enabled = false;

            root.SetLeft(new IP_SubNetButton(new Forms.Button(),
                new Subnet.SubNet(splitSubNetLeft(root.GetSubNet().GetIP(),
                                                   "/" + root.GetSubNet().GetSubnetMask()
                                                 ),
                                   (root.GetSubNet().GetSubnetMask() + 1)
                                   )
                    , 'L')
                );

            root.SetRight(new IP_SubNetButton(new Forms.Button(),
                new Subnet.SubNet(splitSubNetRight(root.GetSubNet().GetIP(),
                                                   "/" + root.GetSubNet().GetSubnetMask()
                                                 ),
                                   (root.GetSubNet().GetSubnetMask() + 1)
                                   )
                     , 'R')
               );
            Forms.Button left = root.GetLeft().GetButton();
            Forms.Button right = root.GetRight().GetButton();
            Forms.Button root1 = root.GetButton();
            left.Text = root.GetLeft().GetSubNet().GetIP() + "/"
    + root.GetLeft().GetSubNet().GetSubnetMask();
            right.Text = root.GetRight().GetSubNet().GetIP() + "/"
                + root.GetRight().GetSubNet().GetSubnetMask();
            right.AutoSize = true;
            left.AutoSize = true;

            left.Location = new System.Drawing.Point(root1.Location.X - root1.Width, root1.Location.Y + root1.Height + root1.Height / 2);




            f.Controls.Add(left);
            right.Location = new System.Drawing.Point(root1.Location.X + root1.Width, left.Location.Y);
            f.Controls.Add(right);
            root.GetLeft().SetButton(left);
            root.GetRight().SetButton(right);
            root.GetLeft().GetButton().Click += (sender, args) =>
            {

                lastClicked = root.GetLeft();
                IPClick(sender);
            };
            root.GetRight().GetButton().Click += (sender, args) =>

            {
                lastClicked = root.GetRight();
                IPClick(sender);
            };
        }
        public void shiftButtons(IP_SubNetButton ip)
        {

            if (ip.GetButton().Location.X > root.GetButton().Location.X)
            {
                shiftRight(root.GetRight(), root.GetRight().GetButton().Location.X);
            }
            else if (ip.GetButton().Location.X < root.GetButton().Location.X)
            {
                shiftLeft(root.GetLeft(), root.GetLeft().GetButton().Location.X);
            }
        }
        public void shiftLeft(IP_SubNetButton ip, int numShift)
        {
            if (ip == null) return;
            int x = ip.GetButton().Location.X, y = ip.GetButton().Location.Y;
            ip.GetButton().Location = new System.Drawing.Point(x - numShift, y);
            numShift /= 2;
            shiftLeft(ip.GetLeft(), numShift);
            shiftLeft(ip.GetRight(), numShift);
        }
        public void shiftRight(IP_SubNetButton ip, int numShift)
        {
            if (ip == null) return;
            int x = ip.GetButton().Location.X, y = ip.GetButton().Location.Y;
            ip.GetButton().Location = new System.Drawing.Point(x + numShift, y);
            numShift /= 2;
            shiftLeft(ip.GetLeft(), numShift);
            shiftLeft(ip.GetRight(), numShift);
        }

        private void IPClick(object sender)
        {


            // shiftButtons(lastClicked);
            IP_SubNetButton left = new IP_SubNetButton(new Forms.Button(),
            new Subnet.SubNet(splitSubNetLeft(lastClicked.GetSubNet().GetIP(),
                                               "/" + lastClicked.GetSubNet().GetSubnetMask()
                                             ),
                               (lastClicked.GetSubNet().GetSubnetMask() + 1)
                               )
                , 'L');

            IP_SubNetButton right = new IP_SubNetButton(new Forms.Button(),
                new Subnet.SubNet(splitSubNetRight(lastClicked.GetSubNet().GetIP(),
                                                   "/" + lastClicked.GetSubNet().GetSubnetMask()
                                                 ),
                                   (lastClicked.GetSubNet().GetSubnetMask() + 1)
                                   )
                    , 'R');

            left.GetButton().AutoSize = true;
            right.GetButton().AutoSize = true;
            left.GetButton().Text = left.GetSubNet().GetIP() + "/"
                + left.GetSubNet().GetSubnetMask();
            right.GetButton().Text = right.GetSubNet().GetIP() + "/"
                + right.GetSubNet().GetSubnetMask();
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

        }
        public void shiftRight(IP_SubNetButton root)
        {
            root.BottomRowLeftMostAtHead(root);
            LinkedList temp = root.GetList();
            IP_SubNetButton mostLeft = root.GetList().GetHead().GetIP();
            int shiftNum = root.GetButton().Location.X + root.GetButton().Width + 5 - mostLeft.GetButton().Location.X;
            Node tempNode = root.GetList().GetHead();
            LinkedList parents = null;
            while (tempNode != null)
            {
                tempNode.GetIP().GetButton().Location = new System.Drawing.Point(
                    tempNode.GetIP().GetButton().Location.X + shiftNum, tempNode.GetIP().GetButton().Location.Y
                    );
                if (parents == null) parents = new LinkedList(new Node(tempNode.GetIP().GetParent()));
                else parents.Add(new Node(tempNode.GetIP().GetParent()));
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
            }
        }


        public void LeftButtonLocation(IP_SubNetButton originalButton, int randomX, int randomY)
        {
            bool tf = false;
            int i = 0;

            IP_SubNetButton left = originalButton.GetLeft();


            left.GetButton().Location = new System.Drawing.Point(randomX, randomY);


            foreach (Forms.Button btn in f.Controls.OfType<Forms.Button>())
            {
                do
                {
                    if (!btn.Equals(left.GetButton()))
                    {
                        if (left.GetButton().Bounds.IntersectsWith(btn.Bounds))
                        {

                            if (btn.Location.X > left.GetButton().Location.X &&
                                btn.Location.Y == left.GetButton().Location.Y)
                            {
                                randomX -= 1;
                            }
                            else if (btn.Location.X < left.GetButton().Location.X &&
                               btn.Location.Y == left.GetButton().Location.Y)
                            {
                                randomX += 1;
                            }
                            else if (btn.Location.X == left.GetButton().Location.X &&
                               btn.Location.Y == left.GetButton().Location.Y)
                            {
                                randomX -= 1;
                                randomY = originalButton.GetButton().Location.Y;
                            }
                            else if (btn.Location.X == left.GetButton().Location.X &&
                              btn.Location.Y < left.GetButton().Location.Y)
                            {

                                randomY += 1;
                            }
                            else if (btn.Location.X == left.GetButton().Location.X &&
                             btn.Location.Y > left.GetButton().Location.Y)
                            {

                                randomY -= 1;
                            }
                            tf = true;
                            randomY += 2;
                            left.GetButton().Location = new System.Drawing.Point(left.GetButton().Location.X, left.GetButton().Location.Y + randomY);
                            i++;
                        }
                        else
                        {
                            // MessageBox.Show("Left button collided " + i + " times");
                            tf = false;
                            // LeftButtonLocation(originalButton, randomX, randomY);
                        }

                    }

                } while (tf);
            }

        }
        public void RightButtonLocation(IP_SubNetButton originalButton, int randomX, int randomY)
        {
            int i = 0;
            bool tf = false;

            IP_SubNetButton right = originalButton.GetRight();
            int x = originalButton.GetButton().Location.X;
            int y = originalButton.GetButton().Location.Y;
            int w = originalButton.GetButton().Width;
            int h = originalButton.GetButton().Height;

            right.GetButton().Location = new System.Drawing.Point(randomX, randomY);


            foreach (Forms.Button btn in f.Controls.OfType<Forms.Button>())
            {
                do
                {
                    if (!btn.Equals(right.GetButton()))
                    {
                        if (right.GetButton().Bounds.IntersectsWith(btn.Bounds))
                        {

                            if (btn.Location.X > right.GetButton().Location.X &&
                                btn.Location.Y == right.GetButton().Location.Y)
                            {
                                randomX -= 1;
                            }
                            else if (btn.Location.X < right.GetButton().Location.X &&
                               btn.Location.Y == right.GetButton().Location.Y)
                            {
                                randomX += 1;
                            }
                            else if (btn.Location.X == right.GetButton().Location.X &&
                               btn.Location.Y == right.GetButton().Location.Y)
                            {
                                randomX += 1;
                                randomY = originalButton.GetButton().Location.Y;
                            }
                            else if (btn.Location.X == right.GetButton().Location.X &&
                              btn.Location.Y < right.GetButton().Location.Y)
                            {

                                randomY += 1;
                            }
                            else if (btn.Location.X == right.GetButton().Location.X &&
                             btn.Location.Y > right.GetButton().Location.Y)
                            {

                                randomY -= 1;
                            }
                            tf = true;

                            right.GetButton().Location = new System.Drawing.Point(right.GetButton().Location.X, right.GetButton().Location.Y + randomY);
                            i++;
                        }
                        else
                        {
                            // MessageBox.Show("right button collided " + i + " times");
                            tf = false;
                            //RightButtonLocation(originalButton, randomX, randomY);
                        }
                    }
                } while (tf);
            }

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
