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
            Operators = new List<IOperator>();
            Operators.Add(new Operator('*', 2));
            Operators.Add(new Operator('/', 2));
            Operators.Add(new Operator('+', 1));
            Operators.Add(new Operator('-', 1));
            Operators.Add(new Operator('(', 0));
            Operators.Add(new Operator(')', 0));
            Convertor = new PolishNotationConvertor(Operators);
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
