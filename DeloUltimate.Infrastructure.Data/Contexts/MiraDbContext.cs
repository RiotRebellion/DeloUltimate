using DeloUltimate.Services.Interfaces;
using System.Configuration;

namespace DeloUltimate.Infrastructure.Data.Contexts
{
    public class MiraDbContext : IMiraDbContext
    {
        private readonly string _connectionString;


        public MiraDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Mira"].ConnectionString;
        }

        public string ConnectionString
        {
            get => _connectionString;
        }
    }
}
