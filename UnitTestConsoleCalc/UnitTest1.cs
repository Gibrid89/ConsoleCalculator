using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleCalc;

namespace UnitTestConsoleCalc
{
    [TestClass]
    public class UnitTestHelper
    {
        [TestMethod]
        public void ConvertToReversePolishNotationTest()
        {
            var calc = new CalcString();
            calc.Operators.Add(new Operator('^', 3));

            var result = calc.ConvertToReversePolishNotation("1+1-2");
            var answer = new string[] { "1", "1", "+", "2", "-" };
            for (int i = 0; i < answer.Length; i++)
            {
                Assert.AreEqual(result[i], answer[i]);
            }

            result = calc.ConvertToReversePolishNotation("2+4*2/(1-5)^2");
            answer = new string[] { "2", "4", "2", "*", "1", "5", "-", "2", "^", "/", "+" };
            for (int i = 0; i < answer.Length; i++)
            {
                Assert.AreEqual(result[i], answer[i]);
            }

            result = calc.ConvertToReversePolishNotation("3^2+6/2*(5+5)");
            answer = new string[] { "3", "2", "^", "6", "2", "/", "5", "5", "+", "*", "+" };
            for (int i = 0; i < answer.Length; i++)
            {
                Assert.AreEqual(result[i], answer[i]);
            }
        }

    }
}
