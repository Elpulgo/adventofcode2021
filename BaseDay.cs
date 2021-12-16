using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace adventofcode2021
{
    public abstract class BaseDay
    {

        public BaseDay(string day = "", bool shouldPrint = true)
        {
            _shouldPrint = shouldPrint;
            _day = day;
        }

        private string _day;

        private string _resultPartOne;
        private string _resultPartTwo;

        private bool _shouldPrint = true;

        private Stopwatch _stopwatchPartOne = new Stopwatch();
        private Stopwatch _stopwatchPartTwo = new Stopwatch();

        public double TimePartOne => _stopwatchPartOne.Elapsed.TotalSeconds;
        public double TimePartTwo => _stopwatchPartTwo.Elapsed.TotalSeconds;

        public void FirstSolution(string result)
        {
            if (_shouldPrint)
            {
                Console.WriteLine($"{_day} - Part 1: {result} | ({TimePartOne} s)");
                return;
            }

            _resultPartOne = result;
        }

        public void SecondSolution(string result)
        {
            if (_shouldPrint)
            {
                Console.WriteLine($"{_day} - Part 2: {result} | ({TimePartTwo} s)");
                return;
            }

            _resultPartTwo = result;
        }

        public string[] ReadInput()
            => File
                .ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), _day, $"{_day}.txt"));

        public string ReadDescription(string day)
            => File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), _day, $"{_day}.md")).First().Replace("-", string.Empty).Trim();

        public void StartTimerOne() => _stopwatchPartOne.Start();
        public void StopTimerOne() => _stopwatchPartOne.Stop();
        public void StartTimerTwo() => _stopwatchPartTwo.Start();
        public void StopTimerTwo() => _stopwatchPartTwo.Stop();

        public abstract void Execute();

        public void Print()
        {
            Console.WriteLine($"{ReadDescription(_day)}\t\t | {_resultPartOne} ({TimePartOne} s) | {_resultPartTwo} ({TimePartTwo} s)");
        }
    }
}
