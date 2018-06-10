using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет статический класс для работы с временными интервалами.
    /// </summary>
    public static class Date
    {
        /// <summary>
        /// Добавляет указанное <c>daysCount</c> количество дней в коллекцию <c>dates</c>.
        /// </summary>
        /// <param name="dates">Коллекция, в которую нужно добавить дни</param>
        /// <param name="daysCount">Количество дней, которое необходимо добавить</param>
        /// <returns>Расширенную коллекцию экземпляров класса <c>DateTime</c></returns>
        public static List<DateTime> AddDays(this List<DateTime> dates, int daysCount)
        {
            var lastDate = dates.OrderBy(i => i).Last();

            for (int i = 0; i < daysCount; i++)
            {
                lastDate = lastDate.AddDays(1);
                dates.Add(lastDate);
            }

            return dates;
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
