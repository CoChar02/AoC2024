
using System.Net;
using System.Numerics;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inps = File.ReadAllLines("Inputs.txt");
            HashSet<(int, int)> rules = new HashSet<(int, int)>();
            List<List<int>> updates = new List<List<int>>();

            bool ruleUpdateDivider = true;

            //For each line of input, add the rules to the hashset and then updates into a list of integers
            foreach (string line in inps)
            {
                if (ruleUpdateDivider) //if not at end of rules
                {
                    if (string.IsNullOrWhiteSpace(line)) //end of rules
                    {
                        ruleUpdateDivider = false;
                    }
                    else 
                    {
                        int[] numbs = line.Split('|').Select(int.Parse).ToArray();
                        rules.Add((numbs[0], numbs[1]));
                    }
                }
                else
                {
                    updates.Add(line.Split(',').Select(int.Parse).ToList()); //Parse each input and add the list to updates
                }
            }

            Console.WriteLine(Part1(rules, updates));
            Console.WriteLine(Part2(rules, updates));
        }
        static int Part1 (HashSet<(int, int)> rules, List<List<int>> updates)
        {
            //Lambda function passes each line to CorrectUpdate which returns true if no rules broken
            //when it returns true, middle value is then added
            return updates.Where(x => CorrectUpdate(x, rules)).Sum(x => x[x.Count / 2]);
        }

        static int Part2(HashSet<(int, int)> rules, List<List<int>> updates)
        {
            List<List<int>> incorrectUpdates = updates.Where(x => !CorrectUpdate(x, rules)).ToList(); //Same as part 1 but only looking for cases where it returns false
            foreach (List<int> update in incorrectUpdates)
            {
                for (int i = 0; i < update.Count - 1; i++)
                {
                    for (int j = i + 1; j < update.Count; j++)
                    {
                        if (rules.Contains((update[j], update[i]))) // if J|I is in our ruleset, we can swap I and J to make the set consistent with the rules
                        {
                            (update[i], update[j]) = (update[j], update[i]); //swap I and J
                        }
                    }
                }
            }
            return incorrectUpdates.Sum(x  => x[x.Count / 2]);
        }

        static bool CorrectUpdate(List<int> pages, HashSet<(int, int)> rules)
        {
            for (int i = pages.Count - 1; i > 0; i--) //Look from the back of the input to see if a rule is broken
            {//rules are checked to see if a rule of i|j exists, if it does a rule is broken as i comes after j in our list
                for (int j = i-1; j >= 0; j--)
                {
                    if(rules.Contains((pages[i], pages[j])))
                    {
                        return false;
                    }
                }
            }
            return true; //no rules broken so we can return true
        }
    }
}
