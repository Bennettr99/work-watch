using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace WorkWatch.Services
{
    public class DataAccessService
    {
        private readonly string _sqlConnectionString;

        public DataAccessService(string sqlServerAddress)
        {
            _sqlConnectionString = $"Server={sqlServerAddress};Database=WorkWatch;Trusted_Connection=True";
        }

        public async Task<int> AddUser(string username, string machineName)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                var results = await connection.QueryAsync<int>("WorkWatch.User_I",
                    new {Username = username, MachineName = machineName},
                    commandType: CommandType.StoredProcedure);

                return results.Single();
            }
        }

        public async Task<int> AddApplication(int userId, string applicationName)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                var results = await connection.QueryAsync<int>("WorkWatch.Application_I",
                    new { UserID = userId, Name = applicationName },
                    commandType: CommandType.StoredProcedure);

                return results.Single();
            }
        }

        public async Task<int> AddInput(int userId, int applicationId)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                var results = await connection.QueryAsync<int>("WorkWatch.Input_I",
                    new { UserID = userId, ApplicationID = applicationId },
                    commandType: CommandType.StoredProcedure);

                return results.Single();
            }
        }

        public async Task UpdateInput(int inputId)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                await connection.ExecuteAsync("WorkWatch.Input_U",
                    new { InputID = inputId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}