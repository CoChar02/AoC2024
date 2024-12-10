
using System.Collections.Generic;
using System.Drawing;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inps = File.ReadAllLines("Inputs.txt");
            int[,] map = new int[inps[0].Length,inps.Length]; //make map from input
            List<(int x, int y)> trailhead = new List<(int x, int y)>();

            for (int i = 0; i < inps.Length; i++) //find trailheads
            {
                string row = inps[i];
                for (int j = 0; j < row.Length; j++) 
                {
                    int point = int.Parse(row[j].ToString());
                    map[j, i] = point;
                    if (point == 0)
                    {
                        trailhead.Add((j, i));
                    }
                }
            }

            Console.WriteLine(Part1(trailhead, map));
            Console.WriteLine(Part2(trailhead, map));
        }

        static int Part1(List<(int x, int y)> trailheads, int[,] map)
        {
            int sum = 0;
            Queue<(int x, int y)> dfsQueue = new Queue<(int x, int y)>(); //DFS queue
            HashSet<(int x, int y)> visitedPeak = new HashSet<(int x, int y)>(); //Set of visited peaks

            foreach ((int x, int y) trailhead in trailheads)
            {
                dfsQueue.Clear();
                visitedPeak.Clear();
                dfsQueue.Enqueue(trailhead); //Push trailhead to queue
                while(dfsQueue.Count > 0)
                {
                    (int x, int y) point = dfsQueue.Dequeue(); //pop first item from queue
                    int elev = map[point.x, point.y];
                    if (elev == 9 && !visitedPeak.Contains((point.x, point.y)))
                    {
                        sum++;
                        visitedPeak.Add((point.x, point.y));
                    }
                    else
                    {
                        if(!IsOutOfBounds(point, map, 0, -1) && map[point.x, point.y - 1] == elev + 1)//move North
                        {
                            dfsQueue.Enqueue((point.x, point.y - 1));
                        }
                        if (!IsOutOfBounds(point, map, 1, 0) && map[point.x + 1, point.y] == elev + 1)//move East
                        {
                            dfsQueue.Enqueue((point.x + 1, point.y));
                        }
                        if (!IsOutOfBounds(point, map, 0, 1) && map[point.x, point.y + 1] == elev + 1)//move South
                        {
                            dfsQueue.Enqueue((point.x, point.y + 1));
                        }
                        if (!IsOutOfBounds(point, map, -1, 0) && map[point.x - 1, point.y] == elev + 1)//move West
                        {
                            dfsQueue.Enqueue((point.x - 1, point.y));
                        }
                    }
                }
            }
            return sum;
        }

        static int Part2(List<(int x, int y)> trailheads, int[,] map) //literally the exact same as part 1 but with visited peaks removed
        {
            int sum = 0;
            Queue<(int x, int y)> dfsQueue = new Queue<(int x, int y)>();

            foreach ((int x, int y) trailhead in trailheads)
            {
                dfsQueue.Clear();
                dfsQueue.Enqueue(trailhead); //Push trailhead to queue
                while (dfsQueue.Count > 0)
                {
                    (int x, int y) point = dfsQueue.Dequeue(); //pop first item from queue
                    int elev = map[point.x, point.y];
                    if (elev == 9)
                    {
                        sum++;
                    }
                    else
                    {
                        if (!IsOutOfBounds(point, map, 0, -1) && map[point.x, point.y - 1] == elev + 1)//move North
                        {
                            dfsQueue.Enqueue((point.x, point.y - 1));
                        }
                        if (!IsOutOfBounds(point, map, 1, 0) && map[point.x + 1, point.y] == elev + 1)//move East
                        {
                            dfsQueue.Enqueue((point.x + 1, point.y));
                        }
                        if (!IsOutOfBounds(point, map, 0, 1) && map[point.x, point.y + 1] == elev + 1)//move South
                        {
                            dfsQueue.Enqueue((point.x, point.y + 1));
                        }
                        if (!IsOutOfBounds(point, map, -1, 0) && map[point.x - 1, point.y] == elev + 1)//move West
                        {
                            dfsQueue.Enqueue((point.x - 1, point.y));
                        }
                    }
                }
            }
            return sum;
        }
        private static bool IsOutOfBounds((int x, int y) point, int[,] map, int xDiff, int yDiff) //simple check against map edges and the amount moved
        {
            int newX = point.x + xDiff;
            int newY = point.y + yDiff;
            if (newX >= map.GetLength(1) || newX < 0 || newY >= map.GetLength(0) || newY < 0) return true;
            else return false;
        }

    }
}
