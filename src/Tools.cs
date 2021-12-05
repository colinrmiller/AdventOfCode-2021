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
    }
}