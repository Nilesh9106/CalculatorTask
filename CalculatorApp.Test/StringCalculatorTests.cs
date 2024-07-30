using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorApp.Test
{
    /// <summary>
    /// Unit tests for the <see cref="StringCalculator"/> class.
    /// </summary>
    public class StringCalculatorTests
    {
        private readonly StringCalculator _calculator;
        string givenInput = null;
        int givenResult = 0;
        public StringCalculatorTests()
        {
            _calculator = new StringCalculator();
            _calculator.AddOccured += delegate (string input, int result)
            {
                givenInput = input;
                givenResult = result;
            };
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
        [Fact]
        public void Add_NegativeNumber_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _calculator.Add("1,-2,3"));
            Assert.Equal("negatives not allowed: -2", ex.Message);
        }
        [Fact]
        public void GetCalledCount_Initially_ReturnsZero()
        {
            Assert.Equal(0, _calculator.GetCalledCount());
        }

        [Fact]
        public void GetCalledCount_AfterOneAddCall_ReturnsOne()
        {
            _calculator.Add("1,2");
            Assert.Equal(1, _calculator.GetCalledCount());
        }

        [Fact]
        public void GetCalledCount_AfterMultipleAddCalls_ReturnsCorrectCount()
        {
            _calculator.Add("1,2");
            _calculator.Add("3,4");
            _calculator.Add("5,6");
            Assert.Equal(3, _calculator.GetCalledCount());
        }
        [Fact]
        public void Add_ShouldTriggerAddOccuredEvent()
        {
            var input = "1,2";
            var expectedResult = 3;

            _calculator.Add(input);

            Assert.Equal(input, givenInput);
            Assert.Equal(expectedResult, givenResult);
        }
        [Fact]
        public void Add_ShouldIgnoreNumberBiggerThan1000()
        {
            var result = _calculator.Add("2,1001");
            Assert.Equal(2, result);
        }
        [Fact]
        public void Add_CustomDelimiterWithMoreThanOneLength()
        {
            var result = _calculator.Add("//[***]\n1***2***3");
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_CustomDelimiterWithOneLength_WithBracket()
        {
            var result = _calculator.Add("//[*]\n1*2*3");
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_CustomDelimiterInValidFormat_ShouldThrowError()
        {
            Assert.Throws<FormatException>(() =>
            {
                _calculator.Add("//[**\n1**2");
            });
        }
        [Fact]
        public void Add_MultipleSingleCharacterDelimiters_ReturnsSum()
        {
            var result = _calculator.Add("//[*][%]\n1*2%3");

            Assert.Equal(6, result);
        }

        [Fact]
        public void Add_MultipleMultiCharacterDelimiters_ReturnsSum()
        {
            var result = _calculator.Add("//[**][%%]\n1**2%%3");

            Assert.Equal(6, result);
        }
    }

}
