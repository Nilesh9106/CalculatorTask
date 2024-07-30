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
        public event Action<string, int> AddOccured;

        /// <summary>
        /// Adds numbers provided in a delimited string format.
        /// </summary>
        /// <param name="numbers">A string containing numbers separated by delimiters.</param>
        /// <returns>The sum of the numbers in the string.</returns>
        /// <exception cref="ArgumentException">Thrown when negative numbers are found in the input string.</exception>
        /// <exception cref="FormatException">Thrown when nonnumeric numbers are found in the input string.</exception>
        public int Add(string numbers)
        {
            _callCount++;
            if (string.IsNullOrEmpty(numbers))
            {
                AddOccured(numbers, 0);
                return 0;
            }
            char delim = ',';
            string orgNumbers = numbers;
            if (numbers.StartsWith("//"))
            {
                // Getting index of \n to get delimiter
                var delimiterIndex = numbers.IndexOf('\n');
                // Getting substring which contains delimiter
                var delimiter = numbers.Substring(2, delimiterIndex - 2);
                // Removing extra values which are there for delimiter config.
                numbers = numbers.Substring(delimiterIndex + 1);
                // Changing delim to new delimiter
                delim = delimiter[0];
            }

            var delimiters = new[] { delim, '\n' };
            var parts = numbers.Split(delimiters, StringSplitOptions.TrimEntries);
            ValidateNumbers(parts);
            int ans = parts.Sum(int.Parse);
            AddOccured(orgNumbers, ans);
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

        /// <summary>
        /// Validates the numbers in the array, ensuring no negative numbers are present.
        /// </summary>
        /// <param name="numbers">An array of number strings to validate.</param>
        /// <exception cref="ArgumentException">Thrown when negative numbers are found in the input array.</exception>
        private void ValidateNumbers(string[] numbers)
        {
            var negatives = numbers.Where(n => int.Parse(n) < 0).ToList();
            if (negatives.Any())
            {
                throw new ArgumentException("negatives not allowed: " + string.Join(", ", negatives));
            }
        }
    }
}
