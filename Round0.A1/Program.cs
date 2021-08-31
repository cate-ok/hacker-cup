using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Round0.A1
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("C:\\code\\HackerCup\\Round0.A1\\input.txt");
            StreamWriter sw = new StreamWriter("C:\\code\\HackerCup\\Round0.A1\\output.txt");

            string[] s = sr.ReadToEnd().Split();

            int n = Int32.Parse(s[0]);

            for (var i = 1; i < n + 1; i++)
            {
                string line = $"Case #{i}: {MakeConsistent(s[i])}";
                sw.WriteLine(line);
            }
            sr.Close();
            sw.Close();
        }

        public static int MakeConsistent(string str)
        {
            List<int> times = new List<int>();
            char primeLetter;
            int sec;

            for (var i = 0; i < str.Length; i++)
            {
                primeLetter = str[i];
                sec = getSec(str, primeLetter);
                times.Add(sec);
            }

            primeLetter = 'A';
            sec = getSec(str, primeLetter);
            times.Add(sec);

            primeLetter = 'B';
            sec = getSec(str, primeLetter);
            times.Add(sec);

            return times.Min();
        }

        private static int getSec(string str, char primeLetter)
        {
            int sec = 0;
            for (var j = 0; j < str.Length; j++)
            {
                if (str[j] == primeLetter) continue;

                if (IsVowel(primeLetter) == IsVowel(str[j]))
                {
                    sec += 2;
                }
                else
                {
                    sec += 1;
                }
            }
            return sec;
        }

        private static bool IsVowel(char letter)
        {
            char[] vowels = { 'A', 'E', 'I', 'O', 'U' };
            return Array.Exists(vowels, vowel => vowel == letter);
        }
    }
}
