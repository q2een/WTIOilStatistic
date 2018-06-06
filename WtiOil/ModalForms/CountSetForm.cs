using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WtiOil
{
    public partial class CountSetForm : Form
    {
        // Исходные данные.
        public List<ItemWTI> Data{ get; set; }

        public int ForecastDaysCount 
        {
            get
            {
                return Int32.Parse(numDays.Value + "");
            }
        }

        // Тип формы.
        private readonly CountSetType type;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="data">Исходные данные</param>
        /// <param name="type">Тип формы</param>
        public CountSetForm(List<ItemWTI> data, CountSetType type)
        {
            InitializeComponent();
            this.Data = data;
            this.type = type;

            switch (type)
            { 
                case CountSetType.Regression :
                    this.Text = "Полиномиальная регрессия";
                    lblText.Text = "Укажите степень полинома";
                    break;
                case CountSetType.Fourier:
                    this.Text = "Фурье-анализ";
                    lblText.Text = "Укажите количество гармоник";
                    break;
            }
        }

        /// <summary>
        /// Возвращает расширенную коллекцию <c>initialArr</c>, добавив в нее 
        /// <c>addNumberCount</c> элементов
        /// </summary>
        /// <remarks>
        /// Входные данные: [1 2 3 4 5 6 7 8] addNumberCount: 5
        /// Выходные данные: [1 2 3 4 5 6 7 8 9 10 11 12 13]
        /// </remarks>
        /// <param name="initialArr">Массив исходных элементов</param>
        /// <param name="addNumberCount">Количество элементов, которых надо добавить в массив</param>
        /// <returns>Расширенный массив значений</returns>
        private double[] GetNumericList(double[] initialArr, int addNumberCount)
        {
            var newArr = Enumerable.Range(initialArr.Length, ForecastDaysCount).Select(i => i + 0.0);
            var result = initialArr.ToList();
            result.AddRange(newArr);

            return result.ToArray();
        }

        /// <summary>
        /// Рассчитывает коэффициенты полиномиальной регрессии и отображает их.
        /// </summary>
        /// <param name="count">Степень полинома</param>
        /// <param name="xValues">Значение Х</param>
        /// <param name="yValues">Значения У(Х)</param>
        private void CalculateRegression(byte count, double[] xValues, double[] yValues)
        {
            var coeff = Regression.GetCoefficients(xValues, yValues, count);

            var x = GetNumericList(xValues, ForecastDaysCount);

            var y = Regression.GetYFromXValue(coeff, x);

            (this.Owner as MainMDI).ShowLineTrend(cbShowInformation.Checked, coeff, y, ForecastDaysCount);
        }

        /// <summary>
        /// Рассчитывает гармоники ряда Фурье и отобржает их.
        /// </summary>
        /// <param name="count">Количество гармоник</param>
        /// <param name="xValues">Значение Х</param>
        /// <param name="yValues">Значения У(Х)</param>
        private void CalculateFourier(byte count, double[] xValues, double[] yValues)
        {
            var harmonics = FourierTransform.GetHarmonics(1.0 /(1.0 * xValues.Length), 1, count, yValues);

            var x = GetNumericList(xValues, ForecastDaysCount);

            var y = FourierTransform.GetYFromXValue(harmonics, x, yValues.Average());

            (this.Owner as MainMDI).ShowFourier(cbShowInformation.Checked, harmonics, y, ForecastDaysCount);
        }

        // Обработка события нажатия на клавишу "Подтвердить".
        private void btnOK_Click(object sender, EventArgs e)
        {
            byte count = Byte.Parse(tbDegree.Text);
            double[] xValues = Enumerable.Range(1, Data.Count).Select(z => z + 0.0).ToArray();
            double[] yValues = Data.Select(i => i.Value).ToArray();

            switch (type)
            { 
                case CountSetType.Regression:
                    CalculateRegression(count, xValues, yValues);
                    break;
                case CountSetType.Fourier:
                    CalculateFourier(count, xValues, yValues);
                    break;
            }

            this.Close();
        }

        // Обработка события ввода данных в текстовое поле. Разрешен ввод только цифр.
        private void tbDegree_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;

            if (!Char.IsDigit(key) && key != 8)
                e.Handled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numDays_ValueChanged(object sender, EventArgs e)
        {
            string value = "";

            switch (Int32.Parse(numDays.Value+""))
            { 
                case 1:
                    value = "день";
                    break;
                case 2:
                case 3:
                case 4:
                    value = "дня";
                    break;
                default:
                    value = "дней";
                    break;
            }

            lblDays.Text = value;
        }

        private void cbForecast_CheckedChanged(object sender, EventArgs e)
        {
            numDays.Enabled = lblDays.Enabled = cbForecast.Checked;
        }
    }

    /// <summary>
    /// Тип формы.
    /// </summary>
    public enum CountSetType
    { 
        Regression,
        Fourier
    }
}
