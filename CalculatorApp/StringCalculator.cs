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
        public event Action<string, int>? AddOccured;

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
                if(AddOccured != null)
                {
                    AddOccured(numbers, 0);
                }
                return 0;
            }
            var delimiters = new List<string>() { "\n"};
            string orgNumbers = numbers;
            if (numbers.StartsWith("//"))
            {
                // Getting index of \n to get delimiter
                var delimiterIndex = numbers.IndexOf('\n');
                // Getting substring which contains delimiter
                var delimiter = numbers.Substring(2, delimiterIndex - 2);
                if(delimiter.Length > 1)
                {
                    //check if it contains square bracket
                    if(delimiter.StartsWith('[') && delimiter.EndsWith(']'))
                    {
                        // removing first and last square brackets
                        var delimString = delimiter.Substring(1, delimiter.Length - 2);
                        // if there are multiple delimiters then will split by brackets
                        var delims = delimString.Split("][");
                        delimiters.AddRange(delims);
                    }
                    else
                    {
                        throw new FormatException("String is in wrong format please confirm the format");
                    }
                }
                else
                {
                    // Changing delim to new delimiter
                    delimiters.Add(delimiter);
                }
                // Removing extra values which are there for delimiter config.
                numbers = numbers.Substring(delimiterIndex + 1);
            }
            else
            {
                delimiters.Add(",");
            }
            // spliting numbers by delimiters
            var parts = numbers.Split(delimiters.ToArray(), StringSplitOptions.TrimEntries);
            // validating negetive numbers
            ValidateNumbers(parts);
            // calculating sum ignoring value bigger than 1000
            int ans = parts.Where(n=>int.Parse(n) <=1000).Sum(int.Parse);
            if (AddOccured != null)
            {
                AddOccured(orgNumbers, ans);
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
