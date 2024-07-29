using CalculatorApp;
using System;

class Program
{
    static void Main(string[] args)
    {
        var calculator = new StringCalculator();
        while (true)
        {
            Console.WriteLine("Enter numbers to add (or type 'exit' to quit):");
            var input = Console.ReadLine();
            if (input?.ToLower() == "exit")
            {
                break;
            }

            try
            {
                var result = calculator.Add(input);
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
