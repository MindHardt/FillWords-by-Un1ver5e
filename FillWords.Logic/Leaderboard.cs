using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FillWords.Logic
{
    public static class Data
    {
        public static string Path = Environment.CurrentDirectory;
        public static Random Randomizer = new Random();
    }
    class User
    {
        public static List<User> LoadedUsers;

        public readonly string Name;
        public int Score { get; private set; }

        public User(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public void AddScore(Game.Field.Word word)
        {
            Score += word.Length;
        }
        public static void LoadLeaderboard()
        {
            LoadedUsers = new List<User>();
            if (!File.Exists(Data.Path + "\\Leaderboard.csv")) return;
            string[] users = File.ReadAllLines(Data.Path + "\\Leaderboard.csv");
            foreach (string s in users)
            {
                string[] parts = s.Split(',');
                LoadedUsers.Add(new User(parts[0], int.Parse(parts[1])));
            }
            return;
        }
    }
}
