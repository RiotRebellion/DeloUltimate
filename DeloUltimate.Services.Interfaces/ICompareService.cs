using DeloUltimate.Domain.Entities.Base;
using System.Collections.Generic;

namespace DeloUltimate.Services.Interfaces
{
    public interface ICompareService<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetDifference(IEnumerable<TEntity> prevCollection, IEnumerable<TEntity> nextCollection);
    }
}
