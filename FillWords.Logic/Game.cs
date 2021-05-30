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

            public Field(int size)
            {
                Words = GetWords(size);
                
            }
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
                    this.letters = new Letter[coordsStrings.Length];
                    string content = WordDictionary.GetWord(letters.Length).ToUpper();
                    for (int i = 0; i < coordsStrings.Length; i++)
                    {
                        string coord = coordsStrings[i];
                        string[] validCoords = coord.Split('.');
                        int x = int.Parse(validCoords[0]);
                        int y = int.Parse(validCoords[1]);
                        this.coordinates[i] = new Coordinates(x, y);
                        letters[i] = new Letter(content[i]);
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
                internal Letter(char symbol)
                {
                    Content = symbol;
                }
            }
            private Word[] GetWords(int size)
            {
                if (!File.Exists(Data.Path + "\\templates\\" + size.ToString() + "v1.csv")) throw new Exception("The target file doesn't exist!");
                string[] templates = Array.FindAll(Directory.GetFiles(Data.Path + "\\templates"), a => a.StartsWith(Data.Path + "\\templates\\" + size.ToString()));
                string chosenTemplate = templates[Data.Randomizer.Next(0, templates.Length)];
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
