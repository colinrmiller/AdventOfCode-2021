using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2022
{
    class Day13
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string textTest = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day13.txt");

            List<string> fullInput = text.Split("\r\n\r\n").ToList();
            List<string> fullInputTest = textTest.Split("\r\n\r\n").ToList();
            List<string> input = fullInput[0].Split("\r\n").ToList();
            List<string> instructions = fullInput[1].Trim().Split("\r\n").ToList();
            List<string> inputTest = fullInputTest[0].Split("\r\n").ToList();
            List<string> instructionsTest = fullInputTest[1].Trim().Split("\r\n").ToList();


            // Console.WriteLine($"{text}");
            // Console.WriteLine($"{input[1]}");

            Console.WriteLine("-------------------");
            Console.WriteLine("------- Test ------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Test(inputTest, instructionsTest)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 1------");
            Console.WriteLine("-------------------");
            // Console.WriteLine($"{Part1(inputTest, instructionsTest)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 2------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part2(input, instructions)}");
        }


        public static int Test(List<string> input, List<string> instructions)
        {
            List<List<int>> points = new List<List<int>> { };
            foreach (string coord in input)
            {
                points.Add(coord.Split(",").ToList().Select(n => int.Parse(n)).ToList());
            }

            foreach (var line in instructions)
            {
                var instrct = line.Split("=").ToList();
                int fold = int.Parse(instrct[1]);

                // check last element of first split of instrct
                if (instrct[0][instrct[0].Length - 1] == 'x')
                {
                    foreach (var point in points)
                    {
                        if (point[0] > fold) point[0] = fold * 2 - point[0];
                    }
                }
                else if (instrct[0][instrct[0].Length - 1] == 'y')
                {
                    foreach (var point in points)
                    {
                        if (point[1] > fold) point[1] = fold * 2 - point[1];
                    }

                }
            }
            List<List<int>> distinct = points.DistinctBy(pair => pair[0] * 10000 + pair[1]).OrderBy(pair => pair[0] * 10000 + pair[1]).ToList();

            PrintPaper(distinct);
            return distinct.Count();
        }

        public static int Part1(List<string> input, List<string> instructions)
        {
            List<Tools.CartCoord> coords = new List<Tools.CartCoord> { };
            foreach (var line in instructions.Take(1))
            {
                var instrct = line.Split("=").ToList();
                // check last element of first split of instrct
                if (instrct[0][instrct[0].Length - 1] == 'x')
                {
                    int fold = int.Parse(instrct[1]);
                    foreach (var row in input)
                    {
                        List<int> coord = row.Split(",").Select(n => int.Parse(n)).ToList();

                        if (coord[0] > fold)
                        {
                            var c = new Tools.CartCoord(2 * fold - coord[0], coord[1]);
                            if (!coords.Contains(c))
                            {
                                coords.Add(c);
                            }
                            else
                            {
                                // Console.WriteLine($"Double: {c}");
                            }
                        }
                        else
                        {
                            coords.Add(new Tools.CartCoord(coord[0], coord[1]));
                        }
                    }
                }
                else if (instrct[0][instrct[0].Length - 1] == 'y')
                {
                    int fold = int.Parse(instrct[1]);
                    foreach (var row in input)
                    {
                        List<int> coord = row.Split(",").Select(n => int.Parse(n)).ToList();

                        if (coord[1] > fold)
                        {
                            var c = new Tools.CartCoord(coord[0], 2 * fold - coord[1]);
                            if (!coords.Contains(c))
                            {
                                coords.Add(c);
                            }
                            else
                            {
                                // Console.WriteLine($"Double: {c}");
                            }
                        }
                        else
                        {
                            coords.Add(new Tools.CartCoord(coord[0], coord[1]));
                        }
                    }
                }
            }
            var distinct = coords.Distinct().ToList();
            foreach (var c in distinct)
            {
                // Console.WriteLine($"{c.X}, {c.Y}");
            }
            // PrintPaper(distinct);
            return distinct.Count;
        }

        public static int Part2(List<string> input, List<string> instructions)
        {
            List<List<int>> points = new List<List<int>> { };
            foreach (string coord in input)
            {
                points.Add(coord.Split(",").ToList().Select(n => int.Parse(n)).ToList());
            }

            foreach (var line in instructions)
            {
                var instrct = line.Split("=").ToList();
                int fold = int.Parse(instrct[1]);

                // check last element of first split of instrct
                if (instrct[0][instrct[0].Length - 1] == 'x')
                {
                    foreach (var point in points)
                    {
                        if (point[0] > fold) point[0] = fold * 2 - point[0];
                    }
                }
                else if (instrct[0][instrct[0].Length - 1] == 'y')
                {
                    foreach (var point in points)
                    {
                        if (point[1] > fold) point[1] = fold * 2 - point[1];
                    }

                }
            }
            List<List<int>> distinct = points.DistinctBy(pair => pair[0] * 10000 + pair[1]).OrderBy(pair => pair[0] * 10000 + pair[1]).ToList();

            PrintPaper(distinct);
            return distinct.Count();
        }


        public static void PrintPaper(List<List<int>> paper)
        {
            List<List<int>> orderdCoords = paper.OrderBy(c => c[0] + c[1] * 100000).ToList();
            Tools.PrintListList(orderdCoords);
            (int x, int y) = (0, 0);
            foreach (var c in orderdCoords)
            {
                while (y < c[1])
                {
                    Console.WriteLine();
                    y++;
                    x = 0;
                }
                if (c[1] == y)
                {
                    while (x < c[0])
                    {
                        Console.Write(".");
                        x++;
                    }
                    if (c[0] == x)
                    {
                        Console.Write("#");
                        x++;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}