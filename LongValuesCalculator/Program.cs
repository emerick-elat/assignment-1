using System.Text;

namespace LongValuesCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? inputText;
            char? op;
            LongCalculator calculator = new LongCalculator();
            while (true)
            {
                Console.WriteLine("Type and operation (+, -, *, /) with large numbers on the same line and press enter to run");
                do
                {
                    inputText = Console.ReadLine();
                } 
                while (inputText is null);

                (string n1, op, string n2) = calculator.ParseInput(inputText);
                   
                if (n1.Length > 0 && n2.Length > 0 && op is not null)
                {
                    char[] x = n1.ToCharArray();
                    char[] y = n2.ToCharArray();
                    switch (op)
                    {
                        case '+':
                            Console.WriteLine("{0} + {1} = {2}", n1, n2, calculator.Add(x, y));
                            break;
                        case '-':
                            Console.WriteLine("{0} - {1} = {2}", n1, n2, calculator.Substract(x, y));
                            break;
                        case '*':
                            Console.WriteLine("{0} x {1} = {2}", n1, n2, calculator.Multiply(x, y));
                            break;
                        case '/':
                            (string q, string r) = calculator.Divide(x, y);
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

    }
}
