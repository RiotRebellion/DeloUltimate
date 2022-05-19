using DeloUltimate.Domain.Entities;
using DeloUltimate.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DeloUltimate.Infrastructure.Data.Repositories
{
    public class AccountRepository : IGenericRepository<Account>
    {
        private readonly IDeloDbContext _dbContext;
        private readonly string _query =
            "select distinct e.CLASSIF_NAME as Department, " +
            "CABINET_NAME as Cabinet, " +
            "SURNAME_PATRON as Username, " +
            "FOLDERS_AVAILABLE AS FolderAccess " +
            "from DEPARTMENT D " +
                "JOIN DEPARTMENT E ON D.ISN_HIGH_NODE = E.ISN_NODE " +
                "FULL JOIN CABINET ON D.ISN_CABINET = CABINET.ISN_CABINET " +
                "FULL JOIN USER_CABINET ON CABINET.ISN_CABINET = USER_CABINET.ISN_CABINET " +
                "FULL JOIN USER_CL on USER_CABINET.ISN_LCLASSIF = USER_CL.ISN_LCLASSIF " +
            "WHERE e.IS_NODE = 0 " +
                "and CABINET_NAME IS NOT NULL " +
                "and e.DELETED = 0 " +
            "UNION ALL " +
            "select distinct e.CLASSIF_NAME as Department, " +
            "CABINET_NAME as Cabinet, " +
            "SURNAME_PATRON as Username, " +
            "FOLDERS_AVAILABLE AS FolderAccess " +
            "from CABINET " +
                "FULL JOIN DEPARTMENT D ON D.ISN_CABINET = CABINET.ISN_CABINET " +
                "FULL JOIN DEPARTMENT E ON D.ISN_HIGH_NODE = E.ISN_NODE " +
                "FULL JOIN USER_CABINET ON CABINET.ISN_CABINET = USER_CABINET.ISN_CABINET " +
                "FULL JOIN USER_CL ON USER_CABINET.ISN_LCLASSIF = USER_CL.ISN_LCLASSIF " +
            "WHERE USER_CABINET.FOLDERS_AVAILABLE is null " +
            "and USER_CL.DELETED = 0 ";

        public AccountRepository(IDeloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Account> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_dbContext.ConnectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(_query, sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Account");
                sqlConnection.Close();

                var result = dataSet.Tables["Account"].AsEnumerable()
                    .Select(dataRow => new Account
                    {
                        Department = dataRow.Field<string>("Department") ?? "", 
                        Cabinet = dataRow.Field<string>("Cabinet") ?? "",
                        Username = dataRow.Field<string>("Username") ?? "",
                        FolderAccess = dataRow.Field<string>("FolderAccess") ?? ""
                    }).ToList<Account>();
                return result;
            }
        }
    }
}
