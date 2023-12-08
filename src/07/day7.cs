using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day7
    {
        public struct Hand : IComparable<Hand>
        {
            public string Cards;
            public long Bid;
            
            private string Alphabet;
            private bool JAsJoker;
            private List<int> Counts;

            public Hand(string h, long b, bool jAsJoker)
            {
                Cards = h;
                Bid = b;
                JAsJoker = jAsJoker;
                Alphabet = jAsJoker ? "J23456789TQKA" : "23456789TJQKA";
                Counts = CountAll();
            }

            int IComparable<Hand>.CompareTo(Hand other)
            {
                var compare = 0;

                compare = this.Is5Kind() - other.Is5Kind();
                if(compare != 0) return compare;

                compare = this.Is4Kind() - other.Is4Kind();
                if(compare != 0) return compare;

                compare = this.IsFullHouse() - other.IsFullHouse();
                if(compare != 0) return compare;

                compare = this.Is3Kind() - other.Is3Kind();
                if(compare != 0) return compare;

                compare = this.Is2Pair() - other.Is2Pair();
                if(compare != 0) return compare;

                compare = this.IsPair() - other.IsPair();
                if(compare != 0) return compare;

                for(int i = 0; i < 5; i++)
                {
                    compare = SuddenDeath(this.Cards.ToCharArray()[i], other.Cards.ToCharArray()[i]);
                    if(compare != 0) return compare;
                }
                return compare;
            }

            public int Count(char c) => Cards.Count(f => f == c);

            public List<int> CountAll()
            {
                Dictionary<char, int> dict = new();
                foreach(char a in Alphabet)
                {
                    if(a == 'J' && JAsJoker)
                        continue;
                    dict.Add(a, Count(a));
                }
                if(JAsJoker)
                    dict[dict.MaxBy(kvp => kvp.Value).Key] += Count('J');
                return dict.Values.ToList();
            }

            public int Is5Kind() => Counts.Contains(5) ? 1 : 0;
            public int Is4Kind() => Counts.Contains(4) ? 1 : 0;
            public int IsFullHouse() => Counts.Contains(3) && Counts.Contains(2) ? 1 : 0; 
            public int Is3Kind() => Counts.Contains(3) ? 1 : 0;
            public int Is2Pair() => Counts.Aggregate(0, (current, value) => value == 2 ? current+1 : current) == 2 ? 1 : 0; 
            public int IsPair() => Counts.Contains(2) ? 1 : 0; 
            public int SuddenDeath(char a, char b) => Alphabet.IndexOf(a) - Alphabet.IndexOf(b);


        }
        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/07/input.txt");
            List<Hand> hands = new ();
            foreach(var hand in input)
            {
                string[] handInput = hand.Split(' ');
                hands.Add(new Hand(handInput[0], long.Parse(handInput[1]), true)); // false solves part 1, true solves part 2
            }
            
            hands.Sort();
            long sum = 0;
            for(int i = 0; i < hands.Count(); i++)
            {
                sum += hands[i].Bid * (i+1);
            }
            Console.WriteLine(sum);
        }
    }
}