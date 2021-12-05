using System;
using System.Collections.Generic;
using System.Globalization;
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
            var test = @"
            0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

            // var input = File
            //     .ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day5", "Day5.txt"))
            var input = test.Split("\n", StringSplitOptions.RemoveEmptyEntries)
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
                // .Where(w => w.first.x == w.last.x || w.first.y == w.last.y)  
                // .Select(s =>
                //     s.first.x == s.last.x
                //     ?
                //         s.first.y == new List<int>(){
                //             s.first.y,
                //             s.last.y
                //         }.Min()
                //             ? (s.first, s.last)
                //             : (s.last, s.first)
                //     :
                //         s.first.x == new List<int>() {
                //             s.first.x,
                //             s.last.x
                //         }.Min()
                //         ? (s.first, s.last)
                //         : (s.last, s.first)
                // )
                // .Select(s => (first: s.Item1, last: s.Item2))
                .ToList();

            var coords = new Dictionary<(int, int), int>();

            foreach (var line in input)
            {
                // var positive = line.last.y - line.first.y == line.last.x - line.first.x;
                // var negative = line.last.y - line.first.y == line.first.x - line.last.x;
// 8,0 -> 0,8  /
// 8 - 0 == 0 - 8

var x1 = line.first.x < line.last.x ? line.first.x : line.last.x;
var x2 = line.first.x > line.last.x ? line.first.x : line.last.x;

var y1 = line.first.y < line.last.y ? line.first.y : line.last.y;
var y2 = line.first.y > line.last.y ? line.first.y : line.last.y;

                var positive = y2 - y1 == x2 - x1;
                var negative = y2 - y1 == x1 - x1;

                Console.WriteLine($"{line.first.x}, {line.first.y} -> {line.last.x}, {line.last.y} # pos: {(positive ? "/" : "")} neg: {(negative ? "\\" : "")}");

              
// 8,0 -> 0,8  /
// 6,4 -> 2,0  \

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

                if(line.first.y == line.last.y)
                {
                    for(var x = line.first.x; x <= line.last.x; x++)
                    {
                        if(coords.ContainsKey((x, line.first.y))){
                            coords[(x, line.first.y)]++; 
                            continue;
                        }

                        coords[(x, line.first.y)] = 1;
                    }
                    continue;
                }

                 var (start, end) = line.first.x < line.last.x
                        ? (line.first, line.last)
                        : (line.last, line.first);

                        var direction = start.y < end.y
                        ? 0
                        : 1;
                    
                    int xd = start.x, yd = start.y;

                    // for(var i = 0; i < 20; i++){
                        while((xd, yd) != (end.x, end.y)){
                            
                            coords.AddOrUpdate(xd, yd);
                            if(direction == 1){ // positive
                                xd += 1;
                                yd -= 1;
                            }else{ // negative
                                xd += 1;
                                yd += 1;
                            }
                        }
                    coords.AddOrUpdate(xd, yd);

                        // if((xd, yd) == (end.x, end.y)){
                          
                        //   continue;
                        // } 
                        // coords.AddOrUpdate(xd, yd);
                        // if(direction == 1){ // positive
                        //     xd += 1;
                        //     yd -= 1;
                        // }else{ // negative
                        //     xd += 1;
                        //     yd += 1;
                        // }

                    // } 

            }

            Console.WriteLine($"Part 1: {coords.Where(w => w.Value > 1).Count()}");

            foreach (var a in coords)
            {
                Console.WriteLine($"{a.Key} - {a.Value}");
            }
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
                 var (first, last, opposite, xOrY) = line.first.x == line.last.x
                    ? (line.first.y, line.last.y, line.first.x, 0)
                    : (line.first.x, line.last.x, line.first.y, 1);

                Enumerable.Range(first, last - first + 1)
                    .ToList()
                    .ForEach(c => {
                        (int x, int y) = xOrY == 0 ? (opposite, c) : (c, opposite);
                        coords.AddOrUpdate(x, y);
                    });
            }

            Console.WriteLine($"Part 1: {coords.Count(c => c.Value > 1)}");
        }
    }

    public static class DictionaryExtension{
        public static void AddOrUpdate(this Dictionary<(int, int), int> dictionary, int x, int y){
            if(dictionary.ContainsKey((x, y))){
                dictionary[(x, y)]++; 
                return;
            }

            dictionary[(x, y)] = 1;
        }
    }
}
