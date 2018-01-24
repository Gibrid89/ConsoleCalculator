namespace ConsoleCalc.Work
{
    /// <summary>
    /// Интерфейс для оператора
    /// </summary>
    public interface IOperator 
    {
        /// <summary>
        /// Представление в виде символа.
        /// </summary>
        char Symbol { get; }

        /// <summary>
        /// Приоритет операции.
        /// </summary>
        Priority Priority { get; }

        /// <summary>
        /// Количество используемых операндов для вычисления.
        /// </summary>
        byte CountOfOperands { get; }

        /// <summary>
        /// Запуск действия оператора.
        /// </summary>
        /// <param name="operands">Операнды.</param>
        /// <returns></returns>
        double Evaluation(double[] operands);
    }
}
