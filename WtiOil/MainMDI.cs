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
        private int childFormNumber = 0;

        public MainMDI()
        {
            InitializeComponent();
            InitialElementsState();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            GetNewDataForm("БезНазвания" + ++childFormNumber).Show();
        }

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

        private List<ItemWTI> GetDataFromTextFile(string path)
        {
            var result = new List<ItemWTI>();
            try
            {                
                string fileData = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    fileData = sr.ReadToEnd();
                }

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

        public string GetFileNameFromPath(string path)
        {
            return path.Split('\\').Last();
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

        private void MainMDI_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                InitialElementsState();
                return;
            }
            
            lblFileName.Text = this.ActiveMdiChild.Text;

            if (this.ActiveMdiChild is DataForm)
            {
                SetDataFormElementsState(true);
                return;
            }

            if (this.ActiveMdiChild is ChartForm)
            {
                SetChartFormElementsState(true);
                return;
            }

            if (this.ActiveMdiChild is StatisticForm)
            {
                SetStatisticFormElementsState(true);
                return;
            }
        }

        private void drawChartMI_Click(object sender, EventArgs e)
        {
            if (!(ActiveMdiChild is IData))
                return;

            var chartForm = GetForm <ChartForm>();
            
            chartForm.DrawChart((this.ActiveMdiChild as IData).Data);
            chartForm.Show();
            chartForm.Activate();
        }

        private void calculateMI_Click(object sender, EventArgs e)
        {
            IData active = (this.ActiveMdiChild as IData);
            var statisticForm = GetForm<StatisticForm>();

            statisticForm.ShowStatistic(active.Data, averageMI.Checked, 
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
            var chartForm = GetForm<ChartForm>();

            chartForm.DrawTrend((this.ActiveMdiChild as IData).Data, Byte.Parse(tbDegree.Text));
            chartForm.Show();
            chartForm.Activate();
        }

        #region Состояние элементов управления.
        public void InitialElementsState()
        {
            saveMI.Enabled = false;
            saveAsMI.Enabled = false;
            editMI.Visible = editRowTSB.Visible = removeTSB.Visible = insertTSB.Visible = editSeparatorTS.Visible = false;
            chartMI.Visible = drawChartTSB.Visible = false;
            statisticsMI.Visible = calculateTSB.Visible = false;
            reportMI.Visible = repotTSB.Visible = false;
            windowsMI.Visible = false;
            lblFileName.Text = "";
            dataMI.Visible = false;
        }

        public void SetDataFormElementsState(bool visible)
        {
            saveMI.Enabled = visible;
            saveAsMI.Enabled = visible;
            editMI.Visible = editRowTSB.Visible = removeTSB.Visible = insertTSB.Visible = editSeparatorTS.Visible = visible;
            chartMI.Visible = drawChartTSB.Visible = visible;
            showLegendMI.Visible = false;
            statisticsMI.Visible = calculateTSB.Visible = visible;
            reportMI.Visible = repotTSB.Visible = visible;
            windowsMI.Visible = visible;
            lblFileName.Text = visible ? lblFileName.Text : "";
            dataMI.Visible = true;
            drawTrendLineMI.Visible = visible;
        }

        public void SetChartFormElementsState(bool visible)
        {
            saveMI.Enabled = visible;
            saveAsMI.Enabled = visible;
            editMI.Visible = editRowTSB.Visible = removeTSB.Visible = insertTSB.Visible = editSeparatorTS.Visible = false;
            chartMI.Visible = drawChartTSB.Visible = visible;
            showLegendMI.Visible = visible;
            statisticsMI.Visible = calculateTSB.Visible = false;
            reportMI.Visible = repotTSB.Visible = false;
            windowsMI.Visible = visible;
            lblFileName.Text = visible ? lblFileName.Text : "";
            dataMI.Visible = false;
            drawTrendLineMI.Visible = visible;
        }

        public void SetStatisticFormElementsState(bool visible)
        {
            saveMI.Enabled = visible;
            saveAsMI.Enabled = visible;
            editMI.Visible = editRowTSB.Visible = removeTSB.Visible = insertTSB.Visible = editSeparatorTS.Visible = false;
            chartMI.Visible = drawChartTSB.Visible = false;
            statisticsMI.Visible = calculateTSB.Visible = visible;
            reportMI.Visible = repotTSB.Visible = false;
            windowsMI.Visible = visible;
            lblFileName.Text = visible ? lblFileName.Text : "";
            dataMI.Visible = false; 
            drawTrendLineMI.Visible = false;
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

        #endregion

        private void createReportMI_Click(object sender, EventArgs e)
        {
           
        }

        private void dateRangeMI_Click(object sender, EventArgs e)
        {
            var dataForm = this.ActiveMdiChild as DataForm;

            if (dataForm == null)
                return;

            var dateRange = new DateRangePickerForm(dataForm);
            dateRange.Show();
        }
    }
}
