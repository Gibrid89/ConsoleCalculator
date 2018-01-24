using System;
using System.Collections.Generic;
using System.Windows;
using ConsoleCalc.Work;

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
            var calc = new CalcString(new PolishNotationConvertor());
            calc.Operators.Add(new Operator('^', (a) => Math.Pow(a[0], a[1]), 2, Priority.Highest));//Пример добавления нового оператора
            calc.Operators.Add(new Operator('%', (a) => a[0] % a[1],          2, Priority.Highest));
            calc.Operators.Add(new Operator('s', (a) => Math.Sqrt(a[0]),      1, Priority.Highest));//Добавление унарного оператора

            Console.Write("Введите строку для расчета. Для выхода введите \"exit\".\n> ");
            string s = Console.ReadLine();
            while (s.ToLower() != "exit")
            {
                try
                {
                    Console.WriteLine("> " + calc.Calculate(s));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
                Console.Write("> ");
                s = Console.ReadLine();
            }
        }
    }

  
}
