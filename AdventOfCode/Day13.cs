using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day13
    {
        public static int Part1()
        {
            Dictionary<int, int> firewall = new Dictionary<int, int>();
            using (StreamReader sr = new StreamReader(Properties.Resources.input_D13))
            {
                while (!sr.EndOfStream)
                {
                    var s = sr.ReadLine().Split(':');
                    firewall.Add(int.Parse(s[0]), int.Parse(s[1]));
                }                
            }               

            int severity = 0;
            foreach (KeyValuePair<int,int> i in firewall)
            {
                if ( GetScannerPos(i) == 1)
                {
                    severity += i.Key * i.Value;
                }
            }
            return severity;
        }

        public static int Part2()
        {


            return 1;
        }

        private static int GetScannerPos(KeyValuePair<int, int> f)
        {
            var i = Math.Round(Math.Cos(f.Key * (180/ (f.Value+1))));
            return (int)i;
        }      

    }
}
