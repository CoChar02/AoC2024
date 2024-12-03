using System.Text.RegularExpressions;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string input = File.ReadAllText("Inputs.txt");
            string pattern = @"mul\(([0-9]{1,3})\,([0-9]{0,3})\)";//Regex pattern checks for mul(1-3 digits, 1-3digits)
            MatchCollection matches = Regex.Matches(input, pattern);
            int total = 0;
            foreach (Match match in matches) //Multiply together numbers in each match
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                total += x * y;
            }
            Console.WriteLine(total);

            total = 0;
            bool dont = false; //starts with do
            pattern = @"mul\(([0-9]{1,3})\,([0-9]{0,3})\)|don't\(\)|do\(\)"; //Same regex as above, also adds don't() and do()
            matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                if (match.ToString() == "don't()") //if dont, enable dont flag and continue
                {
                    dont = true;
                    continue;
                }
                if (match.ToString() == "do()") //if do, disable dont flag and continue
                {
                    dont = false;
                    continue;
                }
                if (dont) //if dont flag dont multiply values
                {
                    continue;
                }
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                total += x * y;
            }
            Console.WriteLine(total);
        }
    }
}
