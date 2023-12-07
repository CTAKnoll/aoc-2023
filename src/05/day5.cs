using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day5
    {
        private delegate long TransitionFunction(long input, bool inverse = false);
        private struct TransitionInfo
        {
            private long source;
            private long dest;
            private long range;
            private long modifier;

            public TransitionInfo(long dest, long source, long range)
            {
                this.source = source;
                this.dest = dest;
                this.modifier = dest - source;
                this.range = range;
            }

            public bool TryTransition(long input, out long output)
            {
                output = input;
                if(input >= source && input <= source + range - 1)
                {
                    output += modifier;
                    return true;
                }
                return false;
            }

            public bool TryInverseTransition(long input, out long output)
            {
                output = input;
                if(input >= dest && input <= dest + range - 1)
                {
                    output -= modifier;
                    return true;
                }
                return false;
            }
        }

#pragma warning disable CS8602
#pragma warning disable CS8618
        private static TransitionFunction SeedToSoil;
        private static TransitionFunction SoilToFert;
        private static TransitionFunction FertToWater;
        private static TransitionFunction WaterToLight;
        private static TransitionFunction LightToTemp;
        private static TransitionFunction TempToHumid;
        private static TransitionFunction HumidToLoc;

        private static TransitionFunction SoilToSeed = (i, _) => SeedToSoil(i, true);
        private static TransitionFunction FertToSoil = (i, _) => SoilToFert(i, true);
        private static TransitionFunction WaterToFert = (i, _) => FertToWater(i, true);
        private static TransitionFunction LightToWater = (i, _) => WaterToLight(i, true);
        private static TransitionFunction TempToLight = (i, _) => LightToTemp(i, true);
        private static TransitionFunction HumidToTemp = (i, _) => TempToHumid(i, true);
        private static TransitionFunction LocToHumid = (i, _) => HumidToLoc(i, true);
#pragma warning restore CS8618
#pragma warning restore CS8602


        public static void Run()
        {
            IEnumerator<string> input = File.ReadLines("src/05/input.txt").GetEnumerator();
            input.MoveNext();
            List<long> seeds = input.Current.Substring(input.Current.IndexOf(':') + 2).Split(' ').Select(s => long.Parse(s)).ToList();
            input.MoveNext();
            
            SeedToSoil = ParseTransitionFunction(input);
            SoilToFert = ParseTransitionFunction(input);
            FertToWater = ParseTransitionFunction(input);
            WaterToLight = ParseTransitionFunction(input);
            LightToTemp = ParseTransitionFunction(input);
            TempToHumid = ParseTransitionFunction(input);
            HumidToLoc = ParseTransitionFunction(input);

            Part1(seeds);
            Part2(seeds);
        }

        private static void Part1(List<long> seeds)
        {
            long minLoc = long.MaxValue;

            foreach(long seed in seeds)
            {
                    long loc = HumidToLoc(
                                    TempToHumid(
                                        LightToTemp(
                                            WaterToLight(
                                                FertToWater(
                                                    SoilToFert(
                                                        SeedToSoil(seed)))))));

                    if(loc < minLoc)
                        minLoc = loc;
            }
            
            Console.WriteLine(minLoc);
        }

        private static void Part2(List<long> seeds)
        {
            for(long i = 0; true; i++)
            {
                long seed = SoilToSeed(
                                FertToSoil(
                                    WaterToFert(
                                        LightToWater(
                                            TempToLight(
                                                HumidToTemp(
                                                    LocToHumid(i)))))));
                if(ValueInSeedRanges(seeds, seed))
                {
                    Console.WriteLine(i);
                    return;
                }
            }
        }

        private static bool ValueInSeedRanges(List<long> seeds, long check)
        {
            for(int i = 0; i < seeds.Count(); i+=2)
            {
                if(check >= seeds[i] && check <= seeds[i] + seeds[i+1] - 1)
                    return true;
            }
            return false;
        }

        private static TransitionFunction ParseTransitionFunction(IEnumerator<string> input)
        {
            input.MoveNext();

            List<TransitionInfo> transitions = new();
            while(input.MoveNext())
            {
                if(String.IsNullOrEmpty(input.Current))
                    break;
                transitions.Add(BuildTransitionInfo(input.Current));
            }
            return BuildTransitionFunction(transitions);
        }


        private static TransitionInfo BuildTransitionInfo(string input)
        {
            string[] args = input.Split(' ');
            return new TransitionInfo(long.Parse(args[0]), long.Parse(args[1]), long.Parse(args[2]));
        }

        private static TransitionFunction BuildTransitionFunction(List<TransitionInfo> transitions)
        {
            return (input, inverse) =>
            {
                foreach(TransitionInfo info in transitions)
                {
                    if(!inverse && info.TryTransition(input, out long output))
                        return output;
                    if(inverse && info.TryInverseTransition(input, out long inverseOutput))
                        return inverseOutput;
                }
                return input;
            };
        }

    }       
}