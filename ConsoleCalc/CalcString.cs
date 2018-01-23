using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.work
{
    /// <summary>
    /// Класс для вычесления строки с математическими выражениями.
    /// </summary>
    public class CalcString
    {
        /// <summary>
        /// Список используемых операторов.
        /// </summary>
        public List<IOperator> Operators { get; set; }
        private IPolishNotationConvertor Convertor;

        /// <summary>
        /// Конструктор. Создает по умолчанию операторы: '*', '/', '+', '-', '(', ')'.
        /// </summary>
        public CalcString()
        {
            Operators = new List<IOperator>();
            Operators.Add(new Operator('*', (a) => a[0] * a[1], 2, 2));
            Operators.Add(new Operator('/', (a) => a[0] / a[1], 2, 2));
            Operators.Add(new Operator('+', (a) => a[0] + a[1], 2, 1));
            Operators.Add(new Operator('-', (a) => a[0] - a[1], 2, 1));
            Operators.Add(new Operator('(', null, 2, 0));
            Operators.Add(new Operator(')', null, 2, 0));
            Convertor = new PolishNotationConvertor(Operators);
        }

        /// <summary>
        /// Вычисляет результат из строки.
        /// </summary>
        /// <param name="input">Строка с выражениями</param>
        /// <returns>Результат вычислений</returns>
        public double Calculate(string input)
        {
            var polishString = Convertor.ConvertToStack(input);
            var resultStack = new Stack<double>();

            foreach (var el in polishString)
            {
                var @operator = el as Operator;
                if ( @operator != null) //оператор
                {
                    var operands = new double[@operator.CountOfOperands];
                    for (int i = @operator.CountOfOperands - 1; i >= 0; i--)
                    {
                        operands[i] = resultStack.Pop();
                    }
                    resultStack.Push (@operator.Evaluation(operands));
                }
                else //операнд
                {
                    resultStack.Push(Convert.ToDouble(el.ToString()));
                }
            }
            return resultStack.Pop();

        }

        /// <summary>
        /// Вызывает метод конвертации в обратную польскую запись из встроенного класса.
        /// </summary>
        /// <param name="input">Конвертируемая строка</param>
        /// <returns>Массив, где каждый элемент строка из числа или оператора.</returns>
        public string[] ConvertToReversePolishNotation(string input)
        {
            try
            {
                return Convertor.ConvertToArray(input);
            }
            catch (Exception)
            {
                throw;
            }           
        }

    }
}
