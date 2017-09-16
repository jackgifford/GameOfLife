using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Life();

            int generation = 0;

            while (generation < 100)
            {
                game.CalculateNextGeneration();
                generation++;
                PrintGeneration(game.Current);
                Console.WriteLine("Generation: {0}", generation);
                Thread.Sleep(1000);
                Console.Clear();

            }
        }

        static void PrintGeneration(State[,] state)
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    char toWrite = state[i, j] == State.Alive ? '#' : ' ';
                    Console.Write(toWrite);
                }

                Console.WriteLine();
            }
        }
    }


    enum State
    {
        Alive,
        Dead
    }

    class Life
    {
        private const int length = 36;
        private const int width = 72;

        public State[,] Current { get; set; }
        private State[,] next;

        public Life()
        {
            Current = Initialise();
            next = Initialise();
        }

        private State[,] Initialise()
        {
            var arr = new State[length, width];
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    arr[i, j] = State.Dead;

            return arr;
        }

        public void CalculateNextGeneration()
        {
            for (int i = 1; i < length - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    var neighboursCount = GetNeighboursAliveCount(i, j);
                    if (Current[i, j] == State.Alive)
                        next[i, j] = HandleAliveCell(neighboursCount);
                    else
                        next[i, j] = HandleDeadCell(neighboursCount);
                }
            }

            NextGeneartion();
        }

        private void NextGeneartion() => Current = next.Clone() as State[,];
        private State HandleDeadCell(int aliveCount) => aliveCount == 3 ? State.Alive : State.Dead;

        private State HandleAliveCell(int aliveCount) => aliveCount == 2 || aliveCount == 3 ? State.Alive : State.Dead;

        private int GetNeighboursAliveCount(int x, int y)
        {
            int count = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i != x && j != y)
                    {
                        if (Current[i, j] == State.Alive)
                            count++;
                    }
                }
            }

            return count;
        }

    }
}