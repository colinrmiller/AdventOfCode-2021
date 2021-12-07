using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day07
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string testText = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day07.txt");

            List<int> input = text.Split(",").Select(n => int.Parse(n)).ToList();
            List<int> testInput = testText.Split(",").Select(n => int.Parse(n)).ToList();
            Console.WriteLine($"{input}\n\n{text}");
            // Console.WriteLine($"{input[0]}");

            // Console.WriteLine("-------------------");
            // Console.WriteLine("------- Test ------");
            // Console.WriteLine("-------------------");
            Console.WriteLine($"{Test(testInput)}");

            // Console.WriteLine("-------------------");
            // Console.WriteLine("-------Part 1------");
            // Console.WriteLine("-------------------");
            // Console.WriteLine($"{Part1(testInput)}");
            Console.WriteLine($"{Part1(input)}");


            // Console.WriteLine("-------------------");
            // Console.WriteLine("-------Part 2------");
            // Console.WriteLine("-------------------");
            // Console.WriteLine($"{Part2(testInput)}");
            Console.WriteLine($"{Part2(input)}");
        }

        public static int Test(List<int> input)
        {
            input = input.OrderBy(x => x).ToList();

            int minIndex = 0;
            int maxIndex = input.Max();
            int index = (minIndex + maxIndex) / 2;

            List<int> pos = calcPos(input, index);
            while (pos[1] > pos[0] || pos[1] > pos[2])
            {
                if (pos[1] > pos[0])
                {
                    maxIndex = index;
                }
                else
                {
                    minIndex = index;
                }
                index = (maxIndex + minIndex) / 2;
                pos = calcPos(input, index);
            }

            return pos[1];
        }
        public static int Part1(List<int> input)
        {
            List<int> orderdInput = input.OrderBy(x => x).ToList();

            int halfIndex = input.Count() / 2;
            int target = (input.Count % 2 != 0) ? input[halfIndex] : difficultMedian(orderdInput);

            int dist = input.Aggregate(0, (acc, elm) => acc + Math.Abs(target - elm));
            return dist;
        }
        // public static UInt64            int halfIndex = input.Count() / 2;
        public static int Part2(List<int> input)

        {
            input = input.OrderBy(x => x).ToList();

            int minIndex = 0;
            int maxIndex = input.Max();
            int index = (minIndex + maxIndex) / 2;

            List<int> pos = calcPos(input, index);
            while (pos[1] > pos[0] || pos[1] > pos[2])
            {
                if (pos[1] > pos[0])
                {
                    maxIndex = index;
                }
                else
                {
                    minIndex = index;
                }
                index = (maxIndex + minIndex) / 2;
                pos = calcPos(input, index);
            }

            return pos[1];
        }
        public static int difficultMedian(List<int> input)
        {
            int halfIndex = input.Count() / 2;
            int highMed = input[halfIndex];
            int lowMed = input[halfIndex - 1];

            int numHigh = input.Where(x => x == highMed).Count();
            int numLow = input.Where(x => x == lowMed).Count();

            if (numLow > numHigh)
            {
                return lowMed;
            }
            else return highMed;
        }

        public static List<int> calcPos(List<int> input, int pos)
        {
            int distNext = input.Aggregate(0, (acc, elm) => acc + triSum(Math.Abs(pos + 1 - elm)));
            int dist = input.Aggregate(0, (acc, elm) => acc + triSum(Math.Abs(pos - elm)));
            int distPrev = input.Aggregate(0, (acc, elm) => acc + triSum(Math.Abs(pos - 1 - elm)));
            return new List<int> { distPrev, dist, distNext };
        }
        public static int triSum(int num)
        {
            return ((int)Math.Pow(num, 2) + num) / 2;
        }
    }
}