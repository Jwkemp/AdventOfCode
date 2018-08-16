
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;

namespace AoC2016
{
    public class Day01 : IDay
    {
        struct Directions
        {
            public char turn;
            public int steps;
            public Directions(char turn, int steps)
            {
                this.turn = turn;
                this.steps = steps;
            }
        }

        enum Direction { north, east, south, west};
        
        List<Directions> GetInput()
        {
            List<Directions> directions = new List<Directions>();
            string[] inputStrings = Properties.Resource.input_D01.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in inputStrings)
            {
                directions.Add(new Directions(s[0], int.Parse(s.Substring(1))));
            }

            return directions;
        }

        public object Part1()
        {
            Direction facing = Direction.north;
            List<Directions> directions = GetInput();
            Vector position = new Vector(0, 0);

            foreach ( Directions d in directions )
            {
                facing = Turn( d.turn, facing);
                position = Step(d.steps, facing, position);
            }
            return Math.Abs(position.X + position.Y);
        }

        public object Part2()
        {
            Direction facing = Direction.north;
            List<Directions> directions = GetInput();
            List<Vector> visitedLocations = new List<Vector>();
            visitedLocations.Add(new Vector(0, 0));

            foreach (Directions d in directions)
            {
                facing = Turn(d.turn, facing);

                for ( int i =0; i < d.steps; i++ )
                {
                    visitedLocations.Add(Step(1, facing, visitedLocations.Last()));
                    if (visitedLocations.Distinct().Count() != visitedLocations.Count())
                    {
                        return Math.Abs(visitedLocations.Last().X + visitedLocations.Last().Y);
                    }
                }               
            }

            throw new Exception("No location visited twice");
        }

        Direction Turn(char turnDir, Direction dir)
        {
            if ( turnDir == 'R' && dir == Direction.west)
            {
                return Direction.north;
            }
            else if (turnDir == 'L' && dir == Direction.north)
            {
                return Direction.west;
            }
            else if (turnDir == 'L')
            {
                return dir - 1;
            }
            else if (turnDir == 'R')
            {
                return dir + 1;
            }
            else
            {
                throw new Exception("Unkown direction");
            } 
        }

        Vector Step(int steps, Direction currDir, Vector currPos)
        {
            switch (currDir)
            {
                case Direction.north:
                    return new Vector(currPos.X, currPos.Y + steps);

                case Direction.east:
                    return new Vector(currPos.X + steps, currPos.Y);

                case Direction.south:
                    return new Vector(currPos.X, currPos.Y - steps);

                case Direction.west:
                    return new Vector(currPos.X - steps, currPos.Y);

                default:
                    throw new Exception("Day01 - Step Unknown Direction.");
            }

        }
    }
}
