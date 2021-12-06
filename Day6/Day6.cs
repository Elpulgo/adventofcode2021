using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Advent.Day6
{
    internal class Day6
    {
        internal void Execute()
        {
            Console.WriteLine("Day 6:");

            var isPartTwo = false;
            var result = Execute(80);
            Console.WriteLine($"Part {(isPartTwo ? "2" : "1")}: {result}");
        }

        private int Execute(int days)
        {
            var initialLanternFish = File
               .ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day6", "Day6.txt"))
               .Split(",")
               .Select(s => Convert.ToInt32(s))
               .ToList();

            var fishs = initialLanternFish.Select(initalTimerCount => new LanternFish(initalTimerCount)).ToList();

            foreach (var day in Enumerable.Range(0, days))
            {
                var newFishThisDay = new List<LanternFish>();

                foreach (var fish in fishs)
                {
                    newFishThisDay.Add(fish.ComputeDay());
                }

                fishs.AddRange(newFishThisDay.Where(w => w != null).ToArray());
            }

            return fishs.Count;
        }
    }

    internal class LanternFish
    {
        private const int ResetTimerValue = 6;
        private int _timer = 0;
        public LanternFish()
        {
            _timer = 8;
        }
        public LanternFish(int initialTimer)
        {
            _timer = initialTimer;
        }

        public LanternFish ComputeDay()
        {
            if (_timer == 0)
            {
                _timer = ResetTimerValue;
                return new LanternFish();
            }

            _timer--;

            return null;
        }

    }
}