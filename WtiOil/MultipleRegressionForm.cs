using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WtiOil
{
    public partial class MultipleRegressionForm : Form
    {
        private readonly IData data;

        public MultipleRegressionForm(IData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void btnOpenGold_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((sender as Button).Name == "btnOpenGold")
                    tbFileGold.Text = openFileDialog.FileName;
                else
                    tbFileDowJones.Text = openFileDialog.FileName;
            }
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbFileDowJones.Text == String.Empty || tbFileGold.Text == String.Empty)
            {
                MessageBox.Show("Укажите путь к файлам", "Ошибка");
                return;
            }
            var main = this.Owner as MainMDI;

            /*TODO: проверка на ошибки в несоответствии даты*/
            var gold = main.GetDataFromTextFile(tbFileGold.Text);
            var DowJones = main.GetDataFromTextFile(tbFileDowJones.Text);

            int min = gold.Count >= DowJones.Count ? DowJones.Count : gold.Count;
            var coll = gold.Count == min ? gold : DowJones;

            var start = data.FullData.IndexOf(data.FullData.First(z => z.Date == coll[0].Date));
            var end = data.FullData.IndexOf(data.FullData.First(z => z.Date == coll[min-1].Date));

            data.Data = data.FullData.Skip(start).Take(end + 1 - start).ToList();


            double[] yValues = data.Data.Select(i => i.Value).ToArray();
            double[] x1 = DowJones.Select(i => i.Value).ToArray();
            double[] x2 = gold.Select(i=> i.Value).ToArray();
            

            var coeffs = Regression.GetMultipleRegressionCoefficients(yValues, x1, x2);
            var newY = Regression.GetMultipleYFromXValue(coeffs, x1, x2);

            main.ShowMultiple(newY, null, null);
            this.Close();
        }
    }
}
