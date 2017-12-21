﻿using System;
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
            List<TowerBit> towerBits = new List<TowerBit>();
			string input = Properties.Resources.input_D13;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach (string s in inputarray)
			{
				towerBits.Add(new TowerBit(s.Substring(0, s.IndexOf(' '))));
				if (s.Contains("->"))
				{
					towerBits.Last().SetHolding(s.Substring(s.IndexOf("->") + 2).Split(','));
				}
			}

            string currBit = towerBits[0].name;
            bool beingHeld = true;
            while (beingHeld)
            {
                beingHeld = false;
                foreach (TowerBit t in towerBits)
                {
                    if (t.holding.Contains(currBit))
                    {
                        currBit = t.name;
                        beingHeld = true;
                        break;
                    }

                }
            }
            return currBit;
        }

        public static int Part2()
        {
			List<TowerBit> towerBits = new List<TowerBit>();
			string input = Properties.Resources.input_D13;
			string[] inputarray = input.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			foreach (string s in inputarray)
			{
				towerBits.Add(new TowerBit(s.Substring(0, s.IndexOf(' '))));
				int from = s.IndexOf('(') + 1;
				towerBits.Last().SetWeight(s.Substring(from, s.IndexOf(')') - from));
				if (s.Contains("->"))
				{
					towerBits.Last().SetHolding(s.Substring(s.IndexOf("->") + 2).Split(','));
				}
			}

            TowerBit baseBit = towerBits.Find(x => x.name == "vmpywg");
            List<int> weights = new List<int>();
            foreach (string name in baseBit.holding)
            {
                TowerBit tb = towerBits.Find(x => x.name == name);
                weights.Add(GetTotalWeight(towerBits.Find(x => x.name == name), towerBits));
            }

            // WRONG - find the node that needs to be adjusted, not at the base.

            List<int> weightsDistinct = weights.Distinct().ToList();
            if (weights.FindAll(x => x == weightsDistinct[0]).Count > 1)
                return weightsDistinct[0];
            else
                return weightsDistinct[1];

        }

        private static int GetTotalWeight(TowerBit tb, List<TowerBit> towerBits)
        {
            int weight = tb.weight;
            foreach (string name in tb.holding)
            {
                // Get to towerBit
                TowerBit str = towerBits.Find(x => x.name == name);
                weight += GetTotalWeight(str, towerBits);
            }
            return weight;
        }

        private class TowerBit
        {
            public string name;
            public List<string> holding;
            public int weight;

            public TowerBit(string _name = "")
            {
                name = _name;
                holding = new List<string>();
                weight = 0;
            }

            public void SetHolding(string[] _holding)
            {
                foreach (string s in _holding)
                {
                    holding.Add(Regex.Replace(s, @"\s+", ""));
                }
            }

            public void SetWeight(string s)
            {
                weight = int.Parse(s);
            }

        }

    }
}
