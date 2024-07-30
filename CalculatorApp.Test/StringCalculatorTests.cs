using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Test
{
    public class StringCalculatorTests
    {
        private readonly StringCalculator _calculator;

        public StringCalculatorTests()
        {
            _calculator = new StringCalculator();
        }

        [Fact]
        public void Add_EmptyString_ReturnsZero()
        {
            var result = _calculator.Add("");
            Assert.Equal(0, result);
        }

        [Fact]
        public void Add_SingleNumber_ReturnsNumber()
        {
            var result = _calculator.Add("1");
            Assert.Equal(1, result);
        }

        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            var result = _calculator.Add("1,2");
            Assert.Equal(3, result);
        }
        [Fact]
        public void Add_MultipleNumbers_ReturnsSum()
        {
            var result = _calculator.Add("1\n2,3");
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_InValidNumber_ThrowError()
        {
            Assert.Throws<FormatException>(() =>
            {
                _calculator.Add("1,\n ");
            });
        }
        [Fact]
        public void Add_CustomDelimiter_ReturnsSum()
        {
            var result = _calculator.Add("//;\n1;2");
            Assert.Equal(3, result);
        }
    }

}
