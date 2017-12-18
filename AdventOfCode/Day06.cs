using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day06
    {
        private static int[] stacks = { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };

        public static int Part1()
        {            
            List<string> stackStates = new List<string>();
            int steps = 0;

            while (!stackStates.Contains(StacksToString(stacks)))
            {
                steps++;
                stackStates.Add(StacksToString(stacks));

                // Distribute Stacks
                int highestStack = FindHighestStack(stacks);
                int value = stacks[highestStack];
                int currStack = highestStack;
                stacks[highestStack] = 0;
                while (value > 0)
                {
                    currStack = currStack < stacks.Length - 1 ? currStack + 1 : 0;
                    stacks[currStack] += 1;
                    value--;
                }
            }

            return steps;
        }

        public static int Part2()
        {
            List<string> stackStates = new List<string>();
            int steps = 0;

            while (!stackStates.Contains(StacksToString(stacks)))
            {
                steps++;
                stackStates.Add(StacksToString(stacks));

                int highestStack = FindHighestStack(stacks);
                int value = stacks[highestStack];
                int currStack = highestStack;
                stacks[highestStack] = 0;
                while (value > 0)
                {
                    currStack = currStack < stacks.Length - 1 ? currStack + 1 : 0;
                    stacks[currStack] += 1;
                    value--;
                }
            }
            return steps - stackStates.IndexOf(StacksToString(stacks));
        }

        private static string StacksToString(int[] stacks)
        {
            string output = "";
            foreach (int i in stacks)
            {
                output += i.ToString();
            }
            return output;
        }

        private static int FindHighestStack(int[] stacks)
        {
            int highestStackValue = 0;
            int highestStack = 0;
            for (int i = 0; i < stacks.Length; ++i)
            {
                if (stacks[i] > highestStackValue)
                {
                    highestStackValue = stacks[i];
                    highestStack = i;
                }
            }
            return highestStack;
        }

    }
}
