using DeloUltimate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeloUltimate.Services.Interfaces
{
    public interface IDataExportService<T>
        where T: class, IEntity
    {
        void ExportToXML(IEnumerable<T> entityCollection);

        void ExportToExcel(IEnumerable<T> entityCollection);
    }
}
