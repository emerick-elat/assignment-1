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
            
            Console.WriteLine("Type and operation with large numbers. Ex: 1574 + 5");
            while (true)
            {

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
                            try
                            {
                                Console.WriteLine("{0} / {1} = {2}", n1, n2, multiply(x, y));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
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
            string result = sb.ToString();
            return result;
        }
        
        private static string substract(char[] value1, char[] value2)
        {
            StringBuilder sb = new StringBuilder();
            int r1, r2, r, carry = 0;

            for (int i = value1.Length - 1, j = value2.Length - 1; i >= 0 || j >= 0; i--, j--)
            {
                r1 = i >= 0 ? (int)char.GetNumericValue(value1[i]) : 0;
                r2 = j >= 0 ? (int)char.GetNumericValue(value2[j]) : 0;

                if (r1 >= r2)
                {
                    r = r1 - (r2 + carry);
                    carry = 0;
                } else
                {
                    r = (r1 + 10) - (r2 + carry);
                    carry = 1;
                }
                
                sb.Insert(0, r);
            }
            string result = sb.ToString();
            return result;
        }
        
        private static string multiply(char[] value1, char[] value2)
        {
            return "not implemented";
        }

        private static string divide(long value, long divisor)
        {
            return "not implemented";

        }
    }
}
