using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет форму для отображения коллекций данных, полученных после регрессии, Фурье и вейвлет-преобразований и т.п.,
    /// где данные предоставляются в виде Параметр - Значение.
    /// </summary>
    public partial class InformationForm : Form, IData
    {
        #region IData Implementation

        /// <summary>
        /// Возвращает или задает полную коллекция данных.
        /// </summary>
        public List<ItemWTI> FullData { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию данных, с которой работает пользователь.
        /// </summary>
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
        /// Результат прямого вейвлет-преобразования.
        /// </summary>
        public double[] Wavelet { get; set; }

        /// <summary>
        /// Количество дней для прогнозируемых данных. 
        /// </summary>
        public int ForecastDaysCount { get; set; }

        /// <summary>
        /// Коллекция, содержащая отображаемую в данном окне информацию.
        /// </summary>
        public List<InformationItem> Information 
        {
            get
            {
                return dgvInformation.DataSource as List<InformationItem>;
            }
            set
            {
                dgvInformation.DataSource = value;
            }
        }

        /// <summary>
        /// Предоставляет форму, содержащую информацию типа <c>type</c>
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
                case InformationType.MultipleRegression:
                    this.Text = "Многофакторная регрессия (цена на золото, индекс Доу-Джонса)";
                    break;
            }

        }

        /// <summary>
        /// Отображает данные, полученные при расчете полиномиальной регрессии, и возвращает отображаемую коллекцию.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="coefficients">Коллекция коэффициентов полинома</param>
        /// <param name="yValues">Значение расчетных У</param>
        /// <param name="forecastDays">Количество дней для прогноза</param>
        /// <returns>Коллекция экземпляров класса InformationItem</returns>
        public List<InformationItem> ShowRegression(IData data, double[] coefficients, double[] yValues, int forecastDays)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            this.YValues = yValues;
            this.ForecastDaysCount = forecastDays;

            var regression = new List<InformationItem>();
            double error = Regression.GetError(Data.Select(i => i.Value).ToArray(), yValues);

            regression.Add(new InformationItem("Степень полинома", (coefficients.Length - 1) + ""));
            regression.Add(new InformationItem("Погрешность", error));
            regression.Add(new InformationItem("Коэффициенты", ""));

            for (int i = 0; i < coefficients.Length; i++)
            {
                var format = Math.Round(Math.Abs(coefficients[i]), 3) < 0.001 ? "{0:e3}" : "{0:0.000}";
                regression.Add(new InformationItem("A[" + i + "]", String.Format(format, coefficients[i])));
            }

            regression.Add(new InformationItem("Расчетные значения: ", ""));
              
            if (forecastDays > 0)
            {
                var lastDate = Data.Last().Date;
                regression.Add(new InformationItem("Прогнозные значения:", ""));

                for (int i = yValues.Length - forecastDays; i < yValues.Length; i++)
                {
                    lastDate = lastDate.AddDays(1);
                    regression.Add(new InformationItem(lastDate.ToString("dd/MM/yyyy"), yValues[i]));
                }
            }
            Information = regression;

            return regression;
        }

        /// <summary>
        /// Отображает данные, полученные при расчете многофакторной регрессии, и возвращает отображаемую коллекцию.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="coefficients">Коллекция коэффициентов полинома</param>
        /// <param name="yValues">Значение расчетных У</param>
        /// <returns>Коллекция экземпляров класса InformationItem</returns>
        public List<InformationItem> ShowMultipleRegression(IData data, double[] coefficients, double[] yValues)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            this.YValues = yValues;

            if (coefficients.Length != 3)
                throw new ArgumentException("coefficients");

            var regression = new List<InformationItem>();

            regression.Add(new InformationItem("Y-пересечение", coefficients[0]));
            regression.Add(new InformationItem("Переменная X1", coefficients[1]));
            regression.Add(new InformationItem("Переменная X2", coefficients[2]));

            Information = regression;

            return regression;
        }

        /// <summary>
        /// Отображает данные, полученные при расчете вейвлет-преобразования, и возвращает отображаемую коллекцию.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="coefficients">Коллекция коэффициентов прямого вейвлет-преобразования</param>
        /// <param name="yValues">Значение расчетных У</param>
        /// <returns>Коллекция экземпляров класса InformationItem</returns>
        public List<InformationItem> ShowWavelet(IData data, double[] coefficients, double[] yValues)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            this.YValues = yValues;
            this.Wavelet = coefficients;

            var wl = new Wavelet();
            var wavelet = new List<InformationItem>();

            var CL = wl.GetD4CL();
            var CH = wl.GetHPFCoeffs(CL);
            double[] iCL, iCH;
            wl.GetInvCoeffs(CL, CH, out iCL, out iCH);

            wavelet.Add(new InformationItem("Коэффициенты прямого вейвлет-преобразования Добеши (D4)", ""));
            wavelet.Add(new InformationItem("Фильтры низких частот: ", ""));
            
            for (int i = 0; i < CL.Length; i++)
                wavelet.Add(new InformationItem("[" + (i + 1) + "]", CL[i]));
            
            wavelet.Add(new InformationItem("Фильтры высоких частот: ", ""));

            for (int i = 0; i < CH.Length; i++)
                wavelet.Add(new InformationItem("[" + (i + 1) + "]", CH[i]));

            wavelet.Add(new InformationItem("Коэффициенты обратного вейвлет-преобразования Добеши (D4)", ""));

            wavelet.Add(new InformationItem("Фильтры низких частот: ", ""));

            for (int i = 0; i < iCL.Length; i++)
                wavelet.Add(new InformationItem("[" + (i + 1) + "]", iCL[i]));
            
            wavelet.Add(new InformationItem("Фильтры высоких частот: ", ""));

            for (int i = 0; i < iCH.Length; i++)
                wavelet.Add(new InformationItem("[" + (i + 1) + "]", iCH[i]));

            /*
            wavelet.Add(new InformationItem("Результат прямого вейвлет-преобразования Добеши D4:",""));

            for (int i = 0; i < coefficients.Length; i++)
            {
                wavelet.Add(new InformationItem("["+i+"]", coefficients[i]));
            }*/

            Information = wavelet;

            return wavelet;
        }

        /// <summary>
        /// Отображает данные, полученные при Фурье-анализе и возвращает коллекцию отображаемых данных.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData</param>
        /// <param name="harmonics">Коллекция гармоник</param>
        /// <param name="yValues">Значение расчетных У</param>
        /// <param name="forecastDays">Количество дней для прогноза</param>
        /// <returns>Коллекция экземпляров класса InformationItem</returns>
        public List<InformationItem> ShowFourier(IData data, List<Harmonic> harmonics, double[] yValues, int forecastDays)
        {
            this.Data = data.Data;
            this.FullData = data.FullData;
            this.YValues = yValues;
            this.ForecastDaysCount = forecastDays;

            if (harmonics == null)
                return new List<InformationItem>();

            var fourier = new List<InformationItem>();

            double error = FourierTransform.GetError(data.Data.Select(i=>i.Value).ToArray(), yValues);
            fourier.Add(new InformationItem("Период", yValues.Length * 1));
            fourier.Add(new InformationItem("Δt", 1));
            fourier.Add(new InformationItem("Погрешность", error));
            fourier.Add(new InformationItem("Число гармоник", (harmonics.Count-1) + ""));

            for (int i = 1; i < harmonics.Count; i++)
            {
                fourier.Add(new InformationItem(i + " Гармоника", ""));
                fourier.Add(new InformationItem("Частота", harmonics[i].Frequency));
                fourier.Add(new InformationItem("Коэффициет Фурье a[" + i + "]",harmonics[i].A));
                fourier.Add(new InformationItem("Коэффициет Фурье b[" + i + "]",harmonics[i].B));
                fourier.Add(new InformationItem("Амплитуда", harmonics[i].Аmplitude));
                fourier.Add(new InformationItem("Фаза, °", harmonics[i].Phase * (180 / Math.PI)));
            }

            if (forecastDays > 0)
            {
                var lastDate = Data.Last().Date;
                fourier.Add(new InformationItem("Прогнозные значения:", ""));

                for (int i = YValues.Length - forecastDays; i < YValues.Length; i++)
                {
                    lastDate = lastDate.AddDays(1);
                    fourier.Add(new InformationItem(lastDate.ToString("dd/MM/yyyy"), yValues[i]));
                }
            }

            Information = fourier;

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
        /// <returns>Коллекция экземпляров класса InformationItem</returns>
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

            Information = statistics;

            return statistics;
        }
    }

    /// <summary>
    /// Тип информации, отображаемой в окне.
    /// </summary>
    public enum InformationType
    { 
        /// <summary>
        /// Элементарные статистики.
        /// </summary>
        Statistics,
        /// <summary>
        /// Полиномиальная регрессия.
        /// </summary>
        Regression,
        /// <summary>
        /// Многофакторная регрессия.
        /// </summary>
        MultipleRegression,
        /// <summary>
        /// Фурье-преобразование.
        /// </summary>
        Fourier,
        /// <summary>
        /// Вейвалет-преобразование.
        /// </summary>
        Wavelet 
    }

    /// <summary>
    /// Предосталяет клас, содержащий данные в формате Параметр - Значение.
    /// </summary>
    public class InformationItem
    {
        /// <summary>
        /// Параметр.
        /// </summary>
        [DisplayName("Параметр")]
        public string Parameter { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        [DisplayName("Значение")]
        public string Value { get; set; }

        /// <summary>
        /// Предосталяет клас, содержащий данные в формате Параметр - Значение.
        /// </summary>
        /// <param name="parameter">Параметр</param>
        /// <param name="value">Значение</param>
        public InformationItem(string parameter, string value)
        {
            this.Parameter = parameter;
            this.Value = value;
        }

        /// <summary>
        /// Предосталяет клас, содержащий данные в формате Параметр - Значение,
        /// где значение представлено в виде числа с плавающей запятой.
        /// </summary>
        /// <param name="parameter">Параметр</param>
        /// <param name="value">Значение</param>
        public InformationItem(string parameter, double? value)
        {
            this.Parameter = parameter;
            this.Value = String.Format("{0:0.000}",value);
        }
    }
}
