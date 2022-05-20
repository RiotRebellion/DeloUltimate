using DeloUltimate.Domain.Entities.Attributes;
using DeloUltimate.Domain.Entities.Base;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

namespace DeloUltimate.Eos.AppServices.DataServices
{
    public class DataExportService<TEntity> : IDataExportService<TEntity>
        where TEntity : class, IEntity
    {
        public void ExportToExcel(IEnumerable<TEntity> entityCollection)
        {
            //Проверяем наличие атрибута RussianName
            if (typeof(TEntity).IsDefined(typeof(RussianNameAttribute)) == false)
            {
                MessageBox.Show($"Класс {typeof(TEntity).Name} не имеет русских названий /n");
                return;
            };

            RussianNameAttribute attribute = (RussianNameAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(RussianNameAttribute));


            Excel.Application excelApp = new Excel.Application();
            excelApp.DisplayAlerts = false;
            excelApp.ScreenUpdating = false;
            excelApp.Visible = false;
            excelApp.UserControl = false;
            excelApp.Interactive = false;

            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel._Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            int maxColumn = 1;
            int column = 1;
            int row = 1;

            // Вывод класса данных
            worksheet.Cells[row, column] = $"{attribute.Name} - {DateTime.Now.ToString("G")}";

            row += 1;
            
            //забиваем названия колонок
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                RussianNameAttribute attr = (RussianNameAttribute)property.GetCustomAttribute(typeof(RussianNameAttribute));
                worksheet.Cells[row, column] = attr.Name;
                ((Excel.Range)worksheet.Cells[row - 1, column]).VerticalAlignment = 1;
                column += 1;
            }
            maxColumn = column;

            // Форматирование заголовка
            ((Excel.Range)worksheet.Cells[row - 1, column]).VerticalAlignment = 1;
            ((Excel.Range)worksheet.Rows[row]).RowHeight = 45;
            Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, column]];
            range.Merge();
            range.Font.Size = 18;
            range.Font.Name = "Tahoma";
            range.VerticalAlignment = 1;
            range.HorizontalAlignment = 1;

            // Форматирование заголовков столбов
            range = worksheet.Range[worksheet.Cells[row, 1], worksheet.Cells[row, column]];
            range.Font.Size = 16;
            range.Font.Name = "Tahoma";
            range.VerticalAlignment = 1;
            range.HorizontalAlignment = 1;

            column = 1;
            row += 1;

            //Выгрузка данных из коллекции
            foreach (var entity in entityCollection)
            {
                foreach (var property in properties)
                {
                    worksheet.Cells[row, column] = property.GetValue(entity);
                    column += 1;
                }
                column = 1;
                row += 1;
            }

            for (int i = 1; i <= maxColumn; i++)
                ((Excel.Range)worksheet.Columns[i]).AutoFit();

            //Сохраняем отчет
            string fileName = $"{attribute.Name}_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            workbook.Saved = true;
            workbook.SaveAs(fileName);
            workbook.Close(false);
            excelApp.Quit();

            MessageBox.Show($"Следующий отчет сохранен на рабочий стол: {fileName}");
        }

        public void ExportToXML(IEnumerable<TEntity> entityCollection)
        {
            var currentDate = DateTime.Now.ToString("yyyy_MM_dd");

            TEntity[] entityArray = entityCollection.ToArray();
            var type = entityArray.GetType();
            var entityType = type.GetElementType();
            var xmlSerializer = new XmlSerializer(type);

            if (Directory.Exists("data/") == false)
                Directory.CreateDirectory("data/");

            var filePath = $"data/{entityType.Name}s_{currentDate}.xml";
            if (File.Exists(filePath)) return;

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {                
                xmlSerializer.Serialize(fileStream, entityArray);
            }
        }
    }
}
