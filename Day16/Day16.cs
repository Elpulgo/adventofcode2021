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

            //PartOne(binaryInput);
            PartTwo(binaryInput);           
        }

        private void PartTwo(string binaryInput)
        {
            // TEST
            binaryInput = String.Join(
                "",
                string.Join("", "38006F45291200").Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            base.StartTimerTwo();
            var packetVersions = new List<int>();

            while (!string.IsNullOrEmpty(binaryInput))
            {
                if (binaryInput.Length < 6)
                    break;


                var packet = new Packet {
                    Version = Convert.ToInt32(binaryInput.Substring(0, 3), 2),
                    PacketType = Convert.ToInt32(binaryInput.Substring(3, 6), 2)
                };

                if (packet.IsOperator)
                {
                    packet.LengthTypeId = Convert.ToInt32(binaryInput.Substring(6, 1), 2);

                    if(packet.LengthTypeId == 0)
                    {
                        // Should be 27
                        var lengthOfSubPackets = Convert.ToInt32(binaryInput.Substring(7, 15), 2);

                        // Then first 11, then 16?
                        var apa = Convert.ToInt32("1010001010", 2);
                        //var apa3 = 0b + apa;
                        //var nisse = "0b11010001010";

                        // while loop here -> starting 1 means continue, starting 0 means last package
                        var nisse = binaryInput.Substring(23, 11);
                        var subPacketOne = Convert.ToInt32(binaryInput.Substring(22, 11), 2); // 10
                        var subPacketTwo = Convert.ToInt32(binaryInput.Substring(33, 16), 2); // 20


                    }
                }

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

            base.StopTimerTwo();
            SecondSolution(string.Empty);
        }

        private void PartOne(string binaryInput)
        {
            base.StartTimerOne();
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

        public class Packet
        {
            public List<int> Bits { get; set; } = new();
            public int PacketType { get; set; }
            public int Version { get; set; }

            public bool IsOperator => PacketType != 4;
            public bool IsLiteral => PacketType == 4;

            public int LengthTypeId { get; set; }
        }
    }
}
