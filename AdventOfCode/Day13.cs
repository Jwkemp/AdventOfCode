using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day13
    {
        public static int Part1()
        {
            Dictionary<int, int> firewall = new Dictionary<int, int>();
			string input = Properties.Resources.input_D13;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach(string s in inputarray)
			{
				var  a = s.Split(':');
				firewall.Add(int.Parse(a[0]), int.Parse(a[1]));
			}
          

            int severity = 0;
            foreach (KeyValuePair<int,int> i in firewall)
            { 
                if ( GetScannerPos(i) == 0)
                {
                    severity += i.Key * i.Value;
                }
            }
            return severity;
        }

        public static int Part2()
        {
			Dictionary<int, int> firewall = new Dictionary<int, int>();
			string input = Properties.Resources.input_D13;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			int highestKey = 0;
			foreach (string s in inputarray)
			{
				var a = s.Split(':');
				firewall.Add(int.Parse(a[0]), int.Parse(a[1]));
				if (highestKey < firewall.Last().Key) highestKey = firewall.Last().Key;
			}

			int delay = -1;
			bool success = false;
			
			while (!success)
			{
				delay++;
				success = true;
				foreach(KeyValuePair<int,int> f in firewall)
				{
					if (!isPosClear(new KeyValuePair<int, int>(f.Key + delay, f.Value)))
					{
						success = false;
						break;
					}
				}
			}

			return delay;
		}

		private static bool isPosClear(KeyValuePair<int, int> f)
		{
			return GetScannerPos(f) != 0;
		}

		private static int GetScannerPos(KeyValuePair<int, int> f)
        {
			var n = f.Key;
			var r = f.Value-1;
			var r2 = r * 2;
			var o = (n % (r2)) + (r - (n % (r2))) - (Math.Abs(r - (n % (r2))));
			return o;
        }      

    }
}
