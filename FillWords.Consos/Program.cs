using System;
using FillWords.Logic;

namespace FillWords.Consos
{
    static class Program
    {
        public static Screen ActiveScreen { get; private set; }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

        }
    }
}
