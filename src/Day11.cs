using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day11
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            string textTest = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day11.txt");

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
            List<List<int>> inputArray = new List<List<int>> { };
            foreach (var line in input)
            {
                inputArray.Add(line.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList());
            }
            return IncBoard2(inputArray);
        }

        public static int Part1(List<string> input)
        {
            List<List<int>> inputArray = new List<List<int>> { };
            foreach (var line in input)
            {
                inputArray.Add(line.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList());
            }
            return IncBoard(inputArray, 100);
        }

        public static int Part2(List<string> input)
        {
            List<List<int>> inputArray = new List<List<int>> { };
            foreach (var line in input)
            {
                inputArray.Add(line.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList());
            }
            return IncBoard2(inputArray);

        }
        public static int IncBoard2(List<List<int>> board, int steps = 0)
        {

            for (int i = 0; i < board.Count; i++)
            {
                for (int j = 0; j < board[0].Count; j++)
                {
                    board[i][j]++;
                }
            }
            int flashCount = TryFlash(board);
            if (flashCount == board.Count * board[0].Count)
            {
                return steps;
            }

            return IncBoard2(board, steps + 1);
        }

        public static int IncBoard(List<List<int>> board, int steps = -1)
        {
            if (steps == 0) return 0;
            int count = 0;

            for (int i = 0; i < board.Count; i++)
            {
                for (int j = 0; j < board[0].Count; j++)
                {
                    board[i][j]++;
                }
            }

            count += TryFlash(board);


            return count + IncBoard(board, steps - 1);
        }

        public static int TryFlash(List<List<int>> board)
        {
            int count = 0;
            bool flashing = true;
            while (flashing)
            {
                flashing = false;
                for (int i = 0; i < board.Count; i++)
                {
                    for (int j = 0; j < board[0].Count; j++)
                    {
                        if (board[i][j] >= 10)
                        {
                            count++;
                            flashing = true;
                            board[i][j] = 0;
                            try { if (board[i + 1][j + 1] != 0) board[i + 1][j + 1]++; } catch { }
                            try { if (board[i][j + 1] != 0) board[i][j + 1]++; } catch { }
                            try { if (board[i - 1][j + 1] != 0) board[i - 1][j + 1]++; } catch { }
                            try { if (board[i + 1][j] != 0) board[i + 1][j]++; } catch { }
                            try { if (board[i - 1][j] != 0) board[i - 1][j]++; } catch { }
                            try { if (board[i + 1][j - 1] != 0) board[i + 1][j - 1]++; } catch { }
                            try { if (board[i][j - 1] != 0) board[i][j - 1]++; } catch { }
                            try { if (board[i - 1][j - 1] != 0) board[i - 1][j - 1]++; } catch { }
                        }
                    }
                }
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