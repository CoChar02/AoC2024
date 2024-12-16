using System.Text.RegularExpressions;

namespace Day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inp = File.ReadAllText("Inputs.txt");
            //copy grid to char array
            string[] gridLines = inp.Split("\r\n\r\n")[0].Split('\n');
            char[,] grid = new char[gridLines[0].Trim().Length, gridLines.Length];
            for (int y = 0; y < gridLines.Length; y++)
            {
                string row = gridLines[y].Trim();
                for (int x = 0; x < gridLines[0].Trim().Length; x++)
                {
                    grid[x, y] = row[x];
                }
            }
            //copy inputs into list
            string moveString = inp.Split("\r\n\r\n")[1].Trim();
            List<(int x, int y)> moves = new List<(int x, int y)>();
            foreach (char Charac in moveString)
            {
                switch (Charac)
                {
                    case '<':
                        moves.Add((-1, 0));
                        break;
                    case '^':
                        moves.Add((0, -1));
                        break;
                    case '>':
                        moves.Add((1, 0));
                        break;
                    case 'v':
                        moves.Add((0, 1));
                        break;
                }
            }
            Console.WriteLine(Part1(moves, grid));
        }

        static long Part1(List<(int x, int y)> moves, char[,] grid)
        {
            //find robot
            (int x, int y) robot = (0, 0);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[j, i] == '@')
                    {
                        robot = (j, i);
                    }
                }
            }
            //do each move
            foreach ((int x, int y) move in moves)
            {
                if (RecursiveMove(robot.x, robot.y, move.x, move.y, grid))
                {
                    robot.x += move.x;
                    robot.y += move.y;
                }
            }
            //find total
            long total = 0;
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[col, row] == 'O')
                    {
                        total += col + row * 100;
                    }
                }
            }
            return total;
        }

        static bool RecursiveMove(int X, int Y, int changeX, int changeY, char[,] grid)
        {
            if (grid[X + changeX, Y + changeY] == '#') //If next tile is wall then we cannot move object forward
            {
                return false;
            }
            if (grid[X + changeX, Y + changeY] == '.' || RecursiveMove(X + changeX, Y + changeY, changeX, changeY, grid))//if empty or recursively call on next object (so we can push stacks of crates)
            {
             grid[X + changeX, Y + changeY] = grid[X, Y];
             grid[X, Y] = '.';
             return true;
            }
            return false;
        }
   
    }
}
