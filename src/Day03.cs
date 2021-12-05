using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;

namespace Advent2022
{
    class Day03
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            // string text = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day03.txt");

            string[] input = text.Split("\r\n");

            Console.WriteLine($"{input[0]}");

            // Part1(input);
            Part2(input);
        }

        public static void Part1(string[] input)
        {
            int[] sum = new int[input[0].Length];

            for (int i = 1; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '1') sum[j]++;
                    else sum[j]--;
                }
            }

            for (int i = 0; i < sum.Length; i++)
            {
                Console.Write($"{sum[i]}");

            }
            int gamma = 0;
            for (int i = 0; i < sum.Length; i++)
            {
                if (sum[i] > 0)
                {
                    Console.Write($"{1}");

                    gamma += (int)Math.Pow(2, (sum.Length - i - 1));
                }
                else
                {
                    Console.Write("0");
                }
            }
            Console.WriteLine($"{gamma}: {gamma * ((int)(Math.Pow(2, sum.Length)) - gamma - 1) }");

        }

        public static void Part2(string[] input)
        {
            (int min, int max) = (0, input.Length - 1);
            int index = 0;


            string[] orderedInputMax = input.OrderBy(item => item).ToArray();
            string[] orderedInputMin = input.OrderBy(item => item).ToArray();
            while (index < input[0].Length)
            {
                int mid = (int)Math.Ceiling(((min + max / 2.0)));
                if ((double)mid == (min + max + 1) / 2.0 && orderedInputMax[mid][index] == '1' && orderedInputMax[mid - 1][index] == '0')
                {
                    orderedInputMax = orderedInputMax.Where(val => val[index] == '1').ToArray();
                }
                else if (orderedInputMax[mid][index] == '0')
                {
                    orderedInputMax = orderedInputMax.Where(val => val[index] == '0').ToArray();
                }
                else
                {
                    orderedInputMax = orderedInputMax.Where(val => val[index] == '1').ToArray();
                }
                max = orderedInputMax.Length - 1;

                index++;
            }
            index = 0;
            max = input.Length;
            while (index < input[0].Length && orderedInputMin.Length > 1)
            {
                int mid = (int)Math.Ceiling(((min + max / 2.0)));
                if ((double)mid == (min + max + 1) / 2.0 && orderedInputMin[mid][index] == '1' && orderedInputMin[mid - 1][index] == '0')
                {
                    orderedInputMin = orderedInputMin.Where(val => val[index] == '0').ToArray();
                }
                else if (orderedInputMin[mid][index] == '0')
                {
                    orderedInputMin = orderedInputMin.Where(val => val[index] == '1').ToArray();
                }
                else
                {
                    orderedInputMin = orderedInputMin.Where(val => val[index] == '0').ToArray();
                }
                max = orderedInputMin.Length - 1;

                index++;
            }
            Console.WriteLine($"{(orderedInputMax[0])}");
            Console.WriteLine($"{(orderedInputMin[0])}");
            Console.WriteLine($"{Tools.ParseBin(orderedInputMax[0])}");
            Console.WriteLine($"{Tools.ParseBin(orderedInputMin[0])}");
            Console.WriteLine($"{Tools.ParseBin(orderedInputMax[0]) * Tools.ParseBin(orderedInputMin[0])}");



        }
    }
}