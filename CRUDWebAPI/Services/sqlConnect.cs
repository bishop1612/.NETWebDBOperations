//This class will setup connections to our sql db
using MySqlConnector;

namespace CRUDWebAPI.Models
{
	public class sqlConnect : IDisposable
    {
        public MySqlConnection Connection { get; }

        public sqlConnect(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}

