using System;
using System.Linq;

namespace adventofcode2021.Day7
{
    internal class Day7 : BaseDay
    {
        internal void Execute()
        {
            Console.WriteLine("Day 7:");

            FirstSolution(InternalExecute(isPartTwo: false).ToString());
            SecondSolution(InternalExecute(isPartTwo: true).ToString());
        }

        private int InternalExecute(bool isPartTwo)
        {
            var input = ReadInput(nameof(Day7))
                .First()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => Convert.ToInt32(s))
                .ToList();

            var median = input[input.Count / 2];
            var avg = input.Sum() / input.Count;

            var min = input.Min();
            var max = input.Max();

            var possibleValues = Enumerable.Range(min, (max - min) + 1)
                .ToDictionary(d => d, d => 0);


            for (int i = min; i <= max; i++)
            {
                // More clever solution using algebra..
                //var delta = i - min;
                //possibleValues[i] = input.Sum(s => (Math.Abs(s - delta) * (Math.Abs(s - delta) + 1)) / 2);

                foreach (var inp in input)
                {
                    var steps = Math.Abs(inp - i);

                    if (isPartTwo)
                    {
                        steps = Enumerable.Range(1, steps)
                            .Select((s, i) => i + 1)
                            .Sum();
                    }

                    possibleValues[i] += steps;
                }
            }

            return possibleValues
                .OrderBy(kvp => kvp.Value)
                .First()
                .Value;
        }
    }
}
