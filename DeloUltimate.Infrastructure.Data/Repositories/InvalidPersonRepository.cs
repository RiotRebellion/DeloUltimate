using DeloUltimate.Domain.Entities;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeloUltimate.Infrastructure.Data.Repositories
{
    public class InvalidPersonRepository : IGenericRepository<InvalidPerson>
    {
        private readonly IDeloDbContext _dbContext;
        private readonly string _query =
            "USE DELO_DB " +
            "SELECT SURNAME_PATRON as Name " +
            "FROM USER_CL AS U " +
            "JOIN DEPARTMENT AS D " +
            "ON U.DUE_DEP = D.DUE " +
            "WHERE U.DELETED = 0 " +
            "AND D.DELETED = 1";

        public InvalidPersonRepository(IDeloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<InvalidPerson> GetAll() 
        {
            using (SqlConnection sqlConnection = new SqlConnection(_dbContext.ConnectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(_query, sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "InvalidPerson");
                sqlConnection.Close();

                var result = dataSet.Tables["InvalidPerson"].AsEnumerable()
                    .Select(dataRow => new InvalidPerson
                    {
                        Name = dataRow.Field<string>("Name")
                    }).ToList<InvalidPerson>();
                return result;
            }
        }
    }
}
