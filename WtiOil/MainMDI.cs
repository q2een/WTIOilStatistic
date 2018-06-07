using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace WtiOil
{
    /* TODO
     1. Сделать прогноз на дату
     2. Доделать / сделать отчеты
     3. Добавить справку
     4. Добавить картинку в О программе. 
     */
    public partial class MainMDI : Form
    {
        /// <summary>
        /// Количество дочерних окон типа "DataForm".
        /// </summary>
        private int childFormNumber = 0;
        private List<ToolStripMenuItem> ChartSeriesItems { get; set; }
        private ChartForm reportChart = new ChartForm();

        // Конструктор класса.
        public MainMDI()
        {
            InitializeComponent();
            ChartSeriesItems = new List<ToolStripMenuItem>();
            InitialElementsState();
        }

        /// <summary>
        /// Получает структурированные данные из файла <c>path</c> 
        /// и возвращает эти данные как коллекцию объектов <c>DataItem</c>.
        /// </summary>
        /// <param name="path">Путь к *.csv файлу</param>
        /// <returns>Коллекция объектов <c>ItemWTI</c></returns>
        public List<DataItem> GetDataFromTextFile(string path)
        {
            var result = new List<DataItem>();
            try
            {                
                string fileData = File.ReadAllText(path);

                result = fileData.Split('\n').Where(z => z.Trim() != String.Empty).Select(i => 
                    {
                        var line = i.Split(';');
                        var date = DateTime.Parse(line[0]);
                        var value = Double.Parse(line[1].Replace('.', ','));
                        return new DataItem(date, value);
                    }).OrderBy(x=> x.Date).ToList();
            }
            catch (Exception ex)
            {
                result = null;
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Возвращает имя файла из пути <c>path</c>.
        /// </summary>
        /// <param name="path">Полный путь к файлу</param>
        /// <returns>Имя файла из пути к файлу</returns>
        private string GetFileNameFromPath(string path)
        {
            return path.Split('\\').Last();
        }

        // Добавляет в меню "График" ряды, которые отображены на графике окна ChartForm.
        private void AddChartMI(Series series)
        {
            if (chartMI.DropDownItems.ContainsKey(series.Name))
                chartMI.DropDownItems.RemoveByKey(series.Name);

            if (ChartSeriesItems == null)
                ChartSeriesItems = new List<ToolStripMenuItem>();

            ToolStripMenuItem mi = new ToolStripMenuItem(series.LegendText);
            mi.CheckOnClick = true;
            mi.Checked = series.Enabled;
            mi.Name = series.Name;
            mi.Tag = series;
            mi.BackColor = Color.FromArgb(80, series.Color);
            mi.Click += showChartSeries_Click;
            ChartSeriesItems.Add(mi);

            chartMI.DropDownItems.Add(mi);
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
        /// <param name="addNumberCount">Количество элементов, которые надо добавить в массив</param>
        /// <returns>Расширенный массив значений</returns>
        private double[] GetNumericList(double[] initialArr, int addNumberCount)
        {
            var newArr = Enumerable.Range(initialArr.Length, addNumberCount).Select(i => i + 0.0);
            var result = initialArr.ToList();
            result.AddRange(newArr);

            return result.ToArray();
        }

        public InformationForm GetStatisticsResult(IData data,  bool isAverage,
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
            var inform = new InformationForm(InformationType.Statistics);
            inform.ShowStatistic(data,isAverage,
                                      isStandardError,
                                      isMediana,
                                      isMode,
                                      isStandardDeviation,
                                      isDispersion,
                                      isSkewness,
                                      isKurtosis,
                                      isInterval,
                                      isMin,
                                      isMax,
                                      isSum);
            return inform;
        }

        public InformationForm GetRegressionResult(IData data, byte count, int forecastDaysCount)
        {
            double[] xValues = Enumerable.Range(1, data.Data.Count).Select(z => z + 0.0).ToArray();
            double[] yValues = data.Data.Select(i => i.Value).ToArray();

            var coeff = Regression.GetCoefficients(xValues, yValues, count);

            var x = GetNumericList(xValues, forecastDaysCount);

            var y = Regression.GetYFromXValue(coeff, x);

            var inform = new InformationForm(InformationType.Regression);
            inform.ShowRegression(data, coeff, y, forecastDaysCount);

            return inform;
        }

        public InformationForm GetFourierResult(IData data, byte count, int forecastDaysCount)
        {
            double[] xValues = Enumerable.Range(1, data.Data.Count).Select(z => z + 0.0).ToArray();
            double[] yValues = data.Data.Select(i => i.Value).ToArray();

            var harmonics = FourierTransform.GetHarmonics(1.0 / (1.0 * xValues.Length), 1, count, yValues);

            var x = GetNumericList(xValues, forecastDaysCount);

            var y = FourierTransform.GetYFromXValue(harmonics, x, yValues.Average());

            var inform = new InformationForm(InformationType.Fourier);
            inform.ShowFourier(data, harmonics, y, forecastDaysCount);

            return inform;
        }

        public InformationForm GetMultipleResult(IData data, double[] dowJones, double[] gold)
        {
            double[] yValues = data.Data.Select(i => i.Value).ToArray();

            var coeffs = Regression.GetMultipleRegressionCoefficients(yValues, dowJones, gold);
            var y = Regression.GetMultipleYFromXValue(coeffs, dowJones,gold);

            var inform = new InformationForm(InformationType.MultipleRegression);
            inform.ShowMultipleRegression(data, coeffs, y);

            return inform;
        }

        public InformationForm GetWaveletResult(IData data)
        {
            double[] yValues = data.Data.Select(i => i.Value).ToArray();

            var coeffs = Wavelet.D4Transform(yValues);
            var y = Wavelet.InverseD4Transform(coeffs);

            var inform = new InformationForm(InformationType.Wavelet);
            inform.ShowWavelet(data, coeffs, y);

            return inform;
        }
        
        /// <summary>
        /// Отрисовывает линию тренда на графике.
        /// </summary>
        /// <param name="showInformation">Флаг, указывающий необходимо ли показывать результат расчетов.</param>
        /// <param name="coefficients">Коэффициенты полиномиальной регрессии</param>
        /// <param name="yValues">Рассчетные значения У(х)</param>
        public void ShowLineTrend(bool showInformation, double[] coefficients, double[] yValues, int forecastDaysCount)
        {
            var data = this.ActiveMdiChild as IData;
            
            if (data == null)
                return;

            var chart = GetChartForm();
            chart.DrawTrend(data, yValues, forecastDaysCount);
            chart.Show();
            chart.Activate();

            if (showInformation)
            {
                var inform = GetInformationForm(InformationType.Regression, data);
                inform.ShowRegression(data, coefficients, yValues, forecastDaysCount);
                inform.Show();
                inform.Activate();
            }
        }

        /// <summary>
        /// Отрисовывает синтезированную функцию при Фурье-анализе.
        /// </summary>
        /// <param name="showInformation">Флаг, указывающий необходимо ли показывать результат расчетов.</param>
        /// <param name="harmonics">Коллекция гармоник.</param>
        /// <param name="yValues">Рассчетные значения У(х)</param>
        public void ShowFourier(bool showInformation, List<Harmonic> harmonics, double[] yValues, int forecastDaysCount)
        { 
            var data = this.ActiveMdiChild as IData;
            
            if (data == null)
                return;

            var chart = GetChartForm();
            chart.DrawFourier(data, yValues, forecastDaysCount);
            chart.Show();
            chart.Activate();

            if (showInformation)
            {
                var inform = GetInformationForm(InformationType.Fourier,data);
                inform.ShowFourier(data, harmonics, yValues, forecastDaysCount);
                inform.Show();
                inform.Activate();
            }
                        
        }

        /// <summary>
        /// Оторбражает линию тренда, полученную при многофакторной регрессии
        /// </summary>
        /// <param name="yValues">Рассчетные значения У(х)</param>
        /// <param name="coefficients">Коеффициенты многофакторной регрессии</param>
        public void ShowMultiple(double[] yValues, double[] coefficients)
        {
            var data = this.ActiveMdiChild as IData;

            if (data == null)
                return;

            var chart = GetChartForm();
            chart.DrawMultipleRegression(data, yValues);
            chart.Show();
            chart.Activate();

            var inform = GetInformationForm(InformationType.MultipleRegression,data);
            inform.ShowMultipleRegression(data, coefficients, yValues);
            inform.Show();
        }
        
        /// <summary>
        /// Возвращает максимальную степень 2, которую содержит число <c>number</c>
        /// </summary>
        /// <param name="number">Число, в котором необходимо найти максимальную степень 2</param>
        /// <returns>Максимальная степень 2, которую содержит число</returns>
        private int GetMaxPowOf2(int number)
        {
            int pow = 0;
            while (1 << pow < number)
                pow++;

            return --pow;
        }

        /// <summary>
        /// Возвращает истину, если <c>first</c> равен <c>second</c>.
        /// </summary>
        /// <param name="first">Экземпляр класса, реализующий IData</param>
        /// <param name="second">Экземпляр класса, реализующий IData</param>
        /// <returns>Результат сравнения</returns>
        private bool isIDataEquals(IData first, IData second)
        {
            return first.FullData == second.FullData;
        }

        #region Состояние элементов управления.
        
        // Начальное состояние элементов управеления.
        public void InitialElementsState()
        {
            SetDataMenuitems(false, false, false);
            SetChartMenuItemsState(false,false,false);
            SetStatisticItemsState(false);
            SetReportItemsState(false);
            lblFileName.Text = "";
        }

        // Состояние элементов управления для классов, реализующих IData.
        private void SetIDataObjectsState(bool isVisible)
        {
            dataMI.Visible = isVisible;
            saveMI.Visible = saveTSB.Visible = isVisible;
            saveAsMI.Visible = isVisible;
        }

        // Состояние элементов меню "Временной ряд".
        private void SetDataMenuitems(bool isFourier, bool isWavelet, bool isMultiple)
        {
            fourierMI.Visible = fourierTSB.Visible = isFourier;
            waveletMI.Visible = waveletTSB.Visible = isWavelet;
            multipleMI.Visible = multipleTSB.Visible = isMultiple;
            dataSeparatorTSB.Visible = isFourier || isWavelet || isMultiple;
            dataSeparatorMI.Visible = isFourier || isWavelet;
        }

        // Состояние элементов меню "График".
        private void SetChartMenuItemsState(bool isDrawChart, bool isLegend, bool isDrawTrend)
        {
            chartMI.Visible = chartSeparatorTSB.Visible = isDrawChart || isDrawTrend;
            drawChartMI.Visible = drawChartTSB.Visible = isDrawChart;
            showLegendMI.Visible = legendSeparatorMI.Visible = scaleMI.Visible = isLegend;
            drawTrendLineMI.Visible = drawTrendLineTSB.Visible = isDrawTrend;
        }

        // Изменение состояний переключателей видимости рядов графика. 
        private void SwitchChartSeriesMIState(bool isVisible)
        {
            foreach (var mi in ChartSeriesItems)
                mi.Visible = isVisible;
        }

        // Состояние элементов меню "Статистика".
        private void SetStatisticItemsState(bool isVisible)
        {
            statisticsMI.Visible = calculateTSB.Visible = isVisible;
        }

        // Состояние элементов меню "Отчет".
        private void SetReportItemsState(bool isVisible)
        {
            reportMI.Visible = repotTSB.Visible = isVisible;
        }

        #endregion

        #region Получение или открытие дочерних форм.

        private DataForm GetNewDataForm(string filename, List<ItemWTI> data = null)
        {
            DataForm childForm = new DataForm(data);
            childForm.MdiParent = this;
            childForm.Text = GetFileNameFromPath(filename);
            childForm.Tag = childFormNumber;
            return childForm;
        }

        private ChartForm GetChartForm()
        {
            var openedFrom = this.MdiChildren.FirstOrDefault(i => i is ChartForm) as ChartForm;
            var form = openedFrom == null ? new ChartForm() : openedFrom;
            form.OnSeriesChanged += chart_OnSeriesChanged;
            form.MdiParent = this;
            return form;
        }

        /// <summary>
        /// Возвращает экземпляр открытой формы InformationForm с типом <c>type</c>.
        /// В случае отстутствия открытой формы создает новый экземпляр и возвращает его.
        /// </summary>
        /// <param name="type">Тип формы InformationForm</param>
        /// <returns>Экземпляр формы InformationForm</returns>
        private InformationForm GetInformationForm(InformationType type,IData data)
        {            
            var openedFroms = this.MdiChildren.Where(i => i is InformationForm);

            var form = openedFroms.Where(i => ((i as InformationForm).Type == type && isIDataEquals((i as InformationForm), data))).FirstOrDefault();

            if (form != null)
                return form as InformationForm;
            
            InformationForm inform = new InformationForm(type);
            inform.MdiParent = this;

            return inform;
        }

        #endregion
        
        #region Обработка событий.

        // Создает новый экземпляр класса DataForm и отображает его.
        private void ShowNewForm(object sender, EventArgs e)
        {
            GetNewDataForm("БезНазвания" + ++childFormNumber).Show();
        }

        // Открывает файл и отображает его содержимое в окне DataForm.
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Файл CSV (*.csv)|*.csv";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                var data = GetDataFromTextFile(FileName).Select(i => new ItemWTI(i.Date, i.Value)).ToList();
                var form = GetNewDataForm(FileName, data);
                form.Show();
            }
        }

        // Обработка события изменнения рядов графика.
        private void chart_OnSeriesChanged(SeriesCollection seriesColl, int index)
        {
            foreach (var series in seriesColl)
            {
                AddChartMI(series);
            }

            foreach (var item in ChartSeriesItems)
            {
                if (!seriesColl.Contains(item.Tag as Series))
                    chartMI.DropDownItems.Remove(item);
            }
        }

        // Обработка события нажития на пункт меню "Сохранить как".
        private void saveAsMI_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        // Обработка события нажития на пункт меню "Выход".
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Обработка события нажития на пункт меню "Показать панель инструментов".
        private void showToolBarMI_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = showToolBarMI.Checked;
        }

        // Обработка события нажития на пункт меню "Показать строку состояния".
        private void showStatusBarMI_Click(object sender, EventArgs e)
        {
            statusBar.Visible = showStatusBarMI.Checked;
        }

        // Обработка события смены активного дочернего MDI-окна.
        // Происходит изменение состояний элементов на главной форме 
        // в зависимости от активной дочерней формы.
        private void MainMDI_MdiChildActivate(object sender, EventArgs e)
        {
            // Состояние элементов управления для классов, реализующих IData.
            SetIDataObjectsState(this.ActiveMdiChild is IData);

            // Пункт меню "Окно".
            windowsMI.Visible = this.ActiveMdiChild != null;

            // Нет активных окон - переход в исходное состояние.
            if (this.ActiveMdiChild == null)
            {
                InitialElementsState();
                return;
            }

            // Строка состояния.
            lblFileName.Text = this.ActiveMdiChild.Text;

            if (this.ActiveMdiChild is DataForm || this.ActiveMdiChild is ChartForm)
            {
                bool state = this.ActiveMdiChild is DataForm;

                SetDataMenuitems(true, true, true);
                SetChartMenuItemsState(true, !state, true);
                SetStatisticItemsState(state);
                SetReportItemsState(state);

                // Удаление элементов меню, содержащих список рядов графика.
                SwitchChartSeriesMIState(!state);
            }

            if (this.ActiveMdiChild is InformationForm)
            {
                SetReportItemsState(false);
                SwitchChartSeriesMIState(false);

                // Управление видимостью элементов при разных типах окна. 
                switch ((this.ActiveMdiChild as InformationForm).Type)
                {
                    case InformationType.Statistics:
                        SetStatisticItemsState(true);
                        SetChartMenuItemsState(false, false, false);
                        SetDataMenuitems(false, false, false);
                        break;
                    case InformationType.Regression:
                        SetStatisticItemsState(false);
                        SetChartMenuItemsState(true, false, true);
                        SetDataMenuitems(false, false, false);
                        break;
                    case InformationType.Fourier:
                        SetChartMenuItemsState(true, false, false);
                        SetStatisticItemsState(false);
                        SetDataMenuitems(true, false, false);
                        break;
                    case InformationType.Wavelet:
                        SetChartMenuItemsState(true, false, false);
                        SetStatisticItemsState(false);
                        SetDataMenuitems(false, true, false);
                        break;
                    case InformationType.MultipleRegression:
                        SetChartMenuItemsState(true, false, false);
                        SetStatisticItemsState(false);
                        SetDataMenuitems(false, false, true);
                        break;

                }
            }
        }
        
        #region Вид отображения окон
        private void CascadeMI_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalMI_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalMI_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllMI_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region График. Пункт меню.

        // Обработка события нажития на пункт меню "Построить график".
        private void drawChartMI_Click(object sender, EventArgs e)
        {
            if (!(ActiveMdiChild is IData))
                return;

            var chartForm = GetChartForm();

            if (ActiveMdiChild is InformationForm)
            {
                var activeFrom = (ActiveMdiChild as InformationForm);

                switch (activeFrom.Type)
                {
                    case InformationType.Statistics:
                        break;
                    case InformationType.Regression:
                        chartForm.DrawTrend(activeFrom, activeFrom.YValues, activeFrom.ForecastDaysCount);
                        break;
                    case InformationType.MultipleRegression:
                        chartForm.DrawMultipleRegression(activeFrom, activeFrom.YValues);
                        break;
                    case InformationType.Fourier:
                        chartForm.DrawFourier(activeFrom, activeFrom.YValues, activeFrom.ForecastDaysCount);
                        break;
                    case InformationType.Wavelet:
                        chartForm.DrawWaveletD4(activeFrom, activeFrom.YValues);
                        break;
                }
            }
            else
                chartForm.DrawChart(this.ActiveMdiChild as IData);

            chartForm.Show();
            chartForm.Activate();
        }

        // Обработка события нажития на пункт меню "Построить линию тренда".
        private void drawTrendLineMI_Click(object sender, EventArgs e)
        {
            var data = (this.ActiveMdiChild as IData);
            var polynimForm = new CountSetForm(data.Data, CountSetType.Regression);
            polynimForm.Show(this);
        }
        
        // Обработка события нажития на пункт меню "Отобразить легенду".
        private void showLegendMI_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
                return;
            if (!(ActiveMdiChild is ChartForm))
                return;

            (ActiveMdiChild as ChartForm).ShowLegend(showLegendMI.Checked);
        }

        // Обработка события нажития на пункт меню "Масштабировать".
        private void scaleMI_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
                return;
            if (!(ActiveMdiChild is ChartForm))
                return;

            (this.ActiveMdiChild as ChartForm).ScaleChart();
        }

        // Обработка события нажития на пункты меню, содержащие ряды графика.
        private void showChartSeries_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
                return;
            if (!(ActiveMdiChild is ChartForm))
                return;

            var mi = sender as ToolStripMenuItem;

            if (mi == null)
                return;

            (mi.Tag as Series).Enabled = mi.Checked;
        }
        
        #endregion

        #region Временной ряд. Пункт меню.

        // Обработка события нажития на пункт меню "Указать период".
        private void dateRangeMI_Click(object sender, EventArgs e)
        {
            var data = this.ActiveMdiChild as IData;

            if (data == null)
                return;

            var dateRange = new DateRangePickerForm(data);
            dateRange.Show();
        }

        // Обработка события нажития на пункт меню "Фурье-анадиз".
        private void fourierMI_Click(object sender, EventArgs e)
        {
            var polynimForm = new CountSetForm((this.ActiveMdiChild as IData).Data, CountSetType.Fourier);
            polynimForm.Show(this);
        }

        // Обработка события нажития на пункт меню "Вейвлет-анализ".
        private void waveletMI_Click(object sender, EventArgs e)
        {
            var idata = (this.ActiveMdiChild as IData);
            idata.Data = idata.Data.Skip(idata.Data.Count - (1 << GetMaxPowOf2(idata.Data.Count))).ToList();

            var data = (this.ActiveMdiChild as IData).Data.Select(i => i.Value).ToArray();
            var coeffs = Wavelet.D4Transform(data);
            var y = Wavelet.InverseD4Transform(coeffs);

            var chart = GetChartForm();
            chart.DrawWaveletD4(this.ActiveMdiChild as IData, y);
            chart.Show();
            chart.Activate();

            var inform = GetInformationForm(InformationType.Wavelet, idata);
            inform.ShowWavelet(idata, coeffs, y);
            inform.Show();
        }

        // Обработка события нажития на пункт меню "Множественная регрессия".
        private void miltipleMI_Click(object sender, EventArgs e)
        {
            var data = this.ActiveMdiChild as IData;
            new MultipleRegressionForm(data).Show(this);
        }

        #endregion

        // Обработка события нажития на пункт меню "Статистика -> Расчитать".
        private void calculateMI_Click(object sender, EventArgs e)
        {
            IData active = (this.ActiveMdiChild as IData);
            var statisticForm = GetInformationForm(InformationType.Statistics, active);

            statisticForm.ShowStatistic(active, averageMI.Checked,
                                                     standardErrorMI.Checked,
                                                     medianMI.Checked,
                                                     modeMI.Checked,
                                                     standardDeviationMI.Checked,
                                                     dispersionMI.Checked,
                                                     skewnessMI.Checked,
                                                     kurtosisMI.Checked,
                                                     intervalMI.Checked,
                                                     minMI.Checked,
                                                     maxMI.Checked,
                                                     sumMI.Checked);
            statisticForm.Show();
            statisticForm.Activate();
        }

        delegate void DrawFunc(IData data, double[] yValues, int forecastDaysCount);

        private DrawFunc GetDrawFunctionByType(InformationType type)
        {
            switch (type)
            {
                case InformationType.Regression:
                    return reportChart.DrawTrend;
                case InformationType.Fourier:
                    return reportChart.DrawFourier;
                case InformationType.MultipleRegression:
                    return reportChart.DrawMultipleRegression;
                case InformationType.Wavelet:
                    return reportChart.DrawWaveletD4;
            }
            return null;   
        }

        private string GetHTMLBlock(InformationForm form, HTMLReportBuilder context, string directoryPath, DrawFunc drawFunc)
        {
            string path = null;

            if (form.YValues != null && form.YValues.Length > 0)
            {
                path = directoryPath + form.Type+".png";
                SaveChartImage(path, drawFunc, form, form.YValues, form.ForecastDaysCount);
            }

            return context.GetBlockByType(form.Type, form.Information, path);
        }

        private void SaveChartImage(string path, DrawFunc drawFunc, IData data, double[] yPoints, int forecastDaysCount)
        {
            reportChart.RemoveAllSeries();
            reportChart.Size = new Size(1280, 720);
            drawFunc.Invoke(data, yPoints, forecastDaysCount);
            reportChart.SaveChart(path);
        }

        public void BuildReport(string path, IEnumerable<InformationForm> forms)
        {
            if (!Directory.Exists(path))
                throw new Exception(path + "\nДанный путь не существует.");

            if (forms == null || forms.Count() == 0)
                throw new Exception("Отсутствуют данные для формирования отчета.");

            string resources = path + @"/resources/";
            Directory.CreateDirectory(resources);

            HTMLReportBuilder html = new HTMLReportBuilder();

            SaveChartImage(resources + @"/data.png", reportChart.DrawChart, forms.First(), null, 0);

            string result = html.GetDataBlock(forms.First().Data, resources + @"/data.png");

            foreach (var form in forms.OrderBy(i=>i.Type))
            {
                var func = GetDrawFunctionByType(form.Type);

                result += GetHTMLBlock(form, html, resources, func);

                if (form.Type != InformationType.Wavelet)
                    continue;

                var waweletPath = resources + "WaveletFunc.png";
                SaveChartImage(waweletPath, reportChart.DrawWaveletFunc, form, form.Wavelet, form.ForecastDaysCount);
                result += html.GetImageBlock(waweletPath, "Результат прямого вейвлет-преобразования");
                
            }

            var report = html.GetDocumentStructure("Отчет", result);
            File.WriteAllText(path + @"/report.html", report);
        }

        public void BuildReport(string path,
                                 bool isStatisticsBlock,
                                 bool isAverage,
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
                                 bool isSum,
                                 bool isRegressionBlock,
                                 int degree,
                                 bool isFourierBlock,
                                 int harmonicsCount)
        {
            string resources = path + @"/resources";
            Directory.CreateDirectory(resources);
            HTMLReportBuilder html = new HTMLReportBuilder();
            string statisticsBlock = "", regressionBlock = "", fourierBlock = "";

            // Images.
            var sourceFunc = resources + "//sourcefunc.png";
            var trend = resources + "//trend.png";
            var fourier = resources + "//fourier.png";
            
            var iData = (this.ActiveMdiChild as IData);
            var data = iData.Data;

            double[] xValues = Enumerable.Range(1, data.Count).Select(z => z + 0.0).ToArray();
            double[] yValues = data.Select(i => i.Value).ToArray();

            var chart = new ChartForm();
            chart.Size = new System.Drawing.Size(1280, 720);
            chart.DrawChart(iData);
            chart.SaveChart(sourceFunc);

            var dataBlock = html.GetDataBlock(data, sourceFunc);


            var inform = new InformationForm(InformationType.Statistics);
            
            if (isStatisticsBlock)
            {
                var statistics = inform.ShowStatistic(iData, isAverage, isStandardError, isMediana, isMode, isStandardDeviation, isDispersion, isSkewness, isKurtosis, isInterval, isMin, isMax, isSum);

                statisticsBlock = html.GetStatisticsBlock(statistics);
            }

            if (isRegressionBlock)
            {
                var coeff = Regression.GetCoefficients(xValues, yValues, (byte)degree);
                var y = Regression.GetYFromXValue(coeff, xValues);
                //var regressionInfo = inform.ShowRegression(iData, coeff, y);

                throw new NotImplementedException();
                //chart.DrawTrend(iData, y);
                chart.SaveChart(trend);
               //regressionBlock = html.GetRegressionBlock(regressionInfo, trend);
            }

            if (isFourierBlock)
            {
                var harmonics = FourierTransform.GetHarmonics(1.0 / (double)((double)1 * xValues.Length), 1, harmonicsCount, yValues);
                throw new NotImplementedException();
                //var yFourier = FourierTransform.GetYFromXValue(harmonics, xValues, yValues);
                //var harmonicsInfo = inform.ShowFourier(iData, harmonics, yFourier);
                //chart.DrawFourier(iData, yFourier);
                //chart.SaveChart(fourier);
                //fourierBlock = html.GetFourierBlock(harmonicsInfo,fourier);
            }

            var body = dataBlock + statisticsBlock + regressionBlock + fourierBlock;
            var report = html.GetDocumentStructure("Отчет", body);
            File.WriteAllText(path+@"/report.html", report);
        }

        private void CreateReport(string path)
        {
            Directory.CreateDirectory(path+@"/resources");
            var sourceFunc = path+@"/resources/sourcefunc.png";
            var iData = (this.ActiveMdiChild as IData);

            var chart = new ChartForm();
            chart.Size = new System.Drawing.Size(1280, 720);
            chart.DrawChart(iData);
            chart.SaveChart(sourceFunc);

            HTMLReportBuilder html = new HTMLReportBuilder();
            var s = html.GetDocumentStructure("Отчет", html.GetDataBlock(iData.Data, sourceFunc));
            File.WriteAllText("report.html", s);
        }

        // Обработка события нажития на пункт меню "Отчет -> Сформировать отчет".
        private void createReportMI_Click(object sender, EventArgs e)
        {

            //CreateReport(Directory.GetCurrentDirectory());
            //new HTMLReportForm().Show(this);
            //BuildReport(Directory.GetCurrentDirectory(), this.MdiChildren.Where(i => i is InformationForm && isIDataEquals(this.ActiveMdiChild as IData, i as IData)).Select(z => z as InformationForm));
        }

        // Обработка события нажития на пункт меню "Справка -> О программе".
        private void aboutMI_Click(object sender, EventArgs e)
        {
            new AboutForm().Show(this);
        }
        #endregion
    }
}
