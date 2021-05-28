using System;
using System.Collections.Generic;
using System.IO;

namespace FillWords.Logic
{
    public class Game
    {
        public class Field
        {
            public Letter[,] Grid { get; private set; }
            private Word[] Words;

            public class Word
            {
                public readonly Letter[] letters;
                private readonly Coordinates[] coordinates;
                public int Length
                {
                    get
                    {
                        return letters.Length;
                    }
                }
                internal Word(string coordinates)
                {
                    string[] coordsStrings = coordinates.Split(',');
                    this.coordinates = new Coordinates[coordsStrings.Length];
                    int i = 0;
                    foreach (string coord in coordsStrings)
                    {
                        int x = int.Parse(coord.Substring(0, coord.IndexOf('.')));
                        int y = int.Parse(coord[coord.IndexOf('.')..]);
                        this.coordinates[i] = new Coordinates(x, y);
                    }
                }
            }

            public class Coordinates
            {
                public readonly int X;
                public readonly int Y;
                internal Coordinates(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }

            public class Letter
            {
                public char Content { get; }
                public LetterState State;

                public enum LetterState
                {
                    Default,
                    Selected,
                    Guessed
                }
            }
            private Word[] GetWords(int size)
            {
                if (!File.Exists(Data.Path + "\\templates\\" + size + "v1")) throw new Exception("The target file doesn't exist!");
                string[] templates = Array.FindAll(Directory.GetFiles(Data.Path + "\\templates"), a => a.StartsWith(Data.Path + "\\templates\\" + size));
                string chosenTemplate = templates[Data.Randomizer.Next(1, templates.Length)];
                string[] words = File.ReadAllLines(chosenTemplate);
                List<Word> wordList = new List<Word>();
                foreach (string s in words)
                {
                    wordList.Add(new Word(s));
                }
                return wordList.ToArray();
            }
        }
    }
    
}
