using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AdventOfCode
{
    class Day15
    {

        public static int Part1()
        {
            Generator A = new Generator(722, 16807);
            Generator B = new Generator(354, 48271);

            int matches = 0;
            for (int i = 0; i < 40000000; ++i)
            {
                A.Next();
                B.Next();

                if ( A.Last16Bits == B.Last16Bits)
                {
                    matches++;
                }
            }
            
            return matches;
        }


        public static int Part2()
        {
            Generator A = new Generator(722, 16807, 4);
            Generator B = new Generator(354, 48271, 8);

            int matches = 0;
            for (int i = 0; i < 5000000; ++i)
            {
                A.Next();
                B.Next();

                if (A.Last16Bits == B.Last16Bits)
                {
                    matches++;
                }
            }

            return matches;
        }     


        private class Generator
        {
            public long current;
            int factor;
            public string Last16Bits { get; private set; }
            int canDivBy;

            public Generator(long _current, int _factor, int divBy = 1 )
            {
                current = _current;
                factor = _factor;
                canDivBy = divBy;
            }

            public void Next()
            {
                current *= factor;
                Math.DivRem(current, 2147483647, out current);
                while (current % canDivBy != 0)
                {
                    current *= factor;
                    Math.DivRem(current, 2147483647, out current);
                }
                Last16Bits = Convert.ToString((current & 0xFFFF), 2);

            }
        }
    }
}
