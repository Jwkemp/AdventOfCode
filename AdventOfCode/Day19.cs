using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
	class Day19
	{
		enum Dir { Up, Down, Left, Right }; 

		private static void Move( Dir dir, ref int x, ref int y )
		{
			switch (dir)
			{
				case Dir.Down:
					y++;
					break;

				case Dir.Left:
					x--;
					break;

				case Dir.Right:
					x++;
					break;

				case Dir.Up:
					y--;
					break;
			}

		}

		private static bool FindNewDirection( ref Dir currDir, int x, int y, string[] grid)
		{
			if (currDir != Dir.Up && y < grid.Count() - 1 && grid[y + 1].ElementAt(x) != ' ') currDir = Dir.Down;
			else if (currDir != Dir.Right && x > 0 && grid[y].ElementAt(x - 1) != ' ') currDir = Dir.Left;
			else if (currDir != Dir.Left && x < grid[y].Count() - 1 && grid[y].ElementAt(x + 1) != ' ') currDir = Dir.Right;
			else if (currDir != Dir.Down && y > 0 && grid[y - 1].ElementAt(x) != ' ') currDir = Dir.Up;
			else return false;

			return true;
		}

		public static string Part1()
		{
			string result = "";
			string[] grid = Properties.Resources.input_D19.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);			
			int y = 0;
			int x = grid[y].IndexOf('|');
			Dir direction = Dir.Down;
			char currentChar = ' ';
			while ( x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Count())
			{
				currentChar = grid[y].ElementAt(x);
				switch (currentChar)
				{
					case '+': // new direction 
						if ( !FindNewDirection(ref direction, x, y, grid)) throw new Exception("WHERE DO WE GO!");
						break;

					case ' ': // broken
						return result;

					default:
						if (char.IsLetter(currentChar))
						{
							result += currentChar;
						}
						break;
				}
				Move(direction, ref x, ref y);
			}

			return result;
		}

		public static int Part2()
		{
			int steps = 0;
			string[] grid = Properties.Resources.input_D19.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			int y = 0;
			int x = grid[y].IndexOf('|');
			Dir direction = Dir.Down;
			char currentChar = ' ';
			while (x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Count())
			{
				currentChar = grid[y].ElementAt(x);
				switch (currentChar)
				{
					case '+': // new direction 
						if (!FindNewDirection(ref direction, x, y, grid)) throw new Exception("WHERE DO WE GO!");
						break;

					case ' ': // broken
						return steps;

					default:
						
						break;
				}
				Move(direction, ref x, ref y);
				steps++;
			}

			return steps;
		}


	}
}
