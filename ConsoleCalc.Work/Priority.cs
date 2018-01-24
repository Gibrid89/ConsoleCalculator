using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.Work
{
    /// <summary>
    /// Приоритет выполнения операций.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Низкий.
        /// </summary>
        Low = 0,
        /// <summary>
        /// Средний.
        /// </summary>
        Middle,
        /// <summary>
        /// Высокий.
        /// </summary>
        High,
        /// <summary>
        /// Наивысший.
        /// </summary>
        Highest
    }
}
