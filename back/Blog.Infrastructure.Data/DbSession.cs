using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Blog.Infrastructure.Data
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection? Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        private readonly IConfiguration _configuration;

        public DbSession(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            Connection?.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
