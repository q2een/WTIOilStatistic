using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет форму для отображения исходных данных. Дата - Цена.
    /// </summary>
    public partial class DataForm : Form, IData
    {
        /// <summary>
        /// Возвращает или задает полную коллекция данных.
        /// </summary>
        public List<ItemWTI> FullData { get; set; }

        private BindingList<ItemWTI> bindingData;
        /// <summary>
        /// Возвращает или задает коллекцию для привязки данных.
        /// </summary>
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

        /// <summary>
        /// Возвращает или задает коллекцию данных, с которой работает пользователь.
        /// </summary>
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

        /// <summary>
        /// Предоставляет форму для отображения исходных данных. Дата - Цена.
        /// </summary>
        /// <param name="data">Коллекция исходных данных</param>
        public DataForm(List<ItemWTI> data = null)
        {
            InitializeComponent();

            this.BindingData = data == null ? new BindingList<ItemWTI>() : new BindingList<ItemWTI>(data);
            this.FullData = data;
        }
    }
}
