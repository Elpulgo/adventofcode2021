using System;
using System.IO;
using System.Linq;
using adventofcode2021;

namespace Advent
{
    internal class Day1 : BaseDay
    {
        public Day1(bool shouldPrint): base(nameof(Day1), shouldPrint) {}

        public override void Execute()
        {
            Console.WriteLine("Day 1:");

            var input = ReadInput()
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

            FirstSolution(increasedPartOne.ToString());
            SecondSolution(increasedPartTwo.ToString());
        }
    }
}
