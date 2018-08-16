using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AoC2016
{
    class Day04 : IDay
    {
        private List<Tuple<string, string, int>> GetInput()
        {
            List<Tuple<string, string, int>> encryptedData = new List<Tuple<string, string, int>>();
            var lines = Properties.Resource.input_D04.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            foreach (string line in lines)
            {
                int indexOf = line.IndexOf('[');
                int indexOf2 = line.LastIndexOf('-');
                string checkSum = line.Substring(indexOf + 1, 5);                
                var roomNumber = int.Parse( line.Substring(indexOf2 + 1, indexOf - indexOf2 - 1));
                string roomName = line.Substring(0, indexOf2);              

                encryptedData.Add(new Tuple<string, string, int>(roomName , checkSum, roomNumber));
            }
            return encryptedData;
        }

        public object Part1()
        {
            int result = 0;

            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();
            List<char> highestChars = new List<char>();
            List<Tuple<string, string, int>> input = GetInput();
            
            foreach ( Tuple<string,string, int> encryptedString in input )
            {
                characterFrequency = GetCharacterFrequency(encryptedString.Item1);
                for (int i = 0; i < 5; ++i)
                {
                    highestChars.Add(GetMostFrequentChar(characterFrequency, highestChars));
                }

                string correctChecksum = "";
                for ( int i = 0; i < highestChars.Count; ++i )
                {
                    correctChecksum += highestChars[i];
                }                

                if (correctChecksum == encryptedString.Item2)
                {
                    result += encryptedString.Item3;
                }

                highestChars.Clear();
                characterFrequency.Clear();
            }


            return result;
        }

        public object Part2()
        {
            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();
            List<char> highestChars = new List<char>();
            List<Tuple<string, string, int>> input = GetInput();
            List<Tuple<string, string, int>> validRooms = new List<Tuple<string, string, int>>();
            List<Tuple<string, string, int>> decryptedRooms = new List<Tuple<string, string, int>>();

            foreach (Tuple<string, string, int> encryptedString in input)
            {
                characterFrequency = GetCharacterFrequency(encryptedString.Item1);
                for (int i = 0; i < 5; ++i)
                {
                    highestChars.Add(GetMostFrequentChar(characterFrequency, highestChars));
                }

                string correctChecksum = "";
                for (int i = 0; i < highestChars.Count; ++i)
                {
                    correctChecksum += highestChars[i];
                }

                if (correctChecksum == encryptedString.Item2)
                {
                    validRooms.Add(encryptedString);
                }

                highestChars.Clear();
                characterFrequency.Clear();
            }

            for ( int i = 0; i < validRooms.Count; ++i ) 
            {
                decryptedRooms.Add( new Tuple<string, string, int>(Decrypt(validRooms[i].Item1, validRooms[i].Item3), validRooms[i].Item2, validRooms[i].Item3));
            }

            return decryptedRooms.Where(s => s.Item1.Contains("northpole object storage")).First().Item3;
        }

        private string Decrypt( string s, int val )
        {
            StringBuilder sb = new StringBuilder(s);

            for ( int i = 0; i < s.Count(); ++i )
            {
                if ( s[i] == '-' )
                {
                    sb[i] = ' ';
                }
                else
                {
                    int c = s[i];
                    c -= 96;

                    c += ( val % 26);
                    if (c > 26) c -= 26;
                    
                    c += 96;
                    sb[i] = (char)c;
                }
            }

            return sb.ToString();
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

        private char GetMostFrequentChar ( Dictionary<char,int> dic, List<char> excludes )
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
    }
}
