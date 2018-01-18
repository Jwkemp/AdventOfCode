using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
	using Vector2 = Tools.Vector2;

	class Day11
    {
        public static int Part1()
        {            
            Vector2 childPos = new Vector2(0, 0);
            List<string> input = new List<string>();
            input.AddRange(Properties.Resources.input_D11.Split(','));            

            foreach ( string dir in input )
            {
                if (dir.Contains('n')) childPos.y += 1;
                if (dir.Contains('e')) childPos.x += 1;
                if (dir.Contains('s')) childPos.y -= 1;
                if (dir.Contains('w')) childPos.x -= 1;                
            }
            return (childPos.x > childPos.y) ? childPos.x : childPos.y;
        }

        public static int Part2()
        {
            int highestDist = 0;
            Vector2 childPos = new Vector2(0, 0);
            List<string> input = new List<string>();
			input.AddRange(Properties.Resources.input_D11.Split(','));

			foreach (string dir in input)
            {
                if (dir.Contains('n')) childPos.y += 1;
                if (dir.Contains('e')) childPos.x += 1;
                if (dir.Contains('s')) childPos.y -= 1;
                if (dir.Contains('w')) childPos.x -= 1;

                if (childPos.x > highestDist) highestDist = childPos.x;
                if (childPos.y > highestDist) highestDist = childPos.y;
            }
            return highestDist;
        }
    }
}
