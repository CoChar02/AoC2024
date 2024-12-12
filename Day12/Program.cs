

using System.Runtime.Serialization;

namespace Day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] stringsInput = File.ReadAllLines("Inputs.txt");
            char[,] inp = new char[stringsInput.Length, stringsInput[0].Length];
            for (int i = 0; i < stringsInput.Length; i++) 
            { 
                for (int j = 0; j < stringsInput[i].Length; j++)
                {
                    inp[j, i] = stringsInput[i][j];
                }
            }
            Console.WriteLine(Part1(inp));
            Console.WriteLine(Part2(inp));
        }

        static int Part1(char[,] inp)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            int sumOfPerim = 0;
            (int x, int y) nextRegion = (0, 0);
            while (nextRegion.x >= 0)
            {
                sumOfPerim += CalcRegion(nextRegion.x, nextRegion.y, inp, visited);
                nextRegion = FindNext(visited, inp);
            }
            return sumOfPerim;
        }
        static int Part2(char[,] inp)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            int sumOfPerim = 0;
            (int x, int y) nextRegion = (0, 0);
            while (nextRegion.x >= 0)
            {
                sumOfPerim += CalcRegionWithSides(nextRegion.x, nextRegion.y, inp, visited);
                nextRegion = FindNext(visited, inp);
            }
            return sumOfPerim;
        }
        static (int x, int y) FindNext(HashSet<(int, int)> visited, char[,] inp)
        {
            for(int i = 0; i < inp.GetLength(0); i++)
            { 
                for(int j = 0; j < inp.GetLength(1); j++)
                {
                    if(!visited.Contains((j, i)))
                    {
                        return (j, i);
                    }
                }
            }
            return (int.MinValue, int.MinValue);
        }

        static int CalcRegion(int x, int y, char[,] inp, HashSet<(int, int)> visited)
        {
            //DFS to find neighbouring tiles of the same region
            char curr = inp[x, y];
            int perim = 0;
            int area = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));
            while (queue.Count > 0)
            {
                (x, y) = queue.Dequeue();
                if(IsOutOfBounds((x,y), inp) || inp[x, y] != curr) //add to perim if out of bounds or not in same region
                {
                    perim++;
                    continue;
                }
                if(!visited.Add((x, y)))
                {
                    continue;
                }
                area++;
                queue.Enqueue((x, y-1)); //North
                queue.Enqueue((x+1, y)); //East
                queue.Enqueue((x, y+1)); //South
                queue.Enqueue ((x-1, y)); //west
            }
            return perim*area;
        }
        private static bool IsOutOfBounds((int x, int y) point, char[,] map) //simple check against map edges and the amount moved
        {;
            if (point.x >= map.GetLength(1) || point.x < 0 || point.y >= map.GetLength(0) || point.y < 0) return true;
            else return false;
        }
        static int CalcRegionWithSides(int x, int y, char[,] inp, HashSet<(int, int)> visited)
        {
            //Same as above for the DFS
            char curr = inp[x, y];
            int area = 0;
            int sides = 0;
            HashSet<(float, float)> pointsInRegion = new HashSet<(float, float)>();
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));
            while (queue.Count > 0)
            {
                (x, y) = queue.Dequeue();
                if (IsOutOfBounds((x, y), inp) || inp[x, y] != curr) //add to perim if out of bounds or not in same region
                {
                    continue;
                }
                if (!visited.Add((x, y)))
                {
                    continue;
                }
                area++;
                pointsInRegion.Add((x, y));
                queue.Enqueue((x, y - 1)); //North
                queue.Enqueue((x + 1, y)); //East
                queue.Enqueue((x, y + 1)); //South
                queue.Enqueue((x - 1, y)); //west
            }
            HashSet<(float, float)> checkCorners = new HashSet<(float, float)>(); //Possible corner positions
            List<(float i, float j)> dirs = [(0.5f, 0.5f), (0.5f, -0.5f), (-0.5f, 0.5f), (-0.5f, -0.5f)];
            foreach((int pointx, int pointy) point in pointsInRegion) //Check each point in the region adding the directions to check each corner
            {
                foreach((float xdiff, float ydiff) in dirs)
                {
                    checkCorners.Add((point.pointx + xdiff, point.pointy + ydiff));
                }
            }
            foreach((float i, float j) in checkCorners)
            {
                (float x, float y)[] connections = dirs.Select(z => (i + z.i, j + z.j)).Where(z => pointsInRegion.Contains(z)).ToArray(); //if corner + a step is in the region
                if (connections.Length == 1 || connections.Length == 3) //if only the original point or original + 2 others (ie not connected to only a single adjacent point)
                {
                    sides += 1;
                }
                else if(connections.Length == 2) //if length is 2 only adds if the second match is on a diagonal to the original, for instances like A B where there are 2 corners on one point
                {//                                                                                                                                  B A
                    (float, float) testCorners;
                    testCorners.Item1 = connections[0].x - connections[1].x;
                    testCorners.Item2 = connections[0].y - connections[1].y;
                    if (testCorners == (1, 1) || testCorners == (-1, 1) || testCorners == (1, -1) || testCorners == (-1, -1))
                    {
                        sides += 2;
                    }
                }
            }
            return sides * area;
        }
    }
}
