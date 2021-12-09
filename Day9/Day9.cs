using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2021.Day9
{
    internal class Day9 : BaseDay
    {
        private int _rowLength = 0;
        private int _columnLength = 0;

        internal void Execute()
        {
            Console.WriteLine("Day 9:");

            var lines = ReadInput(nameof(Day9));

            var input = lines
                .Select(s => s.ToCharArray())
                .SelectMany((s, i) => s.Select((p, columnIndex) => new KeyValuePair<(int X, int Y), (int Value, List<(int, int)> Neighbours)>((columnIndex, i), (Convert.ToInt32(p - '0'), new List<(int, int)>()))))
                .ToDictionary(d => d.Key, d => d.Value);

            _rowLength = lines.First().Length;
            _columnLength = lines.Length;

            var riskLevel = 0;
            var basins = new List<int>();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines.First().Length; x++)
                {
                    var value = input[(x, y)].Value;

                    int?
                        top = FindValue(x, y - 1),
                        down = FindValue(x, y + 1),
                        left = FindValue(x - 1, y),
                        right = FindValue(x + 1, y);

                    if (
                        IsLower(top, value) &&
                        IsLower(down, value) &&
                        IsLower(left, value) &&
                        IsLower(right, value))
                    {
                        riskLevel += (value + 1);

                        var basinCount = 1;
                        var visisted = new HashSet<(int X, int Y)>();
                        visisted.Add((x, y));
                        basinCount += BasinCount(x, y, input, Direction.None, visisted);

                        basins.Add(basinCount);
                    }

                    bool IsLower(int? neighbour, int value)
                     => !neighbour.HasValue ? true : neighbour.Value > value;

                    int? FindValue(int x, int y)
                     => input.TryGetValue((x, y), out var downFound)
                            ? downFound.Value
                            : null;
                }
            }

            FirstSolution(riskLevel.ToString());
            SecondSolution(basins.OrderByDescending(o => o).Take(3).Aggregate((a, b) => a * b).ToString());

        }

        private int BasinCount(
            int x,
            int y,
            Dictionary<(int X, int Y), (int Value, List<(int, int)> Neighbours)> input,
            Direction directionToSkip,
            HashSet<(int X, int Y)> visited)
        {
            var basinCount = 0;

            if (directionToSkip != Direction.Down)
                basinCount += Basins(x, y, Direction.Down, input, visited);
            if (directionToSkip != Direction.Right)
                basinCount += Basins(x, y, Direction.Right, input, visited);
            if (directionToSkip != Direction.Top)
                basinCount += Basins(x, y, Direction.Top, input, visited);
            if (directionToSkip != Direction.Left)
                basinCount += Basins(x, y, Direction.Left, input, visited);

            return basinCount;
        }

        private int Basins(
            int x,
            int y,
            Direction direction,
            Dictionary<(int X, int Y), (int Value, List<(int, int)> Neighbours)> input,
            HashSet<(int X, int Y)> visited)
        {
            var count = 0;

            switch (direction)
            {
                case Direction.Top:
                    while (y > 0)
                    {
                        y--;
                        if (visited.Contains((x, y)))
                            break;

                        if (input[(x, y)].Value != 9)
                        {
                            count++;
                            visited.Add((x, y));
                            count += BasinCount(x, y, input, Direction.Down, visited);
                        }
                        break;
                    }
                    break;
                case Direction.Down:
                    while (y < _columnLength - 1)
                    {
                        y++;
                        if (visited.Contains((x, y)))
                            break;

                        if (input[(x, y)].Value != 9)
                        {
                            count++;
                            visited.Add((x, y));
                            count += BasinCount(x, y, input, Direction.Top, visited);
                        }
                        break;
                    }
                    break;
                case Direction.Left:
                    while (x > 0)
                    {
                        x--;
                        if (visited.Contains((x, y)))
                            break;

                        if (input[(x, y)].Value != 9)
                        {
                            count++;
                            visited.Add((x, y));

                            count += BasinCount(x, y, input, Direction.Right, visited);
                        }
                        break;
                    }
                    break;
                case Direction.Right:
                    while (x < _rowLength - 1)
                    {
                        x++;
                        if (visited.Contains((x, y)))
                            break;

                        if (input[(x, y)].Value != 9)
                        {
                            count++;
                            visited.Add((x, y));

                            count += BasinCount(x, y, input, Direction.Left, visited);
                        }
                        break;
                    }
                    break;
                default:
                    break;
            }

            return count;
        }

        public enum Direction
        {
            None,
            Top,
            Down,
            Left,
            Right

        }
    }
}
