using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2022
{
    class Day8
    {
        public const string START = "AAA";
        public const string END = "ZZZ";

        public struct Node
        {
            
            public string Value;
            public string Left;
            public string Right;
            
            public Node(string val, string left, string right)
            {
                Left = left;
                Right = right;
                Value = val;
            }

            public (string, int) PrimeCache(string input)
            {
                Node current = this;
                int rounds = 0;

                foreach(char c in input)
                {
                    if(current.Value == Day8.END)
                        return (Day8.END, rounds);
                    if(c == 'L')
                        current = Day8.Mapping[current.Left];
                    if(c == 'R')
                        current = Day8.Mapping[current.Right];
                    rounds++;
                }
                return (current.Value, rounds);
            }


        }

        public static Dictionary<string, Node> Mapping;
        public static Dictionary<string, (string, int)> Cache;
        public static Dictionary<(string, int), (string, int)> ZTurboCache;

        public static void Run()
        {
            IEnumerator<string> input = File.ReadLines("src/08/input.txt").GetEnumerator();
            input.MoveNext();
            string directions = input.Current;
            input.MoveNext();

            Mapping = new();
            Cache = new();

            Regex rx = new Regex(@"[A-Z][A-Z][A-Z]");
            while(input.MoveNext())
            {
                MatchCollection matches = rx.Matches(input.Current);
                Node node = new Node(matches[0].Value, matches[1].Value, matches[2].Value);
                Mapping.Add(node.Value, node);
            }
            foreach(var kvp in Mapping)
            {
                Cache.Add(kvp.Key, kvp.Value.PrimeCache(directions));
            }

            Part1(directions);
            Part2(directions);
        }

        public void Part1()
        {
            int totalMoves = 0;
            string current = Day8.START;
            while(current != Day8.END)
            {
                (current, int moves) = Cache[current];
                totalMoves += moves;
            }
            Console.WriteLine(totalMoves);
        }

        public static void Part2(string input)
        {
            //ZTurboCache = BuildTurboCache(input);
            List<long> ModularData = new();
            foreach(var elem in Mapping.Where(kvp => kvp.Key.EndsWith('A')))
            {
                    Node current = elem.Value;
                    int iter = 0;
                    do
                    {
                        switch(input[iter % input.Length])
                        {
                            case 'L': current = Mapping[current.Left]; break;
                            case 'R': current = Mapping[current.Right]; break;
                        }
                        iter++;
                    } while (!current.Value.EndsWith('Z'));
                    //(_, int cycle) = ZTurboCache[(current.Value, iter % input.Length)];

                    ModularData.Add(iter);                    
            }
            long lcm = ModularData.Aggregate(1L, (current, value) => current = LCM(current, value));
            Console.WriteLine(lcm);
        }

        private static long GCF(long a, long b)
        {
            while(b != 0)
            {
                long tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }

        private static long LCM(long a, long b) => (a / GCF(a, b)) * b;

        // unfortunately not actually needed because of how the AOC people decided to choose extremely specific inputs
        // this would be a required step in a truly general solution, and I'm extrmely proud of it so it stays
        public static Dictionary<(string, int), (string, int)> BuildTurboCache(string input)
        {
            Dictionary<(string, int), (string, int)> turboCache = new();
            foreach(var elem in Mapping.Where(kvp => kvp.Key.EndsWith('Z')))
            {
                for(int i = 0; i < input.Count(); i++)
                {
                    string newInput = input.Substring(i) + input.Substring(0, i);
                    Node current = elem.Value;
                    int iter = 0;
                    do
                    {
                        switch(newInput[iter % input.Length])
                        {
                            case 'L': current = Mapping[current.Left]; break;
                            case 'R': current = Mapping[current.Right]; break;
                        }
                        iter++;
                    } while (!current.Value.EndsWith('Z'));
                    turboCache.Add((elem.Key, i), (current.Value, iter));
                }
            }
            return turboCache;
        }
    }
}