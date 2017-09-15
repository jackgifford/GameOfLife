using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    enum State
    {
        Alive, 
        Dead
    }

    class Life
    {
        private const int length = 72;
        private const int width = 36;
        private readonly int generations;

        private State[,] current;
        private State[,] next;

        public Life()
        {
            current = new State[length, width];
            next = new State[length, width];
        }

        private void NextGeneartion() => current = next.Clone() as State[,];
            
        private void CalculateNextGeneration()
        {
            for (int i = 1; i < length - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    var neighboursCount = GetNeighboursAliveCount(i, j);
                    if (current[i, j] == State.Alive)
                        HandleAliveCell(neighboursCount);
                    else
                        HandleDeadCell(neighboursCount);
                }
            }
        }

        private State HandleDeadCell(int aliveCount) => aliveCount == 3 ? State.Alive : State.Dead;

        private State HandleAliveCell(int aliveCount) => aliveCount == 2 || aliveCount == 3 ? State.Alive : State.Dead;

        private int GetNeighboursAliveCount(int x, int y)
        {
            int count = 0;

            for (int i = x -1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i != x && j != y)
                    {
                        if (current[i,j] == State.Alive)
                            count++;
                    }
                }
            }

            return count;
        }

    }
}