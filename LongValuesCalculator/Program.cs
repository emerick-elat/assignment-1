namespace LongValuesCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long x, y;
            char op;
            Console.WriteLine("Hello, World!");
            while (true)
            {
                Console.WriteLine("Enter value");
                if (long.TryParse(Console.ReadLine(), out x)
                    && char.TryParse(Console.ReadLine(), out op)
                    && long.TryParse(Console.ReadLine(), out y))
                {
                    switch (op)
                    {
                        case '+':
                            Console.WriteLine("{0} + {1} = {2}", x, y, add(x, y));
                            break;
                        case '-':
                            Console.WriteLine("{0} - {1} = {2}", x, y, substract(x, y));
                            break;
                        case '*':
                            Console.WriteLine("{0} x {1} = {2}", x, y, multiply(x, y));
                            break;
                        case '/':
                            try
                            {
                                Console.WriteLine("{0} / {1} = {2}", x, y, divide(x, y));
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

        private static long add(long value1, long value2) => value1 + value2;

        private static long substract(long value1, long value2) => value1 - value2;

        private static long multiply(long value1, long value2) => value1 * value2;

        private static long divide(long value, long divisor)
        {
            if (divisor == 0)
            {
                throw new ArgumentException(nameof(divisor), "Cannot divide by 0");
            }
            return value / divisor;
        }
    }
}
