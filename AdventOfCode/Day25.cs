using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day25
    {
        public static int Part1()
        {
            TuringMachine tMach = new TuringMachine();
            
            for (int i = 0; i < 12425180; ++i)
            {
                tMach.Step();
            }

            return tMach.CheckSum();
        }


        public static int Part2()
        {
            return 1;
        }

    }


    class TuringMachine
    {
        enum State { A, B, C, D, E, F };
        State currState;
        Dictionary<int, int> tape;
        int cursorPos;


        public TuringMachine()
        {
            tape = new Dictionary<int, int>();
            currState = State.A;
            cursorPos = 0;
        }

        public void Step()
        {
            switch ( currState )
            {
                case State.A:
                    if (GetTapeValue(cursorPos) == 0)
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos++;
                        currState = State.B;
                    }
                    else
                    {
                        SetTapeValue(cursorPos, 0);
                        cursorPos++;
                        currState = State.F;
                    }
                    break;
                case State.B:
                    if (GetTapeValue(cursorPos) == 0)
                    {
                        SetTapeValue(cursorPos, 0);
                        cursorPos--;
                        currState = State.B;
                    }
                    else
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos--;
                        currState = State.C;
                    }
                    break;
                case State.C:
                    if (GetTapeValue(cursorPos) == 0)
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos--;
                        currState = State.D;
                    }
                    else
                    {
                        SetTapeValue(cursorPos, 0);
                        cursorPos++;
                        currState = State.C;
                    }
                    break;
                case State.D:
                    if (GetTapeValue(cursorPos) == 0)
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos--;
                        currState = State.E;
                    }
                    else
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos++;
                        currState = State.A;
                    }
                    break;
                case State.E:
                    if (GetTapeValue(cursorPos) == 0)
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos--;
                        currState = State.F;
                    }
                    else
                    {
                        SetTapeValue(cursorPos, 0);
                        cursorPos--;
                        currState = State.D;
                    }
                    break;
                case State.F:
                    if (GetTapeValue(cursorPos) == 0)
                    {
                        SetTapeValue(cursorPos, 1);
                        cursorPos++;
                        currState = State.A;
                    }
                    else
                    {
                        SetTapeValue(cursorPos, 0);
                        cursorPos--;
                        currState = State.E;
                    }
                    break;
                default:
                    throw new Exception("What state!?");
            }
        }

        public int CheckSum()
        {
           return  tape.Values.Count(d => d == 1);
        }

        private int GetTapeValue(int index)
        {
            int value = 0;
            if (tape.TryGetValue(index, out value))
            {
                return value;
            }
            else
            {
                tape.Add(index, 0);
                return 0;
            }
        }

        private void SetTapeValue(int index, int value)
        {
            if (tape.Keys.Contains(index))
            {
                tape[index] = value;
            }
            else
            {
                tape.Add(index, value);
            }
        }
    }
}