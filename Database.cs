using System;
using MySqlConnector;

namespace portfolio_backend
{
    public class Database : IDisposable
    {
        public MySqlConnection Connection { get; }

        public Database(string connectionString)
        {
            Connection = new MySqlConnection(System.Environment.GetEnvironmentVariable("DATABASE_URL"));
        }

        public void Dispose() => Connection.Dispose();
    }
}
