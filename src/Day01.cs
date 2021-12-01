using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;

namespace advent_2016
{
    class Day01
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string text = File.ReadAllText(@$"{path}\input\Day01.txt");

            int[] input = text.Split("\n").Select(num => int.Parse(num)).ToArray();

            Console.WriteLine($"{input[0]}");

            Part1(input);
            Part2(input);
        }

        public static void Part1(int[] input)
        {
            int count = 0;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1] < input[i])
                {
                    Console.WriteLine($"{input[0]}, {input[1]}, {count}");

                    count++;
                }
            }

            Console.WriteLine($"Solution (Part 1): {count}");

        }

        public static void Part2(int[] input)
        {
            int count = 0;
            for (int i = 3; i < input.Length; i++)
            {
                int A = input[i - 1] + input[i - 2] + input[i - 3];
                int B = input[i] + input[i - 1] + input[i - 2];
                if (A < B)
                {
                    count++;
                }
            }

            Console.WriteLine($"Solution (Part 2): {count}");

        }
    }
}