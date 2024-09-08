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
        string Add(string value1, string value2);
        string Substract(string value1, string value2);
        string Multiply(string value1, string value2);
        (string, string) Divide(string value1, string value2);
    }
}
