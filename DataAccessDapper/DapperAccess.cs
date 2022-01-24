using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace DataAccessDapper
{
    public class DapperAccess
    {
        public string ConnectionString { get; }
        private const string ReadInvestorByIdQuery = "SELECT * FROM [Stock].[dbo].[Investors] WHERE Id = @Id";
        private const string ReadInvestorQuery = "SELECT * FROM [Stock].[dbo].[Investors]";
        private const string AddInvestorQuery = "INSERT INTO Investors (LegacyCode) OUTPUT INSERTED.Id VALUES (@LegacyCode)";
        private const string DeleteInvestorQuery = "DELETE FROM [Stock].[dbo].[Investors] WHERE Id = @Id";
        public DapperAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<List<Investor>> ReadInvestorById(int Id)
        {
            await using var db = new SqlConnection(ConnectionString);
            var result = await db.QueryAsync<Investor>(sql: ReadInvestorByIdQuery, new { Id = Id });

            return result.ToList();
        }

        public async Task<List<Investor>> ReadAllInvestor()
        {
            await using var db = new SqlConnection(ConnectionString);
            var result = await db.QueryAsync<Investor>(ReadInvestorQuery);
            return result.ToList();
        }


        public async Task<int> AddInvestor(string legacyCode)
        {
            await using var db = new SqlConnection(ConnectionString);
            var result = await db.ExecuteScalarAsync<int>(AddInvestorQuery, new { LegacyCode = legacyCode });
            return result;
        }

        public async Task<int> DeleteInvestor(int id)
        {
            await using var db = new SqlConnection(ConnectionString);
            var result = await db.ExecuteScalarAsync<int>(DeleteInvestorQuery, new { Id = id });
            return result;
        }
    }
}