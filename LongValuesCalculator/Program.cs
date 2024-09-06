using System.Text;

namespace LongValuesCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char? op = null;
            string? inputText;
            string numbers = "0123456789";
            
            while (true)
            {
                op = null;
                Console.WriteLine("Type and operation (+, -, *, /) with large numbers on the same line and press enter to run");
                do
                {
                    inputText = Console.ReadLine();
                }
                while (inputText is null);
                StringBuilder n1 = new StringBuilder();
                StringBuilder n2 = new StringBuilder();
                foreach (char c in inputText)
                {
                    if (c == ' ') continue;
                    if (numbers.Contains(c) && op is null)
                    {
                        n1.Append(c);
                    }
                    else if (!numbers.Contains(c))
                    {
                        op = c;
                    }
                    else if (numbers.Contains(c) && op is not null)
                    {
                        n2.Append(c);
                    }
                }

                if (n1.Length > 0 && n2.Length > 0 && op is not null)
                {
                    char[] x = n1.ToString().ToCharArray();
                    char[] y = n2.ToString().ToCharArray();
                    switch (op)
                    {
                        case '+':
                            Console.WriteLine("{0} + {1} = {2}", n1, n2, add(x, y));
                            break;
                        case '-':
                            Console.WriteLine("{0} - {1} = {2}", n1, n2, substract(x, y));
                            break;
                        case '*':
                            Console.WriteLine("{0} x {1} = {2}", n1, n2, multiply(x, y));
                            break;
                        case '/':
                            (string q, string r) = divide(x, y);
                            Console.WriteLine("q = {0}", q);
                            if(!string.IsNullOrEmpty(r)) Console.WriteLine("r = {0}", r);
                            break;
                        default:
                            Console.WriteLine("Invalid operation");
                            break;
                    }
                }
            }
        }

        private static string add(char[] value1, char[] value2)
        {
            StringBuilder sb = new StringBuilder();
            int r1, r2, r, carry = 0;
            
            for (int i = value1.Length - 1, j = value2.Length - 1; i >= 0 || j >=0 ; i--, j--)
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
        
        private static string substract(char[] value1, char[] value2)
        {
            StringBuilder sb = new StringBuilder();
            int r1, r2, r, carry = 0;
            char sign = new char();
            char[] tmp;
            if (IsGreaterOrEqual(value2, value1))
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
                if (r1 >= (r2+carry))
                {
                    r = r1 - (r2 + carry);
                    carry = 0;
                } else
                {
                    r = (r1 + 10) - (r2 + carry);
                    carry = 1;
                }
                
                if (r == 0 && i == 0) break;
                sb.Insert(0, r);
            }
            if (carry > 0) sb.Insert(0, carry);
            string result = sign == '-' ? $"{sign}{sb.ToString()}" : sb.ToString();
            return result;
        }
        
        private static string multiply(char[] value1, char[] value2)
        {
            char[][] jaggedArray = new char[value2.Length][];
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
                jaggedArray[k++] = sb.ToString().ToCharArray();
            }
            string result = "0";
            foreach (char[] a in jaggedArray)
            {
                result = add(result.ToCharArray(), a);
            }
            return result;
        }

        public static (string, string) divide(char[] value1, char[] value2)
        {
            StringBuilder sb = new StringBuilder();
            (string q, string r) = divideBase(value1[0..value2.Length], value2);
            
            if (q[0] != '0') sb.Append(q);
            for (int i = value2.Length; i < value1.Length; i++)
            {   
                (q, r) = divideBase($"{r}{value1[i]}".ToCharArray(), value2);
                sb.Append(q);
            }
            return (sb.ToString(), r);
        }

        private static (string, string) divideBase(char[] value, char[] divisor)
        {
            char[] r = value[..];
            ulong q = 0;
            while (IsGreaterOrEqual(r, divisor))
            {
                r = substract(r, divisor).ToCharArray();
                q++;
            }

            return (q.ToString(), new string(r));
        }

        private static bool IsGreaterOrEqual(char[] x, char[] y)
        {
            if (y == null)
            {
                return true;
            }
            else if (x == null)
            {
                return false;
            }
            else if (x.Length > y.Length)
            {
                return true;
            }
            else if (y.Length > x.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] < y[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
