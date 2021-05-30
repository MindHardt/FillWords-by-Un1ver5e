using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FillWords.Logic;

namespace FillWords.Consos
{
    public class Screen
    {
        private static ConsoleColor defaultColor = ConsoleColor.Cyan;
        private static ConsoleColor selectedColor = ConsoleColor.White;
        
        private int selectedRow
        {
            get
            {
                return sr;
            }
            set
            {
                sr = value > 0 && value <= buttons.Length ? value : sr ;
                selectedButton = buttons[sr - 1];
            }
        }
        private int sr;
        private Button selectedButton;
        private object[] contents;
        private Button[] buttons;
        public Action<ConsoleKey> KeyAction { get; }

        //ЭКЗЕМПЛЯРЫ ЭКРАНОВ
        public static Screen MainScreen = new Screen
            (
            new object[]{
                "FILLWORDS",
                "by Un1ver5e",
                "",
                new Button("NEW GAME", () => Console.Beep()),
                "",
                new Button("CONTINUE", () => Console.Beep()),
                "",
                new Button("LEADERBOARD", () => Program.ActiveScreen = LeaderBoardScreen),
                "",
                new Button("EXIT", () => Environment.Exit(0))
            },
            (CK) => 
            {
                switch (CK) 
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow: MainScreen.selectedRow--; break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow: break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow: MainScreen.selectedRow++; break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow: break;
                    case ConsoleKey.Enter: MainScreen.selectedButton.action(); break;
                    default: break;
                }
            }
            );
        public static Screen LeaderBoardScreen = new Screen
            (
            new object[]{
                "Leaders",
                "",
                GetLeaders().AsString(),
                "",
                new Button("Return", () => Program.ActiveScreen = MainScreen)
            },
            (CK) =>
            {
                switch (CK)
                {
                    case ConsoleKey.Enter: LeaderBoardScreen.selectedButton.action(); break;
                    default: break;
                }
            }
            );
        private Screen(object[] items, Action<ConsoleKey> keyAction)
        {
            contents = items;
            List<Button> tempButtons = new List<Button>();
            foreach (object obj in items)
            {
                if (obj is Button) tempButtons.Add((Button)obj);
            }
            buttons = tempButtons.ToArray();
            selectedRow = 1;
            KeyAction = keyAction;
        }
        public void Display()
        {
            Console.SetCursorPosition(0, 0);
            foreach (object obj in contents)
            {
                DisplayElement(obj);
            }
        }
        private void DisplayElement(object item)
        {
                int width = Console.WindowWidth;
                string placeholder = item.ToString().Length <= width ? new string(' ', (width - item.ToString().Length) / 2) : "";
                Console.Write(placeholder);
                Console.BackgroundColor = (item == selectedButton) ? selectedColor : defaultColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(item.ToString());
                Console.ResetColor();
        }
        internal class Button
        {
            private string content;
            internal Action action;

            internal Button(string content, Action action)
            {
                this.content = content;
                this.action = action;
            }
            public override string ToString()
            {
                return content;
            }
        }
        private static string[] GetLeaders()
        {
            User.LoadLeaderboard();
            string[] userStrings = new string[User.LoadedUsers.Count];
            for (int i = 0; i < userStrings.Length; i++)
            {
                int l = i.ToString().Length + User.LoadedUsers[i].Name.Length + User.LoadedUsers[i].Score.ToString().Length;
                userStrings[i] = i + 1 + ". " + User.LoadedUsers[i].Name + new string('.', Console.WindowWidth - l - 2) + User.LoadedUsers[i].Score;
            }
            return userStrings;
        }
    }
    internal static class ArrayStringExtension
    {
        internal static string AsString(this string[] ss)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string s in ss)
            {
                stringBuilder.AppendLine(s);
            }
            return stringBuilder.ToString();
        }
    }
}