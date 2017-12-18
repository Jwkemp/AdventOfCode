using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day05
    {
        public static int Part1()
        {
            List<int> input = new List<int>();

            using (StreamReader sr = new StreamReader(Properties.Resources.input_D5))
            {
                while (!sr.EndOfStream)
                {
                    int n = Int32.Parse(sr.ReadLine());
                    input.Add(n);
                }
            }
            int index = 0;
            int value = 0;
            int stepCount = 0;
            while (index < input.Count())
            {
                stepCount++;
                value = input[index];
                input[index]++;
                index += value;

            }

            return stepCount;
        }

        public static int Part2()
        {
            List<int> input = new List<int>();

            using (StreamReader sr = new StreamReader(Properties.Resources.input_D5))
            {
                while (!sr.EndOfStream)
                {
                    int n = Int32.Parse(sr.ReadLine());
                    input.Add(n);
                }
            }
            int index = 0;
            int value = 0;
            int stepCount = 0;
            while (index < input.Count())
            {
                stepCount++;
                value = input[index];
                if (input[index] >= 3) input[index]--;
                else input[index]++;
                index += value;

            }

            return stepCount;
        }

    }
}
