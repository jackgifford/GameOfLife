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

            PrintGeneration(game.Current);
            

            while (generation < 100)
            {
                game.CalculateNextGeneration();
                generation++;
                PrintGeneration(game.Current);
                Console.WriteLine("Generation: {0}", generation);
                
                Thread.Sleep(600);
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
            Seed();
        }

        //Create a seed function to get everything kicked off
        private void Seed()
        {
            int mid_x = width / 2;
            int mid_y = length / 2;

            Current[mid_y, mid_x] = State.Alive;
            Current[mid_y - 2, mid_x] = State.Alive;
            Current[mid_y + 2, mid_x] = State.Alive;
            Current[mid_y, mid_x - 2] = State.Alive;
            Current[mid_y, mid_x + 2] = State.Alive;
            Current[mid_y - 1, mid_x - 1] = State.Alive;
            Current[mid_y - 1, mid_x + 1] = State.Alive;
            Current[mid_y + 1, mid_x - 1] = State.Alive;
            Current[mid_y + 1, mid_x + 1] = State.Alive;
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
        private State HandleDeadCell(int aliveCount)
        {
            if (aliveCount == 3)
                return State.Alive;
            else
                return State.Dead;
        }

        private State HandleAliveCell(int aliveCount)
        {
            if (aliveCount == 2 || aliveCount == 3)
                return State.Alive;
            else
                return State.Dead;
        }

        private int GetNeighboursAliveCount(int x, int y)
        {
            int count = 0;

            var array = new State[]
            {
                    Current[x-1, y-1],
                    Current[x-1, y],
                    Current[x-1, y+1],

                    Current[x, y-1],
                    Current[x, y+1],
                    Current[x+1, y-1],
                    Current[x+1, y],
                    Current[x+1, y+1]
            };

            foreach (var neighbour in array)
            {
                if (neighbour == State.Alive)
                    count++;
            }


            return count;
        }
    }
}