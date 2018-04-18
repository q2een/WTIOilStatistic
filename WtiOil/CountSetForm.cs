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
        /// Рассчитывает коэффициенты полиномиальной регрессии и отображает их.
        /// </summary>
        /// <param name="count">Степень полинома</param>
        /// <param name="xValues">Значение Х</param>
        /// <param name="yValues">Значения У(Х)</param>
        private void CalculateRegression(byte count, double[] xValues, double[] yValues )
        {
            var coeff = PolynomialRegression.GetCoefficients(xValues, yValues, count);

            var y = PolynomialRegression.GetYFromXValue(coeff, xValues);

            (this.Owner as MainMDI).ShowLineTrend(cbShowInformation.Checked, coeff, y);
        }

        /// <summary>
        /// Рассчитывает гармоники ряда Фурье и отобржает их.
        /// </summary>
        /// <param name="count">Количество гармоник</param>
        /// <param name="xValues">Значение Х</param>
        /// <param name="yValues">Значения У(Х)</param>
        private void CalculateFourier(byte count, double[] xValues, double[] yValues)
        {
            var harmonics = FourierTransform.GetHarmonics(1.0 / (double)((double)1 * xValues.Length), 1, count, yValues);
            var y = FourierTransform.GetYFromXValue(harmonics, xValues, yValues);
            
            (this.Owner as MainMDI).ShowFourier(cbShowInformation.Checked, harmonics, y);
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
