using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2016
{
    class Day07 : IDay
    {
        private string[] GetInput()
        {            
            var lines = Properties.Resource.input_D07.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return lines;
        }

        public object Part1()
        {
            var ips = GetInput();

            bool inHypernet;
            bool supportsTLS;
            int result = 0;


            foreach (string ip in ips)
            {
                inHypernet = false;
                supportsTLS = false;
                for (int i = 0; i < ip.Length - 3; ++i)
                {
                    if ( ip[i] == '[' )
                    {
                        inHypernet = true;
                    }
                    else if ( ip[i] == ']' )
                    {
                        inHypernet = false;
                    }
                    else
                    {
                        if (inHypernet)
                        {
                            if (IsABBA(ip.Substring(i, 2), ip.Substring(i + 2, 2)))
                            {
                                supportsTLS = false;
                                i = ip.Length + 1;
                            }
                        }
                        else
                        {
                            if (IsABBA(ip.Substring(i, 2), ip.Substring(i + 2, 2)))
                            {
                                supportsTLS = true;
                            }
                        }
                    }                    
                }
                if (supportsTLS) result++;
            }
            return result;      
        }

        public object Part2()
        {
            var ips = GetInput();

            bool inHypernet;
            int result = 0;

            List<string> BABs = new List<string>();

            foreach (string ip in ips)
            {
                inHypernet = false;
                BABs.Clear();
                for (int i = 0; i < ip.Length - 2; ++i)
                {
                    if (ip[i] == '[')
                    {
                        inHypernet = true;
                    }
                    else if (ip[i] == ']')
                    {
                        inHypernet = false;
                    }
                    else
                    {
                        if (inHypernet != true)
                        {                        
                            if (IsABA(ip.Substring(i,3)))
                            {
                                BABs.Add(GetBABFromABA(ip.Substring(i, 3)));
                            }
                        }
                    }
                }

                for (int i = 0; i < ip.Length - 2; ++i)
                {
                    if (ip[i] == '[')
                    {
                        inHypernet = true;
                    }
                    else if (ip[i] == ']')
                    {
                        inHypernet = false;
                    }
                    else
                    {
                        if (inHypernet == true)
                        {
                            if (IsABA(ip.Substring(i, 3)))
                            {
                                if (BABs.Contains(ip.Substring(i, 3)))
                                {
                                    i = ip.Length + 1;
                                    result++;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private bool SupportsTLS(Tuple<string,string> ip)
        {

            // Check hypernet
            for ( int i = 0; i < ip.Item2.Count() - 3; ++i )
            {
                if (ip.Item2[i] != ip.Item2[i + 1])
                {
                    if (IsABBA(ip.Item2.Substring(i, 2), ip.Item2.Substring(i + 2, 2)))
                    {
                        return false;
                    }
                }
            }

            // Check ip
            for (int i = 0; i < ip.Item1.Count() - 3; ++i)
            {
                if ( ip.Item1[i] != ip.Item1[i + 1] )
                {
                    if (IsABBA(ip.Item1.Substring(i, 2), ip.Item1.Substring(i + 2, 2)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsABBA( string pt1, string pt2 )
        {
            // characters must be different
            if (pt1[0] == pt1[1]) return false;
            if (pt2[0] == pt2[1]) return false;

            // reverse pt2
            var pt2Char = pt2.ToCharArray();
            Array.Reverse(pt2Char);
            pt2 = new string(pt2Char);

            return pt1 == pt2;
        }

        private bool IsABA( string s )
        {
            if (s[0] != s[1] && s[0] == s[2]) return true;
            return false;
        }

        private string GetBABFromABA( string s )
        {
            StringBuilder BAB = new StringBuilder();
            BAB.Append(s[1]);
            BAB.Append(s[0]);
            BAB.Append(s[1]);

            return BAB.ToString();
        }


    }
}
