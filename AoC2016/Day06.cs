using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2016
{
    class Day06 : IDay
    {

        private List<string> GetInput()
        {
            return Properties.Resource.input_D06.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public object Part1()
        {
            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();
            StringBuilder output = new StringBuilder();
            StringBuilder noise = new StringBuilder();
            List<string> input = GetInput();
            int resultLength = 0;

            foreach ( string s in input )
            {
                if ( s.Length > resultLength )
                {
                    resultLength = s.Length;
                }
            }


            for ( int i = 0; i < resultLength; ++i )
            {
                foreach (string s in input)
                {
                    noise.Append(s[i]);
                }

                characterFrequency = GetCharacterFrequency(noise.ToString());
                output.Append(GetMostFrequentChar(characterFrequency, new List<char>()));

                characterFrequency.Clear();
                noise.Clear();
            }

            return output;
        }

        public object Part2()
        {
            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();
            StringBuilder output = new StringBuilder();
            StringBuilder noise = new StringBuilder();
            List<string> input = GetInput();
            int resultLength = 0;

            foreach (string s in input)
            {
                if (s.Length > resultLength)
                {
                    resultLength = s.Length;
                }
            }


            for (int i = 0; i < resultLength; ++i)
            {
                foreach (string s in input)
                {
                    noise.Append(s[i]);
                }

                characterFrequency = GetCharacterFrequency(noise.ToString());
                output.Append(GetLeastFrequentChar(characterFrequency, new List<char>()));

                characterFrequency.Clear();
                noise.Clear();
            }

            return output;
        }
        
        private Dictionary<char, int> GetCharacterFrequency(string words)
        {
            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();

            foreach (char c in words)
            {
                if (!characterFrequency.ContainsKey(c) && c != '-')
                {
                    int freq = words.Count(t => t == c);
                    characterFrequency.Add(c, freq);
                }
            }

            return characterFrequency;
        }

        private char GetMostFrequentChar(Dictionary<char, int> dic, List<char> excludes)
        {
            KeyValuePair<char, int> highestValue = new KeyValuePair<char, int>();
            foreach (KeyValuePair<char, int> kv in dic)
            {
                if (!excludes.Contains(kv.Key))
                {
                    if (kv.Value > highestValue.Value || (kv.Value == highestValue.Value && kv.Key < highestValue.Key))
                    {
                        highestValue = kv;
                    }
                }
            }
            return highestValue.Key;
        }

        private char GetLeastFrequentChar(Dictionary<char, int> dic, List<char> excludes)
        {
            KeyValuePair<char, int> LowestValue = new KeyValuePair<char, int>('_',int.MaxValue);
            foreach (KeyValuePair<char, int> kv in dic)
            {
                if (!excludes.Contains(kv.Key))
                {
                    if (kv.Value < LowestValue.Value || (kv.Value == LowestValue.Value && kv.Key > LowestValue.Key))
                    {
                        LowestValue = kv;
                    }
                }
            }
            return LowestValue.Key;
        }
    }
}
