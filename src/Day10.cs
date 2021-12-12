using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day10
    {

        public class Record
        {
            private List<int> _record = new List<int> { 0, 0, 0, 0 };
            private int currupted = 0;

            public static int findCurroptedPart1(List<char> strand)
            {
                bool stillLooking = true;
                while (stillLooking)
                {
                    stillLooking = false;
                    var stringCopy = strand;
                    for (int i = strand.Count - 1; i > 0; i--)
                    {
                        if ((strand[i] == '}' && strand[i - 1] == '{')
                        || (strand[i] == ')' && strand[i - 1] == '(')
                        || (strand[i] == ']' && strand[i - 1] == '[')
                        || (strand[i] == '>' && strand[i - 1] == '<'))
                        {
                            stillLooking = true;
                            stringCopy.RemoveRange(i - 1, 2);
                            i--;
                        }
                    }
                    strand = stringCopy;
                }
                foreach (var c in strand)
                {
                    if ((int)c == (int)')') return 3;
                    if ((int)c == (int)']') return 57;
                    if ((int)c == (int)'}') return 1197;
                    if ((int)c == (int)'>') return 25137;
                }
                return 0;

            }
            public static UInt64 findCurroptedPart2(List<char> strand)
            {
                bool stillLooking = true;
                while (stillLooking)
                {
                    stillLooking = false;
                    var stringCopy = strand;
                    for (int i = strand.Count - 1; i > 0; i--)
                    {
                        if ((strand[i] == '}' && strand[i - 1] == '{')
                        || (strand[i] == ')' && strand[i - 1] == '(')
                        || (strand[i] == ']' && strand[i - 1] == '[')
                        || (strand[i] == '>' && strand[i - 1] == '<'))
                        {
                            stillLooking = true;
                            stringCopy.RemoveRange(i - 1, 2);
                            i--;
                        }
                    }
                    strand = stringCopy;
                }
                foreach (var c in strand)
                {
                    if ((int)c == (int)')') return 0;
                    if ((int)c == (int)']') return 0;
                    if ((int)c == (int)'}') return 0;
                    if ((int)c == (int)'>') return 0;
                }
                return completeUnfinished(strand);
            }

            public static UInt64 completeUnfinished(List<char> strand)
            {

                Console.Write(string.Join("", strand));
                UInt64 count = 0;
                List<UInt64> p5 = Enumerable.Range(0, strand.Count).Select(i => (UInt64)Math.Pow(5, i)).ToList();

                for (int i = 0; i < strand.Count; i++)
                {
                    UInt64 val = 0;
                    if (strand[i] == '(') val = 1;
                    if (strand[i] == '[') val = 2;
                    if (strand[i] == '{') val = 3;
                    if (strand[i] == '<') val = 4;
                    count += val * p5[i];
                }
                Console.WriteLine($" {count}");
                return count;
            }
        }

        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string textTest = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day10.txt");

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
            Console.WriteLine($"{Part1(input)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 2------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part2(input)}");
        }

        public static UInt64 Test(List<string> input)
        {
            List<UInt64> scores = new List<UInt64> { };
            foreach (var line in input)
            {
                UInt64 val = Record.findCurroptedPart2(line.ToCharArray().ToList());
                scores.Add(val);
            }

            int mid = scores.Where(v => v > 0).Count() / 2;
            return scores.Where(v => v > 0).OrderBy(v => v).ToList()[mid];
        }
        public static int Part1(List<string> input)
        {

            int count = 0;
            foreach (var line in input)
            {
                int val = Record.findCurroptedPart1(line.ToCharArray().ToList());
                count += val;
            }

            return count;
        }
        public static UInt64 Part2(List<string> input)
        {
            List<UInt64> scores = new List<UInt64> { };
            foreach (var line in input)
            {
                UInt64 val = Record.findCurroptedPart2(line.ToCharArray().ToList());
                scores.Add(val);
            }

            int mid = scores.Where(v => v > 0).Count() / 2;
            List<UInt64> orderedScores = scores.Where(v => v > 0).OrderBy(v => v).ToList();
            return scores.Where(v => v > 0).OrderBy(v => v).ToList()[mid];
        }
    }
}