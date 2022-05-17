using DeloUltimate.Domain.Entities;
using DeloUltimate.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DeloUltimate.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : IGenericRepository<Employee>
    {
        private readonly IMiraDbContext _dbContext;
        private readonly string _query =
            "SELECT [c1staff].id as Id, " +
            "[c1staff].name AS Name, " +
            "[c1department].name AS Department, " +
            "[c1staff].post AS Post, " +
            "[c1staff].deleted AS Status " +
            "FROM [c1staff] " +
            "JOIN [c1department] ON [c1staff].departmentGUID = [c1department].id " +
            "WHERE [c1staff].deleted = 0 " +
            "and [c1staff].post not like '%Рецензент диплом%' " +
			"and [c1staff].post not like '' " +
			"and [c1staff].post not like '%ГПХ%' " +
            "and [c1staff].post not like '%Член ГЭК%' " +
            "ORDER BY Department, Name";

        public EmployeeRepository(IMiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_dbContext.ConnectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(_query, sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "employee");
                sqlConnection.Close();

                var result = dataSet.Tables["employee"].AsEnumerable()
                    .Select(dataRow => new Employee
                    {
                        Id = dataRow.Field<string>("Id"),
                        Name = dataRow.Field<string>("Name"),
                        Department = dataRow.Field<string>("Department"),
                        Post = dataRow.Field<string>("Post"),
                        Status = dataRow.Field<short>("Status")
                    }).ToList<Employee>();
                return result;
            }
        }
    }
}
