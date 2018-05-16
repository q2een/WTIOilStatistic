namespace WtiOil
{
    partial class MultipleRegressionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbFileGold = new System.Windows.Forms.TextBox();
            this.btnOpenGold = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpenDowJones = new System.Windows.Forms.Button();
            this.tbFileDowJones = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbShowGold = new System.Windows.Forms.CheckBox();
            this.cbShowDowJones = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenGold);
            this.groupBox1.Controls.Add(this.tbFileGold);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Стоимость золота";
            // 
            // tbFileGold
            // 
            this.tbFileGold.Location = new System.Drawing.Point(6, 32);
            this.tbFileGold.Name = "tbFileGold";
            this.tbFileGold.ReadOnly = true;
            this.tbFileGold.Size = new System.Drawing.Size(253, 20);
            this.tbFileGold.TabIndex = 1;
            // 
            // btnOpenGold
            // 
            this.btnOpenGold.Location = new System.Drawing.Point(267, 30);
            this.btnOpenGold.Name = "btnOpenGold";
            this.btnOpenGold.Size = new System.Drawing.Size(82, 23);
            this.btnOpenGold.TabIndex = 2;
            this.btnOpenGold.Text = "Обзор";
            this.btnOpenGold.UseVisualStyleBackColor = true;
            this.btnOpenGold.Click += new System.EventHandler(this.btnOpenGold_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenDowJones);
            this.groupBox2.Controls.Add(this.tbFileDowJones);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 70);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Индекс Доу-Джонса";
            // 
            // btnOpenDowJones
            // 
            this.btnOpenDowJones.Location = new System.Drawing.Point(267, 30);
            this.btnOpenDowJones.Name = "btnOpenDowJones";
            this.btnOpenDowJones.Size = new System.Drawing.Size(82, 23);
            this.btnOpenDowJones.TabIndex = 2;
            this.btnOpenDowJones.Text = "Обзор";
            this.btnOpenDowJones.UseVisualStyleBackColor = true;
            this.btnOpenDowJones.Click += new System.EventHandler(this.btnOpenGold_Click);
            // 
            // tbFileDowJones
            // 
            this.tbFileDowJones.Location = new System.Drawing.Point(6, 32);
            this.tbFileDowJones.Name = "tbFileDowJones";
            this.tbFileDowJones.ReadOnly = true;
            this.tbFileDowJones.Size = new System.Drawing.Size(253, 20);
            this.tbFileDowJones.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 42);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите путь к файлам с данными:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbShowDowJones);
            this.groupBox3.Controls.Add(this.cbShowGold);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(352, 84);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры";
            // 
            // cbShowGold
            // 
            this.cbShowGold.AutoSize = true;
            this.cbShowGold.Checked = true;
            this.cbShowGold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowGold.Location = new System.Drawing.Point(15, 31);
            this.cbShowGold.Name = "cbShowGold";
            this.cbShowGold.Size = new System.Drawing.Size(244, 17);
            this.cbShowGold.TabIndex = 0;
            this.cbShowGold.Text = "Отображать стоимость золота на графике";
            this.cbShowGold.UseVisualStyleBackColor = true;
            // 
            // cbShowDowJones
            // 
            this.cbShowDowJones.AutoSize = true;
            this.cbShowDowJones.Checked = true;
            this.cbShowDowJones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowDowJones.Location = new System.Drawing.Point(15, 54);
            this.cbShowDowJones.Name = "cbShowDowJones";
            this.cbShowDowJones.Size = new System.Drawing.Size(255, 17);
            this.cbShowDowJones.TabIndex = 0;
            this.cbShowDowJones.Text = "Отображать индекс Доу-Джонса на графике";
            this.cbShowDowJones.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "csv Files|*.csv";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(267, 272);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Подтвердить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // MultipleRegressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 303);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultipleRegressionForm";
            this.Text = "Многофакторная регрессия";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenGold;
        private System.Windows.Forms.TextBox tbFileGold;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenDowJones;
        private System.Windows.Forms.TextBox tbFileDowJones;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbShowDowJones;
        private System.Windows.Forms.CheckBox cbShowGold;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnOk;
    }
}