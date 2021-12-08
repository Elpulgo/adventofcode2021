using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2021.Day8
{
    internal class Day8 : BaseDay
    {
        internal void Execute()
        {
            Console.WriteLine("Day 8:");
            var input = ReadInput(nameof(Day8));

            var segmentDecoder = new Dictionary<int, char[]>()
            {
                {0, new char[6] { 'a', 'b', 'c', 'e', 'f', 'g' } },
                {1, new char[2] { 'c', 'f' } },
                {2, new char[5] { 'a', 'c', 'd', 'e', 'g' } },
                {3, new char[5] { 'a', 'c', 'd', 'f', 'g' } },
                {4, new char[4] { 'b', 'c', 'd', 'f' } },
                {5, new char[5] { 'a', 'b', 'd', 'f', 'g' } },
                {6, new char[6] { 'a', 'b', 'd', 'e', 'f', 'g' } },
                {7, new char[3] { 'a', 'c', 'f' } },
                {8, new char[7] { 'a', 'b', 'c','d', 'e', 'f', 'g' } },
                {9, new char[6] { 'a', 'b', 'c', 'd', 'f', 'g' } }
            };

            var segementPart1 = segmentDecoder
                .GroupBy(g => g.Value.Length)
                .Where(w => w.Count() == 1)
                .SelectMany(s => s.Select(s => s.Value.Length))
                .ToList();

            var inputPart1 = input.Select(s => s.Split("|", StringSplitOptions.RemoveEmptyEntries).Last())
                .SelectMany(s => s.Split(" ", StringSplitOptions.RemoveEmptyEntries));

            FirstSolution(inputPart1.Count(c => segementPart1.Contains(c.Length)).ToString());

            var inputPart2 = input.Select(s => new Segment(s).Compute()).Sum();

            SecondSolution(inputPart2.ToString());
        }

        public class Segment
        {
            public Segment(string line)
            {
                Patterns = line.Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Digits = line.Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

            public string[] Digits { get; set; }
            public string[] Patterns { get; set; }

            public List<string> FiveTwoThree => Patterns.Where(w => w.Length == 5).ToList();
            public List<string> SixNineZero => Patterns.Where(w => w.Length == 6).ToList();


            public int Compute()
            {
                Dictionary<int, (char[] Chars, int Bit)> segmentDecoder = new();

                var one = Patterns.SingleOrDefault(s => s.Length == 2).ToCharArray();
                var four = Patterns.SingleOrDefault(s => s.Length == 4).ToCharArray();
                var seven = Patterns.SingleOrDefault(s => s.Length == 3).ToCharArray();
                var eight = Patterns.SingleOrDefault(s => s.Length == 7).ToCharArray();

                segmentDecoder.Add(1, (one, one.MapToBitSum()));
                segmentDecoder.Add(4, (four, four.MapToBitSum()));
                segmentDecoder.Add(7, (seven, seven.MapToBitSum()));
                segmentDecoder.Add(8, (eight, eight.MapToBitSum()));

                foreach (var item in FiveTwoThree)
                {
                    if(!segmentDecoder[1].Chars.Except(item).Any()){
                        segmentDecoder.Add(3, (item.ToCharArray(), item.ToCharArray().MapToBitSum()));
                        continue;
                    }

                    if(segmentDecoder[4].Chars.Except(item).Count() > 1){
                        segmentDecoder.Add(2, (item.ToCharArray(), item.ToCharArray().MapToBitSum()));
                        continue;
                    } 

                    segmentDecoder.Add(5, (item.ToCharArray(), item.ToCharArray().MapToBitSum()));
                }

                foreach (var item in SixNineZero)
                {
                    if(segmentDecoder[7].Chars.Except(item).Count() == 1){
                        segmentDecoder.Add(6, (item.ToCharArray(), item.ToCharArray().MapToBitSum()));
                        continue;
                    }

                    if(!segmentDecoder[4].Chars.Except(item).Any()){
                        segmentDecoder.Add(9, (item.ToCharArray(), item.ToCharArray().MapToBitSum()));
                        continue;
                    } 

                    segmentDecoder.Add(0, (item.ToCharArray(), item.ToCharArray().MapToBitSum()));
                }
              
                var decodedDigits = new List<int>();

                foreach (var digitSum in Digits.Select(s => s.ToCharArray().MapToBitSum()))
                {
                    decodedDigits.Add(segmentDecoder.Single(s => s.Value.Bit == digitSum).Key);
                }

                return Convert.ToInt32(string.Join("", decodedDigits.Select(s => s)));
            }
        }

    }
    internal static class BitHelper{
        internal static int MapToBitSum(this char[] array){
            return array.Select(s => Bits[s]).Sum();
        }

        internal static Dictionary<char, int> Bits = new Dictionary<char, int>()
        {
            { 'd', 1},
            { 'e', 2},
            { 'a', 4},
            { 'f', 8},
            { 'g', 16},
            { 'b', 32},
            { 'c', 64}
        };
    }
}
