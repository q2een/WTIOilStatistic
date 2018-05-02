namespace WtiOil
{
    partial class HTMLReportForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupStatistics = new System.Windows.Forms.GroupBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.groupRegression = new System.Windows.Forms.GroupBox();
            this.tbDegree = new System.Windows.Forms.TextBox();
            this.cbRegressionBlock = new System.Windows.Forms.CheckBox();
            this.lblRegression = new System.Windows.Forms.Label();
            this.groupFourier = new System.Windows.Forms.GroupBox();
            this.cbFourierBlock = new System.Windows.Forms.CheckBox();
            this.tbHarmonics = new System.Windows.Forms.TextBox();
            this.lblFourier = new System.Windows.Forms.Label();
            this.cbStatistics = new System.Windows.Forms.CheckBox();
            this.cbSum = new System.Windows.Forms.CheckBox();
            this.cbMax = new System.Windows.Forms.CheckBox();
            this.cbMin = new System.Windows.Forms.CheckBox();
            this.cbInterval = new System.Windows.Forms.CheckBox();
            this.cbAverage = new System.Windows.Forms.CheckBox();
            this.cbDispersion = new System.Windows.Forms.CheckBox();
            this.cbStandartError = new System.Windows.Forms.CheckBox();
            this.cbStandardDeviation = new System.Windows.Forms.CheckBox();
            this.cbMedian = new System.Windows.Forms.CheckBox();
            this.cbMode = new System.Windows.Forms.CheckBox();
            this.cbKurtosis = new System.Windows.Forms.CheckBox();
            this.cbSkewness = new System.Windows.Forms.CheckBox();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupStatistics.SuspendLayout();
            this.groupRegression.SuspendLayout();
            this.groupFourier.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путь для сохранения отчета";
            // 
            // groupStatistics
            // 
            this.groupStatistics.Controls.Add(this.cbSkewness);
            this.groupStatistics.Controls.Add(this.cbDispersion);
            this.groupStatistics.Controls.Add(this.cbKurtosis);
            this.groupStatistics.Controls.Add(this.cbMode);
            this.groupStatistics.Controls.Add(this.cbAverage);
            this.groupStatistics.Controls.Add(this.cbMedian);
            this.groupStatistics.Controls.Add(this.cbInterval);
            this.groupStatistics.Controls.Add(this.cbStandardDeviation);
            this.groupStatistics.Controls.Add(this.cbMin);
            this.groupStatistics.Controls.Add(this.cbStandartError);
            this.groupStatistics.Controls.Add(this.cbMax);
            this.groupStatistics.Controls.Add(this.cbSum);
            this.groupStatistics.Controls.Add(this.cbStatistics);
            this.groupStatistics.Location = new System.Drawing.Point(3, 77);
            this.groupStatistics.Name = "groupStatistics";
            this.groupStatistics.Size = new System.Drawing.Size(406, 199);
            this.groupStatistics.TabIndex = 1;
            this.groupStatistics.TabStop = false;
            this.groupStatistics.Text = "Элементарные статистики";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(3, 35);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(325, 20);
            this.tbPath.TabIndex = 2;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(334, 33);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(75, 23);
            this.btnPath.TabIndex = 3;
            this.btnPath.Text = "Обзор ...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // groupRegression
            // 
            this.groupRegression.Controls.Add(this.cbRegressionBlock);
            this.groupRegression.Controls.Add(this.tbDegree);
            this.groupRegression.Controls.Add(this.lblRegression);
            this.groupRegression.Location = new System.Drawing.Point(3, 282);
            this.groupRegression.Name = "groupRegression";
            this.groupRegression.Size = new System.Drawing.Size(406, 93);
            this.groupRegression.TabIndex = 1;
            this.groupRegression.TabStop = false;
            this.groupRegression.Text = "Полиномиальная регрессия";
            // 
            // tbDegree
            // 
            this.tbDegree.Location = new System.Drawing.Point(164, 59);
            this.tbDegree.MaxLength = 3;
            this.tbDegree.Name = "tbDegree";
            this.tbDegree.Size = new System.Drawing.Size(55, 20);
            this.tbDegree.TabIndex = 0;
            this.tbDegree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_KeyPress);
            // 
            // cbRegressionBlock
            // 
            this.cbRegressionBlock.AutoSize = true;
            this.cbRegressionBlock.Checked = true;
            this.cbRegressionBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRegressionBlock.Location = new System.Drawing.Point(12, 28);
            this.cbRegressionBlock.Name = "cbRegressionBlock";
            this.cbRegressionBlock.Size = new System.Drawing.Size(263, 17);
            this.cbRegressionBlock.TabIndex = 1;
            this.cbRegressionBlock.Text = "Добавить в отчет полиномиальную регрессию";
            this.cbRegressionBlock.UseVisualStyleBackColor = true;
            this.cbRegressionBlock.CheckedChanged += new System.EventHandler(this.cbRegressionBlock_CheckedChanged);
            // 
            // lblRegression
            // 
            this.lblRegression.AutoSize = true;
            this.lblRegression.Location = new System.Drawing.Point(9, 62);
            this.lblRegression.Name = "lblRegression";
            this.lblRegression.Size = new System.Drawing.Size(149, 13);
            this.lblRegression.TabIndex = 0;
            this.lblRegression.Text = "Укажите степень полинома";
            // 
            // groupFourier
            // 
            this.groupFourier.Controls.Add(this.cbFourierBlock);
            this.groupFourier.Controls.Add(this.tbHarmonics);
            this.groupFourier.Controls.Add(this.lblFourier);
            this.groupFourier.Location = new System.Drawing.Point(3, 381);
            this.groupFourier.Name = "groupFourier";
            this.groupFourier.Size = new System.Drawing.Size(406, 93);
            this.groupFourier.TabIndex = 1;
            this.groupFourier.TabStop = false;
            this.groupFourier.Text = "Фурье-анализ";
            // 
            // cbFourierBlock
            // 
            this.cbFourierBlock.AutoSize = true;
            this.cbFourierBlock.Checked = true;
            this.cbFourierBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFourierBlock.Location = new System.Drawing.Point(12, 28);
            this.cbFourierBlock.Name = "cbFourierBlock";
            this.cbFourierBlock.Size = new System.Drawing.Size(251, 17);
            this.cbFourierBlock.TabIndex = 1;
            this.cbFourierBlock.Text = "Добавить в отчет результат Фурье-анализа";
            this.cbFourierBlock.UseVisualStyleBackColor = true;
            this.cbFourierBlock.CheckedChanged += new System.EventHandler(this.cbFourierBlock_CheckedChanged);
            // 
            // tbHarmonics
            // 
            this.tbHarmonics.Location = new System.Drawing.Point(180, 59);
            this.tbHarmonics.MaxLength = 3;
            this.tbHarmonics.Name = "tbHarmonics";
            this.tbHarmonics.Size = new System.Drawing.Size(55, 20);
            this.tbHarmonics.TabIndex = 0;
            this.tbHarmonics.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_KeyPress);
            // 
            // lblFourier
            // 
            this.lblFourier.AutoSize = true;
            this.lblFourier.Location = new System.Drawing.Point(9, 62);
            this.lblFourier.Name = "lblFourier";
            this.lblFourier.Size = new System.Drawing.Size(165, 13);
            this.lblFourier.TabIndex = 0;
            this.lblFourier.Text = "Укажите количество гармоник";
            // 
            // cbStatistics
            // 
            this.cbStatistics.AutoSize = true;
            this.cbStatistics.Checked = true;
            this.cbStatistics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStatistics.Location = new System.Drawing.Point(6, 24);
            this.cbStatistics.Name = "cbStatistics";
            this.cbStatistics.Size = new System.Drawing.Size(317, 17);
            this.cbStatistics.TabIndex = 0;
            this.cbStatistics.Text = "Добавить в отчет следующие элементарные статистики:";
            this.cbStatistics.UseVisualStyleBackColor = true;
            this.cbStatistics.CheckedChanged += new System.EventHandler(this.cbStatistics_CheckedChanged);
            // 
            // cbSum
            // 
            this.cbSum.AutoSize = true;
            this.cbSum.Checked = true;
            this.cbSum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSum.Location = new System.Drawing.Point(6, 61);
            this.cbSum.Name = "cbSum";
            this.cbSum.Size = new System.Drawing.Size(60, 17);
            this.cbSum.TabIndex = 0;
            this.cbSum.Text = "Сумма";
            this.cbSum.UseVisualStyleBackColor = true;
            // 
            // cbMax
            // 
            this.cbMax.AutoSize = true;
            this.cbMax.Checked = true;
            this.cbMax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMax.Location = new System.Drawing.Point(6, 84);
            this.cbMax.Name = "cbMax";
            this.cbMax.Size = new System.Drawing.Size(106, 17);
            this.cbMax.TabIndex = 0;
            this.cbMax.Text = "Макс. значение";
            this.cbMax.UseVisualStyleBackColor = true;
            // 
            // cbMin
            // 
            this.cbMin.AutoSize = true;
            this.cbMin.Checked = true;
            this.cbMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMin.Location = new System.Drawing.Point(6, 107);
            this.cbMin.Name = "cbMin";
            this.cbMin.Size = new System.Drawing.Size(100, 17);
            this.cbMin.TabIndex = 0;
            this.cbMin.Text = "Мин. значение";
            this.cbMin.UseVisualStyleBackColor = true;
            // 
            // cbInterval
            // 
            this.cbInterval.AutoSize = true;
            this.cbInterval.Checked = true;
            this.cbInterval.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInterval.Location = new System.Drawing.Point(6, 130);
            this.cbInterval.Name = "cbInterval";
            this.cbInterval.Size = new System.Drawing.Size(75, 17);
            this.cbInterval.TabIndex = 0;
            this.cbInterval.Text = "Интервал";
            this.cbInterval.UseVisualStyleBackColor = true;
            // 
            // cbAverage
            // 
            this.cbAverage.AutoSize = true;
            this.cbAverage.Checked = true;
            this.cbAverage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAverage.Location = new System.Drawing.Point(6, 153);
            this.cbAverage.Name = "cbAverage";
            this.cbAverage.Size = new System.Drawing.Size(119, 17);
            this.cbAverage.TabIndex = 0;
            this.cbAverage.Text = "Среднее значение";
            this.cbAverage.UseVisualStyleBackColor = true;
            // 
            // cbDispersion
            // 
            this.cbDispersion.AutoSize = true;
            this.cbDispersion.Checked = true;
            this.cbDispersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispersion.Location = new System.Drawing.Point(6, 176);
            this.cbDispersion.Name = "cbDispersion";
            this.cbDispersion.Size = new System.Drawing.Size(83, 17);
            this.cbDispersion.TabIndex = 0;
            this.cbDispersion.Text = "Дисперсия";
            this.cbDispersion.UseVisualStyleBackColor = true;
            // 
            // cbStandartError
            // 
            this.cbStandartError.AutoSize = true;
            this.cbStandartError.Checked = true;
            this.cbStandartError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStandartError.Location = new System.Drawing.Point(214, 61);
            this.cbStandartError.Name = "cbStandartError";
            this.cbStandartError.Size = new System.Drawing.Size(132, 17);
            this.cbStandartError.TabIndex = 0;
            this.cbStandartError.Text = "Стандартная ошибка";
            this.cbStandartError.UseVisualStyleBackColor = true;
            // 
            // cbStandardDeviation
            // 
            this.cbStandardDeviation.AutoSize = true;
            this.cbStandardDeviation.Checked = true;
            this.cbStandardDeviation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStandardDeviation.Location = new System.Drawing.Point(214, 84);
            this.cbStandardDeviation.Name = "cbStandardDeviation";
            this.cbStandardDeviation.Size = new System.Drawing.Size(153, 17);
            this.cbStandardDeviation.TabIndex = 0;
            this.cbStandardDeviation.Text = "Стандартное отклонение";
            this.cbStandardDeviation.UseVisualStyleBackColor = true;
            // 
            // cbMedian
            // 
            this.cbMedian.AutoSize = true;
            this.cbMedian.Checked = true;
            this.cbMedian.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMedian.Location = new System.Drawing.Point(214, 107);
            this.cbMedian.Name = "cbMedian";
            this.cbMedian.Size = new System.Drawing.Size(71, 17);
            this.cbMedian.TabIndex = 0;
            this.cbMedian.Text = "Медиана";
            this.cbMedian.UseVisualStyleBackColor = true;
            // 
            // cbMode
            // 
            this.cbMode.AutoSize = true;
            this.cbMode.Checked = true;
            this.cbMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMode.Location = new System.Drawing.Point(214, 130);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(53, 17);
            this.cbMode.TabIndex = 0;
            this.cbMode.Text = "Мода";
            this.cbMode.UseVisualStyleBackColor = true;
            // 
            // cbKurtosis
            // 
            this.cbKurtosis.AutoSize = true;
            this.cbKurtosis.Location = new System.Drawing.Point(214, 153);
            this.cbKurtosis.Name = "cbKurtosis";
            this.cbKurtosis.Size = new System.Drawing.Size(118, 17);
            this.cbKurtosis.TabIndex = 0;
            this.cbKurtosis.Text = "Асимметричность";
            this.cbKurtosis.UseVisualStyleBackColor = true;
            // 
            // cbSkewness
            // 
            this.cbSkewness.AutoSize = true;
            this.cbSkewness.Location = new System.Drawing.Point(214, 176);
            this.cbSkewness.Name = "cbSkewness";
            this.cbSkewness.Size = new System.Drawing.Size(69, 17);
            this.cbSkewness.TabIndex = 0;
            this.cbSkewness.Text = "Эксцесс";
            this.cbSkewness.UseVisualStyleBackColor = true;
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(309, 488);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(100, 23);
            this.btnCreateReport.TabIndex = 4;
            this.btnCreateReport.Text = "Сформировать";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // HTMLReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 523);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.groupFourier);
            this.Controls.Add(this.groupRegression);
            this.Controls.Add(this.groupStatistics);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTMLReportForm";
            this.Text = "Сформировать отчет";
            this.groupStatistics.ResumeLayout(false);
            this.groupStatistics.PerformLayout();
            this.groupRegression.ResumeLayout(false);
            this.groupRegression.PerformLayout();
            this.groupFourier.ResumeLayout(false);
            this.groupFourier.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupStatistics;
        private System.Windows.Forms.CheckBox cbInterval;
        private System.Windows.Forms.CheckBox cbMin;
        private System.Windows.Forms.CheckBox cbMax;
        private System.Windows.Forms.CheckBox cbSum;
        private System.Windows.Forms.CheckBox cbStatistics;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.GroupBox groupRegression;
        private System.Windows.Forms.CheckBox cbRegressionBlock;
        private System.Windows.Forms.TextBox tbDegree;
        private System.Windows.Forms.Label lblRegression;
        private System.Windows.Forms.GroupBox groupFourier;
        private System.Windows.Forms.CheckBox cbFourierBlock;
        private System.Windows.Forms.TextBox tbHarmonics;
        private System.Windows.Forms.Label lblFourier;
        private System.Windows.Forms.CheckBox cbSkewness;
        private System.Windows.Forms.CheckBox cbDispersion;
        private System.Windows.Forms.CheckBox cbKurtosis;
        private System.Windows.Forms.CheckBox cbMode;
        private System.Windows.Forms.CheckBox cbAverage;
        private System.Windows.Forms.CheckBox cbMedian;
        private System.Windows.Forms.CheckBox cbStandardDeviation;
        private System.Windows.Forms.CheckBox cbStandartError;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}