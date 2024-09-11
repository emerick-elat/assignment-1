using System.Text;

namespace ReadText
{
    internal class Program
    {
        public static Stack<string>? texts { get; set; }
        public static string? filename { get; set; }
        public Program()
        {
             texts = new Stack<string>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please, Type your the name of the file");
            filename = Console.ReadLine();
            generateText();
            readText();
            writeText();
        }

        public static void readText()
        {
            Console.WriteLine("Loading the file...");
            string path = $"{filename}.txt";
            string? line;
            texts = new Stack<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                
                while ((line = reader.ReadLine()) != null)
                {
                    texts.Push(line);
                }
            }
        }

        public static void writeText()
        {
            Console.WriteLine("Here is the content of the file in reverse order");
            while (texts?.Count > 0)
            {
                Console.WriteLine(ReverseString(texts.Pop()));
            }
        }

        public static void generateText()
        {
            Console.WriteLine($"Generationg the file {filename}.txt");
            string path = $"{filename}.txt";
            

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
 
                for (int i = 1; i <= 10; i++)
                {
                    writer.WriteLine($"This is the mail No {i}");
                }
            }

            Console.WriteLine("File generated successfully.");
        }

        public static string ReverseString(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                sb.Append(str[i]);
            }
            return sb.ToString();
        }

    }
}
