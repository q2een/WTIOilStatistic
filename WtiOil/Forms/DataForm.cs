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
    public partial class DataForm : Form, IData
    {
        // Полная коллекция данных.
        public List<ItemWTI> FullData { get; set; }

        // Коллекция для привязки данных.
        private BindingList<ItemWTI> bindingData;
        public BindingList<ItemWTI> BindingData
        {
            get
            {
                return bindingData;
            }
            set
            {
                bindingData = value;
                dgvData.DataSource = this.BindingData;
            }
        }

        // Реализация IData.
        public List<ItemWTI> Data
        {
            get
            {
                return BindingData.ToList();
            }
            set
            {
                BindingData = new BindingList<ItemWTI>(value);
            }
        }

        public void AddRow()
        {
            if (Data.Count < 1)
                return;
            Data.Add(new ItemWTI(Data.Last().Date.AddDays(1), 0));   
        }

        public DataForm(List<ItemWTI> data = null)
        {
            InitializeComponent();

            this.BindingData = data == null ? new BindingList<ItemWTI>() : new BindingList<ItemWTI>(data);
            this.FullData = data;
        }
    }
}
