using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography2
{
    internal interface ICryptography
    {
        void GenerateKey();
        string Encrypt(string plaintext);
        string Decrypt(string plaintext);
    }
}
