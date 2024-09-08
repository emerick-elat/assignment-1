using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongValuesCalculator
{
    internal interface ILongCalculator
    {
        (string, char?, string) ParseInput(string input);
        string Add(char[] value1, char[] value2);
        string Substract(char[] value1, char[] value2);
        string Multiply(char[] value1, char[] value2);
        (string, string) Divide(char[] value1, char[] value2);
    }
}
