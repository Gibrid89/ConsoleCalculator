using System;
using System.Collections.Generic;
using System.Windows;

namespace ConsoleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.ReadLine();
        }
    }

    public class CalcString
    {
        public List<Operator> Operators { get; set; }

        public CalcString ()
        {
            Operators = new List<Operator>();
            Operators.Add(new Operator('*', 2));
            Operators.Add(new Operator('/', 2));
            Operators.Add(new Operator('+', 1));
            Operators.Add(new Operator('-', 1));
            Operators.Add(new Operator('(', 0));
            Operators.Add(new Operator(')', 0));
        }

        public string[] ConvertToReversePolishNotation(string input) //Использует закрытую функцию и приводит результат к string[]
        {
            var result = new List<string>();
            foreach (var el in ConvertToRPN(input))
            {
                var tmp = (el as Operator);
                if (tmp != null)
                    result.Add(tmp.Symbol.ToString());
                else
                    result.Add(el as string);
            }
            return result.ToArray();
        }

        private List<object> ConvertToRPN(string input)
        {
            var resultStack = new List<object>();
            var bufStack = new Stack<Operator>();

            for (int i = 0; i < input.Length; i++)
            {
                var oprIndex = Operators.IndexOf(new Operator(input[i]));//найденный оператор
                if (oprIndex > -1) //Если оператор
                {
                    var opr = Operators[oprIndex];
                    if (bufStack.Count <= 0)//Если буферный стэк пустой
                    {
                        bufStack.Push(opr);
                    }
                    else
                    {
                        switch (opr.Symbol)
                        {
                            case '(':
                                {
                                    bufStack.Push(opr);
                                    break;
                                }
                            case ')':
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
                                    break;
                                }
                            default:
                                {
                                    if (opr.Priority > bufStack.Peek().Priority)
                                        bufStack.Push(opr);
                                    else
                                    {
                                        while (bufStack.Count > 0 && opr.Priority <= bufStack.Peek().Priority)
                                        {
                                            resultStack.Add(bufStack.Pop());
                                        }
                                        bufStack.Push(opr);
                                    }
                                    break;
                                }
                        }
                    }
                }
                else if (Char.IsDigit(input[i]))//если число
                {
                    resultStack.Add(input[i].ToString());                   
                }
                else
                {
                    throw new FormatException("Неизвестный оператор.");
                }
                
            }

            if (bufStack.Count > 0)
            {
                foreach (var el in bufStack)
                {
                    resultStack.Add(el);
                }
            }
            return resultStack;
        }
    }

    public class Operator
    {
        public char Symbol { get; private set; }
        public byte Priority { get; private set; }

        public Operator (char symbol, byte priority = 0)
        {
            this.Symbol = symbol;
            this.Priority = priority;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var m = obj as Operator;
            if (m  == null)
                return false;
            return this.Symbol.Equals(m.Symbol);
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }

    
}
