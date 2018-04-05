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
    public partial class ChartForm : Form, IData
    {
        #region IData Implementation
        private List<ItemWTI> data;
        public List<ItemWTI> Data
        {
            get 
            {
                return data;
            }
            set
            {
                data = value;
                DrawChart(data);
            }
        }
        #endregion

        // Конструктор класса.
        public ChartForm():this(null)
        {
        }

        // Конструктор класса.
        public ChartForm(List<ItemWTI> data)
        {
            InitializeComponent();
            this.Data = data;
        }

        /// <summary>
        /// Отображает исходные данные на графике.
        /// </summary>
        /// <param name="dataValues">Коллекция данных</param>
        public void DrawChart(List<ItemWTI> dataValues)
        {
            /* Возможный вариант
            chart.DataSource = data;
            chart.Series["main"].XValueMember = "Date";
            chart.Series["main"].YValueMembers = "Value";
            chart.DataBind();*/

            if (dataValues == null)
                return;
            
            if(this.Data != dataValues)
                this.Data = dataValues;

            // Убрать линию тренда с графика.
            chart.Series["trend"].Enabled = false;
            chart.Series["trend"].Points.Clear();            
            
            chart.Series["main"].Points.Clear();

            foreach (var wti in dataValues)
            {
                chart.Series["main"].Points.AddXY(wti.Date, wti.Value); 
            }

            if (dataValues.Count < 1)
                return;

            // Верхняя и нижняя границы.
            chart.ChartAreas[0].AxisY.Maximum = Math.Round(dataValues.Max(), 1);
            chart.ChartAreas[0].AxisY.Minimum = Math.Round(dataValues.Min(), 1);
            
            // Интервал У
            chart.ChartAreas[0].AxisY.Interval = dataValues.Interval() < 30 ? Math.Floor(dataValues.Interval()/10): chart.ChartAreas[0].AxisY.Interval;

            // Интревал х. Если диапазон значений - менсяц, то интервал равен 1, если нет - 2.   
            chart.ChartAreas[0].AxisX.MajorGrid.Interval = dataValues.Count <= 31 ? 1 : 2;

            // Если диапазон значений - менсяц. Выводить метку каждый день. Если нет - общее количество / 30.
            chart.ChartAreas[0].AxisX.Interval = dataValues.Count <= 31 ? 31 : dataValues.Count / 30;
        }

        /// <summary>
        /// Отображает линию тренда на графике.
        /// </summary>
        /// <param name="dataValues">Коллекция данных</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        public void DrawTrend(List<ItemWTI> dataValues, byte polinomialDegree)
        {
            // Построить график исходной функции.
            DrawChart(dataValues);
            
            // Очищаем данные линии тренда и делаем ее видимой.
            chart.Series["trend"].Points.Clear();
            chart.Series["trend"].Enabled = true;

            // Расчетные значения у.
            var y = GetRegressionCoefficients(polinomialDegree);

            for (int i = 0; i < y.Length; i++)
            {
                chart.Series["trend"].Points.AddXY(dataValues[i].Date, y[i]);
            }
        }

        /// <summary>
        /// Возвращает массив расчетных значений f(x) для полиномиальной регрессии.
        /// </summary>
        /// <param name="polinomialDegree">Степень полинома</param>
        /// <returns>Массив расчетных значений f(x)</returns>
        private double[] GetRegressionCoefficients(byte polinomialDegree)
        {
            var xvalues = Enumerable.Range(1,Data.Count).Select(z => z + 0.0).ToArray();
            var coeff = PolynomialRegression.GetCoefficients(xvalues, Data.Select(i => i.Value).ToArray(), polinomialDegree);
            
            return PolynomialRegression.GetYFromXValue(coeff, xvalues);
        }

        /// <summary>
        /// Отображает/скрывает легенду исходя из флага <c>isEnabled</c>.
        /// </summary>
        /// <param name="isEnabled">Флаг отображжения легенды</param>
        public void ShowLegend(bool isEnabled)
        {
            chart.Legends[0].Enabled = isEnabled;
        }
    }
}
