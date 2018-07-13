using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day24
    {

        static List<Component> GetInput()
        {
            List<Component> components = new List<Component>();
            var s = Properties.Resources.input_D24.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string l in s)
            {
                components.Add(new Component(int.Parse(l.Substring(0, l.IndexOf('/'))), int.Parse(l.Substring(l.IndexOf('/') + 1))));
            }
            return components;
        }

        public static long Part1()
        {
            List<Component> components = GetInput();
            List<Component> currBridge = new List<Component>();
            int maxValue = 0;
            int currConnection = 0;

            return GetStrongestBridge(currConnection, maxValue, components, currBridge);

        }

        static int GetStrongestBridge(int currConnection, int maxValue, List<Component> components, List<Component> currBridge)
        {
            for (int i = 0; i < components.Count; ++i)
            {
                if ((components[i].north.value == currConnection || components[i].south.value == currConnection) && !currBridge.Contains(components[i]))
                {
                    currBridge.Add(components[i]);
                    int val = 0;
                    foreach (Component c in currBridge)
                    {
                        val += c.value;
                    }
                    if (val > maxValue) maxValue = val;

                    int newConnection = 0;
                    if (components[i].north.value == currConnection)
                    {
                        newConnection = components[i].south.value;
                    }
                    else
                    {
                        newConnection = components[i].north.value;
                    }

                    // Repeat
                    int t = GetStrongestBridge(newConnection, maxValue, components, currBridge);
                    if (t > maxValue) maxValue = t;
                    currBridge.Remove(currBridge.Last());
                }
            }

            return maxValue;
        }


        public static int Part2()
        {
            List<Component> components = GetInput();
            List<Component> currBridge = new List<Component>();

            int currConnection = 0;
            Bridge longest = new Bridge(0,0);
            return GetLongestBridge(currConnection, longest, components, currBridge).value;
        }

        static Bridge GetLongestBridge(int currConnection, Bridge longest, List<Component> components, List<Component> currBridge)
        {
            for (int i = 0; i < components.Count; ++i)
            {
                if ((components[i].north.value == currConnection || components[i].south.value == currConnection) && !currBridge.Contains(components[i]))
                {
                    currBridge.Add(components[i]);
                    int val = 0;
                    foreach (Component c in currBridge)
                    {
                        val += c.value;
                    }
                    if (currBridge.Count > longest.length)
                    {
                        longest = new Bridge(currBridge.Count, val);
                    }
                    else if (currBridge.Count == longest.length && val > longest.value)
                    {
                        longest = new Bridge(currBridge.Count, val);
                    }




                    int newConnection = 0;
                    if (components[i].north.value == currConnection)
                    {
                        newConnection = components[i].south.value;
                    }
                    else
                    {
                        newConnection = components[i].north.value;
                    }

                    // Repeat
                    Bridge t = GetLongestBridge(newConnection, longest, components, currBridge);
                    if (t.length > longest.length) longest = t;
                    else if (t.length == longest.length && t.value > longest.value) longest = t;

                    currBridge.Remove(currBridge.Last());
                }
            }

            return longest;
        }
    }


    struct Bridge
    {
        public int value;
        public int length;

        public Bridge(int l, int v)
        {
            value = v;
            length = l;
        }

    }

    struct Magnet
    {
        public int value;
        public bool used;

        public Magnet(int i)
        {
            value = i;
            used = false;
        }
    }

    class Component
    {
        public int value;
        public Magnet north, south;

        public Component(int _north, int _south)
        {
            north = new Magnet(_north);
            south = new Magnet(_south);

            value = _north + _south;
        }

    }

}
