using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Advent2022
{
    class Day05
    {
        public static void run()
        {
            string path = System.Environment.CurrentDirectory;
            // string text = File.ReadAllText(@$"{path}\input\test.txt");
            string text = File.ReadAllText(@$"{path}\input\Day05.txt");

            List<string> input = text.Split("\r\n").ToList();
            Directions5 instructions = new Directions5(input);
            Console.WriteLine($"{text}");
            // Console.WriteLine($"{input[0]}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 1------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part1(instructions)}");

            Console.WriteLine("-------------------");
            Console.WriteLine("-------Part 2------");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{Part2(instructions)}");
        }

        public static int Part1(Directions5 instructions)
        {
            var board = new Board5();
            foreach (var line in instructions.Directions)
            {
                if (line.IsHorizVert())
                {
                    board.AddList(line);
                }
            }
            return board.CountGreaterThanTwo();

        }
        public static int Part2(Directions5 instructions)
        {
            var board = new Board5();
            foreach (var line in instructions.Directions)
            {
                board.AddList(line);
            }
            return board.CountGreaterThanTwo();

        }

        // public static int Part2(List<int> input)
        // {
        // }
        public class CartCoord
        {
            public CartCoord(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(Object obj)
            {
                //Check for null and compare run-time types.
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    CartCoord p = (CartCoord)obj;
                    return (X == p.X) && (Y == p.Y);
                }

            }
            public override int GetHashCode()
            {
                return X + Y * 1_000_000;
            }


        }
        public class CartCoordPair
        {
            public CartCoordPair(CartCoord start, CartCoord end)
            {
                Start = start;
                End = end;
            }
            public CartCoord Start { get; set; }
            public CartCoord End { get; set; }
            public bool IsHorizVert()
            {
                if (Start.X == End.X || Start.Y == End.Y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public List<CartCoord> Line()
            {
                List<int> vector = new List<int> { End.X - Start.X, End.Y - Start.Y };

                int directionX = (vector[0] == 0) ? 0 : (End.X - Start.X) / Math.Abs(End.X - Start.X);
                int directionY = (vector[1] == 0) ? 0 : (End.Y - Start.Y) / Math.Abs(End.Y - Start.Y);
                List<int> direction = new List<int> { directionX, directionY };

                int mag = vector[0] == 0 ? vector[1] / direction[1] : vector[0] / direction[0];

                List<CartCoord> line = new List<CartCoord> { };
                for (int i = 0; i < mag + 1; i++)
                {
                    line.Add(new CartCoord(Start.X + direction[0] * i, Start.Y + direction[1] * i));
                }


                return line;
            }

        }
        public class Directions5
        {
            public List<CartCoordPair> Directions { get; set; }

            public Directions5(List<string> input)
            {
                Directions = new List<CartCoordPair> { };
                foreach (var line in input)
                {
                    int[] inputCoords = Regex.Matches(line, @"(\d)+").Select(match => int.Parse(match.Value.Replace(",", ""))).ToArray();
                    var start = new CartCoord(inputCoords[0], inputCoords[1]);
                    var end = new CartCoord(inputCoords[2], inputCoords[3]);

                    Directions.Add(new CartCoordPair(start, end));
                }
            }

        }
        public class Board5
        {
            public Dictionary<CartCoord, int> Positions = new Dictionary<CartCoord, int> { };
            public void Add(CartCoord pos)
            {
                if (Positions.ContainsKey(pos))
                {
                    Positions[pos] += 1;
                }
                else
                {
                    Positions.Add(pos, 1);
                }
            }
            public void AddList(CartCoordPair line)
            {
                List<CartCoord> span = line.Line();
                foreach (var coord in span)
                {
                    Add(coord);
                }
            }
            public int CountGreaterThanTwo()
            {
                int count = 0;
                foreach (var pair in Positions)
                {
                    if (pair.Value >= 2)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
    }
}