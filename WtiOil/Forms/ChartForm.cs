using System;
using System.Linq;
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

        #region Событие изменения рядов графика
        public delegate void SeriesStateHandler(SeriesCollection series, int index = -1);

        public event SeriesStateHandler OnSeriesChanged = delegate { };
        #endregion

        // Конструктор класса.
        public ChartForm()
        {
            InitializeComponent();
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
     
        /// <summary>
        /// Сохраняет изображение графика в путь <c>path</c>
        /// </summary>
        /// <param name="path">Полный путь к изображению</param>
        public void SaveChart(string path)
        {
            chart.SaveImage(path, ImageFormat.Png);
        }

        /// <summary>
        /// Добавляет ряд к текущему графику.
        /// </summary>
        /// <param name="seriesName">Наименование ряда</param>
        /// <param name="legendText">Текст, отображаемый в легенде к данному ряду</param>
        /// <param name="color">Цвет ряда на графике</param>
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

        public void RemoveAllSeries()
        {
            chart.Series.Clear();
        }

        /// <summary>
        /// Масштабирует график исходя из максимального и минимального значения,
        /// отображаемых на графике. 
        /// </summary>
        public void ScaleChart()
        {
            var points = chart.Series.Where(i => i.Enabled).Select(z => z.Points.Select(k => k.YValues[0]));

            var max = points.Select(i=> i.Max()).Max();
            var min = points.Select(i => i.Min()).Min();

            chart.ChartAreas[0].AxisY.Maximum = Math.Round(max, 1);
            chart.ChartAreas[0].AxisY.Minimum = Math.Round(min, 1);
        }

        public void DrawChart(IData data, double[] yValues, int forecastDaysCount = 0)
        {
            this.DrawChart(data);
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
        /// <param name="seriesName">Наименование ряда графика</param>
        /// <param name="data">Экземаляр класса, реализующего IData<</param>
        /// <param name="yValues">Массив значений У</param>
        /// <param name="legendText">Текст, отображаемый в легенде для данного ряда</param>
        /// <param name="color">Цвет ряда на графике</param>
        private void DrawFunction(string seriesName, IData data, double[] yValues, string legendText, Color color)
        {
            var xValues = data.Data.Select(i=>i.Date).ToArray();
            DrawFunction(seriesName, data, xValues, yValues, legendText, color);
        }

        /// <summary>
        /// Отображает функцию на графике.
        /// </summary>
        /// <param name="seriesName">Наименование ряда графика</param>
        /// <param name="data">Экземаляр класса, реализующего IData<</param>
        /// <param name="xValues">Массив значений Х</param>
        /// <param name="yValues">Массив значений У</param>
        /// <param name="legendText">Текст, отображаемый в легенде для данного ряда</param>
        /// <param name="color">Цвет ряда на графике</param>
        public void DrawFunction(string seriesName, IData data, DateTime[] xValues, double[] yValues, string legendText, Color color)
        {
            if (xValues.Length != yValues.Length)
                throw new ArgumentOutOfRangeException("Размер массивов значений X и Y должен совпадать",innerException:null);

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
                chart.Series[seriesName].Points.AddXY(xValues[i], yValues[i]);
            }
        }

        /// <summary>
        /// Отображает функцию  с прогнозом на графике.
        /// </summary>
        /// <param name="seriesName">Наименование ряда графика</param>
        /// <param name="data">Экземаляр класса, реализующего IData<</param>
        /// <param name="forecastDaysCount">Количество прогнозируемых дней</param>
        /// <param name="yValues">Массив значений У</param>
        /// <param name="legendText">Текст, отображаемый в легенде для данного ряда</param>
        /// <param name="color">Цвет ряда на графике</param>
        private void DrawForecastFunction(string seriesName, IData data,int forecastDaysCount, double[] yValues, string legendText, Color color)
        {
            var xValues = data.Data.Select(i => i.Date).ToList().AddDays(forecastDaysCount).ToArray();

            DrawFunction(seriesName, data, xValues, yValues, legendText, color);
        }

        /// <summary>
        /// Отображает линию тренда на графике.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        public void DrawTrend(IData data, double[] yValues, int forecastDaysCount)
        {
            DrawForecastFunction("trend", data, forecastDaysCount, yValues, "Линия тренда", Color.SteelBlue);
        }

        /// <summary>
        /// Отображает результат Фурье-анализа.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="polinomialDegree">Стиепень полинома для полиномиальной регрессии</param>
        public void DrawFourier(IData data, double[] yValues, int forecastDaysCount)
        {
            DrawForecastFunction("fourier", data, forecastDaysCount, yValues, "Синтезированная функция (Фурье-анализ)", Color.Chocolate);
        }

        /// <summary>
        /// Отображает на графике результат многофакторной регрессии.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="yValues">Массив значений У синтезированной функции</param>
        public void DrawMultipleRegression(IData data, double[] yValues, int forecastDaysCount = 0)
        {
            DrawFunction("multiple", data,yValues,"Многофакторная регрессия", Color.Crimson);
        }

        /// <summary>
        /// Отображает на графике результат обратного вейвлет-преобразования.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="yValues">Массив значений У синтезированной функции</param>
        public void DrawWaveletD4(IData data, double[] yValues, int forecastDaysCount = 0)
        {
            DrawFunction("waveletSynt", data, yValues, "Синтезированная функция (вейвлет-анализ)", Color.IndianRed);
        }

        /// <summary>
        /// Отображает на графике результат прямого вейвлет-преобразования.
        /// </summary>
        /// <param name="data">Экземаляр класса, реализующего IData</param>
        /// <param name="coeffs">Коэффициенты, полученные в результате прямого вейвлет-преобразования</param>
        public void DrawWaveletFunc(IData data, double[] coeffs, int forecastDaysCount = 0)
        {
            DrawFunction("waveletD4", data, coeffs, "Вейвлет функция", Color.MediumSeaGreen);
            
            chart.Series[0].Enabled = false;
            chart.ChartAreas[0].AxisY.Interval= Math.Round((coeffs.Max() - coeffs.Min())/(coeffs.Average()),1);
            ScaleChart();
        }
    }
}
