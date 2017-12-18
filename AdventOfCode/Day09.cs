﻿using System;
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
            using ( StreamReader sr = new StreamReader(Properties.Resources.input_D9))
            {
                Char currentChar = ' ';
                bool isGarbage = false;
                bool isIgnored = false;
                int level = 0;

                while ( !sr.EndOfStream )
                {
                    currentChar = (char)sr.Read();

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
            }
            return score;
        }
        
        public static int Part2()
        {
            int score = 0;
            using (StreamReader sr = new StreamReader(Properties.Resources.input_D9))
            {
                Char currentChar = ' ';
                bool isGarbage = false;
                bool isIgnored = false;
                int level = 0;

                while (!sr.EndOfStream)
                {
                    currentChar = (char)sr.Read();

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
            }
            return score;
        }

    }
}
