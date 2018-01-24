using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleCalc.Work;

namespace UnitTestConsoleCalc
{
    [TestClass]
    public class ConvertToReversePolishNotationTest
    {
        CalcString calc = new CalcString(new PolishNotationConvertor());
        string[] result, answer;

        [TestMethod]
        public void ConvertMethodSimple()
        {
            result = calc.ConvertToReversePolishNotation("1+1-2");
            answer = new string[] { "1", "1", "+", "2", "-" };
            for (int i = 0; i < answer.Length; i++)
            {
                Assert.AreEqual(result[i], answer[i]);
            }
        }

        [TestMethod]
        public void ConvertMethodComplex()
        {
            calc.Operators.Add(new Operator('^', (a) => Math.Pow(a[0], a[1]), 2, Priority.Highest));

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

            result = calc.ConvertToReversePolishNotation("2.5+4,7*25/(981-0.5)^2");
            answer = new string[] { "2,5", "4,7", "25", "*", "981", "0,5", "-", "2", "^", "/", "+" };
            for (int i = 0; i < answer.Length; i++)
            {
                Assert.AreEqual(result[i], answer[i]);
            }
        }

        [TestMethod]
        public void ConvertMethodParenthesesExeptions()
        {
            int i = 0;

            try
            {
                calc.ConvertToReversePolishNotation("(");
                i++;//Если дошли до этой линии, то исключений не было. Тест провален.
                
            }
            catch { }

            try
            {
                calc.ConvertToReversePolishNotation("(()");
                i++;//Если дошли до этой линии, то исключений не было. Тест провален.
            }
            catch { }

            try
            {
                calc.ConvertToReversePolishNotation("3^2+6/2*(5+5))");
                i++;//Если дошли до этой линии, то исключений не было. Тест провален.
            }
            catch { }

            if (i>0)
                Assert.Fail(); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConverteMethodFormatException()
        {
            calc.ConvertToReversePolishNotation("3\\2+6/2*(5+5)");
        }
    }
}
