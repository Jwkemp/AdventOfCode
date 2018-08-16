
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;

namespace AoC2016
{
    public class Day03 : IDay
    {
        List<Tuple<int, int, int>> GetInput()
        {
            List<Tuple<int, int, int>> possibleTriangles = new List<Tuple<int, int, int>>();
            var lines = Properties.Resource.input_D03.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            foreach (string line in lines)
            {
                var args = line.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                possibleTriangles.Add(new Tuple<int, int, int>(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2])));
            }
            return possibleTriangles;
        }

        private bool AreValidTriangleLengths(int a, int b, int c)
        {
            if (a + b > c && b + c > a && a + c > b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object Part1()
        {
            int validTriangleCount = 0;

            List<Tuple<int, int, int>> possibleTriangles = GetInput();
            foreach (Tuple<int, int, int> possibleTriangle in possibleTriangles)
            {
                if (AreValidTriangleLengths(possibleTriangle.Item1, possibleTriangle.Item2, possibleTriangle.Item3))
                {
                    validTriangleCount++;
                }
            }
            return validTriangleCount;
        }

        public object Part2()
        {
            int validTriangleCount = 0;

            List<Tuple<int, int, int>> possibleTriangles = GetInput();
            for (int i = 0; i < possibleTriangles.Count; i += 3 )
            {
                if (AreValidTriangleLengths(possibleTriangles[i].Item1, possibleTriangles[i + 1].Item1, possibleTriangles[i + 2].Item1))
                {
                    validTriangleCount++;
                }
                if (AreValidTriangleLengths(possibleTriangles[i].Item2, possibleTriangles[i + 1].Item2, possibleTriangles[i + 2].Item2))
                {
                    validTriangleCount++;
                }
                if (AreValidTriangleLengths(possibleTriangles[i].Item3, possibleTriangles[i + 1].Item3, possibleTriangles[i + 2].Item3))
                {
                    validTriangleCount++;
                }
            }
            return validTriangleCount;
        }
    }
}
