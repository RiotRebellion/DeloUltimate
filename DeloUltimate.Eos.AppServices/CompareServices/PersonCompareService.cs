using DeloUltimate.Domain.Entities;
using DeloUltimate.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DeloUltimate.Eos.AppServices.CompareServices
{
    public class PersonCompareService : ICompareService<Employee>
    {
        public IEnumerable<Employee> GetCollectionsDifference(IEnumerable<Employee> prevCollection, IEnumerable<Employee> nextCollection)
        {
            IEnumerable<Employee> temp;
            IEnumerable<Employee> result = new List<Employee>();

            //Добавляем новых
            temp = nextCollection.Except(prevCollection).ToList();
            result = result.Union(temp);

            //Добавляем удаленных и помечаем их
            temp = prevCollection.Except(nextCollection).ToList();
            foreach (var item in temp) item.Status = 1;

            result = result.Union(temp);

            return result;
        }
    }
}
