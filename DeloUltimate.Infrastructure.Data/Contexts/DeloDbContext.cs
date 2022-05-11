using DeloUltimate.Services.Interfaces;
using System.Configuration;
using System.Data.Entity;

namespace DeloUltimate.Infrastructure.Data.Contexts
{
    public class DeloDbContext : DbContext, IDeloDbContext
    {
        private readonly string _connectionString;


        public DeloDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Delo"].ConnectionString;
        }

        public string ConnectionString
        {
            get => _connectionString;
        }
    }
}
