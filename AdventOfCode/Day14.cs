using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	class Day14
	{
        public static int Part1()
		{
			int output = 0;
			for (int i = 0; i < 128; ++i)
			{
				string s = KnotHash("oundnydw-" + i);
				string r = "";
				foreach(char c in s)
				{
					int decValue = Convert.ToInt16(c.ToString(), 16);
					r += Convert.ToString(decValue, 2);
				}
				r = r.Replace("0",string.Empty);
				output += r.Length;				
			}
			return output;
		}

		public static int Part2()
		{
			int size = 128;
			int[,] disc = new int[size, size];

			for (int i = 0; i < size; ++i)
			{
                string s = KnotHash("oundnydw-" + i);
				string r = "";
				foreach (char c in s)
				{
					int decValue = Convert.ToInt16(c.ToString(), 16);

                    string t = Convert.ToString(decValue, 2);
                    for (int p = t.Length; p < 4; ++p)
                    {
                        t = "0" + t;
                    }                                 
                    r += t;
				}
				for (int j = 0; j < r.Length; ++j)
				{
					disc[i, j] = int.Parse(r[j].ToString());
				}
			}

            // Set up regions
            int regionNum = 2;
			for (int x = 0; x < size; ++x)
			{
				for (int y = 0; y < size; ++y)
				{
					if (disc[x, y] == 1)
					{
						AssignToRegion(disc, x, y, regionNum);
						regionNum++;
					}
				}
			}         

            return regionNum-2;
		}

		private static void AssignToRegion(int[,] disc, int x, int y, int region)
		{
			if (disc[x, y] == 1)
			{
				disc[x, y] = region;
				if (x < disc.GetLength(0)-1 )
				{
					if (disc[x + 1, y] == 1) //right
					{
						AssignToRegion(disc, x + 1, y, region);
					}
				}
				if (x > 0)
				{
					if (disc[x - 1, y] == 1) //left
					{
						AssignToRegion(disc, x - 1, y, region);
					}
				}

				if (y < disc.GetLength(0)-1)
				{
					if (disc[x, y + 1] == 1) //down
					{
						AssignToRegion(disc, x, y + 1, region);
					}
				}
				if (y > 0)
				{
					if (disc[x, y - 1] == 1) //up
					{
						AssignToRegion(disc, x, y - 1, region);
					}
				}
			}
		}

		private static string KnotHash(string input )
		{		
			List<int> inputList = new List<int>();
			foreach (char c in input)
			{
				inputList.Add(c);
			}
			inputList.AddRange(new int[] { 17, 31, 73, 47, 23 });

			List<int> sparseHash = new List<int>();
			for (int i = 0; i < 256; i++)
			{
				sparseHash.Add(i);
			}

			int currIndx = 0;
			int skipSize = 0;
			for (int j = 0; j < 64; ++j)
			{
				foreach (int i in inputList)
				{
					var temp = Day10.ReverseRange(sparseHash, currIndx, i);
					sparseHash = Day10.ReplaceRange(sparseHash, temp, currIndx, i);

					currIndx += i + skipSize;
					skipSize++;
					while (currIndx > sparseHash.Count) currIndx -= sparseHash.Count;
				}
			}

			List<int> denseHash = new List<int>();
			for (int i = 0; i < 16; ++i)
			{
				int j = i * 16;
				denseHash.Add(sparseHash[j] ^ sparseHash[j + 1] ^ sparseHash[j + 2] ^ sparseHash[j + 3] ^ sparseHash[j + 4] ^ sparseHash[j + 5] ^ sparseHash[j + 6] ^ sparseHash[j + 7] ^ sparseHash[j + 8] ^ sparseHash[j + 9] ^ sparseHash[j + 10] ^ sparseHash[j + 11] ^ sparseHash[j + 12] ^ sparseHash[j + 13] ^ sparseHash[j + 14] ^ sparseHash[j + 15]);
			}

			string output = "";
			foreach (int i in denseHash)
				output += i.ToString("X2");

			return output;
		}

        
	}
}
