using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day17
    {
        public static int Part1()
        {
            int stepSize = 355;
            int currIndex = 0;
            List<int> buffer = new List<int>();
            buffer.Add(0);

            for ( int i = 1; i <= 2017; ++i )
            {
                currIndex = (currIndex + stepSize) % buffer.Count() + 1;
                buffer.Insert(currIndex, i);                
            }
            return buffer[currIndex+1];
        }

        public static int Part2()
        {
            int stepSize = 355;
            int currIndex = 0;
            int bufferSize = 1;
            int after0 = 0;

            for (int i = 1; i <= 50000000; ++i)
            {
                // step to new selected index
                currIndex = (currIndex + stepSize) % bufferSize++;

                if (currIndex == 0) after0 = i;
                currIndex++;
            }
            return after0;
        }


    }
}
