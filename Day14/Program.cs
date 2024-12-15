using System.Globalization;
using System.Text.RegularExpressions;

namespace Day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string regex = @"-?\d+";
            string[] inp = File.ReadAllLines("Inputs.txt");
            Regex rgx = new Regex(regex);
            List<Robot> bots = new List<Robot>();
            foreach (string line in inp)
            {
                MatchCollection matches = rgx.Matches(line);
                bots.Add(new Robot(int.Parse(matches[0].Value), int.Parse(matches[1].Value), int.Parse(matches[2].Value), int.Parse(matches[3].Value)));
            }
            Console.WriteLine(Part1(bots));
        }

        private static long Part1(List<Robot> bots)
        {
            (int x, int y) boardSize = (101, 103);
            foreach (Robot robot in bots)
            {
                Move(robot, 100, boardSize);
            }

            long northWest = bots.Count(r => r.pos.x < boardSize.x / 2 && r.pos.y < boardSize.y / 2);
            long southEast = bots.Count(r => r.pos.x > boardSize.x / 2 && r.pos.y > boardSize.y / 2);
            long northEast = bots.Count(r => r.pos.x < boardSize.x / 2 && r.pos.y > boardSize.y / 2);
            long southWest = bots.Count(r => r.pos.x > boardSize.x / 2 && r.pos.y < boardSize.y / 2);

            return northWest * northEast * southEast * southWest;
        }

        private static void Move(Robot robot, int steps, (int x, int y) boardSize)
        {
            robot.pos.x = (robot.pos.x + robot.change.x * steps) % boardSize.x;
            robot.pos.y = (robot.pos.y + robot.change.y * steps) % boardSize.y;
            if (robot.pos.x < 0) robot.pos.x += boardSize.x;
            if (robot.pos.y < 0) robot.pos.y += boardSize.y;
        }
    }
    class Robot
    {
        public (int x, int y) pos;
        public (int x, int y) change;
        public Robot(int posX, int posY, int changeX, int changeY)
        {
            this.pos.x = posX;
            this.pos.y = posY;
            this.change.x = changeX;
            this.change.y = changeY;
        }
    }
}
