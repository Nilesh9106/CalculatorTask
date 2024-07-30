﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
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
            return parts.Sum(int.Parse);
        }

    }

}
