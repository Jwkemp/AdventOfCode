using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day18
    {
        public static int Part1()
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            var cmds = Properties.Resources.input_D18.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int lastValueSent = 0;
            int temp = 0;

            for ( int i = 0; i < cmds.Length; ++i )
            {
                var cmdSplit = cmds[i].Split(' ');

                switch (cmdSplit[0] )
                {
                    case "snd":                        
                        if (registers.TryGetValue(cmdSplit[1], out temp))
                        {
                            lastValueSent = temp;
                        }
                        break;

                    case "set":
                        if (registers.ContainsKey(cmdSplit[1]))
                        {
                            registers[cmdSplit[1]] = GetValue(cmdSplit[2], registers);
                        }
                        else
                        {
                            registers.Add(cmdSplit[1], GetValue(cmdSplit[2], registers));
                        }

                        break;

                    case "add":
                        if (registers.ContainsKey(cmdSplit[1]))
                        {
                            registers[cmdSplit[1]] += GetValue(cmdSplit[2], registers);
        }
                        else
                        {
                            registers.Add(cmdSplit[1], GetValue(cmdSplit[2], registers));
                        }
                        break;

                    case "mul":
                        if (registers.ContainsKey(cmdSplit[1]))
                        {
                            registers[cmdSplit[1]] *= GetValue(cmdSplit[2], registers);
        }
                        else
                        {
                            registers.Add(cmdSplit[1], 0);
                        }
                        break;

                    case "mod":
                        if (registers.ContainsKey(cmdSplit[1]))
                        {
                            registers[cmdSplit[1]] = registers[cmdSplit[1]] % GetValue(cmdSplit[2], registers);
                        }
                        else
                        {
                            registers.Add(cmdSplit[1], 0);
                        }
                        break;

                    case "rcv":
                        if ( GetValue(cmdSplit[1],registers) != 0 )
                            return lastValueSent;
                        break;

                    case "jgz":
                        if (GetValue(cmdSplit[1], registers) != 0)
                        {
                            i += (GetValue(cmdSplit[2], registers) )- 1;
                        }
                        break;
                }
            }


            return 1;
        }

        public static int Part2()
        {
            
            return 1;
        }

        public static int GetValue(string s, Dictionary<string, int> registers)
        {
            int temp = 0;
            if (int.TryParse(s, out temp))
            {
                return temp;
            }
            else
            {
                if (registers.TryGetValue(s,out temp))
                {
                    return temp;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
