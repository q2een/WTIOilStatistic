using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет класс, содержащий информацию о цене на нефть за определенный день.
    /// </summary>
    public class ItemWTI:DataItem
    {
        /// <summary>
        /// Дата.
        /// </summary>
        [DisplayName("Дата")]
        public new DateTime Date { get; set; }

        /// <summary>
        /// Цена. Долларов за баррель.
        /// </summary>
        [DisplayName("Долларов за баррель")]
        public new double Value { get; set; }

        /// <summary>
        /// Предоставляет класс, содержащий информацию о цене на нефть за определенный день.
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="value">Цена. Долларов за баррель.</param>
        public ItemWTI(DateTime date, double value)
        {
            this.Date = date;
            this.Value = value;
        }

        /// <summary>
        /// Предоставляет класс, содержащий информацию о цене на нефть (0 долларов по умолчанию) за текущий день день.
        /// </summary>
        public ItemWTI():this(DateTime.Now.Date, 0)
        {

        }
    }
}
