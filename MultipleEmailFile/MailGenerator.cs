﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultipleEmailFile
{
    internal class MailGenerator
    {
        private readonly string alphabet = "abcdefghi1234567890";

        public string[] GenerateEmail()
        {
            string username, email = string.Empty;
            string[] domains = { "google.com", "yahoo.com", "yandex.ru", "outlook.com", "hotmail.com", "ventionteams.com" };
            
            string[] emails = new string[1000];
            Random rand = new Random();
            for (int i = 0; i < emails.Length - 1; i++)
            {
                username = alphabet.Substring(0, rand.Next(alphabet.Length));
                rand.Shuffle(username.ToCharArray());
                email = $"{username}@{domains[rand.Next(domains.Length - 1)]}";
                emails[0] = email;
                Console.WriteLine("m");
            }
            return emails;
        }

        public void SaveToFile(string[] emails, string fileName)
        {
            string content = JsonSerializer.Serialize<string[]>(emails);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter s = new StreamWriter(fs))
            {
                s.WriteLine(content);
            }
        }
        public void LoadMailFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var emails = File.ReadAllLines(filePath);
                foreach (var email in emails)
                {
                    Console.WriteLine(email);
                }
            }
        }

        public static bool isValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }


    }
}
