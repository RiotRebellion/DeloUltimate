using DeloUltimate.Domain.Entities.Base;
using System.Collections.Generic;

namespace DeloUltimate.Domain.Entities
{

    public class Employee : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Post { get; set; }

        public short Status { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   Id == employee.Id &&
                   Status == employee.Status;
        }

        public override int GetHashCode()
        {
            int hashCode = -773843914;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Department);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Post);
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            return hashCode;
        }
    }
}
