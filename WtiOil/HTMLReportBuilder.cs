using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    class HTMLReportBuilder
    {
        private int tables = 0, images = 0;

        public string GetDocumentStructure(string title, string body)
        {
            return "<!DOCTYPE HTML> <html> <head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><title>" +
                title + "</title>" + GetStyles() + "</head><body><div id=\"wrapper\">" + body + "</div></body></html>";
        }

        private string GetStyles()
        {
            return "<style>body{font-size:16px}table>caption{text-indent:1.5em;text-align:justify} table tr{text-align:center}table tr:nth-child(odd){background:rgba(68,108,179,.37)}table tr>th{background:#fff;border-top:2px solid rgba(68,108,179,1);border-bottom:2px solid rgba(68,108,179,1)}table tr:hover{background:rgba(68,108,179,.67)}table td:not(:first-child){border-left:1px solid #fff}table.values tr>td:nth-child(4n+4),table.values tr>th:nth-child(4n+4){width: 10px;background: #fff;}</style>";
        }

        private string GetTableStructure(string rows, string caption, string styleClass = "")
        {
            return "<table class=\"" + styleClass + "\" cellspacing=\"0\" width=\"100%\"><caption>" + caption + "</caption>" + rows + "</table>"; ;
        }

        private string GetTableRow(params string[] columnValues)
        {
            var sb = new StringBuilder("<tr>");

            foreach (var value in columnValues)
                sb.Append("<td>" + value + "</td>");

            sb.Append("</tr>");
            return sb.ToString();
        }

        private string GetHeadings(int headingLevel, string value, string align = "center")
        {
            return String.Format("<h{0} align=\"{1}\">{2}</h{0}>", headingLevel, align, value);
        }

        private string GetImage(string path, string alt="")
        {
            return String.Format("<figure class=\"sign\"><p><img src = \"{0}\" width = \"100%\" alt = \"{1}\" ></p>"+
                                "<figcaption>{1}</figcaption></figure> ", path, alt);
        }

        public string GetBlockByType(InformationType type, IEnumerable<InformationItem> data, string imagePath)
        {
            switch (type)
            {
                case InformationType.Regression:
                    return GetRegressionBlock(data, imagePath);
                case InformationType.MultipleRegression:
                    return GetMultipleRegressionBlock(data, imagePath);
                case InformationType.Fourier:
                    return GetFourierBlock(data, imagePath);
                case InformationType.Wavelet:
                    return GetWaveletBlock(data, imagePath);
                case InformationType.Statistics:
                    return GetStatisticsBlock(data);
            }

            return "";
        }

        public string GetBlock(string header,string tableRows, string tableName, string imagePath, string alt, string cssClass)
        {
            var table = !String.IsNullOrEmpty(tableRows) ? GetTableStructure(tableRows, "Таблица " + (++tables) + " - " + tableName, "values") : "";
            var img = imagePath != null ? GetImage(imagePath, "Рисунок " + (++images) + " - " + alt) : "";

            return String.Format("<div class=\"data-block {0}\">{1}</div>", cssClass, GetHeadings(2, header) + img + table);
        }

        public string GetBlock(IEnumerable<InformationItem> data, string header, string tableName, string imagePath, string alt, string cssClass)
        {
            if (data == null || data.Count() == 0)
                return "";

            var sb = new StringBuilder();
            sb.Append("<tr><th>Параметр</th><th>Значение</th></tr>");

            foreach (var item in data)
            {
                sb.Append(GetTableRow(item.Parameter, item.Value));
            }

            return GetBlock(header, sb.ToString(), tableName, imagePath, alt, cssClass);
        }

        public string GetStatisticsBlock(IEnumerable<InformationItem> data)
        {
            return GetBlock(data, "Элементарные статистики", "Элементарные статистики", null, null, "statistics");
        }

        public string GetRegressionBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Полиномиальная регрессия", "Данные, полученные при полиномиальной регрессии", imagePath, "Линия тренда (полиномиальная регрессия)", "regression");
        }
              
        public string GetMultipleRegressionBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Многофакторная регрессия (цена на золото, индекс Доу-Джонса)", "Данные, полученные при многофакторной регрессии", imagePath, "Линия тренда (многофакторная регрессия)", "multiple");
        }
          
        public string GetFourierBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Фурье-анализ", "Данные, полученные при Фурье-анализе", imagePath, "Синтезированная функция (Фурье-анализ)", "fourier");
        }

        public string GetWaveletBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Вейвлет-анализ", "Данные, полученные при вейвлет-анализе", imagePath, "Синтезированная функция (вейвлет-анализ)", "wavelet");
        }

        public string GetDataBlock(IEnumerable<ItemWTI> data,string imagePath, int columnsCount = 5)
        {
            int rowscount = (int)Math.Ceiling(data.Count() / (double)columnsCount);

            var th = new StringBuilder("<tr>");

            for (int i = 0; i < columnsCount; i++)
            {
                th.Append("<th>№</th><th>X</th><th>Y</th>");
                
                if (i != columnsCount - 1) 
                    th.Append("<th>  </th>");
            }

            var rows = new StringBuilder(th.ToString() + "</tr>");

            for (int i = 0; i < rowscount; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < columnsCount; j++)
                {
                    int index = i + (j * rowscount);
                    row.Add(index + 1 + "");
                    row.Add(index < data.Count() ? String.Format("{0:dd/MM/yyyy}", data.ElementAt(index).Date) : "");
                    row.Add(index < data.Count() ? String.Format("{0:0.000}", data.ElementAt(index).Value) : "");
                    if (j != columnsCount - 1) row.Add(" ");
                }
                rows.Append(GetTableRow(row.ToArray()));
            }

            return GetBlock("Динамика цен на нефть марки WTI", rows.ToString(), "Исходные данные", imagePath, "Динамика цен на нефть марки WTI", "values");
        }
    }
}
