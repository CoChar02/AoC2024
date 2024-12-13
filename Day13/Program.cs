using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inps = File.ReadAllLines("Inputs.txt");
            Console.WriteLine(FindTotalCost(inps));
        }

        static long FindTotalCost(string[] inps)
        {
            long sum = 0;
            for(int i = 0; i < inps.Length; i += 4)
            {
                string buttonA = inps[i];
                string buttonB = inps[i + 1];
                string prizeLoc = inps[i + 2];

                sum += CalculateSimultEq(buttonA, buttonB, prizeLoc);
            }
            return sum;
        }

        static long CalculateSimultEq(string buttonA, string buttonB, string prizeLoc)
        {
            (long x, long y) buttonAVals = getNums(buttonA);
            (long x, long y) buttonBVals = getNums(buttonB);
            (long x, long y) prizeVals = getPrize(prizeLoc);

            //Cramers rules for simultaneous equations
            double a1 = buttonAVals.x, b1 = buttonBVals.x, p1 = prizeVals.x; //coefficients
            double a2 = buttonAVals.y, b2 = buttonBVals.y, p2 = prizeVals.y;

            double D = a1 * b2 - a2 * b1; //calculate determinant

            if (D == 0)
            {
                return 0; //No solutions
            }
            double Dx = p1 * b2 - p2 * b1; //Determinant for A
            double Dy = p2 * a1 - p1 * a2; //Determinant for B

            double A = Dx / D;
            double B = Dy / D;

            if (A % 1 != 0 || B % 1 != 0) //Only care about whole number soltions
            {
                return 0;
            }
            return (long)A * 3 + (long)B;
        }

        static (long x, long y) getNums(string line)
        {
            string[] splits = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries); //split on the colon then split on x and y
            int x = int.Parse(splits[0].Trim().Split('+')[1]);
            int y = int.Parse(splits[1].Trim().Split('+')[1]);
            return (x, y);
        }
        static (long x, long y) getPrize(string line)
        {
            string[] splits = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries); //split on the colon then split on x and y
            long x = 10000000000000 + int.Parse(splits[0].Trim().Split('=')[1]); //Add big number to the prize for part 2
            long y = 10000000000000 + int.Parse(splits[1].Trim().Split('=')[1]);
            return (x, y);
        }
    }
}
