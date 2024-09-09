using System.ComponentModel.DataAnnotations;
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

            MailGenerator gen = new MailGenerator();
            
            while (true)
            {
                int choice = 0;
                Console.WriteLine("WELCOME! What do you want to do ?");
                Console.WriteLine("1- Generate Email and Save in files");
                Console.WriteLine("2- Check for duplicates");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"Generating Emails...please wait");
                            for (int i = 1; i <= gen.NumberOfFiles; i++)
                            {
                                gen.SaveToFile(gen.GenerateEmail(), $"email-{i}");
                            }
                            Console.WriteLine("Emails succesfully Generated");
                            break;
                        case 2:
                            Console.WriteLine($"Checking duplicate Emails...please wait");
                            gen.LoadMailFromFile();
                            Console.WriteLine($"Duplicates: {gen.DuplicateEmails}");
                            break;
                        default:
                            Console.WriteLine("Wrong choice");
                            break;
                    }
                }
            }

        }
    }
}
