using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace AdventOfCode
{
    public static class Extensions
    {
        public static void TryAdd<k, v>(this Dictionary<k, v> dictionary, k key, v value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }
    }

    class Day21
    {

        public static int Part1()
        {
            return Solve(5);
        }

        public static int Part2()
        {
            return Solve(18);
        }

        private static int Solve(int interations )
        {
            string[] rules = Properties.Resources.input_D21.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var separators = new char[] { ' ', '=', '>' };

            Dictionary<string, string> rulesMap = new Dictionary<string, string>();

            foreach (var rule in rules)
            {
                var tokens = rule.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string from = tokens[0];
                string to = tokens[1];

                for (int i = 0; i < 4; i++)
                {
                    var newFrom = Rotate(from);
                    rulesMap.TryAdd(newFrom, to);
                    rulesMap.TryAdd(FlipHorizontal(newFrom), to);
                    rulesMap.TryAdd(FlipVertical(newFrom), to);

                    from = newFrom;
                }
            }

            string[] grid = new string[]
            {
            ".#.",
            "..#",
            "###",
            };

            grid = Enhance(iterations: 18, grid: grid, rules: rulesMap);

            return CountActivePixels(grid);
        }

        private static string FlipHorizontal(string grid)
        {
            string[] rows = grid.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] newRows = new string[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                newRows[i] = string.Join("", rows[i].Reverse());
            }

            return string.Join<string>("/", newRows);
        }

        private static string FlipVertical(string grid)
        {
            string[] rows = grid.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] newRows = new string[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                newRows[rows.Length - i - 1] = rows[i];
            }

            return string.Join<string>("/", newRows);
        }

        private static string Rotate(string grid)
        {
            string[] rows = grid.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            char[,] newRows = new char[rows.Length, rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    newRows[rows.Length - j - 1, i] = rows[i][j];
                }
            }

            string[] sNewRows = new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    sNewRows[i] += newRows[i, j];
                }
            }

            string result = string.Join("/", sNewRows);
            return result;
        }

        private static string CopyFrom(string[] grid, int startRow, int startColumn, int num)
        {
            string[] section = new string[num];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    section[i] += grid[i + startRow][j + startColumn];
                }
            }

            return string.Join("/", section);
        }

        private static void CopyTo(string[] grid, string section, int size, int startRow, int startColumn)
        {
            string[] rows = section.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[startRow + i] += rows[i][j];
                }
            }
        }

        private static string[] EnhanceStep(string[] grid, Dictionary<string, string> rules, int size)
        {
            int newSize = size + 1;

            string[] newGrid = new string[grid.Length / size * newSize];

            for (int j = 0; j * size < grid.Length; j++)
            {
                for (int k = 0; k * size < grid.Length; k++)
                {
                    string section = CopyFrom(grid, j * size, k * size, size);
                    CopyTo(newGrid, rules[section], newSize, j * newSize, k * newSize);
                }
            }

            return newGrid;
        }

        private static string[] Enhance(int iterations, string[] grid, Dictionary<string, string> rules)
        {
            for (int i = 0; i < iterations; i++)
            {
                if (grid.Length % 2 == 0)
                {
                    grid = EnhanceStep(grid, rules, size: 2);
                }
                else // % 3 == 0
                {
                    grid = EnhanceStep(grid, rules, size: 3);
                }
            }

            return grid;
        }

        private static int CountActivePixels(string[] grid)
        {
            int countOn = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {
                    if (grid[i][j] == '#')
                    {
                        countOn++;
                    }
                }
            }

            return countOn;
        }
    }
}