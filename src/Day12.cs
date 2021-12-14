using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2022
{
    class Day12
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string textTest = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day12.txt");

            List<string> input = text.Split("\r\n").ToList();
            List<string> inputTest = textTest.Split("\r\n").ToList();


            // Console.WriteLine($"{text}");
            // Console.WriteLine($"{input[0]}");

            Console.WriteLine("-------------------");
            Console.WriteLine("------- Test ------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Test(inputTest)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 1------");
            Console.WriteLine("-------------------");
            // Console.WriteLine($"{Part1(input)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 2------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part2(input)}");
        }

        public static int Test(List<string> input)
        {
            List<List<string>> moves = input.Select(line => line.Split("-").ToList()).ToList();
            List<List<string>> movesReversed = input.Select(line =>
            {
                List<string> list = line.Split("-").ToList();
                list.Reverse();
                return list;
            }).ToList();
            moves = moves.Concat(movesReversed).ToList();
            List<string> hist = new List<string> { };

            var c = new Count();

            move2("start", moves, hist, c, true);

            return c.HistList.ToArray().Distinct().Count();

        }

        public static int Part1(List<string> input)
        {
            List<List<string>> moves = input.Select(line => line.Split("-").ToList()).ToList();
            List<List<string>> movesReversed = input.Select(line =>
            {
                List<string> list = line.Split("-").ToList();
                list.Reverse();
                return list;
            }).ToList();
            moves = moves.Concat(movesReversed).ToList();

            var c = new Count();

            move("start", moves, c);

            return c.val();
        }

        public static int Part2(List<string> input)
        {
            List<List<string>> moves = input.Select(line => line.Split("-").ToList()).ToList();
            List<List<string>> movesReversed = input.Select(line =>
            {
                List<string> list = line.Split("-").ToList();
                list.Reverse();
                return list;
            }).ToList();
            moves = moves.Concat(movesReversed).ToList();
            List<string> hist = new List<string> { };

            var c = new Count();

            move2("start", moves, hist, c, true);

            List<string> distinct = c.HistList.ToArray().Distinct().ToList();
            return distinct.Count();
        }


        public class Count
        {
            private int _c = 0;
            public List<string> HistList = new List<string> { };

            public void inc()
            {
                _c++;
            }
            public int val()
            {
                return _c;
            }
        }

        public static void move(string node, List<List<string>> moves, Count c)
        {
            // when leaving a lower case node, ensure we never return to it.
            if (!IsUpper(node)) moves = moves.Where(m => m[1] != node).ToList();

            if (moves.Where(m => m[1] == "end").Count() == 0)
            {
                return;
            }
            // Console.WriteLine(node);
            // Tools.PrintListList(moves);
            // play out each moveset from current node
            foreach (var m in moves.Where(m => m[0] == node))
            {
                if (m[1] == "end")
                {
                    c.inc();
                }
                else
                {
                    move(m[1], moves, c);
                }
            }
        }

        public static void move2(string node, List<List<string>> moves, List<string> hist, Count c, bool skip, bool skipping = false)
        {
            // make a local copy of moveset; 
            List<List<string>> localMoves = Tools.SloppyCopy(moves);

            if (node == "start") localMoves = localMoves.Where(m => m[1] != node).OrderBy(elm => elm[0]).ToList();
            // when leaving a lower case node, ensure we never return to it if we don't have a skip
            if (!IsUpper(node) && (!skipping))
            {
                localMoves = localMoves.Where(m => m[1] != node).ToList();
            }
            if (localMoves.Where(m => m[1] == "end").Count() == 0)
            {
                return;
            }

            // play out each moveset from current node
            List<List<string>> currentMoves = localMoves.Where(m => m[0] == node).ToList();
            foreach (var m in currentMoves)
            {
                if (m[1] == "end")
                {
                    List<string> histList = Tools.SloppyCopy(hist);
                    histList.Add(node);
                    c.HistList.Add(string.Join(", ", histList));

                    c.inc();
                }
                else
                {
                    List<string> histList = Tools.SloppyCopy(hist);
                    histList.Add(node);
                    if (!IsUpper(m[1]) && skip)
                    {
                        move2(m[1], localMoves, histList, c, false, true);
                        move2(m[1], localMoves, histList, c, true, false);
                    }
                    else
                    {
                        move2(m[1], localMoves, histList, c, skip);
                    }
                }
            }
        }

        public static bool IsUpper(string s)
        {
            if (char.IsUpper(s.ToCharArray()[0]))
            {
                return true;
            }
            return false;
        }
    }
}