using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day07
    {
        public static string Part1()
        {
            List<Node> towerNodes = new List<Node>();
            string input = Properties.Resources.input_D7;
            string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in inputarray)
            {
                towerNodes.Add(new Node(s));
            }

            foreach (Node n in towerNodes)
            {
                foreach (string childName in n.childNames)
                {
                    int index = towerNodes.FindIndex(x => x.name == childName);
                    n.children.Add(towerNodes[index]);
                    towerNodes[index].parent = n;
                }
                n.totalWeight = GetTotalWeight(n, towerNodes);
            }

            foreach ( Node n in towerNodes )
            {
                if ( n.parent == null ) return n.name;
            }
            return "Not Found!";
        }

        public static int Part2()
        {
			List<Node> towerNodes = new List<Node>();
			string input = Properties.Resources.input_D7;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach (string s in inputarray)
			{
				towerNodes.Add(new Node(s));				
			}

            foreach(Node n in towerNodes)
            {               
                foreach (string childName in n.childNames)
                {
                    int index = towerNodes.FindIndex(x => x.name == childName);
                    n.children.Add(towerNodes[index]);
                    towerNodes[index].parent = n;                    
                }
                n.totalWeight = GetTotalWeight(n, towerNodes);
            }

            Node node = towerNodes.Find( x => x.name == "vmpywg");
            int targetWeight = 0;

            while (!node.IsBalanced())
            {
                var weights = node.children.GroupBy(x => x.totalWeight).OrderBy(x => x.Count());
                targetWeight = weights.Last().Key;
                node = weights.First().First();
            }

            var weightDiff = targetWeight - node.totalWeight;
            return (node.weight + weightDiff);

        }

        private static int FixBalance(string baseNode, List<Node> towerBits)
        {
            Node baseBit = towerBits.Find(x => x.name == baseNode);            
            // Get children
            List<int> childIndex = new List<int>();
            List<int> childWeights = new List<int>();
            foreach (string name in baseBit.childNames)
            {
                childIndex.Add(towerBits.FindIndex(x => x.name == name));
                childWeights.Add(GetTotalWeight(towerBits[childIndex.Last()], towerBits));
            }

            // Get child with odd weight
            Node nextBit = null; 
            foreach (int i in childIndex)
            {
                if (childWeights.Where(f => f == GetTotalWeight(towerBits[i], towerBits)).Count() == 1 )
                {
                    // has odd weight
                    nextBit = towerBits[i];
                    break;
                }                
            }       

            if (nextBit != null && nextBit.childNames.Count() > 0 )
            {
                var r = FixBalance(nextBit.name, towerBits);
                if ( r == 0)
                {
                    return 5;
                }
                return r;
            }
            else
            {
                return 0;
            }            
        }

        private static int GetTotalWeight(Node tb, List<Node> towerBits)
        {
            int weight = tb.weight;
            foreach (string name in tb.childNames)
            {
                // Get to towerBit
                Node str = towerBits.Find(x => x.name == name);
                weight += GetTotalWeight(str, towerBits);
            }
            return weight;
        }

        private class Node
        {
            public string name { get; private set; }
            public int weight { get; private set; }
            public int totalWeight { get; set; }
            public List<string> childNames;
            public List<Node> children { get; set; }
            public Node parent { get; set; }

            public Node(string input)
            {
                name = input.Substring(0, input.IndexOf(' '));                
                
                int from = input.IndexOf('(') + 1;
                weight = int.Parse(input.Substring(from, input.IndexOf(')') - from));

                childNames = new List<string>();
                if (input.Contains("->"))
                {
                    childNames.AddRange((input.Substring(input.IndexOf("->") + 2).Split(',')));
                    for (int i = 0; i < childNames.Count; ++i) childNames[i] = childNames[i].Trim();
                }

                children = new List<Node>();
                parent = null;
            }

            public bool IsBalanced()
            {
                return children.GroupBy(x => x.totalWeight).Count() == 1;                 
            }                    
        }
    }
}
