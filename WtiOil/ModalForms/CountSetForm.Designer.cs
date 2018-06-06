namespace WtiOil
{
    partial class CountSetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountSetForm));
            this.lblText = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbDegree = new System.Windows.Forms.TextBox();
            this.cbShowInformation = new System.Windows.Forms.CheckBox();
            this.cbForecast = new System.Windows.Forms.CheckBox();
            this.numDays = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDays = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(12, 21);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(149, 13);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "Укажите степень полинома";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(182, 146);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Подтвердить";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbDegree
            // 
            this.tbDegree.Location = new System.Drawing.Point(184, 18);
            this.tbDegree.MaxLength = 3;
            this.tbDegree.Name = "tbDegree";
            this.tbDegree.Size = new System.Drawing.Size(47, 20);
            this.tbDegree.TabIndex = 3;
            this.tbDegree.Text = "5";
            this.tbDegree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDegree_KeyPress);
            // 
            // cbShowInformation
            // 
            this.cbShowInformation.AutoSize = true;
            this.cbShowInformation.Checked = true;
            this.cbShowInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInformation.Location = new System.Drawing.Point(15, 98);
            this.cbShowInformation.Name = "cbShowInformation";
            this.cbShowInformation.Size = new System.Drawing.Size(178, 30);
            this.cbShowInformation.TabIndex = 4;
            this.cbShowInformation.Text = "Отобразить коэффициенты и \r\nполученные данные";
            this.cbShowInformation.UseVisualStyleBackColor = true;
            // 
            // cbForecast
            // 
            this.cbForecast.AutoSize = true;
            this.cbForecast.Location = new System.Drawing.Point(15, 58);
            this.cbForecast.Name = "cbForecast";
            this.cbForecast.Size = new System.Drawing.Size(147, 17);
            this.cbForecast.TabIndex = 5;
            this.cbForecast.Text = "Добавить прогноз на ...";
            this.cbForecast.UseVisualStyleBackColor = true;
            this.cbForecast.CheckedChanged += new System.EventHandler(this.cbForecast_CheckedChanged);
            // 
            // numDays
            // 
            this.numDays.Enabled = false;
            this.numDays.Location = new System.Drawing.Point(184, 55);
            this.numDays.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numDays.Name = "numDays";
            this.numDays.ReadOnly = true;
            this.numDays.Size = new System.Drawing.Size(47, 20);
            this.numDays.TabIndex = 6;
            this.numDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numDays.ValueChanged += new System.EventHandler(this.numDays_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(15, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.Enabled = false;
            this.lblDays.Location = new System.Drawing.Point(236, 59);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(31, 13);
            this.lblDays.TabIndex = 8;
            this.lblDays.Text = "дней";
            // 
            // CountSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 181);
            this.Controls.Add(this.lblDays);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.numDays);
            this.Controls.Add(this.cbForecast);
            this.Controls.Add(this.cbShowInformation);
            this.Controls.Add(this.tbDegree);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CountSetForm";
            this.Text = "Полиномиальная регрессия";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbDegree;
        private System.Windows.Forms.CheckBox cbShowInformation;
        private System.Windows.Forms.CheckBox cbForecast;
        private System.Windows.Forms.NumericUpDown numDays;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDays;
    }
}