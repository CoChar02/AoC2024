namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("Inputs.txt");
            int numSafeReports1 = 0;
            int numSafeReports2 = 0;
            foreach (string line in lines) //iterate through input
            {
                //Number handling and part1
                bool notValid = false;
                string[] rawNums = line.Split(' ');
                int[] ints = new int[rawNums.Length];
                for (int i = 0; i < ints.Length; i++) //Put input line into array
                {
                    ints[i] = int.Parse(rawNums[i]);
                }
                numSafeReports1 = safeReport(ref notValid, ints) ? numSafeReports1 += 1 : numSafeReports1; //Add 1 to counts if possible levels
                //Part2
                for (int i = 0;i < ints.Length; i++)
                {
                    List<int> dampenedInts = new List<int>();
                    for (int j = 0; j < ints.Length; j++) 
                    {
                        if (i != j)
                        {
                            dampenedInts.Add(ints[j]);
                        }
                    }
                    if(safeReport(ref notValid, dampenedInts.ToArray())) //Add 1 to counts if possible levels, then break to not overcount
                    {
                        numSafeReports2 += 1;
                        break;
                    }
                }
            }
            Console.WriteLine(numSafeReports1);
            Console.WriteLine(numSafeReports2);

        }

        private static bool safeReport(ref bool notValid, int[] ints)
        {
            notValid = false;
            int sign = int.Sign(ints[0] - ints[1]);
            for (int i = 0; i < ints.Length - 1; i++)
            {
                if (sign != int.Sign(ints[i] - ints[i + 1]) || Math.Abs(ints[i] - ints[i + 1]) > 3 || Math.Abs(ints[i] - ints[i + 1]) == 0) //Check valid conditions
                {
                    notValid = true;//Flag not valid if inconsistency
                }
            }
            if (!notValid)
            {
                return true;
            }
            return false;
        }
    }
}
