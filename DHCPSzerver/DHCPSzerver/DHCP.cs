using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCPSzerver
{
    class DHCP
    {
        string mAC;
        string ip;

        public string MAC { get => mAC; set => mAC = value; }
        public string Ip { get => ip; set => ip = value; }

        public DHCP(string[] sor)
        {
            MAC = sor[0];
            Ip = sor[1];
        }
        public DHCP()
        {

        }
    }
}
