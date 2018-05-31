using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

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
        public delegate void SeriesStateHandler(SeriesCollection series, int index = -1);

        public event SeriesStateHandler OnSeriesChanged = delegate { };

        // Конструктор класса.
        public ChartForm()
        {
            InitializeComponent();
        }
             
        public void SaveChart(string path)
        {
            chart.SaveImage(path, ImageFormat.Png);
        }

        private void AddSeries(string seriesName, string legendText, Color color)
        {
            if (chart.Series.FindByName(seriesName) != null)
                return;

            chart.Series.Add(seriesName);
            var series = chart.Series[seriesName];
            series.Enabled = false;
            series.XValueType = ChartValueType.DateTime;
            series.YValueType = ChartValueType.Double;
            series.Legend = "legendMain";
            series.LegendText = legendText;
            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "mainChartArea";
            series.BorderWidth = 3;
            series.Color = color;

            OnSeriesChanged(chart.Series);
        }

        private void RemoveSeries(string seriesName)
        {
            if (chart.Series.Contains(chart.Series[seriesName]))
                chart.Series.RemoveAt(chart.Series.IndexOf(seriesName));
        }

        /// <summary>
        /// Отображает исходные данные на графике.
        /// </summary>
        /// <param name="dataValues">Коллекция данных</param>
        public void DrawChart(IData data)
        {
            this.data = data.Data;
            this.FullData = data.FullData;

            if (Data.Count < 1)
                return;

            chart.Series.Clear();
            AddSeries("main", "Исходные данные", Color.Teal);
            
            foreach (var wti in Data)
            {
                chart.Series["main"].Points.AddXY(wti.Date, wti.Value);
            }

            chart.Series["main"].Enabled = true;
            OnSeriesChanged(chart.Series, chart.Series.IndexOf(chart.Series["main"]));

            // Верхняя и нижняя границы.
            chart.ChartAreas[0].AxisY.Maximum = Math.Round(Data.Max(), 1);
            chart.ChartAreas[0].AxisY.Minimum = Math.Round(Data.Min(), 1);

            // Интервал У.
            chart.ChartAreas[0].AxisY.Interval = Data.Interval() < 20 ? 1 : Math.Floor(Data.Interval() / 20);

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
        private void DrawFunction(string seriesName, IData data, double[] yValues, string legendText, Color color)
        {
            if (chart.Series.FindByName("main") == null || chart.Series["main"].Points.Count != data.Data.Count ||
                chart.Series["main"].Points[0].YValues[0] != data.Data[0].Value)
            {
                // Построить график исходной функции.
                DrawChart(data);
            }

            AddSeries(seriesName, legendText, color);

            // Очищаем данные линии тренда и делаем ее видимой.
            chart.Series[seriesName].Points.Clear();
            chart.Series[seriesName].Enabled = true;
            OnSeriesChanged(chart.Series, chart.Series.IndexOf(chart.Series[seriesName]));

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
            DrawFunction("trend", data, yValues, "Линия тренда", Color.SteelBlue);
        }

        /// <summary>
        /// Отображает результат Фурье-анализа.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        public void DrawFourier(IData data, double[] yValues)
        {
            DrawFunction("fourier", data, yValues, "Синтезированная функция (Фурье-анализ)", Color.Chocolate);
        }

        public void DrawMultipleRegression(IData data, double[] yValues)
        {
            DrawFunction("multiple", data,yValues,"Многофакторная регрессия", Color.Crimson);
        }

        public void DrawWaveletD4(IData data, double[] yValues, double[] coeffs)
        {
            DrawFunction("waveletSynt", data, yValues, "Синтезированная функция (вейвлет-анализ)", Color.IndianRed);
            DrawFunction("waveletD4", data, coeffs, "Вейвлет функция", Color.MediumSeaGreen);
            
            chart.Series["waveletD4"].Enabled = false;
            OnSeriesChanged(chart.Series, chart.Series.IndexOf(chart.Series["waveletD4"]));
        }

        /// <summary>
        /// Отображает/скрывает легенду исходя из флага <c>isEnabled</c>.
        /// </summary>
        /// <param name="isEnabled">Флаг отображжения легенды</param>
        public void ShowLegend(bool isEnabled)
        {
            chart.Legends[0].Enabled = isEnabled;
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
        }
    }
}
