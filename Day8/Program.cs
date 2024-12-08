
namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inps = File.ReadAllLines("Inputs.txt");
            Dictionary<char, List<(int, int)>> antenna = new Dictionary<char, List<(int x, int y)>>();
            for (int i = 0; i < inps.Length; i++) //Loop thru Y axis
            {
                for (int j = 0; j < inps[i].Length; j++) // Loop thru X axis
                {
                    char c = inps[i][j];
                    if (c != '.')
                    {
                        if (!antenna.ContainsKey(c))
                        {
                            antenna.Add(c, new List<(int x, int y)>());
                        }
                        antenna[c].Add((j, i)); //Add to dict
                    }
                }
            }
            Console.WriteLine(Part1(antenna, inps.Length));
            Console.WriteLine(Part2(antenna, inps.Length));
        }

        static int Part1(Dictionary<char, List<(int x, int y)>> antenna, int length)
        {
            HashSet<(int, int)> validCoords = new HashSet<(int, int)>();
            foreach (KeyValuePair<char, List<(int x, int y)>> characterOnMap in antenna)
            {
                for (int i = 0; i < characterOnMap.Value.Count; i++)
                {
                    for (int j = i + 1; j < characterOnMap.Value.Count; j++)
                    {
                        (int x, int y) pointA = characterOnMap.Value[i];
                        (int x, int y) pointB = characterOnMap.Value[j];
                        //Difference in co-ordinates
                        int xDiff = pointA.x - pointB.x;
                        int yDiff = pointA.y - pointB.y;

                        //First new point
                        int newXA = pointA.x + xDiff;
                        int newYA = pointA.y + yDiff;

                        //Second new point
                        int newXB = pointB.x - xDiff;
                        int newYB = pointB.y - yDiff;

                        //Check point 1 is valid
                        if (newXA < length && newXA >= 0 && newYA < length && newYA >= 0)
                        {
                            validCoords.Add((newXA, newYA));
                        }
                        //Check point 2 is valid
                        if (newXB < length && newXB >= 0 && newYB < length && newYB >= 0)
                        {
                            validCoords.Add((newXB, newYB));
                        }
                    }
                }
            }
            return validCoords.Count;
        }
        static int Part2(Dictionary<char, List<(int x, int y)>> antenna, int length)
        {
            HashSet<(int, int)> validCoords = new HashSet<(int, int)>();
            foreach (KeyValuePair<char, List<(int x, int y)>> characterOnMap in antenna)
            {
                for (int i = 0; i < characterOnMap.Value.Count; i++)
                {
                    for (int j = i + 1; j < characterOnMap.Value.Count; j++)
                    {
                        (int x, int y) pointA = characterOnMap.Value[i];
                        (int x, int y) pointB = characterOnMap.Value[j];
                        //Difference in co-ordinates
                        int xDiff = pointA.x - pointB.x;
                        int yDiff = pointA.y - pointB.y;
                        //Take repeated steps until we go off board dir 1
                        for (int k = 0; k < Math.Abs(length / xDiff) || k < Math.Abs(length / yDiff); k++)
                        {
                            int newXA = pointA.x + xDiff * k;
                            int newYA = pointA.y + yDiff * k;
                            if (newXA < length && newXA >= 0 && newYA < length && newYA >= 0)
                            {
                                validCoords.Add((newXA, newYA));
                            }
                        }
                        //Take repeated steps until we go off board dir 2
                        for (int k = 0; k < Math.Abs(length / xDiff) || k < Math.Abs(length / yDiff); k++)
                        {
                            int newXB = pointB.x - xDiff * k;
                            int newYB = pointB.y - yDiff * k;
                            if (newXB < length && newXB >= 0 && newYB < length && newYB >= 0)
                            {
                                validCoords.Add((newXB, newYB));
                            }
                        }
                    }
                }
            }
            return validCoords.Count;

        }
    }
}
