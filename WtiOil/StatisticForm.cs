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
    public partial class StatisticForm : Form, IData
    {
        public List<ItemWTI> Data { get; set; }

        public StatisticForm()
        {
            InitializeComponent();
        }

        private void AddStatisticDataGridRow(string parameter, double? value)
        {
            var row = new DataGridViewRow();
            row.CreateCells(dgvStatistics);
            
            row.Cells[0].Value = parameter;
            row.Cells[1].Value = String.Format("{0:0.000}", value);
            
            dgvStatistics.Rows.Add(row);
        }

        // Вывод элементарных статистик.
        public void ShowStatistic(List<ItemWTI> data, bool isAverage, 
                                                      bool isStandardError, 
                                                      bool isMediana, 
                                                      bool isMode, 
                                                      bool isStandardDeviation, 
                                                      bool isDispersion, 
                                                      bool isSkewness,
                                                      bool isKurtosis,
                                                      bool isInterval,
                                                      bool isMin,
                                                      bool isMax,
                                                      bool isSum)
        {
            if (data == null || data.Count() == 0)
                return;

            this.Data = data;

            dgvStatistics.Rows.Clear();

            if (isAverage)
                AddStatisticDataGridRow("Среднее", data.Average());

            if (isStandardError)
                AddStatisticDataGridRow("Станд. ошибка", data.StandardError());

            if (isMediana)
                AddStatisticDataGridRow("Медиана", data.Median());

            if (isMode)
                AddStatisticDataGridRow("Мода", data.Mode());

            if (isStandardDeviation)
                AddStatisticDataGridRow("Станд. отклонение", data.StandardDeviation());

            if (isDispersion)
                AddStatisticDataGridRow("Дисперсия выборки", data.Dispersion());

            if (isSkewness)
                AddStatisticDataGridRow("Эксцесс", data.Skewness());

            if (isKurtosis)
                AddStatisticDataGridRow("Асимметричность", data.Kurtosis());

            if (isInterval)
                AddStatisticDataGridRow("Интервал", data.Interval());

            if (isMin)
                AddStatisticDataGridRow("Минимум", data.Min());

            if (isMax)
                AddStatisticDataGridRow("Максимум", data.Max());

            if (isSum)
                AddStatisticDataGridRow("Сумма", data.Sum());

                AddStatisticDataGridRow("Счет", data.Count());
        }

    }
}
