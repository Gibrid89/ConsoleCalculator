using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.Work
{
    /// <summary>
    /// Класс математической операции.
    /// </summary>
    public class Operator : IOperator
    {
        /// <summary>
        /// Представление в виде символа.
        /// </summary>
        public char Symbol { get; private set; }

        /// <summary>
        /// Приоритет операции.
        /// </summary>
        public Priority Priority { get; private set; }

        /// <summary>
        /// Количество используемых операндов для вычисления.
        /// </summary>
        public byte CountOfOperands { get; private set; }

        /// <summary>
        /// Делегат действия оператора.
        /// </summary>
        /// <param name="operands">Массив входных операндов.</param>
        /// <returns>Результат действия.</returns>
        public delegate double Action(double[] operands);
        private Action ActionDo; //Само действие.

        /// <summary>
        /// Конструктор оператора. 
        /// </summary>
        /// <param name="symbol">Символьное представление оператора.</param>
        /// <param name="action">Действие оператора.</param>
        /// <param name="countOfOperands">Количество используемых операндов.</param>
        /// <param name="priority">Приоритет оператора.</param>
        public Operator(char symbol, Action action, byte countOfOperands = 2, Priority priority = Priority.Low)
        {
            this.Symbol = symbol;
            this.Priority = priority;
            this.CountOfOperands = countOfOperands;
            this.ActionDo = action;
            
        }

        /// <summary>
        /// Запуск действия оператора.
        /// </summary>
        /// <param name="operands">Операнды. </param>
        /// <returns></returns>
        public double Evaluation (double[] operands)
        {
            if (operands.Length != CountOfOperands)
                throw new ArgumentException("Неверное количество операндов.");
            try
            {
                return ActionDo(operands);
            }
            catch
            {
                throw new Exception("Ошибка в делегате оператора.");
            }
            
        }

        /// <summary>
        /// Конвертирует в строку.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Symbol.ToString();
        }
        
    }
}
