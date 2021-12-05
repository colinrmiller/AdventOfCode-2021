using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day04
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            // string text = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day04.txt");

            List<string> input = text.Split("\r\n\r\n").ToList();
            List<List<int>> input2 = new List<List<int>> { };
            foreach (var line in input)
            {
                input2.Add(Regex.Matches(line, @"\d+").OfType<Match>()
                        .Select(m => int.Parse(m.Groups[0].Value))
                        .ToList());
            }

            // Console.WriteLine($"{text}");
            // Console.WriteLine($"{input[0]}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 1------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part1(input2)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 2------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part2(input2)}");
        }

        public static int Part1(List<List<int>> input)
        {
            List<int> picks = input[0];
            List<Board> boards = new List<Board> { };

            for (int i = 1; i < input.Count; i++)
            {
                boards.Add(new Board(input[i]));
            }

            foreach (var pick in picks)
            {
                foreach (var board in boards)
                {
                    board.AddNumber(pick);
                    if (board.IsComplete())
                    {
                        // subtract board.S from total sum of each board to get the sum(unmarked nums) and multiply to get solution
                        return (board.TotalSum - board.Sum) * pick;
                    }
                }
            }
            return -1;
        }

        public static int Part2(List<List<int>> input)
        {
            List<int> picks = input[0];
            List<Board> boards = new List<Board> { };

            for (int i = 1; i < input.Count; i++)
            {
                boards.Add(new Board(input[i]));
            }

            foreach (var pick in picks)
            {
                List<Board> completedBoards = new List<Board> { };
                foreach (var board in boards)
                {
                    board.AddNumber(pick);
                    if (board.IsComplete())
                    {
                        if (boards.Count > 1)
                        {
                            completedBoards.Add(board);
                        }
                        else
                        {
                            return (board.TotalSum - board.Sum) * pick;
                        }
                    }
                }
                foreach (var board in completedBoards)
                {
                    boards.Remove(board);
                }
            }
            return -1;
        }
        class Board
        {
            public List<int> GameBoard { get; }
            public List<int> RowsAndColumns = Enumerable.Repeat(5, 10).ToList();
            public int Sum = 0;

            public int TotalSum;
            public Board(List<int> squares)
            {
                GameBoard = squares;
                TotalSum = squares.Sum();
            }
            public void AddNumber(int num)
            {
                if (GameBoard.Contains(num))
                {
                    Sum += num;

                    int pos = GameBoard.IndexOf(num);
                    int x = pos % 5;
                    int y = pos / 5;

                    RowsAndColumns[x] -= 1;
                    RowsAndColumns[y + 5] -= 1;
                }
            }
            public bool IsComplete()
            {
                foreach (var rowCol in RowsAndColumns)
                {
                    if (rowCol <= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}