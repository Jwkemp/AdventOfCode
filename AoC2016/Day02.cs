
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;

namespace AoC2016
{
    public static class Extentions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }

    public class Day02 : IDay
    {
        List<string> GetInput()
        {
            return Properties.Resource.input_D02.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }        

        public object Part1()
        {
            string code = "";
            Vector lastBtn = new Vector(0, 0);
            Vector currPos = lastBtn;
            List<string> instructions = GetInput();

            foreach (string s in instructions)
            {
                foreach (char c in s)
                {
                    switch (c)
                    {
                        case 'U':
                            currPos.Y += 1;
                            break;

                        case 'R':
                            currPos.X += 1;
                            break;

                        case 'D':
                            currPos.Y -= 1;
                            break;

                        case 'L':
                            currPos.X -= 1;
                            break;
                    }
                    currPos.X = currPos.X.Clamp(-1, 1);
                    currPos.Y = currPos.Y.Clamp(-1, 1);
                }
                code += VectorToKeyPad1(currPos);
            }
            return code;
        }

        public object Part2()
        {
            string code = "";
            Vector lastBtn = new Vector(0, 0);
            Vector currPos = lastBtn;
            List<string> instructions = GetInput();

            foreach (string s in instructions)
            {
                foreach (char c in s)
                {
                    double Y = currPos.Y;
                    double X = currPos.X;

                    switch (c)
                    {
                        case 'U':
                            if      (X == -2 || X == 2) currPos.Y = 0;
                            else if ( X == 0 )          currPos.Y = (currPos.Y + 1).Clamp(-2,2);
                            else                        currPos.Y = (currPos.Y + 1).Clamp(-1, 1);
                            break;

                        case 'R':
                            if      (Y == -2 || Y == 2) currPos.X = 0;
                            else if (Y == 0)            currPos.X = (currPos.X + 1).Clamp(-2, 2);
                            else                        currPos.X = (currPos.X + 1).Clamp(-1, 1);
                            break;

                        case 'D':
                            if      (X == -2 || X == 2) currPos.Y = 0;
                            else if (X == 0)            currPos.Y = (currPos.Y - 1).Clamp(-2, 2);
                            else                        currPos.Y = (currPos.Y - 1).Clamp(-1, 1);
                            break;

                        case 'L':
                            if      (Y == -2 || Y == 2) currPos.X = 0;
                            else if (Y == 0)            currPos.X = (currPos.X - 1).Clamp(-2, 2);
                            else                        currPos.X = (currPos.X - 1).Clamp(-1, 1);
                            break;
                    }
                }
                code += VectorToKeyPad2(currPos);
            }
            return code;
        }


        private int VectorToKeyPad1(Vector currPos)
        {
            if (currPos == new Vector(-1, 1))       return 1;
            else if (currPos == new Vector(0, 1))   return 2;
            else if (currPos == new Vector(1, 1))   return 3;
            else if (currPos == new Vector(-1, 0))  return 4;
            else if (currPos == new Vector(0, 0))   return 5;
            else if (currPos == new Vector(1, 0))   return 6;
            else if (currPos == new Vector(-1, -1)) return 7;
            else if (currPos == new Vector(0, -1))  return 8;
            else if (currPos == new Vector(1, -1))  return 9;

            throw new Exception("Unknown key");
        }


        private char VectorToKeyPad2(Vector currPos)
        {
            if (currPos == new Vector(0, 2)) return '1';

            else if (currPos == new Vector(-1, 1)) return '2';
            else if (currPos == new Vector(0, 1))  return '3';
            else if (currPos == new Vector(1, 1))  return '4';

            else if (currPos == new Vector(-2, 0)) return '5';
            else if (currPos == new Vector(-1, 0)) return '6';
            else if (currPos == new Vector(0, 0))  return '7';
            else if (currPos == new Vector(1, 0))  return '8';
            else if (currPos == new Vector(2, 0))  return '9';

            else if (currPos == new Vector(-1, -1)) return 'A';
            else if (currPos == new Vector(0, -1))  return 'B';
            else if (currPos == new Vector(1, -1))  return 'C';

            else if (currPos == new Vector(0, -2))  return 'D';

            throw new Exception("Unknown key");
        }
    }
}
