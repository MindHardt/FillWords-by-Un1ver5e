using System;
using FillWords.Logic;

namespace FillWords.Consos
{
    static class Program
    {
        public static Screen ActiveScreen
        {
            get
            {
                return acs;
            }
            set
            {
                acs = value;
                Console.Clear();
            }
        }
        private static Screen acs;
        public static User ActiveUser;

        static void Main(string[] args)
        {
            Console.Title = "FillWords by Un1ver5e";
            Console.CursorVisible = false;
            ActiveScreen = Screen.MainScreen;
            while (true)
            {
                ActiveScreen.Display();
                var CK = Console.ReadKey(true).Key;
                ActiveScreen.KeyAction(CK);
                
            }
        }
    }
}
