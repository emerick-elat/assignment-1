namespace Cryptography2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? text = string.Empty;
            var cipher = new Cryptography();
            cipher.GenerateKey();
            

            while (true)
            {
                Console.WriteLine("Type a text for encryption or decryption");
                text = Console.ReadLine();
                int choice = 1;
                Console.WriteLine("What do you want to do ?");
                Console.WriteLine("1-Encrypt Text");
                Console.WriteLine("2-Decrypt Text");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"Encrypted Text: {cipher.Encrypt(text)}");
                            break;
                        case 2:
                            Console.WriteLine($"Decrypted Text: {cipher.Decrypt(text)}");
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
