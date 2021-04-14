using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCPSzerver
{
    class Excluded
    {
        string ip;

        public string Ip { get => ip; set => ip = value; }

        public Excluded(string ip)
        {
            Ip = ip;
        }
    }
}
