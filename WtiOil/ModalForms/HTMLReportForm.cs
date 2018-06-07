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
        public Control ReportPath 
        {
            get
            {
                return this.panelPath;
            }
        }

        public Control Regression
        {
            get
            {
                return this.panelRegression;
            }
        }

        public Control Fourier
        {
            get
            {
                return this.panelFourier;
            }
        }

        public Control MultipleRegression
        {
            get
            {
                return this.panelMultiple;
            }
        }

        private readonly MainMDI main;

        private readonly IData data;

        private ChartForm reportChart = new ChartForm();

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
        
        public HTMLReportForm(MainMDI context, IData data = null)
        {
            InitializeComponent();

            this.data = data;
            this.main = context;
            tbPath.Text = Directory.GetCurrentDirectory();
            folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();
        }

        // Обработка события ввода данных в текстовое поле. Разрешен ввод только цифр.
        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;

            if (!Char.IsDigit(key) && key != 8)
                e.Handled = true;
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void cbRegressionBlock_CheckedChanged(object sender, EventArgs e)
        {
            Regression.Enabled = cbRegressionBlock.Checked;
        }

        private void cbFourierBlock_CheckedChanged(object sender, EventArgs e)
        {
            Fourier.Enabled = cbFourierBlock.Checked;
        }
        
        private void cbMultipleBlock_CheckedChanged(object sender, EventArgs e)
        {
            MultipleRegression.Enabled = cbMultipleBlock.Checked;
        }

        private void cbStatistics_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control cont in groupStatistics.Controls)
            {
                if (!(cont is CheckBox) || cont.Equals(sender))
                    continue;

                cont.Enabled = cbStatisticsBlock.Checked;
            }
        }

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

        internal InformationForm GetMultiple()
        {
            double[] x1, x2;
            DateTime[] dates;

            GetMultipleData(out dates, out x1, out x2);
            var multiple = main.GetMultipleResult(data, x1, x2);
            Date.SetDateRange(data, dates.First(), dates.Last());

            return multiple;
        }

        internal string GetReportPath()
        {
            CheckPath(tbPath.Text);
            return tbPath.Text;
        }

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
        
        private void cbForecast_CheckedChanged(object sender, EventArgs e)
        {
            numRegressionDays.Enabled = lblRegressionForecastDays.Enabled = cbRegressionForecast.Checked;
        }

        private void cbFourierForecast_CheckedChanged(object sender, EventArgs e)
        {
            numFourierDays.Enabled = lblFourierForecastDays.Enabled = cbFourierForecast.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }




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

        private void SaveChartImage(string path, DrawFunc drawFunc, IData data, double[] yPoints, int forecastDaysCount)
        {
            reportChart.RemoveAllSeries();
            reportChart.Size = new System.Drawing.Size(1280, 720);
            drawFunc.Invoke(data, yPoints, forecastDaysCount);
            reportChart.SaveChart(path);
        }

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

    }
}
