
using System.Data;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("Inputs.txt");
            Console.WriteLine(CountXMAS(input));
            Console.WriteLine(CountMAS(input));
        }

        //Part1
        private static int CountXMAS(string[] input)
        {
            int count = 0;
            for (int row = 0; row < input.Length; row++)
            {
                string line = input[row];
                for (int col = 0; col < line.Length; col++)
                {
                    if (line[col] == 'X') //Search for X's
                    {
                        count += FindXMAS(input, row, col); //Add if XMAS found
                    }
                }
            }
            return count;
        }

        //Part2 same as above but look for A's in the middle of the MAS's
        private static int CountMAS(string[] input)
        {
            int count = 0;
            for (int row = 0; row < input.Length; row++)
            {
                string line = input[row];
                for (int col = 0; col < line.Length; col++)
                {
                    if (line[col] == 'A') //Search for X's
                    {
                        count += MASMatch(input, row, col); //Add if XMAS found
                    }
                }
            }
            return count;
        }



        /*
         * Make a call to match for each possible direction on each match
         *  
         *                -1, 0
         *           -1,-1    -1, +1
         *  0, -1           X           0, 1        
         *           +1,-1    +1, +1
         *                1,0
         * 
         */
        private static int FindXMAS(string[] input, int row, int col)
        {
            //makes calls listed above with appropriate grid changes, if XMAS is present from that X will value of however many it found
            return CountXMASMatch(input, row, col, -1, 0) + CountXMASMatch(input, row, col, -1, -1) + CountXMASMatch(input, row, col, -1, 1) + CountXMASMatch(input, row, col, 0, -1) +
                    CountXMASMatch(input, row, col, 0, 1) + CountXMASMatch(input, row, col, 1, -1) + CountXMASMatch(input, row, col, 1, 1) + CountXMASMatch(input, row, col, 1 ,0);
        }

        private static int CountXMASMatch(string[] input, int row, int col, int rowChange, int colChange)
        {
            bool foundXmas = (Match(input, row + rowChange, col + colChange, 'M') && Match(input, row + rowChange * 2, col + colChange * 2, 'A') && Match(input, row + rowChange * 3, col + colChange * 3, 'S'));
            return foundXmas ? 1 : 0;
        }


        /*
         * 
         * c1.1(-1, -1)   c2.1(-1, 1) 
         *            A
         * c2.2(1, -1)    c1.2(1,1)        
         * 
         * characters can be either M or S, if c1.1 is M, c1.2 is S and vice cersa
         * 
         */
        private static int MASMatch(string[] input, int row, int col)
        {           
            bool foundXmas = (Match(input, row - 1, col - 1, 'M') && Match(input, row + 1, col + 1, 'S') || Match(input, row - 1, col - 1, 'S') && Match(input, row + 1, col + 1, 'M')) //c1
                          && (Match(input, row + 1, col - 1, 'M') && Match(input, row - 1, col + 1, 'S') || Match(input, row + 1, col - 1, 'S') && Match(input, row - 1, col + 1, 'M')); //c2
            return foundXmas ? 1 : 0;
        }

        private static bool Match(string[] input, int row, int col, char target)
        {
            //see if expected letter
            try
            {
                return input[row][col] == target;
            }
            //lazy out of bounds
            catch (Exception e)
            {
                return false;
            }
        }
    }
    
}
