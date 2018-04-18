using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WtiOil
{
    public class ItemWTI
    {
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Долларов за баррель")]
        public double Value { get; set; }

        public ItemWTI(DateTime date, double value)
        {
            this.Date = date;
            this.Value = value;
        }

        public ItemWTI():this(DateTime.Now.Date, 0)
        {

        }
    }
}
