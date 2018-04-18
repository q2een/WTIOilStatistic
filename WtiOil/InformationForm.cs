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
        /// Добавляет строку в элементу управления DataGridView.
        /// </summary>
        /// <param name="parameter">Параметр (первый столбец)</param>
        /// <param name="value">Значние (второй столбец) в формате 0.000</param>
        private void AddInformationDataGridRow(string parameter, double? value)
        {
            AddInformationDataGridRow(parameter, String.Format("{0:0.000}", value));
        }

        /// <summary>
        /// Добавляет строку в элементу управления DataGridView.
        /// </summary>
        /// <param name="parameter">Параметр (первый столбец)</param>
        /// <param name="value">Значние (второй столбец)</param>
        private void AddInformationDataGridRow(string parameter, string value)
        {
            var row = new DataGridViewRow();
            row.CreateCells(dgvInformation);

            row.Cells[0].Value = parameter;
            row.Cells[1].Value = value;

            dgvInformation.Rows.Add(row);
        }

        /// <summary>
        /// Отображает данные, полученные при расчете полиномиальной регрессии.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="coefficients">Коллекция коэффициентов полинома</param>
        /// <param name="yValues">Значение расчетных У</param>
        public void ShowRegression(IData data, double[] coefficients, double[] yValues)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            this.YValues = yValues;
            
            dgvInformation.Rows.Clear();
            double error = PolynomialRegression.GetError(Data.Select(i => i.Value).ToArray(), yValues);

            AddInformationDataGridRow("Степень полинома", (coefficients.Length - 1) + "");
            AddInformationDataGridRow("Погрешность", error);
            AddInformationDataGridRow("Коэффициенты", "");

            for (int i = 0; i < coefficients.Length; i++)
            {
                var format = Math.Round(Math.Abs(coefficients[i]), 3) < 0.001 ? "{0:e3}" : "{0:0.000}";
                AddInformationDataGridRow("A[" + i + "]", String.Format(format, coefficients[i]));
            }

            AddInformationDataGridRow("Расчетные значения: ", "");

            for (int i = 0; i < yValues.Length; i++)
            {
                AddInformationDataGridRow(Data[i].Date.ToString("dd/MM/yyyy"), yValues[i]);
            }
        }

        /// <summary>
        /// Отображает данные, полученные при Фурье-анализе.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="harmonics">Коллекция гармоник</param>
        /// <param name="yValues">Значение расчетных У</param>
        public void ShowFourier(IData data, List<Harmonic> harmonics, double[] yValues)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            YValues = yValues;

            if (harmonics == null)
                return;

            dgvInformation.Rows.Clear();
            double error = FourierTransform.GetError(harmonics, data.Data.Select(i=>i.Value).ToArray(), yValues);
            AddInformationDataGridRow("Период", yValues.Length * 0.2);
            AddInformationDataGridRow("Δt", 0.200);
            AddInformationDataGridRow("Погрешность", error);
            AddInformationDataGridRow("Число гармоник", harmonics.Count + "");

            for (int i = 1; i < harmonics.Count; i++)
            {
                AddInformationDataGridRow(i + " Гармоника", "");
                AddInformationDataGridRow("Частота", harmonics[i].Frequency);
                AddInformationDataGridRow("Коэффициет Фурье a[" + i + "]",harmonics[i].A);
                AddInformationDataGridRow("Коэффициет Фурье b[" + i + "]",harmonics[i].B);
                AddInformationDataGridRow("Амплитуда", harmonics[i].Аmplitude);
                AddInformationDataGridRow("Фаза, °", harmonics[i].Phase * (180 / Math.PI));
            } 

        }

        /// <summary>
        /// Отображает элементарные статистики
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
        public void ShowStatistic(IData data, bool isAverage, 
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

            dgvInformation.Rows.Clear();

            if (isAverage)
                AddInformationDataGridRow("Среднее", Data.Average());

            if (isStandardError)
                AddInformationDataGridRow("Станд. ошибка", Data.StandardError());

            if (isMediana)
                AddInformationDataGridRow("Медиана", Data.Median());

            if (isMode)
                AddInformationDataGridRow("Мода", Data.Mode());

            if (isStandardDeviation)
                AddInformationDataGridRow("Станд. отклонение", Data.StandardDeviation());

            if (isDispersion)
                AddInformationDataGridRow("Дисперсия выборки", Data.Dispersion());

            if (isSkewness)
                AddInformationDataGridRow("Эксцесс", Data.Skewness());

            if (isKurtosis)
                AddInformationDataGridRow("Асимметричность", Data.Kurtosis());

            if (isInterval)
                AddInformationDataGridRow("Интервал", Data.Interval());

            if (isMin)
                AddInformationDataGridRow("Минимум", Data.Min());

            if (isMax)
                AddInformationDataGridRow("Максимум", Data.Max());

            if (isSum)
                AddInformationDataGridRow("Сумма", Data.Sum());

                AddInformationDataGridRow("Счет", Data.Count());
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
}
