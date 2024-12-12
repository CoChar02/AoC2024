
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<long> inp = File.ReadAllText("Inputs.txt").Split(' ').Select(long.Parse).ToList();
            Console.WriteLine(blinkStones(inp, 25));
            Console.WriteLine(blinkStones(inp, 75));

        }

        static long blinkStones(List<long> inp, int numOfLoops)
        {
            //order of items doesnt actually matter despite what the prompt says, so we can store stones in a dictionary where the key is the stone number and the value is the stones count
            Dictionary<long, long> stones = inp.GroupBy(x => x).ToDictionary(x => x.Key, x => x.LongCount());//Make dictionary of input string 
            for (int i = 0; i < numOfLoops; i++)
            {
                Dictionary<long, long> changes = new Dictionary<long, long>(); //Make a temp dictionary of the new stones
                foreach (KeyValuePair<long, long> stone in stones)
                {
                    if (stone.Key == 0) //Rule 1
                    {
                        if (!changes.TryAdd(1, stone.Value)) //Add stone with value 1 to dictionary or update its values if already exists
                        {
                            changes[1] += stone.Value;
                        }
                    }
                    else if (stone.Key.ToString().Length % 2 == 0) //Rule 2
                    {
                        string str = stone.Key.ToString();
                        long left = long.Parse(str[..(str.Length / 2)]);
                        long right = long.Parse(str[(str.Length / 2)..]);
                        if (!changes.TryAdd(left, stone.Value)) //add stone with value of first half to dict or update its values if it already exists
                        {
                            changes[left] += stone.Value;
                        }
                        if (!changes.TryAdd(right, stone.Value))//add stone with value of second half to dict or update its values if it already exists
                        {
                            changes[right] += stone.Value;
                        }
                    }
                    else
                    {
                        if (!changes.TryAdd(stone.Key * 2024, stone.Value)) //Rule 3
                        {
                            changes[stone.Key * 2024] += stone.Value;
                        }
                    }
                    stones.Remove(stone.Key);
                }
                foreach(KeyValuePair<long, long> change in changes) //Update stones with the new dictionary
                {
                    stones[change.Key] = change.Value;
                }
            }
            return stones.Values.Sum(); //return amounts of each stone summed
        }
    }
}

