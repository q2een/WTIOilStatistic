using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WtiOil
{
    public partial class InformationForm : Form, IData
    {
        #region IData Implementation
        public List<ItemWTI> FullData { get; set; }
        public List<ItemWTI> Data { get; set; }
        #endregion 

        /// <summary>
        /// Тип формы.
        /// </summary>
        public InformationType Type { get; set; }

        /// <summary>
        /// Расчетные значения У.
        /// </summary>
        public double[] YValues{ get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="type">Тип формы</param>
        public InformationForm(InformationType type)
        {
            InitializeComponent();
            this.Type = type;

            switch (type)
            { 
                case InformationType.Statistics:
                    this.Text = "Элементарные статистики";
                    break;
                case InformationType.Regression:
                    this.Text = "Полиномиальная регрессия";
                    break;
                case InformationType.Fourier:
                    this.Text = "Анализ временного ряда: Фурье-анализ";
                    break;
                case InformationType.Wavelet:
                    this.Text = "Анализ временного ряда: вейвлет-анализ";
                    break;
            }

        }

        /// <summary>
        /// Отображает данные, полученные при расчете полиномиальной регрессии, и возвращает отображаемую коллекцию.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="coefficients">Коллекция коэффициентов полинома</param>
        /// <param name="yValues">Значение расчетных У</param>
        public List<InformationItem> ShowRegression(IData data, double[] coefficients, double[] yValues)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            this.YValues = yValues;

            var regression = new List<InformationItem>();
            double error = PolynomialRegression.GetError(Data.Select(i => i.Value).ToArray(), yValues);

            regression.Add(new InformationItem("Степень полинома", (coefficients.Length - 1) + ""));
            regression.Add(new InformationItem("Погрешность", error));
            regression.Add(new InformationItem("Коэффициенты", ""));

            for (int i = 0; i < coefficients.Length; i++)
            {
                var format = Math.Round(Math.Abs(coefficients[i]), 3) < 0.001 ? "{0:e3}" : "{0:0.000}";
                regression.Add(new InformationItem("A[" + i + "]", String.Format(format, coefficients[i])));
            }

            regression.Add(new InformationItem("Расчетные значения: ", ""));

            for (int i = 0; i < yValues.Length; i++)
            {
                regression.Add(new InformationItem(Data[i].Date.ToString("dd/MM/yyyy"), yValues[i]));
            }

            dgvInformation.DataSource = regression;

            return regression;
        }

        /// <summary>
        /// Отображает данные, полученные при Фурье-анализе и возвращает коллекцию отображаемых данных.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="harmonics">Коллекция гармоник</param>
        /// <param name="yValues">Значение расчетных У</param>
        public List<InformationItem> ShowFourier(IData data, List<Harmonic> harmonics, double[] yValues)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            YValues = yValues;

            if (harmonics == null)
                return new List<InformationItem>();

            var fourier = new List<InformationItem>();

            double error = FourierTransform.GetError(harmonics, data.Data.Select(i=>i.Value).ToArray(), yValues);
            fourier.Add(new InformationItem("Период", yValues.Length * 0.2));
            fourier.Add(new InformationItem("Δt", 0.200));
            fourier.Add(new InformationItem("Погрешность", error));
            fourier.Add(new InformationItem("Число гармоник", harmonics.Count + ""));

            for (int i = 1; i < harmonics.Count; i++)
            {
                fourier.Add(new InformationItem(i + " Гармоника", ""));
                fourier.Add(new InformationItem("Частота", harmonics[i].Frequency));
                fourier.Add(new InformationItem("Коэффициет Фурье a[" + i + "]",harmonics[i].A));
                fourier.Add(new InformationItem("Коэффициет Фурье b[" + i + "]",harmonics[i].B));
                fourier.Add(new InformationItem("Амплитуда", harmonics[i].Аmplitude));
                fourier.Add(new InformationItem("Фаза, °", harmonics[i].Phase * (180 / Math.PI)));
            }

            dgvInformation.DataSource = fourier;

            return fourier;

        }

        /// <summary>
        /// Отображает элементарные статистики и возвращает отображаемую коллекцию.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="isAverage">Отображать среднее значение</param>
        /// <param name="isStandardError">Отображать стандартную ошибку</param>
        /// <param name="isMediana">Отображать медиану</param>
        /// <param name="isMode">Отображать моду</param>
        /// <param name="isStandardDeviation">Отображать стандартное отклонение</param>
        /// <param name="isDispersion">Отображать димперсию</param>
        /// <param name="isSkewness">Отображать эксцесс</param>
        /// <param name="isKurtosis">Отображать асимметричность</param>
        /// <param name="isInterval">Отображать интервал</param>
        /// <param name="isMin">Отображать минимальное значение</param>
        /// <param name="isMax">Отображать максимальное значение</param>
        /// <param name="isSum">Отображать сумму элементов</param>
        public List<InformationItem> ShowStatistic(IData data, bool isAverage, 
                                                      bool isStandardError, 
                                                      bool isMediana, 
                                                      bool isMode, 
                                                      bool isStandardDeviation, 
                                                      bool isDispersion, 
                                                      bool isSkewness,
                                                      bool isKurtosis,
                                                      bool isInterval,
                                                      bool isMin,
                                                      bool isMax,
                                                      bool isSum)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;

            var statistics = new List<InformationItem>();

            if (isAverage)
                statistics.Add(new InformationItem("Среднее", Data.Average()));

            if (isStandardError)
                statistics.Add(new InformationItem("Станд. ошибка", Data.StandardError()));

            if (isMediana)
                statistics.Add(new InformationItem("Медиана", Data.Median()));

            if (isMode)
                statistics.Add(new InformationItem("Мода", Data.Mode()));

            if (isStandardDeviation)
                statistics.Add(new InformationItem("Станд. отклонение", Data.StandardDeviation()));

            if (isDispersion)
                statistics.Add(new InformationItem("Дисперсия выборки", Data.Dispersion()));

            if (isSkewness)
                statistics.Add(new InformationItem("Эксцесс", Data.Skewness()));

            if (isKurtosis)
                statistics.Add(new InformationItem("Асимметричность", Data.Kurtosis()));

            if (isInterval)
                statistics.Add(new InformationItem("Интервал", Data.Interval()));

            if (isMin)
                statistics.Add(new InformationItem("Минимум", Data.Min()));

            if (isMax)
                statistics.Add(new InformationItem("Максимум", Data.Max()));

            if (isSum)
                statistics.Add(new InformationItem("Сумма", Data.Sum()));

            statistics.Add(new InformationItem("Счет", Data.Count()));

            dgvInformation.DataSource = statistics;

            return statistics;
        }

    }

    /// <summary>
    /// Тип окна.
    /// </summary>
    public enum InformationType
    { 
        Statistics,
        Regression,
        Fourier,
        Wavelet 
    }

    public class InformationItem
    {
        [DisplayName("Параметр")]
        public string Parameter { get; set; }

        [DisplayName("Значение")]
        public string Value { get; set; }

        public InformationItem(string parameter, string value)
        {
            this.Parameter = parameter;
            this.Value = value;
        }

        public InformationItem(string parameter, double? value)
        {
            this.Parameter = parameter;
            this.Value = String.Format("{0:0.000}",value);
        }
    }
}
