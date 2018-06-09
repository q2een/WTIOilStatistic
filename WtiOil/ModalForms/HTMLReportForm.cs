using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using static WtiOil.MainMDI;

namespace WtiOil
{
    public partial class HTMLReportForm : Form
    {
        /// <summary>
        /// Возвращает элемент управления для указания пути сохранения отчета.
        /// </summary>
        public Control ReportPath 
        {
            get
            {
                return this.panelPath;
            }
        }

        /// <summary>
        /// Возвращает элемент управления для указания данных при полиномиальной регрессии.
        /// </summary>
        public Control Regression
        {
            get
            {
                return this.panelRegression;
            }
        }

        /// <summary>
        /// Возвращает элемент управления для указания данных при Фурье-преобразовании.
        /// </summary>
        public Control Fourier
        {
            get
            {
                return this.panelFourier;
            }
        }

        /// <summary>
        /// Возвращает элемент управления для указания данных при многофакторной регрессии.
        /// </summary>
        public Control MultipleRegression
        {
            get
            {
                return this.panelMultiple;
            }
        }

        // Экземпляр родительского MDI окна.
        private readonly MainMDI main;

        // Экземпляр класса, реализующего интерфейс IData.
        private readonly IData data;

        // Экземпляр окна ChartForm, предназначенный для формирования отчетов.
        private ChartForm reportChart = new ChartForm();

        /// <summary>
        /// Предоставляет окно для формирования HTML отчета.
        /// </summary>
        /// <param name="context">Контекст, для которого происходит формирование отчета. Экземпляр класса MainMDI</param>
        /// <param name="data">Экземпляр класса, реализующий интерфейс IData</param>
        public HTMLReportForm(MainMDI context, IData data = null)
        {
            InitializeComponent();

            this.data = data;
            this.main = context;
            tbPath.Text = Directory.GetCurrentDirectory();
            folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();
        }

        #region Обработка введенных пользователем данных.
        /// <summary>
        /// Обрабатывает указанные пользователем файлы и возвращает массивы значений указанных данных.
        /// </summary>
        /// <param name="initialDateRange">Выходной параметр.Временной интервал для данных</param>
        /// <param name="x1">Выходной параметр. Массив значений индекса Доу-Джонса</param>
        /// <param name="x2">Выходной параметр. Массив значений цен на золото</param>
        public void GetMultipleData(out DateTime[] initialDateRange, out double[] x1, out double[] x2)
        {
            x1 = new double[] { };
            x2 = new double[] { };
            initialDateRange = null;
            try
            {
                // Получение данных из файлов.
                var gold = main.GetDataFromTextFile(tbFileGold.Text);
                var DowJones = main.GetDataFromTextFile(tbFileDowJones.Text);

                // Пересечение дат в списках в списках.
                var intersect = gold.Select(i => i.Date).Intersect(DowJones.Select(i => i.Date));
                intersect = intersect.Intersect(data.FullData.Select(i => i.Date)).OrderBy(i => i).ToList();

                if (intersect == null || intersect.Count() == 0)
                    throw new Exception("Исходные данные должны содержать пересекающийся временной интервал");

                initialDateRange = data.Data.Select(i => i.Date).ToArray();

                // Установить временные интервалы.
                Date.SetDateRange(data, intersect.First().Date, intersect.Last().Date);
                Date.SetDateRange(ref gold, intersect.First().Date, intersect.Last().Date);
                Date.SetDateRange(ref DowJones, intersect.First().Date, intersect.Last().Date);

                // Получение значений зависимой и независимых переменных.
                double[] yValues = data.Data.Select(i => i.Value).ToArray();

                x1 = DowJones.Select(i => i.Value).ToArray();
                x2 = gold.Select(i => i.Value).ToArray();                
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Исходные данные не содержат данный временной интервал.\nУкажите другие данные.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Проверяет доступность пути <c>path</c>.
        /// </summary>
        /// <param name="path">Путь к папке</param>
        private void CheckPath(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Невозможно сохранить отчет в выбранный путь. Укажите другой каталог для отчета.");
            }
        }

        /// <summary>
        /// Обрабатывает указанный пользователем путь и возврщает его.
        /// </summary>
        /// <returns>Путь к папке</returns>
        internal string GetReportPath()
        {
            CheckPath(tbPath.Text);
            return tbPath.Text;
        }
        #endregion

        #region Get InformationForm Methods.

        /// <summary>
        /// Возвращает экземпляр класса InformationForm, содержащий данные об элементарых статистиках.
        /// </summary>
        /// <returns>Экземпляр класса InformationForm с типом <c>InformationType.Statistics</c></returns>
        internal InformationForm GetStatistics()
        {
            return main.GetStatisticsResult(data, cbAverage.Checked,
                                           cbStandartError.Checked,
                                           cbMedian.Checked,
                                           cbMode.Checked,
                                           cbStandardDeviation.Checked,
                                           cbDispersion.Checked,
                                           cbSkewness.Checked,
                                           cbKurtosis.Checked,
                                           cbInterval.Checked,
                                           cbMin.Checked,
                                           cbMax.Checked,
                                           cbSum.Checked);
        }

        /// <summary>
        /// Возвращает экземпляр класса InformationForm, содержащий данные о полиномиальной регрессии.
        /// </summary>
        /// <returns>Экземпляр класса InformationForm с типом <c>InformationType.Regression</c></returns>
        internal InformationForm GetRegression()
        {
            try
            {
                byte degree = Byte.Parse(tbDegree.Text);
                int forecastDays = cbRegressionForecast.Checked ? Int32.Parse(numRegressionDays.Value + "") : 0;

                return main.GetRegressionResult(data, degree, forecastDays);
            }
            catch (Exception)
            {
                throw new Exception("Введите корректную степень полинома (от 0 до 255)");
            }
        }

        /// <summary>
        /// Возвращает экземпляр класса InformationForm, содержащий данные о Фурье-преобразовании.
        /// </summary>
        /// <returns>Экземпляр класса InformationForm с типом <c>InformationType.Fourier</c></returns>
        internal InformationForm GetFourier()
        {
            try
            {
                byte harmonics = Byte.Parse(tbHarmonics.Text);
                int forecastDays = cbFourierForecast.Checked ? Int32.Parse(numFourierDays.Value + "") : 0;

                return main.GetFourierResult(data, harmonics, forecastDays);
            }
            catch (Exception)
            {
                throw new Exception("Введите корректное число гармоник (от 0 до 255)");
            }
        }

        /// <summary>
        /// Возвращает экземпляр класса InformationForm, содержащий данные о многофакторной регрессии.
        /// </summary>
        /// <returns>Экземпляр класса InformationForm с типом <c>InformationType.MultipleRegression</c></returns>
        internal InformationForm GetMultiple()
        {
            double[] x1, x2;
            DateTime[] dates;

            GetMultipleData(out dates, out x1, out x2);
            var multiple = main.GetMultipleResult(data, x1, x2);
            Date.SetDateRange(data, dates.First(), dates.Last());

            return multiple;
        }

        #endregion

        #region Формирование отчета.

        /// <summary>
        /// Формирует HTML разметку блока, содержащего информацию из экземпляра класса <c>form</c>.
        /// </summary>
        /// <param name="form">Экземпляр класса <c>InformationForm</c>, содержащий информацию для отчета</param>
        /// <param name="context">Контекст, в котором происходит формирование блока. Экземпляр класса <c>HTMLReportBuilder</c></param>
        /// <param name="directoryPath">Путь к папке с ресурсами для отчета</param>
        /// <param name="drawFunc">Функция для отображения данных на графике</param>
        /// <returns>HTML разметка блока с информацией</returns>
        private string GetHTMLBlock(InformationForm form, HTMLReportBuilder context, string directoryPath, DrawFunc drawFunc)
        {
            string path = null;

            if (form.YValues != null && form.YValues.Length > 0)
            {
                path = directoryPath + form.Type + ".png";
                SaveChartImage(path, drawFunc, form, form.YValues, form.ForecastDaysCount);
            }

            return context.GetBlockByType(form.Type, form.Information, path);
        }

        /// <summary>
        /// Сохраняет изображение графика.
        /// </summary>
        /// <param name="path">Путь к сохраняемому файлу, включая имя файла</param>
        /// <param name="drawFunc">Функция для отображения данных на графике</param>
        /// <param name="data">Экземпляр класса, реализующего интерфейс <c>IData</c></param>
        /// <param name="yPoints">Массив значений Y</param>
        /// <param name="forecastDaysCount">Количество прогнозируемых дней</param>
        private void SaveChartImage(string path, DrawFunc drawFunc, IData data, double[] yPoints, int forecastDaysCount)
        {
            reportChart.RemoveAllSeries();
            reportChart.Size = new System.Drawing.Size(1280, 720);
            drawFunc.Invoke(data, yPoints, forecastDaysCount);
            reportChart.SaveChart(path);
        }

        /// <summary>
        /// Формирует HTML отчет, содержащий данные из <c>forms</c> и сихраняет их в путь <c>path</c>.
        /// </summary>
        /// <param name="path">Путь, для сохранения отчета</param>
        /// <param name="forms">Коллекция форм, содержащих данные для сохранения</param>
        public void BuildReport(string path, IEnumerable<InformationForm> forms)
        {
            if (!Directory.Exists(path))
                throw new Exception(path + "\nДанный путь не существует.");

            if (forms == null || forms.Count() == 0)
                throw new Exception("Отсутствуют данные для формирования отчета.");

            var postfix = DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
            string resources = path + @"/resources" + postfix + "/";

            Directory.CreateDirectory(resources);

            HTMLReportBuilder html = new HTMLReportBuilder();

            // Добавление в отчет исходных данных.
            SaveChartImage(resources + @"/data.png", reportChart.DrawChart, forms.First(), null, 0);
            string result = html.GetDataBlock(forms.First().Data, resources + @"/data.png");

            foreach (var form in forms.OrderBy(i => i.Type))
            {
                var func = main.GetDrawFunctionByType(form.Type, reportChart);
                result += GetHTMLBlock(form, html, resources, func);

                if (form.Type != InformationType.Wavelet)
                    continue;

                var waweletPath = resources + "WaveletFunc.png";
                SaveChartImage(waweletPath, reportChart.DrawWaveletFunc, form, form.Wavelet, form.ForecastDaysCount);
                result += html.GetImageBlock(waweletPath, "Результат прямого вейвлет-преобразования");
            }

            var report = html.GetDocumentStructure("Отчет", result);
            File.WriteAllText(path + "\\Отчет" + postfix + ".html", report);
        }

        #endregion

        #region Обработка событий.
        // Обработка события ввода данных в текстовое поле. Разрешен ввод только цифр.
        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;

            if (!Char.IsDigit(key) && key != 8)
                e.Handled = true;
        }

        // Обработка события выбора папки для сохранения отчета.
        private void btnPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                tbPath.Text = folderBrowserDialog.SelectedPath;
        }

        // "Добавить в отчет полиномиальную регрессию". Обработка события изменения состояния флага.
        private void cbRegressionBlock_CheckedChanged(object sender, EventArgs e)
        {
            Regression.Enabled = cbRegressionBlock.Checked;
        }

        // "Добавить в отчет Фурье-анализ". Обработка события изменения состояния флага.
        private void cbFourierBlock_CheckedChanged(object sender, EventArgs e)
        {
            Fourier.Enabled = cbFourierBlock.Checked;
        }
        
        // "Добавить в отчет многофакторную регрессию". Обработка события изменения состояния флага.
        private void cbMultipleBlock_CheckedChanged(object sender, EventArgs e)
        {
            MultipleRegression.Enabled = cbMultipleBlock.Checked;
        }

        // "Добавить в отчет следующие элементарные статистики". Обработка события изменения состояния флага.
        private void cbStatistics_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control cont in groupStatistics.Controls)
            {
                if (!(cont is CheckBox) || cont.Equals(sender))
                    continue;

                cont.Enabled = cbStatisticsBlock.Checked;
            }
        }

        // "Сформировать отчет". Обработка события нажатия на кнопку.
        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                CheckPath(tbPath.Text);
                List<InformationForm> forms = new List<InformationForm>();

                if (cbStatisticsBlock.Checked)
                    forms.Add(GetStatistics());

                if (cbRegressionBlock.Checked)
                    forms.Add(GetRegression());

                if (cbFourierBlock.Checked)
                    forms.Add(GetFourier());

                if (cbMultipleBlock.Checked)
                    forms.Add(GetMultiple());

                if (cbWaveletBlock.Checked)
                    forms.Add(main.GetWaveletResult(data));

                BuildReport(GetReportPath(), forms);
                MessageBox.Show("Отчет успешно сохранен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  

        }

        // Обработка события смены значения элементов управления numericUpDown.
        private void numDays_ValueChanged(object sender, EventArgs e)
        {
            string value = "";

            switch (Int32.Parse((sender as NumericUpDown).Value + ""))
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

            if ((sender as NumericUpDown).Name == "numRegressionDays")
                lblRegressionForecastDays.Text = value;
            else
                lblFourierForecastDays.Text = value;
        }

        // Обработка события нажатия на кнопку "Обзор".
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((sender as Button).Name == "btnOpenGold")
                    tbFileGold.Text = openFileDialog.FileName;
                else
                    tbFileDowJones.Text = openFileDialog.FileName;
            }
        }
        
        // "Добавить прогноз на ...". Обработка события изменения состояния флага.
        private void cbForecast_CheckedChanged(object sender, EventArgs e)
        {
            numRegressionDays.Enabled = lblRegressionForecastDays.Enabled = cbRegressionForecast.Checked;
        }
        
        // "Добавить прогноз на ...". Обработка события изменения состояния флага.
        private void cbFourierForecast_CheckedChanged(object sender, EventArgs e)
        {
            numFourierDays.Enabled = lblFourierForecastDays.Enabled = cbFourierForecast.Checked;
        }

        //  "Отмена". Обтаботка события нажатия на кнопку.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        #endregion
    }
}
