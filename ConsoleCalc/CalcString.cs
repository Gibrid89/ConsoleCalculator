using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.work
{
    public class CalcString
    {
        public List<IOperator> Operators { get; set; }
        private IPolishNotationConvertor Convertor;

        public CalcString()
        {
            /*Func<double, double, double> twoParams = (x, y) => x * y;*/
            Operators = new List<IOperator>();
            Operators.Add(new Operator('*', (a) => a[0] * a[1], 2, 2));
            Operators.Add(new Operator('/', (a) => a[0] / a[1], 2, 2));
            Operators.Add(new Operator('+', (a) => a[0] + a[1], 2, 1));
            Operators.Add(new Operator('-', (a) => a[0] - a[1], 2, 1));
            Operators.Add(new Operator('(', null, 2, 0));
            Operators.Add(new Operator(')', null, 2, 0));
            Convertor = new PolishNotationConvertor(Operators);
        }
        
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

        public string[] ConvertToReversePolishNotation(string input)
        {
            try
            {
                return Convertor.ConvertToArray(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

    }
}
