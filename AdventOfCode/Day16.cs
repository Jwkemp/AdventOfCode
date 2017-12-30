using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day16
    {
        public static string Part1()
        {
            var cmds = Properties.Resources.input_D16.Split(',');
            StringBuilder line = new StringBuilder("abcdefghijklmnop");

            Dance(ref line, cmds);   

            return line.ToString();
        }

        public static string Part2()
        {
            var cmds = Properties.Resources.input_D16.Split(',');

            StringBuilder line = new StringBuilder("abcdefghijklmnop");
            StringBuilder original = new StringBuilder("abcdefghijklmnop");

            // Find number of repititions before returning to initial state
            int loopCount = 0;
            while (true)
            {
                loopCount++;
                Dance(ref line, cmds);
                if (line.ToString() == original.ToString()) break;
            }
            
            // Perform remainder 
            for (long i = 0; i < 1000000000 % loopCount; ++i)
            {                
                Dance(ref line, cmds);
            }

            return line.ToString();
        }
        
        private static void Dance(ref StringBuilder line, string[] commands)
        {
            foreach (string cmd in commands)
            {
                switch (cmd[0])
                {
                    case 's':
                        Spin(ref line, int.Parse(cmd.Substring(1)));
                        break;

                    case 'x':
                        var a = cmd.Substring(1, cmd.IndexOf('/') - 1);
                        var b = cmd.Substring(cmd.IndexOf('/') + 1);

                        Exchange(ref line, int.Parse(a), int.Parse(b));
                        break;

                    case 'p':
                        char x = Convert.ToChar(cmd.Substring(1, cmd.Length - cmd.IndexOf('/') - 1));
                        char y = Convert.ToChar(cmd.Substring(cmd.Length - cmd.IndexOf('/') + 1));

                        Partner(ref line, x, y);
                        break;

                    default:
                        throw new Exception("Misunderstood Command: " + cmd);
                }
            }
        }

        private static void Spin (ref StringBuilder line, int x )
        {
            string end = line.ToString().Substring(line.Length - x);
            line.Remove(line.Length - x, x);
            line.Insert(0, end);  
        }

        private static void Exchange(ref StringBuilder line, int a, int b)
        {
            char t = line[a];
            line[a] = line[b];
            line[b] = t;             
        }

        private static void Partner(ref StringBuilder line, char a, char b)
        {
            var indexA = line.ToString().IndexOf(a);
            var indexB = line.ToString().IndexOf(b);

            line[indexA] = b;
            line[indexB] = a;
        }
    }
}
