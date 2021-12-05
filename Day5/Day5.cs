using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent.Day5
{
    internal class Day5
    {
        internal void Execute()
        {
            Console.WriteLine("Day 5:");
Part1();
//             var test = @"
//             0,9 -> 5,9
// 8,0 -> 0,8
// 9,4 -> 3,4
// 2,2 -> 2,1
// 7,0 -> 7,4
// 6,4 -> 2,0
// 0,9 -> 2,9
// 3,4 -> 1,4
// 0,0 -> 8,8
// 5,5 -> 8,2";

//             // var input = File
//             //     .ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day5", "Day5.txt"))
//             var input = test.Split("\n", StringSplitOptions.RemoveEmptyEntries)
//                 .ToList()
//                 .Select(line =>
//                 {
//                     var coordinates = line.Split("->", StringSplitOptions.RemoveEmptyEntries);

//                     var firstCoords = coordinates.First().Split(",", StringSplitOptions.RemoveEmptyEntries);
//                     var lastCoords = coordinates.Last().Split(",", StringSplitOptions.RemoveEmptyEntries);

//                     var first = (
//                         x: Convert.ToInt32(firstCoords.First().Trim()),
//                         y: Convert.ToInt32(firstCoords.Last().Trim())
//                     );

//                     var last = (
//                         x: Convert.ToInt32(lastCoords.First().Trim()),
//                         y: Convert.ToInt32(lastCoords.Last().Trim())
//                     );

//                     return (first, last);
//                 }
//                 )
//                 // .Where(w => w.first.x == w.last.x || w.first.y == w.last.y)  
//                 .Select(s =>
//                     s.first.x == s.last.x
//                     ?
//                         s.first.y == new List<int>(){
//                             s.first.y,
//                             s.last.y
//                         }.Min()
//                             ? (s.first, s.last)
//                             : (s.last, s.first)
//                     :
//                         s.first.x == new List<int>() {
//                             s.first.x,
//                             s.last.x
//                         }.Min()
//                         ? (s.first, s.last)
//                         : (s.last, s.first)
//                 )
//                 .Select(s => (first: s.Item1, last: s.Item2))
//                 .ToList();

//             var coords = new Dictionary<(int, int), int>();

//             foreach (var line in input)
//             {
//                 if (line.first.x == line.last.x)
//                 {
//                     for(var y = line.first.y; y <= line.last.y; y++)
//                     {
//                         if(coords.ContainsKey((line.first.x, y))){
//                             coords[(line.first.x, y)]++;
//                             continue;
//                         }

//                         coords[(line.first.x, y)] = 1;
//                     }

//                     continue;
//                 }

//                 for(var x = line.first.x; x <= line.last.x; x++)
//                 {
//                     if(coords.ContainsKey((x, line.first.y))){
//                         coords[(x, line.first.y)]++; 
//                         continue;
//                     }

//                     coords[(x, line.first.y)] = 1;
//                 }
//             }

//             bool IsLinear((int x, int y) first, (int x, int y) last)
//                 => first.x == last.x || first.y == last.y;
            

//             Console.WriteLine($"Part 1: {coords.Where(w => w.Value > 1).Count()}");
        }

        private void Part1(){
            var input = File
                .ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day5", "Day5.txt"))
                .ToList()
                .Select(line =>
                {
                    var coordinates = line.Split("->", StringSplitOptions.RemoveEmptyEntries);

                    var firstCoords = coordinates.First().Split(",", StringSplitOptions.RemoveEmptyEntries);
                    var lastCoords = coordinates.Last().Split(",", StringSplitOptions.RemoveEmptyEntries);

                    var first = (
                        x: Convert.ToInt32(firstCoords.First().Trim()),
                        y: Convert.ToInt32(firstCoords.Last().Trim())
                    );

                    var last = (
                        x: Convert.ToInt32(lastCoords.First().Trim()),
                        y: Convert.ToInt32(lastCoords.Last().Trim())
                    );

                    return (first, last);
                }
                )
                .Where(w => w.first.x == w.last.x || w.first.y == w.last.y)
                .Select(s =>
                    s.first.x == s.last.x
                    ?
                        s.first.y == new List<int>(){
                            s.first.y,
                            s.last.y
                        }.Min()
                            ? (s.first, s.last)
                            : (s.last, s.first)
                    :
                        s.first.x == new List<int>() {
                            s.first.x,
                            s.last.x
                        }.Min()
                        ? (s.first, s.last)
                        : (s.last, s.first)
                )
                .Select(s => (first: s.Item1, last: s.Item2))
                .ToList();

            var coords = new Dictionary<(int, int), int>();

            foreach (var line in input)
            {
                if (line.first.x == line.last.x)
                {
                    for(var y = line.first.y; y <= line.last.y; y++)
                    {
                        if(coords.ContainsKey((line.first.x, y))){
                            coords[(line.first.x, y)]++;
                            continue;
                        }

                        coords[(line.first.x, y)] = 1;
                    }

                    continue;
                }

                for(var x = line.first.x; x <= line.last.x; x++)
                {
                    if(coords.ContainsKey((x, line.first.y))){
                        coords[(x, line.first.y)]++; 
                        continue;
                    }

                    coords[(x, line.first.y)] = 1;
                }
            }

            Console.WriteLine($"Part 1: {coords.Where(w => w.Value > 1).Count()}");
        }
    }
}
