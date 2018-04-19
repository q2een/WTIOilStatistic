using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WtiOil
{
    public partial class MainMDI : Form
    {
        /// <summary>
        /// Количество дочерних окон типа "DataForm".
        /// </summary>
        private int childFormNumber = 0;

        // Конструктор класса.
        public MainMDI()
        {
            InitializeComponent();
            InitialElementsState();
        }

        /// <summary>
        /// Получает структурированные данные из файла <c>path</c> 
        /// и возвращает эти данные как коллекцию объектов <c>ItemWTI</c>.
        /// </summary>
        /// <param name="path">Путь к *.csv файлу</param>
        /// <returns>Коллекция объектов <c>ItemWTI</c></returns>
        private List<ItemWTI> GetDataFromTextFile(string path)
        {
            var result = new List<ItemWTI>();
            try
            {                
                string fileData = File.ReadAllText(path);

                result = fileData.Split('\n').Where(z => z.Trim() != String.Empty).Select(i => 
                    {
                        var line = i.Split(';');
                        var date = DateTime.Parse(line[0]);
                        var value = Double.Parse(line[1].Replace('.', ','));
                        return new ItemWTI(date, value);
                    }).OrderBy(x=> x.Date).ToList();
            }
            catch (Exception ex)
            {
                result = null;
                throw ex;
            }

            return result;
        }

        // Возвращает имя файла из пути path.
        private string GetFileNameFromPath(string path)
        {
            return path.Split('\\').Last();
        }

        /// <summary>
        /// Отрисовывает линию тренда на графике.
        /// </summary>
        /// <param name="showInformation">Флаг, указывающий необходимо ли показывать результат расчетов.</param>
        /// <param name="coefficients">Коэффициенты полиномиальной регрессии</param>
        /// <param name="yValues">Рассчетные значения У(х)</param>
        public void ShowLineTrend(bool showInformation, double[] coefficients, double[] yValues)
        {
            var data = this.ActiveMdiChild as IData;
            
            if (data == null)
                return;

            var chart = GetForm<ChartForm>();
            chart.DrawTrend(data, yValues);
            chart.Show();
            chart.Activate();

            if (showInformation)
            {
                var inform = GetInformationForm(InformationType.Regression);
                inform.ShowRegression(data, coefficients, yValues);
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
        public void ShowFourier(bool showInformation, List<Harmonic> harmonics, double[] yValues)
        { 
            var data = this.ActiveMdiChild as IData;
            
            if (data == null)
                return;

            var chart = GetForm<ChartForm>();
            chart.DrawFourier(data, yValues);
            chart.Show();
            chart.Activate();

            if (showInformation)
            {
                var inform = GetInformationForm(InformationType.Fourier);
                inform.ShowFourier(data, harmonics, yValues);
                inform.Show();
                inform.Activate();
            }
                        
        }

        #region Состояние элементов управления.
        
        // Начальное состояние элементов управеления.
        public void InitialElementsState()
        {
            SetEditMenuItemsState(false);
            SetDataMenuitems(false, false);
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
        private void SetDataMenuitems(bool isFourier, bool isWavelet)
        {
            fourierMI.Visible = fourierTSB.Visible = isFourier;
            waveletMI.Visible = waveletTSB.Visible = isWavelet;
            dataSeparatorTSB.Visible = isFourier || isWavelet;
        }

        // Состояние элементов меню "График".
        private void SetChartMenuItemsState(bool isDrawChart, bool isLegend, bool isDrawTrend)
        {
            chartMI.Visible = chartSeparatorTSB.Visible = isDrawChart || isDrawTrend;
            drawChartMI.Visible = drawChartTSB.Visible = isDrawChart;
            showLegendMI.Visible = legendSeparatorMI.Visible = isLegend;
            drawTrendLineMI.Visible = drawTrendLineTSB.Visible = isDrawTrend;
        }

        // Состояние элементов меню "Правка".
        private void SetEditMenuItemsState(bool isVisible)
        {
            editMI.Visible = editRowTSB.Visible = removeTSB.Visible = insertTSB.Visible = editSeparatorTS.Visible = isVisible;  
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

        private T GetForm<T>() where T : Form, IData, new()
        {
            var openedFrom = this.MdiChildren.FirstOrDefault(i => i is T) as T;
            var form = openedFrom == null ? new T() : openedFrom;
            form.MdiParent = this;
            return form;
        }

        /// <summary>
        /// Возвращает экземпляр открытой формы InformationForm с типом <c>type</c>.
        /// В случае отстутствия открытой формы создает новый экземпляр и возвращает его.
        /// </summary>
        /// <param name="type">Тип формы InformationForm</param>
        /// <returns>Экземпляр формы InformationForm</returns>
        private InformationForm GetInformationForm(InformationType type)
        {            
            var openedFroms = this.MdiChildren.Where(i => i is InformationForm);

            var form = openedFroms.Where(i => (i as InformationForm).Type == type).FirstOrDefault();

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
                var data = GetDataFromTextFile(FileName);
                var form = GetNewDataForm(FileName, data);
                form.Show();
            }
        }

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

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void showToolBarMI_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = showToolBarMI.Checked;
        }

        private void showStatusBarMI_Click(object sender, EventArgs e)
        {
            statusBar.Visible = showStatusBarMI.Checked;
        }

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

                SetDataMenuitems(true, true);
                SetEditMenuItemsState(state);
                SetChartMenuItemsState(true, !state, true);
                SetStatisticItemsState(state);
                SetReportItemsState(state);
            }

            if (this.ActiveMdiChild is InformationForm)
            {
                SetEditMenuItemsState(false);
                SetReportItemsState(false);

                // Управление видимостью элементов при разных типах окна. 
                switch ((this.ActiveMdiChild as InformationForm).Type)
                {
                    case InformationType.Statistics:
                        SetStatisticItemsState(true);
                        SetChartMenuItemsState(false, false, false);
                        SetDataMenuitems(false, false);
                        break;
                    case InformationType.Regression:
                        SetStatisticItemsState(false);
                        SetChartMenuItemsState(false, false, true);
                        break;
                    case InformationType.Fourier:
                        SetChartMenuItemsState(true, false, false);
                        SetStatisticItemsState(false);
                        SetDataMenuitems(true, false);
                        break;
                    case InformationType.Wavelet:
                        SetChartMenuItemsState(true, false, false);
                        SetStatisticItemsState(false);
                        SetDataMenuitems(false, true);
                        break;
                }
            }
        }

        private void drawChartMI_Click(object sender, EventArgs e)
        {
            if (!(ActiveMdiChild is IData))
                return;

            var chartForm = GetForm<ChartForm>();

            if (ActiveMdiChild is InformationForm)
            {
                if ((ActiveMdiChild as InformationForm).Type == InformationType.Fourier)
                    chartForm.DrawFourier(this.ActiveMdiChild as IData, (ActiveMdiChild as InformationForm).YValues);
            }
            else
                chartForm.DrawChart(this.ActiveMdiChild as IData);

            chartForm.Show();
            chartForm.Activate();
        }

        private void calculateMI_Click(object sender, EventArgs e)
        {
            IData active = (this.ActiveMdiChild as IData);
            var statisticForm = GetInformationForm(InformationType.Statistics);

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

        private void showLegendMI_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
                return;
            if (!(ActiveMdiChild is ChartForm))
                return;

            (ActiveMdiChild as ChartForm).ShowLegend(showLegendMI.Checked);
        }

        private void drawTrendLineMI_Click(object sender, EventArgs e)
        {
            var data = (this.ActiveMdiChild as IData);
            if (this.ActiveMdiChild is InformationForm)
            {
                var form = (this.ActiveMdiChild as InformationForm);
                if (form.Type == InformationType.Regression)
                {
                    var chart = GetForm<ChartForm>();
                    chart.DrawTrend(form, form.YValues);
                    chart.Show();
                    chart.Activate();
                    return;
                }
            }

            var polynimForm = new CountSetForm(data.Data, CountSetType.Regression);
            polynimForm.Show(this);
        }

        private void createReportMI_Click(object sender, EventArgs e)
        {

        }

        private void dateRangeMI_Click(object sender, EventArgs e)
        {
            var data = this.ActiveMdiChild as IData;

            if (data == null)
                return;

            var dateRange = new DateRangePickerForm(data);
            dateRange.Show();
        }

        private void fourierMI_Click(object sender, EventArgs e)
        {
            var polynimForm = new CountSetForm((this.ActiveMdiChild as IData).Data, CountSetType.Fourier);
            polynimForm.Show(this);
        }

        #endregion

    }
}
