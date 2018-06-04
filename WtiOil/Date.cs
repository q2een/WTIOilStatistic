using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    public static class Date
    {
        public static bool IsDateRangeValid(List<DateTime> dates)
        {
            return (dates.Max() - dates.Min()).TotalDays == dates.Count - 1;
        }

        public static List<DateTime> GetInvalidDates(List<DateTime> dates)
        {
            var invalid = new List<DateTime>();

            for (int i = 0; i < dates.Count; i++)
            {
                if (i + 1 >= dates.Count)
                    continue;

                var nextDay = dates[i].AddDays(1);

                while (nextDay != dates[i + 1].Date)
                {
                    invalid.Add(nextDay);
                    nextDay = nextDay.AddDays(1);
                }
            }

            return invalid;
        }

        /// <summary>
        /// Устанавливает период с <c>from</c> по <c>to</c> в коллекции <c>data</c>.
        /// </summary>
        /// <param name="data">Коллекция данных</param>
        /// <param name="from">Дата начала периода</param>
        /// <param name="to">Дата конца периода</param>
        public static void SetDateRange(ref List<DataItem> data, DateTime from, DateTime to)
        {
            int indexFrom = data.FindIndex(i=> i.Date == from);
            int indexTo = data.FindIndex(i=> i.Date == to);

            if (indexFrom == -1)
                throw new ArgumentOutOfRangeException("from");

            if (indexTo == -1)
                throw new ArgumentOutOfRangeException("to");

            data = data.Skip(indexFrom).Take(indexTo + 1 - indexFrom).ToList();
        }

        /// <summary>
        /// Устанавливает период с <c>from</c> по <c>to</c> в экземляре класса, реализующий <c>IData</c>.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующий IData</param>
        /// <param name="from">Дата начала периода</param>
        /// <param name="to">Дата конца периода</param>
        public static void SetDateRange(IData data, DateTime from, DateTime to)
        {
            int indexFrom = data.FullData.FindIndex(i=> i.Date == from);
            int indexTo = data.FullData.FindIndex(i=> i.Date == to);

            if (indexFrom == -1)
                throw new ArgumentOutOfRangeException("from");

            if (indexTo == -1)
                throw new ArgumentOutOfRangeException("to");

            data.Data = data.FullData.Skip(indexFrom).Take(indexTo + 1 - indexFrom).ToList();
        }
    }
}
