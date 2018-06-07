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
    public partial class ParamsSetForm : Form
    {
        public WindowType Type { get; private set; }
        public IData Data { get; private set; }
        private MainMDI main;

        HTMLReportForm html;

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
                    this.Text = "Сформировать отчет...";
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

            this.Size = new Size(control.Size.Width, control.Size.Height + 80);
            control.Dock = DockStyle.Fill;
            this.Controls["container"].Controls.Add(control);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
                        main.ShowLineTrend(html.GetRegression());
                        break;
                    case WindowType.Fourier:
                        main.ShowFourier(html.GetFourier());
                        break;
                    case WindowType.MultipleRegression:
                        main.ShowMultiple(html.GetMultiple());
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

    public enum WindowType
    { 
        File,
        Fourier,
        MultipleRegression,
        Regression
    }
}
