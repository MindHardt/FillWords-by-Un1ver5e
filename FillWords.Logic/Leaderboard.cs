using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace FillWords.Logic
{
    public static class Data
    {
        public static string Path = Environment.CurrentDirectory;
        public static Random Randomizer = new Random();
    }
    public class User
    {
        public static List<User> LoadedUsers;


        public readonly string Name;
        public int Score { get; private set; } = 0;


        private User(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public User(string name) => Name = name;


        public void AddScore(int score) => Score += score;

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
            LoadedUsers = LoadedUsers.OrderByDescending(u => u.Score).ThenBy(u => u.Name).ToList();
        }
        public static void SaveLeaderboard()
        {
            LoadedUsers = LoadedUsers.OrderByDescending(u => u.Score).ThenBy(u => u.Name).ToList();
            string[] userStrings = new string[LoadedUsers.Count];
            for (int i = 0; i < userStrings.Length; i++)
            {
                userStrings[i] = LoadedUsers[i].Name + "," + LoadedUsers[i].Score;
            }
            File.WriteAllLines(Data.Path + "\\Leaderboard.csv", userStrings);
        }
    }
}
