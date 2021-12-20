using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2021.Day15
{
    internal class Day15 : BaseDay
    {
        public Day15(bool shouldPrint) : base(nameof(Day15), shouldPrint) { }

        public override void Execute()
        {
            var input = ReadInput();
            //Select(s => s.Select(s => Convert.ToInt32(s - '0')).ToArray()).ToArray();
            var width = input.First().Length;
            var height = input.Length;

            var riskLevels  = new int[width, height];
            for (int i = 0; i < input.Length; i++)
            {
                string item = input[i];
                for (int j = 0; j < width; j++)
                {
                    riskLevels[j, i] = item[j] - '0';
                }
            }

            var apa = ReadInput().SelectMany(s => s.Select(b => (s, b))).ToArray();
            base.StartTimerOne();

            var neighbours = new Dictionary<Point, List<Point>>();
            var visited = new HashSet<Point>();

            //foreach (var x in Enumerable.Range(0, riskLevels.Length))
            //    foreach (var y in Enumerable.Range(0, riskLevels[x].Length))
            //        neighbours.Add(new Point(x, y, riskLevels[x][y], 0), BuildNeighbours(x, y).ToList());


            int FindPath()
            {


                var end = (X: 9, Y: 9);
                (int, int)[] dirs = { (0, 1), (1, 0), (-1, 0), (0, -1) };
                Queue<Point> queue = new Queue<Point>();
                Dictionary<(int, int), int> seen = new Dictionary<(int, int), int>();
                queue.Enqueue(new Point(0, 0, 0, 0));
                seen.Add((0, 0), 0);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    if (current.X == end.X && current.Y == end.Y)
                    {
                        current.Print();
                        return current.Risk;
                    }

                    foreach ((int x, int y) in dirs)
                    {
                        int tX = current.X + x; int tY = current.Y + y;
                        if (tX < 0 || tY < 0 || tX > end.X || tY > end.Y) { continue; }

                        //int risk = multiLevel ? GetRiskLevel(tX, tY) : riskLevels[tX, tY];
                        int risk = riskLevels[tX, tY];
                        if (!seen.TryGetValue((tX, tY), out int cellRisk) || cellRisk > risk + current.Risk)
                        {
                            seen[(tX, tY)] = current.Risk + risk;
                            var newPoint = new Point(tX, tY, current.Risk + risk, current.Steps + 1);
                            newPoint._retrace = current._retrace.Concat(new List<int>() { risk }).ToList();
                            newPoint.points = current.points.Concat(new List<(int, int)>() { (tX, tY) }).ToList();

                            queue.Enqueue(newPoint);
                            // Console.WriteLine($"{tX}, {tY}");
                        }
                    }
                }

                return 0;

            }



            //var current = new Point(0, 0, riskLevels [0][0]);
            //var end = new Point(9, 9, riskLevels [9][9]);
            //var totalRiskMap = new Dictionary<Point, int>();
            //totalRiskMap[current] = 0;

           

            //var apa = totalRiskMap[end];

            base.StopTimerOne();
            FirstSolution(FindPath().ToString());

            //IEnumerable<Point> BuildNeighbours(int x, int y)
            //{
            //    if (x - 1 > 0)
            //        yield return new Point(x - 1, y, riskLevels [x - 1][y]);
            //    if (x + 1 < riskLevels .Length)
            //        yield return new Point(x + 1, y, riskLevels [x + 1][y]);
            //    if (y - 1 > 0)
            //        yield return new Point(x, y - 1, riskLevels [x][y - 1]);
            //    if (y + 1 < riskLevels [0].Length)
            //        yield return new Point(x, y + 1, riskLevels [x][y + 1]);
            //}


        }
    }

    internal record Point(int X, int Y, int Risk, int Steps) : IComparable<Point>
    {
        public List<int> _retrace = new();
        public List<(int, int)> points = new();
        public int CompareTo(Point other)
        {
            int comp = Risk.CompareTo(other.Risk);
            if (comp != 0) { return comp; }

            comp = Steps.CompareTo(other.Steps);
            if (comp != 0) { return comp; }

            return (other.X + other.Y).CompareTo(X + Y);
        }

        public void AddRetrace(int retrace)
        {
            _retrace.Add(retrace);
        }

        public void Print()
        {
            Console.WriteLine("Retrace");
            foreach (var tr in _retrace)
            {
                Console.WriteLine(tr);
            }
            Console.WriteLine("###");


            Console.WriteLine("Retrace points");
            foreach (var tr in points)
            {
                Console.WriteLine(tr);
            }
            Console.WriteLine("###");


            
        }
    }
}
