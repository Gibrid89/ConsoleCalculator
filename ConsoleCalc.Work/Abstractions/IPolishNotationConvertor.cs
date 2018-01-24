using System.Collections.Generic;

namespace ConsoleCalc.Work
{
    /// <summary>
    /// Конвертация в обратную Польскую нотацию.
    /// </summary>
    public interface IPolishNotationConvertor
    {
        /// <summary>
        /// Конвертирует в массив. 
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Массив, где каждый элемент строка из числа или оператора.</returns>
        string[] ConvertToArray(string input, List<IOperator> operators);

        /// <summary>
        /// Конвертирует в стек.
        /// </summary>
        /// <param name="input">Конвертируемая строка.</param>
        /// <returns>Стек объектов, где объект или IOperator или строка.</returns>
        Queue<object> ConvertToQuery(string input, List<IOperator> operators);
    }   
}
