using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day08
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string textTest = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day08.txt");

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

        public static int Test(List<string> input)
        {
            int count = 0;
            foreach (var line in input)
            {

                string valueString = line.Split('|')[1];
                Cipher cipher = new Cipher(line);
                count += cipher.StringValue(valueString);
            }

            return count;

        }
        public static int Part1(List<string> input)
        {
            input = input.Select(line => line.Split('|')[1]).ToList();

            List<List<string>> parsedInput = input.Select(line => line.Split(' ').ToList()).ToList();

            int count = 0;
            foreach (var line in parsedInput)
            {
                foreach (var num in line)
                {
                    if (num.Length == 2 || num.Length == 3 || num.Length == 4 || num.Length == 7) count++;
                }
            }

            return count;
        }
        public static int Part2(List<string> input)
        {
            // List<List<string>> parsedInput = input.Select(line => line.Split(' ').ToList()).ToList();


            int count = 0;
            foreach (var line in input)
            {

                string valueString = line.Split('|')[1];
                Cipher cipher = new Cipher(line);
                count += cipher.StringValue(valueString);
            }

            return count;
        }
        public class Cipher
        {
            // <SUMMARY> 
            // should build a list of             
            public Cipher(string inputLine)
            {
                // generate list of words and order chars in each word internally alphabetically
                List<string> words = inputLine.Split(' ').Where(word => word.Length > 1).Select(word => new string(word.ToCharArray().OrderBy(c => c).ToArray())).ToList();

                words = words.OrderBy(word => word.Length).GroupBy(x => x).Select(y => y.First()).ToList();

                _lookup = new Dictionary<string, int> { };
                _charLookup = new Dictionary<char, char> { };
                _subsets = new Dictionary<string, List<string>> { };
                _ambiguousWords = new List<string> { };
                foreach (var word in words)
                {
                    List<string> subwords = new List<string> { };
                    foreach (var otherWord in words)
                    {
                        if (charDifference(word, otherWord).Count == word.Length - otherWord.Length && word != otherWord)
                        {
                            subwords.Add(otherWord);
                        }
                    }
                    _subsets.Add(word, subwords);
                    // read 1
                    if (word.Length == 2)
                    {
                        _lookup.Add(word, 1);
                    }
                    // read 7
                    else if (word.Length == 3)
                    {
                        _lookup.Add(word, 7);
                    }
                    // read 4
                    else if (word.Length == 4)
                    {
                        _lookup.Add(word, 4);
                    }
                    // read 3
                    else if (word.Length == 5 && subwords.Count() == 2)
                    {
                        _lookup.Add(word, 3);
                    }
                    // lookup 6
                    else if (word.Length == 6 && subwords.Count() == 1)
                    {
                        _lookup.Add(word, 6);
                    }
                    // lookup 9
                    else if (word.Length == 6 && subwords.Count() == 5)
                    {
                        _lookup.Add(word, 9);
                    }
                    else if (word.Length == 6 && subwords.Count() == 2)
                    {
                        _lookup.Add(word, 0);
                    }
                    else if (word.Length == 7)
                    {
                        _lookup.Add(word, 8);
                    }
                    else
                    {
                        _ambiguousWords.Add(word);
                    }
                }

                foreach (var word in _ambiguousWords)
                {
                    string word6 = _lookup.Where(lu => lu.Value == 6).Select(lu => lu.Key).First();
                    if (charDifference(word6, word).Count == 1)
                    {
                        _lookup.Add(word, 5);
                    }
                    else if (charDifference(word6, word).Count == 2)
                    {
                        _lookup.Add(word, 2);
                    }
                }
            }
            public int StringValue(string input)
            {
                int count = 0;

                List<string> words = input.Split(' ').Select(word => new string(word.ToCharArray().OrderBy(c => c).ToArray())).Where(s => s.Length > 0).ToList();

                foreach (var word in words)
                {
                    count = count * 10 + Value(word);
                }
                Console.WriteLine($"{count}");

                return count;

            }
            public int Value(string input)
            {
                return _lookup[input];
            }

            Dictionary<char, char> _charLookup;
            Dictionary<string, int> _lookup;
            Dictionary<string, List<string>> _subsets;
            List<string> _ambiguousWords;
            public List<char> charDifference(string a, string b)
            {
                List<char> diff = new List<char> { };
                foreach (var c in a.ToCharArray())
                {
                    if (!b.Contains(c)) diff.Add(c);
                }

                return diff;
            }
        }

    }
}