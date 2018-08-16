using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2016
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day05();
     //       Console.WriteLine(day.Part1().ToString());
            Console.WriteLine(day.Part2().ToString());
            Console.ReadLine();

        }
    }
}
