using System;
using System.ComponentModel;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет класс, содержащий пару Дата - Значение.
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// Дата.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [DisplayName("Значение")]
        public double Value { get; set; }

        /// <summary>
        /// Предоставляет класс, содержащий пару Дата - Значение.
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="value">Значение</param>
        public DataItem(DateTime date, double value)
        {
            this.Date = date;
            this.Value = value;
        }

        /// <summary>
        /// Предоставляет класс, содержащий пару Дата - Значение.
        /// </summary>
        public DataItem()
        {

        }
    }
}
