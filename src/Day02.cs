using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;

namespace Advent2022
{
    class Day02
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string text = File.ReadAllText(@$"{path}\input\Day02.txt");

            string[] input = text.Split("\n");

            Part2(input);
            // Part2(input);
        }

        public static void Part1(string[] input)
        {
            (int x, int z) = (0, 0);

            foreach (var step in input)
            {
                string direction = step.Split(" ")[0];
                int mag = int.Parse(step.Split(" ")[1]);
                switch (direction)
                {
                    case "forward":
                        {
                            x += mag;
                            Console.WriteLine($"hoz: {x}");
                            Console.WriteLine($"depth: {z}");
                            break;
                        }
                    case "up":
                        {
                            z -= mag;
                            Console.WriteLine($"aim: {z}");
                            break;
                        }
                    case "down":
                        {
                            z += mag;
                            Console.WriteLine($"aim: {z}");
                            break;
                        }
                    default:
                        throw new Exception($"{direction} is not in list");
                }
            }
            Console.WriteLine($"{x * z}");

        }

        public static void Part2(string[] input)
        {
            (int x, int z, int aim) = (0, 0, 0);

            foreach (var step in input)
            {
                string direction = step.Split(" ")[0];
                int mag = int.Parse(step.Split(" ")[1]);
                switch (direction)
                {
                    case "forward":
                        {
                            x += mag;
                            z += mag * aim;
                            Console.WriteLine($"hoz: {x}");
                            Console.WriteLine($"depth: {z}");

                            break;
                        }
                    case "down":
                        {
                            aim += mag;
                            Console.WriteLine($"depth: {z}");
                            Console.WriteLine($"aim: {z}");
                            break;
                        }
                    case "up":
                        {
                            aim -= mag;
                            Console.WriteLine($"depth: {z}");
                            break;
                        }
                    default:
                        throw new Exception($"{direction} is not in list");
                }
            }
            Console.WriteLine($"{x * z}");

        }
    }
}