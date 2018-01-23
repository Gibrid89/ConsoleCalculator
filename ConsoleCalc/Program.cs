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
            calc.Operators.Add(new Operator('^', (a) => Math.Pow(a[0], a[1]), 2, 3));//Пример добавления нового оператора

            /*var result = calc.ConvertToReversePolishNotation("2,5+4,7*25/(981-0.5)^2");
            var answer = new string[] { "2,5", "4,7", "25", "*", "981", "0.5", "-", "2", "^", "/", "+" };
            for (int i = 0; i < answer.Length; i++)
            {
                Console.WriteLine("result = " + result[i] + "; answer = " + answer[i]);
            }*/

            Console.WriteLine("Введите строку для расчета. Для выхода введите \"exit\"");

            Console.Write(">");
            string s = Console.ReadLine();
            while (s.ToLower() != "exit")
            {
                try
                {
                    Console.WriteLine("=" + calc.Calculate(s));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка:" + ex.Message);
                }
                Console.Write(">");
                s = Console.ReadLine();
            }
        }
    }

  
}
