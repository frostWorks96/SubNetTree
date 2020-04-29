using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnet
{
   public class SubNet
    {
        private string ip;
        private int subnetMask;
        private double numOfHosts; 
        public SubNet(string ipAddress, int sbnm)
        {
            this.ip = ipAddress;
            this.subnetMask = sbnm;
        }
        public SubNet(string ipAddress, string sbnm)
        {
            this.ip = ipAddress;
            int.TryParse(sbnm, out this.subnetMask);
        }
        public string GetIP()
        {
            return ip;
        }
        public int GetSubnetMask()
        {
            return subnetMask;
        }
        public string toString()
        {
            return "IP address: " + ip + "/" + subnetMask + "\n" +
                   "Subnet Mask: " + convertSubNetMask() +"\n" +
                   "Number Of hosts: "  + (Math.Pow(2, (32 - subnetMask)) -2);
        }
        private string convertSubNetMask()
        {
            string result = "";
            int counter = 0;
            int subnetMask = this.subnetMask;
            while (subnetMask >= 8)
            {
                result += 8 * 32 + ".";
                subnetMask -= 8;
                counter++;
            }
            for(int i = counter; i < 4; i++)
            {
                if(subnetMask == 0 && i != 3)
                {
                    result += 0+".";
                }else if(subnetMask == 0)
                {
                    result += 0;
                }
                else if(i != 3)
                {
                    result += subnetMask * 32 +".";
                }
                else
                {
                    result += subnetMask * 32;
                }
            }
            return result;
        }
    }
}
