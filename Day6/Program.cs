


using System.Data;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] inps = File.ReadAllLines("Inputs.txt");
        char[,] spaces = new char[inps.Length, inps[0].Length];

        for (int i = 0; i < spaces.GetLength(0); i++)
        {
            for (int j = 0; j < spaces.GetLength(1); j++)
            {
                spaces[i, j] = inps[i][j];
            }
        }

        char[] guards = { '^', '>', 'v', '<' };
        (int i, int j) guardPos = (0, 0);
        char guardStartChar = '\0';
        //Find Guard
        for (int i = 0; i < spaces.GetLength(0); i++)
        {
            for (int j = 0; j < spaces.GetLength(1); j++)
            {
                if (guards.Any(x => spaces[i, j] == x))
                {
                    (guardPos.i, guardPos.j) = (i, j);
                    guardStartChar = spaces[i, j];
                }
            }
        }
        Console.WriteLine(Part1(spaces, guards, guardPos));
        spaces[guardPos.i, guardPos.j] = guardStartChar;
        Console.WriteLine(Part2(spaces, guardPos, guards));
        Console.WriteLine();
    }

    private static int Part1(char[,] spaces, char[] guards, (int, int) startPos)
    {
        /*
         * Guard directions
         *  ^(-1,0)
         *  >(0,1)
         *  v(1,0)
         *  <(0,-1)
         */

        UpdateGuard(ref spaces, guards, startPos);
        return (from char space in spaces where space == 'X' select space).Count();
    }

    private static bool UpdateGuard(ref char[,] spaces, char[] guards, (int i, int j) guardPos)
    {
        bool isLoop = false;
        HashSet<((int, int), char)> visited = new HashSet<((int, int), char)>();
        while (guardPos.i < spaces.GetLength(0) && guardPos.i >= 0 && guardPos.j < spaces.GetLength(0) && guardPos.j >= 0 && !isLoop) //Til we go out of bounds
        {
            switch(spaces[guardPos.i, guardPos.j])
            {
                case '^':
                    guardPos = GuardMove(ref spaces, guardPos, guards, -1, 0, 0, ref visited, ref isLoop);
                    break;
                case '>':
                    guardPos = GuardMove(ref spaces, guardPos, guards, 0, 1, 1, ref visited, ref isLoop);
                    break;
                case 'v':
                    guardPos = GuardMove(ref spaces, guardPos, guards, 1, 0, 2, ref visited, ref isLoop);
                    break;
                case '<':
                    guardPos = GuardMove(ref spaces, guardPos, guards, 0, -1, 3, ref visited, ref isLoop);
                    break;
            }
        }
        return isLoop;
    }

    private static (int, int) GuardMove(ref char[,] spaces, (int i, int j) guardPos, char[] guards, int iMove, int jMove, int guardIndex, ref HashSet<((int, int), char)> visited, ref bool isLoop)
    {
        if (!visited.Add(((guardPos.i, guardPos.j), guards[guardIndex])))
        {
            isLoop = true;
        }
        if (guardPos.i + iMove < spaces.GetLength(0) && guardPos.i + iMove >= 0 && guardPos.j + jMove < spaces.GetLength(0) && guardPos.j + jMove >= 0)
        {
            if (spaces[guardPos.i + iMove, guardPos.j + jMove] == '#') //Hit obstacle
            {
                //Rotate 90 degrees
                spaces[guardPos.i, guardPos.j] = guards[(guardIndex += 1) % 4];
            }
            else
            {
                //Didn't hit an obstacle
                spaces[guardPos.i, guardPos.j] = 'X';
                guardPos.i += iMove;
                guardPos.j += jMove;
                spaces[guardPos.i, guardPos.j] = guards[guardIndex];//Mark current space 
            }
        }
        else
        {
            spaces[guardPos.i, guardPos.j] = 'X';
            guardPos.i += iMove;
            guardPos.j += jMove;
        }
        return guardPos;
    }

    private static int Part2(char[,] spaces, (int i, int j) startPos, char[] guards)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int loopableObstacles = 0;
        for (int i = 0; i < spaces.GetLength(0); i++)
        {
            for (int j = 0; j < spaces.GetLength(1); j++)
            {
                if (spaces[i, j] == 'X') //Only need to check tiles we walked on
                {
                    char[,] placedObstacle = (char[,])spaces.Clone();
                    placedObstacle[i, j] = '#';
                    if (UpdateGuard(ref placedObstacle, guards, startPos))
                    {
                        loopableObstacles++;
                    }
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ToString());
        return loopableObstacles;
    }

}
