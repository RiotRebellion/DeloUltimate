using DeloUltimate.Domain.Entities;
using DeloUltimate.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DeloUltimate.Eos.AppServices.CompareServices
{
    public class PersonCompareService : ICompareService<Employee>
    {
        public IEnumerable<Employee> GetDifference(IEnumerable<Employee> prevCollection, IEnumerable<Employee> nextCollection)
        {
            IEnumerable<Employee> temp;
            IEnumerable<Employee> result = new List<Employee>();

            //Добавляем новых
            temp = nextCollection.Except(prevCollection).ToList();
            result.Concat(temp);

            //Добавляем удаленных
            temp = prevCollection.Except(nextCollection).ToList();

            foreach (var item in temp) item.Status = 1;

            return result.Concat(temp);
            
        }
    }
}
