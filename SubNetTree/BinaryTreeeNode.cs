using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubNetTree
{
    public class BinaryTreeeNode
    {
        IP_SubNetButton ip_SubNetButton;
        public BinaryTreeeNode left, right;
        char LeftOrRight;
        private MainWindow mainVars;
         
 
        public BinaryTreeeNode(IP_SubNetButton ip, char c, MainWindow mw)
        {
            mainVars = mw;
            ip_SubNetButton = ip;
            LeftOrRight = c;

        }
        public char getLetter()
        {
            return LeftOrRight;
        }
        public string getIp()
        {
            return ip_SubNetButton.GetSubNet().GetIP();
        }
        public int getSubNetMask()
        {
            return ip_SubNetButton.GetSubNet().GetSubnetMask();
        }
        public BinaryTreeeNode(IP_SubNetButton ip)
        {
            ip_SubNetButton = ip; 
        }
        public IP_SubNetButton get_ip_subnetbutton()
        {
            return ip_SubNetButton;
        }
        public Subnet.SubNet getSubNet()
        {
            return ip_SubNetButton.GetSubNet();
        }
        public System.Windows.Forms.Button getButton()
        {
            return ip_SubNetButton.GetButton();
        }
        public void setLeftOrRight(char c)
        {
            LeftOrRight = c;
        }
        public void setIP(IP_SubNetButton ip)
        {
            ip_SubNetButton = ip;
        } 
        /*  public void setSubnet(string ip, string sub)
         *  desc add the buttons and set there numbers
         *  
         *  param: the ip button and subnnet mask   
         */
        public void setSubnet(string ip, string sub)
        {

            System.Windows.Forms.ToolTip MyToolTip = new System.Windows.Forms.ToolTip();
            string new_ip = IP_SubNetButton.splitSubNetLeft(ip, sub);
            int new_subnetMask = int.Parse(sub) + 1;
            Subnet.SubNet subnet = new Subnet.SubNet(new_ip, new_subnetMask);
            left = new BinaryTreeeNode(new IP_SubNetButton(subnet, mainVars), 'L', mainVars);
            left.getButton().Text = new_ip + "/" + new_subnetMask;
            left.getButton().AutoSize = true;

            MyToolTip.SetToolTip(left.getButton(), subnet.toString());
            left.getButton().Click += (sender, args) =>
            {
                 
                mainVars.lastClicked = left;
                ip_SubNetButton.ButtonControls(sender);
            };

            
          
            new_ip = IP_SubNetButton.splitSubNetRight(ip, sub);
            subnet = new Subnet.SubNet(new_ip, new_subnetMask);
            right = new BinaryTreeeNode(new IP_SubNetButton(subnet, mainVars), 'R', mainVars); 
            right.getButton().Text = new_ip + "/" + new_subnetMask;
            right.getButton().AutoSize = true;
            MyToolTip.SetToolTip(right.getButton(), subnet.toString());
            right.getButton().Click += (sender, args) =>
            {  
                mainVars.lastClicked = right;
                
                ip_SubNetButton.ButtonControls(sender);
                
            }; 
        }

    }
}
