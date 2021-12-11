using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace adventofcode2021.Day11
{
    internal class Day11 : BaseDay
    {
        private Dictionary<(int X, int Y), Coordinate> _matrix = new();

        internal void Execute()
        {
            var lines = ReadInput(nameof(Day11)).Select(s => s.ToCharArray().Select(s => Convert.ToInt32(s - '0')).ToList()).ToList();
            
            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Count; x++)
                {
                    _matrix.Add((x, y), new Coordinate { Y = y, X = x, CurrentValue =  lines[y][x]});
                }
            }

            // Setup all neighbours
            _matrix.ToList().ForEach(f => f.Value.Neighbours = GetNeighbours(f.Key).Select(s => _matrix[s]).ToList());

            var totalFlash = 0;

            var steps = 0;
            while(steps < 100){
               _matrix.ToList().ForEach(f => {
                    totalFlash += f.Value.Increment(f => GetNeighbours(f), _matrix);
                });


                steps++;
                // var anyLeftToFlash = true;
                // while(anyLeftToFlash){
                //     var coordsToFlash = _matrix.Where(w => w.Value == 9).ToList();
                //     totalFlash += coordsToFlash.Count;
                //     // Find adjecent here

                // }


                // Reset all which have 9 to 0 for this step
                // _matrix.Where(w => w.Value == 9).ToList().ForEach(f => matrix[f.Key] = 0);
            }

            
            FirstSolution(totalFlash.ToString());
            // SecondSolution(string.Empty);
        }

        private IEnumerable<(int X, int Y)> GetNeighbours((int X, int Y) coord){
            
            // Top
            if(_matrix.ContainsKey((coord.X, coord.Y - 1)))
                yield return (coord.X, coord.Y - 1);
            
            // Bottom
            if(_matrix.ContainsKey((coord.X, coord.Y + 1)))
                yield return (coord.X, coord.Y + 1);
            
            // Left
            if(_matrix.ContainsKey((coord.X - 1, coord.Y)))
                yield return (coord.X - 1, coord.Y);
            
            // Right
            if(_matrix.ContainsKey((coord.X + 1, coord.Y)))
                yield return (coord.X + 1, coord.Y);
                        
            // Diagonal Left Top
            if(_matrix.ContainsKey((coord.X - 1, coord.Y - 1)))
                yield return (coord.X - 1, coord.Y - 1);
            
            // Diagonal Right Top
            if(_matrix.ContainsKey((coord.X + 1, coord.Y - 1)))
                yield return (coord.X + 1, coord.Y - 1);

            // Diagonal Left Bottom
            if(_matrix.ContainsKey((coord.X - 1, coord.Y + 1)))
                yield return (coord.X - 1, coord.Y + 1);

            // Diagonal Right Bottom
            if(_matrix.ContainsKey((coord.X + 1, coord.Y + 1)))
                yield return (coord.X + 1, coord.Y + 1);
        }
    }

    internal class Coordinate {
        public int CurrentValue = 0;
        public List<Coordinate> Neighbours = new();

        public int X {get; set;}
        public int Y {get; set;}

        public int Increment(Func<(int, int), IEnumerable<(int, int)>> neighbourFunc, Dictionary<(int X, int Y), Coordinate> matrix){
            if(CurrentValue != 0){
                if(CurrentValue == 9){
                    CurrentValue = 0;
                    return 0;
                }
                CurrentValue++;
                return 0;
            }

            if(CurrentValue == 0){
                var scopedCount = 1;
                neighbourFunc((X, Y)).ToList().ForEach(n => scopedCount += matrix[(n)].Increment(neighbourFunc, matrix));
                CurrentValue = 0;
                CurrentValue++;

                return scopedCount;
            }

            if(CurrentValue == 9){
                    CurrentValue = 0;
                    return 0;
            }
            CurrentValue++;

            return 0;

        }
    }
}
