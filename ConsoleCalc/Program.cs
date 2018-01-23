using System;
using System.Collections.Generic;
using System.Windows;
using ConsoleCalc.work;

namespace ConsoleCalc
{
    /// <summary>
    /// Задание - написать консольный калькулятор, который принимает входную строку, содержащую математическое выражение
    /// (+,-,*,/,скобки) и выводит в консоль его результат. Главный критерий при оценке задания - разработка с использованием
    /// принципов SOLID, TDD. Решение должно быть расширяемым другими операцими.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new CalcString();
            calc.Operators.Add(new Operator('^', (a) => Math.Pow(a[0], a[1]), 2, 3));//Пример добавления нового оператора

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
