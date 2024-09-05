using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MultipleEmailFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static string[] generateEmail()
        {
            string username, email = string.Empty;
            string[] domains = { "google.com", "yahoo.com", "yandex.ru", "outlook.com", "hotmail.com", "ventionteams.com" };
            string alphabet = "abcdefghi1234567890";
            string[] emails = new string[1000];
            Random rand = new Random();
            for (int i = 0; i < emails.Length - 1; i++)
            {
                username = alphabet.Substring(0, rand.Next(alphabet.Length));
                rand.Shuffle(username.ToCharArray());
                email = $"{username}@{domains[rand.Next(domains.Length - 1)]}";
                emails[0] = email;
            }
            return emails;
        }

        public static void saveToFile(string[] emails, string fileName)
        {
            string content = JsonSerializer.Serialize(emails);
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter s = new StreamWriter(fs))
            {
                s.WriteLine(content);
            }
        }

        public static string[] loadMails()
        {
            return new string[] { };
        }

        public static bool isValidEmail(string email) {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
