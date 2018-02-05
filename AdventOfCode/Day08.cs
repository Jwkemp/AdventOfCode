using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day08
    {
        public static int Part1()
        {
            List<Operation> operations = new List<Operation>();
            Dictionary<string, int> register = new Dictionary<string, int>();
            int output = int.MinValue;

			string input = Properties.Resources.input_D08;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			foreach (string s in inputarray)
			{
				Operation o = new Operation(s);
				if (!register.ContainsKey(o.reg))
					register.Add(o.reg, 0);

				if (!register.ContainsKey(o.left))
					register.Add(o.left, 0);

				if (o.CheckCondition(register))
				{
					register[o.reg] += o.DoOperation();
				}
			}		

            foreach ( KeyValuePair<string,int> item in register )
            {
                if (item.Value > output) output = item.Value;
            }

            return output;
        }

        public static int Part2()
        {
            List<Operation> operations = new List<Operation>();
            Dictionary<string, int> register = new Dictionary<string, int>();
            int output = int.MinValue;

			string input = Properties.Resources.input_D08;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			foreach (string s in inputarray)
			{				
                Operation o = new Operation(s);
                if (!register.ContainsKey(o.reg))
                    register.Add(o.reg, 0);

                if (!register.ContainsKey(o.left))
                    register.Add(o.left, 0);

                if (o.CheckCondition(register))
                {
                    register[o.reg] += o.DoOperation();
                    if (register[o.reg] > output) output = register[o.reg];
                }
            }
            return output;
        }

        private class Operation
        {
            public string reg, cmd, left, op;
            public int value, right;

            public Operation(string str)
            {
                var strs = str.Split(' ');
                reg = strs[0];
                cmd = strs[1];
                value = int.Parse(strs[2]);
                left = strs[4];
                op = strs[5];
                right = int.Parse(strs[6]);
            }

            public bool CheckCondition(Dictionary<string, int> register)
            {
                switch ( op)
                {
                    case ">":
                        if (register[left] > right) return true;
                        else return false;

                    case "<":
                        if (register[left] < right) return true;
                        else return false;

                    case "<=":
                        if (register[left] <= right) return true;
                        else return false;


                    case ">=":
                        if (register[left] >= right) return true;
                        else return false;


                    case "!=":
                        if (register[left] != right) return true;
                        else return false;

                    case "==":
                        if (register[left] == right) return true;
                        else return false;
                }

                throw new Exception("Operator not recognised");
            }

            public int DoOperation()
            {
                if ( cmd == "inc" )
                {
                    return value;
                }
                else
                {
                    return value * -1;
                }
            }
        }
    }
}
