using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Repository.Context
{
    public class DapperContext
    {
        private const string StringConnection = "SqlConnection";

        private readonly string connectionString;

        public DapperContext(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString(StringConnection);
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }
}
