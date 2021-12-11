using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace adventofcode2021.Day11
{
    internal class Day11 : BaseDay
    {
        internal void Execute()
        {
            var lines = ReadInput(nameof(Day11)).Select(s => s.ToCharArray().Select(s => Convert.ToInt32(s - '0')).ToList()).ToList();
            
            var matrix = new Dictionary<(int X, int Y), int>();
            
            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Count; x++)
                {
                    matrix.Add((x, y), lines[y][x]);
                }
            }

            var totalFlash = 0;

            while(true){
                matrix.ToList().ForEach(f => matrix[f.Key]++);

                var anyLeftToFlash = true;
                while(anyLeftToFlash){
                    var coordsToFlash = matrix.Where(w => w.Value == 9).ToList();
                    totalFlash += coordsToFlash.Count;
                    // Find adjecent here

                }


                // Reset all which have 9 to 0 for this step
                matrix.Where(w => w.Value == 9).ToList().ForEach(f => matrix[f.Key] = 0);
            }

            
            FirstSolution(string.Empty);
            SecondSolution(string.Empty);
        }

        
    }
}
