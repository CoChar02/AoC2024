

using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inp = File.ReadAllText("Inputs.txt");
            List<int> unpackedInp = Unpack(inp);
            Console.WriteLine(Part1(unpackedInp));
            unpackedInp = Unpack(inp); //reset for part 2
            Console.WriteLine(Part2(unpackedInp));
        }

        static List<int> Unpack(string inp) //Unpacks input into desired format 12345 -> 0..111....22222 (1 0, 2 empty, 3 1s, 4 empty, 5 2s)
        {
            List<int> blocks = new List<int>();
            for (int i = 0; i < inp.Length; i++)
            {
                int id = i % 2 == 0 ? i / 2 : int.MinValue;
                for (int j = 0; j < int.Parse(inp[i].ToString()); j++)
                {
                    blocks.Add(id);
                }
            }
            return blocks;
        }

        static long Part1(List<int> inp)
        {
            int i = 0; //Start I on first empty memory
            int j = inp.Count - 1; //Start J on last memory block
            for (; i < inp.Count; i++)
            {
                if (inp[i] != int.MinValue) //Empty block on I
                {
                    continue;
                }
                while (inp[j] == int.MinValue)//Move J til new data
                {
                    j--;
                }
                if (j < i)
                {
                    break;
                }
                inp[i] = inp[j]; //Swap I and J
                inp[j] = int.MinValue;
                i--;
            }
            long sum = 0;//take sum
            for (int k = 0; k < inp.Count; k++)
            {
                if (inp[k] != int.MinValue)
                {
                    sum += k * inp[k];
                }
            }
            return sum;
        }
        static long Part2(List<int> inp)
        {
            //Start at the back of the array, find length of datablock, then look from front of the array to see if there is a space big enough for this data block
            int i = 0;
            int j = inp.Count - 1;
            while(j > 0) // J has not explored whole dataset
            {
                i = 0;
                if ((inp[j] != int.MinValue))
                {
                    int blockLength = 1;
                    while (j > 0 && inp[j-1] == inp[j]) //find length of data block
                    {
                     blockLength++;
                     j--;
                    }
                    int freeBlockLength = 0;
                    while (i < j) //never move data towards the back of the array
                    {
                        if (((inp[i] == int.MinValue))) //Count consecutive empty spaces 
                        {
                            freeBlockLength++;
                        }
                        else
                        {
                            freeBlockLength = 0; //if not empty reset counter
                        }
                        if (blockLength == freeBlockLength) // when there are enough free spaces to contain the data block
                        {
                            for (int k = 0; k < blockLength; k++) 
                            {
                                inp[i - blockLength + k + 1] = inp[j + blockLength - k - 1]; // swap each piece of data 
                                inp[j + blockLength - k - 1] = int.MinValue;
                            }
                            break; //Can stop looking as data placed
                        }
                        i++; //Keep moving I if we didn't find enough space until i == j
                    }
                }
                j--; //Move J along to find the next block
            }
            long sum = 0;
            for (int k = 0; k < inp.Count; k++)
            {
                if (inp[k] != int.MinValue)
                {
                    sum += k * inp[k];
                }
            }
            return sum;
        }
    }
}
