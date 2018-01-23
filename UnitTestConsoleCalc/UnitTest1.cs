using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleCalc.work;

namespace UnitTestConsoleCalc
{
    [TestClass]
    public class UnitTestHelper
    {
        [TestMethod]
        public void ConvertToReversePolishNotation()
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

            result = calc.ConvertToReversePolishNotation("2,5+4.7*25/(981-0.5)^2");
            answer = new string[] { "2,5", "4.7", "25", "*", "981", "0.5", "-", "2", "^", "/", "+" };
            for (int i = 0; i < answer.Length; i++)
            {
                Assert.AreEqual(result[i], answer[i]);
            }

            //Проверка работы исключений на скобки
            try
            {
                calc.ConvertToReversePolishNotation("(");
                calc.ConvertToReversePolishNotation(")");
                calc.ConvertToReversePolishNotation("())");
                calc.ConvertToReversePolishNotation("(()");
                calc.ConvertToReversePolishNotation("3^2+6/2*(5+5))");
                calc.ConvertToReversePolishNotation("3^2+6/2*((5+5)");
                Assert.Fail(); //Если дошли до этой линии, то исключений не было. Тест провален.
            }
            catch (AssertFailedException e) { throw e; }
            catch { }

            //Проверка работы исключений на неизвестный оператор
            try
            {
                calc.ConvertToReversePolishNotation("3^2%6/2*(5+5)");
                calc.ConvertToReversePolishNotation("3\\2+6/2*(5+5)");
                Assert.Fail(); //Если дошли до этой линии, то исключений не было. Тест провален.
            }
            catch (AssertFailedException e) { throw e; }
            catch { }



        }

    }
}
