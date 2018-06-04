using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    public class Wavelet
    {
        /// <summary>
        /// Масштабирующие коэффициенты прямого преобразования Дjбеши D4.
        /// Фильтр низких частот.
        /// </summary>
        private readonly double[] d4CL = {(1 + Math.Sqrt(3)) / (4 * Math.Sqrt(2)),
                                        (3 + Math.Sqrt(3)) / (4 * Math.Sqrt(2)),
                                        (3 - Math.Sqrt(3)) / (4 * Math.Sqrt(2)),
                                        (1 - Math.Sqrt(3)) / (4 * Math.Sqrt(2))
                                    };

        public double[] GetD4CL()
        {
            return this.d4CL;
        }

        /// <summary>
        /// Возвращает массив фильтров высоких частот на основе низких <c>lowCoefficients</c>.
        /// HPF - high-pass filter
        /// </summary>
        /// <param name="lowCoefficients">Массив фильтров нихкиъ частот</param>
        /// <returns>Массив фильтров высоких частот</returns>
        public double[] GetHPFCoeffs(double[] lowCoefficients)
        {
            var CH = new double[lowCoefficients.Length];

            for (int i = 0; i < lowCoefficients.Length; i++)
                CH[i] = Math.Pow(-1, i) * lowCoefficients[lowCoefficients.Length - 1 - i];

            return CH;
        }

        protected double GetElementAt(double[] arr, int index)
        {
            if (index >= 0)
                return arr[index];

            return arr[arr.Length + index];
        }

        /// <summary>
        /// Находит фильтры низких и высоких частот для обратного преобразования Добеши.
        /// </summary>
        /// <param name="CL">Фильтр низких частот</param>
        /// <param name="CH">Фильтр высоких частот</param>
        /// <param name="iCL">Выхожной параметр. Фильтр низких частот</param>
        /// <param name="iCH">Выхожной параметр. Фильтр высоких частот</param>
        public void GetInvCoeffs(double[] CL, double[] CH, out double[] iCL, out double[] iCH)
        {
            var icl = new List<double>();
            var ich = new List<double>();

            for (int k = 0; k < CL.Length; k += 2)
            {
                icl.AddRange(new double[] { GetElementAt(CL, k - 2), GetElementAt(CH, k - 2) });
                ich.AddRange(new double[] { GetElementAt(CL, k - 1), GetElementAt(CH, k - 1) });
            }

            iCL = icl.ToArray();
            iCH = ich.ToArray();
        }

        /// <summary>
        /// Прямое преобразование Добеши
        /// </summary>
        /// <param name="data">Массив исходных значений</param>
        /// <param name="length">Длина</param>
        /// <param name="CL">Массив фильтров высоких частот</param>
        public void Transform(double[] data, int length, double[] CL)
        {
            if (length < 4)
                throw new ArgumentException("length");

            double[] CH = GetHPFCoeffs(CL);

            int half = length >> 1;

            double[] tmp = new double[length];

            for (int j = 0, i = 0; j < length; j += 2, i++)
                for (int k = 0; k < CL.Length; k++)
                {
                    tmp[i] += GetElementAt(data, (k + j) % length) * CL[k]; ;
                    tmp[i + half] += GetElementAt(data, (k + j) % length) * CH[k]; ;
                }

            for (int i = 0; i < length; i++)
                data[i] = tmp[i];
        }

        /// <summary>
        /// Обратное преобразование Дjбеши.
        /// </summary>
        /// <param name="data">Массив данных</param>
        /// <param name="length">Длина</param>
        /// <param name="iCL">Фильтр низких частот</param>
        /// <param name="iCH">Фильтр высоких частот</param>
        public void InverseTransform(double[] data, int length, double[] iCL, double[] iCH)
        {
            if (length < 4)
                throw new ArgumentException("length");

            int half = length >> 1;

            double[] tmp = new double[length];

            // 1 3 0 2
            //      last smooth val  last coef.  first smooth  first coef
            tmp[0] = data[half - 1] * iCL[0] + data[length - 1] * iCL[1] + data[0] * iCL[2] + data[half] * iCL[3];
            tmp[1] = data[half - 1] * iCH[0] + data[length - 1] * iCH[1] + data[0] * iCH[2] + data[half] * iCH[3];
            int j = 2;
            for (int i = 0; i < half - 1; i++)
            {
                // 0 2 1 3
                // 1 3 2 4
                //     smooth val     coef. val       smooth val    coef. val
                tmp[j++] = data[i] * iCL[0] + data[i + half] * iCL[1] + data[i + 1] * iCL[2] + data[i + half + 1] * iCL[3];
                tmp[j++] = data[i] * iCH[0] + data[i + half] * iCH[1] + data[i + 1] * iCH[2] + data[i + half + 1] * iCH[3];
            }

            for (int i = 0; i < length; i++)
            {
                data[i] = tmp[i];
            }

        }

        /// <summary>
        /// Прямое преобразование Добеши D4.
        /// </summary>
        /// <param name="values">Массив исходных значений</param>
        /// <returns></returns>
        public static double[] D4Transform(IEnumerable<double> values)
        {
            var data = values.ToArray();
            var wave = new Wavelet();

            for (int n = data.Length; n >= 4; n >>= 1)
            {
                wave.Transform(data, n, wave.d4CL);
            }

            return data;
        }

        /// <summary>
        /// Обратное преобразование Добеши D4.
        /// </summary>
        /// <param name="coeffs">Массив преобразованных значений</param>
        public static double[] InverseD4Transform(IEnumerable<double> coeffs)
        {
            var data = coeffs.ToArray();
            var wave = new Wavelet();
            double[] iCL, iCH;
            wave.GetInvCoeffs(wave.d4CL, wave.GetHPFCoeffs(wave.d4CL), out iCL, out iCH);

            for (int n = 4; n <= data.Length; n <<= 1)
            {
                wave.InverseTransform(data, n, iCL, iCH);
            }

            return data;
        }
    }
}
