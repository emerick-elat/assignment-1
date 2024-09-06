using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Cryptography2
{
    internal class Cryptography
    {
        private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private Dictionary<char, char> encryptKey;
        private Dictionary<char, char> decryptKey;

        public Cryptography()
        {
            encryptKey = new Dictionary<char, char>();
            decryptKey = new Dictionary<char, char>();
        }

        public void GenerateKey()
        {
            var random = new Random();
            var shuffledAlphabet = alphabet.OrderBy(c => random.Next()).ToArray();
            /*for (var c = 'a'; c <= 'z'; ++c) { 
                char.ToU
            }

            for (var c = 'A'; c <= 'Z'; ++c) { }*/
            for (int i = 0; i < alphabet.Length; i++)
            {
                encryptKey[alphabet[i]] = shuffledAlphabet[i];
                decryptKey[shuffledAlphabet[i]] = alphabet[i];
            }
            SaveKeyToFile("key.txt");
        }

        private void SaveKeyToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonSerializer.Serialize(encryptKey));
            }
        }

        private void LoadKeyFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string key = reader.ReadLine();
                    encryptKey = JsonSerializer.Deserialize<Dictionary<char, char>>(key);
                }
            }
            else
            {
                throw new FileNotFoundException(nameof(filePath));
            }
        }

        private bool ValidateKey()
        {
            if (encryptKey.Count != alphabet.Length || decryptKey.Count != alphabet.Length)
            {
                return false;
            }
            return true;
        }

        public string Encrypt(string plaintext)
        {
            plaintext = plaintext.ToUpper();
            var ciphertext = string.Empty;
            foreach (var ch in plaintext)
            {
                if (encryptKey.ContainsKey(ch))
                {
                    ciphertext += encryptKey[ch];
                }
                else
                {
                    ciphertext += ch;
                }
            }
            return ciphertext;
        }

        public string Decrypt(string ciphertext)
        {
            var plaintext = string.Empty;
            foreach (var ch in ciphertext)
            {
                if (decryptKey.ContainsKey(ch))
                {
                    plaintext += decryptKey[ch];
                }
                else
                {
                    plaintext += ch;
                }
            }
            return plaintext;
        }
    }
}
