using adventofcode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2021.Day15
{
    internal class Day15 : BaseDay
    {
        public Day15(bool shouldPrint) : base(nameof(Day15), shouldPrint) { }

        public override void Execute()
        {
            var input = ReadInput();
            var width = input.First().Length;
            var height = input.Length;

            var riskLevels = new int[width, height];
            for (int i = 0; i < input.Length; i++)
            {
                string item = input[i];
                for (int j = 0; j < width; j++)
                {
                    riskLevels[j, i] = item[j] - '0';
                }
            }

            base.StartTimerOne();          
            var result = FindPath();
            base.StopTimerOne();
            
            FirstSolution(result.ToString());

            int FindPath()
            {
                var end = new Point(width - 1, height - 1, 0);
                var start = new Point(0, 0, 0);

                var (_, endingPoint) = Dijkstra.RunDijkstra(
                    start,
                    p => BuildNeighbours(p.X, p.Y).Select(s => (s, s.Cost)),
                    (_, p) => p.X == end.X && p.Y == end.Y);


                return endingPoint.Cost;
            }

            IEnumerable<Point> BuildNeighbours(int x, int y)
            {
                if (x - 1 > -1)
                    yield return new Point(x - 1, y, riskLevels[x - 1, y]);
                if (x + 1 < width)
                    yield return new Point(x + 1, y, riskLevels[x + 1, y]);
                if (y - 1 > -1)
                    yield return new Point(x, y - 1, riskLevels[x, y - 1]);
                if (y + 1 < height)
                    yield return new Point(x, y + 1, riskLevels[x, y + 1]);
            }
        }
    }

    internal record Point(int X, int Y, int Cost) : DijkstraNode(X, Y, Cost), IComparable<Point>
    {
        public int CompareTo(Point other)
            => base.Cost.CompareTo(other.Cost);
    }
}
