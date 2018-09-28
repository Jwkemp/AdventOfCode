using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2016
{
    class Day08 : IDay
    {
        bool[][] littleScreen;

        private string[] GetInput()
        {
            var lines = Properties.Resource.input_D08.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return lines;
        }

        public object Part1()
        {
            littleScreen = new bool[6][];
            littleScreen[0] = new bool[50];
            littleScreen[1] = new bool[50];
            littleScreen[2] = new bool[50];
            littleScreen[3] = new bool[50];
            littleScreen[4] = new bool[50];
            littleScreen[5] = new bool[50];          

            for (int x = 0; x < 6; ++x)
            {
                for (int y = 0; y < 50; ++y)
                {
                    littleScreen[x][y] = false;
                }
            }

            var input = GetInput();

            foreach (string command in input)
            {
                var tokens = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                if (tokens[0].StartsWith("rect"))
                {
                    string[] tokens2 = tokens[1].Split('x');
                    Rect(int.Parse(tokens2[0]), int.Parse(tokens2[1]));
                }
                else if (tokens[0].StartsWith("rotate"))
                {
                    if (tokens[1] == "row")
                    {
                        int by = int.Parse(tokens[4]);
                        int row = int.Parse(tokens[2].Substring(tokens[2].IndexOf('=')+1));

                        RotateRow(row, by);

                    }
                    else if (tokens[1] == "column")
                    {
                        int by = int.Parse(tokens[4]);
                        int col = int.Parse(tokens[2].Substring(tokens[2].IndexOf('=')+1));

                        RotateColumn(col, by);
                    }
                    else
                    {
                        throw new Exception("ERROR: Rotate how?");
                    }
                }
                Console.Clear();
                Draw();               
                Thread.Sleep(100);
                
            }

            int count = 0;
            for (int x = 0; x < 6; ++x)
            {
                for (int y = 0; y < 50; ++y)
                {
                    if (littleScreen[x][y]) count++;
                }
            }
            return count;
        }


        private void Rect(int wide, int tall)
        {
            for (int x = 0; x < wide; ++x)
            {
                for (int y = 0; y < tall; ++y)
                {
                    littleScreen[y][x] = true;
                }
            }
        }

        private void RotateRow(int row, int by)
        {
            bool[] oldRow = new bool[50];

            for (int i = 0; i < 50; ++i) oldRow[i] = littleScreen[row][i];

            for (int i = 0; i < littleScreen[row].Length; i++)
            {
                littleScreen[row][(i + by) % littleScreen[row].Length] = oldRow[i];
            }           
        }

        private void RotateColumn(int col, int by)
        {
            bool[] oldColumn = new bool[6];

            for (int i = 0; i < 6; ++i) oldColumn[i] = littleScreen[i][col];

            for (int i = 0; i < littleScreen.Length; i++)
            {
                littleScreen[(i + by) % littleScreen.Length][col] = oldColumn[i];
            }
        }

        private void Draw()
        {
            for (int x = 0; x < littleScreen.Length; ++x)
            {                
                for (int y = 0; y < littleScreen[x].Length; ++y)
                {
                    if (littleScreen[x][y]) Console.Write("X");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public object Part2()
        {
            return 0;
        }
    }
}
