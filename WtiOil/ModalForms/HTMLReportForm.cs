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

        public void GetMultipleData(out double[] x1, out double[] x2)
        {
            x1 = new double[] { };
            x2 = new double[] { };

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
                MessageBox.Show("Исходные данные не содержат данный временной интервал.\nУкажите другие данные.", "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
        
        public HTMLReportForm()
        {
            InitializeComponent();

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
            MultipleRegression.Enabled = cbMultipleBlock.Enabled;
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

        private InformationForm GetRegression()
        {
            byte degree = Byte.Parse(tbDegree.Text);
            int forecastDays = cbRegressionForecast.Checked ? Int32.Parse(numRegressionDays.Value + "") : 0;

            return main.GetRegressionResult(data, degree, forecastDays);
        }

        private InformationForm GetFourier()
        {
            byte harmonics = Byte.Parse(tbHarmonics.Text);
            int forecastDays = cbFourierForecast.Checked ? Int32.Parse(numFourierDays.Value + "") : 0;

            return main.GetFourierResult(data, harmonics, forecastDays);
        }

        private InformationForm GetMultiple()
        {
            double[] x1, x2;

            GetMultipleData(out x1, out x2);
            return main.GetMultipleResult(data, x1, x2);
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                CheckPath(tbPath.Text);
                byte harmonics = 0;

                if (cbFourierBlock.Checked)
                    harmonics = Byte.Parse(tbHarmonics.Text);

                List<InformationForm> forms = new List<InformationForm>();

                if (cbStatisticsBlock.Checked)

                if (cbRegressionBlock.Checked)
                    forms.Add(GetRegression());

                if (cbFourierBlock.Checked)
                    forms.Add(GetFourier());

                if (cbMultipleBlock.Checked)
                    forms.Add(GetMultiple());

                if (cbWaveletBlock.Checked)
                    forms.Add(main.GetWaveletResult(data));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
    }
}
