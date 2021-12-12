using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace adventofcode2021.Day12
{
    internal class Day12 : BaseDay
    {
        internal void Execute()
        {
            Console.WriteLine(nameof(Day12));
            var sw = Stopwatch.StartNew();

            var graph = BuildGraph(ReadInput(nameof(Day12)).Select(s => s.Split("-")).ToList());            

            var result = FindPaths(
                current: graph.Single(s => s.Value == "start"),
                ending: graph.Single(s => s.Value == "end"),
                visited: new List<Node>());

            Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms"); 
            FirstSolution(result.ToString());

            // Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms"); 
            // SecondSolution(string.Empty);
            sw.Stop();
        }

        private List<Node> BuildGraph(List<string[]> lines){
            var graph = new List<Node>();

            lines
                .SelectMany(s => s)
                .Distinct()
                .ToList()
                .ForEach(f => graph.Add(new Node(f)));

            lines.ForEach(line => {
                var firstNode = graph
                    .Single(s => s.Value == line.First());
                var lastNode = graph
                    .Single(s => s.Value == line.Last());

                firstNode.AddAjecent(lastNode);
                lastNode.AddAjecent(firstNode);
            });

            return graph;
        }

        private int FindPaths(Node current, Node ending, List<Node> visited)
        {
            var canBeVisited = visited.LastOrDefault(s => s.Value == current.Value)?.CanBeVisited;

            // If current node occurs in our visited collection and it can't be visited, end this path without finding the end node.
            if(canBeVisited.HasValue && canBeVisited.Value == false)
                return 0;
            
            // End node is found, return increment of paths found.
            // Console.WriteLine(string.Join(",", visited.Select(s => s.Value)));
            if(current.Value == ending.Value)
                return 1;

            current.MarkAsVisited();            
            visited.Add(current);

            var count = 0;

            var adjCanBeVisited  = current.Adjecents
                .Where(w =>  !visited
                    .Where(w => !w.CanBeVisited)
                    .Select(s => s.Value)
                    .Contains(w.Value))
                .ToList();

            // Create new visited list to separate different paths so we don't overwrite another path with visited nodes and continue recursively.
            foreach (var adj in adjCanBeVisited)
                count += FindPaths(adj, ending, visited.Select(s => s).ToList());

            return count;
        }
    }

    internal class Node {
        private string _value;
        private List<Node> _adjecentNodes = new();
        public bool Visited { get; private set; }
        public string Value => _value;
        public ImmutableList<Node> Adjecents => _adjecentNodes.ToImmutableList();
        public bool CanOnlyBeVistedOnce => _value.All(a => char.IsLower(a));
        public Node(string value)
         => _value = value;

        public void AddAjecent(Node node)
         => _adjecentNodes.Add(node);

        public void MarkAsVisited()
         => Visited = true;

        public bool CanBeVisited => !(CanOnlyBeVistedOnce && Visited);
    }
}