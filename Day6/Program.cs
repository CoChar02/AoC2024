


using System.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] inps = File.ReadAllLines("Inputs.txt");
        char[,] spaces = new char[inps.Length, inps[0].Length];

        for(int i = 0; i < spaces.GetLength(0); i++)
        {
            for(int j = 0; j < spaces.GetLength(1); j++)
            {
                spaces[i,j] = inps[i][j];
            }
        }

        char[] guards = { '^', '>', 'v', '<' };
        Console.WriteLine(Part1(spaces, guards));
    }

    private static int Part1(char[,] spaces, char[] guards)
    {
        (int i, int j) guardPos = (0,0);
        int numOfX = 0;
        //Find Guard
        for (int i = 0; i < spaces.GetLength(0); i++)
        {
            for (int j = 0; j < spaces.GetLength(1); j++)
            {
                if (guards.Any(x => spaces[i, j] == x))
                {
                    (guardPos.i, guardPos.j) = (i, j);
                }
            }
        }
        /*
         * Guard directions
         *  ^(-1,0)
         *  >(0,1)
         *  v(1,0)
         *  <(0,-1)
         */
        while (guardPos.i < spaces.GetLength(0) && guardPos.i >= 0 && guardPos.j < spaces.GetLength(0) && guardPos.j >= 0) //Til we go out of bounds
        {
            if (spaces[guardPos.i, guardPos.j] == guards[0])  //if guard facing up
            {
                guardPos = GuardMove(ref spaces, guardPos, guards, -1, 0, 0);
            }
            else if (spaces[guardPos.i, guardPos.j] == guards[1])// facing right
            {
                guardPos = GuardMove(ref spaces, guardPos, guards, 0, 1, 1);
            }
            else if (spaces[guardPos.i, guardPos.j] == guards[2]) // facing down
            {
                guardPos = GuardMove(ref spaces, guardPos, guards, 1, 0, 2);
            }
            else if (spaces[guardPos.i, guardPos.j] == guards[3]) // facing left
            {
                guardPos = GuardMove(ref spaces, guardPos, guards, 0, -1, 3);
            }
        }
        return (from char space in spaces where space == 'X' select space).Count() + 1;
    }

    private static (int, int) GuardMove(ref char[,] spaces, (int i, int j) guardPos, char[] guards, int iMove, int jMove, int guardIndex)
    {
        if(guardPos.i + iMove < spaces.GetLength(0) && guardPos.i + iMove >= 0 && guardPos.j + jMove < spaces.GetLength(0) && guardPos.j + jMove >= 0)
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
            guardPos.i += iMove;
            guardPos.j += jMove;

        }
        return guardPos;
    }
}
