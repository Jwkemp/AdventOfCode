using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AoC2016
{
    class Day05 : IDay
    {
        public object Part1()
        {
            string input = "ugkcyxxp";
            StringBuilder password = new StringBuilder("");
            int index = 0;
            while (password.Length < 8)
            {
                MD5 md5 = MD5.Create();
                var hash = GetMD5Hash(input + index);

                if ( hash.StartsWith("00000") )
                {                    
                    password.Append(hash[5]);                                      
                }             
                index++;
            }

            return password;
        }

        public object Part2()
        {
            string input = "ugkcyxxp";
            StringBuilder password = new StringBuilder("________");
            int index = 0;
            while (password.ToString().Contains("_"))
            {
                MD5 md5 = MD5.Create();
                var hash = GetMD5Hash(input + index);

                if (hash.StartsWith("00000"))
                {
                    int position = 0;
                    if (int.TryParse(hash[5].ToString(), out position) && position < 8 && password[position] == '_')
                    {                        
                        password[position] = hash[6];                        
                        //Console.WriteLine(index);
                        //Console.WriteLine("hash: " + hash);
                        //Console.WriteLine("Password: " + password);
                        //Console.WriteLine("=========================================================");
                    }
                }
                index++;
            }

            return password;
        }

        private string GetMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
