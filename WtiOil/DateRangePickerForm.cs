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
    public partial class DateRangePickerForm : Form
    {
        private DataForm dataForm;

        public DateRangePickerForm(DataForm dataForm)
        {
            InitializeComponent();
            this.dataForm = dataForm;
            tbDateFrom.Text = dataForm.Data[0].Date.Date + "";
            tbDateTo.Text = dataForm.Data.Last().Date.Date + "";
            lblRange.Text = String.Format("c {0:MM/dd/yyyy}\nпо {1:MM/dd/yyyy}", dataForm.Data[0].Date, dataForm.Data.Last().Date.Date);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var from = DateTime.Parse(tbDateFrom.Text);
                var to = DateTime.Parse(tbDateTo.Text);

                if (from > to)
                    throw new Exception("Конечное значение должно быть больше начального");

                if (from < dataForm.FullData[0].Date || to > dataForm.FullData.Last().Date)
                    throw new Exception(String.Format("Начально значение должно быть не раньше {0}, а конечное не позже чем {1}", 
                        dataForm.FullData[0].Date.ToString("MM/dd/yyyy"), dataForm.FullData.Last().Date.ToString("MM/dd/yyyy")));

                // TODO проверка даты по индексам, чтобы не было ошибок, если пропущена дата.
                var start = dataForm.FullData.IndexOf(dataForm.FullData.First(z => z.Date == from));
                var end = dataForm.FullData.IndexOf(dataForm.FullData.First(z => z.Date == to));

                dataForm.BindingData = new System.ComponentModel.BindingList<ItemWTI>(dataForm.FullData.Skip(start).Take(end - start).ToList());

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
