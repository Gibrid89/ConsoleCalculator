using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleCalc.Work;

namespace UnitTestConsoleCalc
{
    /// <summary>
    /// Сводное описание для CreateCalcTests
    /// </summary>
    [TestClass]
    public class CalcStringTests
    {
        CalcString calc = new CalcString(new PolishNotationConvertor());

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCalcStringNullTest()
        {
            calc = new CalcString(null);
        }

        [TestMethod]
        public void CalculateMethodBaseOperationTest()
        {
            Assert.AreEqual(5, calc.Calculate("3+2"));
            Assert.AreEqual(1, calc.Calculate("3-2"));
            Assert.AreEqual(6, calc.Calculate("3*2"));
            Assert.AreEqual(1.5, calc.Calculate("3/2"));
        }

        [TestMethod]
        public void CalculateMethodManyOperationsTest()
        {
            calc.Operators.Add(new Operator('^', (a) => Math.Pow(a[0], a[1]), 2, Priority.Highest));
            
            Assert.AreEqual(6, calc.Calculate("2+2*2"));
            Assert.AreEqual(6, calc.Calculate("2 + 2   * 2"));
            Assert.AreEqual(2.5, calc.Calculate("2+4*2/(1-5)^2"));
        }

        [TestMethod]
        public void CalculateMethodUnaryOperationTest()
        {
            calc.Operators.Add(new Operator('^', (a) => Math.Pow(a[0], a[1]), 2, Priority.Highest));
            calc.Operators.Add(new Operator('s', (a) => Math.Sqrt(a[0]), 1, Priority.Highest));

            Assert.AreEqual(9, calc.Calculate("3^2"));
            Assert.AreEqual(2.5, calc.Calculate("2+s16*2/(1-5)^2"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateMethodNullExeptions()
        {
            calc.Calculate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateMethodEmtyExeptions()
        {
            calc.Calculate("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CalculateMethodFormatExeptions()
        {
            calc.Calculate("3o2");
        }
    }
}
