using System;
using System.Collections.Generic;
using System.Linq;

namespace WtiOil
{
    /// <summary>
    /// Класс для расчта элементарных статистик.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Сумма.
        /// </summary>
        public static double Sum (this IEnumerable<ItemWTI> data)
        {
            return data.Select(i => i.Value).Sum();
        }

        /// <summary>
        /// Максимальное значение.
        /// </summary>
        public static double Max (this IEnumerable<ItemWTI> data)
        {
            return data.Select(i => i.Value).Max(); 
        }

        /// <summary>
        /// Минимальное значение.
        /// </summary>
        public static double Min (this IEnumerable<ItemWTI> data)
        {
           return data.Select(i => i.Value).Min();
        }

        /// <summary>
        /// Интервал.
        /// </summary>
        public static double Interval (this IEnumerable<ItemWTI> data)
        {
            return data.Max() - data.Min();     
        }

        /// <summary>
        /// Среднее.
        /// </summary>
        public static double Average (this IEnumerable<ItemWTI> data)
        {
            return data.Select(i => i.Value).Average();
        }

        /// <summary>
        /// Дисперсия.
        /// </summary>
        public static double Dispersion (this IEnumerable<ItemWTI> data)
        {
             return data.Select(i => Math.Pow(i.Value - data.Average(), 2)).Sum() / (data.Count()-1); 
        }

        /// <summary>
        /// Стандартная ошибка.
        /// </summary>
        public static double StandardError (this IEnumerable<ItemWTI> data)
        {
           return data.StandardDeviation() / Math.Sqrt(data.Count());
        }

        /// <summary>
        /// Стандартное отклонение.
        /// </summary>
        public static double StandardDeviation (this IEnumerable<ItemWTI> data)
        {
            return Math.Sqrt(data.Dispersion()); 
        }

        /// <summary>
        /// Медиана.
        /// </summary>
        public static double Median (this IEnumerable<ItemWTI> data)
        {
                int halfIndex = data.Count() / 2;
                var sorted = data.Select(i => i.Value).OrderBy(n => n).ToArray();

                if (data.Count() % 2 == 0)
                    return (sorted[halfIndex] + sorted[(halfIndex - 1)]) / 2;

                return sorted[halfIndex];            
        }

        /// <summary>
        /// Мода.
        /// </summary>
        public static double? Mode (this IEnumerable<ItemWTI> data)
        {
            var first = data.Select(i => i.Value).GroupBy(item => item).Select(z => new { Value = z.Key, Count = z.Count() }).OrderByDescending(i => i.Count).First();
                return first.Count > 1 ? (double?)first.Value : null;
        }

        /// <summary>
        /// Асимметричность.
        /// </summary>
        public static double Kurtosis (this IEnumerable<ItemWTI> data)
        {
            return (data.Count() / (double)((data.Count() - 1) * (data.Count() - 2))) *
                    data.Select(x => Math.Pow((x.Value - data.Average()) / data.StandardDeviation(), 3)).Sum();
        }

        /// <summary>
        /// Эксцесс.
        /// </summary>
        public static double Skewness (this IEnumerable<ItemWTI> data)
        {
            return ((data.Count() * (data.Count() + 1) / (double)((data.Count() - 1) * (data.Count() - 2) * (data.Count() - 3)))) *
                    data.Select(x => Math.Pow((x.Value - data.Average()) / data.StandardDeviation(), 4)).Sum()
                    - ((3 * Math.Pow((data.Count() - 1), 2)) / (double)(((data.Count() - 2) * (data.Count() - 3)))); 
        }
    }
}
