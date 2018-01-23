using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.work
{
    /// <summary>
    /// Rонвертациz в обратную Польскую нотацию.
    /// </summary>
    internal interface IPolishNotationConvertor
    {
        /// <summary>
        /// Конвертирует в массив. 
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Массив, где каждый элемент строка из числа или оператора.</returns>
        string[] ConvertToArray(string input);

        /// <summary>
        /// Конвертирует в список.
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Список объектов, где объект или IOperator или строка.</returns>
        List<object> ConvertToList(string input);

        /// <summary>
        /// Конвертирует в стек.
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Стек объектов, где объект или IOperator или строка.</returns>
        Stack<object> ConvertToStack(string input);
    }

    
    /// <summary>
    /// Класс конвертации в обратную Польскую нотацию.
    /// </summary>
    internal class PolishNotationConvertor : IPolishNotationConvertor
    {
        private List<IOperator> Operators;
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="operators">Список операторов.</param>
        public PolishNotationConvertor (List<IOperator> operators)
        {
            this.Operators = operators;
        }

        /// <summary>
        /// Конвертирует в массив. 
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Массив, где каждый элемент строка из числа или оператора.</returns>
        public string[] ConvertToArray(string input) 
        {
            var result = new List<string>();
            try
            {
                foreach (var el in ConvertToList(input))
                {
                    var tmp = (el as Operator);
                    if (tmp != null)
                        result.Add(tmp.Symbol.ToString());
                    else
                        result.Add(el as string);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result.ToArray();
        }

        /// <summary>
        /// Конвертирует в стек.
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Стек объектов, где объект или IOperator или строка.</returns>
        public Stack<object> ConvertToStack(string input)
        {
            var result = new Stack<object>();
            try
            {
                var tmp = ConvertToList(input);
                for (int i = tmp.Count - 1; i >= 0; i--)//Инвертируем порядок
                {
                    result.Push(tmp[i]);
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        /// <summary>
        /// Конвертирует в список.
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Список объектов, где объект или IOperator или строка.</returns>
        public List<object> ConvertToList(string input)
        {
            if (!checkParentheses(input))
            {
                throw new FormatException("Неверно расствленны скобки.");
            }
            var resultStack = new List<object>();
            var bufStack = new Stack<IOperator>();

            for (int i = 0; i < input.Length; i++)
            {
                var opr = FindOperator(input[i], Operators);//найденный оператор
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
                                resultStack.Add(bufStack.Pop());
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
                            resultStack.Add(bufStack.Pop());
                        }
                        bufStack.Push(opr);
                    }

                    
                }
                else if (Char.IsDigit(input[i]))//если цифра, то добавляем в строку, пока не вышли за границы и след. символ часть числа
                {
                    string s = "" + input[i];
                    while ((i < input.Length - 1) && (Char.IsDigit(input[i+1]) || (input[i+1] ==',') || (input[i+1] =='.')))
                    {
                        i++;
                        if (input[i] == '.') s += ','; //заменяем точку запятой
                        else s += input[i];                       
                    }
                    resultStack.Add(s);
                }
                else
                {
                    throw new FormatException("Неизвестный оператор.");
                }
            }

            if (bufStack.Count > 0) //Добавляем все, что осталось в буфере в результат
            {
                foreach (var el in bufStack)
                {
                    resultStack.Add(el);
                }
            }
            return resultStack;
        }

        /// <summary>
        /// Проверяет строку на равенсто открывающих и закрывающих скобок.
        /// </summary>
        /// <param name="input">Проверяемая строка.</param>
        /// <returns>Истина, если количество правых и левых скобок равно.</returns>
        private bool checkParentheses (string input)
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
