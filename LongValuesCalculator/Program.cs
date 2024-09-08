using System.Text;

namespace LongValuesCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? inputText;
            LongCalculator calculator = new LongCalculator();
            while (true)
            {
                Console.WriteLine("Type and operation (+, -, *, /) with large numbers on the same line and press enter to run");
                do
                {
                    inputText = Console.ReadLine();
                } 
                while (inputText is null);

                (string x, char? op, string y) = calculator.ParseInput(inputText);
                   
                if (x.Length > 0 && y.Length > 0 && op is not null)
                {
                    switch (op)
                    {
                        case '+':
                            Console.WriteLine("{0} + {1} = {2}", x, y, calculator.Add(x, y));
                            break;
                        case '-':
                            Console.WriteLine("{0} - {1} = {2}", x, y, calculator.Substract(x, y));
                            break;
                        case '*':
                            Console.WriteLine("{0} x {1} = {2}", x, y, calculator.Multiply(x, y));
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
