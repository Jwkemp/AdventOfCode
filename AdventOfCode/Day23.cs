using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day23
    {
        public static long Part1()
        {
            Program23 p1 = new Program23(0, Properties.Resources.input_D18);
            p1.Run();
            return p1.sendCounter;
        }


        public static int Part2()
        {
            int h = 0;           

            for (int b = 109900, c = b + 17000; b <= c; b+=17)
            {
                if (!IsPrime(b))
                {
                    h++;
                }            
            }
            return h;
        }

        public static bool IsPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
    
    class Program23
    {
        public long id;
        private string[] instructions;
        public ConcurrentQueue<long> recievedMessages;
        public ConcurrentQueue<long> sentMessages;
        private Dictionary<string, long> register;
        public long sendCounter, lastValueSent, partOne;


        public Program23(long _id, string _input)
        {
            id = _id;
            instructions = Properties.Resources.input_D23.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            sentMessages = new ConcurrentQueue<long>();
            register = new Dictionary<string, long>();
            register["p"] = id;
            sendCounter = 0;
            lastValueSent = 0;
            partOne = 0;
        }

        public void Run()
        {
            long index = 0;
            while (index >= 0 && index < instructions.Length)
            {
                var cmdSplit = instructions[index].Split(' ');
                switch (cmdSplit[0])
                {
                    case "snd":

                        long sent = 0;
                        lastValueSent = Send(cmdSplit[1], ref register, out sent) ? sent : lastValueSent;
                        sentMessages.Enqueue(sent);
                        break;

                    case "set": Set(cmdSplit[1], cmdSplit[2], ref register); index++; break;
                    case "add": Add(cmdSplit[1], cmdSplit[2], ref register); index++; break;
                    case "sub": Sub(cmdSplit[1], cmdSplit[2], ref register); index++; break;

                    case "mul":
                        sendCounter++;
                        Multiply(cmdSplit[1], cmdSplit[2], ref register);
                        index++;
                        break;
                    case "mod": Modulo(cmdSplit[1], cmdSplit[2], ref register); index++; break;

                    case "rcv":

                        if (recievedMessages == null)
                        {
                            if (GetValue(cmdSplit[1], ref register) != 0)
                            {
                                if (partOne == 0)
                                {
                                    partOne = lastValueSent;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            while (true)
                            {
                                long result;
                                if (recievedMessages.TryDequeue(out result))
                                {
                                    register[cmdSplit[1]] = result;
                                    if (recievedMessages == null)
                                    {
                                        return;
                                    }
                                    break;
                                }

                                // Race condition. Bad jack! >_<
                                if (sentMessages.Count == 0 && recievedMessages.Count == 0)
                                {
                                    Thread.Sleep(50);
                                    if (sentMessages.Count == 0 && recievedMessages.Count == 0)
                                    {
                                        return;
                                    }
                                }
                                Thread.Sleep(50);
                            }
                        }
                        index++;
                        break;

                    case "jnz":
                        index += Jump(cmdSplit[1], cmdSplit[2], ref register);
                        break;

                    default:
                        throw new Exception("A what command?");
                }
            }
        }

        private static bool Send(string regID, ref Dictionary<string, long> reg, out long sent)
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

        private static void Set(string regID, string val, ref Dictionary<string, long> reg)
        {
            if (reg.ContainsKey(regID))
                reg[regID] = GetValue(val, ref reg);
            else
                reg.Add(regID, GetValue(val, ref reg));
        }

        private static void Sub(string regID, string val, ref Dictionary<string, long> reg)
        {
            if (reg.ContainsKey(regID))
                reg[regID] -= GetValue(val, ref reg);
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
            if (GetValue(regID, ref reg) != 0)
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
                if (registers.TryGetValue(s, out temp))
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
