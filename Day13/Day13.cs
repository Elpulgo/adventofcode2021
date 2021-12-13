using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode2021.Day13
{
    internal class Day13 : BaseDay
    {
        internal void Execute()
        {
            Console.WriteLine(nameof(Day13));
            var sw = Stopwatch.StartNew();

            var input = ReadInput(nameof(Day13));
            var (coords, folds) = Setup(input);

            Fold(folds.First().Axis, folds.First().Value, coords);
            FirstSolution(coords.Count(c => c.Value > 0 && c.Value < int.MaxValue).ToString());
            Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms");
            sw.Restart();

            foreach (var fold in folds.Skip(1))
            {
                Fold(fold.Axis, fold.Value, coords);
            }

            Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms");

            // Part 2 can only be seen when printed out, since it forms 8 letters. (Answer is HZLEHJRK)

            var maxX = coords.Where(w => w.Value > 0 && w.Value < int.MaxValue).Select(s => s.Key.X).Max();
            var maxY = coords.Where(w => w.Value > 0 && w.Value < int.MaxValue).Select(s => s.Key.Y).Max();

            for (int y = 0; y <= maxY; y++)
            {
                var yAxis = string.Empty;
                for (int x = 0; x < maxX; x++)
                {
                    if (coords.ContainsKey((x, y)) && coords[(x, y)] > 0)
                    {
                        yAxis += coords[(x, y)].ToString();
                    }
                    else
                    {
                        yAxis += " ";
                    }
                }

                Console.WriteLine(yAxis);
            }
        }

        private void Fold(string axis, int value, Dictionary<(int X, int Y), int> coords)
        {
            if (axis == "y")
                foreach (var co in coords.Where(w => w.Key.Y > value && w.Value > 0 && w.Value < int.MaxValue))
                    HandleNewKey((co.Key.X, value - (co.Key.Y - value)), (co.Key), coords);
            else
                foreach (var co in coords.Where(w => w.Key.X > value && w.Value > 0 && w.Value < int.MaxValue))
                    HandleNewKey((value - (co.Key.X - value), co.Key.Y), (co.Key), coords);

            void HandleNewKey((int X, int Y) newKey, (int X, int Y) oldKey, Dictionary<(int X, int Y), int> coords)
            {
                if (coords.ContainsKey(newKey))
                {
                    coords[newKey] += 1;
                    coords.Remove(oldKey);
                }
            }
        }

        private (Dictionary<(int X, int Y), int> Coords, List<(string Axis, int Value)> Folds) Setup(string[] input)
        {
            var coords = input
                .Where(w => !w.Contains("fold") && w != string.Empty)
                .Select(s => s.Split(",", StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(k => (X: Convert.ToInt32(k.First()), Y: Convert.ToInt32(k.Last())), v => 1);

            var maxX = coords.Select(s => s.Key.X).Max();
            var maxY = coords.Select(s => s.Key.Y).Max();

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    if (!coords.ContainsKey((x, y)))
                        coords.Add((x, y), 0);
                }
            }

            var folds = input
                .Where(w => w.Contains("fold"))
                .Select(s => s.Replace("fold along", string.Empty).Trim())
                .Select(s => s.Split("=", StringSplitOptions.RemoveEmptyEntries))
                .Select(s => (Axis: s.First(), Value: Convert.ToInt32(s.Last())))
                .ToList();

            return (coords, folds);
        }
    }
}