using DeloUltimate.Domain.Entities.Base;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DeloUltimate.Eos.AppServices.DataServices
{
    public class DataExportService<TEntity> : IDataExportService<TEntity>
        where TEntity : class, IEntity
    {
        public void ExportToExcel(IEnumerable<TEntity> entityCollection)
        {
            throw new NotImplementedException();
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

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                
                xmlSerializer.Serialize(fileStream, entityArray);
            }
        }
    }
}
