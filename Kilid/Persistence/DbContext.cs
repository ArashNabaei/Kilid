using Microsoft.Data.SqlClient;
using System.Data;

namespace Kilid.Persistence
{
    public class DbContext
    {
        public IDbConnection Connection { get; }

        private readonly IConfiguration _configuration;

        private bool disposed;

        public DbContext(IConfiguration configuration)
        {
            disposed = false;
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("MyDatabaseConnection"));
            Connection.Open();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    Connection.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
