using System;
using System.Collections;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day2
    {
        public struct Subgame
        {
            public bool Valid => RevealedRed <= 12 &&
                                 RevealedGreen <= 13 &&
                                 RevealedBlue <= 14;
            public int RevealedRed;
            public int RevealedGreen;
            public int RevealedBlue;

            public void Feed(string s)
            {
                String[] split = s.Trim().Split(" ");
                switch(split[1])
                {
                    case "red": RevealedRed += int.Parse(split[0]); break;
                    case "green": RevealedGreen += int.Parse(split[0]); break;
                    case "blue": RevealedBlue += int.Parse(split[0]); break;
                }
            }
        }

        public struct Game
        {
            public int Number;
            public bool Valid => !Subgames.Any(g => !g.Valid);
            public List<Subgame> Subgames;
            
            public Game(int num)
            {
                Number = num;
                Subgames = new();
            }

            public void Add(Subgame s) => Subgames.Add(s);

            public int Power()
            {
                int maxRed = 0; int maxGreen = 0; int maxBlue = 0;
                foreach(Subgame s in Subgames)
                {
                    if(s.RevealedRed > maxRed)
                        maxRed = s.RevealedRed;
                    if(s.RevealedGreen > maxGreen)
                        maxGreen = s.RevealedGreen;
                    if(s.RevealedBlue > maxBlue)
                        maxBlue = s.RevealedBlue;
                }
                return maxRed * maxGreen * maxBlue;
            }
        }

        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/02/input.txt");
            List<Game> games = ProcessGames(input);
            Console.WriteLine(IterateGames(games, Part1Func));
            Console.WriteLine(IterateGames(games, Part2Func));
        }

        private static List<Game> ProcessGames(IEnumerable<string> input)
        {
            List<Game> games = new();
            int index = 1;
            foreach(string gameText in input)
            {
                Game game = new Game(index++);
                IEnumerable<string> subgames = gameText.Substring(gameText.IndexOf(':') + 1).Split(';');
                foreach(string subgameText in subgames)
                {
                    Subgame subgame = new Subgame();
                    IEnumerable<string> feedlines = subgameText.Split(',');
                    foreach(string feedline in feedlines)
                    {
                        subgame.Feed(feedline);
                    }
                    game.Add(subgame);
                }
                games.Add(game);
            }
            return games;
        }

        private static int IterateGames(List<Game> games, PartFunc func)
        {
            int sum = 0;
            foreach(Game game in games)
            {
                sum += func(game);
            }
            return sum;
        }

        private delegate int PartFunc(Game g);
        private static int Part1Func(Game g) => g.Valid ? g.Number : 0;
        private static int Part2Func(Game g) => g.Power();
    }
}