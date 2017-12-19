using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day10
    {

        public static int Part1()
        {
            List<int> input = new List<int>() { 97, 167, 54, 178, 2, 11, 209, 174, 119, 248, 254, 0, 255, 1, 64, 190 };
            List<int> list = new List<int>(); 

            for (int i = 0; i < 256; i++ )
            {
                list.Add(i);
            }

            int currIndx = 0;
            int skipSize = 0;

            foreach ( int i in input )
            {
                var temp = ReverseRange(list, currIndx, i);
                list = ReplaceRange(list, temp, currIndx, i);

                currIndx += i + skipSize;
                skipSize++;
                if (currIndx > list.Count) currIndx -= list.Count;

            }
            return list[0] * list[1];
        }

        public static string Part2()
        {
            string input = "97,167,54,178,2,11,209,174,119,248,254,0,255,1,64,190";
            List<int> inputList = new List<int>();
            foreach (char c in input)
            {
                inputList.Add(c);
            }
            inputList.AddRange(new int[] { 17, 31, 73, 47, 23 });

            List<int> sparseHash = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                sparseHash.Add(i);
            }

            int currIndx = 0;
            int skipSize = 0;
            for (int j = 0; j < 64; ++j)
            {
                foreach (int i in inputList)
                {
                    var temp = ReverseRange(sparseHash, currIndx, i);
                    sparseHash = ReplaceRange(sparseHash, temp, currIndx, i);

                    currIndx += i + skipSize;
                    skipSize++;
                    while (currIndx > sparseHash.Count) currIndx -= sparseHash.Count;
                }
            }

            List<int> denseHash = new List<int>();
            for (int i = 0; i < 16; ++i)
            {
                int j = i * 16;
                denseHash.Add(sparseHash[j] ^ sparseHash[j + 1] ^ sparseHash[j + 2] ^ sparseHash[j + 3] ^ sparseHash[j + 4] ^ sparseHash[j + 5] ^ sparseHash[j + 6] ^ sparseHash[j + 7] ^ sparseHash[j + 8] ^ sparseHash[j + 9] ^ sparseHash[j + 10] ^ sparseHash[j + 11] ^ sparseHash[j + 12] ^ sparseHash[j + 13] ^ sparseHash[j + 14] ^ sparseHash[j + 15]);
            }

            string output = "";
            foreach ( int i in denseHash)
                output += i.ToString("X2");

            return output;
        }


        private static List<int> ReverseRange(List<int> ogList, int from, int length )
        {
            List<int> output = new List<int>();
            for (int i = 0; i < length; ++i)
            {
                if ( from + i > ogList.Count-1 )
                {
                    output.Add(ogList[from + i - (ogList.Count)]);
                }
                else
                    output.Add(ogList[from + i ]);
            }
            output.Reverse();
            return output;
        }

        private static List<int> ReplaceRange(List<int> ogList, List<int> newList, int from, int length)
        {
            List<int> output = ogList;
            for (int i = 0; i < length; ++i)
            {
                if (from + i > ogList.Count - 1)
                {
                    output[from + i - (ogList.Count)] = newList[i];
                }
                else
                    output[from + i] = newList[i];
            }
            return output;
        }
    }
}
