using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day6
    {

        public struct Race
        {
            public long Time;
            public long Distance;
            private double Radical;

            public Race(long t, long d)
            {
                Time = t;
                Distance = d;
                Radical = Math.Sqrt(Math.Pow(Time, 2) - 4 * Distance);
            }

            public int NumAnswers => (int) (Math.Floor((Time + Radical)/2) - Math.Ceiling((Time - Radical)/2)) + 1;
        }
        
        public static void Run()
        {
            List<Race> races = new ()
            {
                new Race(48, 255),
                new Race(87, 1288),
                new Race(69, 1117),
                new Race(81, 1623),
            };

            Part1(races);
            Part2(races);
        }

        private static void Part1(List<Race> races)
        {
            int product = 1;
            foreach(var race in races)
            {
                product *= race.NumAnswers;
            } 
            Console.WriteLine(product);  
        }

        private static void Part2(List<Race> races)
        {
            string time = "";
            string distance = "";
            foreach(var race in races)
            {
                time += race.Time;
                distance += race.Distance;
            }   
            Race realRace = new Race(long.Parse(time), long.Parse(distance));
            Console.WriteLine(realRace.NumAnswers);
        }
    }
}