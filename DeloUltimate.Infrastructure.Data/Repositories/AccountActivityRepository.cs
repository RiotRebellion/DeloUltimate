using DeloUltimate.Domain.Entities;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DeloUltimate.Infrastructure.Data.Repositories
{
    public class AccountActivityRepository : IGenericRepository<AccountActivity>
    {
        private readonly IDeloDbContext _dbContext;
        private readonly string _query =
            "USE DELO_DB " +
            "SELECT SURNAME_PATRON as AccountName, " +
            "ISNULL(MAX(DATE_TIME),  '1900-01-01 00:00:00') as LastAutorizationDate, " +
            "ISNULL(DATEDIFF(DAY, MAX(DATE_TIME), GETDATE()), 999999) as DaysGone " +
            "FROM CONNECTION_LOG " +
            "RIGHT JOIN USER_CL ON CONNECTION_LOG.DELO_USER = USER_CL.CLASSIF_NAME " +
            "WHERE DELETED LIKE 0 " +
            "AND SURNAME_PATRON LIKE '%%' " +
            "AND SURNAME_PATRON NOT LIKE '%[123456789]%' " +
            "AND SURNAME_PATRON NOT LIKE '%[b-Z]%' " +
            "GROUP BY SURNAME_PATRON " +
            "order by LastAutorizationDate desc ";


        public AccountActivityRepository(IDeloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AccountActivity> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_dbContext.ConnectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(_query, sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "AccountActivity");
                sqlConnection.Close();

                var result = dataSet.Tables["AccountActivity"].AsEnumerable()
                    .Select(dataRow => new AccountActivity
                    {
                        AccountName = dataRow.Field<string>("AccountName"),
                        LastAutorizationDate = dataRow.Field<DateTime>("LastAutorizationDate"),
                        DaysGone = dataRow.Field<int>("DaysGone"),
                    }).ToList<AccountActivity>();
                return result;
            }
        }
    }
}
