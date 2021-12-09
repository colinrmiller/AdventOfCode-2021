using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day09
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string textTest = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day09.txt");

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
            List<List<int>> inputArray = new List<List<int>> { };
            List<Pool> pools = new List<Pool> { };

            foreach (var line in input)
            {
                inputArray.Add(line.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList());
            }

            for (int i = 0; i < inputArray.Count; i++)
            {
                for (int j = 0; j < inputArray[i].Count; j++)
                {
                    if (IsNadir(inputArray, i, j))
                    {
                        pools.Add(new Pool(i, j));
                    }
                }
            }
            bool stillLooking = true;
            while (stillLooking == true)
            {
                stillLooking = false;
                for (int i = 0; i < inputArray.Count; i++)
                {
                    for (int j = 0; j < inputArray[i].Count; j++)
                    {
                        if (inputArray[i][j] != 9)
                        {
                            foreach (var pool in pools)
                            {
                                if (pool.Add(i, j))
                                {
                                    stillLooking = true;
                                    break;
                                }
                            }
                        }
                    }
                }


            }
            List<int> poolValues = pools.Select(p => p.Size).OrderByDescending(v => v).Take(3).ToList();
            return poolValues.Aggregate(1, (acc, elm) => acc * elm);
        }
        public static int Part1(List<string> input)
        {
            List<List<int>> inputArray = new List<List<int>> { };
            foreach (var line in input)
            {
                inputArray.Add(line.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList());
            }
            return SquareLoop(inputArray);
        }
        public static int Part2(List<string> input)
        {
            List<List<int>> inputArray = new List<List<int>> { };
            List<Pool> pools = new List<Pool> { };

            foreach (var line in input)
            {
                inputArray.Add(line.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList());
            }

            for (int i = 0; i < inputArray.Count; i++)
            {
                for (int j = 0; j < inputArray[i].Count; j++)
                {
                    if (IsNadir(inputArray, i, j))
                    {
                        pools.Add(new Pool(i, j));
                    }
                }
            }
            bool stillLooking = true;
            while (stillLooking == true)
            {
                stillLooking = false;
                for (int i = 0; i < inputArray.Count; i++)
                {
                    for (int j = 0; j < inputArray[i].Count; j++)
                    {
                        if (inputArray[i][j] != 9)
                        {
                            foreach (var pool in pools)
                            {
                                if (pool.Add(i, j))
                                {
                                    stillLooking = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            List<int> poolValues = pools.Select(p => p.Size).OrderByDescending(v => v).Take(3).ToList();
            return poolValues.Aggregate(1, (acc, elm) => acc * elm);
        }
        public class Pool
        {
            // TODO refactor as HashSet
            private Dictionary<List<int>, bool> _lookup;
            private int _size;
            public int Size
            {
                get
                {
                    return _size;
                }
            }

            public Pool(int i, int j)
            {
                _lookup = new Dictionary<List<int>, bool> { };
                _lookup.Add(new List<int> { i, j }, true);
                _size = 1;
            }
            public bool Add(int i, int j)
            {
                var keyMatches = _lookup.Where(kvp => kvp.Key[0] == i && kvp.Key[1] == j).ToList();
                if (keyMatches.Count == 0)
                {
                    Dictionary<List<int>, bool> lookupCopy = _lookup;
                    foreach (var cpair in lookupCopy.Keys)
                    {
                        if ((Math.Abs(cpair[0] - i) == 1 && cpair[1] == j) || (Math.Abs(cpair[1] - j) == 1 && cpair[0] == i))
                        {
                            _lookup.Add(new List<int> { i, j }, true);
                            _size += 1;
                            return true;
                        }
                    }
                }
                return false;
            }

        }
        public static int SquareLoop(List<List<int>> board)
        {
            int count = 0;
            bool nadir;
            for (int i = 0; i < board.Count; i++)
            {
                for (int j = 0; j < board[0].Count; j++)
                {
                    nadir = true;
                    if (i != 0)
                    {
                        if (board[i][j] >= board[i - 1][j]) nadir = false;
                    }
                    if (i != board.Count - 1)
                    {
                        if (board[i + 1][j] <= board[i][j]) nadir = false;
                    }
                    if (j != 0)
                    {
                        if (board[i][j] >= board[i][j - 1]) nadir = false;
                    }
                    if (j != board[i].Count - 1)
                    {
                        if (board[i][j + 1] <= board[i][j]) nadir = false;
                    }
                    count = nadir ? count + board[i][j] + 1 : count;
                    string print = nadir ? ((board[i][j] + 1).ToString()) : "0";
                    Console.Write(print);
                }
                Console.WriteLine();
            }
            return count;
        }
        public static bool IsNadir(List<List<int>> board, int i, int j)
        {
            bool nadir = true;
            if (i != 0)
            {
                if (board[i][j] >= board[i - 1][j]) nadir = false;
            }
            if (i != board.Count - 1)
            {
                if (board[i + 1][j] <= board[i][j]) nadir = false;
            }
            if (j != 0)
            {
                if (board[i][j] >= board[i][j - 1]) nadir = false;
            }
            if (j != board[i].Count - 1)
            {
                if (board[i][j + 1] <= board[i][j]) nadir = false;
            }
            return nadir;
        }
    }
}