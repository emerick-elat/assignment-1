namespace Cryptography
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        private static string encrypt(string str, char[] key)
        {
            return str;
        }

        private static string decrypt(string str, char[] key)
        {
            return str;
        }

        private static char[] generateKey()
        {
            char[] key = new char[] { };
            return key;
        }

        private static void saveKey(string key)
        {

        }

        public static char[] loadKey()
        {
            return new char[] { };
        }

        private static bool isKeyValid()
        {
            return true;
        }
    }
}
