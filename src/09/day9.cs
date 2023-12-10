using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2022
{
    class Day9
    {
        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/09/input.txt");
            Console.WriteLine(AggregateOn(input, GetNextNumber)); // part 1
            Console.WriteLine(AggregateOn(input, GetPreviousNumber)); // part 2
        }

        private delegate long SequenceGenerator(List<long> nums);
        private static long AggregateOn(IEnumerable<string> input, SequenceGenerator generator)
        {
            return input.Aggregate(0L, (current, value) => {
                List<long> numbers = value.Split(' ').Select(l => long.Parse(l)).ToList();
                current += generator(numbers);
                return current;
            });
        }

        private static long GetNextNumber(List<long> nums)
        {
            List<long> diffs = GetDiffs(nums);
            if(diffs.All(l => l == 0)) return nums[nums.Count() - 1];
            return nums[nums.Count() - 1] + GetNextNumber(diffs);
        }

        private static long GetPreviousNumber(List<long> nums)
        {
            List<long> diffs = GetDiffs(nums);
            if(diffs.All(l => l == 0)) return nums[0];
            return nums[0] - GetPreviousNumber(diffs);
        }

        private static List<long> GetDiffs(List<long> nums)
        {
            List<long> diffs = new();
            for(int i = 0; i < nums.Count() - 1; i++)
            {
                diffs.Add(nums[i+1] - nums[i]);
            }
            return diffs;
        }
    }
}