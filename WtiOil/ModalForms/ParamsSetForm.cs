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

        HTMLReportForm html = new HTMLReportForm();

        public ParamsSetForm(WindowType type)
        {
            InitializeComponent();
            this.Type = type;
            Control control = null;

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

            this.Size = new Size(control.Size.Width, control.Size.Height + 50);
            control.Dock = DockStyle.Fill;
            this.Controls["container"].Controls.Add(control);
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
