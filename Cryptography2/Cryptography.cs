using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            for (int i = 0; i < alphabet.Length; i++)
            {
                encryptKey[alphabet[i]] = shuffledAlphabet[i];
                decryptKey[shuffledAlphabet[i]] = alphabet[i];
            }
            SaveKeyToFile("key.txt");
        }

        public void LoadKeyFromFile(string filePath)
        {
            encryptKey.Clear();
            decryptKey.Clear();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2)
                {
                    char original = parts[0][0];
                    char substitute = parts[1][0];
                    encryptKey[original] = substitute;
                    decryptKey[substitute] = original;
                }
            }
            ValidateKey();
        }

        private void SaveKeyToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var kvp in encryptKey)
                {
                    writer.WriteLine($"{kvp.Key}:{kvp.Value}");
                }
            }
        }

        private void ValidateKey()
        {
            if (encryptKey.Count != alphabet.Length || decryptKey.Count != alphabet.Length)
            {
                throw new InvalidOperationException("Invalid key: not all letters have replacements or some letters are used more than once.");
            }
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
