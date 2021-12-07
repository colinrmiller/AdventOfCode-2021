using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day06
    {
        public static Dictionary<int, UInt64> _lookup;
        // lookup = new Dictionary<int,UInt64>{};
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string text = File.ReadAllText(@$"{path}\input\test.txt");
            string testText = File.ReadAllText(@$"{path}\input\Day06.txt");

            List<int> input = text.Split(",").Select(n => int.Parse(n)).ToList();
            List<int> testInput = testText.Split(",").Select(n => int.Parse(n)).ToList();
            Console.WriteLine($"{input}\n\n{text}");
            // Console.WriteLine($"{input[0]}");

            _lookup = new Dictionary<int, UInt64> { };
            // Console.WriteLine("-------------------");
            // Console.WriteLine("------- Test ------");
            // Console.WriteLine("-------------------");
            // Console.WriteLine($"{Test(testInput, _lookup)}");

            // Console.WriteLine("-------------------");
            // Console.WriteLine("-------Part 1------");
            // Console.WriteLine("-------------------");
            // Console.WriteLine($"{Part1(testInput, _lookup)}");
            // Console.WriteLine($"{Part1(input, _lookup)}");


            // Console.WriteLine("-------------------");
            // Console.WriteLine("-------Part 2------");
            // Console.WriteLine("-------------------");
            Console.WriteLine($"{Part2(testInput, _lookup)}");
            Console.WriteLine($"{Part2(input, _lookup)}");
        }

        public static UInt64 Test(List<int> input, Dictionary<int, UInt64> lookup, int daysLeft = 18)
        {
            UInt64 count = 0;
            int left = daysLeft;
            for (int i = 1; i < 6; i++)
            {
                live(i, left, lookup);
            }

            foreach (int fish in input)
            {
                count += lookup[daysLeft - fish];
            }
            return count;
        }
        public static UInt64 Part1(List<int> input, Dictionary<int, UInt64> lookup)
        {
            UInt64 count = 0;
            int left = 80;

            foreach (int fish in input)
            {
                count += live(fish, left, lookup);
            }
            return count;
        }
        public static UInt64 Part2(List<int> input, Dictionary<int, UInt64> lookup)
        {
            int daysLeft = 256;
            UInt64 count = 0;
            int left = daysLeft;
            for (int i = 1; i < 6; i++)
            {
                live(i, left, lookup);
            }
            Dictionary<int, UInt64> newLookup = lookup.OrderByDescending(kvp => kvp.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (int fish in input)
            {
                count += lookup[daysLeft - fish];
            }
            return count;
        }
        public static UInt64 live(int currentDay, int daysLeft, Dictionary<int, UInt64> lookup)
        {
            if (lookup.ContainsKey(daysLeft - currentDay))
            {
                return lookup[daysLeft - currentDay];
            }
            UInt64 count = 1;
            for (int day = daysLeft - currentDay; day > 0; day -= 7)
            {
                count += live(8, day - 1, lookup);
            }
            lookup.Add(daysLeft - currentDay, count);
            return count;
        }

    }
}