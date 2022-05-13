using DeloUltimate.Domain.Entities.Base;
using System.Collections.Generic;

namespace DeloUltimate.Services.Interfaces
{
    public interface ICompareService<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetCollectionsDifference(IEnumerable<TEntity> prevCollection, IEnumerable<TEntity> nextCollection);
    }
}
