using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tools
{
    class Matrix<t>
    {
        public t[,] values;
        int size;

        public Matrix(int _size)
        {
            size = _size;
            values = new t[size, size];
        }

        public Matrix(params t[] vals)
        {
            size = (int)Math.Round(Math.Sqrt(vals.Length));
            values = new t[size, size];

            int count = 0;
            for (int row = 0; row < size; ++row)
            {
                for (int col = 0; col < size; ++col)
                {
                    values[row, col] = vals[count];
                    count++;
                }
            }
        }

        public static Matrix<t> Rotate90(Matrix<t> original)
        {
            Matrix<t> result = new Matrix<t>(original.values.GetLength(0));

            int newCol = 0, newRow = 0;
            for (int col = 0; col < original.values.GetLength(0); ++col)
            {
                newRow = 0;
                for (int row = original.values.GetLength(0) - 1; row >= 0; --row)
                {
                    result.values[newCol, newRow] = original.values[row, col];
                    newRow++;
                }
                newCol++;
            }
            return result;
        }

        public static Matrix<t> Flip(Matrix<t> original)
        {
            Matrix<t> result = new Matrix<t>(original.values.GetLength(0));

            int newCol = 0, newRow = 0;
            for (int col = 0; col < original.values.GetLength(0); ++col)
            {
                newRow = 0;
                for (int row = original.values.GetLength(0) - 1; row >= 0; --row)
                {
                    result.values[newRow, newCol] = original.values[row, col];
                    newRow++;
                }
                newCol++;
            }
            return result;
        }
    }
}
