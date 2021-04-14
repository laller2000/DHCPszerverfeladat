using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCPSzerver
{
    class Test
    {
        string muvelet;
        string azonosito;

        public string Muvelet { get => muvelet; set => muvelet = value; }
        public string Azonosito { get => azonosito; set => azonosito = value; }

        public Test(string[] sor)
        {
            Muvelet = sor[0];
            Azonosito = sor[1];
        }
    }
}
