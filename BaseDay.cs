using System;
using System.IO;

namespace adventofcode2021
{
    public class BaseDay
    {
        public void FirstSolution(string result)
            => Console.WriteLine($"Part 1: {result}");

        public void SecondSolution(string result)
            => Console.WriteLine($"Part 2: {result}");

        public string[] ReadInput(string day)
            => File
                .ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), day, $"{day}.txt"));

    }
}