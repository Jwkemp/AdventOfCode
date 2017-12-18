using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day04
    {
        public static int Part1()
        {
            int numOfValidPasswords = 0;
            using (StreamReader sr = new StreamReader(Properties.Resources.input_D4))
            {
                while (!sr.EndOfStream)
                {
                    var words = sr.ReadLine().Split(' ');
                    if (words.Length == words.Distinct().Count())
                        numOfValidPasswords++;
                }
            }
            return numOfValidPasswords;
        }

        // Can I be more efficient? Less loops pls >_<
        public static int Part2()
        {
            int numOfValidPasswords = 0;
            bool isValid = true;
            using (StreamReader sr = new StreamReader(Properties.Resources.input_D4))
            {
                while (!sr.EndOfStream)
                {
                    isValid = true;
                    var words = sr.ReadLine().Split(' ');
                    for (int i = 0; i < words.Length; ++i)
                    {
                        for (int j = i + 1; j < words.Length; ++j)
                        {
                            if (words[i].Count() == words[j].Count())
                            {
                                int matches = 0;
                                foreach (char c in words[i])
                                {
                                    int charNumWordOne = words[i].Count(f => f == c);
                                    int charNumWordTwo = words[j].Count(f => f == c);
                                    if (charNumWordOne == charNumWordTwo)
                                    {
                                        matches++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (matches == words[i].Count()) isValid = false;
                            }
                        }
                    }
                    if (isValid) numOfValidPasswords++;
                }
            }
            return numOfValidPasswords;
        }

    }
}
