using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    /// <summary>
    /// A calculator for adding numbers provided in a delimited string format.
    /// </summary>
    public class StringCalculator
    {
        private int _callCount = 0;
        public event Action<string, int>? CalculateOccurred;

        /// <summary>
        /// Adds numbers provided in a delimited string format.
        /// </summary>
        /// <param name="numbers">A string containing numbers separated by delimiters.</param>
        /// <returns>The sum of the numbers in the string.</returns>
        /// <exception cref="ArgumentException">Thrown when negative numbers are found in the input string.</exception>
        /// <exception cref="FormatException">Thrown when nonnumeric numbers are found in the input string or wrong formatted string found.</exception>
        public int Calculate(string numbers, string operation = "+")
        {
            _callCount++;
            if (string.IsNullOrEmpty(numbers))
            {
                if (CalculateOccurred != null)
                {
                    CalculateOccurred(numbers, 0);
                }
                return 0;
            }
            var delimiters = new List<string>() { "\n" };
            string originalNumbers = numbers;
            if (numbers.StartsWith("//"))
            {
                // Getting index of \n to get delimiter
                var delimiterEndIndex = numbers.IndexOf('\n');
                // Getting substring which contains delimiter
                var delimiterString = numbers.Substring(2, delimiterEndIndex - 2);
                // Extracting Delimiters from string
                var delims = ExtractDelimitersFromString(delimiterString);
                delimiters.AddRange(delims);
                // Removing extra values which are there for delimiter config.
                numbers = numbers.Substring(delimiterEndIndex + 1);
            }
            else
            {
                delimiters.Add(",");
            }
            var parts = numbers.Split(delimiters.ToArray(), StringSplitOptions.TrimEntries);
            CheckNegativeNumbers(parts);
            int ans = 0;
            switch (operation)
            {
                case "*":
                    ans = Multiply(parts);
                    break;
                default:
                    ans = parts.Where(n => int.Parse(n) <= 1000).Sum(int.Parse);
                    break;
            }

            if (CalculateOccurred != null)
            {
                CalculateOccurred(originalNumbers, ans);
            }
            return ans;
        }

        /// <summary>
        /// Gets the count of how many times the Add method has been called.
        /// </summary>
        /// <returns>The number of times the Add method has been called.</returns>
        public int GetCalledCount()
        {
            return _callCount;
        }
        public int Multiply(string[] parts)
        {
            int ans = 1;
            foreach (var part in parts)
            {
                int num = int.Parse(part);
                if (num <= 1000)
                {
                    ans *= num;
                }
            }
            return ans;
        }

        /// <summary>
        /// Validates the numbers in the array, ensuring no negative numbers are present.
        /// </summary>
        /// <param name="numbers">An array of number strings to validate.</param>
        /// <exception cref="ArgumentException">Thrown when negative numbers are found in the input array.</exception>
        private void CheckNegativeNumbers(string[] numbers)
        {
            var negatives = numbers.Where(n => int.Parse(n) < 0).ToList();
            if (negatives.Any())
            {
                throw new ArgumentException("negatives not allowed: " + string.Join(", ", negatives));
            }
        }
        /// <summary>
        /// Extracts the delimiters from the delimiter string.
        /// </summary>
        /// <param name="delimiterString">String that contains Delimiters</param>
        /// <returns>List of Delimiters</returns>
        /// <exception cref="FormatException">Thrown if wrong format string is given</exception>
        private List<string> ExtractDelimitersFromString(string delimiterString)
        {
            var delimiters = new List<string>();

            if (delimiterString.Length > 1)
            {
                //check if it contains square bracket
                if (delimiterString.StartsWith('[') && delimiterString.EndsWith(']'))
                {
                    // removing first and last square brackets
                    var delimiterStringWithoutBrackets = delimiterString.Substring(1, delimiterString.Length - 2);
                    // if there are multiple delimiters then will split by brackets
                    var delims = delimiterStringWithoutBrackets.Split("][");
                    delimiters.AddRange(delims);
                }
                else
                {
                    throw new FormatException("String is in wrong format please confirm the format");
                }
            }
            else
            {
                delimiters.Add(delimiterString);
            }
            return delimiters;
        }
    }
}
