using System;
using System.IO;
using System.Linq;
using adventofcode2021;

namespace Advent.Day6
{
    internal class Day6 : BaseDay
    {
        internal void Execute()
        {
            Console.WriteLine("Day 6:");

            FirstSolution(Execute(80).ToString());
            SecondSolution(Execute(256).ToString());
        }

        private ulong Execute(int days)
        {
            var initialLanternFish = File
               .ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day6", "Day6.txt"))
               .Split(",")
               .Select(s => Convert.ToInt32(s))
               .ToList();

            var ages = new ulong[9];

            foreach (var fish in initialLanternFish)
            {
                ages[fish]++;
            }

            foreach (var day in Enumerable.Range(0, days))
            {
                var spawnFish = ages[0];
                var resetFish = ages[0];

                for (var index = 0; index < ages.Length - 1; index++)
                {
                    ages[index] = ages[index + 1];
                }

                ages[8] = spawnFish;
                ages[6] += resetFish;
            }

            ulong spawned = 0;

            for (var i = 0; i <= 8; i++)
            {
                spawned += ages[i];
            }

            return spawned;
        }
    }
}
