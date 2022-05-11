using DeloUltimate.Domain.Entities.Base;

namespace DeloUltimate.Domain.Entities
{
    public class Account : IEntity
    {
        public string Department { get; set; }

        public string Cabinet { get; set; }

        public string Username { get; set; }

        public string FolderAccess { get; set; }
    }
}
