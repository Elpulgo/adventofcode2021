using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode2021.Day12
{
    internal class Day12 : BaseDay
    {

        internal void Execute()
        {
            Console.WriteLine(nameof(Day12));

            var lines = ReadInput(nameof(Day12)).Select(s => s.Split("-")).ToList();

            var start = lines.Where(w => w.Any(a => a == "start")).ToList();
            // var startNode = new Node {
            //     Id = "start",
            //     Connections = start.Where(w => w.Where() !== "start")
            // }
            var end = lines.Where(w => w.Any(a => a == "end")).ToList();


            var sw = Stopwatch.StartNew();



            Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms"); 
            FirstSolution(string.Empty);

            Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms"); 
            SecondSolution(string.Empty);
            sw.Stop();
        }
    }

    internal class Node {
        public string Id {get; set;}
        public List<Node> Connections {get; set; } = new();
    }
}
