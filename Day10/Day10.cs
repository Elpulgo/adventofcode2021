using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2021.Day10
{
    internal class Day10 : BaseDay
    {
        private Dictionary<char, int> ScoreCardPart1 = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private Dictionary<char, ulong> ScoreCardPart2 = new() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        private Dictionary<char, char> OpeningClosingMap = new()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };

        internal void Execute()
        {
            var lines = ReadInput(nameof(Day10)).Select(s => s.ToCharArray());
            var countPartOne = 0;
            var countPartTwo = new List<ulong>();

            foreach (var line in lines)
            {
                char? firstIllegal = null;
                var allOpening = new Stack<char>();

                foreach (var c in line)
                {
                    if (!ScoreCardPart1.ContainsKey(c))
                    {
                        allOpening.Push(c);
                        continue;
                    }
                    var lastOpening = allOpening.Pop();
                    if (OpeningClosingMap[lastOpening] != c && !firstIllegal.HasValue)
                        firstIllegal = c;
                }

                if (firstIllegal.HasValue)
                    countPartOne += ScoreCardPart1[firstIllegal.Value];
                else
                {
                    ulong innerCount = 0;
                    var allUnclosed = new Stack<char>(allOpening.Reverse());
                    while (allUnclosed.Any())
                        innerCount = (innerCount * 5) + ScoreCardPart2[OpeningClosingMap[allUnclosed.Pop()]];

                    countPartTwo.Add(innerCount);
                }
            }

            FirstSolution(countPartOne.ToString());
        
            var index = countPartTwo.Count / 2;
            SecondSolution(countPartTwo.OrderBy(o => o).ToList()[index].ToString());
        }
    }
}
