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

            var resultPartOne = FindPaths(
                current: graph.Single(s => s.Value == "start"),
                ending: graph.Single(s => s.Value == "end"),
                visited: new List<Node>(),
                isPartTwo: false);

            Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms"); 
            FirstSolution(resultPartOne.ToString());

            graph = BuildGraph(ReadInput(nameof(Day12)).Select(s => s.Split("-")).ToList());            

            var resultPartTwo = FindPaths(
                current: graph.Single(s => s.Value == "start"),
                ending: graph.Single(s => s.Value == "end"),
                visited: new List<Node>(),
                isPartTwo: true);

            Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms"); 
            SecondSolution(resultPartTwo.ToString());
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

        private int FindPaths(Node current, Node ending, List<Node> visited, bool isPartTwo)
        {
            var visitedBeforeNode = visited.LastOrDefault(s => s.Value == current.Value);
            var canBeVisited = visitedBeforeNode?.CanBeVisited(isPartTwo) ?? true;

            // If current node occurs in our visited collection and it can't be visited, end this path without finding the end node.
            if(!canBeVisited)
                return 0;
            
            // End node is found, return increment of paths found.
            if(current.Value == ending.Value){

            Console.WriteLine(string.Join(",", visited.Select(s => s.Value)));
                return 1;
            }

            current.MarkAsVisited();            
            visited.Add(current);

            var count = 0;

            var adjCanBeVisited  = current.Adjecents
                .Where(w =>  !visited
                    .Where(w => !w.CanBeVisited(isPartTwo))
                    .Select(s => s.Value)
                    .Contains(w.Value))
                .ToList();

            // Create new visited list to separate different paths so we don't overwrite another path with visited nodes and continue recursively.
            foreach (var adj in adjCanBeVisited)
                count += FindPaths(adj, ending, visited.Select(s => s).ToList(), isPartTwo);

            return count;
        }
    }

    internal class Node {
        private string _value;
        private List<Node> _adjecentNodes = new();
        private int InternalVisitCount = 0;
        public string Value => _value;
        public ImmutableList<Node> Adjecents => _adjecentNodes.ToImmutableList();
        public bool CanOnlyBeVistedOnce => _value.All(a => char.IsLower(a));
        public bool CanOnlyBeVisitedTwice => _value != "start" && Value != "end" && _value.All(a => char.IsLower(a));
        public Node(string value)
         => _value = value;

        public void AddAjecent(Node node)
         => _adjecentNodes.Add(node);

        public void MarkAsVisited()
         => InternalVisitCount += 1;

        public bool CanBeVisited(bool partTwo){
            return partTwo
                ? !(CanOnlyBeVisitedTwice && InternalVisitCount > 1)
                : !(CanOnlyBeVistedOnce && InternalVisitCount > 0);
        }
    }
}