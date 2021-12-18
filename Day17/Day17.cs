using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace adventofcode2021.Day16
{
    internal class Day17 : BaseDay
    {
        public Day17(bool shouldPrint) : base(nameof(Day17), shouldPrint) { }

        private string Regex = @"[0-9-]{1,4}";
        
        private int xHigh = 0;
        private int xLow = 0;
        private int yHigh = 0;
        private int yLow = 0;

        public override void Execute()
        {
            var input = ReadInput().First();

            base.StartTimerOne();

            var split = input.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var xHighToLow = new Regex(Regex).Matches(split.First()).Select(s => Convert.ToInt32(s.Value)).OrderByDescending(o => o).ToList();
            xHigh = xHighToLow.First();
            xLow = xHighToLow.Last();

            var yHighToLow = new Regex(Regex).Matches(split.Last()).Select(s => Convert.ToInt32(s.Value)).OrderByDescending(o => o).ToList();
            yHigh = yHighToLow.First();
            yLow = yHighToLow.Last();

            var velocities = Enumerable.Range(1, 100).Where(probableVX => 
                    (probableVX * (probableVX + 1) / 2) > xLow && 
                    (probableVX * (probableVX + 1) / 2) < xHigh)
                .ToList();

            var maxY = 0;

            foreach (var velocity in velocities)
            {
                
                // Brute force, ugly but true!
                for(var y = 0; y < 10000; y++){
                    var position = (X: 0, Y: 0);
                    
                    var velocityX = velocity;
                    var velocityY = y;

                    var probeRoundMaxY = 0;

                    while(!IsWithinTarget(position)){
                        (velocityX, velocityY) = Step(ref position, velocityX, velocityY);

                        if(IsBeyondTarget(position, velocityX)){
                            probeRoundMaxY = 0;
                            break;
                        }

                        if(position.Y > probeRoundMaxY)
                            probeRoundMaxY = position.Y;
                    }

                    if(probeRoundMaxY > maxY)
                        maxY = probeRoundMaxY;
                }
            }          

            base.StopTimerOne();
            FirstSolution(maxY.ToString());
        }

         private int StepsRequired(int velocity, int target){
            int steps = 0;
            while(target >= 0){
                target -= velocity;
                velocity--;
                steps++;
            }

            return steps;
        }

        private bool IsWithinTarget((int X, int Y) position)
        {
            if ((position.X <= xHigh && position.X >= xLow) && 
                (position.Y <= yHigh && position.Y >= yLow))
                return true;

            return false;
        }

        private bool IsBeyondTarget((int X, int Y) position, int velocityX)
        {
            if(position.Y < yLow)
                return true;

            if(position.X > xHigh || (position.X < xLow && velocityX == 0))
                return true;

            return false;
        }

        // The probe's x position increases by its x velocity.
        // The probe's y position increases by its y velocity.
        // Due to drag, the probe's x velocity changes by 1 toward the value 0; that is, it decreases by 1 if it is greater than 0, increases by 1 if it is less than 0, or does not change if it is already 0.
        // Due to gravity, the probe's y velocity decreases by 1.
        private (int velocityX, int velocityY) Step(ref (int X, int Y) position, int velocityX, int velocityY)
        {
            position.X += velocityX;
            position.Y += velocityY;

            velocityX = velocityX switch
            {
                > 0 => velocityX -= 1,
                < 0 => velocityX += 1,
                _ => velocityX
            };

            velocityY--;

            return (velocityX, velocityY);
        }
    }
}
