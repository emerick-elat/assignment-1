using System.Text;

namespace Cryptography
{
    internal class Program
    {
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        static void Main(string[] args)
        {
            string key = loadKey();
            if (string.IsNullOrEmpty(key))
            {
                key = generateKey();
                if (isKeyValid(key))
                {
                    saveKey(key);
                }
                else
                {
                    Console.WriteLine("The key is Invalid");
                    return;
                }
            }
            while (true)
            {
                Console.WriteLine("Type a text");
                string text = Console.ReadLine();
                int choice = 1;
                Console.WriteLine("What do you want to do ?");
                Console.WriteLine("1-Encrypt Text");
                Console.WriteLine("2-Decrypt Text");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"Encrypted Text: {encrypt(text, key)}");
                            break;
                        case 2:
                            Console.WriteLine($"Decrypted Text: {decrypt(text, key)}");
                            break;
                        default:
                            Console.WriteLine("Wrong choice");
                            break;
                    }
                }
            }
        }

        private static string encrypt(string str, string key)
        {
            int i;
            StringBuilder sbcipher = new StringBuilder();
            foreach (char c in str)
            {
                if (alphabet.Contains(c))
                {
                    i = c - 'a';
                    sbcipher.Append(key[i % 26]);
                }
                else
                {
                    sbcipher.Append(c);
                }
            }
            string cipher = sbcipher.ToString();
            return cipher;
        }

        private static string decrypt(string str, string key)
        {
            char[] letters = alphabet.ToCharArray();
            int step = 5;
            StringBuilder sbcipher = new StringBuilder();
            foreach (char c in str)
            {
                if (alphabet.Contains(c))
                {
                    sbcipher.Append(letters[(c - 'a' + step) % 26]);
                }
                else
                {
                    sbcipher.Append(c);
                }
            }
            string cipher = sbcipher.ToString();
            return cipher;
        }

        private static string generateKey()
        {
            Console.WriteLine("Generating the key...");
            
            char[] key = new char[alphabet.Length];
            Random rand = new Random();
            int step = rand.Next(alphabet.Length);
            for (int i = 0; i < key.Length; i++)
            {
                key[(i + step) % 26] = alphabet[i];
            }
            
            return new string(key);
            
        }

        private static void saveKey(string key)
        {
            Console.WriteLine("Saving the key...");
            string path = "key.txt";
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter s = new StreamWriter(fs))
            {
                s.WriteLine(key);
            }
            Console.WriteLine("Key saved succesfully");
        }

        public static string loadKey()
        {
            string? key = string.Empty, line, path = "key.txt";
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        key = line;
                    }
                }

                return key;
            }
            else
            {
                return string.Empty;
            }
        }

        private static bool isKeyValid(string key)
        {
            Console.WriteLine("Validating the key...");
            
            if (string.IsNullOrEmpty(key)) return false;
            if(key.Length != 26) return false;
            foreach (char c in alphabet)
            {
                if (!key.Contains(c)) return false;
            }
            Console.WriteLine("Validation completed");
            return true;
        }
    }
}
