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

            var min = input.Min();
            var max = input.Max();

            var possibleValues = Enumerable.Range(min, (max - min) + 1)
                .ToDictionary(d => d, d => 0);

            for (int i = min; i <= max; i++)
            {
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
