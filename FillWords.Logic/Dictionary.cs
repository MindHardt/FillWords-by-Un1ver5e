using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FillWords.Logic
{
    static class WordDictionary
    {
        private static readonly string[][] dictionariesBySizes = Initialize();
        private static List<int>[] usedWords = new List<int>[] { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()};
        private static string[][] Initialize()
        {
            List<string>[] temps = new List<string>[] { new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>() };
            string[] words = File.ReadAllLines(Data.Path + "\\Dictionary.txt");
            foreach (string word in words)
            {
                if (word.Length > 2 && word.Length < 9) temps[word.Length - 3].Add(word);
            }
            return new string[][]
            {
                temps[0].ToArray(), temps[1].ToArray(), temps[2].ToArray(), temps[3].ToArray(), temps[4].ToArray(), temps[5].ToArray()
            };
        }
        public static string GetWord(int length)
        {
            int number;
            do
            {
                number = Data.Randomizer.Next(0, dictionariesBySizes[length - 3].Length);
            }
            while (usedWords[length - 3].Contains(number));
            usedWords[length - 3].Add(number);
            return dictionariesBySizes[length - 3][number];
        }
    }
}
