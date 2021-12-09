using System;
using System.Collections.Generic;
namespace Advent2022
{
    public class Tools
    {
        public static int ParseBin(string binString)
        {
            int val = 0;
            for (int i = 0; i < binString.Length; i++)
            {
                try
                {
                    if (char.GetNumericValue(binString[i]) > 0)
                    {
                        val += (int)Math.Pow(2, (binString.Length - i - 1));
                    }
                }
                catch
                {
                    throw new Exception("ERROR: Not a valid binary string");
                }
            }
            return val;

        }
        public static List<List<string>> SquareApply(List<string> listA, List<string> listB, Func<string, string, string> callback)
        {
            var returnVal = new List<List<string>> { };
            foreach (var item in listA)
            {
                var newRow = new List<string> { };
                foreach (var item2 in listB)
                {
                    newRow.Add(callback(item, item2));
                }
                returnVal.Add(newRow);
            }
            return returnVal;
        }

        // public squareLoop(List<List<int>> board, Func<int, int, int> fnc)
        // {
        //     int count = 0;
        //     bool nadir;
        //     for (int i = 0; i < board.Count; i++)
        //     {
        //         for (int j = 0; j < board[0].Count; j++)
        //         {
        //             nadir = true;
        //             if (i != 0)
        //             {
        //                 if (board[i][j] < board[i - 1][j]) nadir = false;
        //             }
        //             else if (i == board.Count)
        //             {
        //                 if (board[i + 1][j] < board[i][j]) nadir = false;
        //             }
        //             if (j != 0)
        //             {
        //                 if (board[j][j] < board[j - 1][j]) nadir = false;
        //             }
        //             else if (j == board[i].Count)
        //             {
        //                 if (board[j + 1][j] < board[j][j]) nadir = false;
        //             }
        //             count = nadir ? count + 1 : count;
        //         }
        //     }
    }
}