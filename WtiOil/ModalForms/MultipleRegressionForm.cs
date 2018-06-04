using System;
using System.Linq;
using System.Windows.Forms;

namespace WtiOil
{
    public partial class MultipleRegressionForm : Form
    {
        /// <summary>
        /// Экземпляр класса, реализующего интерфейс IData.
        /// </summary>
        private readonly IData data;

        /// <summary>
        /// Форма для работы с многофакторной регрессией.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс IData.</param>
        public MultipleRegressionForm(IData data)
        {
            InitializeComponent();
            this.data = data;
        }

        // Обработка события нажатия на кнопку "Обзор".
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
        
        // Обработка события нажатия на кнопку "Ок".
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbFileDowJones.Text == String.Empty || tbFileGold.Text == String.Empty)
            {
                MessageBox.Show("Укажите путь к файлам", "Ошибка");
                return;
            }

            var main = this.Owner as MainMDI;

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
                double[] x1 = DowJones.Select(i => i.Value).ToArray();
                double[] x2 = gold.Select(i => i.Value).ToArray();

                var coeffs = Regression.GetMultipleRegressionCoefficients(yValues, x1, x2);
                var newY = Regression.GetMultipleYFromXValue(coeffs, x1, x2);

                main.ShowMultiple(newY, coeffs);
                this.Close();
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
    }
}
