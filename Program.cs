using System;
using adventofcode2021.Day11;
using adventofcode2021.Day12;
using adventofcode2021.Day2;
using adventofcode2021.Day8;
using adventofcode2021.Helpers;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent for you!");
            var day = new Day12();
            day.Execute();

            var pq = new PriorityQueue<Apa>();
            pq.Enqueue(new Apa(0, 1));
            pq.Enqueue(new Apa(5, 2));
            pq.Enqueue(new Apa(2, 1));
            pq.Enqueue(new Apa(0, 1));
            pq.Enqueue(new Apa(3, 4));
            pq.Enqueue(new Apa(3, 1));

            while(pq.HasValue){
                var myApa = pq.Dequeue();
                Console.WriteLine($"{myApa.x} - {myApa.y}");
            }

        }


    }

    public record Apa(int x, int y) : IComparable<Apa>
    {
        
        public int CompareTo(Apa other)
        {
            return (other.x + other.y).CompareTo(x + y);
        }
    }
}
