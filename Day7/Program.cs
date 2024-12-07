using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inps = File.ReadAllLines("Inputs.txt");
            string numRegex = @"\d+";
            Regex regex = new Regex(numRegex);
            List<List<long>> numbs = new List<List<long>>();
            foreach (string line in inps)
            {
                List<long> dataFromString = new List<long>();
                MatchCollection matches = regex.Matches(line);
                foreach (Match match in matches)
                {
                    dataFromString.Add(long.Parse(match.Value));
                }
                numbs.Add(dataFromString);
            }
            Console.WriteLine(Part1(numbs));
            Console.WriteLine(Part2(numbs));
        }
        static List<string> OpGenerator(int n, bool concat = false)
        {
            List<string> result = new List<string>();
            RecursiveCombinationGenerator("", n, result, concat); //Start recursive operations adder, starts with empty string
            return result;
        }

        static void RecursiveCombinationGenerator(string ops, int opsremaining, List<string> result, bool concat = false)  //generates strings of input combinations like ++++,+*+*, ****, ***+ etc
        {
            if (opsremaining == 0)
            {
                result.Add(ops);
                return;
            }

            RecursiveCombinationGenerator(ops + "+", opsremaining - 1, result, concat);
            RecursiveCombinationGenerator(ops + "*", opsremaining - 1, result, concat);
            if (concat)
            {
                RecursiveCombinationGenerator(ops + "|", opsremaining - 1, result, concat);
            }
        }

        static bool validCalc(List<long> input, bool concat = false)
        {
            List<string> operatorCombinations = OpGenerator(input.Count - 2, concat); //Start number n-1 operations and also not counting first number (goal)
            foreach (string combo in operatorCombinations)
            {
                long currAnswer = input[1]; //starts with current answer is first input
                for (int i = 1; i <= combo.Length; i++)
                {
                    if (combo[i - 1] == '+')
                    {
                        currAnswer += input[i + 1];
                    }
                    if (combo[i - 1] == '*')
                    {
                        currAnswer *= input[i + 1];
                    }
                    if (combo[i - 1] == '|' && concat) //concatenate
                    {
                        currAnswer = long.Parse(currAnswer.ToString() + input[i + 1].ToString()); 
                    }
                }
                if (currAnswer == input[0]) return true;
            }
            return false;
        }

        static long Part1(List<List<long>> numbs)
        {
            return numbs.Where(x => validCalc(x)).Sum(x => x[0]);
        }
        static long Part2(List<List<long>> numbs)
        {
            return numbs.Where(x => validCalc(x, true)).Sum(x => x[0]);
        }
    }
}
