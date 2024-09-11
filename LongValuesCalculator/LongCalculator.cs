using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongValuesCalculator
{
    internal class LongCalculator : ILongCalculator
    {
        private string Operations { get; set; }

        public LongCalculator()
        {
            Operations = "+-*/";
        }

        public string Add(string value1, string value2)
        {
            StringBuilder sb = new StringBuilder();
            int r1, r2, r, carry = 0;

            for (int i = value1.Length - 1, j = value2.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                r1 = i >= 0 ? (int)char.GetNumericValue(value1[i]) : 0;
                r2 = j >= 0 ? (int)char.GetNumericValue(value2[j]) : 0;

                r = r1 + r2 + carry;
                if (r > 9)
                {
                    r = r % 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
                sb.Insert(0, r);
            }
            if (carry > 0) sb.Insert(0, carry);
            string result = sb.ToString();
            return result;
        }

        public string Substract(string value1, string value2)
        {
            StringBuilder sb = new StringBuilder();
            int r1, r2, r, carry = 0;
            char sign = new char();
            string tmp;
            if (Compare(value2, value1) == CompareStatus.Greater)
            {
                sign = '-';
                tmp = value1;
                value1 = value2;
                value2 = tmp;
            }

            for (int i = value1.Length - 1, j = value2.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                r1 = i >= 0 ? (int)char.GetNumericValue(value1[i]) : 0;
                r2 = j >= 0 ? (int)char.GetNumericValue(value2[j]) : 0;
                if (r1 < 0 || r2 < 0) break;
                if (r1 >= (r2 + carry))
                {
                    r = r1 - (r2 + carry);
                    carry = 0;
                }
                else
                {
                    r = (r1 + 10) - (r2 + carry);
                    carry = 1;
                }

                if (r == 0 && i == 0) break;
                sb.Insert(0, r);
            }
            if (carry > 0) sb.Insert(0, carry);
            string result = sb.ToString();
            if (string.IsNullOrEmpty(result) || result == "0" || result == "00" || result == "000")
            {
                return "0";
            }
            else if (sign == '-')
            {
                return $"{sign}{result}";
            }
            else
            {
                return result;
            }
        }

        public string Multiply(string value1, string value2)
        {
            LongCalculator calculator = new LongCalculator();
            string[] jaggedArray = new string[value2.Length];
            int k = 0;
            string suff = "";
            for (int j = (value2.Length - 1); j >= 0; j--)
            {
                StringBuilder sb = new StringBuilder();
                int r1, r2, r, carry = 0;

                for (int i = value1.Length - 1; i >= 0; i--)
                {
                    r1 = (int)char.GetNumericValue(value1[i]);
                    r2 = (int)char.GetNumericValue(value2[j]);

                    r = (r1 * r2) + carry;
                    if (r > 9)
                    {
                        carry = r / 10;
                        r = r % 10;
                    }
                    else
                    {
                        carry = 0;
                    }
                    sb.Insert(0, r);
                }
                sb.Append(suff);
                if (carry > 0) sb.Insert(0, carry);
                suff += "0";
                jaggedArray[k++] = sb.ToString();
            }
            string result = "0";
            foreach (string a in jaggedArray)
            {
                result = calculator.Add(result, a);
            }
            return result;
        }

        public (string, string) Divide(string value1, string value2)
        {
            StringBuilder sb = new StringBuilder();
            (string q, string r) = divideBase(value1[0..value2.Length], value2);

            if (q[0] != '0') sb.Append(q);
            for (int i = value2.Length; i < value1.Length; i++)
            {
                if (r == "0" && value1[i] == '0')
                {
                    sb.Append("0");
                    continue;
                }
                /*if (r == "0" && value1[i] != '0')
                {

                }*/
                (q, r) = divideBase($"{r}{value1[i]}", value2);
                sb.Append(q);
            }
            return (sb.ToString(), r);
        }

        public static (string, string) divideBase(string value, string divisor)
        {
            string r = value;
            ulong q = 0;
            LongCalculator calculator = new LongCalculator();
            while (Compare(r, divisor) != CompareStatus.Less)
            {
                r = calculator.Substract(r, divisor);
                q++;
            }

            return (q.ToString(), new string(r));
        }

        private static CompareStatus Compare(string x, string y)
        {
            if (y == null)
            {
                return CompareStatus.Greater;
            }
            else if (x == null)
            {
                return CompareStatus.Less;
            }
            else if (x.Length > y.Length)
            {
                return CompareStatus.Greater;
            }
            else if (y.Length > x.Length)
            {
                return CompareStatus.Less;
            }
            else
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > y[i])
                    {
                        return CompareStatus.Greater;
                    }
                    else if (x[i] < y[i])
                    {
                        return CompareStatus.Less;
                    }
                }
            }
            return CompareStatus.Equal;
        }

        public (string, char?, string) ParseInput(string input)
        {
            char? op = null;
            while (input is null) ;
            StringBuilder n1 = new StringBuilder();
            StringBuilder n2 = new StringBuilder();
            foreach (char c in input)
            {
                if (c == ' ') continue;
                if (char.IsDigit(c) && op is null)
                {
                    n1.Append(c);
                }
                else if (!char.IsDigit(c))
                {
                    op = c;
                }
                else if (char.IsDigit(c) && op is not null)
                {
                    n2.Append(c);
                }
            }
            return (n1.ToString(), op, n2.ToString());
        }

        public enum CompareStatus { Greater, Equal, Less }
    }
}
