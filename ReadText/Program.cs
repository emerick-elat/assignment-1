namespace ReadText
{
    internal class Program
    {
        public static Stack<string>? texts { get; set; }
        public Program()
        {
             texts = new Stack<string>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please, Type your text line by line!");
            readText();
            writeText();
        }

        public static void readText()
        {
            string text = string.Empty;
            do
            {
                text = Console.ReadLine();
                texts?.Push(text);
            }
            while (text != "#");
        }

        public static void writeText()
        {
            while (texts?.Count > 0)
            {
                Console.WriteLine(texts.Pop());
            }
        }

    }
}
