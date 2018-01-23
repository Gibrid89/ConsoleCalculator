using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.work
{
    public interface IOperator
    {
        char Symbol { get; }
        byte Priority { get; }
        bool Equals(object obj);
        string ToString();
    }

    public class Operator : IOperator
    {
        public char Symbol { get; private set; }
        public byte Priority { get; private set; }

        public Operator(char symbol, byte priority = 0)
        {
            this.Symbol = symbol;
            this.Priority = priority;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var m = obj as Operator;
            if (m == null)
                return false;
            return this.Symbol.Equals(m.Symbol);
        }
        public override string ToString()
        {
            return Symbol.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
