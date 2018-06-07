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
        HTMLReportForm html = new HTMLReportForm();

        public ParamsSetForm()
        {
            var control = html.Controls["groupRegression"];
            this.Controls.Add(control);
            control.Dock = DockStyle.Fill;

        }
    }
}
