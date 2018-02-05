using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	enum Direction { up, right, down, left};

	enum Infection { clean, weak, infected, flagged }

	class Day22
	{
		public static int Part1()
		{
			bool draw = true;
			int infectedCount = 0;
			Tools.Vector2 currentNode = new Tools.Vector2(0, 0);
			Direction currentDir = Direction.up;
			Dictionary<Tools.Vector2, bool> map = new Dictionary<Tools.Vector2, bool>();
			ReadInput(Properties.Resources.input_D22, ref map, ref currentNode);
			if (draw) WriteToConsole(ref map, currentNode);

			for (int i = 0; i < 10000; ++i )
			{
				Move(ref currentDir, ref currentNode, ref map, ref infectedCount);
				if (draw)
				{
					WriteToConsole(ref map, currentNode);
					System.Threading.Thread.Sleep(500);
				}
			}
			
			return infectedCount;
		}

		public static int Part2()
		{
			int infectedCount = 0;
			Tools.Vector2 currentNode = new Tools.Vector2(0, 0);
			Direction currentDir = Direction.up;
			Dictionary<Tools.Vector2, Infection> map = new Dictionary<Tools.Vector2, Infection>();
			ReadInput2(Properties.Resources.input_D22, ref map, ref currentNode);

			for (int i = 0; i < 10000000; ++i)
			{
				Move2(ref currentDir, ref currentNode, ref map, ref infectedCount);				
			}

			return infectedCount;
		}

		private  static void WriteToConsole(ref Dictionary<Tools.Vector2, bool> map, Tools.Vector2 pos )
		{
			Console.Clear();
			// find bounds
			int lowestX = int.MaxValue;
			int highestX = int.MinValue;
			int lowestY = int.MaxValue;
			int highestY = int.MinValue;

			foreach ( KeyValuePair<Tools.Vector2, bool> key in map)
			{
				if (key.Key.x < lowestX) lowestX = key.Key.x;
				if (key.Key.x > highestX) highestX = key.Key.x;
				if (key.Key.y < lowestY) lowestY = key.Key.y;
				if (key.Key.y > highestY) highestY = key.Key.y;
			}

			for (int y = lowestY; y <= highestY; ++y )
			{
				for (int x = lowestX; x <= highestX; ++x)
				{
					if ( map.ContainsKey(new Tools.Vector2(x,y)))
					{
						if ( map[new Tools.Vector2(x, y)] )
						{
							if ( pos.x == x && pos.y == y ) Console.ForegroundColor = ConsoleColor.Red;
							else Console.ForegroundColor = ConsoleColor.DarkGreen;
							Console.Write("#");
						}
						else
						{
							if (pos.x == x && pos.y == y) Console.ForegroundColor = ConsoleColor.Red;
							else Console.ForegroundColor = ConsoleColor.White;
							Console.Write(".");
						}						
					}
					else
					{
						Console.Write(".");
					}					
				}
				Console.WriteLine();
			}
		}

		private static void ReadInput(string input, ref Dictionary<Tools.Vector2, bool> map, ref Tools.Vector2 middle)
		{
			string[] inputs = Properties.Resources.input_D22.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < inputs.Length; ++i)
			{
				for ( int j = 0; j < inputs[i].Length; ++j )
				{
					if ( inputs[i][j] == '.' )
					{
						map.Add(new Tools.Vector2(j, i), false);
					}
					else if (inputs[i][j] == '#')
					{
						map.Add(new Tools.Vector2(j, i), true);
					}					
				}				
			}
			middle = new Tools.Vector2(inputs[0].Length / 2, inputs.Length / 2);
		}

		private static void ReadInput2(string input, ref Dictionary<Tools.Vector2, Infection> map, ref Tools.Vector2 middle)
		{
			string[] inputs = Properties.Resources.input_D22.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < inputs.Length; ++i)
			{
				for (int j = 0; j < inputs[i].Length; ++j)
				{
					if (inputs[i][j] == '.')
					{
						map.Add(new Tools.Vector2(j, i), Infection.clean);
					}
					else if (inputs[i][j] == '#')
					{
						map.Add(new Tools.Vector2(j, i), Infection.infected);
					}
				}
			}
			middle = new Tools.Vector2(inputs[0].Length / 2, inputs.Length / 2);
		}

		private static void Move(ref Direction d, ref Tools.Vector2 pos, ref Dictionary<Tools.Vector2, bool> map, ref int infected)
		{
			// Turn
			bool success = map.ContainsKey(pos);
			if (success)
			{
				if (map[pos])
				{
					d = TurnRight(d);
				}
				else
				{
					d = TurnLeft(d);
				}
			}
			else
			{
				map.Add(new Tools.Vector2(pos.x,pos.y), false);
				d = TurnLeft(d);
			}

			// Toggle node
			map[pos] = !map[pos];
			if (map[pos])
				infected++;

			// Move
			switch (d)
			{
				case Direction.up:
					pos.y--;
					break;

				case Direction.right:
					pos.x++;
					break;

				case Direction.down:
					pos.y++;
					break;

				case Direction.left:
					pos.x--;
					break;

				default:
					throw new Exception("erm wut?");
			}
		}

		private static void Move2(ref Direction d, ref Tools.Vector2 pos, ref Dictionary<Tools.Vector2, Infection> map, ref int infected)
		{
			// Turn
			bool success = map.ContainsKey(pos);
			if (success)
			{
				switch (map[pos])
				{
					case Infection.clean:
						d = TurnLeft(d);
						break;

					case Infection.weak:
						// do not turn
						break;

					case Infection.infected:
						d = TurnRight(d);
						break;

					case Infection.flagged:
						d = TurnAround(d);
						break;

					default:
						throw new Exception("SHUT DOWN EVERYTHING");
						break;
				}
			}
			else
			{
				map.Add(new Tools.Vector2(pos.x, pos.y), 0);
				d = TurnLeft(d);
			}

			// Increase Node
			map[pos] = IncInfection(map[pos]);
			if (map[pos] == Infection.infected)
				infected++;

			// Move
			switch (d)
			{
				case Direction.up:
					pos.y--;
					break;

				case Direction.right:
					pos.x++;
					break;

				case Direction.down:
					pos.y++;
					break;

				case Direction.left:
					pos.x--;
					break;

				default:
					throw new Exception("erm wut?");
			}
		}

		private static Infection IncInfection(Infection currentLevel)
		{
			if (currentLevel != Infection.flagged)			
				currentLevel++;			
			else			
				currentLevel = Infection.clean;

			return currentLevel;
		}

		private static Direction TurnRight( Direction dir )
		{
			if (dir != Direction.left)
				dir++;
			else
				dir = Direction.up;

			return dir;
		}

		private static Direction TurnLeft( Direction dir ) 
		{
			if (dir != Direction.up)
				dir--;
			else
				dir = Direction.left;

			return dir;
		}

		private static Direction TurnAround(Direction dir)
		{
			switch (dir)
			{
				case Direction.up:		return Direction.down; 
				case Direction.right:	return Direction.left; 
				case Direction.left:	return Direction.right; 
				case Direction.down:	return Direction.up;
			}
			return dir;
		}
	}
}
