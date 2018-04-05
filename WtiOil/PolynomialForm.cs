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
    public partial class PolynomialForm : Form
    {
        public PolynomialForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //(this.Owner as MainMDI).deg = Int32.Parse(tbDegree.Text);
            this.Close();
        }
    }
}
