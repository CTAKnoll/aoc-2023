using System;
using System.Collections;
using System.Linq;
using System.IO;

namespace AOC2022
{
    class Day1
    {
        public static void Run()
        {
            IEnumerable<string> input = File.ReadLines("src/01/input.txt");
            Console.WriteLine(GetCalibrationValues(input, str => str));
            Console.WriteLine(GetCalibrationValues(input, LettersToNumbersWithBuffer));
        }

        private static int GetCalibrationValues(IEnumerable<string> input, Func<string, string> map)
        {  
            var sum = 0;
            foreach(var str in input)
            {
                var numeric = map(str).ToArray().Where(Char.IsDigit).ToArray();
                var combined = numeric[0] + "" + numeric[numeric.Count() - 1];
                sum += 10 * (int) Char.GetNumericValue(numeric[0]) + (int) Char.GetNumericValue(numeric[numeric.Count() - 1]);
            }
            return sum;
        }

        private static Dictionary<string, string> dict = new Dictionary<string, string>
        {
            //["zero"] = "0",
            ["one"] = "1",
            ["two"] = "2",
            ["three"] = "3",
            ["four"] = "4",
            ["five"] = "5",
            ["six"] = "6",
            ["seven"] = "7",
            ["eight"] = "8",
            ["nine"] = "9"
        };

        private static string LettersToNumbers(string str)
        {
            return dict.Aggregate(str, 
            (current, value) => current.Replace(value.Key, value.Key + value.Value));
        }

        private static string LettersToNumbersWithBuffer(string str)
        {
            for(int i = 0; i+4 < str.Length; i++)
            {
                var substr = str.Substring(i, 5);
                var match = dict.Keys.FirstOrDefault(key => substr.Contains(key));
                if(!string.IsNullOrEmpty(match))
                {
                    substr = substr.Replace(match, dict[match]);
                    str = str.Substring(0, i) + substr + str.Substring(i+5);
                } 
            }
            return str;
        }
    }
}