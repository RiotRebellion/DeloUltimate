using DeloUltimate.Domain.Entities.Base;

namespace DeloUltimate.Domain.Entities
{
    public class Cabinet : IEntity
    {
        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public string ParentDepartment { get; set; }

        public string CabinetName { get; set; }

        public string CabinetDepartment { get; set; }
    }
}
