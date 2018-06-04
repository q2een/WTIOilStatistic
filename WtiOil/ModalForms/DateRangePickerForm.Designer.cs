namespace WtiOil
{
    partial class DateRangePickerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateRangePickerForm));
            this.tbDateFrom = new System.Windows.Forms.MaskedTextBox();
            this.tbDateTo = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblRange = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbDateFrom
            // 
            this.tbDateFrom.Location = new System.Drawing.Point(134, 36);
            this.tbDateFrom.Mask = "00/00/0000";
            this.tbDateFrom.Name = "tbDateFrom";
            this.tbDateFrom.Size = new System.Drawing.Size(98, 20);
            this.tbDateFrom.TabIndex = 2;
            this.tbDateFrom.ValidatingType = typeof(System.DateTime);
            // 
            // tbDateTo
            // 
            this.tbDateTo.Location = new System.Drawing.Point(134, 62);
            this.tbDateTo.Mask = "00/00/0000";
            this.tbDateTo.Name = "tbDateTo";
            this.tbDateTo.Size = new System.Drawing.Size(98, 20);
            this.tbDateTo.TabIndex = 2;
            this.tbDateTo.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Укажите за какой период учитывать данные";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Конечное значение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Начальное значение";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(147, 98);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(85, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Подтвердить";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(5, 103);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(19, 13);
            this.lblRange.TabIndex = 5;
            this.lblRange.Text = "22";
            // 
            // DateRangePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 133);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDateTo);
            this.Controls.Add(this.tbDateFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateRangePickerForm";
            this.Text = "Выбор периода";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox tbDateFrom;
        private System.Windows.Forms.MaskedTextBox tbDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblRange;
    }
}