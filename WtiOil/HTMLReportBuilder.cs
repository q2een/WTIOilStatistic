using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WtiOil
{
    /// <summary>
    /// Предоставляет класс для формирования отчета в формате гипертекстовой разметки.
    /// </summary>
    class HTMLReportBuilder
    {
        private int tablesCount = 0, imagesCount = 0;

        /// <summary>
        /// Возвращает структуру html документа.
        /// </summary>
        /// <param name="title">Заголовок документа</param>
        /// <param name="body">Тело документа</param>
        /// <returns>Структура документа</returns>
        public string GetDocumentStructure(string title, string body)
        {
            return "<!DOCTYPE HTML> <html> <head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><title>" +
                title + "</title>" + GetStyles() + "</head><body><div id=\"wrapper\">" + body + "</div></body></html>";
        }

        /// <summary>
        /// Возвращает набор стилей для данного документа.
        /// </summary>
        /// <returns>Набор стилей для данного документа.</returns>
        private string GetStyles()
        {
            return "<style>body{font-size:16px;background:#f6f6f6;margin:0}#wrapper{width:960px;margin:10px auto;font-family:Verdana,sans-serif;background:#fff;padding:10px}figure.sign{background:#efefef;text-align:center;padding:10px}figure.sign p{margin:0}figure.sign figcaption{height:40px;line-height:50px}table>caption{text-indent:1.5em;text-align:justify;padding:8px 0}table tr{text-align:center;font-size:0.9em}table tr:nth-child(odd){background:#f6f6f6}table tr>th{background:#efeff0;height:20px;padding:10px 3px}table tr:hover{background:#efefef}table td:not(:first-child){border-left:1px solid #fff}table.values tr>td:nth-child(4n+4),table.values tr>th:nth-child(4n+4){width:10px;background:#fff}</style>";
        }

        /// <summary>
        /// Возвращает структуру таблицы.
        /// </summary>
        /// <param name="rows">Размеченные строки таблицы</param>
        /// <param name="caption">Описание таблицы</param>
        /// <param name="styleClass">CSS класс таблицы</param>
        /// <returns></returns>
        private string GetTableStructure(string rows, string caption, string styleClass = "")
        {
            return "<table class=\"" + styleClass + "\" cellspacing=\"0\" width=\"100%\"><caption>" + caption + "</caption>" + rows + "</table>"; ;
        }

        /// <summary>
        /// Возвращает строку таблицы, где стоблы - параметры <c>columnValues</c>.
        /// </summary>
        /// <param name="columnValues">Значение столбцов строки</param>
        /// <returns>Размеченную строку таблицы</returns>
        private string GetTableRow(params string[] columnValues)
        {
            var sb = new StringBuilder("<tr>");

            foreach (var value in columnValues)
                sb.Append("<td>" + value + "</td>");

            sb.Append("</tr>");
            return sb.ToString();
        }

        /// <summary>
        /// Возвращает заголовок уровня <c>headingLevel</c>.
        /// </summary>
        /// <param name="headingLevel">Уровень заголовка (от 1 до 6)</param>
        /// <param name="value">Текст заголовка</param>
        /// <param name="align">Расположение текста заголовка</param>
        /// <returns>Строку - заголовок</returns>
        private string GetHeadings(int headingLevel, string value, string align = "center")
        {
            return String.Format("<h{0} align=\"{1}\">{2}</h{0}>", headingLevel, align, value);
        }

        /// <summary>
        /// Возвращает блок, содержащий картику и поясняющий текст к ней.
        /// </summary>
        /// <param name="path">Путь к картинке</param>
        /// <param name="alt">Поясняющий текст к картинке</param>
        /// <returns>Блок, содержащий картику и поясняющий текст к ней</returns>
        private string GetImage(string path, string alt="")
        {
            return String.Format("<figure class=\"sign\"><p><img src = \"{0}\" width = \"100%\" alt = \"{1}\" ></p>"+
                                "<figcaption>{1}</figcaption></figure> ", path, alt);
        }

        /// <summary>
        /// Возвращает блок, содержащий форматированный блок картинки.
        /// </summary>
        /// <remarks>
        /// Поясняющий текст к картинке содержит подпись "Рисунок" и номер картинки по порядку в данном экземпляре отчета.
        /// </remarks>
        /// <param name="path">Путь к картинке</param>
        /// <param name="alt">Поясняющий текст к картинке</param>
        /// <returns>Блок, содержащий картику и поясняющий текст к ней</returns>
        public string GetImageBlock(string path, string alt = "")
        {
            return String.Format("<figure class=\"sign\"><p><img src = \"{0}\" width = \"100%\" alt = \"{2}\" ></p>" +
                                "<figcaption>Рисунок {1} - {2}</figcaption></figure> ", path,++imagesCount, alt);
        }

        /// <summary>
        /// Возвращает необходимы размеченный блок с данными в зависимости от типа <c>type</c>.
        /// </summary>
        /// <param name="type">Тип содержимой информации</param>
        /// <param name="data">Экземпляр класса, реализующий интерфейс <c>IData</c></param>
        /// <param name="imagePath">Путь к изображению, отображаемому в данном блоке</param>
        /// <returns>Необходимы размеченный блок с данными</returns>
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

        /// <summary>
        /// Возвращает размеченный блок с данными.
        /// </summary>
        /// <param name="header">Заголовок блока</param>
        /// <param name="tableRows">Строки таблицы блока</param>
        /// <param name="tableName">Заголовок таблицы</param>
        /// <param name="imagePath">Путь к изображаению, отображаемому в данном блоке</param>
        /// <param name="alt">Поясняющий текст к изображению</param>
        /// <param name="cssClass">Класс блока</param>
        /// <returns>Размеченный блок с данными</returns>
        public string GetBlock(string header,string tableRows, string tableName, string imagePath, string alt, string cssClass)
        {
            var table = !String.IsNullOrEmpty(tableRows) ? GetTableStructure(tableRows, "Таблица " + (++tablesCount) + " - " + tableName, "values") : "";
            var img = imagePath != null ? GetImageBlock(imagePath,alt) : "";

            return String.Format("<div class=\"data-block {0}\">{1}</div>", cssClass, GetHeadings(2, header) + img + table);
        }

        /// <summary>
        /// Возвращает размеченный блок с данными <c>data</c>.
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <param name="header">Заголовок блока</param>
        /// <param name="tableName">Заголовок таблицы</param>
        /// <param name="imagePath">Путь к изображаению, отображаемому в данном блоке</param>
        /// <param name="alt">Поясняющий текст к изображению</param>
        /// <param name="cssClass">Класс блока</param>
        /// <returns>Размеченный блок с данными</returns>
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

        /// <summary>
        /// Возвращает блок с элементарными статистиками.
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <returns>Блок с элементарными статистиками</returns>
        public string GetStatisticsBlock(IEnumerable<InformationItem> data)
        {
            return GetBlock(data, "Элементарные статистики", "Элементарные статистики", null, null, "statistics");
        }

        /// <summary>
        /// Возвращает блок с результатами полиномиальной регрессии.
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <param name="imagePath">Путь к изображению, отображаемому в данном блоке</param>
        /// <returns>Блок с результатами полиномиальной регрессии</returns>
        public string GetRegressionBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Полиномиальная регрессия", "Данные, полученные при полиномиальной регрессии", imagePath, "Линия тренда (полиномиальная регрессия)", "regression");
        }

        /// <summary>
        /// Возвращает блок с результатами многофакторной регрессии.
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <param name="imagePath">Путь к изображению, отображаемому в данном блоке</param>
        /// <returns>Блок с результатами многофакторной регрессии</returns> 
        public string GetMultipleRegressionBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Многофакторная регрессия (цена на золото, индекс Доу-Джонса)", "Данные, полученные при многофакторной регрессии", imagePath, "Линия тренда (многофакторная регрессия)", "multiple");
        }

        /// <summary>
        /// Возвращает блок с результатами Фурье-анализа.
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <param name="imagePath">Путь к изображению, отображаемому в данном блоке</param>
        /// <returns>Блок с результатами Фурье-анализа</returns>
        public string GetFourierBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Фурье-анализ", "Данные, полученные при Фурье-анализе", imagePath, "Синтезированная функция (Фурье-анализ)", "fourier");
        }

        /// <summary>
        /// Возвращает блок с результатами вейвлет-преобразования.
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <param name="imagePath">Путь к изображению, отображаемому в данном блоке</param>
        /// <returns>Блок с результатами вейвлет-преобразования</returns>
        public string GetWaveletBlock(IEnumerable<InformationItem> data, string imagePath)
        {
            return GetBlock(data, "Вейвлет-анализ", "Данные, полученные при вейвлет-анализе", imagePath, "Синтезированная функция (вейвлет-анализ)", "wavelet");
        }

        /// <summary>
        /// Возвращает блок с исходными данными
        /// </summary>
        /// <param name="data">Коллекция экземпляров класса <c>InformationItem</c></param>
        /// <param name="imagePath">Путь к изображению, отображаемому в данном блоке</param>
        /// <param name="columnsCount">Количество колонок для отображения таблицы</param>
        /// <returns>Блок с исходными данными</returns>
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
