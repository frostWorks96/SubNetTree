
using System.Windows.Forms;
using Subnet;

namespace SubNetTree
{
    public class IP_SubNetButton
    {
        private Button button;
        private IP_SubNetButton left;
        private IP_SubNetButton right;
        private Subnet.SubNet subNet;

        public IP_SubNetButton(Button button, SubNet subNet)
        {
            this.button = button;
            this.subNet = subNet;
        }
        public Button GetButton()
        {
            return button;
        }
        public IP_SubNetButton GetLeft()
        {
            return left;
        }
        public IP_SubNetButton GetRight()
        {
            return right;
        }
        public Subnet.SubNet GetSubNet()
        {
            return subNet;
        }
        public void SetButton(Button b)
        {
            button = b;
        }
        public void SetLeft(IP_SubNetButton l)
        {
            left = l;
        }
        public void SetRight(IP_SubNetButton r)
        {
            right = r;
        }
        public void SetSubNet(Subnet.SubNet s)
        {
            subNet = s;
        }
        public void printAll(IP_SubNetButton iP)
        {
            if (iP == null) return;
            printAll(iP.left);
            MessageBox.Show(iP.GetSubNet().GetIP() + "/" + iP.GetSubNet().GetSubnetMask());
            printAll(iP.right);
        }
        public void Add(IP_SubNetButton l, IP_SubNetButton r)
        {
            SetLeft(l);
            SetRight(r);
        }
        public bool Intersects(IP_SubNetButton i, Button b)
        {
            if (i == null) return false;
            if (!i.GetButton().Equals(b))
            {
                if (i.GetButton().Bounds.IntersectsWith(b.Bounds)) return true;
                else
                {
                    if(i.left!=null)
                    Intersects(i.left, b);
                    if(i.right!=null)
                    Intersects(i.right, b);
                }
            }
           
            
            return false;
        } 
        public void FillAllIPs(IP_SubNetButton ip)
        {
            if (!(ip.GetSubNet().GetSubnetMask() < 31)) return;
            

                IP_SubNetButton ip1 = new IP_SubNetButton(
                    null, new SubNet(
                        splitSubNetLeft(
                            ip.subNet.GetIP(), "/" + ip.subNet.GetSubnetMask()
                            ), (ip.subNet.GetSubnetMask() + 1)
                            )
                    ), ip2 = new IP_SubNetButton(
                    null, new SubNet(
                        splitSubNetRight(
                            ip.subNet.GetIP(), "/" + ip.subNet.GetSubnetMask()
                            ), (ip.subNet.GetSubnetMask() + 1)
                            )
                    );
                ip.Add(ip1, ip2);
            if (ip.subNet.GetSubnetMask() <10)
            {
                FillAllIPs(ip.left);
                FillAllIPs(ip.right);
                MessageBox.Show(ip.subNet.GetSubnetMask() + "");
            }
             
           

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
