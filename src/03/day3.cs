using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day3
    {
        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/03/input.txt");
            char[][] matrixInput = input.Select(line => line.ToCharArray()).ToArray();
            Console.WriteLine(matrixInput.Count());
        }
    }       
}