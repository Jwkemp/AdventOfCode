using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	class Day21
	{
		public static int Part1()
		{
            List<ArtistRule3> artistRules = new List<ArtistRule3>();

            artistRules.Add(new ArtistRule3());

            Matrix3 a = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Matrix3 b = Matrix3.Rotate90(a);
            Matrix3 c = Matrix3.Rotate90(b);
            Matrix3 d = Matrix3.Rotate90(c);

            artistRules.Last().rules.Add(a);
            artistRules.Last().rules.Add(b);
            artistRules.Last().rules.Add(c);
            artistRules.Last().rules.Add(d);

            artistRules.Last().rules.Add(Matrix3.Flip(a));
            artistRules.Last().rules.Add(Matrix3.Flip(b));
            artistRules.Last().rules.Add(Matrix3.Flip(c));
            artistRules.Last().rules.Add(Matrix3.Flip(d));

            return 0;
		}

		public static int Part2()
		{

			return 0;
		}		

	}

    class ArtistRule3
    {
        public List<Matrix3> rules = new List<Matrix3>();
        public Matrix2 output;

    }

    class ArtistRule2
    {
        public List<Matrix2> rules = new List<Matrix2>();
        public Matrix3 output;

    }


    class Matrix3
    {
        int[,] values = new int[3,3];

        public Matrix3()
        {
            values[0, 0] = 0;
            values[0, 1] = 0;
            values[0, 2] = 0;

            values[1, 0] = 0;
            values[1, 1] = 0;
            values[1, 2] = 0;

            values[2, 0] = 0;
            values[2, 1] = 0;
            values[2, 2] = 0;
        }

        public Matrix3(int a, int b, int c, int d, int e, int f, int g, int h, int i)
        {
            values[0, 0] = a;
            values[0, 1] = b;
            values[0, 2] = c;

            values[1, 0] = d;
            values[1, 1] = e;
            values[1, 2] = f;

            values[2, 0] = g;
            values[2, 1] = h;
            values[2, 2] = i;
        }

        public static Matrix3 Rotate90( Matrix3 original )
        {
            Matrix3 result = new Matrix3();

            result.values[0, 0] = original.values[2, 0];
            result.values[0, 1] = original.values[1, 0];
            result.values[0, 2] = original.values[0, 0];

            result.values[1, 0] = original.values[2, 1];
            result.values[1, 1] = original.values[1, 1];
            result.values[1, 2] = original.values[0, 1];

            result.values[2, 0] = original.values[2, 2];
            result.values[2, 1] = original.values[1, 2];
            result.values[2, 2] = original.values[0, 2];

            return result;
        }

        public static Matrix3 Flip( Matrix3 original)
        {
            Matrix3 result = new Matrix3();

            result.values[0, 0] = original.values[2, 0];
            result.values[0, 1] = original.values[2, 1];
            result.values[0, 2] = original.values[2, 2];

            result.values[1, 0] = original.values[1, 0];
            result.values[1, 1] = original.values[1, 1];
            result.values[1, 2] = original.values[1, 2];

            result.values[2, 0] = original.values[0, 0];
            result.values[2, 1] = original.values[0, 1];
            result.values[2, 2] = original.values[0, 2];

            return result;
        }

    }

    class Matrix2
    {
        int[,] values = new int[2, 2];


        public Matrix2()
        {
            values[0, 0] = 0;
            values[0, 1] = 0;
            values[1, 0] = 0;
            values[1, 1] = 0;


        } 


    }


}
