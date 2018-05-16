using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WtiOil
{
    public class DataItem
    {
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Значение")]
        public double Value { get; set; }

        public DataItem(DateTime date, double value)
        {
            this.Date = date;
            this.Value = value;
        }

        public DataItem()
        {

        }
    }
}
