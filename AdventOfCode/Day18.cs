using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode
{
    class Day18
    {
        public static long Part1()
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();
            var cmds = Properties.Resources.input_D18.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            long lastValueSent = 0;

            long index = 0;
            while ( index >= 0 && index < cmds.Length )
            {
                var cmdSplit = cmds[index].Split(' ');
                switch (cmdSplit[0])
                {
                    case "snd":
                        long temp = 0;
                        lastValueSent = Send(cmdSplit[1], ref registers, out temp) ? temp : lastValueSent;
                        break;

                    case "set": Set(cmdSplit[1], cmdSplit[2], ref registers); break;
                    case "add": Add(cmdSplit[1], cmdSplit[2], ref registers); break;
                    case "mul": Multiply(cmdSplit[1], cmdSplit[2], ref registers); break;
                    case "mod": Modulo(cmdSplit[1], cmdSplit[2], ref registers); break;

                    case "rcv":
                        if (GetValue(cmdSplit[1], ref registers) != 0)
                            return lastValueSent;
                        break;
                }
                if (cmdSplit[0] == "jgz")                
                    index += Jump(cmdSplit[1], cmdSplit[2], ref registers);                
                else
                    index++;
            }
            return 1;
        }

        private static bool Send(string regID, ref Dictionary<string,long> reg, out long sent)
        {
            long temp = 0;
            if (reg.TryGetValue(regID, out temp))
            {
                sent = temp;
                return true;
            }
            sent = 0;
            return false;
        }

        private static void Set(string regID, string val, ref Dictionary<string,long> reg)
        {
            if (reg.ContainsKey(regID))
                reg[regID] = GetValue(val, ref reg);            
            else            
                reg.Add(regID, GetValue(val, ref reg));            
        }

        private static void Add(string regID, string val, ref Dictionary<string, long> reg)
        {
            if (reg.ContainsKey(regID))            
                reg[regID] += GetValue(val, ref reg);            
            else            
                reg.Add(regID, GetValue(val, ref reg));            
        }

        private static void Modulo(string regID, string val, ref Dictionary<string, long> reg)
        {
            if (reg.ContainsKey(regID))            
                reg[regID] = reg[regID] % GetValue(val, ref reg);            
            else            
                reg.Add(regID, 0);            
        }
        
        private static void Multiply(string regID, string val, ref Dictionary<string, long> reg)
        {
            if (reg.ContainsKey(regID))            
                reg[regID] *= GetValue(val, ref reg);            
            else            
                reg.Add(regID, 0);            
        }

        private static long Jump(string regID, string val, ref Dictionary<string, long> reg)
        {
            if (GetValue(regID, ref reg) > 0)
                return (GetValue(val, ref reg));
            else
                return 1;
        }

        private static long GetValue(string s, ref Dictionary<string, long> registers)
        {
            long temp = 0;
            if (long.TryParse(s, out temp))
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
