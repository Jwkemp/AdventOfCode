using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day18
    {
		public static long Part1()
		{
			Program p1 = new Program(0, Properties.Resources.input_D18);
			p1.Run();

			return p1.partOne;
		}


		public static long Part2()
		{
			Program a = new Program(0, Properties.Resources.input_D18 );
			Program b = new Program(1, Properties.Resources.input_D18);

			a.recievedMessages = b.sentMessages;
			b.recievedMessages = a.sentMessages;
			
			try
			{
				var t1 = Task.Run(() => a.Run());
				var t2 = Task.Run(() => b.Run());
				Task.WaitAll(t1, t2);
			}
			catch ( Exception e )
			{
				Console.WriteLine("un oh, " + a.sendCounter);
				Console.WriteLine(e.Message);
			}
			return b.sendCounter;
		}

		class Program
		{
			public long id;
			private string[] instructions;
			public ConcurrentQueue<long> recievedMessages;
			public ConcurrentQueue<long> sentMessages;
			private Dictionary<string, long> register;
			public long sendCounter, lastValueSent, partOne;


			public Program( long _id, string _input)
			{
				id = _id;
				instructions = Properties.Resources.input_D18.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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
							sendCounter++;
							long sent = 0;
							lastValueSent = Send(cmdSplit[1], ref register, out sent) ? sent : lastValueSent;
							sentMessages.Enqueue(sent);
							break;

						case "set": Set(cmdSplit[1], cmdSplit[2], ref register); break;
						case "add": Add(cmdSplit[1], cmdSplit[2], ref register); break;
						case "mul": Multiply(cmdSplit[1], cmdSplit[2], ref register); break;
						case "mod": Modulo(cmdSplit[1], cmdSplit[2], ref register); break;

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
							break;
					}
					if (cmdSplit[0] == "jgz")
						index += Jump(cmdSplit[1], cmdSplit[2], ref register);
					else
						index++;
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
}
