using System;
using System.IO;
using System.Linq;

namespace Advent
{
    internal class Day1
    {
        internal void Execute()
        {
            Console.WriteLine("Day 1:");

            var input = File
                .ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day1", "Day1.txt"))
                .Select(s => Convert.ToInt32(s))
                .ToList();

            var partOne = false;
            var increasedPartOne = 0;
            var increasedPartTwo = 0;


            for (var i = 0; i < input.Count; i++)
            {
                if (partOne)
                {
                    if (input[i] > input[i - 1])
                    {
                        increasedPartOne++;
                    }
                    continue;
                }

                if (input.Count - 1 >= i + 3)
                {

                    var windowOne = input.Skip(i).Take(3);
                    var windowTwo = input.Skip(i + 1).Take(3);

                    if (windowOne.Sum() < windowTwo.Sum())
                    {
                        increasedPartTwo++;
                    }
                }
            }

            Console.WriteLine($"Part 1: {increasedPartOne}");
            Console.WriteLine($"Part 2: {increasedPartTwo}");
        }
    }
}
