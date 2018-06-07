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
            this.cbSkewness = new System.Windows.Forms.CheckBox();
            this.cbDispersion = new System.Windows.Forms.CheckBox();
            this.cbKurtosis = new System.Windows.Forms.CheckBox();
            this.cbMode = new System.Windows.Forms.CheckBox();
            this.cbAverage = new System.Windows.Forms.CheckBox();
            this.cbMedian = new System.Windows.Forms.CheckBox();
            this.cbInterval = new System.Windows.Forms.CheckBox();
            this.cbStandardDeviation = new System.Windows.Forms.CheckBox();
            this.cbMin = new System.Windows.Forms.CheckBox();
            this.cbStandartError = new System.Windows.Forms.CheckBox();
            this.cbMax = new System.Windows.Forms.CheckBox();
            this.cbSum = new System.Windows.Forms.CheckBox();
            this.cbStatisticsBlock = new System.Windows.Forms.CheckBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.groupRegression = new System.Windows.Forms.GroupBox();
            this.panelRegression = new System.Windows.Forms.Panel();
            this.lblRegressionForecastDays = new System.Windows.Forms.Label();
            this.numRegressionDays = new System.Windows.Forms.NumericUpDown();
            this.lblRegression = new System.Windows.Forms.Label();
            this.cbRegressionForecast = new System.Windows.Forms.CheckBox();
            this.tbDegree = new System.Windows.Forms.TextBox();
            this.cbRegressionBlock = new System.Windows.Forms.CheckBox();
            this.groupFourier = new System.Windows.Forms.GroupBox();
            this.panelFourier = new System.Windows.Forms.Panel();
            this.lblFourierForecastDays = new System.Windows.Forms.Label();
            this.lblFourier = new System.Windows.Forms.Label();
            this.tbHarmonics = new System.Windows.Forms.TextBox();
            this.numFourierDays = new System.Windows.Forms.NumericUpDown();
            this.cbFourierForecast = new System.Windows.Forms.CheckBox();
            this.cbFourierBlock = new System.Windows.Forms.CheckBox();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpenDowJones = new System.Windows.Forms.Button();
            this.tbFileDowJones = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenGold = new System.Windows.Forms.Button();
            this.tbFileGold = new System.Windows.Forms.TextBox();
            this.groupMultiple = new System.Windows.Forms.GroupBox();
            this.panelMultiple = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMultipleBlock = new System.Windows.Forms.CheckBox();
            this.groupWavelet = new System.Windows.Forms.GroupBox();
            this.cbWaveletBlock = new System.Windows.Forms.CheckBox();
            this.panelPath = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupStatistics.SuspendLayout();
            this.groupRegression.SuspendLayout();
            this.panelRegression.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegressionDays)).BeginInit();
            this.groupFourier.SuspendLayout();
            this.panelFourier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFourierDays)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupMultiple.SuspendLayout();
            this.panelMultiple.SuspendLayout();
            this.groupWavelet.SuspendLayout();
            this.panelPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите путь для сохранения отчета";
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
            this.groupStatistics.Controls.Add(this.cbStatisticsBlock);
            this.groupStatistics.Location = new System.Drawing.Point(3, 97);
            this.groupStatistics.Name = "groupStatistics";
            this.groupStatistics.Size = new System.Drawing.Size(365, 201);
            this.groupStatistics.TabIndex = 1;
            this.groupStatistics.TabStop = false;
            this.groupStatistics.Text = "Элементарные статистики";
            // 
            // cbSkewness
            // 
            this.cbSkewness.AutoSize = true;
            this.cbSkewness.Location = new System.Drawing.Point(191, 176);
            this.cbSkewness.Name = "cbSkewness";
            this.cbSkewness.Size = new System.Drawing.Size(69, 17);
            this.cbSkewness.TabIndex = 0;
            this.cbSkewness.Text = "Эксцесс";
            this.cbSkewness.UseVisualStyleBackColor = true;
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
            // cbKurtosis
            // 
            this.cbKurtosis.AutoSize = true;
            this.cbKurtosis.Location = new System.Drawing.Point(191, 153);
            this.cbKurtosis.Name = "cbKurtosis";
            this.cbKurtosis.Size = new System.Drawing.Size(118, 17);
            this.cbKurtosis.TabIndex = 0;
            this.cbKurtosis.Text = "Асимметричность";
            this.cbKurtosis.UseVisualStyleBackColor = true;
            // 
            // cbMode
            // 
            this.cbMode.AutoSize = true;
            this.cbMode.Checked = true;
            this.cbMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMode.Location = new System.Drawing.Point(191, 130);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(53, 17);
            this.cbMode.TabIndex = 0;
            this.cbMode.Text = "Мода";
            this.cbMode.UseVisualStyleBackColor = true;
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
            // cbMedian
            // 
            this.cbMedian.AutoSize = true;
            this.cbMedian.Checked = true;
            this.cbMedian.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMedian.Location = new System.Drawing.Point(191, 107);
            this.cbMedian.Name = "cbMedian";
            this.cbMedian.Size = new System.Drawing.Size(71, 17);
            this.cbMedian.TabIndex = 0;
            this.cbMedian.Text = "Медиана";
            this.cbMedian.UseVisualStyleBackColor = true;
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
            // cbStandardDeviation
            // 
            this.cbStandardDeviation.AutoSize = true;
            this.cbStandardDeviation.Checked = true;
            this.cbStandardDeviation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStandardDeviation.Location = new System.Drawing.Point(191, 84);
            this.cbStandardDeviation.Name = "cbStandardDeviation";
            this.cbStandardDeviation.Size = new System.Drawing.Size(153, 17);
            this.cbStandardDeviation.TabIndex = 0;
            this.cbStandardDeviation.Text = "Стандартное отклонение";
            this.cbStandardDeviation.UseVisualStyleBackColor = true;
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
            // cbStandartError
            // 
            this.cbStandartError.AutoSize = true;
            this.cbStandartError.Checked = true;
            this.cbStandartError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStandartError.Location = new System.Drawing.Point(191, 61);
            this.cbStandartError.Name = "cbStandartError";
            this.cbStandartError.Size = new System.Drawing.Size(132, 17);
            this.cbStandartError.TabIndex = 0;
            this.cbStandartError.Text = "Стандартная ошибка";
            this.cbStandartError.UseVisualStyleBackColor = true;
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
            // cbStatisticsBlock
            // 
            this.cbStatisticsBlock.AutoSize = true;
            this.cbStatisticsBlock.Checked = true;
            this.cbStatisticsBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStatisticsBlock.Location = new System.Drawing.Point(6, 24);
            this.cbStatisticsBlock.Name = "cbStatisticsBlock";
            this.cbStatisticsBlock.Size = new System.Drawing.Size(317, 17);
            this.cbStatisticsBlock.TabIndex = 0;
            this.cbStatisticsBlock.Text = "Добавить в отчет следующие элементарные статистики:";
            this.cbStatisticsBlock.UseVisualStyleBackColor = true;
            this.cbStatisticsBlock.CheckedChanged += new System.EventHandler(this.cbStatistics_CheckedChanged);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(6, 36);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(275, 20);
            this.tbPath.TabIndex = 2;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(287, 34);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(64, 23);
            this.btnPath.TabIndex = 3;
            this.btnPath.Text = "Обзор ...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // groupRegression
            // 
            this.groupRegression.Controls.Add(this.panelRegression);
            this.groupRegression.Controls.Add(this.cbRegressionBlock);
            this.groupRegression.Location = new System.Drawing.Point(3, 304);
            this.groupRegression.Name = "groupRegression";
            this.groupRegression.Size = new System.Drawing.Size(365, 153);
            this.groupRegression.TabIndex = 1;
            this.groupRegression.TabStop = false;
            this.groupRegression.Text = "Полиномиальная регрессия";
            // 
            // panelRegression
            // 
            this.panelRegression.Controls.Add(this.lblRegressionForecastDays);
            this.panelRegression.Controls.Add(this.numRegressionDays);
            this.panelRegression.Controls.Add(this.lblRegression);
            this.panelRegression.Controls.Add(this.cbRegressionForecast);
            this.panelRegression.Controls.Add(this.tbDegree);
            this.panelRegression.Location = new System.Drawing.Point(12, 51);
            this.panelRegression.Name = "panelRegression";
            this.panelRegression.Size = new System.Drawing.Size(290, 92);
            this.panelRegression.TabIndex = 8;
            // 
            // lblRegressionForecastDays
            // 
            this.lblRegressionForecastDays.AutoSize = true;
            this.lblRegressionForecastDays.Enabled = false;
            this.lblRegressionForecastDays.Location = new System.Drawing.Point(231, 54);
            this.lblRegressionForecastDays.Name = "lblRegressionForecastDays";
            this.lblRegressionForecastDays.Size = new System.Drawing.Size(31, 13);
            this.lblRegressionForecastDays.TabIndex = 11;
            this.lblRegressionForecastDays.Text = "дней";
            // 
            // numRegressionDays
            // 
            this.numRegressionDays.Enabled = false;
            this.numRegressionDays.Location = new System.Drawing.Point(179, 50);
            this.numRegressionDays.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRegressionDays.Name = "numRegressionDays";
            this.numRegressionDays.ReadOnly = true;
            this.numRegressionDays.Size = new System.Drawing.Size(47, 20);
            this.numRegressionDays.TabIndex = 10;
            this.numRegressionDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRegressionDays.ValueChanged += new System.EventHandler(this.numDays_ValueChanged);
            // 
            // lblRegression
            // 
            this.lblRegression.AutoSize = true;
            this.lblRegression.Location = new System.Drawing.Point(7, 12);
            this.lblRegression.Name = "lblRegression";
            this.lblRegression.Size = new System.Drawing.Size(149, 13);
            this.lblRegression.TabIndex = 0;
            this.lblRegression.Text = "Укажите степень полинома";
            // 
            // cbRegressionForecast
            // 
            this.cbRegressionForecast.AutoSize = true;
            this.cbRegressionForecast.Location = new System.Drawing.Point(10, 53);
            this.cbRegressionForecast.Name = "cbRegressionForecast";
            this.cbRegressionForecast.Size = new System.Drawing.Size(147, 17);
            this.cbRegressionForecast.TabIndex = 9;
            this.cbRegressionForecast.Text = "Добавить прогноз на ...";
            this.cbRegressionForecast.UseVisualStyleBackColor = true;
            this.cbRegressionForecast.CheckedChanged += new System.EventHandler(this.cbForecast_CheckedChanged);
            // 
            // tbDegree
            // 
            this.tbDegree.Location = new System.Drawing.Point(179, 9);
            this.tbDegree.MaxLength = 3;
            this.tbDegree.Name = "tbDegree";
            this.tbDegree.Size = new System.Drawing.Size(47, 20);
            this.tbDegree.TabIndex = 0;
            this.tbDegree.Text = "6";
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
            // groupFourier
            // 
            this.groupFourier.Controls.Add(this.panelFourier);
            this.groupFourier.Controls.Add(this.cbFourierBlock);
            this.groupFourier.Location = new System.Drawing.Point(374, 304);
            this.groupFourier.Name = "groupFourier";
            this.groupFourier.Size = new System.Drawing.Size(365, 153);
            this.groupFourier.TabIndex = 1;
            this.groupFourier.TabStop = false;
            this.groupFourier.Text = "Фурье-анализ";
            // 
            // panelFourier
            // 
            this.panelFourier.Controls.Add(this.lblFourierForecastDays);
            this.panelFourier.Controls.Add(this.lblFourier);
            this.panelFourier.Controls.Add(this.tbHarmonics);
            this.panelFourier.Controls.Add(this.numFourierDays);
            this.panelFourier.Controls.Add(this.cbFourierForecast);
            this.panelFourier.Location = new System.Drawing.Point(12, 51);
            this.panelFourier.Name = "panelFourier";
            this.panelFourier.Size = new System.Drawing.Size(290, 92);
            this.panelFourier.TabIndex = 9;
            // 
            // lblFourierForecastDays
            // 
            this.lblFourierForecastDays.AutoSize = true;
            this.lblFourierForecastDays.Enabled = false;
            this.lblFourierForecastDays.Location = new System.Drawing.Point(235, 55);
            this.lblFourierForecastDays.Name = "lblFourierForecastDays";
            this.lblFourierForecastDays.Size = new System.Drawing.Size(31, 13);
            this.lblFourierForecastDays.TabIndex = 11;
            this.lblFourierForecastDays.Text = "дней";
            // 
            // lblFourier
            // 
            this.lblFourier.AutoSize = true;
            this.lblFourier.Location = new System.Drawing.Point(12, 18);
            this.lblFourier.Name = "lblFourier";
            this.lblFourier.Size = new System.Drawing.Size(165, 13);
            this.lblFourier.TabIndex = 0;
            this.lblFourier.Text = "Укажите количество гармоник";
            // 
            // tbHarmonics
            // 
            this.tbHarmonics.Location = new System.Drawing.Point(183, 15);
            this.tbHarmonics.MaxLength = 3;
            this.tbHarmonics.Name = "tbHarmonics";
            this.tbHarmonics.Size = new System.Drawing.Size(47, 20);
            this.tbHarmonics.TabIndex = 0;
            this.tbHarmonics.Text = "6";
            this.tbHarmonics.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_KeyPress);
            // 
            // numFourierDays
            // 
            this.numFourierDays.Enabled = false;
            this.numFourierDays.Location = new System.Drawing.Point(183, 51);
            this.numFourierDays.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numFourierDays.Name = "numFourierDays";
            this.numFourierDays.ReadOnly = true;
            this.numFourierDays.Size = new System.Drawing.Size(47, 20);
            this.numFourierDays.TabIndex = 10;
            this.numFourierDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numFourierDays.ValueChanged += new System.EventHandler(this.numDays_ValueChanged);
            // 
            // cbFourierForecast
            // 
            this.cbFourierForecast.AutoSize = true;
            this.cbFourierForecast.Location = new System.Drawing.Point(14, 54);
            this.cbFourierForecast.Name = "cbFourierForecast";
            this.cbFourierForecast.Size = new System.Drawing.Size(147, 17);
            this.cbFourierForecast.TabIndex = 9;
            this.cbFourierForecast.Text = "Добавить прогноз на ...";
            this.cbFourierForecast.UseVisualStyleBackColor = true;
            this.cbFourierForecast.CheckedChanged += new System.EventHandler(this.cbFourierForecast_CheckedChanged);
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
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(639, 463);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(100, 23);
            this.btnCreateReport.TabIndex = 4;
            this.btnCreateReport.Text = "Сформировать";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenDowJones);
            this.groupBox2.Controls.Add(this.tbFileDowJones);
            this.groupBox2.Location = new System.Drawing.Point(3, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 70);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Индекс Доу-Джонса";
            // 
            // btnOpenDowJones
            // 
            this.btnOpenDowJones.Location = new System.Drawing.Point(239, 30);
            this.btnOpenDowJones.Name = "btnOpenDowJones";
            this.btnOpenDowJones.Size = new System.Drawing.Size(82, 23);
            this.btnOpenDowJones.TabIndex = 2;
            this.btnOpenDowJones.Text = "Обзор";
            this.btnOpenDowJones.UseVisualStyleBackColor = true;
            this.btnOpenDowJones.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbFileDowJones
            // 
            this.tbFileDowJones.Location = new System.Drawing.Point(6, 32);
            this.tbFileDowJones.Name = "tbFileDowJones";
            this.tbFileDowJones.ReadOnly = true;
            this.tbFileDowJones.Size = new System.Drawing.Size(227, 20);
            this.tbFileDowJones.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenGold);
            this.groupBox1.Controls.Add(this.tbFileGold);
            this.groupBox1.Location = new System.Drawing.Point(3, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Стоимость золота";
            // 
            // btnOpenGold
            // 
            this.btnOpenGold.Location = new System.Drawing.Point(241, 30);
            this.btnOpenGold.Name = "btnOpenGold";
            this.btnOpenGold.Size = new System.Drawing.Size(82, 23);
            this.btnOpenGold.TabIndex = 2;
            this.btnOpenGold.Text = "Обзор";
            this.btnOpenGold.UseVisualStyleBackColor = true;
            this.btnOpenGold.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbFileGold
            // 
            this.tbFileGold.Location = new System.Drawing.Point(6, 32);
            this.tbFileGold.Name = "tbFileGold";
            this.tbFileGold.ReadOnly = true;
            this.tbFileGold.Size = new System.Drawing.Size(227, 20);
            this.tbFileGold.TabIndex = 1;
            // 
            // groupMultiple
            // 
            this.groupMultiple.Controls.Add(this.panelMultiple);
            this.groupMultiple.Controls.Add(this.cbMultipleBlock);
            this.groupMultiple.Location = new System.Drawing.Point(374, 12);
            this.groupMultiple.Name = "groupMultiple";
            this.groupMultiple.Size = new System.Drawing.Size(365, 226);
            this.groupMultiple.TabIndex = 6;
            this.groupMultiple.TabStop = false;
            this.groupMultiple.Text = "Многофакторная регрессия";
            // 
            // panelMultiple
            // 
            this.panelMultiple.Controls.Add(this.groupBox2);
            this.panelMultiple.Controls.Add(this.groupBox1);
            this.panelMultiple.Controls.Add(this.label2);
            this.panelMultiple.Enabled = false;
            this.panelMultiple.Location = new System.Drawing.Point(6, 43);
            this.panelMultiple.Name = "panelMultiple";
            this.panelMultiple.Size = new System.Drawing.Size(353, 189);
            this.panelMultiple.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Укажите путь к файлам с данными:";
            // 
            // cbMultipleBlock
            // 
            this.cbMultipleBlock.AutoSize = true;
            this.cbMultipleBlock.Location = new System.Drawing.Point(6, 24);
            this.cbMultipleBlock.Name = "cbMultipleBlock";
            this.cbMultipleBlock.Size = new System.Drawing.Size(266, 17);
            this.cbMultipleBlock.TabIndex = 0;
            this.cbMultipleBlock.Text = "Добавить в отчет многофакторную регрессию:";
            this.cbMultipleBlock.UseVisualStyleBackColor = true;
            this.cbMultipleBlock.CheckedChanged += new System.EventHandler(this.cbMultipleBlock_CheckedChanged);
            // 
            // groupWavelet
            // 
            this.groupWavelet.Controls.Add(this.cbWaveletBlock);
            this.groupWavelet.Location = new System.Drawing.Point(374, 244);
            this.groupWavelet.Name = "groupWavelet";
            this.groupWavelet.Size = new System.Drawing.Size(365, 54);
            this.groupWavelet.TabIndex = 1;
            this.groupWavelet.TabStop = false;
            this.groupWavelet.Text = "Вейвлет-анализ";
            // 
            // cbWaveletBlock
            // 
            this.cbWaveletBlock.AutoSize = true;
            this.cbWaveletBlock.Checked = true;
            this.cbWaveletBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWaveletBlock.Location = new System.Drawing.Point(12, 28);
            this.cbWaveletBlock.Name = "cbWaveletBlock";
            this.cbWaveletBlock.Size = new System.Drawing.Size(258, 17);
            this.cbWaveletBlock.TabIndex = 1;
            this.cbWaveletBlock.Text = "Добавить в отчет результат вейвлет-анализа";
            this.cbWaveletBlock.UseVisualStyleBackColor = true;
            this.cbWaveletBlock.CheckedChanged += new System.EventHandler(this.cbFourierBlock_CheckedChanged);
            // 
            // panelPath
            // 
            this.panelPath.Controls.Add(this.tbPath);
            this.panelPath.Controls.Add(this.label1);
            this.panelPath.Controls.Add(this.btnPath);
            this.panelPath.Location = new System.Drawing.Point(3, 12);
            this.panelPath.Name = "panelPath";
            this.panelPath.Size = new System.Drawing.Size(365, 79);
            this.panelPath.TabIndex = 7;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "csv Files|*.csv";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(533, 463);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // HTMLReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 492);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelPath);
            this.Controls.Add(this.groupMultiple);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.groupWavelet);
            this.Controls.Add(this.groupFourier);
            this.Controls.Add(this.groupRegression);
            this.Controls.Add(this.groupStatistics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTMLReportForm";
            this.Text = "Сформировать отчет";
            this.groupStatistics.ResumeLayout(false);
            this.groupStatistics.PerformLayout();
            this.groupRegression.ResumeLayout(false);
            this.groupRegression.PerformLayout();
            this.panelRegression.ResumeLayout(false);
            this.panelRegression.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegressionDays)).EndInit();
            this.groupFourier.ResumeLayout(false);
            this.groupFourier.PerformLayout();
            this.panelFourier.ResumeLayout(false);
            this.panelFourier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFourierDays)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupMultiple.ResumeLayout(false);
            this.groupMultiple.PerformLayout();
            this.panelMultiple.ResumeLayout(false);
            this.panelMultiple.PerformLayout();
            this.groupWavelet.ResumeLayout(false);
            this.groupWavelet.PerformLayout();
            this.panelPath.ResumeLayout(false);
            this.panelPath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupStatistics;
        private System.Windows.Forms.CheckBox cbInterval;
        private System.Windows.Forms.CheckBox cbMin;
        private System.Windows.Forms.CheckBox cbMax;
        private System.Windows.Forms.CheckBox cbSum;
        private System.Windows.Forms.CheckBox cbStatisticsBlock;
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenDowJones;
        private System.Windows.Forms.TextBox tbFileDowJones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenGold;
        private System.Windows.Forms.TextBox tbFileGold;
        private System.Windows.Forms.GroupBox groupMultiple;
        private System.Windows.Forms.CheckBox cbMultipleBlock;
        private System.Windows.Forms.GroupBox groupWavelet;
        private System.Windows.Forms.CheckBox cbWaveletBlock;
        private System.Windows.Forms.Label lblRegressionForecastDays;
        private System.Windows.Forms.NumericUpDown numRegressionDays;
        private System.Windows.Forms.CheckBox cbRegressionForecast;
        private System.Windows.Forms.Label lblFourierForecastDays;
        private System.Windows.Forms.NumericUpDown numFourierDays;
        private System.Windows.Forms.CheckBox cbFourierForecast;
        private System.Windows.Forms.Panel panelRegression;
        private System.Windows.Forms.Panel panelFourier;
        private System.Windows.Forms.Panel panelMultiple;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnCancel;
    }
}