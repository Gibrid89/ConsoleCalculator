using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.Work
{
    /// <summary>
    /// Класс конвертации в обратную польскую нотацию.
    /// </summary>
    /// <remarks>Реализует алгоритм «сортировочная станция» Эдсгера Дейкстра</remarks>
    public class PolishNotationConvertor : IPolishNotationConvertor
    {
        /// <summary>
        /// Конвертирует в массив. 
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <param name="operators">Список используемых операторов.</param>
        /// <returns>Массив, где каждый элемент строка из числа или оператора.</returns>
        /// <remarks>Вызывает ConvertToQuery(), и конвертирует очередь в массив.</remarks>
        public string[] ConvertToArray(string input, List<IOperator> operators) 
        {
            var result = new List<string>();
            try
            {
                foreach (var el in ConvertToQuery(input, operators))
                {
                    if (el is IOperator tmp)
                        result.Add(tmp.Symbol.ToString());
                    else
                        result.Add(el as string);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result.ToArray();
        }

        /// <summary>
        /// Конвертирует в очередь.
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <param name="operators">Список используемых операторов.</param>
        /// <returns>Очередь объектов, где объект или IOperator или строка.</returns>
        public Queue<object> ConvertToQuery(string input, List<IOperator> operators)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Строка не должна быть пустой.");
            if (operators == null || operators.Count < 1)
                throw new ArgumentException("Должен быть хотя бы 1 оператор.");
            if (!CheckParentheses(input))
                throw new FormatException("Неверно расствленны скобки.");

            var inp = input.Replace(" ", "");//Удаляем все пробелы.

            var resultQuery = new Queue<object>();
            var bufStack = new Stack<IOperator>();        

            for (int i = 0; i < inp.Length; i++)
            {
                var opr = FindOperator(inp[i], operators);//найденный оператор
                if (opr != null) //Если оператор
                {
                    if ((bufStack.Count <= 0) || (opr.Symbol == '('))
                    {
                        bufStack.Push(opr);
                        continue;
                    }

                    if (opr.Symbol == ')')
                    {
                        try
                        {
                            while (bufStack.Peek().Symbol != '(')
                            {
                                resultQuery.Enqueue(bufStack.Pop());
                            }
                            bufStack.Pop();
                        }
                        catch
                        {
                            throw new FormatException("Неверно расствленны скобки.");
                        }
                        continue;
                    }

                    if (opr.Priority > bufStack.Peek().Priority)
                    {
                        bufStack.Push(opr);
                    }                     
                    else
                    {
                        while (bufStack.Count > 0 && opr.Priority <= bufStack.Peek().Priority)
                        {
                            resultQuery.Enqueue(bufStack.Pop());
                        }
                        bufStack.Push(opr);
                    }                  
                }
                else if (Char.IsDigit(inp[i]))//если цифра, то добавляем в строку, пока не вышли за границы и след. символ часть числа
                {
                    string s = "" + inp[i];
                    while ((i < inp.Length - 1) && (Char.IsDigit(inp[i+1]) || (inp[i+1] ==',') || (inp[i+1] =='.')))
                    {
                        i++;
                        if (inp[i] == '.') s += ','; //заменяем точку запятой
                        else s += inp[i];                       
                    }
                    resultQuery.Enqueue(s);
                }
                else
                {
                    throw new FormatException("Неизвестный оператор \"" + inp[i] + "\"");
                }
            }

            if (bufStack.Count > 0) //Добавляем все, что осталось в буфере в результат
            {
                foreach (var el in bufStack)
                {
                    resultQuery.Enqueue(el);
                }
            }
            return resultQuery;
        }

        /// <summary>
        /// Проверяет строку на равенство открывающих и закрывающих скобок.
        /// </summary>
        /// <param name="input">Проверяемая строка.</param>
        /// <returns>Истина, если количество правых и левых скобок равно.</returns>
        private bool CheckParentheses (string input)
        {
            var left = 0;
            var right = 0;
            foreach (var c in input)
            {
                if (c == '(') left++;
                if (c == ')') right++;
            }
            return left == right;

        }

        /// <summary>
        /// Находит в списке операторов оператор по символу.
        /// </summary>
        /// <param name="c">Искомый символ оператора.</param>
        /// <param name="operators">Список операторов.</param>
        /// <returns></returns>
        private IOperator FindOperator(char c, List<IOperator> operators)
        {
            foreach (var el in operators)
            {
                if (el.Symbol == c) return el;
            }
            return null;
        }
    }
}
