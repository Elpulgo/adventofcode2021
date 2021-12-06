using System;
using System.Linq;

namespace adventofcode2021.Day2
{
    public class Day2 : BaseDay
    {
        public void Execute(){

            var lines = ReadInput(nameof(Day2));

            var horizontal = 0;
            var depth = 0;

            foreach (var line in lines)
            {
                var output = line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

                var operation = output.First();
                var value = Convert.ToInt32(output.Last());

                switch (operation)
                {
                    case "forward":
                    horizontal += value;
                    break;
                    case "down":
                    depth += value;
                    break;
                    case "up":
                    depth -= value;
                    break;
                }
            }

            FirstSolution((horizontal * depth).ToString());

            horizontal = 0;
            depth = 0;
            var aim = 0;

            foreach (var line in lines)
            {
                var output = line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

                var operation = output.First();
                var value = Convert.ToInt32(output.Last());

                switch (operation)
                {
                    case "forward":
                    horizontal += value;
                    depth += aim * value;
                    break;
                    case "down":
                    aim += value;
                    break;
                    case "up":
                    aim -= value;
                    break;
                }
            }

            SecondSolution((horizontal * depth).ToString());
        }
    }
}