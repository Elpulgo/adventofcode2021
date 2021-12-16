using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2021.Day16
{
    internal class Day16 : BaseDay
    {
        public Day16(bool shouldPrint) : base(nameof(Day16), shouldPrint) { }

        public override void Execute()
        {

            var binaryInput = String.Join(
                "",
                string.Join("", ReadInput()).Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            base.StartTimerOne();
            var packets = new List<string>();

            var packetVersions = new List<int>();

            while (!string.IsNullOrEmpty(binaryInput))
            {
                if (binaryInput.Length < 6)
                    break;

                var version = binaryInput.Substring(0, 3);
                var type = binaryInput.Substring(3, 6);

                packetVersions.Add(Convert.ToInt32(version, 2));

                var packetBits = new List<string>();

                binaryInput = binaryInput[6..];

                while (true)
                {
                    var newBits = binaryInput.Substring(0, 5);
                    packetBits.Add(newBits.Substring(1, 4));

                    binaryInput = binaryInput[5..];

                    if (newBits.StartsWith("0"))
                    {
                        // Remove trailing zeros
                        binaryInput = binaryInput[3..];
                        break;
                    }

                }
            }

            base.StopTimerOne();
            FirstSolution(packetVersions.Sum().ToString());
        }
    }
}
