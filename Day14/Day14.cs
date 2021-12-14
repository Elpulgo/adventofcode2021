using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode2021.Day14
{
    internal class Day14 : BaseDay
    {
        private Dictionary<string, char> _operations;
        public Day14(bool shouldPrint) : base(nameof(Day14), shouldPrint) { }
        public override void Execute()
        {
            var input = ReadInput();
            base.StartTimerOne();

            _operations = input.Skip(1).Where(w => w != string.Empty)
                .Select(s => s.Split("->", StringSplitOptions.TrimEntries))
                .ToDictionary(k => k[0].Trim(), v => char.Parse(v[1]));

            var chars = input.First().ToCharArray();

            for(var i = 0; i < 10; i++) { 
                chars = Step(chars).ToArray();
            }

            base.StopTimerOne();            

            var group = chars.GroupBy(g => g).OrderByDescending(o => o.Count());
            FirstSolution((group.First().Count() - group.Last().Count()).ToString());

            //Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms");
            //SecondSolution(string.Empty);
        }

        private IEnumerable<char> Step(char[] chars)
        {
            for (int i = 0; i < chars.Length - 1; i++)
            {
                var first = chars[i];
                var last = chars[i + 1];

                yield return first;
                yield return _operations[$"{chars[i]}{chars[i + 1]}"];

                if (i == chars.Length - 2)
                    yield return last;
            }
        }
    }
}