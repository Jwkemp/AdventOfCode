using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	using Vector2 = Tools.Vector2;

    class Day03
    {
        private const int input = 265149;

        private const int RIGHT = 1;
        private const int UP    = 2;
        private const int LEFT  = 3;
        private const int DOWN  = 4;

        // clean up types
        public static double Part1()
        {
            double n = 1;
            while (Math.Pow((n + n - 1), 2) < input)
            {
                n++;
            }

            double gridLength = (n + n - 1);
            double maxValue = Math.Pow(gridLength, 2);
            double diff = maxValue - input;

            while (diff >= (gridLength - 1))
            {
                diff -= (gridLength - 1);
            }

            double output = (diff > Math.Floor(gridLength / 2)) ? diff - Math.Floor(gridLength / 2) : Math.Floor(gridLength / 2) - diff;

            output += Math.Floor(gridLength / 2);

            return output;
        }

        // Needs clean up
        public static int Part2()
        {
            int gridSize = 2000;
            int[,] grid = new int[gridSize, gridSize];

            Vector2 origin = new Vector2(gridSize / 2, gridSize / 2);
            Vector2 currPos = origin;
            int currCellvalue = 0;
            int dir = RIGHT;
            int sideLength = 1;
            int progressAlongSide = 0;

            grid[origin.x, origin.y] = 1;
            while (currCellvalue < input)
            {
                // Set new position
                switch (dir)
                {
                    case RIGHT:
                        currPos.x += 1;
                        break;
                    case UP:
                        currPos.y += 1;
                        break;
                    case LEFT:
                        currPos.x -= 1;
                        break;
                    case DOWN:
                        currPos.y -= 1;
                        break;
                }
                progressAlongSide += 1;

                // Find next Position
                if (progressAlongSide == sideLength)
                {
                    progressAlongSide = 0;

                    if (dir == DOWN) dir = RIGHT;
                    else dir += 1;

                    if (dir == RIGHT || dir == LEFT) sideLength += 1;
                }

                // Find cell value
                currCellvalue = 0;
                if (currPos.x + 1 < grid.GetLength(0))
                {
                    if (currPos.y + 1 < grid.GetLength(1))
                        currCellvalue += grid[currPos.x + 1, currPos.y + 1];

                    currCellvalue += grid[currPos.x + 1, currPos.y];

                    if (currPos.y - 1 >= 0)
                        currCellvalue += grid[currPos.x + 1, currPos.y - 1];
                }

                if (currPos.y + 1 < grid.GetLength(1))
                    currCellvalue += grid[currPos.x, currPos.y + 1];

                if (currPos.y - 1 >= 0)
                    currCellvalue += grid[currPos.x, currPos.y - 1];

                if (currPos.x - 1 >= 0)
                {
                    if (currPos.y + 1 < grid.GetLength(1))
                        currCellvalue += grid[currPos.x - 1, currPos.y + 1];

                    currCellvalue += grid[currPos.x - 1, currPos.y];

                    if (currPos.y - 1 >= 0)
                        currCellvalue += grid[currPos.x - 1, currPos.y - 1];
                }

                grid[currPos.x, currPos.y] = currCellvalue;
            }
            return currCellvalue;
        }
    }
}
