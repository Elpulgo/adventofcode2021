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


            //var segmentDecoderPart2 = new Dictionary<int, char[]>()
            //{
            //    {0, new char[6] { 'c', 'a', 'g', 'e', 'd', 'b' } },
            //    {1, new char[2] { 'a', 'b' } },
            //    {2, new char[5] { 'g', 'c', 'd', 'f', 'a' } },
            //    {3, new char[5] { 'f', 'b', 'c', 'a', 'd' } },
            //    {4, new char[4] { 'e', 'a', 'f', 'b' } },
            //    {5, new char[5] { 'c', 'd', 'f', 'b', 'e' } },
            //    {6, new char[6] { 'c', 'd', 'f', 'g', 'e', 'b' } },
            //    {7, new char[3] { 'd', 'a', 'b' } },
            //    {8, new char[7] { 'a', 'c', 'e', 'd', 'g', 'f', 'b' } },
            //    {9, new char[6] { 'c', 'e', 'f', 'a', 'b', 'd' } }
            //};

            var charValueDic = new Dictionary<char, int>()
            {
                { 'd', 1},
                { 'e', 2},
                { 'a', 4},
                { 'f', 8},
                { 'g', 16},
                { 'b', 32},
                { 'c', 64}
            };

            var numericValueDic = new Dictionary<int, int>()
            {
                { 1 + 2 + 4 + 16 + 32 + 64, 0 }, // d + e + a + g + b + c
                { 4 + 32, 1 }, // a + b
                { 1 + 4 + 8 + 16 + 64, 2 }, // d + a + f + g + c
                { 1 + 4 + 8 + 32 + 64, 3 }, // d + a + f + b + c
                { 2 + 4 + 8 + 32, 4 }, // e + a + f + b
                { 1 + 2 + 8 + 32 + 64, 5 }, // d + e + f + b + c
                { 1 + 2 + 8 + 16 + 32 + 64, 6 }, // d + e + f + g + b + c
                { 1 + 4 + 32, 7 }, // d + a + b
                { 1 + 2 + 4 + 8 + 16 + 32 + 64, 8 }, // d + e + a + f + g + b + c
                { 1 + 2 + 4 + 8 + 32 + 64, 9 }, // d + e + a + f + b + c
            };

            var inputPart2 = input.Select(s => new Segment(s).Compute(numericValueDic, charValueDic)).Sum();

            SecondSolution(inputPart2.ToString());
        }

        public class Segment
        {
            public Segment(string line)
            {
                Patterns = line.Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Digits = line.Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

            public string[] Patterns { get; set; }
            public string[] Digits { get; set; }

            public int Compute(Dictionary<int, int> sumToDigitMap, Dictionary<char, int> charToIntMap)
            {
                Dictionary<int, char[]> segmentDecoder = new();
                sumToDigitMap.Clear();

                sumToDigitMap.Add(Patterns.SingleOrDefault(s => s.Length == 2).Select(s => charToIntMap[s]).Sum(), 1);
                sumToDigitMap.Add(Patterns.SingleOrDefault(s => s.Length == 4).Select(s => charToIntMap[s]).Sum(), 4);
                sumToDigitMap.Add(Patterns.SingleOrDefault(s => s.Length == 3).Select(s => charToIntMap[s]).Sum(), 7);
                sumToDigitMap.Add(Patterns.SingleOrDefault(s => s.Length == 7).Select(s => charToIntMap[s]).Sum(), 8);
                // 6 chars -> 0, 9, 6

                // den som inte finns i 9 finns i 0 & 6 -> finns också i 2
                // den som inte finns i 0 finns i 6 & 9 -> finns också i 8
                // den som inte finns i 6 finns i 0 & 9 -> finns också i 7

                // 2an finns i ettan

                //5 except 2 = 4
                //5 except 3 = 2

                //2 except 3 = 2
                //2 except 5 = 4

                //3 except 2 = 2
                //3 except 5 = 2
                //   dddd
                //  e    a
                //  e    a
                //   ffff
                //  g    b
                //  g    b
                //   cccc

                var patternsWith6Chars = Patterns.Where(w => w.Length == 6).ToList();

                var attempt1 = patternsWith6Chars[0].Except(patternsWith6Chars[1]).ToList(); // 1
                var attempt2 = patternsWith6Chars[0].Except(patternsWith6Chars[2]).ToList(); // 2

                var isNumber6Attempt1 = Patterns.SingleOrDefault(s => s.Length == 3).Contains(attempt1.Single());
                var isNumber6Attempt2 = Patterns.SingleOrDefault(s => s.Length == 3).Contains(attempt2.Single());
                if(isNumber6Attempt1 || isNumber6Attempt2)
                {
                    // patternsWith6Chars[0] Is number 6
                }

                var attempt3 = patternsWith6Chars[1].Except(patternsWith6Chars[2]).ToList(); // 1
                var attempt4 = patternsWith6Chars[1].Except(patternsWith6Chars[0]).ToList(); // 1

                var attempt5 = patternsWith6Chars[2].Except(patternsWith6Chars[0]).ToList(); // 2
                var attempt6 = patternsWith6Chars[2].Except(patternsWith6Chars[1]).ToList(); // 1


                // 5 chars -> 2, 3, 5

                // 2an finns i ettan

                //5 except 2 = 4
                //5 except 3 = 2

                //2 except 3 = 2
                //2 except 5 = 4

                //3 except 2 = 2
                //3 except 5 = 2
                //   dddd
                //  e    a
                //  e    a
                //   ffff
                //  g    b
                //  g    b
                //   cccc
                var patternsWith5Chars = Patterns.Where(w => w.Length == 5).ToList();

                var resultAttempt1 = patternsWith5Chars[0].Except(patternsWith5Chars[1]).ToList(); // 1
                var resultAttempt2 = patternsWith5Chars[0].Except(patternsWith5Chars[2]).ToList(); // 2

                var resultAttempt3 = patternsWith5Chars[1].Except(patternsWith5Chars[2]).ToList(); // 1
                var resultAttempt4 = patternsWith5Chars[1].Except(patternsWith5Chars[0]).ToList(); // 1

                var resultAttempt5 = patternsWith5Chars[2].Except(patternsWith5Chars[0]).ToList(); // 2
                var resultAttempt6 = patternsWith5Chars[2].Except(patternsWith5Chars[1]).ToList(); // 1

                if (resultAttempt1.Count + resultAttempt2.Count == 2)
                {
                    // Number 3
                    sumToDigitMap.Add(patternsWith5Chars[0].Select(s => charToIntMap[s]).Sum(), 3);
                    patternsWith5Chars.RemoveAt(0);
                }
                else if (resultAttempt3.Count + resultAttempt4.Count == 2)
                {
                    // Number 3
                    sumToDigitMap.Add(patternsWith5Chars[1].Select(s => charToIntMap[s]).Sum(), 3);
                    patternsWith5Chars.RemoveAt(1);
                }
                else if (resultAttempt4.Count + resultAttempt5.Count == 2)
                {
                    // Number 3
                    sumToDigitMap.Add(patternsWith5Chars[2].Select(s => charToIntMap[s]).Sum(), 3);
                    patternsWith5Chars.RemoveAt(2);
                }

                var no5or2 = patternsWith5Chars[0].Except(patternsWith5Chars[1]).ToList();

                for (var i = 0; i < no5or2.Count; i++)
                {
                    if (Patterns.SingleOrDefault(s => s.Length == 2).Contains(no5or2[i]))
                    {
                        // It's number 2 since 1 overlaps with 2
                        if (patternsWith5Chars.First().Contains(no5or2[i]))
                        {
                            sumToDigitMap.Add(patternsWith5Chars.First().Select(s => charToIntMap[s]).Sum(), 2);
                            sumToDigitMap.Add(patternsWith5Chars.Last().Select(s => charToIntMap[s]).Sum(), 5);
                        }
                        else
                        {
                            // It's number 5 since 1 doesn't overlap 5
                            sumToDigitMap.Add(patternsWith5Chars.First().Select(s => charToIntMap[s]).Sum(), 5);
                            sumToDigitMap.Add(patternsWith5Chars.Last().Select(s => charToIntMap[s]).Sum(), 2);
                        }
                        break;
                    }
                }

              

                foreach (var pattern in Patterns)
                {
                    var chars = pattern.ToCharArray();
                    var number = 0;

                    foreach (var c in chars)
                    {
                        number += charToIntMap[c];
                    }

                    if (!sumToDigitMap.TryGetValue(number, out var digit))
                        continue;

                    segmentDecoder[digit] = pattern.ToCharArray();
                }

                var decodedDigits = new List<int>();

                foreach (var digit in Digits.Select(s => s.ToCharArray()))
                {
                    var digitSum = digit.Select(s => charToIntMap[s]).Sum();
                    if(sumToDigitMap.TryGetValue(digitSum, out var decoded))
                    {
                        decodedDigits.Add(decoded);
                    }
                }

                return Convert.ToInt32(string.Join("", decodedDigits.Select(s => s)));
            }
        }
    }
}
