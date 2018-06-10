using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет класс для Фурье-анализа. 
    /// </summary>
    public static class FourierTransform
    {
        /// <summary>
        /// Возвращает коллекцию объектов <c>Harmonic</c>, содержащую информацию о гармониках раяда Фурье.
        /// </summary>
        /// <param name="frequency">Частота повторения функции(T=1/f)</param>
        /// <param name="step">Шаг, с которым расположены абсциссы Y(x)</param>
        /// <param name="harmonicsCount">Количество гармони</param>
        /// <param name="yValues">Массив значений Y(x)</param>
        /// <returns>Коллекцию объектов <c>Harmonic</c></returns>
        public static List<Harmonic> GetHarmonics(double frequency, double step, int harmonicsCount, double[] yValues)
        {
            var harmonics = new List<Harmonic>();
            var length = yValues.Length;

            for (int i = 0; i <= harmonicsCount; i++)
            {
                double F = frequency * i;
                double coef = 2.0 / (double)length;
                double A = 0, B = 0;

                for (int j = 0; j < length; j++)
                {
                    double Z = 2 * Math.PI * F * step * j;
                    A += coef * yValues[j] * Math.Cos(Z);
                    B += coef * yValues[j] * Math.Sin(Z);
                }

                double M = Math.Sqrt((A * A) + (B * B));
                double Phi = -Math.Acos(A / M);
                harmonics.Add(new Harmonic(F,A,B,M, B < 0 ? -Phi : Phi));
            }

            return harmonics;
        }

        /// <summary>
        /// Возвращает значения Y(x) синтезированной функции.
        /// </summary>
        /// <param name="harmonics">Коллекция объектов <c>Harmonic</c></param>
        /// <param name="xValues">Массив значений x</param>
        /// <param name="average">Среднее значение массива точек Y(x)</param>
        /// <returns>Значения Y(x) синтезированной функции.</returns>
        public static double[] GetYFromXValue(List<Harmonic> harmonics, double[] xValues, double average)
        {
            var result = new List<double>();
            for (int j = 0; j < xValues.Length; j++)
            {
                double y = average;

                for (int i = 1; i < harmonics.Count; i++)
                    y += harmonics[i].Аmplitude * Math.Cos(2 * Math.PI * i * harmonics[1].Frequency * xValues[j] + harmonics[i].Phase);

                result.Add(y);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Возвращает погрешность синтезированной функции, полученной из заданных гармоник.
        /// </summary>
        /// <param name="yValues">Массив значений  Y(x)</param>
        /// <param name="fourierY">Массив значений Y*(x)</param>
        /// <returns>Погрешность синтезированной функции</returns>
        public static double GetError(double[] yValues, double[] fourierY)
        {
            double error = 1.0 / (yValues.Length + 1);
            double sum = 0;
            for (int i = 0; i < yValues.Length; i++)
            {
                sum += Math.Pow(yValues[i] - fourierY[i], 2);
            }

            return Math.Sqrt(error * sum);
        }   
    }
}
