using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Tools;

namespace AdventOfCode
{
	class Day21
	{
		public static int Part1()
		{
            List<ArtistRule> artistRules = new List<ArtistRule>();

            var lines = Properties.Resources.input_D21.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string input = line.Substring(0, line.IndexOf(' '));
                Matrix<bool> m;
                if (input.Length == 5)
                {
                    m = new Matrix<bool>(
                        input[0] == '.', input[1] == '.', 
                        input[3] == '.', input[4] == '.');
                }
                else
                {
                    m = new Matrix<bool>(
                        input[0] == '.', input[1] == '.', input[2]  == '.',
                        input[4] == '.', input[5] == '.', input[6]  == '.',
                        input[8] == '.', input[9] == '.', input[10] == '.');
                }

                string output = line.Substring(line.IndexOf('>')+2);
                Matrix<bool> n;
                if (output.Length == 5)
                {
                    n = new Matrix<bool>(
                        output[0] == '.', output[1] == '.',
                        output[3] == '.', output[4] == '.');
                }
                else if (output.Length == 11 )
                {
                    n = new Matrix<bool>(
                        output[0] == '.', output[1] == '.', output[2]  == '.',
                        output[4] == '.', output[5] == '.', output[6]  == '.',
                        output[8] == '.', output[9] == '.', output[10] == '.');
                }
                else
                {
                    n = new Matrix<bool>(
                        output[0]  == '.', output[1]  == '.', output[2]  == '.', output[3]  == '.',
                        output[5]  == '.', output[6]  == '.', output[7]  == '.', output[8]  == '.',
                        output[10] == '.', output[11] == '.', output[12] == '.', output[13] == '.',
                        output[15] == '.', output[16] == '.', output[17] == '.', output[18] == '.');                   
                }
                artistRules.Add(new ArtistRule(m, n));
            }     

            // if grid length % 2
            // rules of 2 

            // else 
            // rules of 3





            return 0;
		}

		public static int Part2()
		{

			return 0;
		}		

	}

    class ArtistRule
    {
        public List<Matrix<bool>> rules;
        public Matrix<bool> output;

        public ArtistRule(Matrix<bool> inputRule, Matrix<bool> outputRule )
        {
            rules = new List<Matrix<bool>>();

            Matrix<bool> b = Matrix<bool>.Rotate90(inputRule);
            Matrix<bool> c = Matrix<bool>.Rotate90(b);
            Matrix<bool> d = Matrix<bool>.Rotate90(c);

            rules.Add(inputRule);
            rules.Add(b);
            rules.Add(c);
            rules.Add(d);
            rules.Add(Matrix<bool>.Flip(inputRule));
            rules.Add(Matrix<bool>.Flip(b));
            rules.Add(Matrix<bool>.Flip(c));
            rules.Add(Matrix<bool>.Flip(d));

            output = outputRule;
        }

    }


}
