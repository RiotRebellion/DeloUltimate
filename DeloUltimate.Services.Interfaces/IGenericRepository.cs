using DeloUltimate.Domain.Entities.Base;
using System.Collections.Generic;

namespace DeloUltimate.Services.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
    }
}
