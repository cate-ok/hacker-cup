using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Round0.B
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("C:\\code\\HackerCup\\Round0.B\\input.txt");
            StreamWriter sw = new StreamWriter("C:\\code\\HackerCup\\Round0.B\\output.txt");

            int gamesCount = Int32.Parse(sr.ReadLine());
            string[][] games = new string[gamesCount][];

            for (var i = 0; i < gamesCount; i++)
            {
                var gameSize = Int32.Parse(sr.ReadLine());
                games[i] = new string[gameSize];

                for (var j = 0; j < gameSize; j++)
                {
                    var line = sr.ReadLine();
                    games[i][j] = line;
                }
            }

            for (var i = 0; i < gamesCount; i++)
            {
                string line = $"Case #{i + 1}: {GetGameResult(games[i])}";
                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }

        private static string GetGameResult(string[] game)
        {
            int gameSize = game.Length;
            List<int> minX = new List<int>();
            HashSet<string> setOfCells = new HashSet<string>();

            // rows
            for (var i = 0; i < gameSize; i++)
            {
                var xToAdd = 0;
                var cellsToFill = new List<string>();
                for (var j = 0; j < gameSize; j++)
                {
                    if (game[i][j] == 'O')
                    {
                        xToAdd = 0;
                        break;
                    }
                    if (game[i][j] == '.')
                    {
                        xToAdd++;
                        var cellName = $"{i},{j}";
                        cellsToFill.Add(cellName);
                    }
                }
                if (xToAdd != 0)
                {
                    minX.Add(xToAdd);
                    var oneString = String.Join("/", cellsToFill);
                    setOfCells.Add(oneString);
                }
            }

            // columns
            for (var j = 0; j < gameSize; j++)
            {
                var xToAdd = 0;
                var cellsToFill = new List<string>();
                for (var i = 0; i < gameSize; i++)
                {
                    if (game[i][j] == 'O')
                    {
                        xToAdd = 0;
                        break;
                    }
                    if (game[i][j] == '.')
                    {
                        xToAdd++;
                        var cellName = $"{i},{j}";
                        cellsToFill.Add(cellName);
                    }
                }
                if (xToAdd != 0)
                {
                    minX.Add(xToAdd);
                    var oneString = String.Join("/", cellsToFill);
                    setOfCells.Add(oneString);
                }
            }

            if (minX.Count == 0)
            {
                return "Impossible";
            }
            else
            {
                var result1 = minX.Min();

                var setIfCellsList = setOfCells.ToList();
                var filteredList = setIfCellsList.Where(x => x.Split("/").Length == result1).ToList();
                var result2 = filteredList.Count;

                return $"{result1} {result2}";
            }
        }
    }
}
