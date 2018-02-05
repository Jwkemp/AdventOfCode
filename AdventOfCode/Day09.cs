using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day09
    {

        public static int Part1()
        {
            int score = 0;
			string input = Properties.Resources.input_D09;

			Char currentChar = ' ';
			bool isGarbage = false;
			bool isIgnored = false;
			int level = 0;
			foreach (char c in input)
			{
				currentChar = c;

                if (isIgnored)
                {
                    isIgnored = false;
                }
                else if (currentChar == '!')
                {
                    isIgnored = true;
                }
                else if ( isGarbage )
                {
                    if (currentChar == '>') 
                            isGarbage = false;
                }
                else if (currentChar == '<')
                {
                    isGarbage = true;
                }
                else if (currentChar == '{')
                {
                    level++;
                }
                else if ( currentChar == '}' )
                {
                    score += level;
                    level--;
                }
                               
            }
            return score;
        }
        
        public static int Part2()
        {
            int score = 0;
			string input = Properties.Resources.input_D09;

			Char currentChar = ' ';
			bool isGarbage = false;
			bool isIgnored = false;
			int level = 0;
			foreach (char c in input)
			{            
                currentChar = c;

                if (isIgnored)
                {
                    isIgnored = false;
                }
                else if (currentChar == '!')
                {
                    isIgnored = true;
                }
                else if (isGarbage)
                {
                    if (currentChar == '>')
                        isGarbage = false;
                    else
                        score++;
                }
                else if (currentChar == '<')
                {
                    isGarbage = true;
                }
                else if (currentChar == '{')
                {
                    level++;
                }
                else if (currentChar == '}')
                {
                    level--;
                }
            }            
			return score;
        }

    }
}
