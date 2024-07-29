using System;
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

                var parts = numbers.Split(',');
                return parts.Sum(int.Parse);
            }
    }

}
