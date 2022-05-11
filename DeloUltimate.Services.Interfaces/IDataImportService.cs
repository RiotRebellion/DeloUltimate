using DeloUltimate.Domain.Entities.Base;
using System.Collections.Generic;

namespace DeloUltimate.Services.Interfaces
{
    public interface IDataImportService<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> ImportFromDatabase();

        IEnumerable<TEntity> ImportFromXML(ref string date);
    }
}
