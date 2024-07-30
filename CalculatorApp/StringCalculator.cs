using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public class StringCalculator
    {
        private int _callCount = 0;

        public int Add(string numbers)
        {
            _callCount++;
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }
            char delim = ',';
            if (numbers.StartsWith("//"))
            {
                // getiing index of \n to get deliminator
                var delimiterIndex = numbers.IndexOf('\n');
                // getting substring which contains deliminator
                var delimiter = numbers.Substring(2, delimiterIndex - 2);
                // removing extra values which are there for deliminator config.
                numbers = numbers.Substring(delimiterIndex + 1);
                // changing delim to new deliminator
                delim = delimiter[0];
            }

            var delimiters = new[] { delim, '\n' };
            var parts = numbers.Split(delimiters, StringSplitOptions.TrimEntries);
            ValidateNumbers(parts);
            return parts.Sum(int.Parse);
        }
        public int GetCalledCount()
        {
            return _callCount;
        }
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
