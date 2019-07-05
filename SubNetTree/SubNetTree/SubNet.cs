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
        private string brodBand;
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
    }
}
