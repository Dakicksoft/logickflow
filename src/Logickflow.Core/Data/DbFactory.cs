using System.Data;
using System.Data.SqlClient;

namespace Logickflow.Core.Data
{
    public class DbFactory
    {
        static internal string DbConnectionString => Configurations.AppSettings.Current.ConnectionString;

        internal IDbConnection DbConnection => new SqlConnection(DbConnectionString);

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(DbConnectionString);
        }

        public static DbContext GetDbContext()
        {
            var conn = GetConnection();
            conn.Open();
            var trans = conn.BeginTransaction();
            return new DbContext(conn, trans);
        }
    }
}