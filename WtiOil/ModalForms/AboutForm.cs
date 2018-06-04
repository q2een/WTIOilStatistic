using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WtiOil
{
    partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.labelProductName.Text = "Анализ и прогнозирование цен на нефть марки WTI";
            this.labelVersion.Text = String.Format("Версия {0}", AssemblyVersion);
            this.labelCopyright.Text = "Copyright (С) Qzeen";
            this.labelCompanyName.Text = "Разработал: Кузин Е.С.";
            this.textBoxDescription.Text = "Курсовой проект по дисциплине СМОД на тему: \"Разработка программы для анализа данных и прогнозирования цен на нефть марки WTI\"";
        }

        #region Методы доступа к атрибутам сборки
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        #endregion
    }
}
