using DeloUltimate.Domain.Entities.Base;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeloUltimate.Eos.AppServices.DataServices
{
    public class DataImportService<TEntity> : IDataImportService<TEntity> 
        where TEntity : class, IEntity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public DataImportService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TEntity> ImportFromDatabase() => _repository.GetAll();

        public IEnumerable<TEntity> ImportFromXML(ref string date)
        {
            TEntity[] entityArray;

            XmlSerializer formatter = new XmlSerializer(typeof(TEntity[]));

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "";
                openFileDialog.Filter = $"xml files ({typeof(TEntity).Name}*.xml | {typeof(TEntity).Name}*.xml";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    date = File.GetCreationTime(filePath).ToString("yyyy/MM/dd");
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        entityArray = formatter.Deserialize(fs) as TEntity[];
                        return entityArray;
                    }
                }
                else
                {
                    return new Collection<TEntity>();
                }
            }
        }
    }
}
