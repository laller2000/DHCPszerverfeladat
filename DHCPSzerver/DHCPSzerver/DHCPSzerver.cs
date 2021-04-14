using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DHCPSzerver
{
    class DHCPSzerver
    {
        static List<DHCP> dhcp = new List<DHCP>();
        static List<Excluded> excludeds = new List<Excluded>();
        static List<DHCP> reserveds = new List<DHCP>();
        static List<Test> tests = new List<Test>();

        static void Main(string[] args)
        {
            Beolvas_excluded("excluded.csv");
            Beolvas_reserved("reserved.csv");
            Beolvas_dchp("dhcp.csv");
            Beolvas_test("test.csv");
            foreach (Test item in tests)
            {
                if (item.Muvelet.Equals("request"))
                {
                    request(item.Azonosito);
                }
                else
                {
                    DHCP torolni = dhcp.Find(a => a.Ip.Equals(item.Azonosito));
                    dhcp.Remove(torolni);
                }
            }
            //--Állapot kiírása-------------------
            try
            {
                using (StreamWriter sw=new StreamWriter("dhcp_kesz.csv"))
                {
                    foreach (DHCP item in dhcp)
                    {
                        sw.WriteLine(string.Join(";",item.MAC,item.Ip));
                    }
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("\nProgram vége!");
            Console.ReadLine();
        }

        private static void request(string MAC)
        {
            string ipcim = "";
            if (dhcp.Find(a => a.MAC.Equals(MAC))==null)
            {
                if (reserveds.Find(a => a.MAC.Equals(MAC))==null)
                {
                    int i;
                    for ( i = 100; i <= 199; i++)
                    {

                        ipcim = "192.168.10." + i.ToString();
                        if (dhcp.Find(a => a.Ip.Equals(ipcim)) == null)
                        {
                            // ez az ip cím nincs kiosztva
                            if (excludeds.Find(a => a.Ip.Equals(ipcim)) == null)
                            {
                                //nincs a kizártak között
                                if (reserveds.Find(a => a.Ip.Equals(ipcim)) == null)
                                {
                                    //nem szerpel a fentartások között
                                    //Eltároljuk a bérelt címeknél
                                    DHCP uj = new DHCP();
                                    uj.Ip = ipcim;
                                    uj.MAC = MAC;
                                    dhcp.Add(uj);
                                    break;
                                }
                            }
                        }
                    }
                    if (i>199)
                    {
                        Console.WriteLine("Sikertelen IP cím kiosztás");
                    }
                    
                }
               
            }
        }

        private static string elso_kioszthato_cim()
        {
            //-- 192.168.10.100-192.168.10.199 közötti értéket ad vissza
            return "192.168.10.100";
        }

        private static void Beolvas_test(string filenev)
        {
            try
            {
                using (StreamReader sr=new StreamReader(filenev))
                {
                    while (!sr.EndOfStream)
                    {
                        tests.Add(new Test(sr.ReadLine().Split(';')));
                    }
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                Console.ReadKey();
                return;
            }
        }

        private static void Beolvas_dchp(string filenev)
        {
            try
            {
                using (StreamReader sr=new StreamReader(filenev))
                {
                    while (!sr.EndOfStream)
                    {
                        dhcp.Add(new DHCP(sr.ReadLine().Split(';')));
                    }
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                Console.ReadKey();
                return;
            }
        }

        private static void Beolvas_reserved(string filenev)
        {
            try
            {
                using (StreamReader sr=new StreamReader(filenev))
                {
                    while (!sr.EndOfStream)
                    {
                        reserveds.Add(new DHCP(sr.ReadLine().Split(';')));
                    }
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                Console.ReadKey();
                return;
            }
        }

        private static void Beolvas_excluded(string filenev)
        {
            try
            {
                using (StreamReader sr=new StreamReader(filenev))
                {
                    while (!sr.EndOfStream)
                    {
                        excludeds.Add(new Excluded(sr.ReadLine()));
                    }
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                Console.ReadKey();
                return;
            }
        }
    }
}
