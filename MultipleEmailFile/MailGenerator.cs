﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultipleEmailFile
{
    internal class MailGenerator
    {
        private readonly string alphabet = "abcdefghi1234567890";
        public int MailPerFile { get; set; }
        public int NumberOfFiles { get; set; }

        public Dictionary<string, int> Occurences { get; set; }
        public string DuplicateEmails { get; set; }

        public MailGenerator()
        {
            MailPerFile = 10;
            NumberOfFiles = 5;
            DuplicateEmails = string.Empty;
            Occurences = new Dictionary<string, int>();
        }

        public MailGenerator(int numberOfMailPerFile, int numberOfFiles)
        {
            MailPerFile = numberOfMailPerFile;
            NumberOfFiles = numberOfFiles;
            DuplicateEmails = string.Empty;
            Occurences = new Dictionary<string, int>();
        }

        public string[] GenerateEmail()
        {
            string username, email = string.Empty;
            string[] domains = { "google.com", "yahoo.com", "yandex.ru", "outlook.com", "hotmail.com", "ventionteams.com" };
            
            string[] emails = new string[MailPerFile];
            Random rand = new Random();
            for (int i = 0; i < emails.Length - 1; i++)
            {
                username = alphabet.Substring(0, rand.Next(alphabet.Length));
                rand.Shuffle(username.ToCharArray());
                email = $"{username}@{domains[rand.Next(domains.Length - 1)]}";
                emails[i] = email;
            }
            return emails;
        }

        public void SaveToFile(string[] emails, string fileName)
        {
            //string content = JsonSerializer.Serialize<string[]>(emails);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter s = new StreamWriter(fs))
            {
                foreach (string email in emails)
                {
                    s.WriteLine(email);
                }
            }
            
        }

        public void LoadMailFromFile()
        {
            Dictionary<string, int> occurences = new Dictionary<string, int>();
            string filePath;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= NumberOfFiles; i++)
            {
                filePath = $"email-{i}";
                if (File.Exists(filePath))
                {
                    var emails = File.ReadAllLines(filePath);
                    foreach (var email in emails)
                    {
                        if (occurences.ContainsKey(email))
                        {
                            occurences[email]++;
                        }
                        else
                        {
                            occurences.Add(email, 1);
                        }

                        if (occurences[email] == NumberOfFiles)
                        {
                            sb.Append($"{email},");
                        }
                    }
                }
            }
            Occurences = occurences;
            DuplicateEmails = sb.ToString();
        }

        public static bool isValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        
    }
}
