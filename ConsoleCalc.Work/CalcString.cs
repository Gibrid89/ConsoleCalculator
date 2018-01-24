using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.Work
{
    /// <summary>
    /// Класс для вычисления строки с математическими выражениями.
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
        public CalcString(IPolishNotationConvertor convertor, List<IOperator> operators = null)
        {
            if (convertor != null)
            {
                this.Convertor = convertor;
            }
            else
            {
                throw new ArgumentNullException("convertor");
            }
            
            if (operators != null)
            {
                this.Operators = operators;
            }
            else
            {
                Operators = new List<IOperator>
                {
                    new Operator('*', (a) => a[0] * a[1], 2, Priority.High),
                    new Operator('/', (a) => a[0] / a[1], 2, Priority.High),
                    new Operator('+', (a) => a[0] + a[1], 2, Priority.Middle),
                    new Operator('-', (a) => a[0] - a[1], 2, Priority.Middle),
                    new Operator('(', null,               2, Priority.Low),
                    new Operator(')', null,               2, Priority.Low)
                };
            }
        }

        /// <summary>
        /// Вычисляет результат из строки.
        /// </summary>
        /// <param name="input">Строка с выражениями</param>
        /// <returns>Результат вычислений</returns>
        public double Calculate(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Строка не должна быть пустой.");
            var polishString = Convertor.ConvertToQuery(input,Operators);
            var resultStack = new Stack<double>();

            foreach (var el in polishString)
            {
                if (el is IOperator @operator) //оператор
                {
                    var operands = new double[@operator.CountOfOperands];
                    try
                    {
                        for (int i = @operator.CountOfOperands - 1; i >= 0; i--)
                        {
                            operands[i] = resultStack.Pop();
                        }
                        resultStack.Push(@operator.Evaluation(operands));
                    }
                    catch { throw new Exception("Лишний оператор."); }
                    
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
                return Convertor.ConvertToArray(input,Operators);
            }
            catch (Exception)
            {
                throw;
            }           
        }

    }
}
