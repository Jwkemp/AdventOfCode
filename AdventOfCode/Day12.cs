using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day12
    {
        public static int Part1()
        {
            Dictionary<int, int[]> pipes = new Dictionary<int, int[]>();
			string[] input = Properties.Resources.input_D13.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach ( string s in input )
			{
				int index = int.Parse(s.Substring(0, s.IndexOf(' ')));
				int[] accessible = Array.ConvertAll(s.Substring(s.IndexOf('>') + 1).Split(','), a => int.Parse(a));
				pipes.Add(index, accessible);
			}
			
            List<int> inGroup0 = new List<int>();
            inGroup0 = (GetMembers(pipes, inGroup0, 0));

            return inGroup0.Count;
        }

        public static int Part2()
        {
            Dictionary<int, int[]> pipes = new Dictionary<int, int[]>();
			string[] input = Properties.Resources.input_D13.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach (string s in input)
			{
				int index = int.Parse(s.Substring(0, s.IndexOf(' ')));
				int[] accessible = Array.ConvertAll(s.Substring(s.IndexOf('>') + 1).Split(','), a => int.Parse(a));
				pipes.Add(index, accessible);
			}

            int groupCount = 0;
            while (pipes.Count > 0)
            {
                List<int> inGroup = new List<int>();
                inGroup = (GetMembers(pipes, inGroup, pipes.First().Key));
                groupCount++;

                foreach (int i in inGroup) pipes.Remove(i);
            }
            return groupCount;
        }

        private static List<int> GetMembers(Dictionary<int, int[]> all, List<int> members, int member)
        {
            members.Add(member);
            foreach ( int i in all[member])
            {
                if (!members.Contains(i))
                {
                    GetMembers(all, members, i);
                }
            }

            return members;
        }
    }
}
