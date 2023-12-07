using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day3
    {
        struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/03/input.txt");
            char[][] matrixInput = input.Select(line => line.ToCharArray()).ToArray();
            List<Point> symbolIndices = GetSymbolIndices(matrixInput);
            List<Point> visited = new();
            
            int sum = 0;
            foreach(Point index in symbolIndices)
            {
                List<Point> adjacentDigits = GetAdjacentDigits(matrixInput, index);
                foreach(Point digit in adjacentDigits)
                {
                    if(!visited.Contains(digit))
                    {
                        sum += GetFullNumberAtPoint(digit, matrixInput, visited);
                    }
                }
            }
            Console.WriteLine(sum);
        }

        private static List<Point> GetSymbolIndices(char[][] matrixInput)
        {
            List<Point> symbolIndices = new();
            for(int y = 0; y < matrixInput.Count(); y++)
            {
                for(int x = 0; x < matrixInput[0].Count(); x++)
                {
                    if(matrixInput[y][x] == '.' || Char.IsDigit(matrixInput[y][x]))
                        continue;
                    symbolIndices.Add(new Point(x, y));
                }
            }
            return symbolIndices;
        }
        
        private static List<Point> GetAdjacentDigits(char[][] input, Point p)
        {
            List<Point> digits = new ();
            for(int i = p.x - 1; i <= p.x + 1; i++)
            {
                for(int j = p.y - 1; j <= p.y + 1; j++)
                {
                    if(i >= 0 && i < input[0].Count() && j >= 0 && j < input.Count() && Char.IsDigit(input[j][i]))
                    {
                        digits.Add(new Point(i, j));
                    }
                }
            }
            return digits;
        }

        private static int GetFullNumberAtPoint(Point p, char[][] input, List<Point> visited)
        {
            string num = "" + input[p.y][p.x];
            
            //left side
            while(false);

            //right side
            while(false);

            return int.Parse(num);
        }


    }       
}