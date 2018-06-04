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
            lblRegression.Enabled = tbDegree.Enabled = cbRegressionBlock.Checked;
        }

        private void cbFourierBlock_CheckedChanged(object sender, EventArgs e)
        {
            lblFourier.Enabled = tbHarmonics.Enabled = cbFourierBlock.Checked;
        }

        private void cbStatistics_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control cont in groupStatistics.Controls)
            {
                if (!(cont is CheckBox) || cont.Equals(sender))
                    continue;

                cont.Enabled = cbStatistics.Checked;
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

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                CheckPath(tbPath.Text);
                int degree = 0, harmonics = 0;

                if (cbRegressionBlock.Checked)
                    degree = Int32.Parse(tbDegree.Text);

                if (cbFourierBlock.Checked)
                    harmonics = Int32.Parse(tbHarmonics.Text);

                (this.Owner as MainMDI).BuildReport(tbPath.Text, cbStatistics.Checked,
                                                     cbAverage.Checked,
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
                                                     cbSum.Checked,
                                                     cbRegressionBlock.Checked,
                                                     degree,
                                                     cbFourierBlock.Checked,
                                                     harmonics);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
