using System;
using System.Collections.Generic;
using System.Windows;
using ConsoleCalc.work;

namespace ConsoleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new CalcString();
            calc.Operators.Add(new Operator('^', 3));
            var result = calc.ConvertToReversePolishNotation("2,5+4.7*25/(981-0.5)^2");
            var answer = new string[] { "2,5", "4.7", "25", "*", "981", "0.5", "-", "2", "^", "/", "+" };
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("result = " + result[i] + "; answer = " + answer[i]);
            }
            Console.ReadLine();
        }
    }

  
}
