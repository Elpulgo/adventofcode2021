using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace adventofcode2021.Day12
{
    internal class Day12 : BaseDay
    {
        private List<string> AllVisits = new List<string>();
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

            Console.WriteLine($"All visists: {AllVisits.Distinct().Count()}");

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
            var canBeVisited = true;
            if(!isPartTwo){
                canBeVisited = visitedBeforeNode?.CanBeVisited(isPartTwo) ?? true;
            }else{

                if(visited.Count(c => c.CanOnlyBeVisitedTwice && c.Visited) > 0 && current.CanOnlyBeVisitedTwice)

                var visitedCount = 0;
                foreach (var node in visited.Where(w => w.Value == current.Value))
                {
                    if(!node.CanBeVisited(isPartTwo))
                        visitedCount++;
                }

                if(visitedCount > 1){
                    canBeVisited = false;
                }
            }

            // Can only visit start ONCE
            if(visited.Any(s => s.Value == "start") && current.Value == "start")
                return 0;

            // If current node occurs in our visited collection and it can't be visited, end this path without finding the end node.
            if(!canBeVisited)
                return 0;

            current.MarkAsVisited();            
            visited.Add(current);
            
            // End node is found, return increment of paths found.
            if(current.Value == ending.Value){
                if(isPartTwo){
                    AllVisits.Add(string.Join(",", visited.Select(s => s.Value)));
                }
                Console.WriteLine(string.Join(",", visited.Select(s => s.Value)));
                return 1;
            }

            var count = 0;


            var adjCanBeVisited = new List<Node>();
            if(!isPartTwo){
                adjCanBeVisited  = current.Adjecents
                    .Where(w =>  !visited
                        .Where(w => !w.CanBeVisited(isPartTwo))
                        .Select(s => s.Value)
                        .Contains(w.Value))
                    .ToList();
            }else{

            foreach (var curAdj in current.Adjecents)
            {
                var innerCount = 0;
                var visitedBefore = visited.Where(w => w.Value == curAdj.Value);
                foreach (var node in visitedBefore)
                {
                    if(!node.CanBeVisited(isPartTwo))
                        innerCount++;
                }

                 if(innerCount < 2){
                  adjCanBeVisited.Add(curAdj);
                 }                
            }
            }

            // Create new visited list to separate different paths so we don't overwrite another path with visited nodes and continue recursively.
            foreach (var adj in adjCanBeVisited)
                count += FindPaths(adj, ending, visited.Select(s => s).ToList(), isPartTwo);

            return count;
        }
    }

    internal class Node {
        private string _value;
        private List<Node> _adjecentNodes = new();
        public bool Visited {get; private set; }
        public string Value => _value;
        public ImmutableList<Node> Adjecents => _adjecentNodes.ToImmutableList();
        public bool CanOnlyBeVistedOnce => _value.All(a => char.IsLower(a));
        public bool CanOnlyBeVisitedTwice => _value != "start" && Value != "end" && _value.All(a => char.IsLower(a));
        public Node(string value)
         => _value = value;

        public void AddAjecent(Node node)
         => _adjecentNodes.Add(node);

        public void MarkAsVisited()
         => Visited = true;

        //  public bool CanBeVisitedPartTwo
        //     => CanOnlyBeVisitedTwice && InternalVisitCount < 1;

        public bool CanBeVisited(bool partTwo)
        {
            if(!partTwo){
                if(CanOnlyBeVistedOnce){
                    return !Visited;
                }
                return true;
            }

            if(Visited && _value == "start" || Visited && _value == "end")
                return false;

            if(!CanOnlyBeVisitedTwice)
                return true;

            return !Visited;            
        }
    }
}