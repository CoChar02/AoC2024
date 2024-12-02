using System.Collections.Immutable;
using System.IO;
using System.Collections;
namespace Day1
{
    internal class Program
    {
        //Part 1
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("Inputs.txt");
            int[] left = new int[lines.Length];
            int[] right = new int[lines.Length];
            //Populate left and right
            for (int i = 0; i < lines.Length; i++)
            {
                left[i] = int.Parse(lines[i].Substring(0, lines[i].IndexOf(' ')));
                right[i] = int.Parse(lines[i].Substring(lines[i].LastIndexOf(' ') + 1));
            }
            //Sort arrays
            Array.Sort(left);
            Array.Sort(right);

            int sum = 0;

            for (int i = 0; i < left.Length; i++)
            {
                sum += Math.Abs(left[i] - right[i]);
            }
            Console.WriteLine(sum);

            //Part 2
            //Iterate over right list and add to a dictionary, then multiply left by amount in that dictionary

            Dictionary<int, int> rightCounts = new Dictionary<int, int>();
            for (int i = 0; i < right.Length; i++)
            {
                {
                    if (rightCounts.ContainsKey(right[i])) //If already in dictionary, add one to the count
                    {
                        rightCounts[right[i]]++;
                    }
                    else
                    {
                        rightCounts.Add(right[i], 1);
                    }
                }
            }
            //Calculate Similarity
            int similarityScore = 0;
            for (int i = 0; i < left.Length; i++)
            {
                if (rightCounts.ContainsKey(left[i]))
                {
                    similarityScore += left[i] * rightCounts[left[i]];

                }
            }
            Console.WriteLine(similarityScore);
        }
    }
}
