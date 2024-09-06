using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MultipleEmailFile
{
    internal class Program
    {
        private const int MAX = 5;
        static void Main(string[] args)
        {   
            MailGenerator gen = new MailGenerator();
            string basepath = "emails";
            for (int i = 1; i < MAX; i++) {
                gen.SaveToFile(gen.GenerateEmail(), $"{basepath}/email-{i}");
            }
        }
    }
}
