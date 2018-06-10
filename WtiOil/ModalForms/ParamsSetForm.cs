using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет модальное окно для указания дополнительных данных, необходимых при расчетах.
    /// </summary>
    public partial class ParamsSetForm : Form
    {
        /// <summary>
        /// Тип отображаемого модального окна.
        /// </summary>
        public WindowType Type { get; private set; }

        /// <summary>
        /// Экземлпяр класса, реализующего интерфейс <c>IData</c>
        /// </summary>
        public IData Data { get; private set; }

        // Экземпляр класса главного MDI окна.
        private readonly MainMDI main;

        // Экземпляр класса HTMLReportForm для формирования HTML отчета.
        private readonly HTMLReportForm html;

        /// <summary>
        /// Предоставляет модальное окно в зависимости от заданного типа <c>type</c>.
        /// </summary>
        /// <param name="context">Экземпляр класса главного MDI окна.</param>
        /// <param name="type">Тип модального окна</param>
        /// <param name="data">Экземлпяр класса, реализующего интерфейс <c>IData</c></param>
        public ParamsSetForm(MainMDI context, WindowType type, IData data)
        {
            InitializeComponent();

            this.Type = type;
            this.main = context;
            this.Data = data;

            Control control = null;
            html = new HTMLReportForm(main, data);

            switch (type)
            {
                case WindowType.File:
                    control = html.ReportPath;
                    this.Text = "Создать отчет...";
                    break;
                case WindowType.Regression:
                    control = html.Regression;
                    this.Text = "Полиномиальная регрессия";
                    break;
                case WindowType.Fourier:
                    control = html.Fourier;
                    this.Text = "Фурье-анализ";
                    break;
                case WindowType.MultipleRegression:
                    control = html.MultipleRegression;
                    this.Text = "Многофакторная регрессия";
                    break;
            }

            // Управление отображением элемента на форме.
            this.Size = new Size(control.Size.Width + 10, control.Size.Height + 80);
            control.Enabled = true;
            control.Dock = DockStyle.Fill;
            this.Controls["container"].Controls.Add(control);
        }

        // "Отмена". Обработка события нажатия на кнопку.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // "Подтвердить". Обработка события нажатия на кнопку.
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Type)
                {
                    case WindowType.File:
                        var openedFroms = main.MdiChildren.Where(i => i is InformationForm && main.isIDataEquals(i as InformationForm, Data));
                        var validForms = openedFroms.Select(i => i as InformationForm).ToList();
                        html.BuildReport(html.GetReportPath(), validForms);
                        break;
                    case WindowType.Regression:
                        main.ShowInformationForm(html.GetRegression());
                        break;
                    case WindowType.Fourier:
                        main.ShowInformationForm(html.GetFourier());
                        break;
                    case WindowType.MultipleRegression:
                        main.ShowInformationForm(html.GetMultiple());
                        break;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }
    }

    /// <summary>
    /// Тип отображаемого модального окна.
    /// </summary>
    public enum WindowType
    { 
        /// <summary>
        /// Выбор папки для сохранения отчета.
        /// </summary>
        File,
        /// <summary>
        /// Данные для Фурье-анализа.
        /// </summary>
        Fourier,
        /// <summary>
        /// Данные для многофакторной регрессии.
        /// </summary>
        MultipleRegression,
        /// <summary>
        /// Данные для полиномиальной регрессии.
        /// </summary>
        Regression
    }
}
