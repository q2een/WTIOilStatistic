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
            this.btnOK.Location = new System.Drawing.Point(146, 100);
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
            this.cbShowInformation.Location = new System.Drawing.Point(15, 54);
            this.cbShowInformation.Name = "cbShowInformation";
            this.cbShowInformation.Size = new System.Drawing.Size(178, 30);
            this.cbShowInformation.TabIndex = 4;
            this.cbShowInformation.Text = "Отобразить коэффициенты и \r\nполученные данные";
            this.cbShowInformation.UseVisualStyleBackColor = true;
            // 
            // CountSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 135);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbDegree;
        private System.Windows.Forms.CheckBox cbShowInformation;
    }
}