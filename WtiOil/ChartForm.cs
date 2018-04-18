using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WtiOil
{
    public partial class ChartForm : Form, IData
    {
        #region IData Implementation

        public List<ItemWTI> FullData { get; set; }

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
                DrawChart(this);
            }
        }
        #endregion

        // Конструктор класса.
        public ChartForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отображает исходные данные на графике.
        /// </summary>
        /// <param name="dataValues">Коллекция данных</param>
        public void DrawChart(IData data)
        {
            this.data = data.Data;
            this.FullData = data.FullData;

            // Убрать линию тренда с графика.
            chart.Series["trend"].Enabled = false;
            chart.Series["trend"].Points.Clear();

            // Убрать Фурье-анализ с графика.
            chart.Series["fourier"].Enabled = false;
            chart.Series["fourier"].Points.Clear(); 
            
            chart.Series["main"].Points.Clear();

            foreach (var wti in Data)
            {
                chart.Series["main"].Points.AddXY(wti.Date, wti.Value); 
            }

            if (Data.Count < 1)
                return;
            
            // Верхняя и нижняя границы.
            chart.ChartAreas[0].AxisY.Maximum = Math.Round(Data.Max(), 1);
            chart.ChartAreas[0].AxisY.Minimum = Math.Round(Data.Min(), 1);
            
            // Интервал У.
            chart.ChartAreas[0].AxisY.Interval = Data.Interval() < 20 ?  1 : Math.Floor(Data.Interval() / 20);

            // Интревал х. Если диапазон значений - менсяц, то интервал равен 1, если нет - 2.   
            chart.ChartAreas[0].AxisX.MajorGrid.Interval = Data.Count <= 31 ? 1 : 2;

            // Если диапазон значений - менсяц. Выводить метку каждый день. Если нет - общее количество / 30.
            chart.ChartAreas[0].AxisX.Interval = Data.Count <= 31 ? 1 : Data.Count / 30;
        }

        /// <summary>
        /// Отображает функцию на графике.
        /// </summary>
        /// <param name="seriesName">Наименование <c>Series</c></param>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        private void DrawFunction(string seriesName, IData data, double[] yValues)
        {
            if (!chart.Series.Contains(chart.Series[seriesName]))
                throw new Exception();

            // Построить график исходной функции.
            DrawChart(data);

            // Очищаем данные линии тренда и делаем ее видимой.
            chart.Series[seriesName].Points.Clear();
            chart.Series[seriesName].Enabled = true;

            for (int i = 0; i < yValues.Length; i++)
            {
                chart.Series[seriesName].Points.AddXY(Data[i].Date, yValues[i]);
            }
        }

        /// <summary>
        /// Отображает линию тренда на графике.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        public void DrawTrend(IData data, double[] yValues)
        {
            DrawFunction("trend", data, yValues);
        }

        /// <summary>
        /// Отображает результат Фурье-анализа.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        public void DrawFourier(IData data, double[] yValues)
        {
            DrawFunction("fourier", data, yValues);
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
