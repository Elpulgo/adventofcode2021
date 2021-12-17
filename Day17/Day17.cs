using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode2021.Day16
{
    internal class Day17 : BaseDay
    {
        public Day17(bool shouldPrint) : base(nameof(Day17), shouldPrint) { }

        private string Regex = @"[0-9-]{1,3}";
        
        private int xHigh = 0;
        private int xLow = 0;

        private int yHigh = 0;
        private int yLow = 0;
        public override void Execute()
        {
            var input = ReadInput().First();
            var split = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var xHighToLow = new Regex(Regex).Matches(split.First()).Select(s => Convert.ToInt32(s.Value)).OrderByDescending(o => o).ToList();
            xHigh = xHighToLow.First();
            xLow = xHighToLow.Last();

            var yHighToLow = new Regex(Regex).Matches(split.Last()).Select(s => Convert.ToInt32(s.Value)).OrderByDescending(o => o).ToList();
            yHigh = yHighToLow.First();
            yLow = yHighToLow.Last();

            var position = (0, 0);
            var steps = 0;
            var xMove = 7;
            var yMove = 2;

            var stepsyHigh = 0 - yHigh;
            var stepsyLow = 0 - yLow;
            var stepsXLow = xLow / xMove;
            var stepsXGhigh = xHigh / xMove;



            while (!IsWithinTarget(position))
            {
                (xMove, yMove) = Step(ref position, xMove, yMove);
                steps++;
            }

            // 0 - yHigh - x == How many steps will it need till we het yHigh?
            // 0 - yLow - x == How many steps will it need till we het yLow?


            // The probe's x position increases by its x velocity.
            // The probe's y position increases by its y velocity.
            // Due to drag, the probe's x velocity changes by 1 toward the value 0; that is, it decreases by 1 if it is greater than 0, increases by 1 if it is less than 0, or does not change if it is already 0.
            // Due to gravity, the probe's y velocity decreases by 1.
        }

        private bool IsWithinTarget((int X, int Y) position)
        {
            if ((position.X <= xHigh && position.X >= xLow) && (position.Y <= yHigh && position.Y >= yLow))
                return true;

            return false;
        }

        private (int xMove, int yMove) Step(ref (int X, int Y) position, int xMove, int yMove)
        {
            position.X += xMove;
            position.Y += yMove;

            xMove = xMove switch
            {
                > 0 => xMove -= 1,
                < 0 => xMove += 1,
                _ => xMove
            };

            yMove--;

            return (xMove, yMove);
        }
    }
}
