using System;
using System.Linq;
using System.Windows.Forms;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет форму для выбора временного промежутка.
    /// </summary>
    public partial class DateRangePickerForm : Form
    {
        private IData data;

        /// <summary>
        /// Предоставляет форму для выбора временного промежутка.
        /// </summary>
        /// <param name="data">Экземпляр класса, реализующего интерфейс <c>IData</c>, содержащий временной промежуток</param>
        public DateRangePickerForm(IData data)
        {
            InitializeComponent();
            this.data = data;
            tbDateFrom.Text = data.Data[0].Date.Date + "";
            tbDateTo.Text = data.Data.Last().Date.Date + "";
            lblRange.Text = String.Format("c {0:dd/MM/yyyy}\nпо {1:dd/MM/yyyy}", data.FullData[0].Date, data.FullData.Last().Date.Date);
        }

        // Обрабатотка события нажатия на кнопку "Подтвердить".
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var from = DateTime.Parse(tbDateFrom.Text);
                var to = DateTime.Parse(tbDateTo.Text);

                if (from > to)
                    throw new Exception("Конечное значение должно быть больше начального");

                if (from < data.FullData[0].Date || to > data.FullData.Last().Date)
                    throw new Exception(String.Format("Начально значение должно быть не раньше {0}, а конечное не позже чем {1}",
                        data.FullData[0].Date.ToString("MM/dd/yyyy"), data.FullData.Last().Date.ToString("MM/dd/yyyy")));

                Date.SetDateRange(data, from, to);

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
