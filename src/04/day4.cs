using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day4
    {

        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/04/input.txt");
            ProcessGames(input); // part 1
        }

        private static void ProcessGames(IEnumerable<string> input)
        {
            int sum = 0;
            foreach(string gameText in input)
            {
                string[] subgames = gameText.Substring(gameText.IndexOf(':') + 1).Split('|');
                List<string> winners = new();
                int matches = 0;
                foreach(string num in subgames[0].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    winners.Add(num);
                }
                foreach(string num in subgames[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if(winners.Contains(num))
                    {
                        matches++;
                    }
                }
                sum += (int) Math.Pow(2, matches - 1);
            }
            Console.WriteLine(sum);
        }


    }       
}